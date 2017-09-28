<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class P2Pool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(P2Pool))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Wallet_Address = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Network1 = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Network2 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.PictureBox14 = New System.Windows.Forms.PictureBox()
        Me.PictureBox15 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.TabControl1.SuspendLayout()
        Me.Network1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Network2.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DarkGreen
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.Control
        Me.Button1.Location = New System.Drawing.Point(637, 206)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(140, 26)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Add Selected Pool(s)"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Wallet_Address
        '
        Me.Wallet_Address.BackColor = System.Drawing.SystemColors.Window
        Me.Wallet_Address.Location = New System.Drawing.Point(613, 180)
        Me.Wallet_Address.MaxLength = 100
        Me.Wallet_Address.Name = "Wallet_Address"
        Me.Wallet_Address.Size = New System.Drawing.Size(189, 20)
        Me.Wallet_Address.TabIndex = 42
        Me.Wallet_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(642, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(130, 20)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "Wallet Address"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.Control
        Me.Label4.Location = New System.Drawing.Point(127, 238)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(358, 13)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Network 1 is recommended for miners with 100MH and above."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(613, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(189, 78)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "-Select the node(s) you would like" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to add to your pool list." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-Enter the wallet " &
    "address you wish to" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "mine to in the box below." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "*Individually add nodes to use d" &
    "ifferent" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "wallet addresses."
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(697, 6)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.ScriptErrorsSuppressed = True
        Me.WebBrowser1.Size = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.TabIndex = 47
        Me.WebBrowser1.Url = New System.Uri("", System.UriKind.Relative)
        Me.WebBrowser1.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Network1)
        Me.TabControl1.Controls.Add(Me.Network2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(588, 224)
        Me.TabControl1.TabIndex = 50
        '
        'Network1
        '
        Me.Network1.Controls.Add(Me.Label1)
        Me.Network1.Controls.Add(Me.DataGridView1)
        Me.Network1.Location = New System.Drawing.Point(4, 22)
        Me.Network1.Name = "Network1"
        Me.Network1.Padding = New System.Windows.Forms.Padding(3)
        Me.Network1.Size = New System.Drawing.Size(580, 198)
        Me.Network1.TabIndex = 0
        Me.Network1.Text = "Network 1"
        Me.Network1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(251, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Loading..."
        Me.Label1.Visible = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(580, 198)
        Me.DataGridView1.TabIndex = 0
        '
        'Network2
        '
        Me.Network2.Controls.Add(Me.Label2)
        Me.Network2.Controls.Add(Me.DataGridView2)
        Me.Network2.Location = New System.Drawing.Point(4, 22)
        Me.Network2.Name = "Network2"
        Me.Network2.Padding = New System.Windows.Forms.Padding(3)
        Me.Network2.Size = New System.Drawing.Size(580, 198)
        Me.Network2.TabIndex = 1
        Me.Network2.Text = "Network 2"
        Me.Network2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(251, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Loading..."
        Me.Label2.Visible = False
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToOrderColumns = True
        Me.DataGridView2.AllowUserToResizeRows = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.Size = New System.Drawing.Size(580, 198)
        Me.DataGridView2.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DarkGreen
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.Control
        Me.Button2.Location = New System.Drawing.Point(652, 30)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(111, 37)
        Me.Button2.TabIndex = 51
        Me.Button2.Text = "Scan"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkGreen
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.PictureBox11)
        Me.Panel1.Controls.Add(Me.PictureBox14)
        Me.Panel1.Controls.Add(Me.PictureBox15)
        Me.Panel1.ForeColor = System.Drawing.SystemColors.Control
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(814, 52)
        Me.Panel1.TabIndex = 90
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(67, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(228, 26)
        Me.Label6.TabIndex = 93
        Me.Label6.Text = "P2Pool Node Scanner"
        '
        'PictureBox11
        '
        Me.PictureBox11.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.ocmlogo
        Me.PictureBox11.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox11.Name = "PictureBox11"
        Me.PictureBox11.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox11.TabIndex = 90
        Me.PictureBox11.TabStop = False
        '
        'PictureBox14
        '
        Me.PictureBox14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox14.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.minimize
        Me.PictureBox14.Location = New System.Drawing.Point(741, 13)
        Me.PictureBox14.Name = "PictureBox14"
        Me.PictureBox14.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox14.TabIndex = 92
        Me.PictureBox14.TabStop = False
        '
        'PictureBox15
        '
        Me.PictureBox15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox15.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.greenx
        Me.PictureBox15.Location = New System.Drawing.Point(779, 13)
        Me.PictureBox15.Name = "PictureBox15"
        Me.PictureBox15.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox15.TabIndex = 91
        Me.PictureBox15.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Panel2.Controls.Add(Me.TabControl1)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.Wallet_Address)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.WebBrowser1)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Location = New System.Drawing.Point(0, 50)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(814, 261)
        Me.Panel2.TabIndex = 91
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'BackgroundWorker2
        '
        Me.BackgroundWorker2.WorkerSupportsCancellation = True
        '
        'P2Pool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(814, 311)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(814, 311)
        Me.MinimumSize = New System.Drawing.Size(814, 311)
        Me.Name = "P2Pool"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "P2Pool Node Scanner"
        Me.TabControl1.ResumeLayout(False)
        Me.Network1.ResumeLayout(False)
        Me.Network1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Network2.ResumeLayout(False)
        Me.Network2.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Wallet_Address As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Network1 As System.Windows.Forms.TabPage
    Friend WithEvents Network2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox11 As PictureBox
    Friend WithEvents PictureBox14 As PictureBox
    Friend WithEvents PictureBox15 As PictureBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
End Class
