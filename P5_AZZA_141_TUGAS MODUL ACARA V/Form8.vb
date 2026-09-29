Imports System.Drawing

Public Class FormProduksi

    '====================================================
    ' VARIABEL POSISI
    '====================================================

    Private posisiAwalLblNG As Point
    Private posisiAwalPictureBox3 As Point

    Private posisiTujuanLblNG As New Point(48, 185)
    Private posisiTujuanPictureBox3 As New Point(10, 185)


    '====================================================
    ' STATUS MENU
    '====================================================

    Private menuTerbuka As Boolean = False

    Private WithEvents timerMenu As New Timer()
    Private membukaMenu As Boolean = False
    Private menutupMenu As Boolean = False


    '====================================================
    ' VARIABEL HOVER
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

    Private Sub FormProduksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Simpan posisi awal
        posisiAwalLblNG = lblNG.Location
        posisiAwalPictureBox3 = PictureBox3.Location

        'Simpan ukuran dan posisi PictureBox
        ukuranAwalPbInput = pbInput.Size
        ukuranAwalPbRiwayat = pbRiwayat.Size

        posisiAwalPbInput = pbInput.Location
        posisiAwalPbRiwayat = pbRiwayat.Location

        'Simpan ukuran dan posisi Logout
        ukuranAwalBtnLogout = btnLogout.Size
        posisiAwalBtnLogout = btnLogout.Location

        'Kondisi awal
        lblInput.Visible = False
        lblRiwayat.Visible = False
        menuTerbuka = False

        lblProduksi.BackColor = Color.Transparent
        lblProduksi.Font = New Font(lblProduksi.Font, FontStyle.Regular)

        timerMenu.Interval = 5

        'Cursor tangan
        lblProduksi.Cursor = Cursors.Hand
        lblInput.Cursor = Cursors.Hand
        lblRiwayat.Cursor = Cursors.Hand
        pbInput.Cursor = Cursors.Hand
        pbRiwayat.Cursor = Cursors.Hand
        lblNG.Cursor = Cursors.Hand
        PictureBox3.Cursor = Cursors.Hand
        btnLogout.Cursor = Cursors.Hand

    End Sub


    '====================================================
    ' KLIK PRODUKSI
    '====================================================

    Private Sub lblProduksi_Click(sender As Object, e As EventArgs) Handles lblProduksi.Click

        If menuTerbuka = False Then

            menuTerbuka = True

            lblProduksi.BackColor = Color.FromArgb(45, 99, 181)
            lblProduksi.Font = New Font(lblProduksi.Font, FontStyle.Bold)

            lblInput.Visible = True
            lblRiwayat.Visible = True

            lblInput.BringToFront()
            lblRiwayat.BringToFront()

            membukaMenu = True
            menutupMenu = False

            timerMenu.Start()

        Else

            menuTerbuka = False

            lblProduksi.BackColor = Color.Transparent
            lblProduksi.Font = New Font(lblProduksi.Font, FontStyle.Regular)

            membukaMenu = False
            menutupMenu = True

            timerMenu.Start()

        End If

    End Sub


    '====================================================
    ' TIMER ANIMASI
    '====================================================

    Private Sub timerMenu_Tick(sender As Object, e As EventArgs) Handles timerMenu.Tick

        '================================================
        ' MEMBUKA MENU
        '================================================

        If membukaMenu Then

            'Animasi lblNG
            Dim yNG As Integer = lblNG.Top

            If yNG < posisiTujuanLblNG.Y Then
                yNG += 10
                If yNG > posisiTujuanLblNG.Y Then yNG = posisiTujuanLblNG.Y
            End If

            lblNG.Location = New Point(posisiTujuanLblNG.X, yNG)


            'Animasi PictureBox3
            Dim yPB As Integer = PictureBox3.Top

            If yPB < posisiTujuanPictureBox3.Y Then
                yPB += 10
                If yPB > posisiTujuanPictureBox3.Y Then yPB = posisiTujuanPictureBox3.Y
            End If

            PictureBox3.Location = New Point(posisiTujuanPictureBox3.X, yPB)


            'Animasi selesai
            If lblNG.Location = posisiTujuanLblNG AndAlso PictureBox3.Location = posisiTujuanPictureBox3 Then
                timerMenu.Stop()
                membukaMenu = False
            End If

        End If


        '================================================
        ' MENUTUP MENU
        '================================================

        If menutupMenu Then

            'Animasi lblNG
            Dim yNG As Integer = lblNG.Top

            If yNG > posisiAwalLblNG.Y Then
                yNG -= 10
                If yNG < posisiAwalLblNG.Y Then yNG = posisiAwalLblNG.Y
            End If

            lblNG.Location = New Point(posisiAwalLblNG.X, yNG)


            'Animasi PictureBox3
            Dim yPB As Integer = PictureBox3.Top

            If yPB > posisiAwalPictureBox3.Y Then
                yPB -= 10
                If yPB < posisiAwalPictureBox3.Y Then yPB = posisiAwalPictureBox3.Y
            End If

            PictureBox3.Location = New Point(posisiAwalPictureBox3.X, yPB)


            'Animasi selesai
            If lblNG.Location = posisiAwalLblNG AndAlso PictureBox3.Location = posisiAwalPictureBox3 Then
                timerMenu.Stop()
                menutupMenu = False

                lblInput.Visible = False
                lblRiwayat.Visible = False
            End If

        End If

    End Sub


    '====================================================
    ' INPUT PRODUKSI
    '====================================================

    Private Sub lblInput_Click(sender As Object, e As EventArgs) Handles lblInput.Click
        BukaFormInput()
    End Sub


    Private Sub pbInput_Click(sender As Object, e As EventArgs) Handles pbInput.Click
        BukaFormInput()
    End Sub


    Private Sub BukaFormInput()

        Dim frm As New FormInputProduksi()

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location

        frm.Show()
        Me.Hide()

    End Sub


    '====================================================
    ' RIWAYAT PRODUKSI
    '====================================================

    Private Sub lblRiwayat_Click(sender As Object, e As EventArgs) Handles lblRiwayat.Click
        BukaFormRiwayat()
    End Sub


    Private Sub pbRiwayat_Click(sender As Object, e As EventArgs) Handles pbRiwayat.Click
        BukaFormRiwayat()
    End Sub


    Private Sub BukaFormRiwayat()

        Dim frm As New FormRiwayatProduksi()

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location

        frm.Show()
        Me.Hide()

    End Sub


    '====================================================
    ' FORM PRODUK NG
    '====================================================

    Private Sub lblNG_Click(sender As Object, e As EventArgs) Handles lblNG.Click
        BukaFormProdukNG()
    End Sub


    Private Sub BukaFormProdukNG()

        Dim frm As New FormProdukNG()

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

        If menuTerbuka = False Then
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
        lblNG.BackColor = Color.Transparent
    End Sub

End Class