Imports System.Data.OleDb

Public Class FormRiwayatKerugian

    Private Sub FormRiwayatKerugian_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        TampilkanDataKerugian()

    End Sub


    Private Sub TampilkanDataKerugian()

        Try

            Koneksi()

            Dim query As String =
                "SELECT " &
                "K.ID_Kerugian, " &
                "P.Nama_Produk, " &
                "TP.Total_Produksi, " &
                "NG.Jenis_NG, " &
                "NG.Jumlah_NG, " &
                "K.Total_Biaya_Keugian " &
                "FROM " &
                "((Data_Pemrosesan_Kerugian AS K " &
                "INNER JOIN Produk AS P " &
                "ON K.ID_Produk = P.ID_Produk) " &
                "INNER JOIN Data_Produk_NG AS NG " &
                "ON K.ID_NG = NG.ID_NG) " &
                "INNER JOIN Data_Pengelolaan_Total_Produksi AS TP " &
                "ON NG.ID_Total_Produk = TP.ID_Total_Produk " &
                "ORDER BY K.ID_Kerugian"

            Using cmdKerugian As New OleDbCommand(
                query,
                CNN
            )

                Using adapter As New OleDbDataAdapter(
                    cmdKerugian
                )

                    Dim dtData As New DataTable()

                    adapter.Fill(dtData)

                    dgvKerugian.DataSource = dtData

                End Using

            End Using

            AturTampilanDataGridView()

            CNN.Close()

        Catch ex As Exception

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If

            MessageBox.Show(
                "Gagal menampilkan data kerugian." &
                vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    Private Sub AturTampilanDataGridView()

        If dgvKerugian.Columns.Count = 0 Then
            Exit Sub
        End If

        dgvKerugian.Columns("ID_Kerugian").HeaderText =
            "ID Kerugian"

        dgvKerugian.Columns("Nama_Produk").HeaderText =
            "Nama Produk"

        dgvKerugian.Columns("Total_Produksi").HeaderText =
            "Total Produksi"

        dgvKerugian.Columns("Jenis_NG").HeaderText =
            "Jenis NG"

        dgvKerugian.Columns("Jumlah_NG").HeaderText =
            "Jumlah NG"

        dgvKerugian.Columns("Total_Biaya_Keugian").HeaderText =
            "Total Biaya Kerugian"

        dgvKerugian.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill

        dgvKerugian.ReadOnly = True

        dgvKerugian.AllowUserToAddRows = False

        dgvKerugian.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect

    End Sub


    Private Sub btnCariKerugian_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnCariKerugian.Click

        Dim kataKunci As String =
            txtCariKerugian.Text.Trim()

        If kataKunci = "" Then

            TampilkanDataKerugian()

            Exit Sub

        End If


        Try

            Koneksi()

            Dim query As String =
                "SELECT " &
                "K.ID_Kerugian, " &
                "P.Nama_Produk, " &
                "TP.Total_Produksi, " &
                "NG.Jenis_NG, " &
                "NG.Jumlah_NG, " &
                "K.Total_Biaya_Keugian " &
                "FROM " &
                "((Data_Pemrosesan_Kerugian AS K " &
                "INNER JOIN Produk AS P " &
                "ON K.ID_Produk = P.ID_Produk) " &
                "INNER JOIN Data_Produk_NG AS NG " &
                "ON K.ID_NG = NG.ID_NG) " &
                "INNER JOIN Data_Pengelolaan_Total_Produksi AS TP " &
                "ON NG.ID_Total_Produk = TP.ID_Total_Produk " &
                "WHERE " &
                "K.ID_Kerugian LIKE ? " &
                "OR P.Nama_Produk LIKE ? " &
                "OR CStr(TP.Total_Produksi) LIKE ? " &
                "OR NG.Jenis_NG LIKE ? " &
                "OR CStr(NG.Jumlah_NG) LIKE ? " &
                "OR CStr(K.Total_Biaya_Keugian) LIKE ? " &
                "ORDER BY K.ID_Kerugian"


            Using cmdCari As New OleDbCommand(
                query,
                CNN
            )

                Dim polaCari As String =
                    "%" & kataKunci & "%"

                cmdCari.Parameters.AddWithValue(
                    "@ID_Kerugian",
                    polaCari
                )

                cmdCari.Parameters.AddWithValue(
                    "@Nama_Produk",
                    polaCari
                )

                cmdCari.Parameters.AddWithValue(
                    "@Total_Produksi",
                    polaCari
                )

                cmdCari.Parameters.AddWithValue(
                    "@Jenis_NG",
                    polaCari
                )

                cmdCari.Parameters.AddWithValue(
                    "@Jumlah_NG",
                    polaCari
                )

                cmdCari.Parameters.AddWithValue(
                    "@Total_Biaya_Keugian",
                    polaCari
                )


                Using adapter As New OleDbDataAdapter(
                    cmdCari
                )

                    Dim dtHasil As New DataTable()

                    adapter.Fill(dtHasil)

                    If dtHasil.Rows.Count > 0 Then

                        dgvKerugian.DataSource = dtHasil

                        AturTampilanDataGridView()

                    Else

                        dgvKerugian.DataSource = Nothing

                        MessageBox.Show(
                            "Data yang dicari tidak ditemukan.",
                            "Informasi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        )

                    End If

                End Using

            End Using

            CNN.Close()

        Catch ex As Exception

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If

            MessageBox.Show(
                "Gagal mencari data kerugian." &
                vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    Private Sub btnKembaliKerugian_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnKembaliKerugian.Click

        FormKerugian.Show()

        Me.Hide()

    End Sub

    Private Sub btnLogOutRiwKerugian_Click(sender As Object, e As EventArgs) Handles btnLogOutRiwKerugian.Click
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
    Private Sub lblRiwUsulanPerbaikan_Click(sender As Object, e As EventArgs) Handles lblRiwUsulanPerbaikan.Click

        FormUsulanPerbaikan.Show()
        Me.Hide()

    End Sub
End Class