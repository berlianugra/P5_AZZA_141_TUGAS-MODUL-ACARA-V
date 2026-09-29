Imports System.Data.OleDb
Imports System.Drawing.Drawing2D

Public Class FormLogin

    Private passwordTampil As Boolean = False

    Private Sub RoundedControl(ctrl As Control, radius As Integer)

        Dim path As New GraphicsPath()
        Dim d As Integer = radius * 2

        path.StartFigure()

        path.AddArc(
            New Rectangle(0, 0, d, d),
            180,
            90
        )

        path.AddArc(
            New Rectangle(
                ctrl.Width - d,
                0,
                d,
                d
            ),
            270,
            90
        )

        path.AddArc(
            New Rectangle(
                ctrl.Width - d,
                ctrl.Height - d,
                d,
                d
            ),
            0,
            90
        )

        path.AddArc(
            New Rectangle(
                0,
                ctrl.Height - d,
                d,
                d
            ),
            90,
            90
        )

        path.CloseFigure()

        ctrl.Region = New Region(path)

    End Sub

    Private Sub LoadJabatan()

        Try

            Koneksi()

            Dim query As String =
                "SELECT DISTINCT Jabatan " &
                "FROM Data_User " &
                "WHERE Jabatan IS NOT NULL"

            cmd = New OleDbCommand(query, CNN)

            Rd = cmd.ExecuteReader()

            cmbJabatan.Items.Clear()

            Dim daftarJabatan As New List(Of String)

            While Rd.Read()

                Dim jabatan As String =
                    Rd("Jabatan").ToString().Trim()

                If jabatan <> "" Then

                    daftarJabatan.Add(jabatan)

                End If

            End While

            Rd.Close()
            Rd = Nothing

            CNN.Close()

            Dim urutanJabatan As New List(Of String) From {
                "Production Manager",
                "Supervisor Quality Assurance",
                "Leader Quality",
                "Quality Inspector"
            }

            For Each jabatan As String In urutanJabatan

                If daftarJabatan.Contains(jabatan) Then

                    cmbJabatan.Items.Add(jabatan)

                End If

            Next


            cmbJabatan.SelectedIndex = -1


        Catch ex As Exception

            If Rd IsNot Nothing Then

                If Not Rd.IsClosed Then
                    Rd.Close()
                End If

                Rd = Nothing

            End If


            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If


            MessageBox.Show(
                "Data jabatan gagal dimuat." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RoundedControl(btnMasuk, 8)
        RoundedControl(txtUsername, 5)
        RoundedControl(txtPassword, 5)
        RoundedControl(cmbJabatan, 5)

        passwordTampil = False

        txtPassword.PasswordChar = "●"c

        imgPasswordBuka.Visible = True
        imgPasswordTutup.Visible = False

        cmbJabatan.DropDownStyle = ComboBoxStyle.DropDownList

        LoadJabatan()

        imgUsername.Visible = True
        imgPassword.Visible = True
        imgJabatan.Visible = True
        Dim tip As New ToolTip()

        tip.SetToolTip(
        btnMasuk,
        "Masuk ke sistem"
    )
    End Sub
    Private Sub Form4_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Me.BeginInvoke(
            New MethodInvoker(
                Sub()

                    Me.ActiveControl = Nothing

                End Sub
            )
        )

    End Sub

    Private Sub imgUsername_Click(
        sender As Object,
        e As EventArgs
    ) Handles imgUsername.Click

        imgUsername.Visible = False

        txtUsername.Focus()

    End Sub

    Private Sub txtUsername_Enter(
        sender As Object,
        e As EventArgs
    ) Handles txtUsername.Enter

        imgUsername.Visible = False

    End Sub

    Private Sub txtUsername_Leave(
        sender As Object,
        e As EventArgs
    ) Handles txtUsername.Leave

        If txtUsername.Text.Trim() = "" Then

            imgUsername.Visible = True

        End If

    End Sub

    Private Sub imgPassword_Click(
        sender As Object,
        e As EventArgs
    ) Handles imgPassword.Click

        imgPassword.Visible = False

        txtPassword.Focus()

    End Sub

    Private Sub txtPassword_Enter(
        sender As Object,
        e As EventArgs
    ) Handles txtPassword.Enter

        imgPassword.Visible = False

    End Sub

    Private Sub txtPassword_Leave(
        sender As Object,
        e As EventArgs
    ) Handles txtPassword.Leave

        If txtPassword.Text.Trim() = "" Then

            imgPassword.Visible = True

        End If

    End Sub

    Private Sub imgPasswordBuka_Click(
    sender As Object,
    e As EventArgs
) Handles imgPasswordBuka.Click

        txtPassword.PasswordChar = ControlChars.NullChar

        passwordTampil = True

        imgPasswordBuka.Visible = False

        imgPasswordTutup.Visible = True

        txtPassword.Focus()

    End Sub

    Private Sub imgPasswordTutup_Click(
    sender As Object,
    e As EventArgs
) Handles imgPasswordTutup.Click

        txtPassword.PasswordChar = "●"c

        passwordTampil = False

        imgPasswordTutup.Visible = False

        imgPasswordBuka.Visible = True

        txtPassword.Focus()

    End Sub

    Private Sub imgJabatan_Click(
        sender As Object,
        e As EventArgs
    ) Handles imgJabatan.Click

        imgJabatan.Visible = False

        cmbJabatan.Focus()

        cmbJabatan.DroppedDown = True

    End Sub

    Private Sub cmbJabatan_Click(
        sender As Object,
        e As EventArgs
    ) Handles cmbJabatan.Click

        imgJabatan.Visible = False

    End Sub

    Private Sub cmbJabatan_DropDown(
        sender As Object,
        e As EventArgs
    ) Handles cmbJabatan.DropDown

        imgJabatan.Visible = False

    End Sub

    Private Sub cmbJabatan_DropDownClosed(
        sender As Object,
        e As EventArgs
    ) Handles cmbJabatan.DropDownClosed

        If cmbJabatan.SelectedIndex = -1 Then

            imgJabatan.Visible = True

        End If

    End Sub

    Private Sub cmbJabatan_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    ) Handles cmbJabatan.SelectedIndexChanged

        If cmbJabatan.SelectedIndex >= 0 Then

            imgJabatan.Visible = False

        End If

    End Sub

    Private Sub btnMasuk_Click(
    sender As Object,
    e As EventArgs
) Handles btnMasuk.Click

        '========================================================
        ' CEK USERNAME
        '========================================================

        If txtUsername.Text.Trim() = "" Then

            MessageBox.Show(
            "Username belum diisi.",
            "Peringatan",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            imgUsername.Visible = False
            txtUsername.Focus()

            Exit Sub

        End If


        '========================================================
        ' CEK PASSWORD
        '========================================================

        If txtPassword.Text.Trim() = "" Then

            MessageBox.Show(
            "Password belum diisi.",
            "Peringatan",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            imgPassword.Visible = False
            txtPassword.Focus()

            Exit Sub

        End If


        '========================================================
        ' CEK JABATAN
        '========================================================

        If cmbJabatan.SelectedIndex = -1 Then

            MessageBox.Show(
            "Silakan pilih jabatan.",
            "Peringatan",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            cmbJabatan.Focus()

            Exit Sub

        End If


        Try

            '====================================================
            ' BUKA DATABASE
            '====================================================

            Koneksi()


            '====================================================
            ' AMBIL DATA USER BERDASARKAN USERNAME
            '====================================================

            Dim query As String =
            "SELECT No_ID, Nama, Username, Password, Jabatan " &
            "FROM Data_User " &
            "WHERE Username = ?"


            cmd = New OleDbCommand(
            query,
            CNN
        )


            cmd.Parameters.AddWithValue(
            "@Username",
            txtUsername.Text.Trim()
        )


            Rd = cmd.ExecuteReader()


            '====================================================
            ' VARIABEL LOGIN
            '====================================================

            Dim loginBerhasil As Boolean = False

            Dim namaUser As String = ""


            '====================================================
            ' BACA DATA DATABASE
            '====================================================

            If Rd.Read() Then

                '------------------------------------------------
                ' DATA DARI DATABASE
                '------------------------------------------------

                Dim usernameDB As String =
                Rd("Username").ToString()

                Dim passwordDB As String =
                Rd("Password").ToString()

                Dim jabatanDB As String =
                Rd("Jabatan").ToString()


                '------------------------------------------------
                ' DATA DARI INPUT USER
                '------------------------------------------------

                Dim usernameInput As String =
                txtUsername.Text.Trim()

                Dim passwordInput As String =
                txtPassword.Text.Trim()

                Dim jabatanInput As String =
                cmbJabatan.Text.Trim()


                '================================================
                ' CEK USERNAME, PASSWORD, DAN JABATAN
                ' CASE-SENSITIVE
                '================================================

                If String.Equals(
                usernameInput,
                usernameDB,
                StringComparison.Ordinal
            ) AndAlso
               String.Equals(
                   passwordInput,
                   passwordDB,
                   StringComparison.Ordinal
               ) AndAlso
               String.Equals(
                   jabatanInput,
                   jabatanDB,
                   StringComparison.Ordinal
               ) Then

                    loginBerhasil = True

                    '--------------------------------------------
                    ' SIMPAN No_ID USER YANG LOGIN
                    '--------------------------------------------

                    A = Rd("No_ID").ToString()

                    '--------------------------------------------
                    ' SIMPAN NAMA USER
                    '--------------------------------------------

                    namaUser =
                    Rd("Nama").ToString()

                End If

            End If


            '====================================================
            ' TUTUP READER DAN DATABASE
            '====================================================

            If Rd IsNot Nothing Then

                If Not Rd.IsClosed Then
                    Rd.Close()
                End If

                Rd = Nothing

            End If


            If CNN IsNot Nothing AndAlso
           CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If


            '====================================================
            ' CEK HASIL LOGIN
            '====================================================

            If loginBerhasil Then


                '================================================
                ' PESAN LOGIN BERHASIL
                '================================================

                MessageBox.Show(
                "Login berhasil." &
                vbCrLf & vbCrLf &
                "Selamat datang, " &
                namaUser & "!",
                "Login Berhasil",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )


                '================================================
                ' MASUK KE FORM SESUAI JABATAN
                '================================================

                Select Case cmbJabatan.Text.Trim()


                '================================================
                ' PRODUCTION MANAGER
                '================================================

                    Case "Production Manager"

                        FormAkun.Show()


                '================================================
                ' SUPERVISOR QUALITY ASSURANCE
                '================================================

                    Case "Supervisor Quality Assurance"

                        Form2.Show()


                '================================================
                ' LEADER QUALITY
                '================================================

                    Case "Leader Quality"

                        FormDashboardKualitas.Show()


                '================================================
                ' QUALITY INSPECTOR
                '================================================

                    Case "Quality Inspector"

                        FormProduksi.Show()


                        '================================================
                        ' JABATAN TIDAK DIKENALI
                        '================================================

                    Case Else

                        MessageBox.Show(
                        "Jabatan tidak memiliki halaman yang sesuai.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    )

                        Exit Sub

                End Select


                '================================================
                ' SEMBUNYIKAN FORM LOGIN
                '================================================

                Me.Hide()


            Else

                '================================================
                ' LOGIN GAGAL
                '================================================

                MessageBox.Show(
                "Username, password, atau jabatan tidak sesuai.",
                "Login Gagal",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

            End If


        Catch ex As Exception


            '====================================================
            ' TUTUP READER JIKA MASIH TERBUKA
            '====================================================

            If Rd IsNot Nothing Then

                Try

                    If Not Rd.IsClosed Then
                        Rd.Close()
                    End If

                Catch
                End Try

                Rd = Nothing

            End If


            '====================================================
            ' TUTUP DATABASE
            '====================================================

            If CNN IsNot Nothing AndAlso
           CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If


            '====================================================
            ' PESAN ERROR
            '====================================================

            MessageBox.Show(
            "Login gagal." &
            vbCrLf & vbCrLf &
            ex.Message,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )

        End Try

    End Sub
    Private Sub lblLupaPassword_Click(
        sender As Object,
        e As EventArgs
    ) Handles lblLupaPassword.Click

        '========================================================
        ' INPUT No_ID
        '========================================================

        Dim noID As String =
            InputBox(
                "Masukkan No_ID Anda:" &
                vbCrLf &
                "Contoh: A001",
                "Verifikasi Identitas"
            ).Trim()


        If noID = "" Then
            Exit Sub
        End If


        '========================================================
        ' INPUT NAMA
        '========================================================

        Dim nama As String =
            InputBox(
                "Masukkan Nama Anda:" &
                vbCrLf &
                "Nama harus sesuai dengan database.",
                "Verifikasi Identitas"
            ).Trim()


        If nama = "" Then
            Exit Sub
        End If


        Try

            '====================================================
            ' BUKA DATABASE
            '====================================================

            Koneksi()


            '====================================================
            ' CEK No_ID DAN NAMA
            '====================================================

            Dim query As String =
                "SELECT Nama, Username, Password, Jabatan " &
                "FROM Data_User " &
                "WHERE No_ID = ? AND Nama = ?"


            cmd = New OleDbCommand(query, CNN)


            '====================================================
            ' PARAMETER
            '====================================================

            cmd.Parameters.AddWithValue(
                "@No_ID",
                noID
            )

            cmd.Parameters.AddWithValue(
                "@Nama",
                nama
            )


            '====================================================
            ' JALANKAN QUERY
            '====================================================

            Rd = cmd.ExecuteReader()


            If Rd.Read() Then

                '====================================================
                ' SIMPAN DATA USER YANG LOGIN
                '====================================================

                A = Rd("No_ID").ToString()

                Dim namaUser As String =
        Rd("Nama").ToString()

                Dim jabatanUser As String =
        Rd("Jabatan").ToString()


                '====================================================
                ' TUTUP DATABASE
                '====================================================

                Rd.Close()
                Rd = Nothing

                CNN.Close()


                '====================================================
                ' PESAN LOGIN BERHASIL
                '====================================================

                MessageBox.Show(
        "Login berhasil." &
        vbCrLf & vbCrLf &
        "Selamat datang, " &
        namaUser & "!" &
        vbCrLf &
        "Jabatan: " &
        jabatanUser,
        "Login Berhasil",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    )


                '====================================================
                ' BUKA DASHBOARD SESUAI JABATAN
                '====================================================

                Select Case jabatanUser

                    Case "Production Manager"

                        FormDataProduk.Show()


                    Case "Supervisor Quality Assurance"

                        FormRiwayatKualitas.Show()


                    Case "Leader Quality"

                        FormDashboardKualitas.Show()


                    Case "Quality Inspector"

                        FormDataProduk.Show()


                    Case Else

                        MessageBox.Show(
                "Jabatan tidak memiliki dashboard yang sesuai.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

                        Exit Sub

                End Select


                '====================================================
                ' SEMBUNYIKAN FORM LOGIN
                '====================================================

                Me.Hide()


            Else

                '================================================
                ' DATA TIDAK SESUAI
                '================================================

                Rd.Close()
                Rd = Nothing

                CNN.Close()


                MessageBox.Show(
                    "No_ID dan Nama tidak sesuai dengan data pengguna.",
                    "Verifikasi Gagal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )

            End If


        Catch ex As Exception

            '====================================================
            ' TUTUP READER
            '====================================================

            If Rd IsNot Nothing Then

                If Not Rd.IsClosed Then
                    Rd.Close()
                End If

                Rd = Nothing

            End If


            '====================================================
            ' TUTUP CONNECTION
            '====================================================

            If CNN IsNot Nothing AndAlso
               CNN.State = ConnectionState.Open Then

                CNN.Close()

            End If


            MessageBox.Show(
                "Proses verifikasi gagal." &
                vbCrLf & vbCrLf &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim jam As Integer = DateTime.Now.Hour

        If jam >= 5 AndAlso jam < 11 Then
            lblSelamat.Text = "Selamat Pagi!"
        ElseIf jam >= 11 AndAlso jam < 15 Then
            lblSelamat.Text = "Selamat Siang!"
        ElseIf jam >= 15 AndAlso jam < 18 Then
            lblSelamat.Text = "Selamat Sore!"
        Else
            lblSelamat.Text = "Selamat Malam!"
        End If
    End Sub
End Class