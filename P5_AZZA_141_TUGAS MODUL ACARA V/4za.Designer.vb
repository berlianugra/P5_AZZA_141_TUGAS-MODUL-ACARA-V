<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblSelamat = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.cmbJabatan = New System.Windows.Forms.ComboBox()
        Me.btnMasuk = New System.Windows.Forms.Button()
        Me.imgUsername = New System.Windows.Forms.PictureBox()
        Me.imgPassword = New System.Windows.Forms.PictureBox()
        Me.imgJabatan = New System.Windows.Forms.PictureBox()
        Me.imgPasswordBuka = New System.Windows.Forms.PictureBox()
        Me.lblLupaPassword = New System.Windows.Forms.Label()
        Me.imgPasswordTutup = New System.Windows.Forms.PictureBox()
        CType(Me.imgUsername, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgJabatan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPasswordBuka, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPasswordTutup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSelamat
        '
        Me.lblSelamat.AutoSize = True
        Me.lblSelamat.BackColor = System.Drawing.Color.Transparent
        Me.lblSelamat.Font = New System.Drawing.Font("Segoe Fluent Icons", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelamat.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lblSelamat.Location = New System.Drawing.Point(22, 241)
        Me.lblSelamat.Name = "lblSelamat"
        Me.lblSelamat.Size = New System.Drawing.Size(121, 36)
        Me.lblSelamat.TabIndex = 50
        Me.lblSelamat.Text = "Selamat"
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.LightGray
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(711, 436)
        Me.txtPassword.Multiline = True
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(281, 35)
        Me.txtPassword.TabIndex = 52
        '
        'txtUsername
        '
        Me.txtUsername.BackColor = System.Drawing.Color.LightGray
        Me.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUsername.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(711, 375)
        Me.txtUsername.Multiline = True
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(281, 35)
        Me.txtUsername.TabIndex = 51
        '
        'cmbJabatan
        '
        Me.cmbJabatan.BackColor = System.Drawing.Color.LightGray
        Me.cmbJabatan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbJabatan.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbJabatan.FormattingEnabled = True
        Me.cmbJabatan.Items.AddRange(New Object() {"Part Number 7105-5552", "Part Number 7105-5551", "Part Number 7105-3578"})
        Me.cmbJabatan.Location = New System.Drawing.Point(711, 507)
        Me.cmbJabatan.Name = "cmbJabatan"
        Me.cmbJabatan.Size = New System.Drawing.Size(281, 32)
        Me.cmbJabatan.TabIndex = 53
        '
        'btnMasuk
        '
        Me.btnMasuk.BackColor = System.Drawing.Color.Transparent
        Me.btnMasuk.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_24_115341
        Me.btnMasuk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMasuk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMasuk.Location = New System.Drawing.Point(666, 564)
        Me.btnMasuk.Name = "btnMasuk"
        Me.btnMasuk.Size = New System.Drawing.Size(367, 44)
        Me.btnMasuk.TabIndex = 54
        Me.btnMasuk.UseVisualStyleBackColor = False
        '
        'imgUsername
        '
        Me.imgUsername.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.dashboard
        Me.imgUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.imgUsername.Image = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_24_121334
        Me.imgUsername.Location = New System.Drawing.Point(716, 379)
        Me.imgUsername.Name = "imgUsername"
        Me.imgUsername.Size = New System.Drawing.Size(268, 28)
        Me.imgUsername.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgUsername.TabIndex = 55
        Me.imgUsername.TabStop = False
        '
        'imgPassword
        '
        Me.imgPassword.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.dashboard
        Me.imgPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.imgPassword.Image = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_24_114913
        Me.imgPassword.Location = New System.Drawing.Point(716, 440)
        Me.imgPassword.Name = "imgPassword"
        Me.imgPassword.Size = New System.Drawing.Size(134, 28)
        Me.imgPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgPassword.TabIndex = 56
        Me.imgPassword.TabStop = False
        '
        'imgJabatan
        '
        Me.imgJabatan.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.dashboard
        Me.imgJabatan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.imgJabatan.Image = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_24_141013
        Me.imgJabatan.Location = New System.Drawing.Point(716, 512)
        Me.imgJabatan.Name = "imgJabatan"
        Me.imgJabatan.Size = New System.Drawing.Size(249, 26)
        Me.imgJabatan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgJabatan.TabIndex = 57
        Me.imgJabatan.TabStop = False
        '
        'imgPasswordBuka
        '
        Me.imgPasswordBuka.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.dashboard
        Me.imgPasswordBuka.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.imgPasswordBuka.Image = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_24_130639
        Me.imgPasswordBuka.Location = New System.Drawing.Point(949, 440)
        Me.imgPasswordBuka.Name = "imgPasswordBuka"
        Me.imgPasswordBuka.Size = New System.Drawing.Size(40, 28)
        Me.imgPasswordBuka.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgPasswordBuka.TabIndex = 58
        Me.imgPasswordBuka.TabStop = False
        '
        'lblLupaPassword
        '
        Me.lblLupaPassword.AutoSize = True
        Me.lblLupaPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblLupaPassword.Font = New System.Drawing.Font("Segoe Fluent Icons", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLupaPassword.ForeColor = System.Drawing.Color.Red
        Me.lblLupaPassword.Location = New System.Drawing.Point(709, 472)
        Me.lblLupaPassword.Name = "lblLupaPassword"
        Me.lblLupaPassword.Size = New System.Drawing.Size(122, 20)
        Me.lblLupaPassword.TabIndex = 59
        Me.lblLupaPassword.Text = "Lupa Password?"
        '
        'imgPasswordTutup
        '
        Me.imgPasswordTutup.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.dashboard
        Me.imgPasswordTutup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.imgPasswordTutup.Image = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_24_131806
        Me.imgPasswordTutup.Location = New System.Drawing.Point(951, 437)
        Me.imgPasswordTutup.Name = "imgPasswordTutup"
        Me.imgPasswordTutup.Size = New System.Drawing.Size(36, 33)
        Me.imgPasswordTutup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgPasswordTutup.TabIndex = 60
        Me.imgPasswordTutup.TabStop = False
        '
        'FormLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_24_113806
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1128, 694)
        Me.Controls.Add(Me.imgPasswordTutup)
        Me.Controls.Add(Me.lblLupaPassword)
        Me.Controls.Add(Me.imgPasswordBuka)
        Me.Controls.Add(Me.imgJabatan)
        Me.Controls.Add(Me.imgPassword)
        Me.Controls.Add(Me.imgUsername)
        Me.Controls.Add(Me.btnMasuk)
        Me.Controls.Add(Me.cmbJabatan)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.lblSelamat)
        Me.DoubleBuffered = True
        Me.Name = "FormLogin"
        Me.Text = "Form4"
        CType(Me.imgUsername, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgJabatan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPasswordBuka, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPasswordTutup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSelamat As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents cmbJabatan As ComboBox
    Friend WithEvents btnMasuk As Button
    Friend WithEvents imgUsername As PictureBox
    Friend WithEvents imgPassword As PictureBox
    Friend WithEvents imgJabatan As PictureBox
    Friend WithEvents imgPasswordBuka As PictureBox
    Friend WithEvents lblLupaPassword As Label
    Friend WithEvents imgPasswordTutup As PictureBox
End Class
