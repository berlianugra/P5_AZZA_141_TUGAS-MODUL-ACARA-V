Public Class FormDashboardKualitas

    Private produksiTerbuka As Boolean = False
    Private ukuranAwalPbInput As Size
    Private ukuranAwalPbRiwayat As Size
    Private posisiAwalPbInput As Point
    Private posisiAwalPbRiwayat As Point
    Private ukuranAwalbtnLogout As Size
    Private posisiAwalbtnLogout As Point

    Private Sub FormDashboardKualitas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Simpan ukuran dan posisi awal PictureBox
        ukuranAwalPbInput = pbInputKualitas.Size
        ukuranAwalPbRiwayat = pbRiwayatKualitas.Size

        posisiAwalPbInput = pbInputKualitas.Location
        posisiAwalPbRiwayat = pbRiwayatKualitas.Location

        'Simpan ukuran dan posisi awal tombol Logout
        ukuranAwalbtnLogout = btnLogoutIK.Size
        posisiAwalbtnLogout = btnLogoutIK.Location

        '========================================
        ' KONDISI AWAL
        '========================================

        produksiTerbuka = False

        lblKualitas.Visible = True

        lblInputKualitas.Visible = False
        lblRiwayatKualitas.Visible = False

        'Warna awal
        lblKualitas.BackColor = Color.Transparent
        lblInputKualitas.BackColor = Color.Transparent
        lblRiwayatKualitas.BackColor = Color.Transparent

        'Font awal
        lblKualitas.Font =
            New Font(lblKualitas.Font, FontStyle.Regular)

        'Pastikan Kualitas berada di depan
        lblKualitas.BringToFront()
    End Sub

    Private Sub lblKualitas_Click(sender As Object, e As EventArgs) Handles lblKualitas.Click

        If produksiTerbuka = False Then

            produksiTerbuka = True

            'Tampilkan submenu
            lblInputKualitas.Visible = True
            lblRiwayatKualitas.Visible = True

            'Menu Kualitas aktif
            lblKualitas.BackColor =
            Color.FromArgb(45, 99, 181)

            lblKualitas.Font =
            New Font(lblKualitas.Font, FontStyle.Bold)

            'Pastikan tampil di depan
            lblInputKualitas.BringToFront()
            lblRiwayatKualitas.BringToFront()
            lblKualitas.BringToFront()

        Else

            '========================================
            ' JIKA MENU SUDAH TERBUKA
            ' MAKA TUTUP
            '========================================

            produksiTerbuka = False

            'Sembunyikan submenu
            lblInputKualitas.Visible = False
            lblRiwayatKualitas.Visible = False

            'Kembalikan Kualitas
            lblKualitas.BackColor = Color.Transparent

            lblKualitas.Font =
            New Font(lblKualitas.Font, FontStyle.Regular)

        End If

    End Sub



    Private Sub lblProduksiKualitas_Click(sender As Object, e As EventArgs) Handles lblProduksiKualitas.Click
        Dim formProduksiKualitas As New FormInputKualitas()
        formProduksiKualitas.Show()
        Me.Hide()
    End Sub

    'PictureBox Input Produksi
    Private Sub pbInputKualitas_Click(sender As Object, e As EventArgs) Handles pbInputKualitas.Click

        Dim formInput As New FormInputKualitas()
        formInput.Show()
        Me.Hide()

    End Sub

    'Riwayat Produksi
    Private Sub lblInputKualitas_Click(sender As Object, e As EventArgs) Handles lblRiwayatKualitas.Click, lblInputKualitas.Click

        Dim FormInputKualitas As New FormInputKualitas()
        FormInputKualitas.Show()
        Me.Hide()

    End Sub

    'PictureBox Riwayat Produksi
    Private Sub pbRiwayat_Click(sender As Object, e As EventArgs) Handles pbRiwayatKualitas.Click

        Dim formRiwayat As New formRiwayatKualitas()
        formRiwayat.Show()
        Me.Hide()

    End Sub

    'PictureBox Riwayat Produksi
    Private Sub lblNG_Click(sender As Object, e As EventArgs) Handles lblNGKualitas.Click

        Dim FormProdukNG As New formRiwayatKualitas()
        FormProdukNG.Show()
        Me.Hide()

    End Sub

    'Hover Input
    Private Sub lblInput_MouseEnter(sender As Object, e As EventArgs) Handles lblInputKualitas.MouseEnter

        lblInputKualitas.BackColor = Color.DarkOrange

    End Sub

    Private Sub lblInput_MouseLeave(sender As Object, e As EventArgs) Handles lblInputKualitas.MouseLeave

        lblInputKualitas.BackColor = Color.Transparent

    End Sub

    'Hover Riwayat
    Private Sub lblRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles lblRiwayatKualitas.MouseEnter

        lblRiwayatKualitas.BackColor = Color.DarkOrange

    End Sub

    Private Sub lblRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles lblRiwayatKualitas.MouseLeave

        lblRiwayatKualitas.BackColor = Color.Transparent

    End Sub

    'Hover Dashboard
    Private Sub lblDashboard_MouseEnter(sender As Object, e As EventArgs) Handles lblDashboardKualitas.MouseEnter

        lblDashboardKualitas.BackColor = Color.FromArgb(45, 99, 181)

    End Sub

    Private Sub lblDashboard_MouseLeave(sender As Object, e As EventArgs) Handles lblDashboardKualitas.MouseLeave

        lblDashboardKualitas.BackColor = Color.Transparent

    End Sub

    'Hover Produksi
    Private Sub lblProduksi_MouseEnter(sender As Object, e As EventArgs) Handles lblKualitas.MouseEnter

        lblKualitas.BackColor = Color.FromArgb(45, 99, 181)

    End Sub

    Private Sub lblProduksi_MouseLeave(sender As Object, e As EventArgs) Handles lblKualitas.MouseLeave

        lblKualitas.BackColor = Color.Transparent

    End Sub

    'Hover NG
    Private Sub lblNG_MouseEnter(sender As Object, e As EventArgs) Handles lblNGKualitas.MouseEnter

        lblNGKualitas.BackColor = Color.FromArgb(45, 99, 181)

    End Sub

    Private Sub lblNG_MouseLeave(sender As Object, e As EventArgs) Handles lblNGKualitas.MouseLeave

        lblNGKualitas.BackColor = Color.Transparent

    End Sub

    'Pop-up PictureBox Input
    Private Sub pbInput_MouseEnter(sender As Object, e As EventArgs) Handles pbInputKualitas.MouseEnter

        pbInputKualitas.Size = New Size(
            ukuranAwalPbInput.Width + 8,
            ukuranAwalPbInput.Height + 8
        )

        pbInputKualitas.Location = New Point(
            posisiAwalPbInput.X - 4,
            posisiAwalPbInput.Y - 4
        )

    End Sub

    Private Sub pbInput_MouseLeave(sender As Object, e As EventArgs) Handles pbInputKualitas.MouseLeave

        pbInputKualitas.Size = ukuranAwalPbInput
        pbInputKualitas.Location = posisiAwalPbInput

    End Sub

    'Pop-up PictureBox Riwayat
    Private Sub pbRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles pbRiwayatKualitas.MouseEnter

        pbRiwayatKualitas.Size = New Size(
            ukuranAwalPbRiwayat.Width + 8,
            ukuranAwalPbRiwayat.Height + 8
        )

        pbRiwayatKualitas.Location = New Point(
            posisiAwalPbRiwayat.X - 4,
            posisiAwalPbRiwayat.Y - 4
        )

    End Sub

    Private Sub pbRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles pbRiwayatKualitas.MouseLeave

        pbRiwayatKualitas.Size = ukuranAwalPbRiwayat
        pbRiwayatKualitas.Location = posisiAwalPbRiwayat

    End Sub

    'Pop-up PictureBox Input
    Private Sub btnLogout_MouseEnter(sender As Object, e As EventArgs) Handles btnLogoutIK.MouseEnter

        btnLogoutIK.Size = New Size(
            ukuranAwalbtnLogout.Width + 8,
            ukuranAwalbtnLogout.Height + 8
        )

        btnLogoutIK.Location = New Point(
            posisiAwalbtnLogout.X - 4,
            posisiAwalbtnLogout.Y - 4
        )

    End Sub

    Private Sub btnLogout_MouseLeave(sender As Object, e As EventArgs) Handles btnLogoutIK.MouseLeave

        btnLogoutIK.Size = ukuranAwalbtnLogout
        btnLogoutIK.Location = posisiAwalbtnLogout

    End Sub

    Private Sub lblProduksiKualitas_MouseEnter(sender As Object, e As EventArgs) Handles lblProduksiKualitas.MouseEnter
        lblProduksiKualitas.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblProduksiKualitas_MouseLeave(sender As Object, e As EventArgs) Handles lblProduksiKualitas.MouseLeave
        lblProduksiKualitas.BackColor = Color.Transparent
    End Sub

    Private Sub btnLogoutIK_Click(sender As Object, e As EventArgs) Handles btnLogoutIK.Click

        Dim hasil As DialogResult

        hasil = MessageBox.Show(
        "Apakah kamu yakin ingin logout?",
        "Konfirmasi Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If hasil = DialogResult.Yes Then

            'Buka Form Login
            FormLogin.Show()

            'Tutup/sembunyikan dashboard
            Me.Hide()

        Else

            'Tetap di dashboard
            Me.Show()

        End If

    End Sub
End Class