Imports System.Environment
Imports System.IO
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports VertcoinOneClickMiner.Core

Public Class settings
    Private ReadOnly _logger As ILogger
    Dim JSONConverter As JavaScriptSerializer = New JavaScriptSerializer()

    Public Sub New(logger As ILogger)
        InitializeComponent()
        _logger = logger
    End Sub

    Private Sub settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Invoke(New MethodInvoker(AddressOf Style))
            If keep_miner_alive = True Then
                CheckBox6.Checked = True
            Else
                CheckBox6.Checked = False
            End If
            If keep_p2pool_alive = True Then
                CheckBox5.Checked = True
            Else
                CheckBox5.Checked = False
            End If
            If start_minimized = True Then
                CheckBox3.Checked = True
            Else
                CheckBox3.Checked = False
            End If
            If use_upnp = True Then
                CheckBox4.Checked = True
            Else
                CheckBox4.Checked = False
            End If
            If start_with_windows = True Then
                CheckBox9.Checked = True
            Else
                CheckBox9.Checked = False
            End If
            If autostart_p2pool = True Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            If autostart_mining = True Then
                CheckBox7.Checked = True
            Else
                CheckBox7.Checked = False
            End If
            If show_cli = True Then
                CheckBox2.Checked = True
            Else
                CheckBox2.Checked = False
            End If
            If mine_when_idle = True Then
                CheckBox10.Checked = True
            Else
                CheckBox10.Checked = False
            End If
            If p2pool_network = "1" Then
                If p2pool_port = "9347" Or p2pool_port = "" Then
                    p2pool_port = "9346"
                End If
                If mining_port = "9181" Or mining_port = "" Then
                    mining_port = "9171"
                End If
            ElseIf p2pool_network = "2" Then
                If p2pool_port = "9346" Or p2pool_port = "" Then
                    p2pool_port = "9347"
                End If
                If mining_port = "9171" Or mining_port = "" Then
                    mining_port = "9181"
                End If
            End If
            Node_Fee.Text = p2pool_node_fee & "%"
            Node_Donation.Text = p2pool_donation & "%"
            TextBox3.Text = max_connections
            TextBox4.Text = p2pool_port
            TextBox5.Text = mining_port
            Intensity_Text.Text = mining_intensity
            Devices_Text.Text = devices
            Fee_Address_Text.Text = p2pool_fee_address
            ComboBox1.SelectedItem = p2pool_network
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
        Finally
            _logger.Trace("Loaded: OK.")
        End Try

    End Sub

    Private Sub Settings_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        Try
            If Not Intensity_Text.Text = "" Then
                mining_intensity = Convert.ToDecimal(Intensity_Text.Text)
            End If
            If Devices_Text.Text = "" Then
                devices = ""
            Else
                devices = Devices_Text.Text
            End If
            If Not Node_Fee.Text = "" Then
                p2pool_node_fee = Convert.ToDecimal(Node_Fee.Text.Replace("%", ""))
            End If
            If Not Node_Donation.Text = "" Then
                p2pool_donation = Convert.ToDecimal(Node_Donation.Text.Replace("%", ""))
            End If
            If Not TextBox3.Text = "" Then
                max_connections = TextBox3.Text
            End If
            If Fee_Address_Text.Text = "" Then
                p2pool_fee_address = "VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG" 'If fee address is empty, default to Dev Donation address to keep config alive.
            Else
                p2pool_fee_address = Fee_Address_Text.Text
            End If
            If Not TextBox4.Text = "" Then
                p2pool_port = TextBox4.Text
            End If
            If Not TextBox5.Text = "" Then
                mining_port = TextBox5.Text
            End If
            If CheckBox3.Checked = True Then
                start_minimized = True
            Else
                start_minimized = False
            End If
            If CheckBox4.Checked = True Then
                use_upnp = True
            Else
                use_upnp = False
            End If
            If CheckBox1.Checked = True Then
                autostart_p2pool = True
            Else
                autostart_p2pool = False
            End If
            If CheckBox7.Checked = True Then
                autostart_mining = True
            Else
                autostart_mining = False
            End If
            If CheckBox6.Checked = True Then
                keep_miner_alive = True
            Else
                keep_miner_alive = False
            End If
            If CheckBox5.Checked = True Then
                keep_p2pool_alive = True
            Else
                keep_p2pool_alive = False
            End If
            If CheckBox9.Checked = True Then
                start_with_windows = True
            Else
                start_with_windows = False
            End If
            If CheckBox2.Checked = True Then
                show_cli = True
            Else
                show_cli = False
            End If
            If CheckBox10.Checked = True Then
                mine_when_idle = True
            Else
                mine_when_idle = False
            End If
            If ComboBox1.SelectedItem = "1" Then
                p2pool_network = "1"
            ElseIf ComboBox1.SelectedItem = "2" Then
                p2pool_network = "2"
            End If
            'Saves current settings.
            Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
            'Adds startup entry to registry after settings have been saved.
            Invoke(New MethodInvoker(AddressOf Main.StartWithWindows))
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
        Finally
            _logger.Trace("Closed: OK.")
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim result1 As DialogResult = MsgBox("Please select the location of your Vertcoin Data Directory.", MessageBoxButtons.OKCancel)
        If result1 = DialogResult.OK Then
            Dim result2 As Windows.Forms.DialogResult = dir_browse.ShowDialog()
            If result2 = Windows.Forms.DialogResult.OK Then
                appdata = dir_browse.SelectedPath
            End If
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim result2 As DialogResult = MsgBox("Would you like to reset the OCM settings to default?", MessageBoxButtons.OKCancel)
        If result2 = DialogResult.OK Then
            Try
                CheckBox9.Checked = False
                CheckBox4.Checked = False
                CheckBox3.Checked = False
                CheckBox2.Checked = False
                CheckBox7.Checked = False
                CheckBox1.Checked = False
                CheckBox5.Checked = False
                CheckBox6.Checked = False
                CheckBox10.Checked = False
                ComboBox1.SelectedIndex = 0
                Node_Fee.Text = "0"
                Node_Donation.Text = "1"
                TextBox3.Text = "50"
                TextBox4.Text = "9346"
                TextBox5.Text = "9171"
                Intensity_Text.Text = 0
                default_miner = ""
                Devices_Text.Text = ""
                Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
            Catch ex As IOException
                _logger.LogError(ex)
            Finally
                _logger.Trace("Main() SaveSettings: OK.")
                MsgBox("Settings set back to defaults and closing Settings Menu.")
            End Try
        End If

    End Sub

    Public Sub Style()

        Panel1.BackColor = Color.FromArgb(27, 92, 46)
        Button1.BackColor = Color.FromArgb(27, 92, 46)
        Button5.BackColor = Color.FromArgb(27, 92, 46)
        Button6.BackColor = Color.FromArgb(27, 92, 46)
        Button2.BackColor = Color.FromArgb(27, 92, 46)
        Button3.BackColor = Color.FromArgb(27, 92, 46)
        Button4.BackColor = Color.FromArgb(27, 92, 46)
        Panel2.BackColor = Color.FromArgb(41, 54, 61)

    End Sub

    Dim drag As Boolean = False
    Dim mousex As Integer, mousey As Integer

    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown, Panel2.MouseDown, PictureBox11.MouseDown, Label1.MouseDown

        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove, Panel2.MouseMove, PictureBox11.MouseMove, Label1.MouseMove

        If drag Then
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
        End If

    End Sub

    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp, Panel2.MouseUp, PictureBox11.MouseUp, Label1.MouseUp

        drag = False

    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click

        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        If ComboBox1.SelectedItem = 1 Then
            If TextBox4.Text = "9347" Or TextBox4.Text = "" Then
                TextBox4.Text = "9346"
            End If
            If TextBox5.Text = "9181" Or TextBox5.Text = "" Then
                TextBox5.Text = "9171"
            End If
        ElseIf ComboBox1.SelectedItem = 2 Then
            If TextBox4.Text = "9346" Or TextBox4.Text = "" Then
                TextBox4.Text = "9347"
            End If
            If TextBox5.Text = "9171" Or TextBox5.Text = "" Then
                TextBox5.Text = "9181"
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Intensity_Text.Text = "9"

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Intensity_Text.Text = "14"

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Intensity_Text.Text = "0"

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim result1 As DialogResult = MsgBox("Please select the location that you would like the OCM to store it's settings, miner, and p2pool data.", MessageBoxButtons.OKCancel)
        If result1 = DialogResult.OK Then
            Dim result2 As Windows.Forms.DialogResult = dir_browse.ShowDialog()
            If result2 = Windows.Forms.DialogResult.OK Then
                settingsfolder = dir_browse.SelectedPath
                My.Settings.settingsfolder = settingsfolder
                My.Settings.Save()
            End If
        End If

    End Sub

    Public Sub Resize_Settings()

        Dim screenwidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenheight As Integer = Screen.PrimaryScreen.Bounds.Height
        Dim smallwindowwidth As Double = 0.2266 * screenwidth
        Dim smallwindowheight As Double = 0.3102 * screenheight
        Dim largewindowwidth As Double = 0.4532 * screenwidth
        Dim largewindowheight As Double = 0.4653 * screenheight

        'MsgBox(screenwidth & "," & screenheight & " " & smallwindowwidth & "," & smallwindowheight)

        If minmax = False Then 'Shrink
            Me.Size = New Size(smallwindowwidth, smallwindowheight)
            PictureBox2.Image = My.Resources.greenplus
            minmax = True
        ElseIf minmax = True Then 'Grow
            Me.Size = New Size(largewindowwidth, largewindowheight)
            PictureBox2.Image = My.Resources.greenminus
            minmax = False
        End If

    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click

        Me.Close()

    End Sub

End Class