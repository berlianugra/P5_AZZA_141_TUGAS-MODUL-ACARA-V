Public Class FormKerugian

    Private Sub FormKerugian_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblInputKerugian.Visible = False
        lblRiwayatKerugian.Visible = False

    End Sub


    Private Sub lblKerugian_MouseEnter(sender As Object, e As EventArgs) Handles lblKerugian.MouseEnter

        lblInputKerugian.Visible = True
        lblRiwayatKerugian.Visible = True

    End Sub


    Private Sub lblInputKerugian_MouseEnter(sender As Object, e As EventArgs) Handles lblInputKerugian.MouseEnter

        lblInputKerugian.ForeColor = Color.Orange

    End Sub


    Private Sub lblInputKerugian_MouseLeave(sender As Object, e As EventArgs) Handles lblInputKerugian.MouseLeave

        lblInputKerugian.ForeColor = Color.White

    End Sub


    Private Sub lblInputKerugian_Click(sender As Object, e As EventArgs) Handles lblInputKerugian.Click

        FormInputKerugian.Show()
        Me.Hide()

    End Sub


    Private Sub lblRiwayatKerugian_MouseEnter(sender As Object, e As EventArgs) Handles lblRiwayatKerugian.MouseEnter

        lblRiwayatKerugian.ForeColor = Color.Orange

    End Sub


    Private Sub lblRiwayatKerugian_MouseLeave(sender As Object, e As EventArgs) Handles lblRiwayatKerugian.MouseLeave

        lblRiwayatKerugian.ForeColor = Color.White

    End Sub


    Private Sub lblRiwayatKerugian_Click(sender As Object, e As EventArgs) Handles lblRiwayatKerugian.Click

        FormRiwayatKerugian.Show()
        Me.Hide()

    End Sub


    Private Sub pcbInputKerugian_Click(sender As Object, e As EventArgs) Handles pcbInputKerugian.Click
        FormInputKerugian.Show()
        Me.Hide()

    End Sub


    Private Sub pcbRiwayatKerugian_Click(sender As Object, e As EventArgs) Handles pcbRiwayatKerugian.Click

        FormRiwayatKerugian.Show()
        Me.Hide()

    End Sub

    Private Sub btnLogoutKerugian_Click(sender As Object, e As EventArgs) Handles btnLogoutKerugian.Click

        Dim hasil As DialogResult

        hasil = MessageBox.Show(
        "Apakah Anda yakin ingin logout?",
        "Konfirmasi Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If hasil = DialogResult.Yes Then
            FormLogin.Show()
            Me.Hide()
        End If

    End Sub

    Private Sub lblUsulanPerbaikan_Click(sender As Object, e As EventArgs) Handles lblUsulanPerbaikan.Click
        FormUsulanPerbaikan.Show()
    End Sub
End Class