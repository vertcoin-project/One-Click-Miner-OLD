<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileDirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SystemLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.P2PoolConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.P2PoolWebInterfaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContactToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateStatsInterval = New System.Windows.Forms.Timer(Me.components)
        Me.UpdateStats = New System.ComponentModel.BackgroundWorker()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Uptime_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Uptime_Checker = New System.ComponentModel.BackgroundWorker()
        Me.Updater = New System.ComponentModel.BackgroundWorker()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Select_Data_Dir = New System.Windows.Forms.FolderBrowserDialog()
        Me.Auto_Update_Notify = New System.ComponentModel.BackgroundWorker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Clock = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(146, 196)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(146, 22)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Miner Hashrate"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.DarkSlateGray
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Font = New System.Drawing.Font("MS Reference Sans Serif", 29.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.ForeColor = System.Drawing.SystemColors.Control
        Me.TextBox3.Location = New System.Drawing.Point(47, 224)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(341, 57)
        Me.TextBox3.TabIndex = 40
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(153, 83)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(129, 22)
        Me.Label19.TabIndex = 21
        Me.Label19.Text = "Pool Address"
        '
        'MenuStrip
        '
        Me.MenuStrip.AutoSize = False
        Me.MenuStrip.BackColor = System.Drawing.Color.DarkGreen
        Me.MenuStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(59, 2)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip.Size = New System.Drawing.Size(273, 50)
        Me.MenuStrip.TabIndex = 85
        Me.MenuStrip.Text = "MenuStrip"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(53, 46)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'UpdateToolStripMenuItem
        '
        Me.UpdateToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.UpdateToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem"
        Me.UpdateToolStripMenuItem.Size = New System.Drawing.Size(140, 28)
        Me.UpdateToolStripMenuItem.Text = "Update"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(140, 28)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(88, 46)
        Me.EditToolStripMenuItem.Text = "Settings"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileDirectoryToolStripMenuItem, Me.SystemLogToolStripMenuItem, Me.P2PoolConfigToolStripMenuItem, Me.P2PoolWebInterfaceToolStripMenuItem})
        Me.ViewToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(64, 46)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'FileDirectoryToolStripMenuItem
        '
        Me.FileDirectoryToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.FileDirectoryToolStripMenuItem.Name = "FileDirectoryToolStripMenuItem"
        Me.FileDirectoryToolStripMenuItem.Size = New System.Drawing.Size(261, 28)
        Me.FileDirectoryToolStripMenuItem.Text = "Miner Config"
        '
        'SystemLogToolStripMenuItem
        '
        Me.SystemLogToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.SystemLogToolStripMenuItem.Name = "SystemLogToolStripMenuItem"
        Me.SystemLogToolStripMenuItem.Size = New System.Drawing.Size(261, 28)
        Me.SystemLogToolStripMenuItem.Text = "One-Click Log"
        '
        'P2PoolConfigToolStripMenuItem
        '
        Me.P2PoolConfigToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.P2PoolConfigToolStripMenuItem.Name = "P2PoolConfigToolStripMenuItem"
        Me.P2PoolConfigToolStripMenuItem.Size = New System.Drawing.Size(261, 28)
        Me.P2PoolConfigToolStripMenuItem.Text = "P2Pool Config"
        '
        'P2PoolWebInterfaceToolStripMenuItem
        '
        Me.P2PoolWebInterfaceToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.P2PoolWebInterfaceToolStripMenuItem.Name = "P2PoolWebInterfaceToolStripMenuItem"
        Me.P2PoolWebInterfaceToolStripMenuItem.Size = New System.Drawing.Size(261, 28)
        Me.P2PoolWebInterfaceToolStripMenuItem.Text = "P2Pool Web Interface"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.ContactToolStripMenuItem})
        Me.HelpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(62, 46)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(143, 28)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ContactToolStripMenuItem
        '
        Me.ContactToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.ContactToolStripMenuItem.Name = "ContactToolStripMenuItem"
        Me.ContactToolStripMenuItem.Size = New System.Drawing.Size(143, 28)
        Me.ContactToolStripMenuItem.Text = "Contact"
        '
        'UpdateStatsInterval
        '
        Me.UpdateStatsInterval.Enabled = True
        Me.UpdateStatsInterval.Interval = 1000
        '
        'UpdateStats
        '
        Me.UpdateStats.WorkerSupportsCancellation = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Vertcoin One-Click Miner"
        Me.NotifyIcon1.Visible = True
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(300, 56)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(118, 17)
        Me.CheckBox1.TabIndex = 6
        Me.CheckBox1.Text = "Run Local Node"
        Me.ToolTip.SetToolTip(Me.CheckBox1, "Select this to point your miner directly to your local p2pool node.")
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DarkGreen
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.Control
        Me.Button1.Location = New System.Drawing.Point(156, 27)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(123, 22)
        Me.Button1.TabIndex = 44
        Me.Button1.Text = "Find P2Pool Nodes"
        Me.ToolTip.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip"))
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.DarkGreen
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.Control
        Me.Button3.Location = New System.Drawing.Point(97, 55)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(47, 23)
        Me.Button3.TabIndex = 90
        Me.Button3.Text = "Start"
        Me.ToolTip.SetToolTip(Me.Button3, "Toggles your miner on/off.")
        Me.Button3.UseVisualStyleBackColor = False
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.help_small
        Me.PictureBox9.Location = New System.Drawing.Point(278, 88)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(15, 15)
        Me.PictureBox9.TabIndex = 39
        Me.PictureBox9.TabStop = False
        Me.ToolTip.SetToolTip(Me.PictureBox9, resources.GetString("PictureBox9.ToolTip"))
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.help_small
        Me.PictureBox3.Location = New System.Drawing.Point(109, 9)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(15, 15)
        Me.PictureBox3.TabIndex = 28
        Me.PictureBox3.TabStop = False
        Me.ToolTip.SetToolTip(Me.PictureBox3, "Select your hardware type to launch the respective miner for your hardware.")
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DarkGreen
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.Control
        Me.Button2.Location = New System.Drawing.Point(332, 199)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(91, 23)
        Me.Button2.TabIndex = 91
        Me.Button2.Text = "Add Pool"
        Me.ToolTip.SetToolTip(Me.Button2, "Toggles your miner on/off.")
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.DarkGreen
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.SystemColors.Control
        Me.Button4.Location = New System.Drawing.Point(12, 199)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(91, 23)
        Me.Button4.TabIndex = 93
        Me.Button4.Text = "Remove Pool"
        Me.ToolTip.SetToolTip(Me.Button4, "Toggles your miner on/off.")
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Uptime_Timer
        '
        Me.Uptime_Timer.Interval = 1000
        '
        'Uptime_Checker
        '
        Me.Uptime_Checker.WorkerSupportsCancellation = True
        '
        'Updater
        '
        Me.Updater.WorkerSupportsCancellation = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(780, 213)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.TabIndex = 86
        Me.WebBrowser1.Visible = False
        '
        'Select_Data_Dir
        '
        Me.Select_Data_Dir.RootFolder = System.Environment.SpecialFolder.MyComputer
        Me.Select_Data_Dir.ShowNewFolderButton = False
        '
        'Auto_Update_Notify
        '
        Me.Auto_Update_Notify.WorkerSupportsCancellation = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkGreen
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.PictureBox6)
        Me.Panel1.Controls.Add(Me.PictureBox4)
        Me.Panel1.Controls.Add(Me.MenuStrip)
        Me.Panel1.ForeColor = System.Drawing.SystemColors.Control
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(435, 52)
        Me.Panel1.TabIndex = 88
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.ocmlogo
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 90
        Me.PictureBox1.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.minimize
        Me.PictureBox6.Location = New System.Drawing.Point(351, 13)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 92
        Me.PictureBox6.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.greenx
        Me.PictureBox4.Location = New System.Drawing.Point(389, 13)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 91
        Me.PictureBox4.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Panel3.Controls.Add(Me.Button4)
        Me.Panel3.Controls.Add(Me.DataGridView1)
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.ComboBox1)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.TextBox3)
        Me.Panel3.Controls.Add(Me.TextBox1)
        Me.Panel3.Controls.Add(Me.PictureBox9)
        Me.Panel3.Controls.Add(Me.TextBox2)
        Me.Panel3.Controls.Add(Me.CheckBox1)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.PictureBox3)
        Me.Panel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.SystemColors.Control
        Me.Panel3.Location = New System.Drawing.Point(0, 50)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(435, 285)
        Me.Panel3.TabIndex = 91
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
        Me.DataGridView1.Location = New System.Drawing.Point(12, 108)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(411, 85)
        Me.DataGridView1.TabIndex = 92
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"AMD", "NVIDIA", "CPU"})
        Me.ComboBox1.Location = New System.Drawing.Point(12, 56)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(79, 21)
        Me.ComboBox1.TabIndex = 88
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(53, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 18)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Miner"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label7.Enabled = False
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Yellow
        Me.Label7.Location = New System.Drawing.Point(150, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(135, 17)
        Me.Label7.TabIndex = 87
        Me.Label7.Text = "Update Available!"
        Me.Label7.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(303, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 18)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Local P2Pool"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(291, 27)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(132, 22)
        Me.TextBox1.TabIndex = 43
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "Offline"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(12, 27)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(132, 22)
        Me.TextBox2.TabIndex = 16
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "Offline"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Clock
        '
        Me.Clock.Enabled = True
        Me.Clock.Interval = 1000
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 335)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.WebBrowser1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(435, 335)
        Me.MinimumSize = New System.Drawing.Size(435, 335)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vertcoin One-Click Miner - BETA"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileDirectoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContactToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateStatsInterval As System.Windows.Forms.Timer
    Public WithEvents UpdateStats As System.ComponentModel.BackgroundWorker
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents Uptime_Timer As System.Windows.Forms.Timer
    Friend WithEvents Uptime_Checker As System.ComponentModel.BackgroundWorker
    Friend WithEvents Updater As System.ComponentModel.BackgroundWorker
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents Select_Data_Dir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Auto_Update_Notify As System.ComponentModel.BackgroundWorker
    Friend WithEvents P2PoolConfigToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents P2PoolWebInterfaceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button3 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Clock As Timer
    Friend WithEvents Button2 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents DataGridView1 As DataGridView
End Class
