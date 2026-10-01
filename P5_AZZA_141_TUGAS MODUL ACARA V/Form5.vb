Imports System.Data.OleDb
Imports System.Windows.Forms.DataVisualization.Charting

Public Class FormRiwayatKualitas

    '==========================================================
    ' FORM LOAD
    '==========================================================
    Private Sub FormRiwayatKualitas_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterScreen

        AturDataGridView()
        AturDateTimePicker()
        LoadDataKualitas()
        LoadJenisNG()
        AturChart()

    End Sub


    '==========================================================
    ' DATETIMEPICKER
    '==========================================================
    Private Sub AturDateTimePicker()

        dtpTanggal.Format = DateTimePickerFormat.Custom
        dtpTanggal.CustomFormat = "dd/MM/yyyy"

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd/MM/yyyy"

    End Sub


    '==========================================================
    ' DATAGRIDVIEW
    '==========================================================
    Private Sub AturDataGridView()

        With dgvRiwayatKualitas

            .AutoGenerateColumns = True
            .ReadOnly = True

            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False

            .MultiSelect = False

            .SelectionMode =
                DataGridViewSelectionMode.FullRowSelect

            .ScrollBars = ScrollBars.Both

            .AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None

            .AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None

            .ColumnHeadersHeight = 45

            .ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            .ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter

            .ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.True

            .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter

            .DefaultCellStyle.WrapMode =
                DataGridViewTriState.False

            .RowTemplate.Height = 35

        End With

    End Sub


    '==========================================================
    ' LOAD SEMUA DATA KE DGV
    '==========================================================
    Private Sub LoadDataKualitas()

        Try

            Koneksi()

            Dim query As String =
                "SELECT " &
                "K.ID_Kualitas, " &
                "K.No_ID, " &
                "P.Nama_Produk, " &
                "K.Level_Sigma, " &
                "NG.Jenis_NG, " &
                "NG.Jumlah_NG, " &
                "TP.ID_Total_Produk, " &
                "TP.ID_Produk, " &
                "TP.Total_Produksi, " &
                "TP.Tanggal_Produksi " &
                "FROM ((Data_Kualitas AS K " &
                "INNER JOIN Data_Produk_NG AS NG " &
                "ON K.ID_NG = NG.ID_NG) " &
                "INNER JOIN Data_Pengelolaan_Total_Produksi AS TP " &
                "ON K.ID_Total_Produk = TP.ID_Total_Produk) " &
                "INNER JOIN Produk AS P " &
                "ON TP.ID_Produk = P.ID_Produk " &
                "ORDER BY TP.Tanggal_Produksi DESC, K.ID_Kualitas DESC"

            da = New OleDbDataAdapter(query, CNN)

            dt = New DataTable()

            da.Fill(dt)

            dgvRiwayatKualitas.DataSource = dt

            AturKolomGrid()

            CNN.Close()


            '==================================================
            ' SET TANGGAL KE DATA TERBARU
            '==================================================
            If dt.Rows.Count > 0 Then

                If Not IsDBNull(dt.Rows(0)("Tanggal_Produksi")) Then

                    Dim tanggalTerbaru As Date =
                        Convert.ToDateTime(
                            dt.Rows(0)("Tanggal_Produksi")
                        )

                    DateTimePicker1.Value = tanggalTerbaru
                    dtpTanggal.Value = tanggalTerbaru

                End If

            End If

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menampilkan data kualitas." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' ATUR KOLOM DGV
    '==========================================================
    Private Sub AturKolomGrid()

        If dgvRiwayatKualitas.Columns.Count = 0 Then
            Exit Sub
        End If

        With dgvRiwayatKualitas

            If .Columns.Contains("ID_Kualitas") Then
                .Columns("ID_Kualitas").HeaderText =
                    "ID Kualitas"
                .Columns("ID_Kualitas").Width = 90
            End If

            If .Columns.Contains("No_ID") Then
                .Columns("No_ID").HeaderText =
                    "No ID"
                .Columns("No_ID").Width = 70
            End If

            If .Columns.Contains("Nama_Produk") Then
                .Columns("Nama_Produk").HeaderText =
                    "Nama Produk"
                .Columns("Nama_Produk").Width = 180
            End If

            If .Columns.Contains("Level_Sigma") Then
                .Columns("Level_Sigma").HeaderText =
                    "Level Sigma"
                .Columns("Level_Sigma").Width = 90
            End If

            If .Columns.Contains("Jenis_NG") Then
                .Columns("Jenis_NG").HeaderText =
                    "Jenis NG"
                .Columns("Jenis_NG").Width = 140
            End If

            If .Columns.Contains("Jumlah_NG") Then
                .Columns("Jumlah_NG").HeaderText =
                    "Jumlah NG"
                .Columns("Jumlah_NG").Width = 90
            End If

            If .Columns.Contains("ID_Total_Produk") Then
                .Columns("ID_Total_Produk").HeaderText =
                    "ID Total Produksi"
                .Columns("ID_Total_Produk").Width = 125
            End If

            If .Columns.Contains("ID_Produk") Then
                .Columns("ID_Produk").HeaderText =
                    "ID Produk"
                .Columns("ID_Produk").Width = 90
            End If

            If .Columns.Contains("Total_Produksi") Then
                .Columns("Total_Produksi").HeaderText =
                    "Total Produksi"
                .Columns("Total_Produksi").Width = 110
            End If

            If .Columns.Contains("Tanggal_Produksi") Then

                .Columns("Tanggal_Produksi").HeaderText =
                    "Tanggal Produksi"

                .Columns("Tanggal_Produksi").Width = 120

                .Columns(
                    "Tanggal_Produksi"
                ).DefaultCellStyle.Format =
                    "dd/MM/yyyy"

            End If


            For Each kolom As DataGridViewColumn In .Columns

                kolom.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter

                kolom.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter

            Next

        End With

    End Sub


    '==========================================================
    ' LOAD COMBOBOX JENIS NG
    '==========================================================
    Private Sub LoadJenisNG()

        Try

            Koneksi()

            cmbJenisNG.Items.Clear()

            cmbJenisNG.Items.Add(
                "Semua Jenis NG"
            )

            Dim query As String =
                "SELECT DISTINCT Jenis_NG " &
                "FROM Data_Produk_NG " &
                "WHERE Jenis_NG IS NOT NULL " &
                "ORDER BY Jenis_NG"

            cmd = New OleDbCommand(
                query,
                CNN
            )

            Rd = cmd.ExecuteReader()

            While Rd.Read()

                cmbJenisNG.Items.Add(
                    Rd("Jenis_NG").ToString()
                )

            End While

            Rd.Close()

            CNN.Close()

            cmbJenisNG.SelectedIndex = 0

        Catch ex As Exception

            Try

                If Rd IsNot Nothing AndAlso
                   Not Rd.IsClosed Then

                    Rd.Close()

                End If

            Catch
            End Try

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mengambil jenis NG." &
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

            Koneksi()

            Dim keyword As String =
                txtCari.Text.Trim()

            Dim query As String =
                "SELECT " &
                "K.ID_Kualitas, " &
                "K.No_ID, " &
                "P.Nama_Produk, " &
                "K.Level_Sigma, " &
                "NG.Jenis_NG, " &
                "NG.Jumlah_NG, " &
                "TP.ID_Total_Produk, " &
                "TP.ID_Produk, " &
                "TP.Total_Produksi, " &
                "TP.Tanggal_Produksi " &
                "FROM ((Data_Kualitas AS K " &
                "INNER JOIN Data_Produk_NG AS NG " &
                "ON K.ID_NG = NG.ID_NG) " &
                "INNER JOIN Data_Pengelolaan_Total_Produksi AS TP " &
                "ON K.ID_Total_Produk = TP.ID_Total_Produk) " &
                "INNER JOIN Produk AS P " &
                "ON TP.ID_Produk = P.ID_Produk " &
                "WHERE " &
                "K.ID_Kualitas LIKE ? OR " &
                "K.No_ID LIKE ? OR " &
                "P.Nama_Produk LIKE ? OR " &
                "NG.Jenis_NG LIKE ? " &
                "ORDER BY TP.Tanggal_Produksi DESC, " &
                "K.ID_Kualitas DESC"

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

            dgvRiwayatKualitas.DataSource = dt

            AturKolomGrid()

            CNN.Close()

            BuatPChart()

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mencari data." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' TOMBOL TAMPILKAN
    '==========================================================
    Private Sub btnTampilkan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnTampilkan.Click

        FilterJenisNG()

    End Sub


    '==========================================================
    ' FILTER JENIS NG
    '==========================================================
    Private Sub FilterJenisNG()

        Try

            Koneksi()

            Dim query As String =
                "SELECT " &
                "K.ID_Kualitas, " &
                "K.No_ID, " &
                "P.Nama_Produk, " &
                "K.Level_Sigma, " &
                "NG.Jenis_NG, " &
                "NG.Jumlah_NG, " &
                "TP.ID_Total_Produk, " &
                "TP.ID_Produk, " &
                "TP.Total_Produksi, " &
                "TP.Tanggal_Produksi " &
                "FROM ((Data_Kualitas AS K " &
                "INNER JOIN Data_Produk_NG AS NG " &
                "ON K.ID_NG = NG.ID_NG) " &
                "INNER JOIN Data_Pengelolaan_Total_Produksi AS TP " &
                "ON K.ID_Total_Produk = TP.ID_Total_Produk) " &
                "INNER JOIN Produk AS P " &
                "ON TP.ID_Produk = P.ID_Produk "

            If cmbJenisNG.Text <>
                "Semua Jenis NG" Then

                query &= "WHERE NG.Jenis_NG = ? "

            End If

            query &=
                "ORDER BY TP.Tanggal_Produksi DESC, " &
                "K.ID_Kualitas DESC"

            cmd = New OleDbCommand(
                query,
                CNN
            )

            If cmbJenisNG.Text <>
                "Semua Jenis NG" Then

                cmd.Parameters.AddWithValue(
                    "@JenisNG",
                    cmbJenisNG.Text
                )

            End If

            da = New OleDbDataAdapter(cmd)

            dt = New DataTable()

            da.Fill(dt)

            dgvRiwayatKualitas.DataSource = dt

            AturKolomGrid()

            CNN.Close()

            BuatPChart()

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal memfilter data." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' EVENT TANGGAL
    '==========================================================
    Private Sub dtpTanggal_ValueChanged(
        sender As Object,
        e As EventArgs
    ) Handles dtpTanggal.ValueChanged

        BuatPChart()

    End Sub


    '==========================================================
    ' ATUR CHART
    '==========================================================
    Private Sub AturChart()

        Try

            With Chart1

                'Hapus semua konfigurasi bawaan Designer
                .Series.Clear()
                .ChartAreas.Clear()
                .Legends.Clear()
                .Titles.Clear()

                '==================================================
                ' BUAT CHART AREA BARU
                '==================================================
                Dim area As New ChartArea(
                    "AreaPChart"
                )

                area.BackColor = Color.White


                '==================================================
                ' AXIS X
                '==================================================
                area.AxisX.Title =
                    "Tanggal Produksi"

                area.AxisX.TitleFont =
                    New Font(
                        "Segoe UI",
                        10,
                        FontStyle.Bold
                    )

                area.AxisX.LabelStyle.Font =
                    New Font(
                        "Segoe UI",
                        8
                    )

                area.AxisX.LabelStyle.Angle = -45

                area.AxisX.MajorGrid.Enabled = False


                '==================================================
                ' AXIS Y
                '==================================================
                area.AxisY.Title =
                    "Proporsi NG (%)"

                area.AxisY.TitleFont =
                    New Font(
                        "Segoe UI",
                        10,
                        FontStyle.Bold
                    )

                area.AxisY.LabelStyle.Font =
                    New Font(
                        "Segoe UI",
                        8
                    )

                area.AxisY.Minimum = 0

                area.AxisY.MajorGrid.LineDashStyle =
                    ChartDashStyle.Dash


                '==================================================
                ' MASUKKAN AREA KE CHART
                '==================================================
                .ChartAreas.Add(area)

                .Dock = DockStyle.None

                .Width = 650
                .Height = 350

                .BackColor = Color.White

            End With


            'Buat grafik setelah ChartArea selesai dibuat
            BuatPChart()

        Catch ex As Exception

            MessageBox.Show(
                "Gagal mengatur grafik." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' BUAT P-CHART
    '==========================================================
    Private Sub BuatPChart()

        Try

            '==================================================
            ' CEK CHART AREA
            '==================================================
            If Chart1.ChartAreas.Count = 0 Then
                Exit Sub
            End If


            '==================================================
            ' BERSIHKAN CHART LAMA
            '==================================================
            Chart1.Series.Clear()
            Chart1.Legends.Clear()
            Chart1.Titles.Clear()


            '==================================================
            ' TANGGAL YANG DIPILIH
            '==================================================
            Dim tanggalDipilih As Date =
                dtpTanggal.Value.Date


            '==================================================
            ' AMBIL DATA SESUAI TANGGAL
            '==================================================
            Dim dataGrafik As New List(Of DataGridViewRow)

            For Each row As DataGridViewRow In
                dgvRiwayatKualitas.Rows

                If row.IsNewRow Then
                    Continue For
                End If

                Try

                    If row.Cells(
                        "Tanggal_Produksi"
                    ).Value Is Nothing Then

                        Continue For

                    End If


                    If IsDBNull(
                        row.Cells(
                            "Tanggal_Produksi"
                        ).Value
                    ) Then

                        Continue For

                    End If


                    Dim tanggalData As Date =
                        Convert.ToDateTime(
                            row.Cells(
                                "Tanggal_Produksi"
                            ).Value
                        ).Date


                    If tanggalData =
                        tanggalDipilih Then

                        dataGrafik.Add(row)

                    End If

                Catch

                    Continue For

                End Try

            Next


            '==================================================
            ' TIDAK ADA DATA
            '==================================================
            If dataGrafik.Count = 0 Then

                AturSumbuY(10)

                Return

            End If


            '==================================================
            ' HITUNG TOTAL NG DAN PRODUKSI
            '==================================================
            Dim totalNG As Double = 0
            Dim totalProduksi As Double = 0


            For Each row As DataGridViewRow
                In dataGrafik

                Try

                    If row.Cells(
                        "Jumlah_NG"
                    ).Value Is Nothing Then

                        Continue For

                    End If


                    If row.Cells(
                        "Total_Produksi"
                    ).Value Is Nothing Then

                        Continue For

                    End If


                    If IsDBNull(
                        row.Cells(
                            "Jumlah_NG"
                        ).Value
                    ) Then

                        Continue For

                    End If


                    If IsDBNull(
                        row.Cells(
                            "Total_Produksi"
                        ).Value
                    ) Then

                        Continue For

                    End If


                    Dim jumlahNG As Double =
                        Convert.ToDouble(
                            row.Cells(
                                "Jumlah_NG"
                            ).Value
                        )


                    Dim produksi As Double =
                        Convert.ToDouble(
                            row.Cells(
                                "Total_Produksi"
                            ).Value
                        )


                    If produksi > 0 Then

                        totalNG += jumlahNG
                        totalProduksi += produksi

                    End If

                Catch

                    Continue For

                End Try

            Next


            '==================================================
            ' PRODUKSI TIDAK ADA
            '==================================================
            If totalProduksi <= 0 Then

                AturSumbuY(10)

                Return

            End If


            '==================================================
            ' p-BAR / CL
            '==================================================
            Dim pBar As Double =
                totalNG / totalProduksi


            '==================================================
            ' SERIES PROPORSI NG
            '==================================================
            Dim seriesP As New Series(
                "Proporsi NG"
            )

            seriesP.ChartType =
                SeriesChartType.Line

            seriesP.BorderWidth = 3

            seriesP.MarkerStyle =
                MarkerStyle.Circle

            seriesP.MarkerSize = 7

            seriesP.IsValueShownAsLabel = True

            seriesP.Label =
                "#VALY{0.00}%"

            seriesP.ToolTip =
                "Proporsi NG = #VALY{0.00}%"


            '==================================================
            ' SERIES CL
            '==================================================
            Dim seriesCL As New Series(
                "CL"
            )

            seriesCL.ChartType =
                SeriesChartType.Line

            seriesCL.BorderWidth = 2

            seriesCL.BorderDashStyle =
                ChartDashStyle.Dash


            '==================================================
            ' SERIES UCL
            '==================================================
            Dim seriesUCL As New Series(
                "UCL"
            )

            seriesUCL.ChartType =
                SeriesChartType.Line

            seriesUCL.BorderWidth = 2

            seriesUCL.BorderDashStyle =
                ChartDashStyle.Dot


            '==================================================
            ' SERIES LCL
            '==================================================
            Dim seriesLCL As New Series(
                "LCL"
            )

            seriesLCL.ChartType =
                SeriesChartType.Line

            seriesLCL.BorderWidth = 2

            seriesLCL.BorderDashStyle =
                ChartDashStyle.Dot


            '==================================================
            ' NILAI MAKSIMUM
            '==================================================
            Dim nilaiMaksimum As Double = 0


            '==================================================
            ' HITUNG DATA P-CHART
            '==================================================
            For Each row As DataGridViewRow
                In dataGrafik

                Try

                    If row.Cells(
                        "Jumlah_NG"
                    ).Value Is Nothing Then

                        Continue For

                    End If


                    If row.Cells(
                        "Total_Produksi"
                    ).Value Is Nothing Then

                        Continue For

                    End If


                    If IsDBNull(
                        row.Cells(
                            "Jumlah_NG"
                        ).Value
                    ) Then

                        Continue For

                    End If


                    If IsDBNull(
                        row.Cells(
                            "Total_Produksi"
                        ).Value
                    ) Then

                        Continue For

                    End If


                    Dim jumlahNG As Double =
                        Convert.ToDouble(
                            row.Cells(
                                "Jumlah_NG"
                            ).Value
                        )


                    Dim produksi As Double =
                        Convert.ToDouble(
                            row.Cells(
                                "Total_Produksi"
                            ).Value
                        )


                    If produksi <= 0 Then
                        Continue For
                    End If


                    '==================================================
                    ' PROPORSI NG
                    '==================================================
                    Dim p As Double =
                        jumlahNG / produksi

                    Dim persenP As Double =
                        p * 100


                    '==================================================
                    ' STANDARD ERROR
                    '==================================================
                    Dim sigmaP As Double =
                        Math.Sqrt(
                            (
                                pBar *
                                (1 - pBar)
                            ) / produksi
                        )


                    '==================================================
                    ' UCL
                    '==================================================
                    Dim ucl As Double =
                        pBar + (3 * sigmaP)


                    '==================================================
                    ' LCL
                    '==================================================
                    Dim lcl As Double =
                        pBar - (3 * sigmaP)


                    '==================================================
                    ' BATAS LCL
                    '==================================================
                    If lcl < 0 Then
                        lcl = 0
                    End If


                    '==================================================
                    ' BATAS UCL
                    '==================================================
                    If ucl > 1 Then
                        ucl = 1
                    End If


                    '==================================================
                    ' KONVERSI KE PERSEN
                    '==================================================
                    Dim persenCL As Double =
                        pBar * 100

                    Dim persenUCL As Double =
                        ucl * 100

                    Dim persenLCL As Double =
                        lcl * 100


                    '==================================================
                    ' TANGGAL
                    '==================================================
                    Dim tanggal As String = ""


                    If Not IsDBNull(
                        row.Cells(
                            "Tanggal_Produksi"
                        ).Value
                    ) Then

                        tanggal =
                            Convert.ToDateTime(
                                row.Cells(
                                    "Tanggal_Produksi"
                                ).Value
                            ).ToString("dd/MM")

                    End If


                    '==================================================
                    ' MASUKKAN PROPORSI
                    '==================================================
                    Dim indexP As Integer =
                        seriesP.Points.AddY(
                            persenP
                        )


                    seriesP.Points(
                        indexP
                    ).AxisLabel =
                        tanggal


                    '==================================================
                    ' MASUKKAN CL
                    '==================================================
                    seriesCL.Points.AddY(
                        persenCL
                    )


                    '==================================================
                    ' MASUKKAN UCL
                    '==================================================
                    seriesUCL.Points.AddY(
                        persenUCL
                    )


                    '==================================================
                    ' MASUKKAN LCL
                    '==================================================
                    seriesLCL.Points.AddY(
                        persenLCL
                    )


                    '==================================================
                    ' CARI NILAI TERBESAR
                    '==================================================
                    If persenP > nilaiMaksimum Then

                        nilaiMaksimum =
                            persenP

                    End If


                    If persenUCL > nilaiMaksimum Then

                        nilaiMaksimum =
                            persenUCL

                    End If


                    If persenLCL > nilaiMaksimum Then

                        nilaiMaksimum =
                            persenLCL

                    End If


                Catch

                    Continue For

                End Try

            Next


            '==================================================
            ' MASUKKAN SERIES
            '==================================================
            Chart1.Series.Add(seriesP)

            Chart1.Series.Add(seriesCL)

            Chart1.Series.Add(seriesUCL)

            Chart1.Series.Add(seriesLCL)


            '==================================================
            ' LEGEND TEXT
            '==================================================
            seriesP.LegendText =
                "Proporsi NG"

            seriesCL.LegendText =
                "CL"

            seriesUCL.LegendText =
                "UCL"

            seriesLCL.LegendText =
                "LCL"


            '==================================================
            ' LEGEND
            '==================================================
            Dim legend As New Legend()

            legend.Docking =
                Docking.Bottom

            legend.Alignment =
                StringAlignment.Center

            legend.Font =
                New Font(
                    "Segoe UI",
                    8
                )

            Chart1.Legends.Add(legend)


            '==================================================
            ' JUDUL
            '==================================================
            Dim title As New Title(
                "P-Chart Produk NG"
            )

            title.Font =
                New Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                )

            Chart1.Titles.Add(title)


            '==================================================
            ' ATUR SUMBU Y
            '==================================================
            AturSumbuY(
                nilaiMaksimum
            )


        Catch ex As Exception

            MessageBox.Show(
                "Gagal membuat P-Chart." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '==========================================================
    ' ATUR SUMBU Y
    '==========================================================
    Private Sub AturSumbuY(
        nilaiMaksimum As Double
    )

        If Chart1.ChartAreas.Count = 0 Then
            Exit Sub
        End If


        Dim maksimum As Double


        If nilaiMaksimum <= 0 Then

            maksimum = 10

        Else

            maksimum =
                Math.Ceiling(
                    nilaiMaksimum * 1.2
                )

        End If


        If maksimum > 100 Then
            maksimum = 100
        End If


        If maksimum < 1 Then
            maksimum = 1
        End If


        '==================================================
        ' PENTING:
        ' TIDAK LAGI MEMANGGIL ChartAreas("AreaPChart")
        '
        ' Kita langsung ambil ChartArea pertama.
        ' Jadi tidak peduli nama AreaChart,
        ' ChartArea1, atau AreaPChart.
        '==================================================
        Dim area As ChartArea =
            Chart1.ChartAreas(0)


        With area.AxisY

            .Minimum = 0

            .Maximum = maksimum


            Dim interval As Double =
                maksimum / 5


            If interval <= 0 Then
                interval = 1
            End If


            .Interval = interval

        End With

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


    '==========================================================
    ' LOGOUT
    '==========================================================
    Private Sub btnLogoutIK_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnLogoutIK.Click

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

End Class