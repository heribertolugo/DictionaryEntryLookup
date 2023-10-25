Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml.Serialization


''' <summary>
''' DataBase Manager for managing an Entry object in a DataBase.
''' </summary>
''' <remarks>The database must have 1 table called Words. In that table 3 columns. First is RowID, a identity row of int. Second a Word column of varchar. Third, a OEntry column of type xml. 
''' The databse connection string is kept in the settings xml file, usually found in appdata folder.</remarks>
Public Class DBMan
    Private Shared _currentEntryId As Integer

    ''' <summary>
    ''' Inserts a Entry into database.
    ''' </summary>
    ''' <param name="entry">A non-empty Entry object.</param>
    ''' <returns>Return true if Entry was succesfully inserted into database, false otherwise.</returns>
    ''' <remarks></remarks>
    Public Shared Function AddEntry(ByVal entry As Entry) As Boolean
        Dim eXmlObject As System.Data.SqlTypes.SqlXml = EntryToXml(entry)

        entryCmd.CommandText = "INSERT INTO Words (Word, OEntry) VALUES (@word, @oEntry) SELECT CAST(scope_identity() AS int) "
        SetConnection(dicDBConn)

        Try
            entryCmd.Parameters.Add("@word", SqlDbType.VarChar).Value = entry.Word
            entryCmd.Parameters.Add("@oEntry", SqlDbType.Xml).Value = eXmlObject

            _currentEntryId = CInt(entryCmd.ExecuteScalar())

            entryCmd.Parameters.Clear()
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)
            Return True
        Catch ex As System.Data.SqlClient.SqlException
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updates the data for an Entry in database.
    ''' </summary>
    ''' <param name="entryID">The unique ID given to an entry by identity in database.</param>
    ''' <param name="entry">A non-empty Entry object.</param>
    ''' <returns>Return true if Entry was succesfully updated into database, false otherwise.</returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateEntry(ByVal entryID As Integer, ByVal entry As Entry) As Boolean
        Dim eXmlObject As System.Data.SqlTypes.SqlXml = EntryToXml(Entry)

        entryCmd.CommandText = "UPDATE Words SET Word=@word, OEntry=@oEntry WHERE RowID=@rowId"
        SetConnection(dicDBConn)

        Try
            entryCmd.Parameters.Add("@rowId", SqlDbType.Int).Value = entryID
            entryCmd.Parameters.Add("@word", SqlDbType.VarChar).Value = entry.Word
            entryCmd.Parameters.Add("@oEntry", SqlDbType.Xml).Value = eXmlObject

            entryCmd.ExecuteNonQuery()

            entryCmd.Parameters.Clear()
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)
            _currentEntryId = entryID
            Return True
        Catch ex As System.Data.SqlClient.SqlException
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Deletes an Entry from the databse.
    ''' </summary>
    ''' <param name="entryID">The unique ID given to an entry by identity in database.</param>
    ''' <returns>A non-empty Entry object.</returns>
    ''' <remarks>Return true if Entry was succesfully deleted from database, false otherwise.</remarks>
    Public Shared Function DeleteEntry(ByVal entryID As Integer) As Boolean
        Dim rowsDeleted As Integer

        entryCmd.CommandText = "DELETE FROM Words WHERE RowID=@rowId"
        SetConnection(dicDBConn)

        Try
            entryCmd.Parameters.Add("@rowId", SqlDbType.Int).Value = entryID

            rowsDeleted = entryCmd.ExecuteNonQuery()

            entryCmd.Parameters.Clear()
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)

            If rowsDeleted > 0 Then
                _currentEntryId = 0
                Return True
            Else
                Return False
            End If

        Catch ex As System.Data.SqlClient.SqlException
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Search for Entry's in database by matching part of word, or entire word.
    ''' </summary>
    ''' <param name="chars">Any combination of characters in string format to match to a word belonging to an Entry in database.</param>
    ''' <param name="useAsWildcard">If set to true, will search for all Entry's whos word begins with the string passed through chars parameter.</param>
    ''' <returns>Returns a Dictionary List object of all matching Entry's found in database. ID number as key, Entry as value.</returns>
    ''' <remarks></remarks>
    Public Shared Function FindEntries(ByVal chars As String, Optional ByVal useAsWildcard As Boolean = False) As Dictionary(Of Integer, Entry)
        Dim entries As New Dictionary(Of Integer, Entry)
        Dim queryData As SqlDataReader
        Dim xmlEntry As System.Data.SqlTypes.SqlXml

        entryCmd.CommandText = If(useAsWildcard, "SELECT * FROM Words WHERE Word LIKE '" & chars & "%' ORDER BY Word", "SELECT * FROM Words WHERE Word = '" & chars & "' ORDER BY Word")
        SetConnection(dicDBConn)

        Try
            'entryCmd.Parameters.Add("@charcs", SqlDbType.VarChar).Value = chars

            queryData = entryCmd.ExecuteReader()

            While queryData.Read
                xmlEntry = queryData.GetSqlXml(2)
                entries.Add(CInt(queryData("RowID")), XmlToEntry(xmlEntry))
            End While

            entryCmd.Parameters.Clear()
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)
        Catch ex As System.Data.SqlClient.SqlException
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)
        End Try

        Return entries
    End Function

    ''' <summary>
    ''' Search for an Entry in database by ID number.
    ''' </summary>
    ''' <param name="id">The unique ID given to an entry by identity in database.</param>
    ''' <returns>Returns an Entry object whos ID matches the ID passed as parameter. </returns>
    ''' <remarks>A Nothing Entry returned if no match found.</remarks>
    Public Shared Function FindEntryById(ByVal id As Integer) As Entry
        Dim entry As New Entry
        Dim queryData As SqlDataReader
        Dim xmlEntry As System.Data.SqlTypes.SqlXml

        entryCmd.CommandText = "SELECT * FROM Words WHERE RowID = @id"
        SetConnection(dicDBConn)

        Try
            entryCmd.Parameters.Add("@id", SqlDbType.Int).Value = id

            queryData = entryCmd.ExecuteReader()

            If queryData.HasRows Then
                queryData.Read()
                xmlEntry = queryData.GetSqlXml(2)
                entry = XmlToEntry(xmlEntry)
            Else
                entry = Nothing
            End If

            entryCmd.Parameters.Clear()
            entryCmd.CommandText = Nothing
            SetConnection(dicDBConn)
        Catch ex As System.Data.SqlClient.SqlException
            entry = Nothing
            SetConnection(dicDBConn)
        End Try

        Return entry
    End Function

    ''' <summary>
    ''' Gets the ID number of the last Entry object.
    ''' </summary>
    ''' <value></value>
    ''' <returns>integer value of the last Entry object. </returns>
    ''' <remarks>Note: any searches for an Entry will not be reflected. 
    ''' Only edits to an Entry, either by updating an existing Entry, or creation of a new Entry.
    ''' Deleted Entry will set to 0.</remarks>
    Public Shared ReadOnly Property CurrentEntryId As Integer
        Get
            Return _currentEntryId
        End Get
    End Property

    ''' <summary>
    ''' Sets connection for sql db server to open
    ''' </summary>
    ''' <param name="conn">A connection to a db server</param>
    ''' <remarks></remarks>
    Public Shared Sub SetConnection(Optional ByVal conn As SqlConnection = Nothing)
        Dim sConn As SqlConnection = If(conn Is Nothing, dicDBConn, conn)

        With sConn
            Select Case .State
                Case ConnectionState.Open
                    .Close()
                    .Open()
                    Exit Select
                Case ConnectionState.Closed
                    .Open()
                    Exit Select
                Case Else
                    .Close()
                    .Open()
                    Exit Select
            End Select
        End With
    End Sub

    Public Shared Function IsDbConnected(Optional ByVal conn As SqlConnection = Nothing) As Boolean
        Dim sConn As SqlConnection = If(conn Is Nothing, dicDBConn, conn)

        If sConn.State = ConnectionState.Open Then Return True

        Return False
    End Function

    Public Shared Function DbConnectState(Optional ByVal conn As SqlConnection = Nothing) As ConnectionState
        Dim sConn As SqlConnection = If(conn Is Nothing, dicDBConn, conn)

        Return sConn.State
    End Function



    ''' <summary>
    ''' Serializes an Entry to XML.
    ''' </summary>
    ''' <param name="entry">Valid Entry object.</param>
    ''' <returns>Returns a serialized Entry as XML.</returns>
    ''' <remarks></remarks>
    Private Shared Function EntryToXml(ByVal entry As Entry) As SqlTypes.SqlXml
        Dim x As New XmlSerializer(entry.GetType)
        Dim xmlAsString As New StringWriter
        x.Serialize(xmlAsString, entry)

        Dim objData As System.Data.SqlTypes.SqlXml = New System.Data.SqlTypes.SqlXml(New System.Xml.XmlTextReader(xmlAsString.ToString, System.Xml.XmlNodeType.Document, Nothing))

        xmlAsString.Close()
        Return objData
    End Function

    ''' <summary>
    ''' De-serializes XML to an Entry object.
    ''' </summary>
    ''' <param name="xmlEntry">valid serialized Entry as SQL XML</param>
    ''' <returns>Returns an Entry object representing the XML passed as parameter.</returns>
    ''' <remarks></remarks>
    Private Shared Function XmlToEntry(ByVal xmlEntry As System.Data.SqlTypes.SqlXml) As Entry
        Dim x As New XmlSerializer(GetType(Entry))
        Dim xmlAsString As StringReader
        Dim entry As Entry

        xmlAsString = New StringReader(xmlEntry.Value)

        entry = x.Deserialize(xmlAsString)

        Return entry
    End Function

    ''' <summary>
    ''' De-serializes XML to an Entry object.
    ''' </summary>
    ''' <param name="xmlEntry">valid serialized Entry as XML string</param>
    ''' <returns>Returns an Entry object representing the XML passed as parameter.</returns>
    ''' <remarks></remarks>
    Private Shared Function XmlToEntry(ByVal xmlEntry As String) As Entry
        Dim x As New XmlSerializer(GetType(Entry))
        Dim xmlAsString As StringReader
        Dim entry As Entry

        xmlAsString = New StringReader(xmlEntry)

        entry = x.Deserialize(xmlAsString)

        Return entry
    End Function

    Public Shared Sub SetConnTimeOut(ByVal timeout As Integer)
        With dicDBConn
            Dim timoutIndex As Integer = .ConnectionString.IndexOf("Connection Timeout")
            Dim conString As String = If(timoutIndex > 0, .ConnectionString.Substring(0, timoutIndex - 1), .ConnectionString)

            dicDBConn.ConnectionString = conString & ";Connection Timeout=" & timeout.ToString
        End With
    End Sub

    '*** The db source is swapped for debug session, and live seesion prior to publishing. Yes a sub-routine couldve been made to do this automatically.
    '
    '"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\Documents\Projects\Visual Studio 2010\Projects\Dictionary\Dictionary\DictionaryDB.mdf;Integrated Security=True;User Instance=True"
    '
    Private Shared WithEvents dicDBConn As New SqlConnection(My.Settings.DictionaryDBConnectionString)
    Private Shared entryCmd As New SqlCommand("", dicDBConn)

End Class
