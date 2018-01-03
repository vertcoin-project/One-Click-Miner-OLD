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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Uptime_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Uptime_Checker = New System.ComponentModel.BackgroundWorker()
        Me.Updater = New System.ComponentModel.BackgroundWorker()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Select_Data_Dir = New System.Windows.Forms.FolderBrowserDialog()
        Me.Auto_Update_Notify = New System.ComponentModel.BackgroundWorker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Clock = New System.Windows.Forms.Timer(Me.components)
        Me.Idle_Check = New System.Windows.Forms.Timer(Me.components)
        Me.Idle_Worker = New System.ComponentModel.BackgroundWorker()
        Me.Idle_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Form_Load = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'TextBox3
        '
        resources.ApplyResources(Me.TextBox3, "TextBox3")
        Me.TextBox3.BackColor = System.Drawing.Color.DarkSlateGray
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.ForeColor = System.Drawing.SystemColors.Control
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.Name = "Label19"
        '
        'MenuStrip
        '
        resources.ApplyResources(Me.MenuStrip, "MenuStrip")
        Me.MenuStrip.BackColor = System.Drawing.Color.DarkGreen
        Me.MenuStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'UpdateToolStripMenuItem
        '
        Me.UpdateToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.UpdateToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem"
        resources.ApplyResources(Me.UpdateToolStripMenuItem, "UpdateToolStripMenuItem")
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileDirectoryToolStripMenuItem, Me.SystemLogToolStripMenuItem, Me.P2PoolConfigToolStripMenuItem})
        Me.ViewToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        resources.ApplyResources(Me.ViewToolStripMenuItem, "ViewToolStripMenuItem")
        '
        'FileDirectoryToolStripMenuItem
        '
        Me.FileDirectoryToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.FileDirectoryToolStripMenuItem.Name = "FileDirectoryToolStripMenuItem"
        resources.ApplyResources(Me.FileDirectoryToolStripMenuItem, "FileDirectoryToolStripMenuItem")
        '
        'SystemLogToolStripMenuItem
        '
        Me.SystemLogToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.SystemLogToolStripMenuItem.Name = "SystemLogToolStripMenuItem"
        resources.ApplyResources(Me.SystemLogToolStripMenuItem, "SystemLogToolStripMenuItem")
        '
        'P2PoolConfigToolStripMenuItem
        '
        Me.P2PoolConfigToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.P2PoolConfigToolStripMenuItem.Name = "P2PoolConfigToolStripMenuItem"
        resources.ApplyResources(Me.P2PoolConfigToolStripMenuItem, "P2PoolConfigToolStripMenuItem")
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.ContactToolStripMenuItem})
        Me.HelpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
        '
        'ContactToolStripMenuItem
        '
        Me.ContactToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.ContactToolStripMenuItem.Name = "ContactToolStripMenuItem"
        resources.ApplyResources(Me.ContactToolStripMenuItem, "ContactToolStripMenuItem")
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
        resources.ApplyResources(Me.NotifyIcon1, "NotifyIcon1")
        '
        'CheckBox1
        '
        resources.ApplyResources(Me.CheckBox1, "CheckBox1")
        Me.CheckBox1.Name = "CheckBox1"
        Me.ToolTip.SetToolTip(Me.CheckBox1, resources.GetString("CheckBox1.ToolTip"))
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.BackColor = System.Drawing.Color.DarkGreen
        Me.Button1.ForeColor = System.Drawing.SystemColors.Control
        Me.Button1.Name = "Button1"
        Me.ToolTip.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip"))
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.DarkGreen
        resources.ApplyResources(Me.Button3, "Button3")
        Me.Button3.ForeColor = System.Drawing.SystemColors.Control
        Me.Button3.Name = "Button3"
        Me.ToolTip.SetToolTip(Me.Button3, resources.GetString("Button3.ToolTip"))
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.BackColor = System.Drawing.Color.DarkGreen
        Me.Button2.ForeColor = System.Drawing.SystemColors.Control
        Me.Button2.Name = "Button2"
        Me.ToolTip.SetToolTip(Me.Button2, resources.GetString("Button2.ToolTip"))
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button4
        '
        resources.ApplyResources(Me.Button4, "Button4")
        Me.Button4.BackColor = System.Drawing.Color.DarkGreen
        Me.Button4.ForeColor = System.Drawing.SystemColors.Control
        Me.Button4.Name = "Button4"
        Me.ToolTip.SetToolTip(Me.Button4, resources.GetString("Button4.ToolTip"))
        Me.Button4.UseVisualStyleBackColor = False
        '
        'CheckBox2
        '
        resources.ApplyResources(Me.CheckBox2, "CheckBox2")
        Me.CheckBox2.Name = "CheckBox2"
        Me.ToolTip.SetToolTip(Me.CheckBox2, resources.GetString("CheckBox2.ToolTip"))
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'PictureBox9
        '
        resources.ApplyResources(Me.PictureBox9, "PictureBox9")
        Me.PictureBox9.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.help_small
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.TabStop = False
        Me.ToolTip.SetToolTip(Me.PictureBox9, resources.GetString("PictureBox9.ToolTip"))
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.help_small
        resources.ApplyResources(Me.PictureBox3, "PictureBox3")
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.TabStop = False
        Me.ToolTip.SetToolTip(Me.PictureBox3, resources.GetString("PictureBox3.ToolTip"))
        '
        'Uptime_Timer
        '
        Me.Uptime_Timer.Interval = 500
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
        resources.ApplyResources(Me.WebBrowser1, "WebBrowser1")
        Me.WebBrowser1.Name = "WebBrowser1"
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
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.BackColor = System.Drawing.Color.DarkGreen
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.PictureBox6)
        Me.Panel1.Controls.Add(Me.PictureBox4)
        Me.Panel1.Controls.Add(Me.MenuStrip)
        Me.Panel1.ForeColor = System.Drawing.SystemColors.Control
        Me.Panel1.Name = "Panel1"
        '
        'PictureBox2
        '
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.greenplus
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.ocmlogo
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'PictureBox6
        '
        resources.ApplyResources(Me.PictureBox6, "PictureBox6")
        Me.PictureBox6.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.minimize
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.TabStop = False
        '
        'PictureBox4
        '
        resources.ApplyResources(Me.PictureBox4, "PictureBox4")
        Me.PictureBox4.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.greenx
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.TabStop = False
        '
        'Panel3
        '
        resources.ApplyResources(Me.Panel3, "Panel3")
        Me.Panel3.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.CheckBox2)
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
        Me.Panel3.ForeColor = System.Drawing.SystemColors.Control
        Me.Panel3.Name = "Panel3"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        resources.ApplyResources(Me.DataGridView1, "DataGridView1")
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        '
        'ComboBox1
        '
        resources.ApplyResources(Me.ComboBox1, "ComboBox1")
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {resources.GetString("ComboBox1.Items"), resources.GetString("ComboBox1.Items1"), resources.GetString("ComboBox1.Items2"), resources.GetString("ComboBox1.Items3")})
        Me.ComboBox1.Name = "ComboBox1"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label7.ForeColor = System.Drawing.Color.Yellow
        Me.Label7.Name = "Label7"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'TextBox1
        '
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.TabStop = False
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.TextBox2, "TextBox2")
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.TabStop = False
        '
        'Clock
        '
        Me.Clock.Enabled = True
        Me.Clock.Interval = 1000
        '
        'Idle_Check
        '
        Me.Idle_Check.Interval = 10000
        '
        'Idle_Worker
        '
        '
        'Idle_Timer
        '
        Me.Idle_Timer.Interval = 1000
        '
        'Form_Load
        '
        Me.Form_Load.Interval = 250
        '
        'Main
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.WebBrowser1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Idle_Check As Timer
    Friend WithEvents Idle_Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents Idle_Timer As Timer
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Form_Load As Timer
End Class
