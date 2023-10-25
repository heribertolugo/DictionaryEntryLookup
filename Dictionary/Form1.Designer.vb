<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Word_Box = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DeleteWord_But = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Variant_Box = New System.Windows.Forms.TextBox()
        Me.Symbols_But = New System.Windows.Forms.Button()
        Me.SaveWord_But = New System.Windows.Forms.Button()
        Me.NewWord_But = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SaveDefin_But = New System.Windows.Forms.Button()
        Me.NewDefin_But = New System.Windows.Forms.Button()
        Me.Defin_TControl = New System.Windows.Forms.TabControl()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Origin_Box = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Pron_Box = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Syl_Box = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.FDefin_DGV = New System.Windows.Forms.DataGridView()
        Me.Column1 = New Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx()
        Me.Column2 = New Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx()
        Me.Column3 = New Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ConfigFormatting_But = New System.Windows.Forms.Button()
        Me.Dictionary_LBox = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Search_Box = New System.Windows.Forms.TextBox()
        Me.DataGridViewTextBoxColumnEx1 = New Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx()
        Me.DataGridViewTextBoxColumnEx2 = New Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx()
        Me.DataGridViewTextBoxColumnEx3 = New Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx()
        Me.AboutBut_Lbl = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.FDefin_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Word_Box
        '
        Me.Word_Box.Location = New System.Drawing.Point(6, 49)
        Me.Word_Box.Name = "Word_Box"
        Me.Word_Box.Size = New System.Drawing.Size(287, 20)
        Me.Word_Box.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DeleteWord_But)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Variant_Box)
        Me.GroupBox1.Controls.Add(Me.Symbols_But)
        Me.GroupBox1.Controls.Add(Me.SaveWord_But)
        Me.GroupBox1.Controls.Add(Me.NewWord_But)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Origin_Box)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Pron_Box)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Syl_Box)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Word_Box)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(541, 521)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Raw"
        '
        'DeleteWord_But
        '
        Me.DeleteWord_But.Enabled = False
        Me.DeleteWord_But.Location = New System.Drawing.Point(233, 492)
        Me.DeleteWord_But.Name = "DeleteWord_But"
        Me.DeleteWord_But.Size = New System.Drawing.Size(75, 23)
        Me.DeleteWord_But.TabIndex = 13
        Me.DeleteWord_But.Text = "Delete Word"
        Me.DeleteWord_But.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(300, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Variant"
        '
        'Variant_Box
        '
        Me.Variant_Box.Location = New System.Drawing.Point(299, 49)
        Me.Variant_Box.Name = "Variant_Box"
        Me.Variant_Box.Size = New System.Drawing.Size(218, 20)
        Me.Variant_Box.TabIndex = 1
        '
        'Symbols_But
        '
        Me.Symbols_But.Location = New System.Drawing.Point(310, 142)
        Me.Symbols_But.Name = "Symbols_But"
        Me.Symbols_But.Size = New System.Drawing.Size(75, 23)
        Me.Symbols_But.TabIndex = 4
        Me.Symbols_But.Text = "Symbols"
        Me.Symbols_But.UseVisualStyleBackColor = True
        '
        'SaveWord_But
        '
        Me.SaveWord_But.Enabled = False
        Me.SaveWord_But.Location = New System.Drawing.Point(442, 492)
        Me.SaveWord_But.Name = "SaveWord_But"
        Me.SaveWord_But.Size = New System.Drawing.Size(75, 23)
        Me.SaveWord_But.TabIndex = 9
        Me.SaveWord_But.Text = "Save Word"
        Me.SaveWord_But.UseVisualStyleBackColor = True
        '
        'NewWord_But
        '
        Me.NewWord_But.Location = New System.Drawing.Point(13, 492)
        Me.NewWord_But.Name = "NewWord_But"
        Me.NewWord_But.Size = New System.Drawing.Size(75, 23)
        Me.NewWord_But.TabIndex = 8
        Me.NewWord_But.Text = "New Word"
        Me.NewWord_But.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.GroupBox2.Controls.Add(Me.SaveDefin_But)
        Me.GroupBox2.Controls.Add(Me.NewDefin_But)
        Me.GroupBox2.Controls.Add(Me.Defin_TControl)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 237)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(517, 249)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Definitions/Homographs"
        '
        'SaveDefin_But
        '
        Me.SaveDefin_But.Enabled = False
        Me.SaveDefin_But.Location = New System.Drawing.Point(261, 220)
        Me.SaveDefin_But.Name = "SaveDefin_But"
        Me.SaveDefin_But.Size = New System.Drawing.Size(96, 23)
        Me.SaveDefin_But.TabIndex = 7
        Me.SaveDefin_But.Text = "Save Definition"
        Me.SaveDefin_But.UseVisualStyleBackColor = True
        '
        'NewDefin_But
        '
        Me.NewDefin_But.Location = New System.Drawing.Point(159, 220)
        Me.NewDefin_But.Name = "NewDefin_But"
        Me.NewDefin_But.Size = New System.Drawing.Size(96, 23)
        Me.NewDefin_But.TabIndex = 6
        Me.NewDefin_But.Text = "New Definition"
        Me.NewDefin_But.UseVisualStyleBackColor = True
        '
        'Defin_TControl
        '
        Me.Defin_TControl.Location = New System.Drawing.Point(7, 19)
        Me.Defin_TControl.Name = "Defin_TControl"
        Me.Defin_TControl.SelectedIndex = 0
        Me.Defin_TControl.Size = New System.Drawing.Size(504, 194)
        Me.Defin_TControl.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 177)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Origin"
        '
        'Origin_Box
        '
        Me.Origin_Box.Location = New System.Drawing.Point(6, 196)
        Me.Origin_Box.Name = "Origin_Box"
        Me.Origin_Box.Size = New System.Drawing.Size(287, 20)
        Me.Origin_Box.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 125)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Pronunciation "
        '
        'Pron_Box
        '
        Me.Pron_Box.Location = New System.Drawing.Point(6, 144)
        Me.Pron_Box.Name = "Pron_Box"
        Me.Pron_Box.Size = New System.Drawing.Size(287, 20)
        Me.Pron_Box.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(206, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Syllables (Use period to seperate syllables)"
        '
        'Syl_Box
        '
        Me.Syl_Box.Location = New System.Drawing.Point(6, 94)
        Me.Syl_Box.Name = "Syl_Box"
        Me.Syl_Box.Size = New System.Drawing.Size(287, 20)
        Me.Syl_Box.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Word"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.Panel2)
        Me.GroupBox3.Controls.Add(Me.Panel1)
        Me.GroupBox3.Location = New System.Drawing.Point(576, 21)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(380, 521)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Formatted"
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.Add(Me.FDefin_DGV)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 43)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(374, 475)
        Me.Panel2.TabIndex = 8
        '
        'FDefin_DGV
        '
        Me.FDefin_DGV.AllowUserToAddRows = False
        Me.FDefin_DGV.AllowUserToDeleteRows = False
        Me.FDefin_DGV.AllowUserToResizeColumns = False
        Me.FDefin_DGV.AllowUserToResizeRows = False
        Me.FDefin_DGV.BackgroundColor = System.Drawing.SystemColors.Control
        Me.FDefin_DGV.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.FDefin_DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.FDefin_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FDefin_DGV.ColumnHeadersVisible = False
        Me.FDefin_DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FDefin_DGV.DefaultCellStyle = DataGridViewCellStyle1
        Me.FDefin_DGV.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.FDefin_DGV.Location = New System.Drawing.Point(3, 3)
        Me.FDefin_DGV.Name = "FDefin_DGV"
        Me.FDefin_DGV.ReadOnly = True
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FDefin_DGV.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.FDefin_DGV.RowHeadersVisible = False
        Me.FDefin_DGV.Size = New System.Drawing.Size(346, 22)
        Me.FDefin_DGV.TabIndex = 5
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column1.Width = 5
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column2.HeaderText = "Column3"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column3.HeaderText = "Column4"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ConfigFormatting_But)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(374, 27)
        Me.Panel1.TabIndex = 7
        '
        'ConfigFormatting_But
        '
        Me.ConfigFormatting_But.FlatAppearance.BorderSize = 0
        Me.ConfigFormatting_But.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ConfigFormatting_But.Image = Global.Dictionary.My.Resources.Resources.process
        Me.ConfigFormatting_But.Location = New System.Drawing.Point(327, 3)
        Me.ConfigFormatting_But.Name = "ConfigFormatting_But"
        Me.ConfigFormatting_But.Size = New System.Drawing.Size(25, 23)
        Me.ConfigFormatting_But.TabIndex = 6
        Me.ConfigFormatting_But.UseVisualStyleBackColor = True
        Me.ConfigFormatting_But.Visible = False
        '
        'Dictionary_LBox
        '
        Me.Dictionary_LBox.FormattingEnabled = True
        Me.Dictionary_LBox.HorizontalScrollbar = True
        Me.Dictionary_LBox.Location = New System.Drawing.Point(976, 60)
        Me.Dictionary_LBox.Name = "Dictionary_LBox"
        Me.Dictionary_LBox.Size = New System.Drawing.Size(120, 485)
        Me.Dictionary_LBox.TabIndex = 3
        Me.Dictionary_LBox.TabStop = False
        Me.Dictionary_LBox.UseTabStops = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(969, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Search Words in Dictionary"
        '
        'Search_Box
        '
        Me.Search_Box.Location = New System.Drawing.Point(976, 37)
        Me.Search_Box.Name = "Search_Box"
        Me.Search_Box.Size = New System.Drawing.Size(120, 20)
        Me.Search_Box.TabIndex = 10
        '
        'DataGridViewTextBoxColumnEx1
        '
        Me.DataGridViewTextBoxColumnEx1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumnEx1.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumnEx1.Name = "DataGridViewTextBoxColumnEx1"
        Me.DataGridViewTextBoxColumnEx1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumnEx1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumnEx2
        '
        Me.DataGridViewTextBoxColumnEx2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumnEx2.HeaderText = "Column3"
        Me.DataGridViewTextBoxColumnEx2.Name = "DataGridViewTextBoxColumnEx2"
        '
        'DataGridViewTextBoxColumnEx3
        '
        Me.DataGridViewTextBoxColumnEx3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumnEx3.HeaderText = "Column4"
        Me.DataGridViewTextBoxColumnEx3.Name = "DataGridViewTextBoxColumnEx3"
        '
        'AboutBut_Lbl
        '
        Me.AboutBut_Lbl.AutoSize = True
        Me.AboutBut_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.AboutBut_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AboutBut_Lbl.Font = New System.Drawing.Font("Modern No. 20", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AboutBut_Lbl.ForeColor = System.Drawing.Color.DarkGray
        Me.AboutBut_Lbl.Location = New System.Drawing.Point(1085, -3)
        Me.AboutBut_Lbl.Name = "AboutBut_Lbl"
        Me.AboutBut_Lbl.Size = New System.Drawing.Size(20, 24)
        Me.AboutBut_Lbl.TabIndex = 11
        Me.AboutBut_Lbl.Text = "?"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1108, 577)
        Me.Controls.Add(Me.AboutBut_Lbl)
        Me.Controls.Add(Me.Search_Box)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Dictionary_LBox)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1124, 615)
        Me.MinimumSize = New System.Drawing.Size(1124, 615)
        Me.Name = "Form1"
        Me.Text = "Dictionary Entry & Lookup"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.FDefin_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Word_Box As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Syl_Box As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Pron_Box As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Defin_TControl As System.Windows.Forms.TabControl
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Origin_Box As System.Windows.Forms.TextBox
    Friend WithEvents NewWord_But As System.Windows.Forms.Button
    Friend WithEvents SaveWord_But As System.Windows.Forms.Button
    Friend WithEvents SaveDefin_But As System.Windows.Forms.Button
    Friend WithEvents NewDefin_But As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Dictionary_LBox As System.Windows.Forms.ListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Symbols_But As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Variant_Box As System.Windows.Forms.TextBox
    Friend WithEvents Search_Box As System.Windows.Forms.TextBox
    Friend WithEvents DeleteWord_But As System.Windows.Forms.Button
    Friend WithEvents FDefin_DGV As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx
    Friend WithEvents Column2 As Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx
    Friend WithEvents Column3 As Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx
    Friend WithEvents ConfigFormatting_But As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumnEx1 As Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx
    Friend WithEvents DataGridViewTextBoxColumnEx2 As Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx
    Friend WithEvents DataGridViewTextBoxColumnEx3 As Dictionary.SpannedDataGridView.DataGridViewTextBoxColumnEx
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents AboutBut_Lbl As System.Windows.Forms.Label

End Class
