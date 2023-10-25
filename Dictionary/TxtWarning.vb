Public Class TxtWarning

    Private warningBgColor As Integer = 255
    Private warningColor As Integer = 255
    Private WarningDirection As String = "down"
    Private warningPause As Integer

    ''' <summary>
    ''' Displayes the text specified in the parentbox parameter for a brief period, using an animated blending effect to fade in then out.
    ''' </summary>
    ''' <param name="parentBox">The parent object in which to display the text.</param>
    ''' <param name="text">The text to be displayed.</param>
    ''' <param name="pause">The amount of time in milliseconds for which to "pause" when the text is at the apex of the blending.</param>
    ''' <remarks></remarks>
    Public Sub ShowWarning(ByVal parentBox As Object, ByVal text As String, Optional ByVal pause As Integer = 10)
        warningPause = pause

        WarningLabel.Text = text
        WarningLabel.TextAlign = ContentAlignment.MiddleCenter
        WarningLabel.Location = New Point(0, 0)
        WarningLabel.Font = New Font(parentBox.Font.FontFamily.ToString, 30, System.Drawing.FontStyle.Bold)
        WarningLabel.ForeColor = Color.White
        WarningLabel.AutoSize = False
        WarningLabel.Dock = DockStyle.Fill
        parentBox.Controls.Add(WarningLabel)
        WarningLabel.BringToFront()

        WarningTimer.Interval = 10
        WarningTimer.Enabled = True
    End Sub

    Private Sub WarningTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarningTimer.Tick

        If WarningDirection = "down" Then
            Me.WarningTimer.Interval = 10
            If warningColor = 0 Then
                WarningDirection = "pause"
                Exit Sub
            End If

            warningColor -= 15
            warningBgColor -= 15

        ElseIf WarningDirection = "pause" Then
            WarningDirection = "up"
            Me.WarningTimer.Interval = warningPause
        Else
            Me.WarningTimer.Interval = 10
            warningColor += 15
            warningBgColor += 15

            If warningColor = 255 Then

                WarningTimer.Enabled = False
                WarningLabel.Parent.Controls.Remove(WarningLabel)
                warningColor = 255
                warningBgColor = 255
                WarningDirection = "down"
                WarningLabel.Dispose()
            End If
        End If

        WarningLabel.ForeColor = Color.FromArgb(255, 255, warningColor)
        WarningLabel.BackColor = Color.FromArgb(warningBgColor, warningBgColor, warningBgColor)

    End Sub


    Private WithEvents WarningTimer As Timer = New System.Windows.Forms.Timer
    Private WithEvents WarningLabel As System.Windows.Forms.Label = New System.Windows.Forms.Label
End Class
