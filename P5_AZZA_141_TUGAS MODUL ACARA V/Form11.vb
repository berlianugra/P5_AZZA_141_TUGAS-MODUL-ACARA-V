Imports System.Data.OleDb
Imports System.Windows.Forms.DataVisualization.Charting

Public Class FormRiwayatProduksi

    '========================================================
    ' VARIABEL
    '========================================================

    Private dtRiwayat As New DataTable()
    Private Const PLACEHOLDER_TEXT As String = "Cari data..."
    Private sedangPlaceholder As Boolean = False
    Private siapInputBaru As Boolean = False

    'Posisi & ukuran awal tombol logout
    Private ukuranAwalBtnLogout As Size
    Private posisiAwalBtnLogout As Point


    '========================================================
    ' FORM LOAD
    '========================================================

    Private Sub FormRiwayatProduksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Textbox pencarian
        txtRiwayatData.ReadOnly = False
        txtRiwayatData.Enabled = True
        PasangPlaceholder()

        'Simpan posisi & ukuran awal logout
        ukuranAwalBtnLogout = btnLogout.Size
        posisiAwalBtnLogout = btnLogout.Location

        'Atur DGV, ComboBox dan Chart
        AturDataGridView()
        IsiComboProduk()
        TampilkanDataRiwayat()
        AturChart()

        'Cursor
        lblDashboard.Cursor = Cursors.Hand
        lblProduksi.Cursor = Cursors.Hand
        lblNG.Cursor = Cursors.Hand
        lblInput.Cursor = Cursors.Hand
        lblRiwayat.Cursor = Cursors.Hand
        btnLogout.Cursor = Cursors.Hand

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
    ' TEXTBOX ENTER
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
    ' TEXTBOX LEAVE
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
        dgvRiwayat.RowHeadersVisible = False
        dgvRiwayat.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRiwayat.MultiSelect = False
        dgvRiwayat.Columns.Clear()

        'Checkbox pilih
        Dim colPilih As New DataGridViewCheckBoxColumn()
        colPilih.Name = "Pilih"
        colPilih.HeaderText = ""
        colPilih.Width = 40
        colPilih.ReadOnly = False
        dgvRiwayat.Columns.Add(colPilih)

        'Kolom data
        TambahKolom("ID_Total_Produk", "ID Total Produk", 120)
        TambahKolom("No_ID", "No ID", 80)
        TambahKolom("ID_Produk", "ID Produk", 90)
        TambahKolom("Kode_Batch", "Kode Batch", 130)
        TambahKolom("Total_Produksi", "Total Produksi", 110)
        TambahKolom("Tanggal_Produksi", "Tanggal Produksi", 120)

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
        kolom.ReadOnly = True

        dgvRiwayat.Columns.Add(kolom)

    End Sub


    '========================================================
    ' ISI COMBOBOX PRODUK
    '========================================================

    Private Sub IsiComboProduk()

        cmbNamaProduk.Items.Clear()

        cmbNamaProduk.Items.Add("Part Number 7105-5552")
        cmbNamaProduk.Items.Add("Part Number 7105-5551")
        cmbNamaProduk.Items.Add("Part Number 7105-3578")

        cmbNamaProduk.SelectedIndex = -1

    End Sub


    '========================================================
    ' MAPPING NAMA PRODUK KE ID
    '========================================================

    Private Function GetIDProduk() As String

        Select Case cmbNamaProduk.Text.Trim()

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
    ' TAMPILKAN SEMUA DATA RIWAYAT
    '========================================================

    Private Sub TampilkanDataRiwayat()

        Try

            Koneksi()

            Dim query As String = "SELECT [ID_Total_Produk], [No_ID], [ID_Produk], [Kode_Batch], [Total_Produksi], [Tanggal_Produksi] " &
                                  "FROM [Data_Pengelolaan_Total_Produksi] " &
                                  "ORDER BY [Tanggal_Produksi] ASC"

            dtRiwayat.Clear()

            Using cmdRiwayat As New OleDbCommand(query, CNN)
                Using adapter As New OleDbDataAdapter(cmdRiwayat)
                    adapter.Fill(dtRiwayat)
                End Using
            End Using

            dgvRiwayat.DataSource = Nothing
            dgvRiwayat.DataSource = dtRiwayat

        Catch ex As Exception

            MessageBox.Show("Gagal mengambil data riwayat." & vbCrLf & ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

        Finally

            TutupKoneksi()

        End Try

    End Sub


    '========================================================
    ' BUTTON CARI 1
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

            view.RowFilter = "[ID_Total_Produk] LIKE '%" & keyword & "%' OR " &
                             "[No_ID] LIKE '%" & keyword & "%' OR " &
                             "[ID_Produk] LIKE '%" & keyword & "%' OR " &
                             "[Kode_Batch] LIKE '%" & keyword & "%'"

            dgvRiwayat.DataSource = Nothing
            dgvRiwayat.DataSource = view

            siapInputBaru = True
            txtRiwayatData.ForeColor = Color.Black

        Catch ex As Exception

            MessageBox.Show("Gagal mencari data." & vbCrLf & ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

        End Try

    End Sub


    '========================================================
    ' BUTTON HAPUS
    '========================================================

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click

        Dim adaData As Boolean = False

        For Each row As DataGridViewRow In dgvRiwayat.Rows

            If Not row.IsNewRow AndAlso
               row.Cells("Pilih").Value IsNot Nothing AndAlso
               Convert.ToBoolean(row.Cells("Pilih").Value) Then

                adaData = True
                Exit For

            End If

        Next

        If Not adaData Then

            MessageBox.Show("Pilih data yang ingin dihapus terlebih dahulu.",
                            "Peringatan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)

            Exit Sub

        End If

        Dim jawab As DialogResult = MessageBox.Show(
            "Anda yakin ingin menghapus data yang dipilih?",
            "Konfirmasi Hapus",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If jawab = DialogResult.No Then Exit Sub

        Try

            Koneksi()

            For Each row As DataGridViewRow In dgvRiwayat.Rows

                If Not row.IsNewRow AndAlso
                   row.Cells("Pilih").Value IsNot Nothing AndAlso
                   Convert.ToBoolean(row.Cells("Pilih").Value) Then

                    Dim idTotal As String = row.Cells("ID_Total_Produk").Value.ToString()

                    Dim queryDelete As String = "DELETE FROM [Data_Pengelolaan_Total_Produksi] WHERE [ID_Total_Produk] = ?"

                    Using cmdDelete As New OleDbCommand(queryDelete, CNN)

                        cmdDelete.Parameters.AddWithValue("@p1", idTotal)
                        cmdDelete.ExecuteNonQuery()

                    End Using

                End If

            Next

            MessageBox.Show("Data berhasil dihapus.",
                            "Berhasil",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)

            dtRiwayat.Clear()
            TampilkanDataRiwayat()

        Catch ex As Exception

            MessageBox.Show("Data gagal dihapus." & vbCrLf & ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

        Finally

            TutupKoneksi()

        End Try

    End Sub


    '========================================================
    ' ATUR CHART
    '========================================================

    Private Sub AturChart()

        Chart1.Series.Clear()
        Chart1.Legends.Clear()
        Chart1.Titles.Clear()

        Chart1.Titles.Add("Total Produksi")

        Dim series As New Series("Produksi")
        series.ChartType = SeriesChartType.Pie
        series.IsValueShownAsLabel = True

        Chart1.Series.Add(series)

        Dim legend As New Legend("LegendProduksi")
        legend.Docking = Docking.Right

        Chart1.Legends.Add(legend)
        series.Legend = "LegendProduksi"

    End Sub


    '========================================================
    ' BUTTON CARI 2
    '========================================================

    Private Sub btnCari2_Click(sender As Object, e As EventArgs) Handles btnCari2.Click

        If cmbNamaProduk.SelectedIndex = -1 Then

            MessageBox.Show("Pilih nama produk terlebih dahulu.",
                            "Peringatan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)

            Exit Sub

        End If

        Dim idProduk As String = GetIDProduk()

        If idProduk = "" Then

            MessageBox.Show("ID produk tidak ditemukan.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

            Exit Sub

        End If

        TampilkanChartProduk(idProduk)

    End Sub


    '========================================================
    ' TAMPILKAN CHART BERDASARKAN PRODUK
    '========================================================

    Private Sub TampilkanChartProduk(idProduk As String)

        Try

            Koneksi()

            Dim query As String = "SELECT [Kode_Batch], [Total_Produksi] " &
                                  "FROM [Data_Pengelolaan_Total_Produksi] " &
                                  "WHERE [ID_Produk] = ? " &
                                  "ORDER BY [Tanggal_Produksi] ASC"

            Dim tabel As New DataTable()

            Using cmdChart As New OleDbCommand(query, CNN)

                cmdChart.Parameters.AddWithValue("@p1", idProduk)

                Using adapter As New OleDbDataAdapter(cmdChart)
                    adapter.Fill(tabel)
                End Using

            End Using

            Chart1.Series.Clear()
            Chart1.Legends.Clear()
            Chart1.Titles.Clear()

            Chart1.Titles.Add("Total Produksi - " & cmbNamaProduk.Text)

            Dim series As New Series("Produksi")
            series.ChartType = SeriesChartType.Pie
            series.IsValueShownAsLabel = True

            For Each row As DataRow In tabel.Rows

                Dim kodeBatch As String = row("Kode_Batch").ToString()
                Dim total As Double = Convert.ToDouble(row("Total_Produksi"))
                Dim indexPoint As Integer = series.Points.AddY(total)

                series.Points(indexPoint).LegendText = kodeBatch & " : " & total.ToString("#,##0") & " Pcs"

            Next

            Chart1.Series.Add(series)

            Dim legend As New Legend("LegendProduksi")
            legend.Docking = Docking.Right

            Chart1.Legends.Add(legend)
            series.Legend = "LegendProduksi"

        Catch ex As Exception

            MessageBox.Show("Gagal menampilkan chart." & vbCrLf & ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

        Finally

            TutupKoneksi()

        End Try

    End Sub

    '========================================================
    ' NAVIGASI lblInput
    '========================================================

    Private Sub lblInput_Click(sender As Object, e As EventArgs) Handles lblInput.Click

        Dim frm As New FormInputProduksi()

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()

        Me.Hide()

    End Sub


    '========================================================
    ' NAVIGASI lblProduksi
    '========================================================

    Private Sub lblProduksi_Click(sender As Object, e As EventArgs) Handles lblProduksi.Click

        Dim frm As New FormProduksi()

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()

        Me.Hide()

    End Sub


    '========================================================
    ' NAVIGASI lblNG
    '========================================================

    Private Sub lblNG_Click(sender As Object, e As EventArgs) Handles lblNG.Click

        Dim frm As New FormProdukNG()

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = Me.Location
        frm.Show()

        Me.Hide()

    End Sub

    '========================================================
    ' HOVER lblDashboard
    '========================================================

    Private Sub lblDashboard_MouseEnter(sender As Object, e As EventArgs) Handles lblDashboard.MouseEnter
        lblDashboard.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblDashboard_MouseLeave(sender As Object, e As EventArgs) Handles lblDashboard.MouseLeave
        lblDashboard.BackColor = Color.Transparent
    End Sub


    '========================================================
    ' HOVER lblProduksi
    '========================================================

    Private Sub lblProduksi_MouseEnter(sender As Object, e As EventArgs) Handles lblProduksi.MouseEnter
        lblProduksi.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblProduksi_MouseLeave(sender As Object, e As EventArgs) Handles lblProduksi.MouseLeave
        lblProduksi.BackColor = Color.Transparent
    End Sub


    '========================================================
    ' HOVER lblNG
    '========================================================

    Private Sub lblNG_MouseEnter(sender As Object, e As EventArgs) Handles lblNG.MouseEnter
        lblNG.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblNG_MouseLeave(sender As Object, e As EventArgs) Handles lblNG.MouseLeave
        lblNG.BackColor = Color.Transparent
    End Sub


    '========================================================
    ' HOVER lblInput
    '========================================================

    Private Sub lblInput_MouseEnter(sender As Object, e As EventArgs) Handles lblInput.MouseEnter
        lblInput.ForeColor = Color.DarkOrange
    End Sub

    Private Sub lblInput_MouseLeave(sender As Object, e As EventArgs) Handles lblInput.MouseLeave
        lblInput.ForeColor = Color.White
    End Sub


    '========================================================
    ' HOVER lblRiwayat
    '========================================================

    Private Sub lblRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles lblRiwayat.MouseEnter
        lblRiwayat.ForeColor = Color.DarkOrange
    End Sub

    Private Sub lblRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles lblRiwayat.MouseLeave
        lblRiwayat.ForeColor = Color.White
    End Sub


    '========================================================
    ' HOVER BUTTON LOGOUT
    '========================================================

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

    '========================================================
    ' TUTUP KONEKSI
    '========================================================

    Private Sub TutupKoneksi()

        If CNN IsNot Nothing AndAlso CNN.State = ConnectionState.Open Then
            CNN.Close()
        End If

    End Sub


    '========================================================
    ' PANEL PAINT
    '========================================================

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

End Class