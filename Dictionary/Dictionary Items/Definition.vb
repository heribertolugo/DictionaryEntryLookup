Imports System.Xml.Serialization

''' <summary>
''' A Definition for a Entry (word).
''' </summary>
''' <remarks>A Definition is comprised of different components. The actual "meaning" in a definition is kept in a Term. 
''' The Term Item is the de-facto meaning for a Entry (word). A Definition is based on it's Part Of Speech. Under this 
''' particular Part Of Speech, different Terms (different meanings) can exist for a Entry (word). A Definition also has 
''' a note, which is for additional info about this Definition, such as it's plural use. A Term within a Definition is 
''' the Term Item (actual meaning), and a Note. The Term's Note is generally used for sample sentences to demonstrate this 
''' particular usage of the Entry (word). A Definition can have Multiple Terms.</remarks>
Public Class Definition
    Private _partOfSpeech As String
    Private _terms As List(Of Term)
    Private _note As String

    ''' <summary>
    ''' Initializes a new instance of a Definition Object.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _terms = New List(Of Term)
    End Sub

    ''' <summary>
    ''' Gets or Sets the Part of Speach for this definition.
    ''' </summary>
    ''' <value>A string value representing the Definition's Part of Speech.</value>
    ''' <returns>A string value representing the Definition's Part of Speech.</returns>
    ''' <remarks></remarks>
    <XmlAttribute(AttributeName:="PartOfSpeech")> _
    Public Property PartOfSpeech As String
        Get
            Return _partOfSpeech
        End Get
        Set(value As String)
            _partOfSpeech = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the collection of Terms in the Definition as Generic List.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlArray(ElementName:="Terms")> _
    Public ReadOnly Property Terms As List(Of Term)
        Get
            Return _terms
        End Get
    End Property

    ''' <summary>
    ''' Gets or Sets the note for this Definition.
    ''' </summary>
    ''' <value>A string value representing the Definition's Note.</value>
    ''' <returns>A string value representing the Definition's Note.</returns>
    ''' <remarks></remarks>
    <XmlElementAttribute(ElementName:="Note")> _
    Public Property Note As String
        Get
            Return _note
        End Get
        Set(value As String)
            _note = value
        End Set
    End Property

    ''' <summary>
    ''' Adds a new Term to the Definition.
    ''' </summary>
    ''' <param name="item">The Term item (actual meaning in a Definition for a word). </param>
    ''' <param name="note">A note for this Term in a Definition, ie: a sample sentence of word usage under this "Term".</param>
    ''' <remarks></remarks>
    Public Sub AddTerm(ByVal item As String, Optional ByVal note As String = Nothing)
        If _terms.Count > 0 Then
            Dim dups = _terms.Where(Function(i) i.TermItem = item)
            If dups.Count > 0 Then Exit Sub
        End If
        Dim nTerm As New Term
        nTerm.TermItem = item
        nTerm.Note = note

        _terms.Add(nTerm)
    End Sub

    ''' <summary>
    ''' Updates data in a Term.
    ''' </summary>
    ''' <param name="itemNumber">The index number of the Term within the Terms Collection in the Definition.</param>
    ''' <param name="item">A new value for the item in a Term (a meaning of a word in this Term). Optional, if not set, the prior value for item will be used.</param>
    ''' <param name="note">A new value for the note in a Term. Optional, if not set, the prior value for item will be used.</param>
    ''' <remarks></remarks>
    Public Sub EditTerm(ByVal itemNumber As Integer, Optional ByVal item As String = Nothing, Optional ByVal note As String = "foobarfubar1979")
        Dim lastIndex As Integer = _terms.Count - 1
        If itemNumber > lastIndex Then Exit Sub

        Dim nTerm As New Term
        nTerm.TermItem = IIf(item = Nothing, _terms(itemNumber).TermItem, item)
        nTerm.Note = IIf(note = "foobarfubar1979", _terms(itemNumber).Note, note)

        _terms.Item(itemNumber) = nTerm
    End Sub

    ''' <summary>
    ''' Deletes a Term from the Definition.
    ''' </summary>
    ''' <param name="itemNumber">The index number of the Term within the Terms Collection in the Definition.</param>
    ''' <remarks></remarks>
    Public Sub DeleteTerm(ByVal itemNumber As Integer)
        Dim lastIndex As Integer = _terms.Count - 1
        If itemNumber > lastIndex Then Exit Sub

        _terms.RemoveAt(itemNumber)
    End Sub

    ''' <summary>
    ''' Resturns a string of a definition.
    ''' </summary>
    ''' <returns>Returns the first Term in the definition.</returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return If(Me.Terms.Count > 0, Me.Terms(0).TermItem, Nothing)
    End Function
End Class

''' <summary>
''' A definition Term.
''' </summary>
''' <remarks>Definition Term is the actual part of a definition where the meaning to a word is kept. 
''' A term is made up of the item (meaning) and a note (which can be a sample sentence) for this meaning.</remarks>
Public Class Term
    Private _termItem As String
    Private _note As String

    ''' <summary>
    ''' Gets or Sets the Term Item in this Term.
    ''' </summary>
    ''' <value>A string value representing a Term Item for this Term.</value>
    ''' <returns>A string value representing a Term Item for this Term.</returns>
    ''' <remarks>A Term Item is the actual meaning part of a Definition for a Entry (word).</remarks>
    <XmlElementAttribute(ElementName:="Item")> _
    Public Property TermItem As String
        Get
            Return _termItem
        End Get
        Set(value As String)
            _termItem = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the Note in this Term.
    ''' </summary>
    ''' <value>A string value representing a Note for this Term.</value>
    ''' <returns>A string value representing a Note for this Term.</returns>
    ''' <remarks>A note for this Term in a Definition, ie: a sample sentence of word usage under this "Term".</remarks>
    <XmlElementAttribute(ElementName:="Note")> _
    Public Property Note As String
        Get
            Return _note
        End Get
        Set(value As String)
            _note = value
        End Set
    End Property
End Class
