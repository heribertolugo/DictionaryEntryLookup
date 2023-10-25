Public Class FWordOptions

    Private Sub FWordOptions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        PopulateFontsDropDown()
    End Sub

    Private Sub PopulateFontsDropDown()
        Dim fontFamilies() As FontFamily
        Dim installedFontCollection As New Drawing.Text.InstalledFontCollection()

        fontFamilies = installedFontCollection.Families

        For Each f As FontFamily In fontFamilies
            Dim fbox As New TextBox

            fbox.Text = f.Name
            fbox.Font = New Font(f.Name, 1)
            'fbox.ReadOnly = True

            Me.FontFamily_CBox.Items.Add(fbox)

        Next
    End Sub
End Class