Imports System.Globalization
Imports System.Data.OleDb
Imports System.Drawing.Drawing2D

Public Class FormInputKualitas

    '========================================================
    ' FORM LOAD
    '========================================================
    Private Sub FormInputKualitas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Membuat tombol menjadi rounded
        BulatkanTombol(btnSimpan)
        BulatkanTombol(btnReset)

        'Tanggal otomatis hari ini
        dtpTanggal.Value = Date.Today

        'TextBox hasil dibuat ReadOnly
        txtTotalProduksi.ReadOnly = True
        txtPersentaseCacat.ReadOnly = True
        txtLevelSigma.ReadOnly = True
        txtJumlahNG.ReadOnly = True

        'Warna TextBox hasil
        txtTotalProduksi.BackColor = Color.LightGray
        txtPersentaseCacat.BackColor = Color.LightGray
        txtLevelSigma.BackColor = Color.LightGray
        txtJumlahNG.BackColor = Color.LightGray

        'ComboBox hanya bisa memilih
        cmbNamaProduk.DropDownStyle = ComboBoxStyle.DropDownList
        cmbJenisNG.DropDownStyle = ComboBoxStyle.DropDownList

        'Load data ComboBox
        LoadProduk()
        LoadJenisNG()

        'Kosongkan hasil
        cmbNamaProduk.SelectedIndex = -1
        cmbJenisNG.SelectedIndex = -1

        txtTotalProduksi.Clear()
        txtJumlahNG.Clear()
        txtPersentaseCacat.Clear()
        txtLevelSigma.Clear()

    End Sub


    '========================================================
    ' MEMBUAT TOMBOL BULAT
    '========================================================
    Private Sub BulatkanTombol(btn As Button)

        Dim path As New GraphicsPath()

        Dim radius As Integer = 20

        path.AddArc(
            0,
            0,
            radius,
            radius,
            180,
            90
        )

        path.AddArc(
            btn.Width - radius,
            0,
            radius,
            radius,
            270,
            90
        )

        path.AddArc(
            btn.Width - radius,
            btn.Height - radius,
            radius,
            radius,
            0,
            90
        )

        path.AddArc(
            0,
            btn.Height - radius,
            radius,
            radius,
            90,
            90
        )

        path.CloseAllFigures()

        btn.Region = New Region(path)

    End Sub


    '========================================================
    ' LOAD PRODUK
    '========================================================
    Private Sub LoadProduk()

        Try

            cmbNamaProduk.Items.Clear()

            Dim query As String =
                "SELECT ID_Produk, Nama_Produk " &
                "FROM Produk " &
                "ORDER BY Nama_Produk ASC"

            Using cmd As New OleDbCommand(query, CNN)

                If CNN.State = ConnectionState.Closed Then
                    CNN.Open()
                End If

                Using Rd As OleDbDataReader = cmd.ExecuteReader()

                    While Rd.Read()

                        Dim idProduk As Integer =
                            Convert.ToInt32(Rd("ID_Produk"))

                        Dim namaProduk As String =
                            Rd("Nama_Produk").ToString()

                        cmbNamaProduk.Items.Add(
                            New ProdukItem(idProduk, namaProduk)
                        )

                    End While

                End Using

            End Using

        Catch ex As Exception

            MessageBox.Show(
                "Gagal memuat data produk." &
                vbCrLf & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        Finally

            If CNN.State = ConnectionState.Open Then
                CNN.Close()
            End If

        End Try

    End Sub


    '========================================================
    ' LOAD JENIS NG
    '========================================================
    Private Sub LoadJenisNG()

        Try

            cmbJenisNG.Items.Clear()

            Dim query As String =
                "SELECT DISTINCT Jenis_NG " &
                "FROM Data_Produk_NG " &
                "WHERE Jenis_NG IS NOT NULL " &
                "ORDER BY Jenis_NG ASC"

            Using cmd As New OleDbCommand(query, CNN)

                If CNN.State = ConnectionState.Closed Then
                    CNN.Open()
                End If

                Using Rd As OleDbDataReader = cmd.ExecuteReader()

                    While Rd.Read()

                        cmbJenisNG.Items.Add(
                            Rd("Jenis_NG").ToString()
                        )

                    End While

                End Using

            End Using

        Catch ex As Exception

            MessageBox.Show(
                "Gagal memuat jenis NG." &
                vbCrLf & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        Finally

            If CNN.State = ConnectionState.Open Then
                CNN.Close()
            End If

        End Try

    End Sub


    '========================================================
    ' SAAT PRODUK DIPILIH
    '========================================================
    Private Sub cmbNamaProduk_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    ) Handles cmbNamaProduk.SelectedIndexChanged

        If cmbNamaProduk.SelectedIndex = -1 Then

            txtTotalProduksi.Clear()
            txtJumlahNG.Clear()
            txtPersentaseCacat.Clear()
            txtLevelSigma.Clear()

            Exit Sub

        End If

        Dim produk As ProdukItem =
            CType(cmbNamaProduk.SelectedItem, ProdukItem)

        'Ambil total produksi berdasarkan produk + tanggal
        AmbilTotalProduksi(
            produk.ID,
            dtpTanggal.Value.Date
        )

        'Kalau jenis NG sudah dipilih,
        'langsung ambil jumlah NG
        If cmbJenisNG.SelectedIndex <> -1 Then

            AmbilJumlahNG(
                produk.ID,
                dtpTanggal.Value.Date,
                cmbJenisNG.Text
            )

        End If

    End Sub


    '========================================================
    ' SAAT TANGGAL BERUBAH
    '========================================================
    Private Sub dtpTanggal_ValueChanged(
        sender As Object,
        e As EventArgs
    ) Handles dtpTanggal.ValueChanged

        If cmbNamaProduk.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim produk As ProdukItem =
            CType(cmbNamaProduk.SelectedItem, ProdukItem)

        'Ambil total produksi sesuai tanggal
        AmbilTotalProduksi(
            produk.ID,
            dtpTanggal.Value.Date
        )

        'Kalau jenis NG sudah dipilih
        If cmbJenisNG.SelectedIndex <> -1 Then

            AmbilJumlahNG(
                produk.ID,
                dtpTanggal.Value.Date,
                cmbJenisNG.Text
            )

        End If

    End Sub


    '========================================================
    ' AMBIL TOTAL PRODUKSI
    '========================================================
    Private Sub AmbilTotalProduksi(
        idProduk As Integer,
        tanggalProduksi As Date
    )

        Try

            txtTotalProduksi.Clear()
            txtJumlahNG.Clear()
            txtPersentaseCacat.Clear()
            txtLevelSigma.Clear()

            Dim tanggalBesok As Date =
                tanggalProduksi.Date.AddDays(1)

            Dim query As String =
                "SELECT TOP 1 ID_Total_Produk, Total_Produksi " &
                "FROM Data_Pengelolaan_Total_Produksi " &
                "WHERE ID_Produk = ? " &
                "AND Tanggal_Produksi >= ? " &
                "AND Tanggal_Produksi < ? " &
                "ORDER BY ID_Total_Produk DESC"

            Using cmd As New OleDbCommand(query, CNN)

                cmd.Parameters.AddWithValue(
                    "@ID_Produk",
                    idProduk
                )

                cmd.Parameters.AddWithValue(
                    "@TanggalAwal",
                    tanggalProduksi.Date
                )

                cmd.Parameters.AddWithValue(
                    "@TanggalBesok",
                    tanggalBesok
                )

                If CNN.State = ConnectionState.Closed Then
                    CNN.Open()
                End If

                Using Rd As OleDbDataReader =
                    cmd.ExecuteReader()

                    If Rd.Read() Then

                        txtTotalProduksi.Text =
                            Rd("Total_Produksi").ToString()

                    Else

                        txtTotalProduksi.Clear()
                        txtJumlahNG.Clear()
                        txtPersentaseCacat.Clear()
                        txtLevelSigma.Clear()

                    End If

                End Using

            End Using

            'Kalau jenis NG sudah dipilih,
            'ambil jumlah NG juga
            If cmbJenisNG.SelectedIndex <> -1 Then

                AmbilJumlahNG(
                    idProduk,
                    tanggalProduksi,
                    cmbJenisNG.Text
                )

            End If

        Catch ex As Exception

            MessageBox.Show(
                "Gagal mengambil total produksi." &
                vbCrLf & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        Finally

            If CNN.State = ConnectionState.Open Then
                CNN.Close()
            End If

        End Try

    End Sub


    '========================================================
    ' SAAT JENIS NG DIPILIH
    '========================================================
    Private Sub cmbJenisNG_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    ) Handles cmbJenisNG.SelectedIndexChanged

        If cmbNamaProduk.SelectedIndex = -1 Then
            Exit Sub
        End If

        If cmbJenisNG.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim produk As ProdukItem =
            CType(cmbNamaProduk.SelectedItem, ProdukItem)

        AmbilJumlahNG(
            produk.ID,
            dtpTanggal.Value.Date,
            cmbJenisNG.Text
        )

    End Sub


    '========================================================
    ' AMBIL JUMLAH NG
    '========================================================
    Private Sub AmbilJumlahNG(
        idProduk As Integer,
        tanggalProduksi As Date,
        jenisNG As String
    )

        Try

            txtJumlahNG.Clear()
            txtPersentaseCacat.Clear()
            txtLevelSigma.Clear()

            Dim idTotalProduk As Integer = 0

            Dim tanggalBesok As Date =
                tanggalProduksi.Date.AddDays(1)

            '-----------------------------------------------
            ' CARI ID TOTAL PRODUKSI
            '-----------------------------------------------
            Dim queryTotalProduksi As String =
                "SELECT TOP 1 ID_Total_Produk " &
                "FROM Data_Pengelolaan_Total_Produksi " &
                "WHERE ID_Produk = ? " &
                "AND Tanggal_Produksi >= ? " &
                "AND Tanggal_Produksi < ? " &
                "ORDER BY ID_Total_Produk DESC"

            Using cmdTotal As New OleDbCommand(
                queryTotalProduksi,
                CNN
            )

                cmdTotal.Parameters.AddWithValue(
                    "@ID_Produk",
                    idProduk
                )

                cmdTotal.Parameters.AddWithValue(
                    "@TanggalAwal",
                    tanggalProduksi.Date
                )

                cmdTotal.Parameters.AddWithValue(
                    "@TanggalBesok",
                    tanggalBesok
                )

                If CNN.State = ConnectionState.Closed Then
                    CNN.Open()
                End If

                Dim hasil As Object =
                    cmdTotal.ExecuteScalar()

                If hasil Is Nothing OrElse
                   hasil Is DBNull.Value Then

                    txtJumlahNG.Clear()
                    txtPersentaseCacat.Clear()
                    txtLevelSigma.Clear()

                    Exit Sub

                End If

                idTotalProduk =
                    Convert.ToInt32(hasil)

            End Using


            '-----------------------------------------------
            ' CARI JUMLAH NG
            '-----------------------------------------------
            Dim queryNG As String =
                "SELECT TOP 1 Jumlah_NG " &
                "FROM Data_Produk_NG " &
                "WHERE ID_Total_Produk = ? " &
                "AND Jenis_NG = ? " &
                "ORDER BY ID_NG DESC"

            Using cmdNG As New OleDbCommand(
                queryNG,
                CNN
            )

                cmdNG.Parameters.AddWithValue(
                    "@ID_Total_Produk",
                    idTotalProduk
                )

                cmdNG.Parameters.AddWithValue(
                    "@Jenis_NG",
                    jenisNG
                )

                If CNN.State = ConnectionState.Closed Then
                    CNN.Open()
                End If

                Dim hasilNG As Object =
                    cmdNG.ExecuteScalar()

                If hasilNG Is Nothing OrElse
                   hasilNG Is DBNull.Value Then

                    txtJumlahNG.Clear()
                    txtPersentaseCacat.Clear()
                    txtLevelSigma.Clear()

                Else

                    txtJumlahNG.Text =
                        hasilNG.ToString()

                    HitungKualitas()

                End If

            End Using

        Catch ex As Exception

            MessageBox.Show(
                "Gagal mengambil jumlah NG." &
                vbCrLf & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        Finally

            If CNN.State = ConnectionState.Open Then
                CNN.Close()
            End If

        End Try

    End Sub


    '========================================================
    ' JUMLAH NG BERUBAH
    '========================================================
    Private Sub txtJumlahNG_TextChanged(
        sender As Object,
        e As EventArgs
    ) Handles txtJumlahNG.TextChanged

        HitungKualitas()

    End Sub


    '========================================================
    ' HITUNG PERSENTASE CACAT + SIGMA
    '========================================================
    Private Sub HitungKualitas()

        Dim jumlahNG As Double
        Dim totalProduksi As Double

        'Kalau data belum lengkap
        If Not Double.TryParse(
            txtJumlahNG.Text,
            jumlahNG
        ) Then

            txtPersentaseCacat.Clear()
            txtLevelSigma.Clear()

            Exit Sub

        End If


        If Not Double.TryParse(
            txtTotalProduksi.Text,
            totalProduksi
        ) Then

            txtPersentaseCacat.Clear()
            txtLevelSigma.Clear()

            Exit Sub

        End If


        'Menghindari pembagian dengan 0
        If totalProduksi <= 0 Then

            txtPersentaseCacat.Clear()
            txtLevelSigma.Clear()

            Exit Sub

        End If


        '-----------------------------------------------
        ' PERSENTASE CACAT
        '-----------------------------------------------
        Dim persentaseCacat As Double =
            (jumlahNG / totalProduksi) * 100

        txtPersentaseCacat.Text =
            persentaseCacat.ToString("0.00") & " %"


        '-----------------------------------------------
        ' DPMO
        '-----------------------------------------------
        Dim dpmo As Double =
            (jumlahNG / totalProduksi) * 1000000


        '-----------------------------------------------
        ' LEVEL SIGMA
        '-----------------------------------------------
        Dim sigma As Double

        If dpmo <= 0 Then

            sigma = 6

        ElseIf dpmo >= 1000000 Then

            sigma = 0

        Else

            sigma =
                NormalInverse(
                    1 - (dpmo / 1000000)
                ) + 1.5

        End If


        'Batas Sigma 0 - 6
        If sigma < 0 Then
            sigma = 0
        End If

        If sigma > 6 Then
            sigma = 6
        End If


        txtLevelSigma.Text =
            sigma.ToString("0.00")

    End Sub


    '========================================================
    ' NORMAL INVERSE
    '========================================================
    Private Function NormalInverse(p As Double) As Double

        If p <= 0 OrElse p >= 1 Then

            If p <= 0 Then
                Return Double.NegativeInfinity
            Else
                Return Double.PositiveInfinity
            End If

        End If


        Dim a1 As Double =
            -39.6968302866538

        Dim a2 As Double =
            220.946098424521

        Dim a3 As Double =
            -275.928510446969

        Dim a4 As Double =
            138.357751867269

        Dim a5 As Double =
            -30.6647980661472

        Dim a6 As Double =
            2.50662827745924


        Dim b1 As Double =
            -54.4760987982241

        Dim b2 As Double =
            161.585836858041

        Dim b3 As Double =
            -155.698979859887

        Dim b4 As Double =
            66.8013118877197

        Dim b5 As Double =
            -13.2806815528857


        Dim c1 As Double =
            -0.00778489400243029

        Dim c2 As Double =
            -0.322396458041136

        Dim c3 As Double =
            -2.40075827716184

        Dim c4 As Double =
            -2.54973253934373

        Dim c5 As Double =
            4.37466414146497

        Dim c6 As Double =
            2.93816398269878


        Dim d1 As Double =
            0.00778469570904146

        Dim d2 As Double =
            0.32246712907004

        Dim d3 As Double =
            2.445134137143

        Dim d4 As Double =
            3.75440866190742


        Dim q As Double
        Dim r As Double


        If p < 0.02425 Then

            q = Math.Sqrt(
                -2 * Math.Log(p)
            )

            Return (
                ((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6
            ) /
            (
                (((d1 * q + d2) * q + d3) * q + d4) * q + 1
            )

        ElseIf p > 1 - 0.02425 Then

            q = Math.Sqrt(
                -2 * Math.Log(1 - p)
            )

            Return -(
                ((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6
            ) /
            (
                (((d1 * q + d2) * q + d3) * q + d4) * q + 1
            )

        Else

            q = p - 0.5

            r = q * q

            Return (
                (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q
            ) /
            (
                ((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1
            )

        End If

    End Function


    '========================================================
    ' GENERATE ID KUALITAS
    '========================================================
    Private Function GenerateIDKualitas() As String

        Dim nomorBaru As Integer = 1

        Try

            Dim query As String =
                "SELECT MAX(ID_Kualitas) FROM Data_Kualitas"

            Using cmd As New OleDbCommand(query, CNN)

                If CNN.State = ConnectionState.Closed Then
                    CNN.Open()
                End If

                Dim hasil As Object =
                    cmd.ExecuteScalar()

                If hasil IsNot Nothing AndAlso
                   hasil IsNot DBNull.Value Then

                    Dim idTerakhir As String =
                        hasil.ToString()

                    If idTerakhir.StartsWith("K") Then

                        Dim angka As Integer

                        If Integer.TryParse(
                            idTerakhir.Substring(1),
                            angka
                        ) Then

                            nomorBaru = angka + 1

                        End If

                    End If

                End If

            End Using

        Catch ex As Exception

            nomorBaru = 1

        Finally

            If CNN.State = ConnectionState.Open Then
                CNN.Close()
            End If

        End Try


        Return "K" & nomorBaru.ToString("000")

    End Function


    '========================================================
    ' SIMPAN DATA KUALITAS
    '========================================================
    Private Sub btnSimpan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnSimpan.Click

        'Validasi produk
        If cmbNamaProduk.SelectedIndex = -1 Then

            MessageBox.Show(
                "Silakan pilih produk terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        'Validasi jenis NG
        If cmbJenisNG.SelectedIndex = -1 Then

            MessageBox.Show(
                "Silakan pilih jenis NG terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        'Validasi total produksi
        If String.IsNullOrWhiteSpace(
            txtTotalProduksi.Text
        ) Then

            MessageBox.Show(
                "Data total produksi untuk tanggal dan produk tersebut tidak ditemukan.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        'Validasi jumlah NG
        If String.IsNullOrWhiteSpace(
            txtJumlahNG.Text
        ) Then

            MessageBox.Show(
                "Data jumlah NG untuk jenis NG tersebut tidak ditemukan.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        Try

            Dim produk As ProdukItem =
                CType(cmbNamaProduk.SelectedItem, ProdukItem)

            Dim tanggalProduksi As Date =
                dtpTanggal.Value.Date

            Dim jenisNG As String =
                cmbJenisNG.Text


            '================================================
            ' CARI ID TOTAL PRODUKSI
            '================================================
            Dim idTotalProduk As Integer = 0

            Dim tanggalBesok As Date =
                tanggalProduksi.AddDays(1)

            Dim queryTotal As String =
                "SELECT TOP 1 ID_Total_Produk " &
                "FROM Data_Pengelolaan_Total_Produksi " &
                "WHERE ID_Produk = ? " &
                "AND Tanggal_Produksi >= ? " &
                "AND Tanggal_Produksi < ? " &
                "ORDER BY ID_Total_Produk DESC"


            If CNN.State = ConnectionState.Closed Then
                CNN.Open()
            End If


            Using cmdTotal As New OleDbCommand(
                queryTotal,
                CNN
            )

                cmdTotal.Parameters.AddWithValue(
                    "@ID_Produk",
                    produk.ID
                )

                cmdTotal.Parameters.AddWithValue(
                    "@TanggalAwal",
                    tanggalProduksi
                )

                cmdTotal.Parameters.AddWithValue(
                    "@TanggalBesok",
                    tanggalBesok
                )

                Dim hasilTotal As Object =
                    cmdTotal.ExecuteScalar()

                If hasilTotal Is Nothing OrElse
                   hasilTotal Is DBNull.Value Then

                    MessageBox.Show(
                        "Data total produksi tidak ditemukan.",
                        "Peringatan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    Exit Sub

                End If

                idTotalProduk =
                    Convert.ToInt32(hasilTotal)

            End Using


            '================================================
            ' CARI ID NG
            '================================================
            Dim idNG As Integer = 0

            Dim queryNG As String =
                "SELECT TOP 1 ID_NG " &
                "FROM Data_Produk_NG " &
                "WHERE ID_Total_Produk = ? " &
                "AND Jenis_NG = ? " &
                "ORDER BY ID_NG DESC"


            Using cmdNG As New OleDbCommand(
                queryNG,
                CNN
            )

                cmdNG.Parameters.AddWithValue(
                    "@ID_Total_Produk",
                    idTotalProduk
                )

                cmdNG.Parameters.AddWithValue(
                    "@Jenis_NG",
                    jenisNG
                )

                Dim hasilNG As Object =
                    cmdNG.ExecuteScalar()

                If hasilNG Is Nothing OrElse
                   hasilNG Is DBNull.Value Then

                    MessageBox.Show(
                        "Data jenis NG tidak ditemukan.",
                        "Peringatan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    Exit Sub

                End If

                idNG =
                    Convert.ToInt32(hasilNG)

            End Using


            '================================================
            ' GENERATE ID KUALITAS
            '================================================
            Dim idKualitas As String =
                GenerateIDKualitas()


            '================================================
            ' SIMPAN DATA
            '================================================
            Dim querySimpan As String =
                "INSERT INTO Data_Kualitas " &
                "(ID_Kualitas, No_ID, Level_Sigma, ID_NG, ID_Total_Produk) " &
                "VALUES (?, ?, ?, ?, ?)"


            Using cmdSimpan As New OleDbCommand(
                querySimpan,
                CNN
            )

                cmdSimpan.Parameters.AddWithValue(
                    "@ID_Kualitas",
                    idKualitas
                )

                'Tetap menggunakan A seperti kode sebelumnya
                cmdSimpan.Parameters.AddWithValue(
                    "@No_ID",
                    A
                )

                cmdSimpan.Parameters.AddWithValue(
                    "@Level_Sigma",
                    txtLevelSigma.Text
                )

                cmdSimpan.Parameters.AddWithValue(
                    "@ID_NG",
                    idNG
                )

                cmdSimpan.Parameters.AddWithValue(
                    "@ID_Total_Produk",
                    idTotalProduk
                )


                Dim hasil As Integer =
                    cmdSimpan.ExecuteNonQuery()


                If hasil > 0 Then

                    MessageBox.Show(
                        "Data kualitas berhasil disimpan.",
                        "Berhasil",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    )

                    ResetForm()

                End If

            End Using


        Catch ex As Exception

            MessageBox.Show(
                "Data kualitas gagal disimpan." &
                vbCrLf & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        Finally

            If CNN.State = ConnectionState.Open Then
                CNN.Close()
            End If

        End Try

    End Sub


    '========================================================
    ' RESET FORM
    '========================================================
    Private Sub btnReset_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnReset.Click

        ResetForm()

    End Sub


    Private Sub ResetForm()

        dtpTanggal.Value = Date.Today

        cmbNamaProduk.SelectedIndex = -1
        cmbJenisNG.SelectedIndex = -1

        txtTotalProduksi.Clear()
        txtJumlahNG.Clear()
        txtPersentaseCacat.Clear()
        txtLevelSigma.Clear()

    End Sub

    Private Sub lblDashboardKualitas_Click(
        sender As Object,
        e As EventArgs
    ) Handles lblDashboardKualitas.Click

        FormDashboardKualitas.Show()
        Me.Hide()

    End Sub

    Private Sub lblProduksiKualitas_Click(
        sender As Object,
        e As EventArgs
    ) Handles lblProduksiKualitas.Click

        FormProduksi.Show()
        Me.Hide()

    End Sub

End Class