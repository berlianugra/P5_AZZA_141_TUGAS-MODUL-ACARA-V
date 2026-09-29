Imports System.Data.OleDb
Imports System.Drawing
Imports System.Windows.Forms.DataVisualization.Charting

Public Class FormRiwayatNG

    '========================================================
    ' VARIABEL
    '========================================================

    Private dtRiwayat As New DataTable()
    Private Const PLACEHOLDER_TEXT As String = "Cari riwayat data..."
    Private sedangPlaceholder As Boolean = False
    Private siapInputBaru As Boolean = False
    Private ukuranAwalBtnLogout As Size
    Private posisiAwalBtnLogout As Point


    '========================================================
    ' FORM LOAD
    '========================================================

    Private Sub FormRiwayatNG_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '====================================================
        ' TEXTBOX PENCARIAN
        '====================================================

        txtRiwayatData.ReadOnly = False
        txtRiwayatData.Enabled = True
        PasangPlaceholder()


        '====================================================
        ' DATAGRIDVIEW
        '====================================================

        AturDataGridView()


        '====================================================
        ' TAMPILKAN DATA
        '====================================================

        TampilkanDataRiwayat()


        '====================================================
        ' CHART
        '====================================================

        AturChart()


        '====================================================
        ' MENU PATEN
        '====================================================

        lblInput.Visible = True
        lblRiwayat.Visible = True
        lblInput.BringToFront()
        lblRiwayat.BringToFront()


        '====================================================
        ' CURSOR MENU
        '====================================================

        lblDashboard.Cursor = Cursors.Hand
        lblProduksi.Cursor = Cursors.Hand
        lblNG.Cursor = Cursors.Hand
        lblInput.Cursor = Cursors.Hand
        lblRiwayat.Cursor = Cursors.Hand
        btnLogout.Cursor = Cursors.Hand


        '====================================================
        ' SIMPAN UKURAN DAN POSISI AWAL LOGOUT
        '====================================================

        ukuranAwalBtnLogout = btnLogout.Size
        posisiAwalBtnLogout = btnLogout.Location

    End Sub


    '========================================================
    ' PLACEHOLDER TEXTBOX
    '========================================================

    Private Sub PasangPlaceholder()

        sedangPlaceholder = True
        siapInputBaru = False
        txtRiwayatData.Text = PLACEHOLDER_TEXT
        txtRiwayatData.ForeColor = Color.Gray

    End Sub


    '========================================================
    ' TEXTBOX MENDAPAT FOCUS
    '========================================================

    Private Sub txtRiwayatData_Enter(sender As Object, e As EventArgs) Handles txtRiwayatData.Enter

        If sedangPlaceholder Then

            txtRiwayatData.Clear()
            txtRiwayatData.ForeColor = Color.Black
            sedangPlaceholder = False
            siapInputBaru = False

            Exit Sub

        End If


        If siapInputBaru Then

            txtRiwayatData.Clear()
            txtRiwayatData.ForeColor = Color.Black
            siapInputBaru = False

        End If

    End Sub


    '========================================================
    ' TEXTBOX KEHILANGAN FOCUS
    '========================================================

    Private Sub txtRiwayatData_Leave(sender As Object, e As EventArgs) Handles txtRiwayatData.Leave

        If String.IsNullOrWhiteSpace(txtRiwayatData.Text) Then
            PasangPlaceholder()
        End If

    End Sub


    '========================================================
    ' ENTER PADA TEXTBOX
    '========================================================

    Private Sub txtRiwayatData_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRiwayatData.KeyDown

        If e.KeyCode = Keys.Enter Then

            e.SuppressKeyPress = True
            e.Handled = True

            If sedangPlaceholder Then Exit Sub
            If String.IsNullOrWhiteSpace(txtRiwayatData.Text) Then Exit Sub

            btnCari1.PerformClick()

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
        dgvRiwayat.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRiwayat.MultiSelect = False
        dgvRiwayat.ReadOnly = False

        dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgvRiwayat.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dgvRiwayat.ScrollBars = ScrollBars.Both

        dgvRiwayat.RowTemplate.Height = 30

        dgvRiwayat.Columns.Clear()


        '====================================================
        ' CHECKBOX PILIH
        '====================================================

        Dim colPilih As New DataGridViewCheckBoxColumn()

        colPilih.Name = "Pilih"
        colPilih.HeaderText = ""
        colPilih.Width = 45
        colPilih.ReadOnly = False
        colPilih.AutoSizeMode = DataGridViewAutoSizeColumnMode.None

        dgvRiwayat.Columns.Add(colPilih)


        '====================================================
        ' KOLOM DATA
        '====================================================

        TambahKolom("ID_NG", "ID NG", 90)
        TambahKolom("No_ID", "No ID", 80)
        TambahKolom("ID_Total_Produk", "ID Total Produk", 130)
        TambahKolom("Jumlah_Jenis_NG", "Jumlah Jenis NG", 125)
        TambahKolom("Jenis_NG", "Jenis NG", 160)
        TambahKolom("Jumlah_NG", "Jumlah NG", 100)

    End Sub


    '========================================================
    ' TAMBAH KOLOM DGV
    '========================================================

    Private Sub TambahKolom(nama As String, header As String, lebar As Integer)

        Dim kolom As New DataGridViewTextBoxColumn()

        kolom.Name = nama
        kolom.HeaderText = header
        kolom.DataPropertyName = nama
        kolom.Width = lebar
        kolom.MinimumWidth = lebar
        kolom.ReadOnly = True
        kolom.AutoSizeMode = DataGridViewAutoSizeColumnMode.None

        dgvRiwayat.Columns.Add(kolom)

    End Sub


    '========================================================
    ' TAMPILKAN SEMUA DATA RIWAYAT NG
    '========================================================

    Private Sub TampilkanDataRiwayat()

        Try

            Koneksi()

            Dim query As String =
                "SELECT " &
                "[ID_NG], " &
                "[No_ID], " &
                "[ID_Total_Produk], " &
                "[Jumlah_Jenis_NG], " &
                "[Jenis_NG], " &
                "[Jumlah_NG] " &
                "FROM [Data_Produk_NG] " &
                "ORDER BY [ID_NG] ASC"

            dtRiwayat.Clear()

            Using cmdRiwayat As New OleDbCommand(query, CNN)

                Using adapter As New OleDbDataAdapter(cmdRiwayat)
                    adapter.Fill(dtRiwayat)
                End Using

            End Using

            dgvRiwayat.DataSource = Nothing
            dgvRiwayat.DataSource = dtRiwayat

        Catch ex As Exception

            MessageBox.Show(
                "Gagal mengambil data riwayat NG." &
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
    ' BUTTON CARI DATA
    '========================================================

    Private Sub btnCari1_Click(sender As Object, e As EventArgs) Handles btnCari1.Click

        If sedangPlaceholder Then

            dgvRiwayat.DataSource = dtRiwayat
            Exit Sub

        End If


        Dim kataKunci As String = txtRiwayatData.Text.Trim()


        If kataKunci = "" Then

            dgvRiwayat.DataSource = dtRiwayat
            Exit Sub

        End If


        Try

            Dim view As New DataView(dtRiwayat)

            Dim keyword As String = kataKunci.Replace("'", "''")


            view.RowFilter =
                "[ID_NG] LIKE '%" &
                keyword &
                "%' OR " &
                "[No_ID] LIKE '%" &
                keyword &
                "%' OR " &
                "[ID_Total_Produk] LIKE '%" &
                keyword &
                "%' OR " &
                "[Jenis_NG] LIKE '%" &
                keyword &
                "%'"


            dgvRiwayat.DataSource = Nothing
            dgvRiwayat.DataSource = view

            siapInputBaru = True
            txtRiwayatData.ForeColor = Color.Black

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
    ' BUTTON HAPUS
    '========================================================

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click

        Dim adaData As Boolean = False


        '====================================================
        ' CEK CHECKBOX
        '====================================================

        For Each row As DataGridViewRow In dgvRiwayat.Rows

            If Not row.IsNewRow Then

                If row.Cells("Pilih").Value IsNot Nothing Then

                    If Convert.ToBoolean(row.Cells("Pilih").Value) Then

                        adaData = True
                        Exit For

                    End If

                End If

            End If

        Next


        '====================================================
        ' TIDAK ADA DATA
        '====================================================

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

        Dim jawab As DialogResult =
            MessageBox.Show(
                "Anda yakin ingin menghapus data yang dipilih?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )


        If jawab = DialogResult.No Then Exit Sub


        Try

            Koneksi()


            '================================================
            ' HAPUS DATA
            '================================================

            For Each row As DataGridViewRow In dgvRiwayat.Rows

                If Not row.IsNewRow Then

                    If row.Cells("Pilih").Value IsNot Nothing AndAlso
                       Convert.ToBoolean(row.Cells("Pilih").Value) Then

                        Dim idNG As String =
                            row.Cells("ID_NG").Value.ToString()

                        Dim queryDelete As String =
                            "DELETE FROM [Data_Produk_NG] " &
                            "WHERE [ID_NG] = ?"

                        Using cmdDelete As New OleDbCommand(queryDelete, CNN)

                            cmdDelete.Parameters.AddWithValue("@p1", idNG)
                            cmdDelete.ExecuteNonQuery()

                        End Using

                    End If

                End If

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

            dtRiwayat.Clear()
            TampilkanDataRiwayat()


        Catch ex As Exception

            MessageBox.Show(
                "Data gagal dihapus." &
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
    ' CHART AWAL
    '========================================================

    Private Sub AturChart()

        Chart1.Series.Clear()
        Chart1.ChartAreas.Clear()
        Chart1.Legends.Clear()
        Chart1.Titles.Clear()

        Chart1.BackColor = Color.White
        Chart1.BorderlineColor = Color.LightGray
        Chart1.BorderlineDashStyle = ChartDashStyle.Solid
        Chart1.BorderlineWidth = 1


        '====================================================
        ' CHART AREA
        '====================================================

        Dim area As New ChartArea("AreaNG")

        area.BackColor = Color.White
        area.BorderColor = Color.White

        area.Position.Auto = False
        area.Position.X = 3
        area.Position.Y = 3
        area.Position.Width = 94
        area.Position.Height = 94


        '====================================================
        ' SUMBU X
        '====================================================

        area.AxisX.Title = "Jumlah NG"
        area.AxisX.TitleFont = New Font("Segoe UI", 9, FontStyle.Bold)
        area.AxisX.LabelStyle.Font = New Font("Segoe UI", 8)
        area.AxisX.MajorGrid.Enabled = True
        area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
        area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash
        area.AxisX.LineColor = Color.FromArgb(180, 180, 180)


        '====================================================
        ' SUMBU Y
        '====================================================

        area.AxisY.Title = "ID NG"
        area.AxisY.TitleFont = New Font("Segoe UI", 9, FontStyle.Bold)
        area.AxisY.LabelStyle.Font = New Font("Segoe UI", 8)
        area.AxisY.MajorGrid.Enabled = False
        area.AxisY.LineColor = Color.FromArgb(180, 180, 180)
        area.AxisY.Interval = 1

        Chart1.ChartAreas.Add(area)


        '====================================================
        ' SERIES
        '====================================================

        Dim series As New Series("Jumlah NG")

        series.ChartType = SeriesChartType.Bar
        series.IsValueShownAsLabel = True
        series.IsVisibleInLegend = False
        series.YValueType = ChartValueType.Double
        series.XValueType = ChartValueType.String
        series.SetCustomProperty("PointWidth", "0.65")
        series.Label = "#VAL{#,##0}"
        series.Font = New Font("Segoe UI", 8, FontStyle.Bold)

        Chart1.Series.Add(series)

    End Sub


    '========================================================
    ' BUTTON CARI CHART
    '========================================================

    Private Sub btnCari2_Click(sender As Object, e As EventArgs) Handles btnCari2.Click

        If cmbJenisNG.SelectedIndex = -1 Then

            MessageBox.Show(
                "Pilih jenis NG terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        Dim jenisNG As String = cmbJenisNG.Text.Trim()


        If jenisNG = "" Then

            MessageBox.Show(
                "Jenis NG belum dipilih.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        TampilkanChartJenisNG(jenisNG)

    End Sub


    '========================================================
    ' TAMPILKAN CHART BERDASARKAN JENIS NG
    '========================================================

    Private Sub TampilkanChartJenisNG(jenisNG As String)

        Try

            Koneksi()


            '================================================
            ' QUERY
            '================================================

            Dim query As String =
                "SELECT [ID_NG], [Jumlah_NG] " &
                "FROM [Data_Produk_NG] " &
                "WHERE [Jenis_NG] = ? " &
                "ORDER BY [Jumlah_NG] DESC"


            Dim tabel As New DataTable()


            Using cmdChart As New OleDbCommand(query, CNN)

                cmdChart.Parameters.AddWithValue("@p1", jenisNG)

                Using adapter As New OleDbDataAdapter(cmdChart)
                    adapter.Fill(tabel)
                End Using

            End Using


            '================================================
            ' RESET CHART
            '================================================

            Chart1.Series.Clear()
            Chart1.ChartAreas.Clear()
            Chart1.Legends.Clear()
            Chart1.Titles.Clear()


            '================================================
            ' BACKGROUND
            '================================================

            Chart1.BackColor = Color.White
            Chart1.BorderlineColor = Color.LightGray
            Chart1.BorderlineDashStyle = ChartDashStyle.Solid
            Chart1.BorderlineWidth = 1


            '================================================
            ' CHART AREA
            '================================================

            Dim area As New ChartArea("AreaNG")

            area.BackColor = Color.White
            area.BorderColor = Color.White

            area.Position.Auto = False
            area.Position.X = 3
            area.Position.Y = 8
            area.Position.Width = 94
            area.Position.Height = 88


            '================================================
            ' SUMBU X
            '================================================

            area.AxisX.Title = "Jumlah NG"
            area.AxisX.TitleFont = New Font("Segoe UI", 9, FontStyle.Bold)
            area.AxisX.LabelStyle.Font = New Font("Segoe UI", 8)
            area.AxisX.MajorGrid.Enabled = True
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash
            area.AxisX.LineColor = Color.FromArgb(180, 180, 180)


            '================================================
            ' SUMBU Y
            '================================================

            area.AxisY.Title = "ID NG"
            area.AxisY.TitleFont = New Font("Segoe UI", 9, FontStyle.Bold)
            area.AxisY.LabelStyle.Font = New Font("Segoe UI", 8)
            area.AxisY.MajorGrid.Enabled = False
            area.AxisY.LineColor = Color.FromArgb(180, 180, 180)
            area.AxisY.Interval = 1

            Chart1.ChartAreas.Add(area)


            '================================================
            ' SERIES
            '================================================

            Dim series As New Series("Jumlah NG")

            series.ChartType = SeriesChartType.Bar
            series.IsValueShownAsLabel = True
            series.IsVisibleInLegend = False
            series.YValueType = ChartValueType.Double
            series.XValueType = ChartValueType.String
            series.SetCustomProperty("PointWidth", "0.65")
            series.Label = "#VAL{#,##0}"
            series.Font = New Font("Segoe UI", 8, FontStyle.Bold)


            '================================================
            ' MASUKKAN DATA
            '================================================

            For Each row As DataRow In tabel.Rows

                Dim idNG As String = row("ID_NG").ToString()
                Dim jumlahNG As Double = 0

                If Not IsDBNull(row("Jumlah_NG")) Then

                    Double.TryParse(
                        row("Jumlah_NG").ToString(),
                        jumlahNG
                    )

                End If


                Dim pointIndex As Integer =
                    series.Points.AddY(jumlahNG)


                'Label sumbu Y
                series.Points(pointIndex).AxisLabel = idNG

                'Label angka
                series.Points(pointIndex).Label =
                    jumlahNG.ToString("#,##0")

            Next


            Chart1.Series.Add(series)


            '================================================
            ' JUDUL
            '================================================

            Dim judul As New Title()

            judul.Text = "Riwayat " & jenisNG & " - Jumlah NG"
            judul.Font = New Font("Segoe UI", 12, FontStyle.Bold)
            judul.ForeColor = Color.FromArgb(35, 35, 70)
            judul.Alignment = ContentAlignment.TopLeft

            Chart1.Titles.Add(judul)


            '================================================
            ' TIDAK ADA DATA
            '================================================

            If tabel.Rows.Count = 0 Then

                MessageBox.Show(
                    "Tidak ada data NG untuk jenis " & jenisNG & ".",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            End If


        Catch ex As Exception

            MessageBox.Show(
                "Gagal menampilkan chart." &
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
    ' BUTTON KEMBALI
    '========================================================

    Private Sub btnKembali_Click(sender As Object, e As EventArgs)
        Me.Close()
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


    Private Sub lblInput_Click(sender As Object, e As EventArgs) Handles lblInput.Click

        Dim frm As New FormInputNG()
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
    ' PANEL PAINT
    '========================================================

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

End Class