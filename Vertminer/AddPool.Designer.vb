<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddPool
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
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.PictureBox14 = New System.Windows.Forms.PictureBox()
        Me.PictureBox15 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Wallet_Address = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Pool_Address = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Password_Address = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BackgroundWorker2
        '
        Me.BackgroundWorker2.WorkerSupportsCancellation = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(67, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 26)
        Me.Label6.TabIndex = 93
        Me.Label6.Text = "Add Pool"
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
        Me.PictureBox14.Location = New System.Drawing.Point(243, 13)
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
        Me.PictureBox15.Location = New System.Drawing.Point(281, 13)
        Me.PictureBox15.Name = "PictureBox15"
        Me.PictureBox15.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox15.TabIndex = 91
        Me.PictureBox15.TabStop = False
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
        Me.Panel1.Size = New System.Drawing.Size(316, 52)
        Me.Panel1.TabIndex = 92
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DarkGreen
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.Control
        Me.Button1.Location = New System.Drawing.Point(116, 129)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(82, 26)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Add Pool"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Wallet_Address
        '
        Me.Wallet_Address.BackColor = System.Drawing.SystemColors.Window
        Me.Wallet_Address.Location = New System.Drawing.Point(20, 103)
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
        Me.Label3.Location = New System.Drawing.Point(49, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(130, 20)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "Wallet Address"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Panel2.Controls.Add(Me.Pool_Address)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Password_Address)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.Wallet_Address)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(0, 50)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(316, 176)
        Me.Panel2.TabIndex = 93
        '
        'Pool_Address
        '
        Me.Pool_Address.BackColor = System.Drawing.SystemColors.Window
        Me.Pool_Address.Location = New System.Drawing.Point(20, 44)
        Me.Pool_Address.MaxLength = 100
        Me.Pool_Address.Name = "Pool_Address"
        Me.Pool_Address.Size = New System.Drawing.Size(274, 20)
        Me.Pool_Address.TabIndex = 47
        Me.Pool_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(100, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 20)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Pool Address"
        '
        'Password_Address
        '
        Me.Password_Address.BackColor = System.Drawing.SystemColors.Window
        Me.Password_Address.Location = New System.Drawing.Point(215, 103)
        Me.Password_Address.MaxLength = 100
        Me.Password_Address.Name = "Password_Address"
        Me.Password_Address.Size = New System.Drawing.Size(79, 20)
        Me.Password_Address.TabIndex = 45
        Me.Password_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(211, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 20)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Password"
        '
        'AddPool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(316, 226)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximumSize = New System.Drawing.Size(316, 226)
        Me.MinimumSize = New System.Drawing.Size(316, 226)
        Me.Name = "AddPool"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AddPool"
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox11 As PictureBox
    Friend WithEvents PictureBox14 As PictureBox
    Friend WithEvents PictureBox15 As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Wallet_Address As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Password_Address As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Pool_Address As TextBox
    Friend WithEvents Label2 As Label
End Class
