Public Class FormUsulanPerbaikan

    Private Sub FormUsulanPerbaikan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblInputUsulan.Visible = False
        lblRiwayatUsulan.Visible = False

    End Sub


    Private Sub lblUsulanPerbaikan_MouseEnter(sender As Object, e As EventArgs)

        lblInputUsulan.Visible = True
        lblRiwayatUsulan.Visible = True

    End Sub


    Private Sub lblInputUsulan_MouseEnter(sender As Object, e As EventArgs)

        lblInputUsulan.ForeColor = Color.Orange

    End Sub


    Private Sub lblInputUsulan_MouseLeave(sender As Object, e As EventArgs)

        lblInputUsulan.ForeColor = Color.White

    End Sub


    Private Sub lblInputUsulan_Click(sender As Object, e As EventArgs)

        FormInputUsulan.Show()
        Me.Hide()

    End Sub


    Private Sub lblRiwayatUsulan_MouseEnter(sender As Object, e As EventArgs)

        lblRiwayatUsulan.ForeColor = Color.Orange

    End Sub


    Private Sub lblRiwayatUsulan_MouseLeave(sender As Object, e As EventArgs)

        lblRiwayatUsulan.ForeColor = Color.White

    End Sub


    Private Sub lblRiwayatUsulan_Click(sender As Object, e As EventArgs)

        FormRiwayatUsulan.Show()
        Me.Hide()

    End Sub


    Private Sub pcbInputUsulan_Click(sender As Object, e As EventArgs) Handles pcbInputUsulan.Click
        FormInputUsulan.Show()
        Me.Hide()

    End Sub


    Private Sub pcbRiwayatUsulan_Click(sender As Object, e As EventArgs) Handles pcbRiwayatUsulan.Click

        FormRiwayatUsulan.Show()
        Me.Hide()

    End Sub

    Private Sub btnLogOutUsulan_Click(sender As Object, e As EventArgs) Handles btnLogOutUsulan.Click
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

    Private Sub lblKerugian_Click(sender As Object, e As EventArgs) Handles lblKerugian.Click
        FormKerugian.Show()
    End Sub
End Class