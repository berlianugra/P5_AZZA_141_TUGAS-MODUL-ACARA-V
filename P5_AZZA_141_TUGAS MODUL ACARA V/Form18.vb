Imports System.Data.OleDb

Public Class FormInputUsulan

    '========================================================
    ' VARIABEL DATA AKTIF
    '========================================================

    Private usulanSudahDitambahkan As Boolean = False

    Private idTotalProdukAktif As String = ""
    Private idNGAktif As String = ""
    Private idKualitasAktif As String = ""
    Private noIDAktif As String = ""
    Private idKerugianAktif As String = ""


    '========================================================
    ' FORM LOAD
    '========================================================

    Private Sub FormInputUsulan_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        dtpTanggal.Value = DateTime.Now

        txtTotalProduksi.ReadOnly = True
        txtJumlahNG.ReadOnly = True

        txtTotalProduksi.BackColor = Color.LightGray
        txtJumlahNG.BackColor = Color.LightGray

        LoadProduk()
        LoadJenisNG()

    End Sub


    '========================================================
    ' LOAD PRODUK
    '========================================================

    Private Sub LoadProduk()

        Try

            Koneksi()

            cmbNamaProduk.Items.Clear()

            Dim query As String =
                "SELECT [ID_Produk], [Nama_Produk] " &
                "FROM [Produk] " &
                "ORDER BY [Nama_Produk]"

            Using cmdProduk As New OleDbCommand(
                query,
                CNN
            )

                Using rdProduk As OleDbDataReader =
                    cmdProduk.ExecuteReader()

                    While rdProduk.Read()

                        cmbNamaProduk.Items.Add(
                            New ProdukItem(
                                rdProduk("ID_Produk").ToString(),
                                rdProduk("Nama_Produk").ToString()
                            )
                        )

                    End While

                End Using

            End Using

            CNN.Close()

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mengambil data produk." &
                vbCrLf &
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
                "SELECT DISTINCT [Jenis_NG] " &
                "FROM [Data_Produk_NG] " &
                "WHERE [Jenis_NG] IS NOT NULL " &
                "ORDER BY [Jenis_NG]"

            Using cmdJenis As New OleDbCommand(
                query,
                CNN
            )

                Using rdJenis As OleDbDataReader =
                    cmdJenis.ExecuteReader()

                    While rdJenis.Read()

                        cmbJenisNG.Items.Add(
                            rdJenis("Jenis_NG").ToString()
                        )

                    End While

                End Using

            End Using

            CNN.Close()

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mengambil jenis NG." &
                vbCrLf &
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
            Exit Sub
        End If

        Dim produk As ProdukItem =
            TryCast(
                cmbNamaProduk.SelectedItem,
                ProdukItem
            )

        If produk Is Nothing Then
            Exit Sub
        End If


        '--------------------------------------------
        ' Reset data aktif
        '--------------------------------------------

        idTotalProdukAktif = ""
        idNGAktif = ""
        idKualitasAktif = ""
        noIDAktif = ""
        idKerugianAktif = ""

        txtTotalProduksi.Clear()
        txtJumlahNG.Clear()

        usulanSudahDitambahkan = False


        '--------------------------------------------
        ' Cek total produksi
        '--------------------------------------------

        AmbilTotalProduksi(produk.ID)

    End Sub


    '========================================================
    ' CEK TOTAL PRODUKSI
    '========================================================

    Private Sub AmbilTotalProduksi(
        idProduk As String
    )

        Try

            Koneksi()

            Dim query As String =
                "SELECT TOP 1 " &
                "[ID_Total_Produk], " &
                "[Total_Produksi] " &
                "FROM [Data_Pengelolaan_Total_Produksi] " &
                "WHERE [ID_Produk] = ? " &
                "ORDER BY [Tanggal_Produksi] DESC"

            Using cmdProduksi As New OleDbCommand(
                query,
                CNN
            )

                cmdProduksi.Parameters.Add(
                    "@ID_Produk",
                    OleDbType.VarWChar
                ).Value = idProduk


                Using rdProduksi As OleDbDataReader =
                    cmdProduksi.ExecuteReader()

                    If rdProduksi.Read() Then

                        idTotalProdukAktif =
                            rdProduksi(
                                "ID_Total_Produk"
                            ).ToString()

                        txtTotalProduksi.Text =
                            rdProduksi(
                                "Total_Produksi"
                            ).ToString()

                    Else

                        idTotalProdukAktif = ""

                        txtTotalProduksi.Clear()

                        MessageBox.Show(
                            "Data total produksi untuk produk tersebut belum tersedia.",
                            "Data Tidak Ditemukan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        )

                    End If

                End Using

            End Using

            CNN.Close()

        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mengecek data total produksi." &
                vbCrLf &
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


        '--------------------------------------------
        ' Pastikan total produksi tersedia
        '--------------------------------------------

        If idTotalProdukAktif = "" Then

            MessageBox.Show(
                "Data total produksi untuk produk tersebut belum tersedia.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cmbJenisNG.SelectedIndex = -1

            Exit Sub

        End If


        '--------------------------------------------
        ' Reset data sebelumnya
        '--------------------------------------------

        txtJumlahNG.Clear()

        idNGAktif = ""
        idKualitasAktif = ""
        noIDAktif = ""
        idKerugianAktif = ""

        usulanSudahDitambahkan = False


        '--------------------------------------------
        ' Cek seluruh data pendukung
        '--------------------------------------------

        AmbilDataPendukung()

    End Sub


    '========================================================
    ' CEK DATA NG, KUALITAS, DAN KERUGIAN
    '========================================================

    Private Sub AmbilDataPendukung()

        Try

            Koneksi()


            '================================================
            ' 1. CEK DATA PRODUK NG
            '================================================

            Dim queryNG As String =
                "SELECT TOP 1 " &
                "[ID_NG], " &
                "[Jumlah_NG] " &
                "FROM [Data_Produk_NG] " &
                "WHERE [ID_Total_Produk] = ? " &
                "AND [Jenis_NG] = ?"

            Using cmdNG As New OleDbCommand(
                queryNG,
                CNN
            )

                cmdNG.Parameters.Add(
                    "@ID_Total_Produk",
                    OleDbType.VarWChar
                ).Value = idTotalProdukAktif

                cmdNG.Parameters.Add(
                    "@Jenis_NG",
                    OleDbType.VarWChar
                ).Value = cmbJenisNG.Text


                Using rdNG As OleDbDataReader =
                    cmdNG.ExecuteReader()

                    If rdNG.Read() Then

                        idNGAktif =
                            rdNG(
                                "ID_NG"
                            ).ToString()

                        txtJumlahNG.Text =
                            rdNG(
                                "Jumlah_NG"
                            ).ToString()

                    Else

                        idNGAktif = ""

                        txtJumlahNG.Clear()

                        CNN.Close()

                        MessageBox.Show(
                            "Data NG untuk produk dan jenis NG tersebut belum tersedia.",
                            "Data Tidak Ditemukan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        )

                        Exit Sub

                    End If

                End Using

            End Using


            '================================================
            ' 2. CEK DATA KUALITAS
            '================================================

            Dim queryKualitas As String =
                "SELECT TOP 1 " &
                "[ID_Kualitas], " &
                "[No_ID] " &
                "FROM [Data_Kualitas] " &
                "WHERE [ID_NG] = ? " &
                "AND [ID_Total_Produk] = ?"

            Using cmdKualitas As New OleDbCommand(
                queryKualitas,
                CNN
            )

                cmdKualitas.Parameters.Add(
                    "@ID_NG",
                    OleDbType.VarWChar
                ).Value = idNGAktif

                cmdKualitas.Parameters.Add(
                    "@ID_Total_Produk",
                    OleDbType.VarWChar
                ).Value = idTotalProdukAktif


                Using rdKualitas As OleDbDataReader =
                    cmdKualitas.ExecuteReader()

                    If rdKualitas.Read() Then

                        If IsDBNull(
                            rdKualitas("ID_Kualitas")
                        ) Then

                            CNN.Close()

                            MessageBox.Show(
                                "ID Kualitas belum tersedia.",
                                "Data Tidak Ditemukan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            )

                            Exit Sub

                        End If


                        idKualitasAktif =
                            rdKualitas(
                                "ID_Kualitas"
                            ).ToString()


                        If Not IsDBNull(
                            rdKualitas("No_ID")
                        ) Then

                            noIDAktif =
                                rdKualitas(
                                    "No_ID"
                                ).ToString()

                        Else

                            noIDAktif = ""

                        End If

                    Else

                        idKualitasAktif = ""
                        noIDAktif = ""

                        CNN.Close()

                        MessageBox.Show(
                            "Data kualitas untuk produk dan jenis NG tersebut belum tersedia.",
                            "Data Tidak Ditemukan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        )

                        Exit Sub

                    End If

                End Using

            End Using


            '================================================
            ' 3. CEK DATA KERUGIAN
            '================================================

            Dim produk As ProdukItem =
                TryCast(
                    cmbNamaProduk.SelectedItem,
                    ProdukItem
                )


            If produk Is Nothing Then

                CNN.Close()
                Exit Sub

            End If


            Dim queryKerugian As String =
                "SELECT TOP 1 [ID_Kerugian] " &
                "FROM [Data_Pemrosesan_Kerugian] " &
                "WHERE [ID_Produk] = ? " &
                "AND [ID_NG] = ? " &
                "ORDER BY [ID_Kerugian] DESC"


            Using cmdKerugian As New OleDbCommand(
                queryKerugian,
                CNN
            )

                cmdKerugian.Parameters.Add(
                    "@ID_Produk",
                    OleDbType.VarWChar
                ).Value = produk.ID

                cmdKerugian.Parameters.Add(
                    "@ID_NG",
                    OleDbType.VarWChar
                ).Value = idNGAktif


                Dim hasilKerugian As Object =
                    cmdKerugian.ExecuteScalar()


                If hasilKerugian Is Nothing OrElse
                   IsDBNull(hasilKerugian) Then

                    idKerugianAktif = ""

                    CNN.Close()

                    MessageBox.Show(
                        "Data kerugian untuk produk dan jenis NG tersebut belum tersedia.",
                        "Data Tidak Ditemukan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    Exit Sub

                End If


                idKerugianAktif =
                    hasilKerugian.ToString()

            End Using


            CNN.Close()


            '================================================
            ' SEMUA DATA TERSEDIA
            '================================================

            MessageBox.Show(
                "Data produk, NG, kualitas, dan kerugian tersedia.",
                "Data Tersedia",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal mengecek data pendukung usulan." &
                vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    '========================================================
    ' TOMBOL TAMBAHKAN
    '========================================================

    Private Sub btnTambahUsulan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnTambahUsulan.Click

        If cmbNamaProduk.SelectedIndex = -1 Then

            MessageBox.Show(
                "Silakan pilih nama produk terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cmbNamaProduk.Focus()

            Exit Sub

        End If


        If cmbJenisNG.SelectedIndex = -1 Then

            MessageBox.Show(
                "Silakan pilih jenis NG terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            cmbJenisNG.Focus()

            Exit Sub

        End If


        If idTotalProdukAktif = "" Then

            MessageBox.Show(
                "Data total produksi belum tersedia.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If idNGAktif = "" Then

            MessageBox.Show(
                "Data NG belum tersedia.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If idKualitasAktif = "" Then

            MessageBox.Show(
                "Data kualitas belum tersedia.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If idKerugianAktif = "" Then

            MessageBox.Show(
                "Data kerugian belum tersedia.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If rtbUsulan.Text.Trim() = "" Then

            MessageBox.Show(
                "Silakan masukkan usulan perbaikan terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            rtbUsulan.Focus()

            Exit Sub

        End If


        usulanSudahDitambahkan = True


        MessageBox.Show(
            "Data usulan berhasil ditambahkan. Silakan tekan tombol Simpan.",
            "Berhasil",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        )

    End Sub


    '========================================================
    ' TOMBOL SIMPAN
    '========================================================

    Private Sub btnSimpanUsulan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnSimpanUsulan.Click

        If Not usulanSudahDitambahkan Then

            MessageBox.Show(
                "Silakan tekan tombol Tambahkan terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If idTotalProdukAktif = "" OrElse
           idNGAktif = "" OrElse
           idKualitasAktif = "" OrElse
           idKerugianAktif = "" Then

            MessageBox.Show(
                "Data pendukung usulan belum lengkap.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If


        If rtbUsulan.Text.Trim() = "" Then

            MessageBox.Show(
                "Silakan masukkan usulan perbaikan terlebih dahulu.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            rtbUsulan.Focus()

            Exit Sub

        End If


        Try

            Koneksi()


            '================================================
            ' BUAT ID USULAN OTOMATIS
            '================================================

            Dim idUsulan As String =
                "USL" &
                DateTime.Now.ToString(
                    "yyyyMMddHHmmssfff"
                )


            '================================================
            ' KONFIRMASI
            '================================================

            Dim pesan As String =
                "Data usulan perbaikan akan disimpan." &
                vbCrLf &
                vbCrLf &
                "ID Usulan      : " &
                idUsulan &
                vbCrLf &
                "ID Kualitas    : " &
                idKualitasAktif &
                vbCrLf &
                "No ID          : " &
                noIDAktif &
                vbCrLf &
                "ID Kerugian    : " &
                idKerugianAktif &
                vbCrLf &
                "Tanggal        : " &
                dtpTanggal.Value.ToString(
                    "dd/MM/yyyy"
                ) &
                vbCrLf &
                "Nama Produk    : " &
                cmbNamaProduk.Text &
                vbCrLf &
                "Jenis NG       : " &
                cmbJenisNG.Text &
                vbCrLf &
                "Total Produksi : " &
                txtTotalProduksi.Text &
                vbCrLf &
                "Jumlah NG      : " &
                txtJumlahNG.Text &
                vbCrLf &
                "Usulan         : " &
                rtbUsulan.Text


            Dim hasil As DialogResult =
                MessageBox.Show(
                    pesan,
                    "Konfirmasi Simpan",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                )


            If hasil <> DialogResult.Yes Then

                CNN.Close()

                Exit Sub

            End If


            '================================================
            ' INSERT KE DATA_USULAN
            '================================================

            Dim querySimpan As String =
                "INSERT INTO [Data_Usulan] " &
                "([ID_Usulan], [ID_Kualitas], [No_ID], " &
                "[ID_Kerugian], [Usulan_Perbaikan], [Tanggal_Usulan]) " &
                "VALUES (?, ?, ?, ?, ?, ?)"


            Using cmdSimpan As New OleDbCommand(
                querySimpan,
                CNN
            )

                cmdSimpan.Parameters.Add(
                    "@ID_Usulan",
                    OleDbType.VarWChar
                ).Value = idUsulan


                cmdSimpan.Parameters.Add(
                    "@ID_Kualitas",
                    OleDbType.VarWChar
                ).Value = idKualitasAktif


                cmdSimpan.Parameters.Add(
                    "@No_ID",
                    OleDbType.VarWChar
                ).Value = noIDAktif


                cmdSimpan.Parameters.Add(
                    "@ID_Kerugian",
                    OleDbType.VarWChar
                ).Value = idKerugianAktif


                cmdSimpan.Parameters.Add(
                    "@Usulan_Perbaikan",
                    OleDbType.LongVarWChar
                ).Value = rtbUsulan.Text.Trim()


                cmdSimpan.Parameters.Add(
                    "@Tanggal_Usulan",
                    OleDbType.Date
                ).Value = dtpTanggal.Value


                cmdSimpan.ExecuteNonQuery()

            End Using


            CNN.Close()


            MessageBox.Show(
                "Data usulan berhasil disimpan." &
                vbCrLf &
                "ID Usulan: " &
                idUsulan,
                "Berhasil",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )


            FormRiwayatUsulan.Show()
            Me.Hide()


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(
                "Gagal menyimpan data usulan." &
                vbCrLf &
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

    Private Sub btnResetUsulan_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnResetUsulan.Click

        dtpTanggal.Value =
            DateTime.Now

        cmbNamaProduk.SelectedIndex = -1
        cmbJenisNG.SelectedIndex = -1

        txtTotalProduksi.Clear()
        txtJumlahNG.Clear()

        rtbUsulan.Clear()

        idTotalProdukAktif = ""
        idNGAktif = ""
        idKualitasAktif = ""
        noIDAktif = ""
        idKerugianAktif = ""

        usulanSudahDitambahkan = False

        cmbNamaProduk.Focus()

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

    Private Sub btnLogOutInUsulan_Click(sender As Object, e As EventArgs) Handles btnLogOutInUsulan.Click
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

    Private Sub lblInKerugian_Click(sender As Object, e As EventArgs) Handles lblInKerugian.Click
        FormKerugian.Show()
    End Sub
End Class