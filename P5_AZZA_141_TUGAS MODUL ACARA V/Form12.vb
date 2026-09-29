Imports System.Data.OleDb

Public Class FormInputNG

    Private ukuranAwalBtnLogout As Size
    Private posisiAwalBtnLogout As Point

    '========================================================
    ' FORM LOAD
    '========================================================

    Private Sub FormInputNG_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Tanggal default hari ini
        dtpTanggal.Value = Date.Today

        'Jumlah NG harus bisa diinput
        txtJumlahNG.ReadOnly = False

        'Jangan menambahkan Items ke ComboBox.
        'Collection sudah dibuat dari Designer.
        cbNamaProduk.SelectedIndex = -1
        cbJenisNG.SelectedIndex = -1
        txtJumlahNG.Clear()

        'Simpan ukuran dan posisi awal tombol logout
        ukuranAwalBtnLogout = btnLogout.Size
        posisiAwalBtnLogout = btnLogout.Location

        'Cursor menu
        lblDashboard.Cursor = Cursors.Hand
        lblProduksi.Cursor = Cursors.Hand
        lblNG.Cursor = Cursors.Hand
        lblInput.Cursor = Cursors.Hand
        lblRiwayat.Cursor = Cursors.Hand
        btnLogout.Cursor = Cursors.Hand

        'Biar keliatan
        lblInput.Visible = True
        lblRiwayat.Visible = True
        lblInput.BringToFront()
        lblRiwayat.BringToFront()
    End Sub


    '========================================================
    ' MAPPING NAMA PRODUK → ID PRODUK
    '========================================================

    Private Function AmbilIDProduk() As String

        Select Case cbNamaProduk.Text.Trim()

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


    '========================================================
    ' MEMBUAT ID_NG OTOMATIS
    '========================================================

    Private Function BuatIDNG() As String

        Dim nomorTerbesar As Integer = 0

        Try

            Koneksi()

            Dim query As String = "SELECT [ID_NG] FROM [Data_Produk_NG]"

            Using cmdID As New OleDbCommand(query, CNN)

                Using reader As OleDbDataReader = cmdID.ExecuteReader()

                    While reader.Read()

                        If Not IsDBNull(reader("ID_NG")) Then

                            Dim idLama As String = reader("ID_NG").ToString().Trim()

                            If idLama.Length >= 3 AndAlso
                               idLama.StartsWith("NG", StringComparison.OrdinalIgnoreCase) Then

                                Dim angka As Integer

                                If Integer.TryParse(idLama.Substring(2), angka) Then

                                    If angka > nomorTerbesar Then
                                        nomorTerbesar = angka
                                    End If

                                End If

                            End If

                        End If

                    End While

                End Using

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Gagal mengecek ID NG dari database." &
                vbCrLf &
                ex.Message
            )

        Finally

            TutupKoneksi()

        End Try

        Return "NG" & (nomorTerbesar + 1).ToString("000")

    End Function


    '========================================================
    ' MENCARI ID_TOTAL_PRODUK
    '========================================================

    Private Function CariIDTotalProduk(idProduk As String, tanggal As Date) As String

        Dim idTotalProduk As String = ""

        Try

            Koneksi()

            Dim tanggalAwal As Date = tanggal.Date
            Dim tanggalBesok As Date = tanggalAwal.AddDays(1)

            Dim query As String =
                "SELECT [ID_Total_Produk] " &
                "FROM [Data_Pengelolaan_Total_Produksi] " &
                "WHERE [ID_Produk] = ? " &
                "AND [Tanggal_Produksi] >= ? " &
                "AND [Tanggal_Produksi] < ?"

            Using cmdCari As New OleDbCommand(query, CNN)

                cmdCari.Parameters.Add("@p1", OleDbType.VarWChar).Value = idProduk
                cmdCari.Parameters.Add("@p2", OleDbType.Date).Value = tanggalAwal
                cmdCari.Parameters.Add("@p3", OleDbType.Date).Value = tanggalBesok

                Dim hasil As Object = cmdCari.ExecuteScalar()

                If hasil IsNot Nothing AndAlso hasil IsNot DBNull.Value Then
                    idTotalProduk = hasil.ToString().Trim()
                End If

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Gagal mencari ID Total Produk." &
                vbCrLf &
                ex.Message
            )

        Finally

            TutupKoneksi()

        End Try

        Return idTotalProduk

    End Function


    '========================================================
    ' BUTTON SIMPAN
    '========================================================

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        '====================================================
        ' VALIDASI NAMA PRODUK
        '====================================================

        If cbNamaProduk.SelectedIndex = -1 OrElse
           String.IsNullOrWhiteSpace(cbNamaProduk.Text) Then

            MessageBox.Show(
                "Silakan pilih nama produk terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cbNamaProduk.Focus()
            Exit Sub

        End If


        '====================================================
        ' VALIDASI JENIS NG
        '====================================================

        If cbJenisNG.SelectedIndex = -1 OrElse
           String.IsNullOrWhiteSpace(cbJenisNG.Text) Then

            MessageBox.Show(
                "Silakan pilih jenis NG terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cbJenisNG.Focus()
            Exit Sub

        End If


        '====================================================
        ' VALIDASI JUMLAH NG
        '====================================================

        Dim jumlahNG As Double

        If String.IsNullOrWhiteSpace(txtJumlahNG.Text) Then

            MessageBox.Show(
                "Silakan masukkan jumlah NG.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtJumlahNG.Focus()
            Exit Sub

        End If


        If Not Double.TryParse(txtJumlahNG.Text.Trim(), jumlahNG) Then

            MessageBox.Show(
                "Jumlah NG harus berupa angka.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtJumlahNG.Focus()
            Exit Sub

        End If


        If jumlahNG <= 0 Then

            MessageBox.Show(
                "Jumlah NG harus lebih dari 0.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtJumlahNG.Focus()
            Exit Sub

        End If


        Try

            '================================================
            ' AMBIL ID PRODUK DARI COMBOBOX
            '================================================

            Dim idProduk As String = AmbilIDProduk()

            If idProduk = "" Then

                MessageBox.Show(
                    "ID produk untuk nama produk yang dipilih tidak ditemukan.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )

                Exit Sub

            End If


            '================================================
            ' CARI ID_TOTAL_PRODUK
            '================================================

            Dim idTotalProduk As String =
                CariIDTotalProduk(idProduk, dtpTanggal.Value)

            If idTotalProduk = "" Then

                MessageBox.Show(
                    "Data produksi untuk produk " &
                    cbNamaProduk.Text &
                    " pada tanggal " &
                    dtpTanggal.Value.ToString("dd/MM/yyyy") &
                    " tidak ditemukan." &
                    vbCrLf &
                    vbCrLf &
                    "Pastikan data produksi untuk produk dan tanggal tersebut sudah ada.",
                    "Data Tidak Ditemukan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                Exit Sub

            End If


            '================================================
            ' BUAT ID_NG OTOMATIS
            '================================================

            Dim idNG As String = BuatIDNG()


            '================================================
            ' SIMPAN KE DATABASE
            '================================================

            Koneksi()

            Dim queryInsert As String =
                "INSERT INTO [Data_Produk_NG] " &
                "([ID_NG], [No_ID], [ID_Total_Produk], [Jumlah_Jenis_NG], [Jenis_NG], [Jumlah_NG]) " &
                "VALUES (?, ?, ?, ?, ?, ?)"

            Using cmdSimpan As New OleDbCommand(queryInsert, CNN)

                cmdSimpan.Parameters.Add("@p1", OleDbType.VarWChar).Value = idNG
                cmdSimpan.Parameters.Add("@p2", OleDbType.VarWChar).Value = "A004"
                cmdSimpan.Parameters.Add("@p3", OleDbType.VarWChar).Value = idTotalProduk
                cmdSimpan.Parameters.Add("@p4", OleDbType.Integer).Value = 3
                cmdSimpan.Parameters.Add("@p5", OleDbType.VarWChar).Value = cbJenisNG.Text.Trim()
                cmdSimpan.Parameters.Add("@p6", OleDbType.Double).Value = jumlahNG

                cmdSimpan.ExecuteNonQuery()

            End Using


            '================================================
            ' BERHASIL
            '================================================

            MessageBox.Show(
                "Data NG berhasil disimpan." &
                vbCrLf &
                vbCrLf &
                "ID NG : " & idNG &
                vbCrLf &
                "No ID : A004" &
                vbCrLf &
                "ID Total Produk : " & idTotalProduk &
                vbCrLf &
                "Jumlah Jenis NG : 3" &
                vbCrLf &
                "Jenis NG : " & cbJenisNG.Text &
                vbCrLf &
                "Jumlah NG : " & jumlahNG.ToString("#,##0"),
                "Berhasil",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )


            '================================================
            ' RESET FORM SETELAH BERHASIL
            '================================================

            ResetForm()

        Catch ex As Exception

            MessageBox.Show(
                "Data gagal disimpan." &
                vbCrLf &
                vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        Finally

            TutupKoneksi()

        End Try

    End Sub


    '========================================================
    ' BUTTON RESET
    '========================================================

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetForm()
    End Sub


    '========================================================
    ' FUNGSI RESET FORM
    '========================================================

    Private Sub ResetForm()

        dtpTanggal.Value = Date.Today
        cbNamaProduk.SelectedIndex = -1
        cbJenisNG.SelectedIndex = -1
        txtJumlahNG.Clear()
        cbNamaProduk.Focus()

    End Sub


    '========================================================
    ' TUTUP KONEKSI
    '========================================================

    Private Sub TutupKoneksi()

        If CNN IsNot Nothing Then

            If CNN.State = ConnectionState.Open Then
                CNN.Close()
            End If

        End If

    End Sub


    '========================================================
    ' NAVIGASI MENU
    '========================================================

    Private Sub lblProduksi_Click(sender As Object, e As EventArgs) Handles lblProduksi.Click

        Dim frm As New FormProduksi()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()
        Me.Hide()

    End Sub


    Private Sub lblNG_Click(sender As Object, e As EventArgs) Handles lblNG.Click

        Dim frm As New FormProdukNG()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()
        Me.Hide()

    End Sub


    Private Sub lblRiwayat_Click(sender As Object, e As EventArgs) Handles lblRiwayat.Click

        Dim frm As New FormRiwayatNG()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()
        Me.Hide()

    End Sub


    '========================================================
    ' HOVER DASHBOARD
    '========================================================

    Private Sub lblDashboard_MouseEnter(sender As Object, e As EventArgs) Handles lblDashboard.MouseEnter
        lblDashboard.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblDashboard_MouseLeave(sender As Object, e As EventArgs) Handles lblDashboard.MouseLeave
        lblDashboard.BackColor = Color.Transparent
    End Sub


    '========================================================
    ' HOVER PRODUKSI
    '========================================================

    Private Sub lblProduksi_MouseEnter(sender As Object, e As EventArgs) Handles lblProduksi.MouseEnter
        lblProduksi.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblProduksi_MouseLeave(sender As Object, e As EventArgs) Handles lblProduksi.MouseLeave
        lblProduksi.BackColor = Color.Transparent
    End Sub


    '========================================================
    ' HOVER NG
    '========================================================

    Private Sub lblNG_MouseEnter(sender As Object, e As EventArgs) Handles lblNG.MouseEnter
        lblNG.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblNG_MouseLeave(sender As Object, e As EventArgs) Handles lblNG.MouseLeave
        lblNG.BackColor = Color.Transparent
    End Sub


    '========================================================
    ' HOVER INPUT
    '========================================================

    Private Sub lblInput_MouseEnter(sender As Object, e As EventArgs) Handles lblInput.MouseEnter
        lblInput.ForeColor = Color.DarkOrange
    End Sub

    Private Sub lblInput_MouseLeave(sender As Object, e As EventArgs) Handles lblInput.MouseLeave
        lblInput.ForeColor = Color.White
    End Sub


    '========================================================
    ' HOVER RIWAYAT
    '========================================================

    Private Sub lblRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles lblRiwayat.MouseEnter
        lblRiwayat.ForeColor = Color.DarkOrange
    End Sub

    Private Sub lblRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles lblRiwayat.MouseLeave
        lblRiwayat.ForeColor = Color.White
    End Sub


    '========================================================
    ' HOVER LOGOUT
    '========================================================

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

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click

        Dim frm As New FormLogin()
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()

        Me.Close()

    End Sub

    Private Sub btnLogout_MouseLeave(sender As Object, e As EventArgs) Handles btnLogout.MouseLeave

        btnLogout.Size = ukuranAwalBtnLogout
        btnLogout.Location = posisiAwalBtnLogout

    End Sub

End Class