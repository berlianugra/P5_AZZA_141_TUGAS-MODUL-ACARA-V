Imports System.Drawing

Public Class FormProdukNG

    '====================================================
    ' STATUS MENU
    '====================================================

    Private menuNGTerbuka As Boolean = False

    '====================================================
    ' POSISI & UKURAN AWAL
    '====================================================

    Private ukuranAwalPbInput As Size
    Private ukuranAwalPbRiwayat As Size
    Private posisiAwalPbInput As Point
    Private posisiAwalPbRiwayat As Point
    Private ukuranAwalBtnLogout As Size
    Private posisiAwalBtnLogout As Point

    '====================================================
    ' FORM LOAD
    '====================================================

    Private Sub FormProdukNG_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Simpan ukuran dan posisi awal
        ukuranAwalPbInput = pbInput.Size
        ukuranAwalPbRiwayat = pbRiwayat.Size
        posisiAwalPbInput = pbInput.Location
        posisiAwalPbRiwayat = pbRiwayat.Location
        ukuranAwalBtnLogout = btnLogout.Size
        posisiAwalBtnLogout = btnLogout.Location

        'Kondisi awal
        lblInput.Visible = False
        lblRiwayat.Visible = False
        menuNGTerbuka = False

        lblProduksi.BackColor = Color.Transparent
        lblNG.BackColor = Color.Transparent
        lblProduksi.Font = New Font(lblProduksi.Font, FontStyle.Regular)

        'Cursor
        lblProduksi.Cursor = Cursors.Hand
        lblNG.Cursor = Cursors.Hand
        lblInput.Cursor = Cursors.Hand
        lblRiwayat.Cursor = Cursors.Hand
        pbInput.Cursor = Cursors.Hand
        pbRiwayat.Cursor = Cursors.Hand
        btnLogout.Cursor = Cursors.Hand

    End Sub

    '====================================================
    ' KLIK lblProduksi
    '====================================================

    Private Sub lblProduksi_Click(sender As Object, e As EventArgs) Handles lblProduksi.Click

        'Aktifkan tampilan lblProduksi
        lblProduksi.BackColor = Color.FromArgb(45, 99, 181)
        lblProduksi.Font = New Font(lblProduksi.Font, FontStyle.Bold)

        'Buka Form Produksi
        Dim frm As New FormProduksi()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()
        Me.Hide()

    End Sub

    '====================================================
    ' KLIK lblNG
    '====================================================

    Private Sub lblNG_Click(sender As Object, e As EventArgs) Handles lblNG.Click

        If Not menuNGTerbuka Then

            menuNGTerbuka = True
            lblNG.BackColor = Color.FromArgb(45, 99, 181)

            lblInput.Visible = True
            lblRiwayat.Visible = True

            lblInput.BringToFront()
            lblRiwayat.BringToFront()

        Else

            menuNGTerbuka = False
            lblNG.BackColor = Color.Transparent
            lblInput.Visible = False
            lblRiwayat.Visible = False

        End If

    End Sub

    '====================================================
    ' INPUT NG
    '====================================================

    Private Sub lblInput_Click(sender As Object, e As EventArgs) Handles lblInput.Click
        BukaFormInputNG()
    End Sub

    Private Sub pbInput_Click(sender As Object, e As EventArgs) Handles pbInput.Click
        BukaFormInputNG()
    End Sub

    Private Sub BukaFormInputNG()

        Dim frm As New FormInputNG()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()
        Me.Hide()

    End Sub

    '====================================================
    ' RIWAYAT NG
    '====================================================

    Private Sub lblRiwayat_Click(sender As Object, e As EventArgs) Handles lblRiwayat.Click
        BukaFormRiwayatNG()
    End Sub

    Private Sub pbRiwayat_Click(sender As Object, e As EventArgs) Handles pbRiwayat.Click
        BukaFormRiwayatNG()
    End Sub

    Private Sub BukaFormRiwayatNG()

        Dim frm As New FormRiwayatNG()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()
        Me.Hide()

    End Sub

    '====================================================
    ' HOVER lblInput
    '====================================================

    Private Sub lblInput_MouseEnter(sender As Object, e As EventArgs) Handles lblInput.MouseEnter
        lblInput.ForeColor = Color.DarkOrange
    End Sub

    Private Sub lblInput_MouseLeave(sender As Object, e As EventArgs) Handles lblInput.MouseLeave
        lblInput.ForeColor = Color.White
    End Sub

    '====================================================
    ' HOVER lblRiwayat
    '====================================================

    Private Sub lblRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles lblRiwayat.MouseEnter
        lblRiwayat.ForeColor = Color.DarkOrange
    End Sub

    Private Sub lblRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles lblRiwayat.MouseLeave
        lblRiwayat.ForeColor = Color.White
    End Sub

    '====================================================
    ' HOVER pbInput
    '====================================================

    Private Sub pbInput_MouseEnter(sender As Object, e As EventArgs) Handles pbInput.MouseEnter

        pbInput.Size = New Size(ukuranAwalPbInput.Width + 10, ukuranAwalPbInput.Height + 10)
        pbInput.Location = New Point(posisiAwalPbInput.X - 5, posisiAwalPbInput.Y - 5)

    End Sub

    Private Sub pbInput_MouseLeave(sender As Object, e As EventArgs) Handles pbInput.MouseLeave

        pbInput.Size = ukuranAwalPbInput
        pbInput.Location = posisiAwalPbInput

    End Sub

    '====================================================
    ' HOVER pbRiwayat
    '====================================================

    Private Sub pbRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles pbRiwayat.MouseEnter

        pbRiwayat.Size = New Size(ukuranAwalPbRiwayat.Width + 10, ukuranAwalPbRiwayat.Height + 10)
        pbRiwayat.Location = New Point(posisiAwalPbRiwayat.X - 5, posisiAwalPbRiwayat.Y - 5)

    End Sub

    Private Sub pbRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles pbRiwayat.MouseLeave

        pbRiwayat.Size = ukuranAwalPbRiwayat
        pbRiwayat.Location = posisiAwalPbRiwayat

    End Sub

    '====================================================
    ' HOVER BUTTON LOGOUT
    '====================================================

    Private Sub btnLogout_MouseEnter(sender As Object, e As EventArgs) Handles btnLogout.MouseEnter

        btnLogout.Size = New Size(ukuranAwalBtnLogout.Width + 4, ukuranAwalBtnLogout.Height + 4)
        btnLogout.Location = New Point(posisiAwalBtnLogout.X - 2, posisiAwalBtnLogout.Y - 2)

    End Sub

    Private Sub btnLogout_MouseLeave(sender As Object, e As EventArgs) Handles btnLogout.MouseLeave

        btnLogout.Size = ukuranAwalBtnLogout
        btnLogout.Location = posisiAwalBtnLogout

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click

        Dim frm As New FormLogin()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()

        Me.Close()

    End Sub

    '====================================================
    ' HOVER lblDashboard
    '====================================================

    Private Sub lblDashboard_MouseEnter(sender As Object, e As EventArgs) Handles lblDashboard.MouseEnter
        lblDashboard.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblDashboard_MouseLeave(sender As Object, e As EventArgs) Handles lblDashboard.MouseLeave
        lblDashboard.BackColor = Color.Transparent
    End Sub

    '====================================================
    ' HOVER lblProduksi
    '====================================================

    Private Sub lblProduksi_MouseEnter(sender As Object, e As EventArgs) Handles lblProduksi.MouseEnter
        lblProduksi.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblProduksi_MouseLeave(sender As Object, e As EventArgs) Handles lblProduksi.MouseLeave

        If Not lblProduksi.Font.Bold Then
            lblProduksi.BackColor = Color.Transparent
        End If

    End Sub

    '====================================================
    ' HOVER lblNG
    '====================================================

    Private Sub lblNG_MouseEnter(sender As Object, e As EventArgs) Handles lblNG.MouseEnter
        lblNG.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblNG_MouseLeave(sender As Object, e As EventArgs) Handles lblNG.MouseLeave

        If Not menuNGTerbuka Then
            lblNG.BackColor = Color.Transparent
        End If

    End Sub

End Class