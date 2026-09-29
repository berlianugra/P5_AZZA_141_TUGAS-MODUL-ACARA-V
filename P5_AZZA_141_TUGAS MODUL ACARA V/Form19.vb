Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Text

Public Class FormLaporanQC

    '========================================================
    ' VARIABEL
    '========================================================

    Private dtLaporan As New DataTable()

    Private Const PLACEHOLDER_TEXT As String = "Cari riwayat data"

    Private sedangPlaceholder As Boolean = False

    Private siapInputBaru As Boolean = False


    '========================================================
    ' FORM LOAD
    '========================================================

    Private Sub FormLaporanQC_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        '-----------------------------------------------
        ' TEXTBOX
        '-----------------------------------------------

        txtRiwayatData.ReadOnly = False
        txtRiwayatData.Enabled = True

        PasangPlaceholder()


        '-----------------------------------------------
        ' DATAGRIDVIEW
        '-----------------------------------------------

        AturDataGridView()


        '-----------------------------------------------
        ' CHECKBOX PILIH SEMUA
        '-----------------------------------------------

        cbPilihData.Checked = False


        '-----------------------------------------------
        ' TANGGAL
        '-----------------------------------------------

        dtpTanggal.Format =
            DateTimePickerFormat.Custom

        dtpTanggal.CustomFormat =
            "dddd  dd MMMM yyyy"


        '-----------------------------------------------
        ' TAMPILKAN DATA
        '-----------------------------------------------

        TampilkanSemuaData()

    End Sub


    '========================================================
    ' PLACEHOLDER
    '========================================================

    Private Sub PasangPlaceholder()

        sedangPlaceholder = True
        siapInputBaru = False

        txtRiwayatData.Text =
            PLACEHOLDER_TEXT

        txtRiwayatData.ForeColor =
            Color.Gray

    End Sub


    '========================================================
    ' TEXTBOX ENTER
    '========================================================

    Private Sub txtRiwayatData_Enter(
        sender As Object,
        e As EventArgs
    ) Handles txtRiwayatData.Enter

        If sedangPlaceholder Then

            txtRiwayatData.Clear()

            txtRiwayatData.ForeColor =
                Color.Black

            sedangPlaceholder = False

            siapInputBaru = False

            Exit Sub

        End If


        If siapInputBaru Then

            txtRiwayatData.Clear()

            txtRiwayatData.ForeColor =
                Color.Black

            siapInputBaru = False

        End If

    End Sub


    '========================================================
    ' TEXTBOX LEAVE
    '========================================================

    Private Sub txtRiwayatData_Leave(
        sender As Object,
        e As EventArgs
    ) Handles txtRiwayatData.Leave

        If String.IsNullOrWhiteSpace(
            txtRiwayatData.Text
        ) Then

            PasangPlaceholder()

        End If

    End Sub


    '========================================================
    ' ENTER UNTUK SEARCH
    '========================================================

    Private Sub txtRiwayatData_KeyDown(
        sender As Object,
        e As KeyEventArgs
    ) Handles txtRiwayatData.KeyDown

        If e.KeyCode = Keys.Enter Then

            e.SuppressKeyPress = True
            e.Handled = True


            If sedangPlaceholder Then
                Exit Sub
            End If


            If String.IsNullOrWhiteSpace(
                txtRiwayatData.Text
            ) Then

                Exit Sub

            End If


            btnCari.PerformClick()

        End If

    End Sub


    '========================================================
    ' ATUR DATAGRIDVIEW
    '========================================================

    Private Sub AturDataGridView()

        dgvRiwayat.AutoGenerateColumns = False

        dgvRiwayat.AllowUserToAddRows = False

        dgvRiwayat.AllowUserToDeleteRows = False

        dgvRiwayat.AllowUserToResizeRows = False

        dgvRiwayat.RowHeadersVisible = False

        dgvRiwayat.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect

        dgvRiwayat.MultiSelect = False

        dgvRiwayat.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.None

        dgvRiwayat.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.None

        dgvRiwayat.ScrollBars =
            ScrollBars.Both

        dgvRiwayat.Columns.Clear()


        '====================================================
        ' CHECKBOX
        '====================================================

        Dim colPilih As New DataGridViewCheckBoxColumn()

        colPilih.Name = "Pilih"

        colPilih.HeaderText = ""

        colPilih.Width = 40

        colPilih.ReadOnly = False

        colPilih.DataPropertyName = ""

        dgvRiwayat.Columns.Add(
            colPilih
        )


        '====================================================
        ' KOLOM DATABASE
        '====================================================

        TambahKolom(
            "ID_Laporan",
            "ID Laporan",
            90
        )


        TambahKolom(
            "No_ID",
            "No ID",
            70
        )


        TambahKolom(
            "Tanggal_Laporan",
            "Tanggal Laporan",
            110
        )


        TambahKolom(
            "ID_Total_Produk",
            "ID Total Produk",
            115
        )


        TambahKolom(
            "ID_NG",
            "ID NG",
            80
        )


        TambahKolom(
            "ID_Kualitas",
            "ID Kualitas",
            90
        )


        TambahKolom(
            "ID_Kerugian",
            "ID Kerugian",
            90
        )


        TambahKolom(
            "ID_Usulan",
            "ID Usulan",
            90
        )


        '====================================================
        ' TAMPILAN
        '====================================================

        dgvRiwayat.RowTemplate.Height = 25

        dgvRiwayat.ColumnHeadersHeight = 30

        dgvRiwayat.EnableHeadersVisualStyles = False

        dgvRiwayat.ColumnHeadersDefaultCellStyle.Font =
            New Font(
                "Segoe UI",
                9,
                FontStyle.Bold
            )

        dgvRiwayat.DefaultCellStyle.Font =
            New Font(
                "Segoe UI",
                9,
                FontStyle.Regular
            )

    End Sub


    '========================================================
    ' TAMBAH KOLOM
    '========================================================

    Private Sub TambahKolom(
        nama As String,
        header As String,
        lebar As Integer
    )

        Dim kolom As New DataGridViewTextBoxColumn()

        kolom.Name = nama

        kolom.HeaderText = header

        kolom.DataPropertyName = nama

        kolom.Width = lebar

        kolom.ReadOnly = True

        dgvRiwayat.Columns.Add(
            kolom
        )

    End Sub


    '========================================================
    ' TAMPILKAN SEMUA DATA
    '========================================================

    Private Sub TampilkanSemuaData()

        Try

            Koneksi()


            Dim query As String =
                "SELECT " &
                "[ID_Laporan], " &
                "[No_ID], " &
                "[Tanggal_Laporan], " &
                "[ID_Total_Produk], " &
                "[ID_NG], " &
                "[ID_Kualitas], " &
                "[ID_Kerugian], " &
                "[ID_Usulan] " &
                "FROM [Laporan_QC] " &
                "ORDER BY [Tanggal_Laporan] ASC"


            dtLaporan.Clear()


            Using cmd As New OleDbCommand(
                query,
                CNN
            )

                Using adapter As New OleDbDataAdapter(
                    cmd
                )

                    adapter.Fill(
                        dtLaporan
                    )

                End Using

            End Using


            TampilkanDataKeDGV(
                dtLaporan
            )


        Catch ex As Exception

            MessageBox.Show(
                "Gagal mengambil data laporan." &
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
    ' TAMPILKAN DATATABLE KE DGV
    '========================================================

    Private Sub TampilkanDataKeDGV(
        tabel As DataTable
    )

        dgvRiwayat.DataSource = Nothing

        dgvRiwayat.DataSource = tabel


        '====================================================
        ' CHECKBOX DI RESET
        '====================================================

        For Each row As DataGridViewRow In dgvRiwayat.Rows

            If Not row.IsNewRow Then

                row.Cells("Pilih").Value = False

            End If

        Next


        cbPilihData.Checked = False

    End Sub


    '========================================================
    ' BUTTON CARI
    '========================================================

    Private Sub btnCari_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnCari.Click

        If sedangPlaceholder Then

            TampilkanDataKeDGV(
                dtLaporan
            )

            Exit Sub

        End If


        Dim keyword As String =
            txtRiwayatData.Text.Trim()


        If keyword = "" Then

            TampilkanDataKeDGV(
                dtLaporan
            )

            Exit Sub

        End If


        Try

            Dim view As New DataView(
                dtLaporan
            )


            Dim kata As String =
                keyword.Replace(
                    "'",
                    "''"
                )


            view.RowFilter =
                "[ID_Laporan] LIKE '%" &
                kata &
                "%' OR " &
                "[No_ID] LIKE '%" &
                kata &
                "%' OR " &
                "[ID_Total_Produk] LIKE '%" &
                kata &
                "%' OR " &
                "[ID_NG] LIKE '%" &
                kata &
                "%' OR " &
                "[ID_Kualitas] LIKE '%" &
                kata &
                "%' OR " &
                "[ID_Kerugian] LIKE '%" &
                kata &
                "%' OR " &
                "[ID_Usulan] LIKE '%" &
                kata &
                "%'"


            dgvRiwayat.DataSource = Nothing

            dgvRiwayat.DataSource = view


            siapInputBaru = True

            txtRiwayatData.ForeColor =
                Color.Black

            cbPilihData.Checked = False


        Catch ex As Exception

            MessageBox.Show(
                "Gagal mencari data." &
                vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' CHECKBOX PILIH SEMUA
    '========================================================

    Private Sub cbPilihData_CheckedChanged(
        sender As Object,
        e As EventArgs
    ) Handles cbPilihData.CheckedChanged

        For Each row As DataGridViewRow In dgvRiwayat.Rows

            If Not row.IsNewRow Then

                row.Cells(
                    "Pilih"
                ).Value =
                    cbPilihData.Checked

            End If

        Next

        dgvRiwayat.Refresh()

    End Sub


    '========================================================
    ' BUTTON HAPUS
    '========================================================

    Private Sub btnHapus_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnHapus.Click

        Dim adaData As Boolean = False


        '====================================================
        ' CEK DATA
        '====================================================

        For Each row As DataGridViewRow In dgvRiwayat.Rows

            If Not row.IsNewRow Then

                If row.Cells("Pilih").Value IsNot Nothing Then

                    If Convert.ToBoolean(
                        row.Cells("Pilih").Value
                    ) Then

                        adaData = True

                        Exit For

                    End If

                End If

            End If

        Next


        If Not adaData Then

            MessageBox.Show(
                "Pilih data yang ingin dihapus terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        '====================================================
        ' KONFIRMASI
        '====================================================

        Dim jawaban As DialogResult =
            MessageBox.Show(
                "Yakin ingin menghapus data yang dipilih?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )


        If jawaban =
            DialogResult.No Then

            Exit Sub

        End If


        '====================================================
        ' HAPUS
        '====================================================

        Try

            Koneksi()


            Dim daftarID As New List(Of String)


            For Each row As DataGridViewRow In dgvRiwayat.Rows

                If Not row.IsNewRow Then

                    If row.Cells("Pilih").Value IsNot Nothing Then

                        If Convert.ToBoolean(
                            row.Cells("Pilih").Value
                        ) Then

                            Dim idLaporan As String =
                                AmbilNilaiDGV(
                                    row,
                                    "ID_Laporan"
                                )


                            If idLaporan <> "" Then

                                daftarID.Add(
                                    idLaporan
                                )

                            End If

                        End If

                    End If

                End If

            Next


            '================================================
            ' DELETE SATU-SATU
            '================================================

            For Each idLaporan As String In daftarID

                Dim queryDelete As String =
                    "DELETE FROM [Laporan_QC] " &
                    "WHERE [ID_Laporan] = ?"


                Using cmdDelete As New OleDbCommand(
                    queryDelete,
                    CNN
                )

                    cmdDelete.Parameters.AddWithValue(
                        "@p1",
                        idLaporan
                    )


                    cmdDelete.ExecuteNonQuery()

                End Using

            Next


            MessageBox.Show(
                "Data berhasil dihapus.",
                "Berhasil",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )


            '================================================
            ' REFRESH
            '================================================

            TampilkanSemuaData()


        Catch ex As Exception

            MessageBox.Show(
                "Gagal menghapus data." &
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
    ' AMBIL NILAI DGV
    '========================================================

    Private Function AmbilNilaiDGV(
        row As DataGridViewRow,
        namaKolom As String
    ) As String

        If row.Cells(namaKolom).Value Is Nothing Then

            Return ""

        End If


        If IsDBNull(
            row.Cells(namaKolom).Value
        ) Then

            Return ""

        End If


        Return row.Cells(
            namaKolom
        ).Value.ToString()

    End Function


    '========================================================
    ' HITUNG DATA DIPILIH
    '========================================================

    Private Function HitungDataDipilih() As Integer

        Dim jumlah As Integer = 0


        For Each row As DataGridViewRow In dgvRiwayat.Rows

            If Not row.IsNewRow Then

                If row.Cells("Pilih").Value IsNot Nothing Then

                    If Convert.ToBoolean(
                        row.Cells("Pilih").Value
                    ) Then

                        jumlah += 1

                    End If

                End If

            End If

        Next


        Return jumlah

    End Function


    '========================================================
    ' BUTTON BUAT LAPORAN
    '========================================================

    Private Sub btnBuat_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnBuat.Click

        '====================================================
        ' CEK DATA
        '====================================================

        Dim jumlahDipilih As Integer =
            HitungDataDipilih()


        If jumlahDipilih = 0 Then

            MessageBox.Show(
                "Pilih data yang ingin dibuat menjadi laporan terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        '====================================================
        ' SAVE DIALOG
        '====================================================

        Dim saveDialog As New SaveFileDialog()


        saveDialog.Title =
            "Simpan Laporan Quality Control"


        saveDialog.Filter =
            "PDF Files (*.pdf)|*.pdf"


        saveDialog.DefaultExt =
            "pdf"


        saveDialog.AddExtension =
            True


        saveDialog.FileName =
            "Laporan_QC_" &
            dtpTanggal.Value.ToString(
                "dd-MM-yyyy"
            ) &
            ".pdf"


        If saveDialog.ShowDialog() =
            DialogResult.Cancel Then

            Exit Sub

        End If


        '====================================================
        ' BUAT PDF
        '====================================================

        Try

            BuatPDFNative(
                saveDialog.FileName
            )


            MessageBox.Show(
                "Laporan PDF berhasil dibuat." &
                vbCrLf &
                vbCrLf &
                "Jumlah data: " &
                jumlahDipilih.ToString() &
                " data.",
                "Berhasil",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )


        Catch ex As Exception

            MessageBox.Show(
                "Gagal membuat laporan PDF." &
                vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' PDF NATIVE
    '========================================================

    Private Sub BuatPDFNative(
        namaFile As String
    )

        Dim pages As New List(Of String)


        Dim content As New StringBuilder()


        Dim xKiri As Integer = 35

        Dim y As Integer = 550


        '====================================================
        ' JUDUL
        '====================================================

        TambahText(
            content,
            "LAPORAN QUALITY CONTROL",
            xKiri,
            y,
            18
        )


        y -= 24


        TambahText(
            content,
            "Laporan Data Quality Control",
            xKiri,
            y,
            10
        )


        y -= 12


        TambahGaris(
            content,
            35,
            y,
            807,
            y
        )


        y -= 25


        '====================================================
        ' INFORMASI
        '====================================================

        TambahText(
            content,
            "Tanggal Laporan : " &
            dtpTanggal.Value.ToString(
                "dd/MM/yyyy"
            ),
            xKiri,
            y,
            10
        )


        y -= 16


        TambahText(
            content,
            "Jumlah Data     : " &
            HitungDataDipilih().ToString() &
            " data",
            xKiri,
            y,
            10
        )


        y -= 16


        TambahText(
            content,
            "Tanggal Dibuat  : " &
            DateTime.Now.ToString(
                "dd/MM/yyyy HH:mm"
            ),
            xKiri,
            y,
            10
        )


        y -= 30


        '====================================================
        ' HEADER TABEL
        '====================================================

        BuatHeaderTabel(
            content,
            y
        )


        y -= 25


        Dim nomor As Integer = 0


        '====================================================
        ' DATA TERPILIH
        '====================================================

        For Each row As DataGridViewRow In dgvRiwayat.Rows

            If Not row.IsNewRow Then

                Dim dipilih As Boolean = False


                If row.Cells("Pilih").Value IsNot Nothing Then

                    dipilih =
                        Convert.ToBoolean(
                            row.Cells("Pilih").Value
                        )

                End If


                If dipilih Then

                    '========================================
                    ' JIKA HALAMAN PENUH
                    '========================================

                    If y < 65 Then

                        TambahFooter(
                            content
                        )


                        pages.Add(
                            content.ToString()
                        )


                        content =
                            New StringBuilder()


                        y = 550


                        TambahText(
                            content,
                            "LAPORAN QUALITY CONTROL",
                            xKiri,
                            y,
                            14
                        )


                        y -= 25


                        TambahGaris(
                            content,
                            35,
                            y,
                            807,
                            y
                        )


                        y -= 25


                        BuatHeaderTabel(
                            content,
                            y
                        )


                        y -= 25

                    End If


                    nomor += 1


                    Dim idLaporan As String =
                        AmbilNilaiPDF(
                            row,
                            "ID_Laporan"
                        )


                    Dim noID As String =
                        AmbilNilaiPDF(
                            row,
                            "No_ID"
                        )


                    Dim tanggal As String =
                        AmbilTanggalPDF(
                            row,
                            "Tanggal_Laporan"
                        )


                    Dim idTotalProduk As String =
                        AmbilNilaiPDF(
                            row,
                            "ID_Total_Produk"
                        )


                    Dim idNG As String =
                        AmbilNilaiPDF(
                            row,
                            "ID_NG"
                        )


                    Dim idKualitas As String =
                        AmbilNilaiPDF(
                            row,
                            "ID_Kualitas"
                        )


                    Dim idKerugian As String =
                        AmbilNilaiPDF(
                            row,
                            "ID_Kerugian"
                        )


                    Dim idUsulan As String =
                        AmbilNilaiPDF(
                            row,
                            "ID_Usulan"
                        )


                    '========================================
                    ' TULIS KE PDF
                    '========================================

                    TambahText(
                        content,
                        nomor.ToString(),
                        38,
                        y,
                        7
                    )


                    TambahText(
                        content,
                        PotongText(
                            idLaporan,
                            12
                        ),
                        65,
                        y,
                        7
                    )


                    TambahText(
                        content,
                        PotongText(
                            noID,
                            10
                        ),
                        125,
                        y,
                        7
                    )


                    TambahText(
                        content,
                        PotongText(
                            tanggal,
                            12
                        ),
                        175,
                        y,
                        7
                    )


                    TambahText(
                        content,
                        PotongText(
                            idTotalProduk,
                            17
                        ),
                        245,
                        y,
                        7
                    )


                    TambahText(
                        content,
                        PotongText(
                            idNG,
                            12
                        ),
                        335,
                        y,
                        7
                    )


                    TambahText(
                        content,
                        PotongText(
                            idKualitas,
                            13
                        ),
                        405,
                        y,
                        7
                    )


                    TambahText(
                        content,
                        PotongText(
                            idKerugian,
                            13
                        ),
                        485,
                        y,
                        7
                    )


                    TambahText(
                        content,
                        PotongText(
                            idUsulan,
                            13
                        ),
                        565,
                        y,
                        7
                    )


                    TambahGaris(
                        content,
                        35,
                        y - 6,
                        807,
                        y - 6
                    )


                    y -= 20

                End If

            End If

        Next


        '====================================================
        ' FOOTER
        '====================================================

        TambahFooter(
            content
        )


        pages.Add(
            content.ToString()
        )


        '====================================================
        ' BANGUN FILE
        '====================================================

        BuatFilePDF(
            namaFile,
            pages
        )

    End Sub


    '========================================================
    ' HEADER TABEL PDF
    '========================================================

    Private Sub BuatHeaderTabel(
        content As StringBuilder,
        y As Integer
    )

        TambahText(
            content,
            "No",
            38,
            y,
            8
        )


        TambahText(
            content,
            "ID Laporan",
            65,
            y,
            8
        )


        TambahText(
            content,
            "No ID",
            125,
            y,
            8
        )


        TambahText(
            content,
            "Tanggal",
            175,
            y,
            8
        )


        TambahText(
            content,
            "ID Total Produk",
            245,
            y,
            8
        )


        TambahText(
            content,
            "ID NG",
            335,
            y,
            8
        )


        TambahText(
            content,
            "ID Kualitas",
            405,
            y,
            8
        )


        TambahText(
            content,
            "ID Kerugian",
            485,
            y,
            8
        )


        TambahText(
            content,
            "ID Usulan",
            565,
            y,
            8
        )


        TambahGaris(
            content,
            35,
            y - 7,
            807,
            y - 7
        )

    End Sub


    '========================================================
    ' TAMBAH TEXT PDF
    '========================================================

    Private Sub TambahText(
        content As StringBuilder,
        teks As String,
        x As Integer,
        y As Integer,
        ukuran As Integer
    )

        teks =
            EscapePDFText(
                teks
            )


        content.AppendLine(
            "BT"
        )


        content.AppendLine(
            "/F1 " &
            ukuran.ToString() &
            " Tf"
        )


        content.AppendLine(
            x.ToString() &
            " " &
            y.ToString() &
            " Td"
        )


        content.AppendLine(
            "(" &
            teks &
            ") Tj"
        )


        content.AppendLine(
            "ET"
        )

    End Sub


    '========================================================
    ' TAMBAH GARIS
    '========================================================

    Private Sub TambahGaris(
        content As StringBuilder,
        x1 As Integer,
        y1 As Integer,
        x2 As Integer,
        y2 As Integer
    )

        content.AppendLine(
            x1.ToString() &
            " " &
            y1.ToString() &
            " m"
        )


        content.AppendLine(
            x2.ToString() &
            " " &
            y2.ToString() &
            " l"
        )


        content.AppendLine(
            "S"
        )

    End Sub


    '========================================================
    ' FOOTER PDF
    '========================================================

    Private Sub TambahFooter(
        content As StringBuilder
    )

        TambahGaris(
            content,
            35,
            40,
            807,
            40
        )


        TambahText(
            content,
            "Sistem Informasi Quality Control",
            35,
            25,
            7
        )


        TambahText(
            content,
            "Laporan Quality Control",
            650,
            25,
            7
        )

    End Sub


    '========================================================
    ' ESCAPE PDF
    '========================================================

    Private Function EscapePDFText(
        teks As String
    ) As String

        If teks Is Nothing Then

            Return ""

        End If


        teks =
            teks.Replace(
                "\",
                "\\"
            )


        teks =
            teks.Replace(
                "(",
                "\("
            )


        teks =
            teks.Replace(
                ")",
                "\)"
            )


        Return teks

    End Function


    '========================================================
    ' POTONG TEXT
    '========================================================

    Private Function PotongText(
        teks As String,
        panjang As Integer
    ) As String

        If teks Is Nothing Then

            Return ""

        End If


        If teks.Length <= panjang Then

            Return teks

        End If


        Return teks.Substring(
            0,
            panjang - 2
        ) &
        ".."

    End Function


    '========================================================
    ' AMBIL NILAI PDF
    '========================================================

    Private Function AmbilNilaiPDF(
        row As DataGridViewRow,
        namaKolom As String
    ) As String

        If row.Cells(namaKolom).Value Is Nothing Then

            Return ""

        End If


        If IsDBNull(
            row.Cells(namaKolom).Value
        ) Then

            Return ""

        End If


        Return row.Cells(
            namaKolom
        ).Value.ToString()

    End Function


    '========================================================
    ' AMBIL TANGGAL PDF
    '========================================================

    Private Function AmbilTanggalPDF(
        row As DataGridViewRow,
        namaKolom As String
    ) As String

        If row.Cells(namaKolom).Value Is Nothing Then

            Return ""

        End If


        If IsDBNull(
            row.Cells(namaKolom).Value
        ) Then

            Return ""

        End If


        Dim tanggal As DateTime


        If DateTime.TryParse(
            row.Cells(namaKolom).Value.ToString(),
            tanggal
        ) Then

            Return tanggal.ToString(
                "dd/MM/yyyy"
            )

        End If


        Return row.Cells(
            namaKolom
        ).Value.ToString()

    End Function


    '========================================================
    ' BUAT FILE PDF NATIVE
    '========================================================

    Private Sub BuatFilePDF(
        namaFile As String,
        pages As List(Of String)
    )

        Dim objects As New List(Of String)


        '====================================================
        ' OBJECT 1
        ' CATALOG
        '====================================================

        objects.Add(
            "<< /Type /Catalog /Pages 2 0 R >>"
        )


        '====================================================
        ' OBJECT 2
        ' PAGES
        '====================================================

        Dim kids As New StringBuilder()

        kids.Append(
            "["
        )


        For i As Integer = 0 To pages.Count - 1

            Dim pageObject As Integer =
                4 + (i * 2)


            kids.Append(
                pageObject.ToString() &
                " 0 R "
            )

        Next


        kids.Append(
            "]"
        )


        objects.Add(
            "<< /Type /Pages " &
            "/Kids " &
            kids.ToString() &
            " " &
            "/Count " &
            pages.Count.ToString() &
            " >>"
        )


        '====================================================
        ' OBJECT 3
        ' FONT
        '====================================================

        objects.Add(
            "<< /Type /Font " &
            "/Subtype /Type1 " &
            "/BaseFont /Helvetica >>"
        )


        '====================================================
        ' PAGE OBJECT
        '====================================================

        For i As Integer = 0 To pages.Count - 1

            Dim pageObject As Integer =
                4 + (i * 2)


            Dim contentObject As Integer =
                pageObject + 1


            Dim pageText As String =
                "<< /Type /Page " &
                "/Parent 2 0 R " &
                "/MediaBox [0 0 842 595] " &
                "/Resources << " &
                "/Font << /F1 3 0 R >> " &
                ">> " &
                "/Contents " &
                contentObject.ToString() &
                " 0 R >>"


            objects.Add(
                pageText
            )


            '================================================
            ' CONTENT
            '================================================

            Dim isi As String =
                pages(i)


            Dim panjangIsi As Integer =
                Encoding.ASCII.GetByteCount(
                    isi
                )


            Dim streamText As String =
                "<< /Length " &
                panjangIsi.ToString() &
                " >>" &
                vbLf &
                "stream" &
                vbLf &
                isi &
                vbLf &
                "endstream"


            objects.Add(
                streamText
            )

        Next


        '====================================================
        ' TULIS FILE
        '====================================================

        Using fs As New FileStream(
            namaFile,
            FileMode.Create,
            FileAccess.Write
        )

            Using writer As New StreamWriter(
                fs,
                Encoding.ASCII
            )

                writer.NewLine =
                    vbLf


                '============================================
                ' PDF HEADER
                '============================================

                writer.WriteLine(
                    "%PDF-1.4"
                )


                writer.WriteLine(
                    "%PDFNative"
                )


                '============================================
                ' OFFSET
                '============================================

                Dim offsets As New List(Of Long)


                offsets.Add(
                    0
                )


                '============================================
                ' OBJECT
                '============================================

                For i As Integer = 0 To objects.Count - 1

                    writer.Flush()


                    offsets.Add(
                        fs.Position
                    )


                    writer.WriteLine(
                        (i + 1).ToString() &
                        " 0 obj"
                    )


                    writer.WriteLine(
                        objects(i)
                    )


                    writer.WriteLine(
                        "endobj"
                    )

                Next


                writer.Flush()


                '============================================
                ' XREF
                '============================================

                Dim posisiXref As Long =
                    fs.Position


                writer.WriteLine(
                    "xref"
                )


                writer.WriteLine(
                    "0 " &
                    (objects.Count + 1).ToString()
                )


                writer.WriteLine(
                    "0000000000 65535 f "
                )


                For i As Integer = 1 To offsets.Count - 1

                    writer.WriteLine(
                        offsets(i).ToString(
                            "0000000000"
                        ) &
                        " 00000 n "
                    )

                Next


                '============================================
                ' TRAILER
                '============================================

                writer.WriteLine(
                    "trailer"
                )


                writer.WriteLine(
                    "<< /Size " &
                    (objects.Count + 1).ToString() &
                    " /Root 1 0 R >>"
                )


                writer.WriteLine(
                    "startxref"
                )


                writer.WriteLine(
                    posisiXref.ToString()
                )


                writer.WriteLine(
                    "%%EOF"
                )


            End Using

        End Using

    End Sub


    '========================================================
    ' TUTUP KONEKSI
    '========================================================

    Private Sub TutupKoneksi()

        If CNN IsNot Nothing Then

            If CNN.State =
                ConnectionState.Open Then

                CNN.Close()

            End If

        End If

    End Sub

End Class