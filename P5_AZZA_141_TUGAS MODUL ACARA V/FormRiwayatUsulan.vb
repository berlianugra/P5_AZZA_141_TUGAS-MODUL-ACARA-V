Imports System.Data.OleDb

Public Class FormRiwayatUsulan


    '========================================================
    ' FORM LOAD
    '========================================================

    Private Sub FormRiwayatUsulan_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        TampilkanDataUsulan()

    End Sub


    '========================================================
    ' QUERY DASAR DATA USULAN
    '========================================================

    Private Function QueryDataUsulan() As String

        Dim query As String =
            "SELECT " &
            "U.[ID_Usulan] AS [ID Usulan], " &
            "U.[Tanggal_Usulan] AS [Tanggal], " &
            "P.[Nama_Produk] AS [Nama Produk], " &
            "TP.[Total_Produksi] AS [Total Produksi], " &
            "NG.[Jenis_NG] AS [Jenis NG], " &
            "NG.[Jumlah_NG] AS [Jumlah NG], " &
            "U.[Usulan_Perbaikan] AS [Usulan] " &
            "FROM ((([Data_Usulan] AS U " &
            "INNER JOIN [Data_Kualitas] AS K " &
            "ON U.[ID_Kualitas] = K.[ID_Kualitas]) " &
            "INNER JOIN [Data_Produk_NG] AS NG " &
            "ON K.[ID_NG] = NG.[ID_NG]) " &
            "INNER JOIN [Data_Pengelolaan_Total_Produksi] AS TP " &
            "ON K.[ID_Total_Produk] = TP.[ID_Total_Produk]) " &
            "INNER JOIN [Produk] AS P " &
            "ON TP.[ID_Produk] = P.[ID_Produk] " &
            "ORDER BY U.[Tanggal_Usulan] DESC"

        Return query

    End Function


    '========================================================
    ' TAMPILKAN SEMUA DATA
    '========================================================

    Private Sub TampilkanDataUsulan()

        Try

            Koneksi()

            Dim query As String =
                QueryDataUsulan()

            Using cmd As New OleDbCommand(
                query,
                CNN
            )

                Using adapter As New OleDbDataAdapter(
                    cmd
                )

                    Dim dtData As New DataTable()

                    adapter.Fill(dtData)

                    dgvUsulan.DataSource =
                        dtData

                End Using

            End Using


            AturDGV()


            CNN.Close()

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menampilkan data usulan." &
                vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' ATUR TAMPILAN DATAGRIDVIEW
    '========================================================

    Private Sub AturDGV()

        If dgvUsulan.Columns.Count = 0 Then
            Exit Sub
        End If


        If dgvUsulan.Columns.Contains(
            "ID Usulan"
        ) Then

            dgvUsulan.Columns(
                "ID Usulan"
            ).HeaderText = "ID Usulan"

        End If


        If dgvUsulan.Columns.Contains(
            "Tanggal"
        ) Then

            dgvUsulan.Columns(
                "Tanggal"
            ).HeaderText = "Tanggal"

        End If


        If dgvUsulan.Columns.Contains(
            "Nama Produk"
        ) Then

            dgvUsulan.Columns(
                "Nama Produk"
            ).HeaderText = "Nama Produk"

        End If


        If dgvUsulan.Columns.Contains(
            "Total Produksi"
        ) Then

            dgvUsulan.Columns(
                "Total Produksi"
            ).HeaderText = "Total Produksi"

        End If


        If dgvUsulan.Columns.Contains(
            "Jenis NG"
        ) Then

            dgvUsulan.Columns(
                "Jenis NG"
            ).HeaderText = "Jenis NG"

        End If


        If dgvUsulan.Columns.Contains(
            "Jumlah NG"
        ) Then

            dgvUsulan.Columns(
                "Jumlah NG"
            ).HeaderText = "Jumlah NG"

        End If


        If dgvUsulan.Columns.Contains(
            "Usulan"
        ) Then

            dgvUsulan.Columns(
                "Usulan"
            ).HeaderText = "Usulan"

        End If


        dgvUsulan.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill

        dgvUsulan.ReadOnly = True

        dgvUsulan.AllowUserToAddRows = False

        dgvUsulan.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect

    End Sub


    '========================================================
    ' CARI DATA
    ' BISA BERDASARKAN SEMUA DATA YANG TAMPIL DI DGV
    '========================================================

    Private Sub btnCariUsulan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnCariUsulan.Click

        Dim kataCari As String =
            txtCariUsulan.Text.Trim()


        If kataCari = "" Then

            TampilkanDataUsulan()

            Exit Sub

        End If


        Try

            Koneksi()


            Dim query As String =
                "SELECT " &
                "U.[ID_Usulan] AS [ID Usulan], " &
                "U.[Tanggal_Usulan] AS [Tanggal], " &
                "P.[Nama_Produk] AS [Nama Produk], " &
                "TP.[Total_Produksi] AS [Total Produksi], " &
                "NG.[Jenis_NG] AS [Jenis NG], " &
                "NG.[Jumlah_NG] AS [Jumlah NG], " &
                "U.[Usulan_Perbaikan] AS [Usulan] " &
                "FROM ((([Data_Usulan] AS U " &
                "INNER JOIN [Data_Kualitas] AS K " &
                "ON U.[ID_Kualitas] = K.[ID_Kualitas]) " &
                "INNER JOIN [Data_Produk_NG] AS NG " &
                "ON K.[ID_NG] = NG.[ID_NG]) " &
                "INNER JOIN [Data_Pengelolaan_Total_Produksi] AS TP " &
                "ON K.[ID_Total_Produk] = TP.[ID_Total_Produk]) " &
                "INNER JOIN [Produk] AS P " &
                "ON TP.[ID_Produk] = P.[ID_Produk] " &
                "WHERE " &
                "U.[ID_Usulan] LIKE ? " &
                "OR P.[Nama_Produk] LIKE ? " &
                "OR NG.[Jenis_NG] LIKE ? " &
                "OR U.[Usulan_Perbaikan] LIKE ? " &
                "OR FORMAT(U.[Tanggal_Usulan], 'dd/mm/yyyy') LIKE ? " &
                "OR CSTR(TP.[Total_Produksi]) LIKE ? " &
                "OR CSTR(NG.[Jumlah_NG]) LIKE ? " &
                "ORDER BY U.[Tanggal_Usulan] DESC"


            Using cmdCari As New OleDbCommand(
                query,
                CNN
            )

                Dim nilaiCari As String =
                    "%" &
                    kataCari &
                    "%"


                cmdCari.Parameters.Add(
                    "@ID_Usulan",
                    OleDbType.VarWChar
                ).Value = nilaiCari


                cmdCari.Parameters.Add(
                    "@Nama_Produk",
                    OleDbType.VarWChar
                ).Value = nilaiCari


                cmdCari.Parameters.Add(
                    "@Jenis_NG",
                    OleDbType.VarWChar
                ).Value = nilaiCari


                cmdCari.Parameters.Add(
                    "@Usulan_Perbaikan",
                    OleDbType.VarWChar
                ).Value = nilaiCari


                cmdCari.Parameters.Add(
                    "@Tanggal",
                    OleDbType.VarWChar
                ).Value = nilaiCari


                cmdCari.Parameters.Add(
                    "@Total_Produksi",
                    OleDbType.VarWChar
                ).Value = nilaiCari


                cmdCari.Parameters.Add(
                    "@Jumlah_NG",
                    OleDbType.VarWChar
                ).Value = nilaiCari


                Using adapter As New OleDbDataAdapter(
                    cmdCari
                )

                    Dim dtHasil As New DataTable()

                    adapter.Fill(dtHasil)


                    If dtHasil.Rows.Count > 0 Then

                        dgvUsulan.DataSource =
                            dtHasil

                        AturDGV()

                    Else

                        dgvUsulan.DataSource =
                            Nothing

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

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mencari data usulan." &
                vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' ENTER DI TEXTBOX = CARI
    '========================================================

    Private Sub txtCariUsulan_KeyDown(
        sender As Object,
        e As KeyEventArgs
    ) Handles txtCariUsulan.KeyDown

        If e.KeyCode = Keys.Enter Then

            btnCariUsulan.PerformClick()

            e.SuppressKeyPress = True

        End If

    End Sub


    '========================================================
    ' TOMBOL KEMBALI
    '========================================================

    Private Sub btnKembaliUsulan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnKembaliUsulan.Click

        FormUsulanPerbaikan.Show()

        Me.Hide()

    End Sub


    '========================================================
    ' TUTUP KONEKSI
    '========================================================

    Private Sub TutupKoneksi()

        Try

            If CNN IsNot Nothing AndAlso
               CNN.State <> ConnectionState.Closed Then

                CNN.Close()

            End If

        Catch

        End Try

    End Sub
    Private Sub lblUsulanPerbaikan_Click(
    sender As Object,
    e As EventArgs
) Handles lblUsulanPerbaikan.Click

        FormUsulanPerbaikan.Show()
        Me.Hide()

    End Sub

    Private Sub btnLogOutRiwUsulan_Click(sender As Object, e As EventArgs) Handles btnLogOutRiwUsulan.Click
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

    Private Sub lblRiwKerugian_Click(sender As Object, e As EventArgs) Handles lblRiwKerugian.Click
        FormKerugian.Show()
    End Sub
End Class