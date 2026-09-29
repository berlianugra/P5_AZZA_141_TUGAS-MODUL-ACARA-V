Imports System.Globalization
Imports System.Data.OleDb
Imports System.Drawing.Drawing2D

Public Class FormInputKualitas

    '========================================================
    ' FORM LOAD
    '========================================================
    Private Sub FormInputKualitas_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        RoundedButton(btnSimpan, 15)
        RoundedButton(btnReset, 15)

        'Tanggal awal = hari ini
        dtpTanggal.Value = Date.Today

        'Textbox hasil tidak dapat diketik manual
        txtTotalProduksi.ReadOnly = True
        txtPersentaseCacat.ReadOnly = True
        txtLevelSigma.ReadOnly = True
        txtJumlahNG.ReadOnly = True

        'Warna textbox hasil
        txtJumlahNG.BackColor = Color.LightGray
        txtTotalProduksi.BackColor = Color.LightGray
        txtPersentaseCacat.BackColor = Color.LightGray
        txtLevelSigma.BackColor = Color.LightGray

        'ComboBox tidak bisa diketik manual
        cmbNamaProduk.DropDownStyle = ComboBoxStyle.DropDownList
        cmbJenisNG.DropDownStyle = ComboBoxStyle.DropDownList

        'Load data dari database
        LoadProduk()
        LoadJenisNG()

        'Awal kosong
        cmbNamaProduk.SelectedIndex = -1
        cmbJenisNG.SelectedIndex = -1

        txtTotalProduksi.Clear()
        txtJumlahNG.Clear()
        txtPersentaseCacat.Clear()
        txtLevelSigma.Clear()

    End Sub


    '========================================================
    ' ROUNDED BUTTON
    '========================================================
    Private Sub RoundedButton(btn As Button, radius As Integer)

        Dim path As New GraphicsPath()

        Dim d As Integer = radius * 2

        path.StartFigure()

        path.AddArc(
            New Rectangle(0, 0, d, d),
            180,
            90
        )

        path.AddArc(
            New Rectangle(btn.Width - d, 0, d, d),
            270,
            90
        )

        path.AddArc(
            New Rectangle(
                btn.Width - d,
                btn.Height - d,
                d,
                d
            ),
            0,
            90
        )

        path.AddArc(
            New Rectangle(
                0,
                btn.Height - d,
                d,
                d
            ),
            90,
            90
        )

        path.CloseFigure()

        btn.Region = New Region(path)

    End Sub


    '========================================================
    ' LOAD PRODUK
    '========================================================
    Private Sub LoadProduk()

        Try

            Koneksi()

            cmbNamaProduk.Items.Clear()

            Dim query As String =
                "SELECT ID_Produk, Nama_Produk " &
                "FROM Produk " &
                "ORDER BY Nama_Produk ASC"

            cmd = New OleDbCommand(query, CNN)

            Rd = cmd.ExecuteReader()

            While Rd.Read()

                cmbNamaProduk.Items.Add(
                    New ProdukItem(
                        Rd("ID_Produk").ToString(),
                        Rd("Nama_Produk").ToString()
                    )
                )

            End While

            Rd.Close()
            Rd = Nothing

            CNN.Close()

            cmbNamaProduk.SelectedIndex = -1

        Catch ex As Exception

            If Rd IsNot Nothing AndAlso
               Not Rd.IsClosed Then

                Rd.Close()

            End If

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If

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


    '========================================================
    ' LOAD JENIS NG
    '========================================================
    Private Sub LoadJenisNG()

        Try

            Koneksi()

            cmbJenisNG.Items.Clear()

            Dim query As String =
                "SELECT DISTINCT Jenis_NG " &
                "FROM Data_Produk_NG " &
                "WHERE Jenis_NG IS NOT NULL " &
                "ORDER BY Jenis_NG ASC"

            cmd = New OleDbCommand(query, CNN)

            Rd = cmd.ExecuteReader()

            While Rd.Read()

                cmbJenisNG.Items.Add(
                    Rd("Jenis_NG").ToString()
                )

            End While

            Rd.Close()
            Rd = Nothing

            CNN.Close()

            cmbJenisNG.SelectedIndex = -1

        Catch ex As Exception

            If Rd IsNot Nothing AndAlso
               Not Rd.IsClosed Then

                Rd.Close()

            End If

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If

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


    '========================================================
    ' PRODUK DIPILIH
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

        'Tanggal TIDAK diubah.
        'Tanggal yang dipilih user digunakan untuk mencari produksi.
        AmbilTotalProduksi(
            produk.ID,
            dtpTanggal.Value.Date
        )

        'Kalau jenis NG sudah dipilih,
        'ambil kembali jumlah NG sesuai produk + tanggal + jenis NG
        If cmbJenisNG.SelectedIndex <> -1 Then

            AmbilJumlahNG(
                produk.ID,
                dtpTanggal.Value.Date,
                cmbJenisNG.Text
            )

        End If

    End Sub


    '========================================================
    ' TANGGAL PRODUKSI BERUBAH
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

        'Tanggal hanya menjadi filter pencarian.
        'Tanggal TIDAK diganti oleh database.
        AmbilTotalProduksi(
            produk.ID,
            dtpTanggal.Value.Date
        )

        If cmbJenisNG.SelectedIndex <> -1 Then

            AmbilJumlahNG(
                produk.ID,
                dtpTanggal.Value.Date,
                cmbJenisNG.Text
            )

        End If

    End Sub


    '========================================================
    ' AMBIL TOTAL PRODUKSI BERDASARKAN
    ' PRODUK + TANGGAL
    '========================================================
    Private Sub AmbilTotalProduksi(
        idProduk As String,
        tanggalProduksi As Date
    )

        Try

            Koneksi()

            Dim query As String =
                "SELECT TOP 1 ID_Total_Produk, Total_Produksi " &
                "FROM Data_Pengelolaan_Total_Produksi " &
                "WHERE ID_Produk = ? " &
                "AND Tanggal_Produksi = ? " &
                "ORDER BY ID_Total_Produk DESC"

            cmd = New OleDbCommand(query, CNN)

            cmd.Parameters.AddWithValue(
                "@ID_Produk",
                idProduk
            )

            cmd.Parameters.AddWithValue(
                "@Tanggal_Produksi",
                tanggalProduksi
            )

            Rd = cmd.ExecuteReader()

            If Rd.Read() Then

                txtTotalProduksi.Text =
                    Rd("Total_Produksi").ToString()

            Else

                txtTotalProduksi.Clear()
                txtJumlahNG.Clear()
                txtPersentaseCacat.Clear()
                txtLevelSigma.Clear()

            End If

            Rd.Close()
            Rd = Nothing

            CNN.Close()

        Catch ex As Exception

            If Rd IsNot Nothing AndAlso
               Not Rd.IsClosed Then

                Rd.Close()

            End If

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If

            MessageBox.Show(
                "Gagal mengambil total produksi." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' JENIS NG DIPILIH
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
    ' BERDASARKAN PRODUK + TANGGAL + JENIS NG
    '========================================================
    Private Sub AmbilJumlahNG(
        idProduk As String,
        tanggalProduksi As Date,
        jenisNG As String
    )

        Try

            Koneksi()

            'Cari ID Total Produksi sesuai produk + tanggal
            Dim queryTotal As String =
                "SELECT TOP 1 ID_Total_Produk " &
                "FROM Data_Pengelolaan_Total_Produksi " &
                "WHERE ID_Produk = ? " &
                "AND Tanggal_Produksi = ? " &
                "ORDER BY ID_Total_Produk DESC"

            cmd = New OleDbCommand(queryTotal, CNN)

            cmd.Parameters.AddWithValue(
                "@ID_Produk",
                idProduk
            )

            cmd.Parameters.AddWithValue(
                "@Tanggal_Produksi",
                tanggalProduksi
            )

            Dim idTotalProduk As Object =
                cmd.ExecuteScalar()

            If idTotalProduk Is Nothing OrElse
               IsDBNull(idTotalProduk) Then

                txtJumlahNG.Clear()
                txtPersentaseCacat.Clear()
                txtLevelSigma.Clear()

                CNN.Close()

                Exit Sub

            End If


            'Cari jumlah NG
            Dim queryNG As String =
                "SELECT TOP 1 Jumlah_NG " &
                "FROM Data_Produk_NG " &
                "WHERE ID_Total_Produk = ? " &
                "AND Jenis_NG = ? " &
                "ORDER BY ID_NG DESC"

            cmd = New OleDbCommand(queryNG, CNN)

            cmd.Parameters.AddWithValue(
                "@ID_Total_Produk",
                idTotalProduk.ToString()
            )

            cmd.Parameters.AddWithValue(
                "@Jenis_NG",
                jenisNG
            )

            Dim jumlahNG As Object =
                cmd.ExecuteScalar()

            If jumlahNG IsNot Nothing AndAlso
               Not IsDBNull(jumlahNG) Then

                txtJumlahNG.Text =
                    jumlahNG.ToString()

            Else

                txtJumlahNG.Clear()
                txtPersentaseCacat.Clear()
                txtLevelSigma.Clear()

            End If

            CNN.Close()

            HitungKualitas()

        Catch ex As Exception

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If

            MessageBox.Show(
                "Gagal mengambil jumlah NG." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' HITUNG KUALITAS
    '========================================================
    Private Sub txtJumlahNG_TextChanged(
        sender As Object,
        e As EventArgs
    ) Handles txtJumlahNG.TextChanged

        HitungKualitas()

    End Sub


    Private Sub HitungKualitas()

        Dim jumlahNG As Double
        Dim totalProduksi As Double

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

        If totalProduksi <= 0 Then

            txtPersentaseCacat.Clear()
            txtLevelSigma.Clear()

            Exit Sub

        End If

        If jumlahNG < 0 Then

            txtPersentaseCacat.Clear()
            txtLevelSigma.Clear()

            Exit Sub

        End If


        'Persentase cacat
        Dim persentaseCacat As Double =
            (jumlahNG / totalProduksi) * 100

        txtPersentaseCacat.Text =
            persentaseCacat.ToString("0.00") & " %"


        'DPMO
        Dim dpmo As Double =
            (jumlahNG / totalProduksi) * 1000000


        'Level Sigma
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


        If sigma < 0 Then sigma = 0
        If sigma > 6 Then sigma = 6

        txtLevelSigma.Text =
            sigma.ToString("0.00")

    End Sub


    '========================================================
    ' NORMAL INVERSE
    '========================================================
    Private Function NormalInverse(p As Double) As Double

        If p <= 0 OrElse p >= 1 Then
            Throw New ArgumentOutOfRangeException()
        End If

        Dim a1 As Double = -39.6968302866538
        Dim a2 As Double = 220.946098424521
        Dim a3 As Double = -275.928510446969
        Dim a4 As Double = 138.357751867269
        Dim a5 As Double = -30.6647980661472
        Dim a6 As Double = 2.50662827745924

        Dim b1 As Double = -54.4760987982241
        Dim b2 As Double = 161.585836858041
        Dim b3 As Double = -155.698979859888
        Dim b4 As Double = 66.8013118877197
        Dim b5 As Double = -13.2806815528857

        Dim c1 As Double = -0.00778489400243029
        Dim c2 As Double = -0.322396458041136
        Dim c3 As Double = -2.40075827716184
        Dim c4 As Double = -2.54973253934373
        Dim c5 As Double = 4.37466414146497
        Dim c6 As Double = 2.93816398269878

        Dim d1 As Double = 0.00778469570904146
        Dim d2 As Double = 0.32246712907004
        Dim d3 As Double = 2.445134137143
        Dim d4 As Double = 3.75440866190742

        Dim q As Double
        Dim r As Double

        If p < 0.02425 Then

            q = Math.Sqrt(-2 * Math.Log(p))

            Return (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) /
                   ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)

        ElseIf p > 1 - 0.02425 Then

            q = Math.Sqrt(-2 * Math.Log(1 - p))

            Return -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) /
                    ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)

        Else

            q = p - 0.5
            r = q * q

            Return (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q /
                   (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1)

        End If

    End Function


    '========================================================
    ' GENERATE ID KUALITAS
    '========================================================
    Private Function GenerateIDKualitas() As String

        Try

            If CNN Is Nothing OrElse
               CNN.State <> ConnectionState.Open Then

                Koneksi()

            End If

            Dim query As String =
                "SELECT MAX(Val(Mid(ID_Kualitas, 2))) " &
                "FROM Data_Kualitas"

            cmd = New OleDbCommand(query, CNN)

            Dim hasil As Object =
                cmd.ExecuteScalar()

            If hasil Is Nothing OrElse
               IsDBNull(hasil) Then

                Return "K001"

            End If

            Dim nomorTerakhir As Integer =
                CInt(hasil) + 1

            Return "K" &
                nomorTerakhir.ToString("D3")

        Catch ex As Exception

            MessageBox.Show(
                "Gagal membuat ID Kualitas." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

            Return ""

        End Try

    End Function


    '========================================================
    ' SIMPAN DATA KUALITAS
    '========================================================
    Private Sub SimpanDataKualitas()

        Try

            Koneksi()

            Dim produk As ProdukItem =
                CType(cmbNamaProduk.SelectedItem, ProdukItem)


            'Cari ID Total Produksi
            Dim queryTotal As String =
                "SELECT TOP 1 ID_Total_Produk " &
                "FROM Data_Pengelolaan_Total_Produksi " &
                "WHERE ID_Produk = ? " &
                "AND Tanggal_Produksi = ? " &
                "ORDER BY ID_Total_Produk DESC"

            cmd = New OleDbCommand(
                queryTotal,
                CNN
            )

            cmd.Parameters.AddWithValue(
                "@ID_Produk",
                produk.ID
            )

            cmd.Parameters.AddWithValue(
                "@Tanggal_Produksi",
                dtpTanggal.Value.Date
            )

            Dim idTotalProduk As Object =
                cmd.ExecuteScalar()

            If idTotalProduk Is Nothing OrElse
               IsDBNull(idTotalProduk) Then

                CNN.Close()

                MessageBox.Show(
                    "Data total produksi untuk produk dan tanggal tersebut tidak ditemukan.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                Exit Sub

            End If


            'Cari ID NG
            Dim queryNG As String =
                "SELECT TOP 1 ID_NG " &
                "FROM Data_Produk_NG " &
                "WHERE ID_Total_Produk = ? " &
                "AND Jenis_NG = ? " &
                "ORDER BY ID_NG DESC"

            cmd = New OleDbCommand(
                queryNG,
                CNN
            )

            cmd.Parameters.AddWithValue(
                "@ID_Total_Produk",
                idTotalProduk.ToString()
            )

            cmd.Parameters.AddWithValue(
                "@Jenis_NG",
                cmbJenisNG.Text
            )

            Dim idNG As Object =
                cmd.ExecuteScalar()

            If idNG Is Nothing OrElse
               IsDBNull(idNG) Then

                CNN.Close()

                MessageBox.Show(
                    "Data jenis NG tidak ditemukan.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                Exit Sub

            End If


            'Generate ID Kualitas
            Dim idKualitas As String =
                GenerateIDKualitas()

            If idKualitas = "" Then

                CNN.Close()

                Exit Sub

            End If


            'Insert
            Dim queryInsert As String =
                "INSERT INTO Data_Kualitas " &
                "(ID_Kualitas, No_ID, Level_Sigma, ID_NG, ID_Total_Produk) " &
                "VALUES (?, ?, ?, ?, ?)"

            cmd = New OleDbCommand(
                queryInsert,
                CNN
            )

            cmd.Parameters.AddWithValue(
                "@ID_Kualitas",
                idKualitas
            )

            cmd.Parameters.AddWithValue(
                "@No_ID",
                A
            )

            cmd.Parameters.AddWithValue(
                "@Level_Sigma",
                CDbl(txtLevelSigma.Text)
            )

            cmd.Parameters.AddWithValue(
                "@ID_NG",
                idNG.ToString()
            )

            cmd.Parameters.AddWithValue(
                "@ID_Total_Produk",
                idTotalProduk.ToString()
            )

            cmd.ExecuteNonQuery()

            CNN.Close()


            MessageBox.Show(
                "Data kualitas berhasil disimpan ke database." &
                vbCrLf & vbCrLf &
                "ID Kualitas : " & idKualitas,
                "Berhasil",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            ResetForm()

        Catch ex As Exception

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If

            MessageBox.Show(
                "Data gagal disimpan ke database." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' RESET
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

        txtJumlahNG.Clear()
        txtTotalProduksi.Clear()
        txtPersentaseCacat.Clear()
        txtLevelSigma.Clear()

        cmbNamaProduk.Focus()

    End Sub


    '========================================================
    ' SIMPAN
    '========================================================
    Private Sub btnSimpan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnSimpan.Click

        If cmbNamaProduk.SelectedIndex = -1 Then

            MessageBox.Show(
                "Silakan pilih nama produk.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cmbNamaProduk.Focus()

            Exit Sub

        End If


        If cmbJenisNG.SelectedIndex = -1 Then

            MessageBox.Show(
                "Silakan pilih jenis NG.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cmbJenisNG.Focus()

            Exit Sub

        End If


        Dim jumlahNG As Double

        If Not Double.TryParse(
            txtJumlahNG.Text,
            jumlahNG
        ) Then

            MessageBox.Show(
                "Jumlah NG belum tersedia.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        Dim totalProduksi As Double

        If Not Double.TryParse(
            txtTotalProduksi.Text,
            totalProduksi
        ) Then

            MessageBox.Show(
                "Total produksi belum tersedia.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If jumlahNG < 0 Then

            MessageBox.Show(
                "Jumlah NG tidak boleh kurang dari 0.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If jumlahNG > totalProduksi Then

            MessageBox.Show(
                "Jumlah NG tidak boleh lebih besar dari total produksi.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        Dim pesan As String =
            "Data kualitas akan disimpan." &
            vbCrLf & vbCrLf &
            "Tanggal          : " &
            dtpTanggal.Value.ToString("dd/MM/yyyy") &
            vbCrLf &
            "Nama Produk      : " &
            cmbNamaProduk.Text &
            vbCrLf &
            "Jenis NG         : " &
            cmbJenisNG.Text &
            vbCrLf &
            "Jumlah NG        : " &
            txtJumlahNG.Text &
            vbCrLf &
            "Total Produksi   : " &
            txtTotalProduksi.Text &
            vbCrLf &
            "Persentase Cacat : " &
            txtPersentaseCacat.Text &
            vbCrLf &
            "Level Sigma      : " &
            txtLevelSigma.Text


        Dim hasil As DialogResult =
            MessageBox.Show(
                pesan,
                "Konfirmasi Simpan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )


        If hasil = DialogResult.Yes Then

            SimpanDataKualitas()

        End If

    End Sub


    '========================================================
    ' KEMBALI KE DASHBOARD
    '========================================================
    Private Sub Label1_Click(
        sender As Object,
        e As EventArgs
    ) Handles lblDashboardKualitas.Click

        FormDashboardKualitas.Show()
        Me.Hide()

    End Sub


    Private Sub Label19_Click(
        sender As Object,
        e As EventArgs
    ) Handles lblProduksiKualitas.Click

        FormDashboardKualitas.Show()
        Me.Hide()

    End Sub
    Private produksiTerbuka As Boolean = False
    Private ukuranAwalbtnLogout As Size
    Private posisiAwalbtnLogout As Point

    Private Sub FormDashboardKualitas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Simpan ukuran dan posisi awal tombol Logout
        ukuranAwalbtnLogout = btnLogoutIK.Size
        posisiAwalbtnLogout = btnLogoutIK.Location

        '========================================
        ' KONDISI AWAL
        '========================================

        produksiTerbuka = False

        lblKualitas.Visible = True

        lblInputKualitas.Visible = False
        lblRiwayatKualitas.Visible = False

        'Warna awal
        lblKualitas.BackColor = Color.Transparent
        lblInputKualitas.BackColor = Color.Transparent
        lblRiwayatKualitas.BackColor = Color.Transparent

        'Font awal
        lblKualitas.Font =
            New Font(lblKualitas.Font, FontStyle.Regular)

        'Pastikan Kualitas berada di depan
        lblKualitas.BringToFront()
    End Sub

    Private Sub lblKualitas_Click(sender As Object, e As EventArgs) Handles lblKualitas.Click

        If produksiTerbuka = False Then

            produksiTerbuka = True

            'Tampilkan submenu
            lblInputKualitas.Visible = True
            lblRiwayatKualitas.Visible = True

            'Menu Kualitas aktif
            lblKualitas.BackColor =
            Color.FromArgb(45, 99, 181)

            lblKualitas.Font =
            New Font(lblKualitas.Font, FontStyle.Bold)

            'Pastikan tampil di depan
            lblInputKualitas.BringToFront()
            lblRiwayatKualitas.BringToFront()
            lblKualitas.BringToFront()

        Else

            '========================================
            ' JIKA MENU SUDAH TERBUKA
            ' MAKA TUTUP
            '========================================

            produksiTerbuka = False

            'Sembunyikan submenu
            lblInputKualitas.Visible = False
            lblRiwayatKualitas.Visible = False

            'Kembalikan Kualitas
            lblKualitas.BackColor = Color.Transparent

            lblKualitas.Font =
            New Font(lblKualitas.Font, FontStyle.Regular)

        End If

    End Sub



    Private Sub lblProduksiKualitas_Click(sender As Object, e As EventArgs) Handles lblProduksiKualitas.Click
        Dim formProduksiKualitas As New FormInputKualitas()
        formProduksiKualitas.Show()
        Me.Hide()
    End Sub


    'Riwayat Produksi
    Private Sub lblInputKualitas_Click(sender As Object, e As EventArgs) Handles lblRiwayatKualitas.Click, lblInputKualitas.Click

        Dim FormInputKualitas As New FormInputKualitas()
        FormInputKualitas.Show()
        Me.Hide()

    End Sub

    'PictureBox Riwayat Produksi
    Private Sub lblNG_Click(sender As Object, e As EventArgs) Handles lblNGKualitas.Click

        Dim FormProdukNG As New formRiwayatKualitas()
        FormProdukNG.Show()
        Me.Hide()

    End Sub

    'Hover Input
    Private Sub lblInput_MouseEnter(sender As Object, e As EventArgs) Handles lblInputKualitas.MouseEnter

        lblInputKualitas.BackColor = Color.DarkOrange

    End Sub

    Private Sub lblInput_MouseLeave(sender As Object, e As EventArgs) Handles lblInputKualitas.MouseLeave

        lblInputKualitas.BackColor = Color.Transparent

    End Sub

    'Hover Riwayat
    Private Sub lblRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles lblRiwayatKualitas.MouseEnter

        lblRiwayatKualitas.BackColor = Color.DarkOrange

    End Sub

    Private Sub lblRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles lblRiwayatKualitas.MouseLeave

        lblRiwayatKualitas.BackColor = Color.Transparent

    End Sub

    'Hover Dashboard
    Private Sub lblDashboard_MouseEnter(sender As Object, e As EventArgs) Handles lblDashboardKualitas.MouseEnter

        lblDashboardKualitas.BackColor = Color.FromArgb(45, 99, 181)

    End Sub

    Private Sub lblDashboard_MouseLeave(sender As Object, e As EventArgs) Handles lblDashboardKualitas.MouseLeave

        lblDashboardKualitas.BackColor = Color.Transparent

    End Sub

    'Hover Produksi
    Private Sub lblProduksi_MouseEnter(sender As Object, e As EventArgs) Handles lblKualitas.MouseEnter

        lblKualitas.BackColor = Color.FromArgb(45, 99, 181)

    End Sub

    Private Sub lblProduksi_MouseLeave(sender As Object, e As EventArgs) Handles lblKualitas.MouseLeave

        lblKualitas.BackColor = Color.Transparent

    End Sub

    'Hover NG
    Private Sub lblNG_MouseEnter(sender As Object, e As EventArgs) Handles lblNGKualitas.MouseEnter

        lblNGKualitas.BackColor = Color.FromArgb(45, 99, 181)

    End Sub

    Private Sub lblNG_MouseLeave(sender As Object, e As EventArgs) Handles lblNGKualitas.MouseLeave

        lblNGKualitas.BackColor = Color.Transparent

    End Sub


    'Pop-up PictureBox Input
    Private Sub btnLogout_MouseEnter(sender As Object, e As EventArgs) Handles btnLogoutIK.MouseEnter

        btnLogoutIK.Size = New Size(
            ukuranAwalbtnLogout.Width + 8,
            ukuranAwalbtnLogout.Height + 8
        )

        btnLogoutIK.Location = New Point(
            posisiAwalbtnLogout.X - 4,
            posisiAwalbtnLogout.Y - 4
        )

    End Sub

    Private Sub btnLogout_MouseLeave(sender As Object, e As EventArgs) Handles btnLogoutIK.MouseLeave

        btnLogoutIK.Size = ukuranAwalbtnLogout
        btnLogoutIK.Location = posisiAwalbtnLogout

    End Sub

    Private Sub lblProduksiKualitas_MouseEnter(sender As Object, e As EventArgs) Handles lblProduksiKualitas.MouseEnter
        lblProduksiKualitas.BackColor = Color.FromArgb(45, 99, 181)
    End Sub

    Private Sub lblProduksiKualitas_MouseLeave(sender As Object, e As EventArgs) Handles lblProduksiKualitas.MouseLeave
        lblProduksiKualitas.BackColor = Color.Transparent
    End Sub

    Private Sub btnLogoutIK_Click(sender As Object, e As EventArgs) Handles btnLogoutIK.Click

        Dim hasil As DialogResult

        hasil = MessageBox.Show(
        "Apakah kamu yakin ingin logout?",
        "Konfirmasi Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If hasil = DialogResult.Yes Then

            'Buka Form Login
            FormLogin.Show()

            'Tutup/sembunyikan dashboard
            Me.Hide()

        Else

            'Tetap di dashboard
            Me.Show()

        End If

    End Sub
End Class