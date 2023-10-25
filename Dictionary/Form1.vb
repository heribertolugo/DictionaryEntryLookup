
Public Class Form1

    Private CurrentWord As New Entry
    Private SymWinVisible As Boolean = False
    Private CurrentWordDbId As Integer = 0
    Private DefFormatSyles As New Dictionary(Of String, DataGridViewCellStyle)

#Region "Event Handlers"

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DBMan.SetConnection()
        SetFormattedEntryFormatting()
    End Sub

    ''' <summary>
    ''' Inserts a new DefTabPage, so a new definition can be worked on.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will also add needed event handlers for DefTabPage events.</remarks>
    Private Sub NewDefinition(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewDefin_But.Click
        Dim defTab As New DefTabPage
        Dim warning As New TxtWarning

        With Me.Defin_TControl
            'Before we allow a new definition tab to be added, lets do some preliminary checks..
            ' Check if definition has at least 1 saved term
            ' Check to make sure a Part of Speech has been saved
            ' Check to make sure we have the current defintion saved before we can create a new one
            If .TabCount > 0 Then
                If CType(.TabPages(.TabCount - 1), DefTabPage).Definition.Terms.Count = 0 Then
                    warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "definition #" & (.TabCount).ToString & " is empty", 500)
                    Exit Sub
                ElseIf CType(.TabPages(.TabCount - 1), DefTabPage).Definition.PartOfSpeech = Nothing Then
                    warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "Part of Speech", 500)
                    Exit Sub
                ElseIf CurrentWord.Definitions.Count < .TabCount Then
                    warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "definition #" & (.TabCount).ToString & " not saved", 500)
                    Exit Sub
                End If
            End If


            defTab.Name = "DefTab_" + .TabCount.ToString
            deft = defTab

            AddHandler deft.NewTermAdded, AddressOf TermAdded
            AddHandler deft.NewTermSaved, AddressOf TermSaved
            AddHandler deft.PartOfSpeechEdited, AddressOf EnableSaveDef

            .Controls.Add(defTab)

            .SelectedTab = defTab
        End With

    End Sub

    ''' <summary>
    ''' Saves a definition to the current word
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SaveDefinition(sender As System.Object, e As System.EventArgs) Handles SaveDefin_But.Click
        Dim warning As New TxtWarning

        With Me.Defin_TControl

            'Make sure we have a Part of Speech before we save the definition
            If CType(.SelectedTab, DefTabPage).Definition.PartOfSpeech.Count < 1 Then
                warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "Part of Speech", 500)
                Exit Sub
            End If


            'If the current definition has not been previously saved, add it as a new definition
            If CurrentWord.Definitions.Count <= .SelectedIndex Then
                Me.CurrentWord.Definitions.Add(CType(.SelectedTab, DefTabPage).Definition)
            Else
                'If the definition already exists, save the edited version
                Me.CurrentWord.Definitions(.SelectedIndex) = (CType(Me.Defin_TControl.SelectedTab, DefTabPage).Definition)
            End If

            'Lets check to see if it is saved, then lets provide some feedback.
            Dim savedDef As Definition = CType(Me.Defin_TControl.SelectedTab, DefTabPage).Definition
            Dim IsavedDef = Me.CurrentWord.Definitions.Where(Function(d) d.Equals(savedDef))
            If IsavedDef.Count > 0 Then
                warning.ShowWarning(sender.parent, "S A V E D")
                CType(.SelectedTab, DefTabPage).Saved = True
            Else
                warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "Unknown", 500)
                CType(.SelectedTab, DefTabPage).Saved = False
            End If
        End With


        EnableSaveWordButton()
    End Sub

    ''' <summary>
    ''' Handles processes to be done when a new control has been added to Defin_TControl
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>The controls added should always be of DefTabPage type. When a new DefTabPage is added, disable the save button.</remarks>
    Private Sub NewDefinitionPageAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles Defin_TControl.ControlAdded
        Me.SaveDefin_But.Enabled = False
    End Sub

    ''' <summary>
    ''' Handles processes to be done when a DefTabPage has been selected.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>When the the selectedIndex in DefinTControl changes becasue a DefPageTab has been selected, we need to run a check to determine 
    ''' if the save button for definitions should or should not be enabled.</remarks>
    Private Sub DefTabPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Defin_TControl.SelectedIndexChanged


        EnableDefinitionButton()
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the text in Origin_Box has changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will update the origin for the word of which we are working on.</remarks>
    Private Sub SetOrigin(sender As System.Object, e As System.EventArgs) Handles Origin_Box.TextChanged
        Me.CurrentWord.Origin = sender.text
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the text in Pron_Box has changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will update the pronunciation for the word of which we are working on.</remarks>
    Private Sub SetPronunciation(sender As System.Object, e As System.EventArgs) Handles Pron_Box.TextChanged
        Me.CurrentWord.Pronunciation = sender.text
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the text in Syl_Box has changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will update the syllables for the word of which we are working on.</remarks>
    Private Sub SetSyllable(sender As System.Object, e As System.EventArgs) Handles Syl_Box.TextChanged
        Me.CurrentWord.Syllables = sender.text
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the text in Word_Box has changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will update the syllables for the word of which we are working on. 
    ''' Then run a check to determine if the save button for the word should or should not be enabled.</remarks>
    Private Sub SetCurrentWord(sender As System.Object, e As System.EventArgs) Handles Word_Box.TextChanged
        Me.CurrentWord.Word = sender.text

        EnableSaveWordButton()
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the text in Variant_Box has changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will update the variant for the word of which we are working on.</remarks>
    Private Sub SetVariant(sender As System.Object, e As System.EventArgs) Handles Variant_Box.TextChanged
        Me.CurrentWord.VariantWord = sender.text

    End Sub

    ''' <summary>
    ''' Handles processes to be done when the user clicks the "save word" button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will add a new entry to the dictionary if it does not exist, or update it if the word already exists.</remarks>
    Private Sub SaveCurrentWord(sender As System.Object, e As System.EventArgs) Handles SaveWord_But.Click

        Dim currentEntries As Dictionary(Of Integer, Entry) = DBMan.FindEntries(Me.CurrentWord.Word)
        Dim currentEntryById As Entry = DBMan.FindEntryById(Me.CurrentWordDbId)
        Dim warning As New TxtWarning

        'This is a new word. No matches for the word or ID exist in dictionary
        If currentEntries.Count < 1 And currentEntryById Is Nothing Then
            If DBMan.AddEntry(Me.CurrentWord) Then 'Attempt to update db
                Me.CurrentWordDbId = DBMan.CurrentEntryId
                warning.ShowWarning(sender.parent, "S A V E D") 'update success
                EnableDeleteWordButton()
                SetFormattedEntryData()
            Else
                warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "F1 #SaveWord1", 500) 'update fail - Form1 -> line #SaveWord1
            End If

            'This word was not found in the dictionary, but the ID number was found
        ElseIf currentEntries.Count < 1 And currentEntryById IsNot Nothing Then
            Dim doUpdate As DialogResult = MsgBox(Me.CurrentWord.Word & " is in dictionary, but spelled " & currentEntryById.Word & " . " & Chr(13) & _
                                                  "Do you want to update the word " & currentEntryById.Word & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Duplicate Entry found")

            If doUpdate = Windows.Forms.DialogResult.Yes Then
                If DBMan.UpdateEntry(Me.CurrentWordDbId, Me.CurrentWord) Then 'Attempt to update db
                    Me.CurrentWordDbId = DBMan.CurrentEntryId
                    warning.ShowWarning(sender.parent, "S A V E D") 'update success
                    EnableDeleteWordButton()
                    SetFormattedEntryData()
                Else
                    warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "F1 #SaveWord2", 500) 'update fail - Form1 -> line #SaveWord2
                End If
            End If

            'This word exists in dictionary
        ElseIf currentEntries.Count = 1 Then
            Dim doUpdate As DialogResult

            'This word exists in dictionary, and the ID number matches the dictionary ID number
            If Me.CurrentWordDbId = currentEntries.Keys.ElementAt(0) Then
                doUpdate = MsgBox(currentEntries(currentEntries.Keys.ElementAt(0)).Word & " already exists in dictionary. " & Chr(13) & _
                                                  "Do you want to update this word?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Duplicate Entry found")
            Else
                'This word exists in dictionary, but ID number does not match
                doUpdate = MsgBox(currentEntries(currentEntries.Keys.ElementAt(0)).Word & " is in dictionary, but the ID does not match. " & Chr(13) & _
                                                  "Do you want to over write the word already in the dictionary?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Duplicate Entry found")
            End If

            If doUpdate = Windows.Forms.DialogResult.Yes Then
                If DBMan.UpdateEntry(currentEntries.Keys.ElementAt(0), Me.CurrentWord) Then 'Attempt to update db
                    Me.CurrentWordDbId = currentEntries.Keys.ElementAt(0)
                    warning.ShowWarning(sender.parent, "S A V E D") 'update success
                    EnableDeleteWordButton()
                    SetFormattedEntryData()
                Else
                    warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "F1 #SaveWord3", 500) 'update fail - Form1 -> line #SaveWord3
                End If
            End If

        Else
            'This should never happen
            warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "multi entries", 500)
        End If
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the user clicks the "delete word" button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will delete the entry from the dictionary.</remarks>
    Private Sub DeleteCurrentWord(sender As System.Object, e As System.EventArgs) Handles DeleteWord_But.Click
        Dim warning As New TxtWarning
        Dim okToDelete As DialogResult = MsgBox("This will permanantly delete this entry." & Chr(13) & "Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Confirmation")

        If okToDelete = Windows.Forms.DialogResult.Yes Then
            If DBMan.DeleteEntry(Me.CurrentWordDbId) Then 'Attempt to update db
                warning.ShowWarning(sender.parent, "D E L E T E D") 'update success
                NewWord_But.PerformClick()
                SearchDictionary(Search_Box, System.EventArgs.Empty)
            Else
                warning.ShowWarning(sender.parent, "E R R O R" & vbCrLf & "F1 #DeleteWord", 500) 'update fail - Form1 -> line #DeleteCurrentWord
            End If
        End If

    End Sub

    ''' <summary>
    ''' Handles processes to be done when user clicks "new word" button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will reset the form and any properties/variables</remarks>
    Private Sub StartNewEntry(sender As System.Object, e As System.EventArgs) Handles NewWord_But.Click
        Me.CurrentWord = New Entry
        Me.CurrentWordDbId = 0

        Me.Word_Box.Text = ""
        Me.Variant_Box.Text = ""
        Me.Syl_Box.Text = ""
        Me.Pron_Box.Text = ""
        Me.Origin_Box.Text = ""

        Me.Defin_TControl.TabPages.Clear()

        EnableDefinitionButton()
        EnableDeleteWordButton()
        SetFormattedEntryData()
    End Sub

    ''' <summary>
    ''' Handles processes to be done when user clicks inside Dictionary_LBox.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This is used to set the current word to the selected item.</remarks>
    Private Sub DictionaryWordSelected(sender As System.Object, e As MouseEventArgs) Handles Dictionary_LBox.MouseClick

        With Me.Dictionary_LBox
            If .SelectedItems.Count < 1 Then Exit Sub

            If Not IsLItemClicked(Me.Dictionary_LBox, New Point(e.X, e.Y)) Then Exit Sub

            Dim sEntry As Entry = DBMan.FindEntryById(CType(.SelectedValue, Integer))

            Me.LoadEntry(sEntry, CType(.SelectedValue, Integer))

        End With
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the Symbols button has been clicked.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will open a box with available pronounciation symbols which can be selected.</remarks>
    Private Sub ShowSymbols(sender As System.Object, e As System.EventArgs) Handles Symbols_But.Click
        If SymWinVisible Then
            symWin.Close()
            SymWinVisible = False
        Else
            symWin = New Symbols_Win
            Dim senderBut As Button = CType(sender, Button)
            Dim senderLoc As New Point(CType(sender, Button).PointToScreen(CType(sender, Button).Location))

            symWin.StartPosition = FormStartPosition.Manual
            symWin.Location = New Point(senderBut.PointToScreen(New Point(0, 0)).X + senderBut.Width, senderBut.PointToScreen(New Point(0, 0)).Y)

            symWin.Show()
            SymWinVisible = True
        End If
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the form is closed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will update the variant for the word of which we are working on.</remarks>
    Private Sub SelectedSym(ByVal sender As Object, ByVal e As EventArgs) Handles symWin.FormClosed
        If Not symWin.Symbol = Nothing Then
            Me.Pron_Box.Text &= ChrW(symWin.Symbol)
        End If
        SymWinVisible = False
    End Sub

    ''' <summary>
    ''' Handles processes to be done when the form window is moved.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will close the symbol box if it was opened.</remarks>
    Private Sub MoveSymbolBox(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Move
        If SymWinVisible Then
            symWin.Close()
            SymWinVisible = False
        End If
    End Sub

    ''' <summary>
    ''' Handles processes to be done when user types in Syllable TextBox and Pronounciation TextBox.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This will insert a middot when the user types a period(.).</remarks>
    Private Sub InsertMidDot(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles Syl_Box.KeyPress, Pron_Box.KeyPress
        Dim middot As Char = ChrW(&HB7)
        If e.KeyChar = "." Then
            e.KeyChar = middot
        End If
    End Sub

    ''' <summary>
    ''' Searches the dictionary for words that begin with the characters entered in Search_Box, as they are typed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Upon finding matches in the dictionary, the word from the results (entry's) will get bound to Dictionary_LBox (ListBox), 
    ''' and each ListBox item will hold the database ID value of the corresponding entry. </remarks>
    Private Sub SearchDictionary(sender As System.Object, e As System.EventArgs) Handles Search_Box.TextChanged
        'test to make sure there is something in textbox, otherwise the whole db will be pulled..
        If CType(sender, TextBox).Text.Count < 1 Then Exit Sub

        'create a container to hold our entry's and their corresponding id numbers
        Dim entryList As New Dictionary(Of Integer, Entry)(DBMan.FindEntries(CType(sender, TextBox).Text, True))
        'create a container to hold our words from our entry's
        Dim wordlist As New List(Of String)

        'populate our word container
        For Each entryitem In entryList.Values.Where(Function(w) w.Word <> "")
            wordlist.Add(entryitem.Word)
        Next

        'container to hold our words and the ID number, of their corresponding entry
        'populate the container. 
        Dim iwordList As Dictionary(Of Integer, String) = wordlist.ToDictionary(Function(w) entryList.Where(Function(k) w = entryList.Item(k.Key).Word)(0).Key)


        'bind our ListBox to hold the database id value for each entry, but display the corresponding word in the entry
        Me.Dictionary_LBox.DataBindings.Clear()
        Me.Dictionary_LBox.DataSource = iwordList.ToList
        Me.Dictionary_LBox.DisplayMember = "Value"
        Me.Dictionary_LBox.ValueMember = "Key"
        Me.Dictionary_LBox.ClearSelected()

    End Sub

    ''' <summary>
    ''' Gives user a dialog box from which they can choose formating options for current word Entry members.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Options chosen here are saved to app settings.</remarks>
    Private Sub ConfigFormatting_But_Click(sender As System.Object, e As System.EventArgs) Handles ConfigFormatting_But.Click
        Dim f As New FWordOptions
        f.ShowDialog()
    End Sub

#End Region

#Region "Functions and Sub-Procedures"

    Private Sub TermAdded(ByVal sender As System.Object, ByVal e As DefTabPageEventArgs)
        'Actions to take when a Term is added in our dynamically created DefTabPages
        'e has the control's controlKey, so we can access items in the tab that the event sender is in


        EnableDefinitionButton() 'sender, e
    End Sub

    Private Sub TermSaved(ByVal sender As System.Object, ByVal e As DefTabPageEventArgs)
        'Actions to take when a Term is saved succesfully in our dynamically created DefTabPages
        'e has the control's controlKey, so we can access items in the tab that the event sender is in


        EnableDefinitionButton() 'sender, e
    End Sub

    Private Sub EnableSaveDef(ByVal sender As System.Object, ByVal e As DefTabPageEventArgs)
        'Actions to take when the PartOfSpeech Textbox text is changed in our dynamically created DefTabPages
        'e has the control's controlKey, so we can access items in the tab that the event sender is in


        EnableDefinitionButton() 'sender, e
    End Sub

    Private Sub TermSelectedChanged(ByVal sender As System.Object, ByVal e As DefTabPageEventArgs)
        'Actions to take when a different Term is selected and displayed in our dynamically created DefTabPages
        'e has the control's controlKey, so we can access items in the tab that the event sender is in


        EnableDefinitionButton() 'sender, e
    End Sub

    Private Sub LoadEntry(ByVal dictionaryEntry As Entry, Optional ByVal id As Integer = 0)
        StartNewEntry(Nothing, System.EventArgs.Empty)

        With dictionaryEntry
            Me.CurrentWordDbId = id
            Me.Word_Box.Text = .Word
            Me.Variant_Box.Text = .VariantWord
            Me.Syl_Box.Text = .Syllables
            Me.Pron_Box.Text = .Pronunciation
            Me.Origin_Box.Text = .Origin

            For Each def As Definition In .Definitions
                Me.NewDefin_But.PerformClick()
                Dim deftab As New DefTabPage
                deftab = CType(Me.Defin_TControl.TabPages(Me.Defin_TControl.TabCount - 1), DefTabPage)
                deftab.Definition = def
                Me.SaveDefin_But.PerformClick()
            Next
        End With

        SetFormattedEntryData()
        EnableDeleteWordButton()
    End Sub

    ''' <summary>
    ''' This will insert all values for the Entry members to be shown formatted in the "Formatted" group box.
    ''' </summary>
    ''' <remarks>All items will be inserted into the FDefin_DGV (DataGridView) with their formatting options attached.</remarks>
    Private Sub SetFormattedEntryData()
        With Me.FDefin_DGV

            .Rows.Clear()

            .Rows.Add()
            .Rows(.RowCount - 1).Cells(0).Value = Me.CurrentWord.Word
            .Rows(.RowCount - 1).Cells(0).Style = DefFormatSyles("word")
            Dim wcell As DataGridViewTextBoxCell = .Rows(.RowCount - 1).Cells(0)
            CType(wcell, SpannedDataGridView.DataGridViewTextBoxCellEx).ColumnSpan = 2
            .Rows(.RowCount - 1).Cells(2).Value = Me.CurrentWord.VariantWord
            .Rows(.RowCount - 1).Cells(2).Style = DefFormatSyles("vword")

            If Me.CurrentWord.Syllables IsNot Nothing Then
                .Rows.Add()
                .Rows(.RowCount - 1).Cells(0).Value = Me.CurrentWord.Syllables
                .Rows(.RowCount - 1).DefaultCellStyle = DefFormatSyles("syl")
                Dim scell As DataGridViewTextBoxCell = .Rows(.RowCount - 1).Cells(0)
                CType(scell, SpannedDataGridView.DataGridViewTextBoxCellEx).ColumnSpan = 2
            End If

            If Me.CurrentWord.Pronunciation IsNot Nothing Then
                .Rows.Add()
                .Rows(.RowCount - 1).Cells(0).Value = "Pronunciation:"
                .Rows(.RowCount - 1).Cells(0).Style = DefFormatSyles("default")
                .Rows(.RowCount - 1).Cells(1).Value = "/" & Me.CurrentWord.Pronunciation & "/"
                .Rows(.RowCount - 1).Cells(1).Style = DefFormatSyles("pron")
                Dim pcell As DataGridViewTextBoxCell = .Rows(.RowCount - 1).Cells(1)
                CType(pcell, SpannedDataGridView.DataGridViewTextBoxCellEx).ColumnSpan = 2
            End If


            For Each d As Definition In Me.CurrentWord.Definitions

                .Rows.Add()
                .Rows(.RowCount - 1).Cells(0).Value = d.PartOfSpeech
                .Rows(.RowCount - 1).DefaultCellStyle = DefFormatSyles("pos")
                Dim pscell As DataGridViewTextBoxCell = .Rows(.RowCount - 1).Cells(0)
                CType(pscell, SpannedDataGridView.DataGridViewTextBoxCellEx).ColumnSpan = 2

                If d.Note IsNot Nothing Then
                    .Rows.Add()
                    .Rows(.RowCount - 1).Cells(1).Value = d.Note
                    .Rows(.RowCount - 1).DefaultCellStyle = DefFormatSyles("defnote")
                    Dim dcell As DataGridViewTextBoxCell = .Rows(.RowCount - 1).Cells(1)
                    CType(dcell, SpannedDataGridView.DataGridViewTextBoxCellEx).ColumnSpan = 2
                End If

                For Each t As Term In d.Terms
                    .Rows.Add()
                    .Rows(.RowCount - 1).Cells(1).Value = Chr(149) & t.TermItem
                    .Rows(.RowCount - 1).DefaultCellStyle = DefFormatSyles("term")
                    Dim tcell As DataGridViewTextBoxCell = .Rows(.RowCount - 1).Cells(1)
                    CType(tcell, SpannedDataGridView.DataGridViewTextBoxCellEx).ColumnSpan = 2

                    If t.Note IsNot Nothing Then
                        .Rows.Add()
                        .Rows(.RowCount - 1).Cells(1).Value = t.Note
                        .Rows(.RowCount - 1).DefaultCellStyle = DefFormatSyles("termnote")
                        Dim tncell As DataGridViewTextBoxCell = .Rows(.RowCount - 1).Cells(1)
                        CType(tncell, SpannedDataGridView.DataGridViewTextBoxCellEx).ColumnSpan = 2
                    End If
                Next

            Next

            If Me.CurrentWord.Origin IsNot Nothing Then
                .Rows.Add()
                .Rows(.RowCount - 1).Cells(0).Value = "Origin"
                .Rows(.RowCount - 1).Cells(0).Style = DefFormatSyles("default")
                .Rows(.RowCount - 1).Cells(0).OwningColumn.Width = 20
                .Rows(.RowCount - 1).Cells(1).Value = Me.CurrentWord.Origin
                .Rows(.RowCount - 1).Cells(1).Style = DefFormatSyles("origin")
                Dim ocell As DataGridViewTextBoxCell = .Rows(.RowCount - 1).Cells(1)
                CType(ocell, SpannedDataGridView.DataGridViewTextBoxCellEx).ColumnSpan = 2
                CType(ocell, SpannedDataGridView.DataGridViewTextBoxCellEx).Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End If

            .Height = .Rows.GetRowsHeight(DataGridViewElementStates.Visible)

            .ClearSelection()
            .ReadOnly = True
            .Enabled = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        End With
    End Sub

    ''' <summary>
    ''' This will set all formatting options for the current word Entry members.
    ''' </summary>
    ''' <remarks>Formatting options will be added to DefFormatStyles Dictionary List as DataGridViewCellStyle.</remarks>
    Private Sub SetFormattedEntryFormatting()

        DefFormatSyles.Add("word", New DataGridViewCellStyle)
        DefFormatSyles("word").Font = New Font(Me.Font.FontFamily.ToString, 16, FontStyle.Bold, GraphicsUnit.Pixel)
        DefFormatSyles("word").ForeColor = Color.Green

        DefFormatSyles.Add("vword", New DataGridViewCellStyle)
        DefFormatSyles("vword").Font = New Font(Me.Font.FontFamily.ToString, 14, FontStyle.Regular, GraphicsUnit.Pixel)
        DefFormatSyles("vword").ForeColor = Color.DarkGreen

        DefFormatSyles.Add("syl", New DataGridViewCellStyle)
        DefFormatSyles("syl").Font = New Font(Me.Font.FontFamily.ToString, 12, FontStyle.Regular, GraphicsUnit.Pixel)
        DefFormatSyles("syl").ForeColor = Color.Black

        DefFormatSyles.Add("pron", New DataGridViewCellStyle)
        DefFormatSyles("pron").Font = New Font(Me.Font.FontFamily.ToString, 12, FontStyle.Bold, GraphicsUnit.Pixel)
        DefFormatSyles("pron").ForeColor = Color.Black

        DefFormatSyles.Add("pos", New DataGridViewCellStyle)
        DefFormatSyles("pos").Font = New Font(Me.Font.FontFamily.ToString, 12, FontStyle.Bold + FontStyle.Underline, GraphicsUnit.Pixel)
        DefFormatSyles("pos").ForeColor = Color.DarkRed

        DefFormatSyles.Add("defnote", New DataGridViewCellStyle)
        DefFormatSyles("defnote").Font = New Font(Me.Font.FontFamily.ToString, 12, FontStyle.Regular, GraphicsUnit.Pixel)
        DefFormatSyles("defnote").ForeColor = Color.DarkGray

        DefFormatSyles.Add("term", New DataGridViewCellStyle)
        DefFormatSyles("term").Font = New Font(Me.Font.FontFamily.ToString, 12, FontStyle.Regular, GraphicsUnit.Pixel)
        DefFormatSyles("term").ForeColor = Color.Black

        DefFormatSyles.Add("termnote", New DataGridViewCellStyle)
        DefFormatSyles("termnote").Font = New Font(Me.Font.FontFamily.ToString, 12, FontStyle.Italic, GraphicsUnit.Pixel)
        DefFormatSyles("termnote").ForeColor = Color.DarkGray

        DefFormatSyles.Add("origin", New DataGridViewCellStyle)
        DefFormatSyles("origin").Font = New Font(Me.Font.FontFamily.ToString, 12, FontStyle.Regular, GraphicsUnit.Pixel)
        DefFormatSyles("origin").ForeColor = Color.SlateGray
        DefFormatSyles("origin").Alignment = DataGridViewContentAlignment.TopLeft

        DefFormatSyles.Add("default", New DataGridViewCellStyle)
        DefFormatSyles("default").Font = New Font(Me.Font.FontFamily.ToString, Me.Font.Size, Me.Font.Style, Me.Font.Unit)
        DefFormatSyles("default").ForeColor = Color.Black
        DefFormatSyles("origin").Alignment = DataGridViewContentAlignment.TopLeft

    End Sub

    'Should be obsolete. left incase it must be uncommented due to calling under a specific scenario
    'Commented out to be able to find where it is being called, so it can be updated with new version of sub-procedure
    'Private Sub EnableDefinitionButton(ByVal sender As System.Object, ByVal e As DefTabPageEventArgs)
    '    Dim termsOk As Boolean = CType(Me.Defin_TControl.SelectedTab, DefTabPage).Definition.Terms.Count > 0
    '    Dim partOfSpeechOk As Boolean = Me.Defin_TControl.SelectedTab.Controls(deft.ObjectNames.PartOfSpeech_TextBox & e.DefTabPageControlKey).Text.Count > 0

    '    Me.SaveDefin_But.Enabled = termsOk And partOfSpeechOk
    'End Sub

    ''' <summary>
    ''' Enables the save button for definition if the required criteria are met.
    ''' </summary>
    ''' <remarks>The criteria required to enable the save button are: 1- At least 1 term has been saved. 2- A Part of Speech has been entered.</remarks>
    Private Sub EnableDefinitionButton()
        If Me.Defin_TControl.TabCount < 1 Then Exit Sub

        Dim termsOk As Boolean = CType(Me.Defin_TControl.SelectedTab, DefTabPage).Definition.Terms.Count > 0
        Dim partOfSpeechOk As Boolean = Me.Defin_TControl.SelectedTab.Controls(deft.ObjectNames.PartOfSpeech_TextBox & CType(Me.Defin_TControl.SelectedTab, DefTabPage).ControlKey).Text.Count > 0

        Me.SaveDefin_But.Enabled = IIf(termsOk And partOfSpeechOk, True, False)
    End Sub

    ''' <summary>
    ''' Enables the save button for the current word if the required criteria are met.
    ''' </summary>
    ''' <remarks>The criteria required to enable the save button are: 1- At least 1 definition has been saved. 2- A Word has been entered.</remarks>
    Private Sub EnableSaveWordButton()
        If Me.CurrentWord.Word Is Nothing Then Exit Sub
        Me.SaveWord_But.Enabled = Me.CurrentWord.Word.Count > 0 And Me.CurrentWord.Definitions.Count > 0
    End Sub

    ''' <summary>
    ''' Enables the delete button for the current word if the required criteria are met.
    ''' </summary>
    ''' <remarks>The criteria required to enable the save button are: The current word's id (CurrentWordDbId) is found in database.</remarks>
    Private Sub EnableDeleteWordButton()
        Me.DeleteWord_But.Enabled = If(DBMan.FindEntryById(CurrentWordDbId) Is Nothing, False, True)
    End Sub

    ''' <summary>
    ''' Will check if the given points are inside the selected item in lBox.
    ''' </summary>
    ''' <param name="lBox">A listBox with selectable items set to one.</param>
    ''' <param name="clickCords">Coordinates to verify whether they fall within lBox selected item.</param>
    ''' <returns>Returns true if coordinates are within the area of the selected item.</returns>
    ''' <remarks></remarks>
    Private Function IsLItemClicked(ByVal lBox As ListBox, ByVal clickCords As Point) As Boolean
        Dim itemBox As Rectangle = lBox.GetItemRectangle(lBox.SelectedIndex)

        If clickCords.X < itemBox.Left Or clickCords.X > itemBox.Right Then Return False
        If clickCords.Y < itemBox.Top Or clickCords.Y > itemBox.Bottom Then Return False

        Return True
    End Function

    'Obsolete - No longer using xml file for dictionary. Using database instead with autoupdate.
    'Private Sub UpdateDictionaryListBox(ByRef DictionaryListBox As System.Windows.Forms.ListBox, ByVal dictionaryFileName As String)
    '    If Not File.Exists(dictionaryFileName) Then Exit Sub

    '    Dim dictionary As XDocument
    '    Dim words
    '    dictionary = XDocument.Load(dictionaryFileName) 'My.Application.Info.DirectoryPath & 
    '    words = dictionary.Descendants("Entry").Select(Function(ex) New With {.id = ex.Attribute("Word").Value})

    '    Me.Dictionary_LBox.Items.Clear()

    '    For Each w In words
    '        Me.Dictionary_LBox.Items.Add(w.id)
    '    Next
    'End Sub

#End Region

    Friend WithEvents deft As DefTabPage
    Friend WithEvents symWin As Symbols_Win

    Private Sub ShowAbout(sender As System.Object, e As System.EventArgs) Handles AboutBut_Lbl.Click
        AboutBox1.ShowDialog()
    End Sub
End Class
