Imports System.Environment
Imports System.IO

Public Class settings

    Private Sub settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Node_Fee.Text = P2P_Node_Fee & "%"
            Node_Donation.Text = P2P_Donation & "%"
            TextBox3.Text = MaxConnections
            TextBox2.Text = MiningIdle
            TextBox1.Text = RestartDelay
            TextBox4.Text = p2pool_port
            TextBox5.Text = mining_port
            Fee_Address_Text.Text = P2P_Node_Fee_Address
            If Keep_Miner_Alive = True Then
                CheckBox6.Checked = True
            Else
                CheckBox6.Checked = False
            End If
            If Keep_P2Pool_Alive = True Then
                CheckBox5.Checked = True
            Else
                CheckBox5.Checked = False
            End If
            If start_minimized = True Then
                CheckBox3.Checked = True
            Else
                CheckBox3.Checked = False
            End If
            If hide_windows = True Then
                CheckBox2.Checked = True
            Else
                CheckBox2.Checked = False
            End If
            If use_UPnP = True Then
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
            If start_mining_when_idle = True Then
                CheckBox8.Checked = True
            Else
                CheckBox8.Checked = False
            End If
            If P2P_Network = 1 Then
                If p2pool_port = "9347" Or p2pool_port = "" Then
                    p2pool_port = "9346"
                End If
                If mining_port = "9181" Or mining_port = "" Then
                    mining_port = "9171"
                End If
                TextBox4.Text = p2pool_port
                TextBox5.Text = mining_port
            ElseIf P2P_Network = 2 Then
                If p2pool_port = "9346" Or p2pool_port = "" Then
                    p2pool_port = "9347"
                End If
                If mining_port = "9171" Or mining_port = "" Then
                    mining_port = "9181"
                End If
                TextBox4.Text = p2pool_port
                TextBox5.Text = mining_port
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Settings(), " & ex.Message)
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Settings() Loaded: OK.")
        End Try

    End Sub

    Private Sub Settings_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        Try
            If Not Node_Fee.Text = "" Then
                P2P_Node_Fee = Convert.ToInt32(Node_Fee.Text.Replace("%", ""))
            End If
            If Not Node_Donation.Text = "" Then
                P2P_Donation = Convert.ToInt32(Node_Donation.Text.Replace("%", ""))
            End If
            If Not TextBox3.Text = "" Then
                MaxConnections = TextBox3.Text
            End If
            If Fee_Address_Text.Text = "" Then
                P2P_Node_Fee_Address = "VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG" 'If fee address is empty, default to Dev Donation address to keep config alive.
            Else
                P2P_Node_Fee_Address = Fee_Address_Text.Text
            End If
            If Not TextBox1.Text = "" Then
                RestartDelay = TextBox1.Text
            End If
            If Not TextBox4.Text = "" Then
                p2pool_port = TextBox4.Text
            End If
            If Not TextBox5.Text = "" Then
                mining_port = TextBox5.Text
            End If
            If Not TextBox2.Text = "" Then
                MiningIdle = TextBox2.Text
            End If
            If CheckBox3.Checked = True Then
                start_minimized = True
            Else
                start_minimized = False
            End If
            If CheckBox2.Checked = True Then
                hide_windows = True
            Else
                hide_windows = False
            End If
            If CheckBox4.Checked = True Then
                use_UPnP = True
            Else
                use_UPnP = False
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
            If CheckBox8.Checked = True Then
                start_mining_when_idle = True
            Else
                start_mining_when_idle = False
            End If
            If CheckBox6.Checked = True Then
                Keep_Miner_Alive = True
            Else
                Keep_Miner_Alive = False
            End If
            If CheckBox5.Checked = True Then
                Keep_P2Pool_Alive = True
            Else
                Keep_P2Pool_Alive = False
            End If
            If CheckBox9.Checked = True Then
                start_with_windows = True
            Else
                start_with_windows = False
            End If
            'Saves current settings.
            Invoke(New MethodInvoker(AddressOf main.SaveSettingsIni))
            'Adds startup entry to registry after settings have been saved.
            Invoke(New MethodInvoker(AddressOf main.StartWithWindows))
            'Updates the timer interval to the selected miner restart delay.
            If RestartDelay <= 0 Then
                RestartDelay = 1
            End If
            Main.Uptime_Timer.Interval = RestartDelay * 1000
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Settings(), " & ex.Message)
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Settings() Closed: OK.")
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim result1 As DialogResult = MsgBox("Set Vertcoin Data Diretory to default location in AppData\Roaming?", MessageBoxButtons.OKCancel)
        If result1 = DialogResult.OK Then
            appdata = GetFolderPath(SpecialFolder.ApplicationData) & "\Vertcoin"
        End If
        MsgBox("Vertcoin Data Directory set to default.")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Do
            Try
                If System.IO.File.Exists(SettingsIni) = True Then
                    Dim objWriter As New System.IO.StreamWriter(SettingsIni)
                    objWriter.WriteLine(appdata)
                    objWriter.WriteLine("Start Minimized=false")
                    objWriter.WriteLine("Hide Windows=false")
                    objWriter.WriteLine("Start With Windows=false")
                    objWriter.WriteLine("Autostart P2Pool=false")
                    objWriter.WriteLine("Autostart Mining=false")
                    objWriter.WriteLine("Mine When Idle=false")
                    objWriter.WriteLine("Keep Miner Alive=false")
                    objWriter.WriteLine("Keep P2Pool Alive=false")
                    objWriter.WriteLine("Use UPnP=false")
                    objWriter.WriteLine("P2Pool Network=1")
                    objWriter.WriteLine("P2Pool Node Fee (%)=0")
                    objWriter.WriteLine("P2Pool Donation (%)=1")
                    objWriter.WriteLine("Maximum P2Pool Connections=50")
                    objWriter.WriteLine("Mining Idle (s)=0")
                    objWriter.WriteLine("Mining Restart Delay (s)=2")
                    objWriter.WriteLine("P2Pool Port=9346")
                    objWriter.WriteLine("Mining Port=9171")
                    objWriter.WriteLine("Mining Intensity=0")
                    Intensity = 0
                    objWriter.WriteLine("Worker Name=VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG")
                    Worker = "VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG"
                    objWriter.WriteLine("Worker Password=x")
                    Password = "x"
                    objWriter.WriteLine("P2Pool Fee Address=VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG")
                    objWriter.WriteLine("Pool URL=stratum+tcp://vtc.alwayshashing.com:9171")
                    Pool_Address = "stratum+tcp://vtc.alwayshashing.com:9171"
                    objWriter.WriteLine("One-Click Version=" & Miner_Version)
                    objWriter.WriteLine("P2Pool Version=" & P2Pool_Version)
                    objWriter.WriteLine("AMD Miner Version=" & AMD_Version)
                    objWriter.WriteLine("Nvidia Miner Version=" & Nvidia_Version)
                    objWriter.WriteLine("CPU Miner Version=" & CPU_Version)
                    objWriter.WriteLine("Default Miner=")
                    objWriter.Write("Additional Miner Config=")
                    additional_config = ""
                    objWriter.Close()
                End If
                Exit Do
            Catch ex As IOException
                'Settings.ini is still in use so pause before trying again.
                System.Threading.Thread.Sleep(100)
                NewLog = NewLog & Environment.NewLine
                NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() SaveSettings: " & ex.Message)
            Finally
                NewLog = NewLog & Environment.NewLine
                NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() SaveSettings: OK.")
                MsgBox("Settings set back to defaults.")
                Invoke(New MethodInvoker(AddressOf Main.LoadSettingsIni))
                Invoke(New MethodInvoker(AddressOf Main.Update_P2Pool_Text))
                Invoke(New MethodInvoker(AddressOf Main.Update_Miner_Text))
            End Try
        Loop

    End Sub
End Class