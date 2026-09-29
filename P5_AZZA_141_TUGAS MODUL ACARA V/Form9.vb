Imports System.Data.OleDb
Imports System.Globalization
Imports System.Drawing

Public Class FormInputProduksi

    '====================================================
    ' VARIABEL HOVER LOGOUT
    '====================================================

    Private ukuranAwalBtnLogout As Size
    Private posisiAwalBtnLogout As Point


    '====================================================
    ' FORM LOAD
    '====================================================

    Private Sub FormInputProduksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dtpTanggal.Value = DateTime.Now

        txtKode.ReadOnly = True
        txtTotalProduksi.ReadOnly = False
        txtTotalProduksi.Enabled = True

        txtKode.Clear()
        txtTotalProduksi.Clear()

        '================================================
        ' SIDEBAR STATIS
        '================================================

        lblDashboard.Visible = True
        lblProduksi.Visible = True
        lblInput.Visible = True
        lblRiwayat.Visible = True
        lblNG.Visible = True

        '================================================
        ' CURSOR MENU
        '================================================

        lblDashboard.Cursor = Cursors.Hand
        lblProduksi.Cursor = Cursors.Hand
        lblInput.Cursor = Cursors.Hand
        lblRiwayat.Cursor = Cursors.Hand
        lblNG.Cursor = Cursors.Hand

        btnLogout.Cursor = Cursors.Hand

        '================================================
        ' POSISI AWAL LOGOUT
        '================================================

        ukuranAwalBtnLogout = btnLogout.Size
        posisiAwalBtnLogout = btnLogout.Location

    End Sub


    '====================================================
    ' TOTAL PRODUKSI HANYA BOLEH ANGKA
    '====================================================

    Private Sub txtTotalProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalProduksi.KeyPress

        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If

    End Sub


    '====================================================
    ' MENENTUKAN ID PRODUK
    '====================================================

    Private Function GetIDProduk() As String

        Dim produk As String = cmbNamaProduk.Text.Trim()

        Select Case produk

            Case "Part Number 7105-5552"
                Return "P001"

            Case "Part Number 7105-5551"
                Return "P002"

            Case "Part Number 7105-3578"
                Return "P003"

            Case Else
                Return ""

        End Select

    End Function


    '====================================================
    ' MEMBUAT KODE BATCH
    '====================================================

    Private Function BuatKodeBatch(idProduk As String, tanggal As DateTime) As String

        Dim nomorProduk As String = ""

        Select Case idProduk

            Case "P001"
                nomorProduk = "1"

            Case "P002"
                nomorProduk = "2"

            Case "P003"
                nomorProduk = "3"

            Case Else
                Return ""

        End Select

        Dim bulan As String = tanggal.ToString("MMM", CultureInfo.InvariantCulture).ToUpper()
        Dim tahun As String = tanggal.ToString("yy")

        Return "B-P" & nomorProduk & bulan & tahun

    End Function


    '====================================================
    ' BUTTON KODE
    '====================================================

    Private Sub btnKode_Click(sender As Object, e As EventArgs) Handles btnKode.Click

        If cmbNamaProduk.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(cmbNamaProduk.Text) Then

            MessageBox.Show(
                "Silakan pilih nama produk terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cmbNamaProduk.Focus()
            Exit Sub

        End If

        Try

            Dim idProduk As String = GetIDProduk()

            If idProduk = "" Then

                MessageBox.Show(
                    "Produk yang dipilih belum memiliki ID Produk.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                Exit Sub

            End If

            txtKode.Text = BuatKodeBatch(idProduk, dtpTanggal.Value)

        Catch ex As Exception

            MessageBox.Show(
                "Gagal membuat kode batch." & vbCrLf & vbCrLf & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '====================================================
    ' MEMBUAT ID TOTAL PRODUK
    '====================================================

    Private Function BuatIDTotalProduk() As String

        Dim nomorTerbesar As Integer = 0

        Dim query As String =
            "SELECT [ID_Total_Produk] " &
            "FROM [Data_Pengelolaan_Total_Produksi]"

        Using cmdID As New OleDbCommand(query, CNN)

            Using reader As OleDbDataReader = cmdID.ExecuteReader()

                While reader.Read()

                    If Not IsDBNull(reader("ID_Total_Produk")) Then

                        Dim idDatabase As String =
                            reader("ID_Total_Produk").ToString().Trim()

                        If idDatabase.StartsWith("TP") Then

                            Dim angka As String =
                                idDatabase.Substring(2)

                            Dim nomor As Integer

                            If Integer.TryParse(angka, nomor) Then

                                If nomor > nomorTerbesar Then
                                    nomorTerbesar = nomor
                                End If

                            End If

                        End If

                    End If

                End While

            End Using

        End Using

        nomorTerbesar += 1

        Return "TP" & nomorTerbesar.ToString("000")

    End Function


    '====================================================
    ' BUTTON SIMPAN
    '====================================================

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        '================================================
        ' CEK NAMA PRODUK
        '================================================

        If cmbNamaProduk.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(cmbNamaProduk.Text) Then

            MessageBox.Show(
                "Silakan pilih nama produk terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cmbNamaProduk.Focus()
            Exit Sub

        End If


        '================================================
        ' CEK TOTAL PRODUKSI
        '================================================

        If String.IsNullOrWhiteSpace(txtTotalProduksi.Text) Then

            MessageBox.Show(
                "Silakan masukkan total produksi terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtTotalProduksi.Focus()
            Exit Sub

        End If


        '================================================
        ' KONVERSI TOTAL PRODUKSI
        '================================================

        Dim totalProduksi As Integer

        If Not Integer.TryParse(txtTotalProduksi.Text.Trim(), totalProduksi) Then

            MessageBox.Show(
                "Total produksi harus berupa angka.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtTotalProduksi.Focus()
            Exit Sub

        End If


        '================================================
        ' TOTAL PRODUKSI HARUS > 0
        '================================================

        If totalProduksi <= 0 Then

            MessageBox.Show(
                "Total produksi harus lebih dari 0.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtTotalProduksi.Focus()
            Exit Sub

        End If


        '================================================
        ' CEK KODE BATCH
        '================================================

        If String.IsNullOrWhiteSpace(txtKode.Text) Then

            MessageBox.Show(
                "Silakan tekan tombol Kode terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        Try

            '================================================
            ' BUKA DATABASE
            '================================================

            Koneksi()


            '================================================
            ' AMBIL ID PRODUK
            '================================================

            Dim idProduk As String = GetIDProduk()

            If idProduk = "" Then

                MessageBox.Show(
                    "Produk yang dipilih belum memiliki ID Produk.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                Exit Sub

            End If


            '================================================
            ' BUAT ID TOTAL PRODUK
            '================================================

            Dim idTotalProduk As String = BuatIDTotalProduk()


            '================================================
            ' NO ID
            '================================================

            Dim noID As String = "A004"


            '================================================
            ' QUERY INSERT
            '================================================

            Dim querySimpan As String =
                "INSERT INTO [Data_Pengelolaan_Total_Produksi] " &
                "([ID_Total_Produk], [No_ID], [ID_Produk], " &
                "[Tanggal_Produksi], [Total_Produksi], [Kode_Batch]) " &
                "VALUES (?, ?, ?, ?, ?, ?)"


            Using cmdSimpan As New OleDbCommand(querySimpan, CNN)

                Dim p1 As New OleDbParameter("@p1", OleDbType.VarWChar)
                p1.Value = idTotalProduk
                cmdSimpan.Parameters.Add(p1)

                Dim p2 As New OleDbParameter("@p2", OleDbType.VarWChar)
                p2.Value = noID
                cmdSimpan.Parameters.Add(p2)

                Dim p3 As New OleDbParameter("@p3", OleDbType.VarWChar)
                p3.Value = idProduk
                cmdSimpan.Parameters.Add(p3)

                Dim p4 As New OleDbParameter("@p4", OleDbType.Date)
                p4.Value = dtpTanggal.Value.Date
                cmdSimpan.Parameters.Add(p4)

                Dim p5 As New OleDbParameter("@p5", OleDbType.Integer)
                p5.Value = totalProduksi
                cmdSimpan.Parameters.Add(p5)

                Dim p6 As New OleDbParameter("@p6", OleDbType.VarWChar)
                p6.Value = txtKode.Text.Trim()
                cmdSimpan.Parameters.Add(p6)

                cmdSimpan.ExecuteNonQuery()

            End Using


            '================================================
            ' PESAN BERHASIL
            '================================================

            MessageBox.Show(
                "Data berhasil disimpan." & vbCrLf & vbCrLf &
                "ID Total Produk : " & idTotalProduk & vbCrLf &
                "No ID : " & noID & vbCrLf &
                "ID Produk : " & idProduk & vbCrLf &
                "Kode Batch : " & txtKode.Text,
                "Berhasil",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )


            '================================================
            ' RESET
            '================================================

            ResetForm()


        Catch ex As OleDbException

            MessageBox.Show(
                "Data gagal disimpan." & vbCrLf & vbCrLf & ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )


        Catch ex As Exception

            MessageBox.Show(
                "Data gagal disimpan." & vbCrLf & vbCrLf & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )


        Finally

            TutupKoneksi()

        End Try

    End Sub


    '====================================================
    ' BUTTON RESET
    '====================================================

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetForm()
    End Sub


    '====================================================
    ' RESET FORM
    '====================================================

    Private Sub ResetForm()

        dtpTanggal.Value = DateTime.Now
        cmbNamaProduk.SelectedIndex = -1
        txtTotalProduksi.Clear()
        txtKode.Clear()

        cmbNamaProduk.Focus()

    End Sub


    '====================================================
    ' TUTUP KONEKSI
    '====================================================

    Private Sub TutupKoneksi()

        If CNN IsNot Nothing AndAlso CNN.State = ConnectionState.Open Then
            CNN.Close()
        End If

    End Sub


    '====================================================
    ' NAVIGASI PRODUKSI
    '====================================================

    Private Sub lblProduksi_Click(sender As Object, e As EventArgs) Handles lblProduksi.Click

        Dim frm As New FormProduksi()

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location

        frm.Show()
        Me.Hide()

    End Sub


    '====================================================
    ' NAVIGASI INPUT PRODUKSI
    '====================================================

    Private Sub lblInput_Click(sender As Object, e As EventArgs) Handles lblInput.Click

        'Sudah berada di FormInputProduksi.
        'Tidak perlu membuka form baru.

    End Sub


    '====================================================
    ' NAVIGASI RIWAYAT PRODUKSI
    '====================================================

    Private Sub lblRiwayat_Click(sender As Object, e As EventArgs) Handles lblRiwayat.Click

        Dim frm As New FormRiwayatProduksi()

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location

        frm.Show()
        Me.Hide()

    End Sub


    '====================================================
    ' NAVIGASI PRODUK NG
    '====================================================

    Private Sub lblNG_Click(sender As Object, e As EventArgs) Handles lblNG.Click

        Dim frm As New FormProdukNG()

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location

        frm.Show()
        Me.Hide()

    End Sub


    '====================================================
    ' HOVER DASHBOARD
    '====================================================

    Private Sub lblDashboard_MouseEnter(sender As Object, e As EventArgs) Handles lblDashboard.MouseEnter
        lblDashboard.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblDashboard_MouseLeave(sender As Object, e As EventArgs) Handles lblDashboard.MouseLeave
        lblDashboard.BackColor = Color.Transparent
    End Sub


    '====================================================
    ' HOVER PRODUKSI
    '====================================================

    Private Sub lblProduksi_MouseEnter(sender As Object, e As EventArgs) Handles lblProduksi.MouseEnter
        lblProduksi.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblProduksi_MouseLeave(sender As Object, e As EventArgs) Handles lblProduksi.MouseLeave
        lblProduksi.BackColor = Color.Transparent
    End Sub


    '====================================================
    ' HOVER INPUT
    '====================================================

    Private Sub lblInput_MouseEnter(sender As Object, e As EventArgs) Handles lblInput.MouseEnter
        lblInput.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblInput_MouseLeave(sender As Object, e As EventArgs) Handles lblInput.MouseLeave
        lblInput.BackColor = Color.Transparent
    End Sub


    '====================================================
    ' HOVER RIWAYAT
    '====================================================

    Private Sub lblRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles lblRiwayat.MouseEnter
        lblRiwayat.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles lblRiwayat.MouseLeave
        lblRiwayat.BackColor = Color.Transparent
    End Sub


    '====================================================
    ' HOVER PRODUK NG
    '====================================================

    Private Sub lblNG_MouseEnter(sender As Object, e As EventArgs) Handles lblNG.MouseEnter
        lblNG.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblNG_MouseLeave(sender As Object, e As EventArgs) Handles lblNG.MouseLeave
        lblNG.BackColor = Color.Transparent
    End Sub


    '====================================================
    ' HOVER BUTTON LOGOUT
    '====================================================

    Private Sub btnLogout_MouseEnter(sender As Object, e As EventArgs) Handles btnLogout.MouseEnter

        btnLogout.Size = New Size(
            ukuranAwalBtnLogout.Width + 4,
            ukuranAwalBtnLogout.Height + 4
        )

        btnLogout.Location = New Point(
            posisiAwalBtnLogout.X - 2,
            posisiAwalBtnLogout.Y - 2
        )

    End Sub


    Private Sub btnLogout_MouseLeave(sender As Object, e As EventArgs) Handles btnLogout.MouseLeave

        btnLogout.Size = ukuranAwalBtnLogout
        btnLogout.Location = posisiAwalBtnLogout

    End Sub


    '====================================================
    ' BUTTON LOGOUT
    '====================================================

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click

        Dim frm As New FormLogin()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()

        Me.Close()

    End Sub

End Class