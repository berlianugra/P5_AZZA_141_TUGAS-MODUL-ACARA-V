Imports System.Data.OleDb

Public Class FormDataProduk

    '==========================================================
    ' STATUS EDIT
    '==========================================================
    Private modeEdit As Boolean = False


    '==========================================================
    ' FORM LOAD
    '==========================================================
    Private Sub FormDataProduk_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterScreen

        AturDGV()
        KunciInput()
        LoadDataProduk()

    End Sub


    '==========================================================
    ' ATUR DATAGRIDVIEW
    '==========================================================
    Private Sub AturDGV()

        With dgvProduk

            .AutoGenerateColumns = True
            .ReadOnly = True

            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False

            .MultiSelect = False

            .SelectionMode =
                DataGridViewSelectionMode.FullRowSelect

            .AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill

            .RowHeadersVisible = False

        End With

    End Sub


    '==========================================================
    ' KUNCI INPUT
    '==========================================================
    Private Sub KunciInput()

        txtIDProduk.ReadOnly = True
        txtNamaProduk.ReadOnly = True
        txtSpesifikasiProduk.ReadOnly = True
        txtHPP.ReadOnly = True

        txtIDProduk.BackColor = Color.White
        txtNamaProduk.BackColor = Color.White
        txtSpesifikasiProduk.BackColor = Color.White
        txtHPP.BackColor = Color.White

        modeEdit = False

    End Sub


    '==========================================================
    ' BUKA INPUT SAAT EDIT
    '==========================================================
    Private Sub BukaInput()

        ' ID PRODUK TIDAK BOLEH DIUBAH
        txtIDProduk.ReadOnly = True

        txtNamaProduk.ReadOnly = False
        txtSpesifikasiProduk.ReadOnly = False
        txtHPP.ReadOnly = False

        txtNamaProduk.BackColor = Color.White
        txtSpesifikasiProduk.BackColor = Color.White
        txtHPP.BackColor = Color.White

        modeEdit = True

        txtNamaProduk.Focus()

    End Sub


    '==========================================================
    ' LOAD DATA PRODUK
    '==========================================================
    Private Sub LoadDataProduk()

        Try

            Koneksi()

            Dim query As String =
                "SELECT ID_Produk, Nama_Produk, " &
                "Spesifikasi_Produk, HPP " &
                "FROM Data_Produk " &
                "ORDER BY ID_Produk"

            da = New OleDbDataAdapter(
                query,
                CNN
            )

            dt = New DataTable()

            da.Fill(dt)

            dgvProduk.DataSource = dt

            CNN.Close()

            AturKolomDGV()

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menampilkan data produk." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' ATUR HEADER DGV
    '==========================================================
    Private Sub AturKolomDGV()

        If dgvProduk.Columns.Count = 0 Then
            Exit Sub
        End If


        If dgvProduk.Columns.Contains("ID_Produk") Then

            dgvProduk.Columns("ID_Produk").HeaderText =
                "ID Produk"

        End If


        If dgvProduk.Columns.Contains("Nama_Produk") Then

            dgvProduk.Columns("Nama_Produk").HeaderText =
                "Nama Produk"

        End If


        If dgvProduk.Columns.Contains("Spesifikasi_Produk") Then

            dgvProduk.Columns("Spesifikasi_Produk").HeaderText =
                "Spesifikasi Produk"

        End If


        If dgvProduk.Columns.Contains("HPP") Then

            dgvProduk.Columns("HPP").HeaderText =
                "HPP"

        End If

    End Sub


    '==========================================================
    ' KLIK DATA PADA DGV
    '==========================================================
    Private Sub dgvProduk_CellClick(
        sender As Object,
        e As DataGridViewCellEventArgs
    ) Handles dgvProduk.CellClick

        If e.RowIndex < 0 Then
            Exit Sub
        End If


        Try

            Dim row As DataGridViewRow =
                dgvProduk.Rows(e.RowIndex)


            '==================================================
            ' ID PRODUK
            '==================================================
            If row.Cells("ID_Produk").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("ID_Produk").Value) Then

                txtIDProduk.Text =
                    row.Cells("ID_Produk").Value.ToString()

            Else

                txtIDProduk.Clear()

            End If


            '==================================================
            ' NAMA PRODUK
            '==================================================
            If row.Cells("Nama_Produk").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("Nama_Produk").Value) Then

                txtNamaProduk.Text =
                    row.Cells("Nama_Produk").Value.ToString()

            Else

                txtNamaProduk.Clear()

            End If


            '==================================================
            ' SPESIFIKASI PRODUK
            '==================================================
            If row.Cells("Spesifikasi_Produk").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("Spesifikasi_Produk").Value) Then

                txtSpesifikasiProduk.Text =
                    row.Cells("Spesifikasi_Produk").Value.ToString()

            Else

                txtSpesifikasiProduk.Clear()

            End If


            '==================================================
            ' HPP
            '==================================================
            If row.Cells("HPP").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("HPP").Value) Then

                txtHPP.Text =
                    row.Cells("HPP").Value.ToString()

            Else

                txtHPP.Clear()

            End If


            '==================================================
            ' DATA TETAP TERKUNCI SETELAH KLIK DGV
            '==================================================
            KunciInput()


        Catch ex As Exception

            MessageBox.Show(
                "Gagal mengambil data produk." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' TOMBOL EDIT
    '==========================================================
    Private Sub btnEdit_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnEdit.Click

        If txtIDProduk.Text.Trim() = "" Then

            MessageBox.Show(
                "Pilih data produk yang ingin diedit terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If modeEdit = False Then

            BukaInput()

        Else

            MessageBox.Show(
                "Data sedang dalam mode edit." &
                vbCrLf &
                "Silakan ubah data kemudian klik Simpan.",
                "Informasi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

        End If

    End Sub


    '==========================================================
    ' TOMBOL SIMPAN
    '==========================================================
    Private Sub btnSimpan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnSimpan.Click

        '======================================================
        ' HARUS DALAM MODE EDIT
        '======================================================
        If modeEdit = False Then

            MessageBox.Show(
                "Klik Edit terlebih dahulu sebelum menyimpan perubahan.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        '======================================================
        ' VALIDASI ID
        '======================================================
        If txtIDProduk.Text.Trim() = "" Then

            MessageBox.Show(
                "ID Produk tidak boleh kosong.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        '======================================================
        ' VALIDASI NAMA
        '======================================================
        If txtNamaProduk.Text.Trim() = "" Then

            MessageBox.Show(
                "Nama Produk tidak boleh kosong.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtNamaProduk.Focus()

            Exit Sub

        End If


        '======================================================
        ' VALIDASI SPESIFIKASI
        '======================================================
        If txtSpesifikasiProduk.Text.Trim() = "" Then

            MessageBox.Show(
                "Spesifikasi Produk tidak boleh kosong.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtSpesifikasiProduk.Focus()

            Exit Sub

        End If


        '======================================================
        ' VALIDASI HPP
        '======================================================
        If txtHPP.Text.Trim() = "" Then

            MessageBox.Show(
                "HPP tidak boleh kosong.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtHPP.Focus()

            Exit Sub

        End If


        '======================================================
        ' CEK HPP HARUS ANGKA
        '======================================================
        Dim nilaiHPP As Decimal

        If Not Decimal.TryParse(
            txtHPP.Text.Trim(),
            nilaiHPP
        ) Then

            MessageBox.Show(
                "HPP harus berupa angka.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtHPP.Focus()

            Exit Sub

        End If


        '======================================================
        ' KONFIRMASI
        '======================================================
        Dim konfirmasi As DialogResult =
            MessageBox.Show(
                "Apakah Anda yakin ingin menyimpan perubahan data produk ini?",
                "Konfirmasi Simpan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )


        If konfirmasi <> DialogResult.Yes Then
            Exit Sub
        End If


        Try

            Koneksi()


            '==================================================
            ' QUERY UPDATE
            '==================================================
            Dim query As String =
                "UPDATE Data_Produk SET " &
                "Nama_Produk = ?, " &
                "Spesifikasi_Produk = ?, " &
                "HPP = ? " &
                "WHERE ID_Produk = ?"


            cmd = New OleDbCommand(
                query,
                CNN
            )


            '==================================================
            ' PARAMETER
            '==================================================
            cmd.Parameters.AddWithValue(
                "@NamaProduk",
                txtNamaProduk.Text.Trim()
            )


            cmd.Parameters.AddWithValue(
                "@Spesifikasi",
                txtSpesifikasiProduk.Text.Trim()
            )


            cmd.Parameters.AddWithValue(
                "@HPP",
                nilaiHPP
            )


            cmd.Parameters.AddWithValue(
                "@IDProduk",
                txtIDProduk.Text.Trim()
            )


            '==================================================
            ' EKSEKUSI UPDATE
            '==================================================
            Dim jumlah As Integer =
                cmd.ExecuteNonQuery()


            CNN.Close()


            If jumlah > 0 Then

                MessageBox.Show(
                    "Data produk berhasil diperbarui.",
                    "Berhasil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            Else

                MessageBox.Show(
                    "Data produk tidak ditemukan atau tidak ada perubahan.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            End If


            '==================================================
            ' KEMBALIKAN KE KONDISI AWAL
            '==================================================
            modeEdit = False

            KunciInput()

            LoadDataProduk()

            BersihkanInput()

            dgvProduk.ClearSelection()


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menyimpan perubahan data produk." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' TOMBOL HAPUS
    '==========================================================
    Private Sub btnHapus_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnHapus.Click

        If txtIDProduk.Text.Trim() = "" Then

            MessageBox.Show(
                "Pilih data produk yang ingin dihapus terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        '======================================================
        ' KONFIRMASI HAPUS
        '======================================================
        Dim konfirmasi As DialogResult =
            MessageBox.Show(
                "Apakah Anda yakin ingin menghapus data produk berikut?" &
                vbCrLf & vbCrLf &
                "ID Produk : " & txtIDProduk.Text &
                vbCrLf &
                "Nama Produk : " & txtNamaProduk.Text,
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )


        If konfirmasi <> DialogResult.Yes Then
            Exit Sub
        End If


        Try

            Koneksi()


            '==================================================
            ' QUERY DELETE
            '==================================================
            Dim query As String =
                "DELETE FROM Data_Produk " &
                "WHERE ID_Produk = ?"


            cmd = New OleDbCommand(
                query,
                CNN
            )


            cmd.Parameters.AddWithValue(
                "@IDProduk",
                txtIDProduk.Text.Trim()
            )


            Dim jumlah As Integer =
                cmd.ExecuteNonQuery()


            CNN.Close()


            If jumlah > 0 Then

                MessageBox.Show(
                    "Data produk berhasil dihapus.",
                    "Berhasil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            Else

                MessageBox.Show(
                    "Data produk tidak ditemukan.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            End If


            '==================================================
            ' RESET
            '==================================================
            modeEdit = False

            KunciInput()

            BersihkanInput()

            LoadDataProduk()

            dgvProduk.ClearSelection()


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menghapus data produk." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' TOMBOL CARI
    '==========================================================
    Private Sub btnCari_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnCari.Click

        CariDataProduk()

    End Sub


    '==========================================================
    ' ENTER PADA TEXTBOX CARI
    '==========================================================
    Private Sub txtCari_KeyDown(
        sender As Object,
        e As KeyEventArgs
    ) Handles txtHPP.KeyDown

        If e.KeyCode = Keys.Enter Then

            e.SuppressKeyPress = True

            CariDataProduk()

        End If

    End Sub


    '==========================================================
    ' CARI DATA PRODUK
    '==========================================================
    Private Sub CariDataProduk()

        Try

            Koneksi()


            Dim keyword As String =
                txtHPP.Text.Trim()


            '==================================================
            ' JIKA PENCARIAN KOSONG
            '==================================================
            If keyword = "" Then

                CNN.Close()

                LoadDataProduk()

                BersihkanInput()

                KunciInput()

                dgvProduk.ClearSelection()

                Exit Sub

            End If


            '==================================================
            ' QUERY SEARCH
            '==================================================
            Dim query As String =
                "SELECT ID_Produk, Nama_Produk, " &
                "Spesifikasi_Produk, HPP " &
                "FROM Data_Produk " &
                "WHERE ID_Produk LIKE ? " &
                "OR Nama_Produk LIKE ? " &
                "OR Spesifikasi_Produk LIKE ? " &
                "OR HPP LIKE ? " &
                "ORDER BY ID_Produk"


            cmd = New OleDbCommand(
                query,
                CNN
            )


            '==================================================
            ' PARAMETER
            '==================================================
            cmd.Parameters.AddWithValue(
                "@p1",
                "%" & keyword & "%"
            )


            cmd.Parameters.AddWithValue(
                "@p2",
                "%" & keyword & "%"
            )


            cmd.Parameters.AddWithValue(
                "@p3",
                "%" & keyword & "%"
            )


            cmd.Parameters.AddWithValue(
                "@p4",
                "%" & keyword & "%"
            )


            '==================================================
            ' TAMPILKAN HASIL KE DGV
            '==================================================
            da = New OleDbDataAdapter(cmd)

            dt = New DataTable()

            da.Fill(dt)

            dgvProduk.DataSource = dt


            CNN.Close()


            AturKolomDGV()

            BersihkanInput()

            KunciInput()

            dgvProduk.ClearSelection()


            '==================================================
            ' JIKA DATA TIDAK DITEMUKAN
            '==================================================
            If dt.Rows.Count = 0 Then

                MessageBox.Show(
                    "Data produk tidak ditemukan.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            End If


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mencari data produk." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' TOMBOL RESET
    '==========================================================
    Private Sub btnReset_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnReset.Click

        txtHPP.Clear()

        BersihkanInput()

        KunciInput()

        LoadDataProduk()

        dgvProduk.ClearSelection()

    End Sub


    '==========================================================
    ' BERSIHKAN INPUT
    '==========================================================
    Private Sub BersihkanInput()

        txtIDProduk.Clear()
        txtNamaProduk.Clear()
        txtSpesifikasiProduk.Clear()
        txtHPP.Clear()

    End Sub


    '==========================================================
    ' TUTUP KONEKSI
    '==========================================================
    Private Sub TutupKoneksi()

        Try

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If

        Catch
            ' Tidak melakukan apa-apa
        End Try

    End Sub


    '==========================================================
    ' TOMBOL KEMBALI
    '==========================================================
    Private Sub btnKembali_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnKembali.Click

        FormDashboardKualitas.Show()

        Me.Hide()

    End Sub

End Class