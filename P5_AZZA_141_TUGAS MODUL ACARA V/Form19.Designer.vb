<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLaporanQC
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cbPilihData = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpTanggal = New System.Windows.Forms.DateTimePicker()
        Me.btnBuat = New System.Windows.Forms.Button()
        Me.dgvRiwayat = New System.Windows.Forms.DataGridView()
        Me.btnHapus = New System.Windows.Forms.Button()
        Me.btnCari = New System.Windows.Forms.Button()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.txtRiwayatData = New System.Windows.Forms.TextBox()
        Me.lblNamaProduk = New System.Windows.Forms.Label()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvRiwayat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cbPilihData)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.dtpTanggal)
        Me.Panel2.Controls.Add(Me.btnBuat)
        Me.Panel2.Controls.Add(Me.dgvRiwayat)
        Me.Panel2.Controls.Add(Me.btnHapus)
        Me.Panel2.Controls.Add(Me.btnCari)
        Me.Panel2.Controls.Add(Me.PictureBox4)
        Me.Panel2.Controls.Add(Me.PictureBox9)
        Me.Panel2.Controls.Add(Me.txtRiwayatData)
        Me.Panel2.Controls.Add(Me.lblNamaProduk)
        Me.Panel2.Location = New System.Drawing.Point(275, 80)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(673, 440)
        Me.Panel2.TabIndex = 159
        '
        'cbPilihData
        '
        Me.cbPilihData.AutoSize = True
        Me.cbPilihData.Location = New System.Drawing.Point(24, 208)
        Me.cbPilihData.Name = "cbPilihData"
        Me.cbPilihData.Size = New System.Drawing.Size(134, 21)
        Me.cbPilihData.TabIndex = 134
        Me.cbPilihData.Text = "Pilih semua data"
        Me.cbPilihData.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("MS Reference Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(21, 176)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 18)
        Me.Label1.TabIndex = 132
        Me.Label1.Text = "Data Produksi"
        '
        'dtpTanggal
        '
        Me.dtpTanggal.Location = New System.Drawing.Point(24, 129)
        Me.dtpTanggal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dtpTanggal.Name = "dtpTanggal"
        Me.dtpTanggal.Size = New System.Drawing.Size(250, 22)
        Me.dtpTanggal.TabIndex = 131
        '
        'btnBuat
        '
        Me.btnBuat.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.btnBuat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuat.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBuat.Location = New System.Drawing.Point(541, 397)
        Me.btnBuat.Name = "btnBuat"
        Me.btnBuat.Size = New System.Drawing.Size(107, 28)
        Me.btnBuat.TabIndex = 130
        Me.btnBuat.Text = "Buat Laporan"
        Me.btnBuat.UseVisualStyleBackColor = False
        '
        'dgvRiwayat
        '
        Me.dgvRiwayat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRiwayat.Location = New System.Drawing.Point(24, 245)
        Me.dgvRiwayat.Name = "dgvRiwayat"
        Me.dgvRiwayat.RowHeadersWidth = 51
        Me.dgvRiwayat.RowTemplate.Height = 24
        Me.dgvRiwayat.Size = New System.Drawing.Size(624, 132)
        Me.dgvRiwayat.TabIndex = 127
        '
        'btnHapus
        '
        Me.btnHapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(6, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.btnHapus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapus.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnHapus.Location = New System.Drawing.Point(410, 397)
        Me.btnHapus.Name = "btnHapus"
        Me.btnHapus.Size = New System.Drawing.Size(107, 28)
        Me.btnHapus.TabIndex = 128
        Me.btnHapus.Text = "Hapus"
        Me.btnHapus.UseVisualStyleBackColor = False
        '
        'btnCari
        '
        Me.btnCari.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.btnCari.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCari.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCari.Location = New System.Drawing.Point(541, 203)
        Me.btnCari.Name = "btnCari"
        Me.btnCari.Size = New System.Drawing.Size(107, 28)
        Me.btnCari.TabIndex = 36
        Me.btnCari.Text = "Cari"
        Me.btnCari.UseVisualStyleBackColor = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.White
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox4.Location = New System.Drawing.Point(354, 207)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(22, 22)
        Me.PictureBox4.TabIndex = 35
        Me.PictureBox4.TabStop = False
        '
        'PictureBox9
        '
        Me.PictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox9.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(673, 82)
        Me.PictureBox9.TabIndex = 33
        Me.PictureBox9.TabStop = False
        '
        'txtRiwayatData
        '
        Me.txtRiwayatData.BackColor = System.Drawing.SystemColors.Window
        Me.txtRiwayatData.Location = New System.Drawing.Point(373, 206)
        Me.txtRiwayatData.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtRiwayatData.Name = "txtRiwayatData"
        Me.txtRiwayatData.ReadOnly = True
        Me.txtRiwayatData.Size = New System.Drawing.Size(162, 22)
        Me.txtRiwayatData.TabIndex = 21
        Me.txtRiwayatData.Text = "Cari riwayat data"
        Me.txtRiwayatData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblNamaProduk
        '
        Me.lblNamaProduk.AutoSize = True
        Me.lblNamaProduk.BackColor = System.Drawing.Color.Transparent
        Me.lblNamaProduk.Font = New System.Drawing.Font("MS Reference Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNamaProduk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblNamaProduk.Location = New System.Drawing.Point(21, 99)
        Me.lblNamaProduk.Name = "lblNamaProduk"
        Me.lblNamaProduk.Size = New System.Drawing.Size(70, 18)
        Me.lblNamaProduk.TabIndex = 19
        Me.lblNamaProduk.Text = "Tanggal"
        '
        'btnLogout
        '
        Me.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLogout.Location = New System.Drawing.Point(675, 2)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(165, 36)
        Me.btnLogout.TabIndex = 158
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'PictureBox8
        '
        Me.PictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox8.Location = New System.Drawing.Point(854, 4)
        Me.PictureBox8.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(35, 30)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox8.TabIndex = 157
        Me.PictureBox8.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("MS Reference Sans Serif", 6.5!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label10.Location = New System.Drawing.Point(890, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 15)
        Me.Label10.TabIndex = 156
        Me.Label10.Text = "Production Manager"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label9.Location = New System.Drawing.Point(889, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 18)
        Me.Label9.TabIndex = 155
        Me.Label9.Text = "A001"
        '
        'FormLaporanQC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.WhatsApp_Image_2026_09_23_at_16_24_41
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1017, 558)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.PictureBox8)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Name = "FormLaporanQC"
        Me.Text = "FormLaporanQC"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvRiwayat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents cbPilihData As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpTanggal As DateTimePicker
    Friend WithEvents btnBuat As Button
    Friend WithEvents dgvRiwayat As DataGridView
    Friend WithEvents btnHapus As Button
    Friend WithEvents btnCari As Button
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents txtRiwayatData As TextBox
    Friend WithEvents lblNamaProduk As Label
    Friend WithEvents btnLogout As Button
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
End Class
