<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRiwayatProduksi
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.btnCari2 = New System.Windows.Forms.Button()
        Me.btnHapus = New System.Windows.Forms.Button()
        Me.dgvRiwayat = New System.Windows.Forms.DataGridView()
        Me.btnCari1 = New System.Windows.Forms.Button()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.lblNamaProduk = New System.Windows.Forms.Label()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbNamaProduk = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtRiwayatData = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lblRiwayat = New System.Windows.Forms.Label()
        Me.lblInput = New System.Windows.Forms.Label()
        Me.lblNG = New System.Windows.Forms.Label()
        Me.lblDashboard = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblProduksi = New System.Windows.Forms.Label()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRiwayat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(397, 164)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(306, 286)
        Me.Chart1.TabIndex = 131
        Me.Chart1.Text = "Chart1"
        '
        'btnCari2
        '
        Me.btnCari2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.btnCari2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCari2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCari2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCari2.Location = New System.Drawing.Point(596, 120)
        Me.btnCari2.Name = "btnCari2"
        Me.btnCari2.Size = New System.Drawing.Size(107, 28)
        Me.btnCari2.TabIndex = 130
        Me.btnCari2.Text = "Cari"
        Me.btnCari2.UseVisualStyleBackColor = False
        '
        'btnHapus
        '
        Me.btnHapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(6, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.btnHapus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapus.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnHapus.Location = New System.Drawing.Point(28, 422)
        Me.btnHapus.Name = "btnHapus"
        Me.btnHapus.Size = New System.Drawing.Size(107, 28)
        Me.btnHapus.TabIndex = 128
        Me.btnHapus.Text = "Hapus"
        Me.btnHapus.UseVisualStyleBackColor = False
        '
        'dgvRiwayat
        '
        Me.dgvRiwayat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRiwayat.Location = New System.Drawing.Point(28, 134)
        Me.dgvRiwayat.Name = "dgvRiwayat"
        Me.dgvRiwayat.RowHeadersWidth = 51
        Me.dgvRiwayat.RowTemplate.Height = 24
        Me.dgvRiwayat.Size = New System.Drawing.Size(328, 270)
        Me.dgvRiwayat.TabIndex = 127
        '
        'btnCari1
        '
        Me.btnCari1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.btnCari1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCari1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCari1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCari1.Location = New System.Drawing.Point(218, 91)
        Me.btnCari1.Name = "btnCari1"
        Me.btnCari1.Size = New System.Drawing.Size(107, 28)
        Me.btnCari1.TabIndex = 36
        Me.btnCari1.Text = "Cari"
        Me.btnCari1.UseVisualStyleBackColor = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.White
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox4.Location = New System.Drawing.Point(28, 94)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(22, 22)
        Me.PictureBox4.TabIndex = 35
        Me.PictureBox4.TabStop = False
        '
        'PictureBox9
        '
        Me.PictureBox9.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_27_092327
        Me.PictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox9.Location = New System.Drawing.Point(0, -1)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(390, 82)
        Me.PictureBox9.TabIndex = 33
        Me.PictureBox9.TabStop = False
        '
        'lblNamaProduk
        '
        Me.lblNamaProduk.AutoSize = True
        Me.lblNamaProduk.BackColor = System.Drawing.Color.Transparent
        Me.lblNamaProduk.Font = New System.Drawing.Font("MS Reference Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNamaProduk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblNamaProduk.Location = New System.Drawing.Point(394, 97)
        Me.lblNamaProduk.Name = "lblNamaProduk"
        Me.lblNamaProduk.Size = New System.Drawing.Size(112, 18)
        Me.lblNamaProduk.TabIndex = 19
        Me.lblNamaProduk.Text = "Nama Produk"
        '
        'btnLogout
        '
        Me.btnLogout.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_27_072248
        Me.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLogout.Location = New System.Drawing.Point(675, 2)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(165, 36)
        Me.btnLogout.TabIndex = 138
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'PictureBox8
        '
        Me.PictureBox8.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_27_072434
        Me.PictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox8.Location = New System.Drawing.Point(854, 4)
        Me.PictureBox8.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(35, 30)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox8.TabIndex = 137
        Me.PictureBox8.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label10.Location = New System.Drawing.Point(889, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 18)
        Me.Label10.TabIndex = 136
        Me.Label10.Text = "Quality Inspector"
        '
        'cmbNamaProduk
        '
        Me.cmbNamaProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNamaProduk.FormattingEnabled = True
        Me.cmbNamaProduk.Items.AddRange(New Object() {"Part Number 7105-5552", "Part Number 7105-5551", "Part Number 7105-3578"})
        Me.cmbNamaProduk.Location = New System.Drawing.Point(397, 123)
        Me.cmbNamaProduk.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbNamaProduk.Name = "cmbNamaProduk"
        Me.cmbNamaProduk.Size = New System.Drawing.Size(193, 24)
        Me.cmbNamaProduk.TabIndex = 131
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cmbNamaProduk)
        Me.Panel2.Controls.Add(Me.Chart1)
        Me.Panel2.Controls.Add(Me.btnCari2)
        Me.Panel2.Controls.Add(Me.btnHapus)
        Me.Panel2.Controls.Add(Me.dgvRiwayat)
        Me.Panel2.Controls.Add(Me.btnCari1)
        Me.Panel2.Controls.Add(Me.PictureBox4)
        Me.Panel2.Controls.Add(Me.PictureBox9)
        Me.Panel2.Controls.Add(Me.txtRiwayatData)
        Me.Panel2.Controls.Add(Me.lblNamaProduk)
        Me.Panel2.Location = New System.Drawing.Point(241, 67)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(737, 467)
        Me.Panel2.TabIndex = 139
        '
        'txtRiwayatData
        '
        Me.txtRiwayatData.BackColor = System.Drawing.SystemColors.Window
        Me.txtRiwayatData.Location = New System.Drawing.Point(50, 94)
        Me.txtRiwayatData.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtRiwayatData.Name = "txtRiwayatData"
        Me.txtRiwayatData.ReadOnly = True
        Me.txtRiwayatData.Size = New System.Drawing.Size(162, 22)
        Me.txtRiwayatData.TabIndex = 21
        Me.txtRiwayatData.Text = "Cari riwayat data"
        Me.txtRiwayatData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.Label9.TabIndex = 135
        Me.Label9.Text = "A004"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_27_072027
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(21, 122)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 134
        Me.PictureBox2.TabStop = False
        '
        'lblRiwayat
        '
        Me.lblRiwayat.AutoSize = True
        Me.lblRiwayat.BackColor = System.Drawing.Color.Transparent
        Me.lblRiwayat.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRiwayat.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblRiwayat.Location = New System.Drawing.Point(60, 191)
        Me.lblRiwayat.Name = "lblRiwayat"
        Me.lblRiwayat.Size = New System.Drawing.Size(69, 18)
        Me.lblRiwayat.TabIndex = 132
        Me.lblRiwayat.Text = "Riwayat"
        '
        'lblInput
        '
        Me.lblInput.AutoSize = True
        Me.lblInput.BackColor = System.Drawing.Color.Transparent
        Me.lblInput.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInput.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lblInput.Location = New System.Drawing.Point(60, 165)
        Me.lblInput.Name = "lblInput"
        Me.lblInput.Size = New System.Drawing.Size(51, 18)
        Me.lblInput.TabIndex = 131
        Me.lblInput.Text = "Input"
        '
        'lblNG
        '
        Me.lblNG.AutoSize = True
        Me.lblNG.BackColor = System.Drawing.Color.Transparent
        Me.lblNG.Font = New System.Drawing.Font("MS Reference Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNG.ForeColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblNG.Location = New System.Drawing.Point(57, 219)
        Me.lblNG.Name = "lblNG"
        Me.lblNG.Size = New System.Drawing.Size(101, 22)
        Me.lblNG.TabIndex = 128
        Me.lblNG.Text = "Produk NG"
        '
        'lblDashboard
        '
        Me.lblDashboard.AutoSize = True
        Me.lblDashboard.BackColor = System.Drawing.Color.Transparent
        Me.lblDashboard.Font = New System.Drawing.Font("MS Reference Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashboard.ForeColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblDashboard.Location = New System.Drawing.Point(57, 87)
        Me.lblDashboard.Name = "lblDashboard"
        Me.lblDashboard.Size = New System.Drawing.Size(102, 22)
        Me.lblDashboard.TabIndex = 127
        Me.lblDashboard.Text = "Dashboard"
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_27_072001
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(21, 213)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 130
        Me.PictureBox3.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.Screenshot_2026_09_27_071946
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(21, 84)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 129
        Me.PictureBox1.TabStop = False
        '
        'lblProduksi
        '
        Me.lblProduksi.AutoSize = True
        Me.lblProduksi.BackColor = System.Drawing.Color.Transparent
        Me.lblProduksi.Font = New System.Drawing.Font("MS Reference Sans Serif", 10.2!, System.Drawing.FontStyle.Bold)
        Me.lblProduksi.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lblProduksi.Location = New System.Drawing.Point(59, 127)
        Me.lblProduksi.Name = "lblProduksi"
        Me.lblProduksi.Size = New System.Drawing.Size(91, 22)
        Me.lblProduksi.TabIndex = 133
        Me.lblProduksi.Text = "Produksi"
        '
        'FormRiwayatProduksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.P5_AZZA_141_TUGAS_MODUL_ACARA_V.My.Resources.Resources.WhatsApp_Image_2026_09_23_at_16_24_41
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1017, 558)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.PictureBox8)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.lblRiwayat)
        Me.Controls.Add(Me.lblInput)
        Me.Controls.Add(Me.lblNG)
        Me.Controls.Add(Me.lblDashboard)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblProduksi)
        Me.Name = "FormRiwayatProduksi"
        Me.Text = "FormRiwayatProduksi"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRiwayat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents btnCari2 As Button
    Friend WithEvents btnHapus As Button
    Friend WithEvents dgvRiwayat As DataGridView
    Friend WithEvents btnCari1 As Button
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents lblNamaProduk As Label
    Friend WithEvents btnLogout As Button
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents Label10 As Label
    Friend WithEvents cmbNamaProduk As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtRiwayatData As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lblRiwayat As Label
    Friend WithEvents lblInput As Label
    Friend WithEvents lblNG As Label
    Friend WithEvents lblDashboard As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblProduksi As Label
End Class
