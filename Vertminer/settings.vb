Imports System.Environment
Imports System.IO
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class settings

    Dim JSONConverter As JavaScriptSerializer = New JavaScriptSerializer()

    Private Sub settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Invoke(New MethodInvoker(AddressOf Style))
            Node_Fee.Text = p2pool_node_fee & "%"
            Node_Donation.Text = p2pool_donation & "%"
            TextBox3.Text = max_connections
            TextBox4.Text = p2pool_port
            TextBox5.Text = mining_port
            Intensity_Text.Text = mining_intensity
            Fee_Address_Text.Text = p2pool_fee_address
            ComboBox1.SelectedItem = p2pool_network
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
        Catch ex As Exception
            MsgBox(ex.Message)
            newlog = newlog & Environment.NewLine
            newlog = newlog & ("- " & timenow & ", " & "Settings(), " & ex.Message)
        Finally
            newlog = newlog & Environment.NewLine
            newlog = newlog & ("- " & timenow & ", " & "Settings() Loaded: OK.")
        End Try

    End Sub

    Private Sub Settings_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        Try
            If Not Intensity_Text.Text = "" Then
                mining_intensity = Convert.ToDecimal(Intensity_Text.Text)
            End If
            If Not Node_Fee.Text = "" Then
                p2pool_node_fee = Convert.ToInt32(Node_Fee.Text.Replace("%", ""))
            End If
            If Not Node_Donation.Text = "" Then
                p2pool_donation = Convert.ToInt32(Node_Donation.Text.Replace("%", ""))
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
            newlog = newlog & Environment.NewLine
            newlog = newlog & ("- " & timenow & ", " & "Settings(), " & ex.Message)
        Finally
            newlog = newlog & Environment.NewLine
            newlog = newlog & ("- " & timenow & ", " & "Settings() Closed: OK.")
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim result1 As DialogResult = MsgBox("Set Vertcoin Data Diretory to default location in AppData\Roaming?", MessageBoxButtons.OKCancel)
        If result1 = DialogResult.OK Then
            appdata = GetFolderPath(SpecialFolder.ApplicationData) & "\Vertcoin"
            MsgBox("Vertcoin Data Directory set to default.")
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            Dim newjson = New Settings_JSON
            newjson.appdata = ""
            newjson.start_minimized = "false"
            newjson.start_with_windows = "false"
            newjson.autostart_p2pool = "false"
            newjson.autostart_mining = "false"
            newjson.mine_when_idle = "false"
            newjson.keep_miner_alive = "false"
            newjson.keep_p2pool_alive = "false"
            newjson.use_upnp = "false"
            newjson.p2pool_network = "1"
            newjson.p2pool_node_fee = "0"
            newjson.p2pool_donation = "1"
            newjson.max_connections = "50"
            newjson.p2pool_port = "9346"
            newjson.mining_port = "9171"
            newjson.mining_intensity = 0
            newjson.p2pool_fee_address = "VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG"
            newjson.p2pool_version = p2pool_version
            newjson.amd_version = amd_version
            newjson.nvidia_version = nvidia_version
            newjson.cpu_version = cpu_version
            newjson.default_miner = ""
            newjson.pools.Clear()
            Dim poolcount = pools.Count()
            Dim workercount = workers.Count()
            Dim passwordcount = passwords.Count()
            Dim count As Decimal = Decimal.MaxValue
            count = Math.Min(count, poolcount)
            count = Math.Min(count, workercount)
            count = Math.Min(count, passwordcount)
            If Not count = 0 Then
                For x = 0 To count - 1
                    If Not pools(x) = "" And Not workers(x) = "" And Not passwords(x) = "" Then
                        Dim pooljson As Pools_JSON = New Pools_JSON()
                        pooljson.url = pools(x)
                        pooljson.user = workers(x)
                        pooljson.pass = passwords(x)
                        newjson.pools.Add(pooljson)
                    End If
                Next
            End If
            Dim jsonstring = JSONConverter.Serialize(newjson)
            Dim jsonFormatted As String = JValue.Parse(jsonstring).ToString(Formatting.Indented)
            File.WriteAllText(settingsfile, jsonFormatted)
        Catch ex As IOException
            newlog = newlog & Environment.NewLine
            newlog = newlog & ("- " & timenow & ", " & "Main() SaveSettings: " & ex.Message)
        Finally
            newlog = newlog & Environment.NewLine
            newlog = newlog & ("- " & timenow & ", " & "Main() SaveSettings: OK.")
            MsgBox("Settings set back to defaults.")
            Invoke(New MethodInvoker(AddressOf Main.LoadSettingsJSON))
            Invoke(New MethodInvoker(AddressOf Main.Update_Pool_Info))
        End Try

    End Sub

    Public Sub Style()

        Panel1.BackColor = Color.FromArgb(27, 92, 46)
        Button1.BackColor = Color.FromArgb(27, 92, 46)
        Button5.BackColor = Color.FromArgb(27, 92, 46)
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

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click

        Me.Close()

    End Sub

End Class