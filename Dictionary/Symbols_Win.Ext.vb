'Extensive searching & research to figure out some common symbols used in dictionaries..
'Sources:
'http://jrgraphix.net/r/Unicode/0020-007F  <--- only one i really needed, and found last.. 
'http://www.phon.ucl.ac.uk/home/wells/ipa-unicode.htm
'http://www.inf.fu-berlin.de/lehre/SS05/efs/materials/pronunciation.html
'http://www.unicode.org/charts/PDF/U0250.pdf
'http://www.unicode.org/charts/PDF/U1D00.pdf
'http://webdesign.about.com/library/bl_htmlcodes.htm


Partial Public Class Symbols_Win
    Private _symbol As Integer = Nothing
    Private ipaSymbols As New List(Of Integer)
    Private phoneticSymbols As New List(Of Integer)
    Private latin1Symbols As New List(Of Integer)
    Private latinExtASymbols As New List(Of Integer)
    Private diacriticSymbols As New List(Of Integer)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MyBase.MinimizeBox = False
        MyBase.MaximizeBox = False
        MyBase.ControlBox = False
        MyBase.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        SetLatinExtASymbols()
        SetIpaSymbols()
        SetPhonSymbols()
        Setlatin1Symbols()
        SetDiacriticSymbols()
        init()

        Me.TopMost = True
    End Sub

    ''' <summary>
    ''' Value of the modal window based on the pronounciation symbol chosen.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Symbol As Integer
        Get
            Return _symbol
        End Get

    End Property

    ''' <summary>
    ''' Initialization of controls and objects in Symbols_Win
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub init()
        Dim ipaColCount As Integer = Math.Ceiling(Math.Sqrt(ipaSymbols.Count))
        Dim phoColCount As Integer = Math.Ceiling(Math.Sqrt(phoneticSymbols.Count))
        Dim ipaRowCount As Integer = Math.Ceiling(CDbl(ipaSymbols.Count) / CDbl(ipaColCount))
        Dim phoRowCount As Integer = Math.Ceiling(CDbl(phoneticSymbols.Count) / CDbl(phoColCount))
        Dim layoutColCount As Integer = If(ipaColCount > phoColCount, ipaColCount, phoColCount)
        Dim layoutRowCount As Integer = If(ipaRowCount > phoRowCount, ipaRowCount, phoRowCount)

        Dim LatinExtATab As New System.Windows.Forms.TabPage
        Dim IpaTab As New System.Windows.Forms.TabPage
        Dim PhoTab As New System.Windows.Forms.TabPage
        Dim Latin1Tab As New System.Windows.Forms.TabPage
        Dim DiacriticTab As New System.Windows.Forms.TabPage

        previewLbl = New System.Windows.Forms.Label

        LatinExtATab.Name = "LatinExtA_Tab"
        LatinExtATab.Text = "Latin Ext A"
        IpaTab.Name = "IPA_Tab"
        IpaTab.Text = "IPA"
        PhoTab.Name = "Phonetic_Tab"
        PhoTab.Text = "Phonetic"
        Latin1Tab.Name = "Latin1_Tab"
        Latin1Tab.Text = "Latin1"
        DiacriticTab.Name = "Diacritic_Tab"
        DiacriticTab.Text = "Diacritics"

        previewLbl.Size = New Size(50, 50)
        previewLbl.Dock = DockStyle.Bottom
        previewLbl.Font = New Font(Me.Font.FontFamily.ToString, 30, FontStyle.Regular)
        previewLbl.BorderStyle = BorderStyle.FixedSingle
        previewLbl.TextAlign = ContentAlignment.TopCenter

        AddButtonsHex(layoutColCount, latinExtASymbols, LatinExtATab)
        AddButtonsHex(layoutColCount, ipaSymbols, IpaTab)
        AddButtonsHex(layoutColCount, phoneticSymbols, PhoTab)
        AddButtonsHex(layoutColCount, latin1Symbols, Latin1Tab)
        AddButtonsHex(layoutColCount, diacriticSymbols, DiacriticTab)

        SymbolGroups = New System.Windows.Forms.TabControl
        SymbolGroups.Name = "SymbolGroups"
        SymbolGroups.Dock = DockStyle.Fill
        SymbolGroups.SuspendLayout()

        SymbolGroups.TabPages.Add(LatinExtATab)
        SymbolGroups.TabPages.Add(IpaTab)
        SymbolGroups.TabPages.Add(PhoTab)
        SymbolGroups.TabPages.Add(Latin1Tab)
        SymbolGroups.TabPages.Add(DiacriticTab)

        SymbolGroups.ResumeLayout(False)
        SymbolGroups.PerformLayout()

        MyBase.Controls.Add(previewLbl)
        MyBase.Controls.Add(SymbolGroups)
        Me.Width = (layoutColCount * 20) + 30
        Me.Height = (layoutRowCount * 20) + previewLbl.Height + 20
    End Sub

    ''' <summary>
    ''' Populates latinExtASymbols list
    ''' </summary>
    ''' <remarks>These are Latin Extended-A Symbols</remarks>
    Private Sub SetLatinExtASymbols()
        For s As Integer = &H100 To &H17F Step +1
            latinExtASymbols.Add(s)
        Next
    End Sub

    ''' <summary>
    ''' Populates ipaSymbols list
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIpaSymbols()
        For s As Integer = &H250 To &H2AF Step +1
            ipaSymbols.Add(s)
        Next
    End Sub

    ''' <summary>
    ''' Populates phoeticSymbols list
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPhonSymbols()
        For s As Integer = &H1D00 To &H1D7F Step +1
            phoneticSymbols.Add(s)
        Next
    End Sub

    ''' <summary>
    ''' Populates latin1Symbols list.
    ''' </summary>
    ''' <remarks>These are Latin-1 Suppliment Symbols</remarks>
    Private Sub Setlatin1Symbols()
        For s As Integer = &HC0 To &HFF Step +1
            latin1Symbols.Add(s)
        Next
    End Sub

    ''' <summary>
    ''' Populates diacriticSymbols list.
    ''' </summary>
    ''' <remarks>These are Spacing diacritics, suprasegmentals, Non-spacing diacritics and suprasegmentals Symbols</remarks>
    Private Sub SetDiacriticSymbols()

        diacriticSymbols.Add(&H26B)
        diacriticSymbols.Add(&H2B0)
        diacriticSymbols.Add(&H2B1)
        diacriticSymbols.Add(&H2B2)
        diacriticSymbols.Add(&H2B4)
        diacriticSymbols.Add(&H2B7)
        diacriticSymbols.Add(&H2BC)
        diacriticSymbols.Add(&H2C8)
        diacriticSymbols.Add(&H2CC)
        diacriticSymbols.Add(&H2D0)
        diacriticSymbols.Add(&H2D1)
        diacriticSymbols.Add(&H2DE)
        diacriticSymbols.Add(&H2E0)
        diacriticSymbols.Add(&H2E4)
        diacriticSymbols.Add(&H300)
        diacriticSymbols.Add(&H301)
        diacriticSymbols.Add(&H303)
        diacriticSymbols.Add(&H304)
        diacriticSymbols.Add(&H306)
        diacriticSymbols.Add(&H308)
        diacriticSymbols.Add(&H30A)
        diacriticSymbols.Add(&H30B)
        diacriticSymbols.Add(&H30F)
        diacriticSymbols.Add(&H318)
        diacriticSymbols.Add(&H319)
        diacriticSymbols.Add(&H31A)
        diacriticSymbols.Add(&H31C)
        diacriticSymbols.Add(&H31D)
        diacriticSymbols.Add(&H31E)
        diacriticSymbols.Add(&H31F)
        diacriticSymbols.Add(&H320)
        diacriticSymbols.Add(&H324)
        diacriticSymbols.Add(&H325)
        diacriticSymbols.Add(&H329)
        diacriticSymbols.Add(&H32A)
        diacriticSymbols.Add(&H32C)
        diacriticSymbols.Add(&H32F)
        diacriticSymbols.Add(&H330)
        diacriticSymbols.Add(&H334)
        diacriticSymbols.Add(&H339)
        diacriticSymbols.Add(&H33A)
        diacriticSymbols.Add(&H33B)
        diacriticSymbols.Add(&H33C)
        diacriticSymbols.Add(&H33D)
        diacriticSymbols.Add(&H35C)
        diacriticSymbols.Add(&H361)

    End Sub

    ''' <summary>
    ''' Sets the Symbol property according to the tag of the button passed as sender. And closes the Symbol window.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This is an event handler.</remarks>
    Private Sub SetSymbol(ByVal sender As Button, ByVal e As EventArgs)
        _symbol = CInt(sender.tag)
        MyBase.Close()
    End Sub

    ''' <summary>
    ''' Sets the text in the preview lablel to the tag value of the button passed as sender.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This is used as a mouseover event handler. Text is set as "wide character"</remarks>
    Private Sub PreviewSymbol(ByVal sender As Button, ByVal e As EventArgs)
        previewLbl.Text = ChrW(sender.tag)
    End Sub

    ''' <summary>
    ''' Sets the text in the preview lablel to the tag value of the button passed as sender.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This is used as a mouseover event handler. </remarks>
    Private Sub PreviewSymbol2(ByVal sender As Button, ByVal e As EventArgs)
        previewLbl.Text = Chr(sender.tag)
    End Sub

    ''' <summary>
    ''' Creates and adds buttons to a parent TabPage. The values and text of the buttons are the character hex values passes as a list through buts parameter. 
    ''' </summary>
    ''' <param name="cols">Number of columns to produce when adding buttons.</param>
    ''' <param name="buts">List of hex values to assing to the buttons as value, and text displayed on button.</param>
    ''' <param name="parent">The TabPage which will be modified to house the buttons created.</param>
    ''' <remarks></remarks>
    Private Sub AddButtonsHex(ByVal cols As Integer, ByVal buts As List(Of Integer), ByRef parent As TabPage)
        Dim curCol As Integer = 0
        Dim curRow As Integer = 0

        For Each b As Integer In buts
            Dim sBut As New System.Windows.Forms.Button

            sBut.Name = b.ToString & "_But"
            sBut.Tag = b.ToString
            sBut.Text = ChrW(b)
            sBut.Size = New Size(20, 20)
            sBut.Location = New Point(curCol * sBut.Size.Width, curRow * sBut.Size.Height)

            parent.Controls.Add(sBut)

            AddHandler sBut.Click, AddressOf SetSymbol
            AddHandler sBut.MouseHover, AddressOf PreviewSymbol

            If curCol = (cols - 1) Then
                curCol = 0
                curRow += 1
            Else
                curCol += 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' Creates and adds buttons to a parent TabPage. The values and text of the buttons are the character hex values passes as a list through buts parameter. 
    ''' </summary>
    ''' <param name="cols">Number of columns to produce when adding buttons.</param>
    ''' <param name="buts">List of hex values to assing to the buttons as value, and text displayed on button.</param>
    ''' <param name="parent">The TabPage which will be modified to house the buttons created.</param>
    ''' <remarks>This will use 1252 encoding.</remarks>
    Private Sub AddButtons1252(ByVal cols As Integer, ByVal buts As List(Of Integer), ByRef parent As TabPage)
        Dim curCol As Integer = 0
        Dim curRow As Integer = 0

        For Each b As Integer In buts
            Dim sBut As New System.Windows.Forms.Button

            sBut.Name = b.ToString & "_But"
            sBut.Tag = b.ToString
            Dim e = System.Text.Encoding.GetEncoding(1252)
            Dim s = e.GetString(New Byte() {b})

            sBut.Text = s
            sBut.Size = New Size(20, 20)
            sBut.Location = New Point(curCol * sBut.Size.Width, curRow * sBut.Size.Height)

            parent.Controls.Add(sBut)

            AddHandler sBut.Click, AddressOf SetSymbol
            AddHandler sBut.MouseHover, AddressOf PreviewSymbol2

            If curCol = (cols - 1) Then
                curCol = 0
                curRow += 1
            Else
                curCol += 1
            End If
        Next
    End Sub


    Friend WithEvents SymbolGroups As System.Windows.Forms.TabControl
    Friend WithEvents previewLbl As System.Windows.Forms.Label
End Class

