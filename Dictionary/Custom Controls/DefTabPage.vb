Imports System.ComponentModel

Public Class DefTabPage
    Inherits TabPage
    Public Event NewTermAdded As EventHandler
    Public Event NewTermSaved As EventHandler
    Public Event PartOfSpeechEdited As EventHandler
    Public Event TermSelected As EventHandler

    Private _marginLeft As Integer = 20
    Private _marginTop As Integer = 20
    Private _controlKey As String
    Private _definition As New Definition
    Private textSet As Boolean = False
    Private _saved As Boolean = False
    Private Const defaultTerm As String = "Definition Term"
    Private Const defaultTermNote As String = "Definition Term Note (ie: an example sentence)"
    Private _objectNames As DefTabPageObjectNames
    Private savedColor As Color = Color.DarkSeaGreen
    Private unSavedColor As Color = Color.FromKnownColor(KnownColor.Control)


#Region "Constructors and Initialization"

    ''' <summary>
    ''' Creates a new instance of DefTabPage.
    ''' </summary>
    ''' <param name="controlKey">A unique string which will be used for the naming of controls in DefTabPage.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal controlKey As String)
        _controlKey = controlKey

        Me.Init()

        _objectNames = New DefTabPageObjectNames
    End Sub

    ''' <summary>
    ''' Creates a new instance of DefTabPage.
    ''' </summary>
    ''' <remarks>The controlKey will be randomly generated.</remarks>
    Public Sub New()
        Dim uid As Guid = Guid.NewGuid
        Me._controlKey = uid.ToString

        Me.Init()

    End Sub

    ''' <summary>
    ''' Initial preocessing.
    ''' </summary>
    ''' <remarks>Sets the objects which will be in the DefTabPage</remarks>
    Private Sub Init()
        Me.SuspendLayout()

        Me.PartOfSpeechLbl = New System.Windows.Forms.Label
        Me.PartOfSpeechLbl.Name = "PartOfSpeech_Lbl_" & _controlKey
        Me.PartOfSpeechLbl.Location = New Point(MarginLeft, MarginTop)
        Me.PartOfSpeechLbl.Text = "Part Of Speech"
        Me.PartOfSpeechLbl.AutoSize = True


        Me.PartOfSpeechBox = New TextBox
        Me.PartOfSpeechBox.Name = "PartOfSpeech_Box_" & _controlKey
        Me.PartOfSpeechBox.Location = New Point(PartOfSpeechLbl.Location.X, PartOfSpeechLbl.Bottom - 5)
        Me.PartOfSpeechBox.Size = New Size(143, 20)
        Me.PartOfSpeechBox.AutoCompleteMode = AutoCompleteMode.Suggest
        Dim poSpeechList As New AutoCompleteStringCollection
        poSpeechList.AddRange({"Verb", "Noun", "Adjective", "Adverb", "Pronoun", "Preposition", "Conjunction", "Interjection", "Transative Verb", "Intransitive Verb", "Article"})
        Me.PartOfSpeechBox.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.PartOfSpeechBox.AutoCompleteCustomSource = poSpeechList


        Me.DefinitionLbl = New Label
        Me.DefinitionLbl.Name = "Definition_Lbl_" & _controlKey
        Me.DefinitionLbl.Location = New Point(Me.PartOfSpeechBox.Location.X + Me.PartOfSpeechBox.Width + 20, Me.PartOfSpeechLbl.Location.Y)
        Me.DefinitionLbl.Text = "Definition Note:"
        Me.DefinitionLbl.AutoSize = True


        Me.DefinitionNoteBox = New TextBox
        Me.DefinitionNoteBox.Name = "DefinitionNote_Box_" & _controlKey
        Me.DefinitionNoteBox.Location = New Point(Me.DefinitionLbl.Location.X, Me.DefinitionLbl.Bottom - 5)
        Me.DefinitionNoteBox.Size = New Size(225, 20)


        Me.TermsCount = New System.Windows.Forms.NumericUpDown
        Me.TermsCount.Name = "TermsCount_NUD_" & _controlKey
        Me.TermsCount.Location = New Point(MarginLeft, MarginTop)
        Me.TermsCount.Size = New Size(43, 20)
        Me.TermsCount.Minimum = 0
        Me.TermsCount.Maximum = 0


        Me.TermsBox = New System.Windows.Forms.TextBox
        Me.TermsBox.Name = "Terms_Box_" & _controlKey
        Me.TermsBox.ForeColor = Color.DarkGray
        Me.TermsBox.Text = defaultTerm
        Me.TermsBox.Size = New Size(247, 40)
        Me.TermsBox.Location = New Point(Me.TermsCount.Right + 1, Me.MarginTop)
        Me.TermsBox.Multiline = True


        Me.TermsNoteBox = New System.Windows.Forms.TextBox
        Me.TermsNoteBox.Name = "TermsNote_Box_" & _controlKey
        Me.TermsNoteBox.ForeColor = Color.DarkGray
        Me.TermsNoteBox.Text = defaultTermNote
        Me.TermsNoteBox.Size = New Size(247, 20)
        Me.TermsNoteBox.Location = New Point(Me.TermsCount.Right + 1, Me.TermsBox.Bottom + 5)


        Me.TermsNewBut = New System.Windows.Forms.Button
        Me.TermsNewBut.Name = "TermsNew_But_" & _controlKey
        Me.TermsNewBut.Size = New Size(75, 23)
        Me.TermsNewBut.Location = New Point(Me.TermsBox.Right + 5, Me.TermsBox.Top)
        Me.TermsNewBut.Text = "New"
        Me.TermsNewBut.BackColor = unSavedColor


        Me.TermsSaveBut = New System.Windows.Forms.Button
        Me.TermsSaveBut.Name = "TermsSave_But_" & _controlKey
        Me.TermsSaveBut.Size = New Size(75, 23)
        Me.TermsSaveBut.Location = New Point(Me.TermsNoteBox.Right + 5, Me.TermsNoteBox.Bottom - Me.TermsSaveBut.Height)
        Me.TermsSaveBut.Text = "Save"
        Me.TermsSaveBut.Enabled = False
        Me.TermsSaveBut.BackColor = unSavedColor


        Dim termsGBoxCSize As New Size
        termsGBoxCSize.Height = Me.TermsNoteBox.Bottom - Me.TermsBox.Top
        termsGBoxCSize.Width = Me.TermsNewBut.Right - Me.TermsCount.Left
        Me.TermsGBox = New GroupBox
        Me.TermsGBox.SuspendLayout()
        Me.TermsGBox.Name = "Terms_GBox" & _controlKey
        Me.TermsGBox.Text = "Terms"
        Me.TermsGBox.Size = New Size(termsGBoxCSize.Width + 40, termsGBoxCSize.Height + 40)
        Me.TermsGBox.Location = New Point(Me.MarginLeft, Me.PartOfSpeechBox.Bottom + 5)
        Me.TermsGBox.Controls.Add(TermsCount)
        Me.TermsGBox.Controls.Add(TermsBox)
        Me.TermsGBox.Controls.Add(TermsNoteBox)
        Me.TermsGBox.Controls.Add(TermsNewBut)
        Me.TermsGBox.Controls.Add(TermsSaveBut)

        MyBase.Controls.Add(PartOfSpeechLbl)
        MyBase.Controls.Add(PartOfSpeechBox)
        MyBase.Controls.Add(DefinitionLbl)
        MyBase.Controls.Add(DefinitionNoteBox)
        MyBase.Controls.Add(TermsGBox)


        Me.TermsGBox.ResumeLayout(False)
        Me.TermsGBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region

#Region "Properties"

    ''' <summary>
    ''' Get or Set the distance between the left boundry of DefTabePage and the controls that are placed on left side of the tabpage.
    ''' </summary>
    ''' <value>Margin value as integer</value>
    ''' <returns>Return distance between DefTabPage left boundry and the controls in tabpage</returns>
    ''' <remarks>Unlike margin property, this works for margin inside tabpage, like padding. It is used for aligning the placement of controls in DefTabPage.</remarks>
    Public Property MarginLeft As Integer
        Get
            Return _marginLeft
        End Get
        Set(value As Integer)
            _marginLeft = value
        End Set
    End Property

    ''' <summary>
    '''  Get or Set the distance between the top boundry of DefTabePage and the controls that are placed on top side of the tabpage.
    ''' </summary>
    ''' <value>Margin value as integer</value>
    ''' <returns>Return distance between DefTabPage top boundry and the controls in tabpage</returns>
    ''' <remarks>Unlike margin property, this works for margin inside tabpage, like padding. It is used for aligning the placement of controls in DefTabPage.</remarks>
    Public Property MarginTop As Integer
        Get
            Return _marginTop
        End Get
        Set(value As Integer)
            _marginTop = value
        End Set
    End Property

    ''' <summary>
    '''  Get or Set whether the definition has been saved. 
    ''' </summary>
    ''' <value>Boolean, true if saved, false if not saved.</value>
    ''' <returns>Boolean, true if saved, false if not saved.</returns>
    ''' <remarks>This will set the background color of the DefTabPage to the savedColor property value if saved, or to the unSavedColor property value if not saved.</remarks>
    Public Property Saved As Boolean
        Get
            Return _saved
        End Get
        Set(value As Boolean)
            _saved = value
            If value Then
                Me.BackColor = savedColor
            Else
                Me.BackColor = unSavedColor
            End If
        End Set
    End Property

    ''' <summary>
    ''' A unique string for the naming of controls in DefTabPage.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns a string containing the unique string which is appended to the name of each control in DefTabPage.</returns>
    ''' <remarks>To get the names which must be appended to ControlKey to get the actual complete name of the control, please use ObjectNames property.</remarks>
    Public ReadOnly Property ControlKey As String
        Get
            Return _controlKey
        End Get
    End Property

    ''' <summary>
    '''  Get or Set the Definition belonging to a Dictionary Entry. 
    ''' </summary>
    ''' <value>A Definition object to set as the current Definition in DefTabPage.</value>
    ''' <returns>The current Definition as a Definition object in DefTabPage.</returns>
    ''' <remarks></remarks>
    Public Property Definition As Definition
        Get
            Return _definition
        End Get
        Set(value As Definition)
            '_definition = value
            SetDefinition(value)
        End Set
    End Property

    ''' <summary>
    ''' Names of the objects in a DefTabPage.
    ''' </summary>
    ''' <remarks>These are the absolute names. The actual name for each instance of the DefTabPage object would be 
    ''' the absolute name with the controlKey appended to it.</remarks>
    Public ReadOnly Property ObjectNames As DefTabPageObjectNames
        Get
            Return _objectNames
        End Get
    End Property

    ''' <summary>
    ''' The text displayed on the tabpage
    ''' </summary>
    ''' <value>A string containing the text to display on tab header</value>
    ''' <returns>The text displayed on header for tabpage</returns>
    ''' <remarks>This is overridden so the textset property can be set. Textset property is used to determine whether or not default tab text should be used.</remarks>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(value As String)
            MyBase.Text = value
            textSet = True
        End Set
    End Property

#End Region

#Region "Event Handlers"

#Region "Overrides"

    ''' <summary>
    ''' Event handler for when the parent of the tabpage is changed, or initially set.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks>This will set the default tab text if the text has not been set.</remarks>
    Protected Overrides Sub OnParentChanged(e As System.EventArgs)
        MyBase.OnParentChanged(e)
        If Me.textSet = False Then Me.Text = CType(MyBase.Parent, System.Windows.Forms.TabControl).TabCount.ToString
    End Sub

#End Region

#Region "DefTabPage Controls"

    ''' <summary>
    ''' Sets DefTabPage for allowing a new Term to be added to definition.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will raise the NewTermAdded event if conditions are met to allow a new term to be added.</remarks>
    Private Sub NewTerm(ByVal sender As Object, ByVal e As EventArgs) Handles TermsNewBut.Click
        Dim ea As New DefTabPageEventArgs(_controlKey, e)

        With Me.TermsCount

            'If the previous Term was saved, we would expect TermsCount to have incremented.
            'If previous Term was not saved, do not allow a new term to be able to be added.
            If .Maximum <> Me.Definition.Terms.Count Then Exit Sub

            .Maximum = .Maximum + 1

            If .Minimum = 0 Then
                .Minimum = 1
            Else
                Me.TermsBox.Text = Nothing
                Me.TermsNoteBox.Text = Nothing

                Me.UnSetTermBox(Me.TermsBox, System.EventArgs.Empty)
                Me.UnSetTermNoteBox(Me.TermsNoteBox, System.EventArgs.Empty)

                Me.TermsGBox.BackColor = unSavedColor
            End If
        End With

        Me.TermsCount.UpButton()
        Me.TermsSaveBut.Enabled = True

        Me.Saved = False

        RaiseEvent NewTermAdded(sender, ea)


    End Sub

    ''' <summary>
    ''' Saves a Term to current Definition in this DefTabPage.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SaveTerm(ByVal sender As Object, ByVal e As EventArgs) Handles TermsSaveBut.Click
        Dim ea As New DefTabPageEventArgs(_controlKey, e)

        Dim termVal As String = If(Me.TermsBox.Text = defaultTerm, Nothing, Me.TermsBox.Text)
        Dim noteVal As String = If(Me.TermsNoteBox.Text = defaultTermNote, Nothing, Me.TermsNoteBox.Text)
        Dim termIndex As Integer = Me.TermsCount.Value - 1
        Dim termsCount As Integer = Me.Definition.Terms.Count
        Dim warning As New TxtWarning

        With Me.Definition
            If termVal Is Nothing And termsCount < termIndex + 1 Then
                warning.ShowWarning(Me, "E R R O R" & vbCrLf & "Empty Term", 500)
                Exit Sub
            ElseIf termVal Is Nothing And termsCount = termIndex + 1 Then
                .DeleteTerm(termIndex)
                warning.ShowWarning(Me, "DELETED" & vbCrLf & "Empty Term", 500)
                RaiseEvent NewTermSaved(sender, ea)
                Me.TermsGBox.BackColor = unSavedColor
                Exit Sub
            End If


            If termsCount < termIndex + 1 Then
                .AddTerm(termVal, noteVal)

            ElseIf termsCount >= termIndex + 1 Then
                .EditTerm(termIndex, termVal, noteVal)
            End If

            Dim sTerm = .Terms.Where(Function(t) t.TermItem = termVal And t.Note = noteVal)

            If sTerm.Count > 0 Then
                warning.ShowWarning(Me, "S A V E D")
                RaiseEvent NewTermSaved(sender, ea)
                Me.TermsGBox.BackColor = savedColor
            Else
                warning.ShowWarning(Me, "E R R O R" & vbCrLf & "Unknown", 500)
                Me.TermsGBox.BackColor = unSavedColor
            End If

        End With

    End Sub

    ''' <summary>
    ''' Clears the TermBox so user can enter text.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetTermBox(ByVal sender As Object, ByVal e As EventArgs) Handles TermsBox.Enter
        If TermsBox.Text = defaultTerm Then
            TermsBox.Text = Nothing
            TermsBox.ForeColor = Me.Parent.ForeColor
        End If

    End Sub

    ''' <summary>
    ''' Places the descriptive text in the TermBox if user has not entered any text.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UnSetTermBox(ByVal sender As Object, ByVal e As EventArgs) Handles TermsBox.Leave
        If TermsBox.Text = Nothing Then
            TermsBox.Text = defaultTerm
            TermsBox.ForeColor = Color.DarkGray
        End If

    End Sub

    ''' <summary>
    ''' Clears the TermNoteBox so user can enter text.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetTermNoteBox(ByVal sender As Object, ByVal e As EventArgs) Handles TermsNoteBox.Enter
        If TermsNoteBox.Text = defaultTermNote Then
            TermsNoteBox.Text = Nothing
            TermsNoteBox.ForeColor = Me.Parent.ForeColor
        End If

    End Sub

    ''' <summary>
    ''' Places the descriptive text in the TermNoteBox if user has not entered any text.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UnSetTermNoteBox(ByVal sender As Object, ByVal e As EventArgs) Handles TermsNoteBox.Leave
        If TermsNoteBox.Text = Nothing Then
            TermsNoteBox.Text = defaultTermNote
            TermsNoteBox.ForeColor = Color.DarkGray
        End If

    End Sub

    ''' <summary>
    ''' This will populate the TermBox and the TermNoteBox with the appropriate Term property values according to the TermsCount value.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>The TermsCount value is a representation of the index number in the list of Terms in the Definition in this DefTabPage.</remarks>
    Private Sub ShowTerm(ByVal sender As Object, ByVal e As EventArgs) Handles TermsCount.ValueChanged
        Dim ea As New DefTabPageEventArgs(_controlKey, e)
        Dim termIndex As Integer = Me.TermsCount.Value - 1
        Dim termVal As String
        Dim termNote As String

        RemoveHandler TermsBox.TextChanged, AddressOf EditedTerm
        RemoveHandler TermsNoteBox.TextChanged, AddressOf EditedTerm

        If Me.Definition.Terms.Count = 0 Then
            AddHandler TermsBox.TextChanged, AddressOf EditedTerm
            AddHandler TermsNoteBox.TextChanged, AddressOf EditedTerm
            Exit Sub
        ElseIf Me.Definition.Terms.Count = termIndex Then
            termVal = Nothing
            termNote = Nothing
            Me.TermsGBox.BackColor = unSavedColor
        Else
            termVal = Me.Definition.Terms(termIndex).TermItem
            termNote = Me.Definition.Terms(termIndex).Note
            Me.TermsGBox.BackColor = savedColor
        End If


        Me.TermsBox.Text = termVal
        Me.TermsNoteBox.Text = termNote

        Me.TermsBox.ForeColor = Me.Parent.ForeColor
        Me.TermsNoteBox.ForeColor = Me.Parent.ForeColor
        Me.UnSetTermBox(Me.TermsBox, EventArgs.Empty)
        Me.UnSetTermNoteBox(Me.TermsNoteBox, EventArgs.Empty)

        RaiseEvent TermSelected(sender, ea)

        AddHandler TermsBox.TextChanged, AddressOf EditedTerm
        AddHandler TermsNoteBox.TextChanged, AddressOf EditedTerm
    End Sub

    ''' <summary>
    ''' Sets the Part of Speech for the Definition in this DefTabPage.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>The value for the Term Of Speech is aquired from the PartOfSpeechBox.</remarks>
    Private Sub SetPartOfSpeech(ByVal sender As Object, ByVal e As EventArgs) Handles PartOfSpeechBox.TextChanged
        Dim ea As New DefTabPageEventArgs(_controlKey, e)

        Me.Definition.PartOfSpeech = sender.text
        Me.Saved = False

        RaiseEvent PartOfSpeechEdited(sender, ea)
    End Sub

    ''' <summary>
    ''' Sets the Definition Note for the Definition in this DefTabPage.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>The value for the Note is aquired from the DefinitionNoteBox.</remarks>
    Private Sub SetDefinitionNote(ByVal sender As Object, ByVal e As EventArgs) Handles DefinitionNoteBox.TextChanged
        Me.Definition.Note = CType(sender, TextBox).Text
        Me.Saved = False
    End Sub

    ''' <summary>
    ''' When the TermsBox or TermsNoteBox text changes, mark this Definition as unsaved, and show the visual cues.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub EditedTerm(ByVal sender As Object, ByVal e As EventArgs) Handles TermsBox.TextChanged, TermsNoteBox.TextChanged
        Me.Saved = False

        If Me.TermsGBox IsNot Nothing Then
            Me.TermsGBox.BackColor = unSavedColor
            Me.BackColor = unSavedColor
        End If
    End Sub
#End Region
#End Region


    ''' <summary>
    ''' Sets the Definition belonging to this DefTabPage.
    ''' </summary>
    ''' <param name="Definition">A valid Definition object.</param>
    ''' <remarks>Not called directly. Please use Definition property to set Definition for this DefTabPage.</remarks>
    Private Sub SetDefinition(ByVal Definition As Definition)
        Me.PartOfSpeechBox.Text = Definition.PartOfSpeech
        Me.DefinitionNoteBox.Text = Definition.Note

        For Each t As Term In Definition.Terms
            Me.TermsNewBut.PerformClick()
            Me.TermsBox.Text = If(t.TermItem, defaultTerm)
            Me.TermsNoteBox.Text = If(t.Note, defaultTermNote)
            Me.TermsSaveBut.PerformClick()

            'Resolve bug i have yet to figure out. text stays gray, as if it is defaults
            Me.TermsBox.ForeColor = If(t.TermItem = Nothing, Color.DarkGray, Me.Parent.ForeColor)
            Me.TermsNoteBox.ForeColor = If(t.Note = Nothing, Color.DarkGray, Me.Parent.ForeColor)

        Next
    End Sub

    Friend WithEvents PartOfSpeechLbl As System.Windows.Forms.Label
    Friend WithEvents PartOfSpeechBox As System.Windows.Forms.TextBox
    Friend WithEvents TermsCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents TermsGBox As System.Windows.Forms.GroupBox
    Friend WithEvents TermsBox As System.Windows.Forms.TextBox
    Friend WithEvents TermsNoteBox As System.Windows.Forms.TextBox
    Friend WithEvents TermsNewBut As System.Windows.Forms.Button
    Friend WithEvents TermsSaveBut As System.Windows.Forms.Button
    Friend WithEvents DefinitionLbl As System.Windows.Forms.Label
    Friend WithEvents DefinitionNoteBox As System.Windows.Forms.TextBox


    ''' <summary>
    ''' The Structure used for returning the names of the controls in DefTabPage. 
    ''' </summary>
    ''' <remarks>This is only accessed through the ObjectNames property of the DefTabPage.</remarks>
    <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
    <BrowsableAttribute(False)> _
    Structure DefTabPageObjectNames
        Public ReadOnly Property PartOfSpeech_Label As String
            Get
                Return "PartOfSpeech_Lbl_"
            End Get
        End Property
        Public ReadOnly Property PartOfSpeech_TextBox As String
            Get
                Return "PartOfSpeech_Box_"
            End Get
        End Property
        Public ReadOnly Property Terms_NumericUpDown As String
            Get
                Return "TermsCount_NUD_"
            End Get
        End Property
        Public ReadOnly Property Terms_GroupBox As String
            Get
                Return "Terms_GBox"
            End Get
        End Property
        Public ReadOnly Property Terms_TextBox As String
            Get
                Return "Terms_Box_"
            End Get
        End Property
        Public ReadOnly Property TermsNote_TextBox As String
            Get
                Return "TermsNote_Box_"
            End Get
        End Property
        Public ReadOnly Property TermsNew_Button As String
            Get
                Return "TermsNew_But_"
            End Get
        End Property
        Public ReadOnly Property TermsSave_Button As String
            Get
                Return "TermsSave_But_"
            End Get
        End Property
        Public ReadOnly Property Definition_Label As String
            Get
                Return "Definition_Lbl_"
            End Get
        End Property
        Public ReadOnly Property DefinitionNote_TextBox As String
            Get
                Return "DefinitionNote_Box_"
            End Get
        End Property

    End Structure
End Class

''' <summary>
''' Custom EventArgs for DefTabPage.
''' </summary>
''' <remarks>This will return the EventArgs associated with a control, along with the controlKey.</remarks>
Class DefTabPageEventArgs
    Inherits EventArgs
    Private _defTabPageControlKey As String
    Private _eArgs As EventArgs


    Public Sub New(ByVal controlKey As String, ByVal e As EventArgs)
        _defTabPageControlKey = controlKey
        _eArgs = e
    End Sub

    Public ReadOnly Property DefTabPageControlKey As String
        Get
            Return _defTabPageControlKey
        End Get

    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
        Return _eArgs.Equals(obj)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return _eArgs.GetHashCode()
    End Function

End Class



