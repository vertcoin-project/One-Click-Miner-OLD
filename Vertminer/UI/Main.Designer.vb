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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MinerHashRateLabel = New System.Windows.Forms.Label()
        Me.tbMinerHashrate = New System.Windows.Forms.TextBox()
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
        Me.cbRunOnLocalNode = New System.Windows.Forms.CheckBox()
        Me.bFindP2PoolNodes = New System.Windows.Forms.Button()
        Me.bStartButton = New System.Windows.Forms.Button()
        Me.bAddPool = New System.Windows.Forms.Button()
        Me.bRemovePool = New System.Windows.Forms.Button()
        Me.cbSelectAllPools = New System.Windows.Forms.CheckBox()
        Me.HelpIcon = New System.Windows.Forms.PictureBox()
        Me.Uptime_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Uptime_Checker = New System.ComponentModel.BackgroundWorker()
        Me.Updater = New System.ComponentModel.BackgroundWorker()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Select_Data_Dir = New System.Windows.Forms.FolderBrowserDialog()
        Me.Auto_Update_Notify = New System.ComponentModel.BackgroundWorker()
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.VertMinerFeeLabel = New System.Windows.Forms.Label()
        Me.lHardwareType = New System.Windows.Forms.Label()
        Me.PoolDataGrid = New System.Windows.Forms.DataGridView()
        Me.cbMinerSelect = New System.Windows.Forms.ComboBox()
        Me.lMinerLabel = New System.Windows.Forms.Label()
        Me.UpdateNotificationText = New System.Windows.Forms.Label()
        Me.lLocalP2PoolLabel = New System.Windows.Forms.Label()
        Me.tbLocalPoolStatus = New System.Windows.Forms.TextBox()
        Me.tbMinerStatus = New System.Windows.Forms.TextBox()
        Me.Clock = New System.Windows.Forms.Timer(Me.components)
        Me.Idle_Check = New System.Windows.Forms.Timer(Me.components)
        Me.Idle_Worker = New System.ComponentModel.BackgroundWorker()
        Me.Idle_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Form_Load = New System.Windows.Forms.Timer(Me.components)
        Me.PoolDataCollectionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.IsSelectedDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PoolDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WorkerDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PasswordDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStrip.SuspendLayout()
        CType(Me.HelpIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainPanel.SuspendLayout()
        CType(Me.PoolDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PoolDataCollectionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MinerHashRateLabel
        '
        resources.ApplyResources(Me.MinerHashRateLabel, "MinerHashRateLabel")
        Me.MinerHashRateLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.MinerHashRateLabel.Name = "MinerHashRateLabel"
        '
        'tbMinerHashrate
        '
        resources.ApplyResources(Me.tbMinerHashrate, "tbMinerHashrate")
        Me.tbMinerHashrate.BackColor = System.Drawing.SystemColors.Control
        Me.tbMinerHashrate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbMinerHashrate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.tbMinerHashrate.Name = "tbMinerHashrate"
        Me.tbMinerHashrate.ReadOnly = True
        '
        'MenuStrip
        '
        resources.ApplyResources(Me.MenuStrip, "MenuStrip")
        Me.MenuStrip.BackColor = System.Drawing.Color.DimGray
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
        'cbRunOnLocalNode
        '
        resources.ApplyResources(Me.cbRunOnLocalNode, "cbRunOnLocalNode")
        Me.cbRunOnLocalNode.BackColor = System.Drawing.SystemColors.Control
        Me.cbRunOnLocalNode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.cbRunOnLocalNode.Name = "cbRunOnLocalNode"
        Me.ToolTip.SetToolTip(Me.cbRunOnLocalNode, resources.GetString("cbRunOnLocalNode.ToolTip"))
        Me.cbRunOnLocalNode.UseVisualStyleBackColor = False
        '
        'bFindP2PoolNodes
        '
        resources.ApplyResources(Me.bFindP2PoolNodes, "bFindP2PoolNodes")
        Me.bFindP2PoolNodes.BackColor = System.Drawing.SystemColors.Control
        Me.bFindP2PoolNodes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.bFindP2PoolNodes.Name = "bFindP2PoolNodes"
        Me.ToolTip.SetToolTip(Me.bFindP2PoolNodes, resources.GetString("bFindP2PoolNodes.ToolTip"))
        Me.bFindP2PoolNodes.UseVisualStyleBackColor = False
        '
        'bStartButton
        '
        Me.bStartButton.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.bStartButton, "bStartButton")
        Me.bStartButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.bStartButton.Name = "bStartButton"
        Me.ToolTip.SetToolTip(Me.bStartButton, resources.GetString("bStartButton.ToolTip"))
        Me.bStartButton.UseVisualStyleBackColor = False
        '
        'bAddPool
        '
        resources.ApplyResources(Me.bAddPool, "bAddPool")
        Me.bAddPool.BackColor = System.Drawing.SystemColors.Control
        Me.bAddPool.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.bAddPool.Name = "bAddPool"
        Me.ToolTip.SetToolTip(Me.bAddPool, resources.GetString("bAddPool.ToolTip"))
        Me.bAddPool.UseVisualStyleBackColor = False
        '
        'bRemovePool
        '
        resources.ApplyResources(Me.bRemovePool, "bRemovePool")
        Me.bRemovePool.BackColor = System.Drawing.SystemColors.Control
        Me.bRemovePool.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.bRemovePool.Name = "bRemovePool"
        Me.ToolTip.SetToolTip(Me.bRemovePool, resources.GetString("bRemovePool.ToolTip"))
        Me.bRemovePool.UseVisualStyleBackColor = False
        '
        'cbSelectAllPools
        '
        resources.ApplyResources(Me.cbSelectAllPools, "cbSelectAllPools")
        Me.cbSelectAllPools.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.cbSelectAllPools.Name = "cbSelectAllPools"
        Me.ToolTip.SetToolTip(Me.cbSelectAllPools, resources.GetString("cbSelectAllPools.ToolTip"))
        Me.cbSelectAllPools.UseVisualStyleBackColor = True
        '
        'HelpIcon
        '
        Me.HelpIcon.Image = Global.VertcoinOneClickMiner.My.Resources.Resources.help_small
        resources.ApplyResources(Me.HelpIcon, "HelpIcon")
        Me.HelpIcon.Name = "HelpIcon"
        Me.HelpIcon.TabStop = False
        Me.ToolTip.SetToolTip(Me.HelpIcon, resources.GetString("HelpIcon.ToolTip"))
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
        'MainPanel
        '
        Me.MainPanel.BackColor = System.Drawing.SystemColors.Control
        Me.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MainPanel.Controls.Add(Me.VertMinerFeeLabel)
        Me.MainPanel.Controls.Add(Me.lHardwareType)
        Me.MainPanel.Controls.Add(Me.cbSelectAllPools)
        Me.MainPanel.Controls.Add(Me.bRemovePool)
        Me.MainPanel.Controls.Add(Me.PoolDataGrid)
        Me.MainPanel.Controls.Add(Me.bAddPool)
        Me.MainPanel.Controls.Add(Me.bStartButton)
        Me.MainPanel.Controls.Add(Me.cbMinerSelect)
        Me.MainPanel.Controls.Add(Me.bFindP2PoolNodes)
        Me.MainPanel.Controls.Add(Me.lMinerLabel)
        Me.MainPanel.Controls.Add(Me.UpdateNotificationText)
        Me.MainPanel.Controls.Add(Me.MinerHashRateLabel)
        Me.MainPanel.Controls.Add(Me.lLocalP2PoolLabel)
        Me.MainPanel.Controls.Add(Me.tbMinerHashrate)
        Me.MainPanel.Controls.Add(Me.tbLocalPoolStatus)
        Me.MainPanel.Controls.Add(Me.tbMinerStatus)
        Me.MainPanel.Controls.Add(Me.cbRunOnLocalNode)
        Me.MainPanel.Controls.Add(Me.HelpIcon)
        resources.ApplyResources(Me.MainPanel, "MainPanel")
        Me.MainPanel.ForeColor = System.Drawing.SystemColors.Control
        Me.MainPanel.Name = "MainPanel"
        '
        'VertMinerFeeLabel
        '
        resources.ApplyResources(Me.VertMinerFeeLabel, "VertMinerFeeLabel")
        Me.VertMinerFeeLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VertMinerFeeLabel.Name = "VertMinerFeeLabel"
        '
        'lHardwareType
        '
        resources.ApplyResources(Me.lHardwareType, "lHardwareType")
        Me.lHardwareType.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lHardwareType.Name = "lHardwareType"
        '
        'PoolDataGrid
        '
        Me.PoolDataGrid.AllowUserToAddRows = False
        Me.PoolDataGrid.AllowUserToDeleteRows = False
        Me.PoolDataGrid.AllowUserToResizeRows = False
        Me.PoolDataGrid.AutoGenerateColumns = False
        Me.PoolDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.PoolDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PoolDataGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.PoolDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PoolDataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IsSelectedDataGridViewCheckBoxColumn, Me.DescriptionDataGridViewTextBoxColumn, Me.PoolDataGridViewTextBoxColumn, Me.WorkerDataGridViewTextBoxColumn, Me.PasswordDataGridViewTextBoxColumn})
        Me.PoolDataGrid.DataSource = Me.PoolDataCollectionBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PoolDataGrid.DefaultCellStyle = DataGridViewCellStyle2
        resources.ApplyResources(Me.PoolDataGrid, "PoolDataGrid")
        Me.PoolDataGrid.Name = "PoolDataGrid"
        Me.PoolDataGrid.RowHeadersVisible = False
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PoolDataGrid.RowsDefaultCellStyle = DataGridViewCellStyle3
        '
        'cbMinerSelect
        '
        resources.ApplyResources(Me.cbMinerSelect, "cbMinerSelect")
        Me.cbMinerSelect.FormattingEnabled = True
        Me.cbMinerSelect.Items.AddRange(New Object() {resources.GetString("cbMinerSelect.Items"), resources.GetString("cbMinerSelect.Items1"), resources.GetString("cbMinerSelect.Items2"), resources.GetString("cbMinerSelect.Items3")})
        Me.cbMinerSelect.Name = "cbMinerSelect"
        '
        'lMinerLabel
        '
        resources.ApplyResources(Me.lMinerLabel, "lMinerLabel")
        Me.lMinerLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lMinerLabel.Name = "lMinerLabel"
        '
        'UpdateNotificationText
        '
        resources.ApplyResources(Me.UpdateNotificationText, "UpdateNotificationText")
        Me.UpdateNotificationText.Cursor = System.Windows.Forms.Cursors.Hand
        Me.UpdateNotificationText.ForeColor = System.Drawing.Color.Red
        Me.UpdateNotificationText.Name = "UpdateNotificationText"
        '
        'lLocalP2PoolLabel
        '
        resources.ApplyResources(Me.lLocalP2PoolLabel, "lLocalP2PoolLabel")
        Me.lLocalP2PoolLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lLocalP2PoolLabel.Name = "lLocalP2PoolLabel"
        '
        'tbLocalPoolStatus
        '
        resources.ApplyResources(Me.tbLocalPoolStatus, "tbLocalPoolStatus")
        Me.tbLocalPoolStatus.BackColor = System.Drawing.SystemColors.Control
        Me.tbLocalPoolStatus.Name = "tbLocalPoolStatus"
        Me.tbLocalPoolStatus.ReadOnly = True
        Me.tbLocalPoolStatus.TabStop = False
        '
        'tbMinerStatus
        '
        Me.tbMinerStatus.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.tbMinerStatus, "tbMinerStatus")
        Me.tbMinerStatus.Name = "tbMinerStatus"
        Me.tbMinerStatus.ReadOnly = True
        Me.tbMinerStatus.TabStop = False
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
        'PoolDataCollectionBindingSource
        '
        Me.PoolDataCollectionBindingSource.DataSource = GetType(VertcoinOneClickMiner.PoolDataCollection)
        '
        'IsSelectedDataGridViewCheckBoxColumn
        '
        Me.IsSelectedDataGridViewCheckBoxColumn.DataPropertyName = "IsSelected"
        resources.ApplyResources(Me.IsSelectedDataGridViewCheckBoxColumn, "IsSelectedDataGridViewCheckBoxColumn")
        Me.IsSelectedDataGridViewCheckBoxColumn.Name = "IsSelectedDataGridViewCheckBoxColumn"
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        resources.ApplyResources(Me.DescriptionDataGridViewTextBoxColumn, "DescriptionDataGridViewTextBoxColumn")
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        '
        'PoolDataGridViewTextBoxColumn
        '
        Me.PoolDataGridViewTextBoxColumn.DataPropertyName = "Pool"
        resources.ApplyResources(Me.PoolDataGridViewTextBoxColumn, "PoolDataGridViewTextBoxColumn")
        Me.PoolDataGridViewTextBoxColumn.Name = "PoolDataGridViewTextBoxColumn"
        '
        'WorkerDataGridViewTextBoxColumn
        '
        Me.WorkerDataGridViewTextBoxColumn.DataPropertyName = "Worker"
        resources.ApplyResources(Me.WorkerDataGridViewTextBoxColumn, "WorkerDataGridViewTextBoxColumn")
        Me.WorkerDataGridViewTextBoxColumn.Name = "WorkerDataGridViewTextBoxColumn"
        '
        'PasswordDataGridViewTextBoxColumn
        '
        Me.PasswordDataGridViewTextBoxColumn.DataPropertyName = "Password"
        resources.ApplyResources(Me.PasswordDataGridViewTextBoxColumn, "PasswordDataGridViewTextBoxColumn")
        Me.PasswordDataGridViewTextBoxColumn.Name = "PasswordDataGridViewTextBoxColumn"
        '
        'Main
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainPanel)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.WebBrowser1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        CType(Me.HelpIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainPanel.ResumeLayout(False)
        Me.MainPanel.PerformLayout()
        CType(Me.PoolDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PoolDataCollectionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
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
    Friend WithEvents MinerHashRateLabel As Label
    Friend WithEvents tbMinerHashrate As TextBox
    Friend WithEvents MainPanel As System.Windows.Forms.Panel
    Friend WithEvents bStartButton As Button
    Friend WithEvents cbMinerSelect As ComboBox
    Friend WithEvents bFindP2PoolNodes As Button
    Friend WithEvents lMinerLabel As Label
    Friend WithEvents UpdateNotificationText As Label
    Friend WithEvents lLocalP2PoolLabel As Label
    Friend WithEvents tbLocalPoolStatus As TextBox
    Friend WithEvents tbMinerStatus As TextBox
    Friend WithEvents cbRunOnLocalNode As CheckBox
    Friend WithEvents HelpIcon As PictureBox
    Friend WithEvents Clock As Timer
    Friend WithEvents bAddPool As Button
    Friend WithEvents bRemovePool As Button
    Friend WithEvents PoolDataGrid As DataGridView
    Friend WithEvents cbSelectAllPools As CheckBox
    Friend WithEvents Idle_Check As Timer
    Friend WithEvents Idle_Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents Idle_Timer As Timer
    Friend WithEvents Form_Load As Timer
    Friend WithEvents VertMinerFeeLabel As Label
    Friend WithEvents lHardwareType As Label
    Friend WithEvents IsSelectedDataGridViewCheckBoxColumn As DataGridViewCheckBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PoolDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents WorkerDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PasswordDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PoolDataCollectionBindingSource As BindingSource
End Class
