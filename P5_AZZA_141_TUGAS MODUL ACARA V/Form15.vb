Public Class Form2
    Private Sub PictureBoxInputKerugian_Click(sender As Object, e As EventArgs) Handles PictureBoxInputKerugian.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBoxRiwayatKerugian_Click(sender As Object, e As EventArgs) Handles PictureBoxRiwayatKerugian.Click
        Form3.Show()
        Me.Hide()
    End Sub
End Class