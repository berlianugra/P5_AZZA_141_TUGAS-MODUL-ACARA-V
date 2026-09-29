Imports System.Data.OleDb

Public Class FormAkun

    '==========================================================
    ' STATUS FORM
    '==========================================================
    Private modeEdit As Boolean = False


    '==========================================================
    ' FORM LOAD
    '==========================================================
    Private Sub FormAkun_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterScreen

        AturDGV()
        LoadJabatan()
        KunciInput()
        LoadDataAkun()

        txtNoID.Clear()
        txtNama.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        cmbJabatan.SelectedIndex = -1
        txtCari.Clear()

    End Sub


    '==========================================================
    ' ATUR DATAGRIDVIEW
    '==========================================================
    Private Sub AturDGV()

        With dgvAkun

            .AutoGenerateColumns = True
            .ReadOnly = True

            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False

            .MultiSelect = False

            .SelectionMode =
                DataGridViewSelectionMode.FullRowSelect

            .AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill

            .RowHeadersVisible = False

            .AllowUserToOrderColumns = False

        End With

    End Sub


    '==========================================================
    ' KUNCI INPUT
    '==========================================================
    Private Sub KunciInput()

        txtNoID.ReadOnly = True
        txtNama.ReadOnly = True
        txtUsername.ReadOnly = True
        txtPassword.ReadOnly = True

        cmbJabatan.Enabled = False

        modeEdit = False

    End Sub


    '==========================================================
    ' BUKA INPUT UNTUK EDIT
    '==========================================================
    Private Sub BukaInput()

        txtNoID.ReadOnly = False
        txtNama.ReadOnly = False
        txtUsername.ReadOnly = False
        txtPassword.ReadOnly = False

        cmbJabatan.Enabled = True

        modeEdit = True

        txtNama.Focus()

    End Sub


    '==========================================================
    ' LOAD DATA AKUN
    '==========================================================
    Private Sub LoadDataAkun()

        Try

            Koneksi()

            Dim query As String =
                "SELECT No_ID, Nama, Username, Password, Jabatan " &
                "FROM Data_User " &
                "ORDER BY No_ID"

            da = New OleDbDataAdapter(
                query,
                CNN
            )

            dt = New DataTable()

            da.Fill(dt)

            dgvAkun.DataSource = dt

            CNN.Close()

            AturKolomDGV()

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menampilkan data akun." &
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

        If dgvAkun.Columns.Count = 0 Then
            Exit Sub
        End If

        If dgvAkun.Columns.Contains("No_ID") Then

            dgvAkun.Columns("No_ID").HeaderText =
                "No ID"

        End If

        If dgvAkun.Columns.Contains("Nama") Then

            dgvAkun.Columns("Nama").HeaderText =
                "Nama"

        End If

        If dgvAkun.Columns.Contains("Username") Then

            dgvAkun.Columns("Username").HeaderText =
                "Username"

        End If

        If dgvAkun.Columns.Contains("Password") Then

            dgvAkun.Columns("Password").HeaderText =
                "Password"

        End If

        If dgvAkun.Columns.Contains("Jabatan") Then

            dgvAkun.Columns("Jabatan").HeaderText =
                "Jabatan"

        End If

    End Sub


    '==========================================================
    ' KLIK DGV
    '==========================================================
    Private Sub dgvAkun_CellClick(
        sender As Object,
        e As DataGridViewCellEventArgs
    ) Handles dgvAkun.CellClick

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        Try

            Dim row As DataGridViewRow =
                dgvAkun.Rows(e.RowIndex)


            '==================================================
            ' NO ID
            '==================================================
            If row.Cells("No_ID").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("No_ID").Value) Then

                txtNoID.Text =
                    row.Cells("No_ID").Value.ToString()

            Else

                txtNoID.Clear()

            End If


            '==================================================
            ' NAMA
            '==================================================
            If row.Cells("Nama").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("Nama").Value) Then

                txtNama.Text =
                    row.Cells("Nama").Value.ToString()

            Else

                txtNama.Clear()

            End If


            '==================================================
            ' USERNAME
            '==================================================
            If row.Cells("Username").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("Username").Value) Then

                txtUsername.Text =
                    row.Cells("Username").Value.ToString()

            Else

                txtUsername.Clear()

            End If


            '==================================================
            ' PASSWORD
            '==================================================
            If row.Cells("Password").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("Password").Value) Then

                txtPassword.Text =
                    row.Cells("Password").Value.ToString()

            Else

                txtPassword.Clear()

            End If


            '==================================================
            ' JABATAN
            '==================================================
            If row.Cells("Jabatan").Value IsNot Nothing AndAlso
               Not IsDBNull(row.Cells("Jabatan").Value) Then

                cmbJabatan.Text =
                    row.Cells("Jabatan").Value.ToString()

            Else

                cmbJabatan.SelectedIndex = -1

            End If


            '==================================================
            ' SETELAH KLIK DATA, INPUT TERKUNCI
            '==================================================
            KunciInput()

        Catch ex As Exception

            MessageBox.Show(
                "Gagal mengambil data akun." &
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

        If txtNoID.Text.Trim() = "" Then

            MessageBox.Show(
                "Pilih data akun yang ingin diedit terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If modeEdit = False Then

            BukaInput()

            MessageBox.Show(
                "Mode edit aktif." &
                vbCrLf &
                "Silakan ubah data yang diperlukan.",
                "Edit Data",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

        Else

            MessageBox.Show(
                "Data sedang dalam mode edit." &
                vbCrLf &
                "Klik Simpan untuk menyimpan perubahan.",
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
        ' CEK MODE EDIT
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
        ' VALIDASI NO ID
        '======================================================
        If txtNoID.Text.Trim() = "" Then

            MessageBox.Show(
                "No ID tidak boleh kosong.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtNoID.Focus()

            Exit Sub

        End If


        '======================================================
        ' VALIDASI NAMA
        '======================================================
        If txtNama.Text.Trim() = "" Then

            MessageBox.Show(
                "Nama tidak boleh kosong.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtNama.Focus()

            Exit Sub

        End If


        '======================================================
        ' VALIDASI USERNAME
        '======================================================
        If txtUsername.Text.Trim() = "" Then

            MessageBox.Show(
                "Username tidak boleh kosong.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtUsername.Focus()

            Exit Sub

        End If


        '======================================================
        ' VALIDASI PASSWORD
        '======================================================
        If txtPassword.Text.Trim() = "" Then

            MessageBox.Show(
                "Password tidak boleh kosong.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            txtPassword.Focus()

            Exit Sub

        End If


        '======================================================
        ' VALIDASI JABATAN
        '======================================================
        If cmbJabatan.SelectedIndex = -1 OrElse
           cmbJabatan.Text.Trim() = "" Then

            MessageBox.Show(
                "Jabatan harus dipilih.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cmbJabatan.Focus()

            Exit Sub

        End If


        '======================================================
        ' KONFIRMASI
        '======================================================
        Dim konfirmasi As DialogResult =
            MessageBox.Show(
                "Apakah Anda yakin ingin menyimpan perubahan data ini?" &
                vbCrLf & vbCrLf &
                "No ID : " & txtNoID.Text &
                vbCrLf &
                "Nama : " & txtNama.Text,
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
            ' UPDATE DATA USER
            '==================================================
            Dim query As String =
                "UPDATE Data_User SET " &
                "Nama = ?, " &
                "Username = ?, " &
                "Password = ?, " &
                "Jabatan = ? " &
                "WHERE No_ID = ?"


            cmd = New OleDbCommand(
                query,
                CNN
            )


            '==================================================
            ' PARAMETER
            '==================================================
            cmd.Parameters.AddWithValue(
                "@Nama",
                txtNama.Text.Trim()
            )

            cmd.Parameters.AddWithValue(
                "@Username",
                txtUsername.Text.Trim()
            )

            cmd.Parameters.AddWithValue(
                "@Password",
                txtPassword.Text.Trim()
            )

            cmd.Parameters.AddWithValue(
                "@Jabatan",
                cmbJabatan.Text.Trim()
            )

            cmd.Parameters.AddWithValue(
                "@No_ID",
                txtNoID.Text.Trim()
            )


            '==================================================
            ' EKSEKUSI
            '==================================================
            Dim jumlahBerubah As Integer =
                cmd.ExecuteNonQuery()


            CNN.Close()


            '==================================================
            ' CEK HASIL
            '==================================================
            If jumlahBerubah > 0 Then

                MessageBox.Show(
                    "Data akun berhasil diperbarui.",
                    "Berhasil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

                modeEdit = False

                KunciInput()

                LoadDataAkun()

                BersihkanInput()

                dgvAkun.ClearSelection()

            Else

                MessageBox.Show(
                    "Data tidak berhasil diperbarui." &
                    vbCrLf & vbCrLf &
                    "No ID tidak ditemukan di database.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

            End If


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menyimpan perubahan data." &
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

        If txtNoID.Text.Trim() = "" Then

            MessageBox.Show(
                "Pilih data akun yang ingin dihapus terlebih dahulu.",
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
                "Apakah Anda yakin ingin menghapus akun berikut?" &
                vbCrLf & vbCrLf &
                "No ID : " & txtNoID.Text &
                vbCrLf &
                "Nama : " & txtNama.Text &
                vbCrLf &
                "Username : " & txtUsername.Text,
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
            ' HAPUS DATA
            '==================================================
            Dim query As String =
                "DELETE FROM Data_User " &
                "WHERE No_ID = ?"


            cmd = New OleDbCommand(
                query,
                CNN
            )


            cmd.Parameters.AddWithValue(
                "@No_ID",
                txtNoID.Text.Trim()
            )


            Dim jumlahDihapus As Integer =
                cmd.ExecuteNonQuery()


            CNN.Close()


            '==================================================
            ' CEK HASIL
            '==================================================
            If jumlahDihapus > 0 Then

                MessageBox.Show(
                    "Data akun berhasil dihapus.",
                    "Berhasil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

                modeEdit = False

                KunciInput()

                BersihkanInput()

                LoadDataAkun()

                dgvAkun.ClearSelection()

            Else

                MessageBox.Show(
                    "Data tidak ditemukan sehingga tidak dapat dihapus.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

            End If


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menghapus data akun." &
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

        BersihkanInput()

        KunciInput()

        txtCari.Clear()

        dgvAkun.ClearSelection()

        LoadDataAkun()

    End Sub


    '==========================================================
    ' BERSIHKAN INPUT
    '==========================================================
    Private Sub BersihkanInput()

        txtNoID.Clear()
        txtNama.Clear()
        txtUsername.Clear()
        txtPassword.Clear()

        cmbJabatan.SelectedIndex = -1

    End Sub


    '==========================================================
    ' LOAD JABATAN
    '==========================================================
    Private Sub LoadJabatan()

        cmbJabatan.Items.Clear()

        cmbJabatan.Items.Add(
            "Production Manager"
        )

        cmbJabatan.Items.Add(
            "Supervisor Quality Assurance"
        )

        cmbJabatan.Items.Add(
            "Leader Quality"
        )

        cmbJabatan.Items.Add(
            "Quality Inspector"
        )

        cmbJabatan.SelectedIndex = -1

        cmbJabatan.DropDownStyle =
            ComboBoxStyle.DropDownList

    End Sub


    '==========================================================
    ' TOMBOL CARI
    '==========================================================
    Private Sub btnCari_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnCari.Click

        CariData()

    End Sub


    '==========================================================
    ' ENTER PADA TEXTBOX CARI
    '==========================================================
    Private Sub txtCari_KeyDown(
        sender As Object,
        e As KeyEventArgs
    ) Handles txtCari.KeyDown

        If e.KeyCode = Keys.Enter Then

            e.SuppressKeyPress = True

            CariData()

        End If

    End Sub


    '==========================================================
    ' CARI DATA
    '==========================================================
    Private Sub CariData()

        Try

            Dim keyword As String =
                txtCari.Text.Trim()


            '==================================================
            ' JIKA KOSONG, TAMPILKAN SEMUA DATA
            '==================================================
            If keyword = "" Then

                LoadDataAkun()

                BersihkanInput()

                KunciInput()

                dgvAkun.ClearSelection()

                Exit Sub

            End If


            Koneksi()


            '==================================================
            ' QUERY PENCARIAN
            '==================================================
            Dim query As String =
                "SELECT No_ID, Nama, Username, Password, Jabatan " &
                "FROM Data_User " &
                "WHERE No_ID LIKE ? " &
                "OR Nama LIKE ? " &
                "OR Username LIKE ? " &
                "OR Jabatan LIKE ? " &
                "ORDER BY No_ID"


            cmd = New OleDbCommand(
                query,
                CNN
            )


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


            da = New OleDbDataAdapter(cmd)

            dt = New DataTable()

            da.Fill(dt)


            dgvAkun.DataSource = dt

            CNN.Close()


            AturKolomDGV()


            '==================================================
            ' KOSONGKAN DETAIL SAAT HASIL PENCARIAN BERUBAH
            '==================================================
            BersihkanInput()

            KunciInput()

            dgvAkun.ClearSelection()


            '==================================================
            ' PESAN JIKA TIDAK ADA DATA
            '==================================================
            If dt.Rows.Count = 0 Then

                MessageBox.Show(
                    "Data akun tidak ditemukan.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            End If


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mencari data akun." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

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

        End Try

    End Sub


End Class