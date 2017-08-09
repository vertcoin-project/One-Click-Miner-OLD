
Imports System.IO
Imports System.IO.Compression
Imports System.Environment
Imports System.Net
Imports System.Net.Sockets
Imports Open.Nat



Public Class Main

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            If Environment.Is64BitOperatingSystem = True Then
                platform = True '64-bit
            Else
                platform = False '32-bit
            End If
            SettingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Vertcoin One-Click Miner"
            SettingsIni = SettingsFolder & "\Settings.ini"
            SysLog = SettingsFolder & "\SysLog.txt"
            P2PoolFolder = SettingsFolder & "\p2pool"
            AmdFolder = SettingsFolder & "\amd"
            NvidiaFolder = SettingsFolder & "\nvidia"
            CPUFolder = SettingsFolder & "\cpu"
            UpdateInterval.Start()
            If System.IO.Directory.Exists(SettingsFolder) = False Then
                System.IO.Directory.CreateDirectory(SettingsFolder)
            End If
            If System.IO.File.Exists(SettingsIni) = False Then
                File.Create(SettingsIni).Dispose()
                Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
            Else
                Invoke(New MethodInvoker(AddressOf Update_Settings))
            End If
            If System.IO.File.Exists(SysLog) = False Then
                File.Create(SysLog).Dispose()
            End If
            'Load Settings
            Invoke(New MethodInvoker(AddressOf LoadSettingsIni))
            'Check for default miner selected.  This is kept separate from autostart to allow user to see checkbox.
            If DefaultMiner = "amd" Then
                amd_check.Checked = True
            ElseIf DefaultMiner = "nvidia" Then
                nvidia_check.Checked = True
            ElseIf DefaultMiner = "cpu" Then
                cpu_check.Checked = True
            End If
            If P2P_Network = 1 Then
                ComboBox1.SelectedItem = "1"
            ElseIf P2P_Network = 2 Then
                ComboBox1.SelectedItem = "2"
            End If
            If Pool_Address.Contains("localhost") Then
                CheckBox1.Checked = True
            End If
            'Window state on start
            If start_minimized = True Then
                Me.WindowState = FormWindowState.Minimized
            Else
                Me.WindowState = FormWindowState.Normal
            End If
            'Copies previous log file to string
            LogFileString = File.ReadAllText(SysLog)
            'Check if p2pool or miner are already running
            P2Pool_Detected = False
            AMD_Detected = False
            Nvidia_Detected = False
            CPU_Detected = False
            For Each p As Process In System.Diagnostics.Process.GetProcesses
                If p.ProcessName.Contains("ocm_p2pool") Then
                    P2Pool_Detected = True
                ElseIf p.ProcessName.Contains("ocm_sgminer") Then
                    AMD_Detected = True
                ElseIf p.ProcessName.Contains("ocm_ccminer") Then
                    Nvidia_Detected = True
                ElseIf p.ProcessName.Contains("ocm_cpuminer") Then
                    CPU_Detected = True
                ElseIf p.ProcessName.Contains("run_p2pool") Or p.ProcessName.Contains("p2pool") Then
                    otherp2pool = True
                ElseIf p.ProcessName.Contains("ccminer") Or p.ProcessName.Contains("sgminer") Or p.ProcessName.Contains("cpuminer") Then
                    otherminer = True
                End If
            Next
            If P2Pool_Detected = True Then
                'P2Pool is already running
                PictureBox1.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
                TextBox1.Text = "Online"
                Button1.Text = "Disable"
                Uptime_Timer.Start()
            End If
            If AMD_Detected = True Or Nvidia_Detected = True Or CPU_Detected = True Then
                'Miner is already running
                Additional_Configuration_Text.Enabled = False
                Pool_Address_Text.Enabled = False
                Worker_Address_Text.Enabled = False
                Worker_Address_Text.Enabled = False
                Password_Text.Enabled = False
                Intensity_Text.Enabled = False
                CheckBox1.Enabled = False
                Uptime_Timer.Start()
            End If
            If AMD_Detected = True Then
                PictureBox2.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
                TextBox2.Text = "Online"
                Button2.Text = "Disable"
            End If
            If Nvidia_Detected = True Then
                PictureBox5.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
                TextBox2.Text = "Online"
                Button4.Text = "Disable"
            End If
            If CPU_Detected = True Then
                PictureBox7.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
                TextBox2.Text = "Online"
                Button5.Text = "Disable"
            End If
            'Autostart variables
            If autostart_mining = True Then
                If amd_check.Checked = True Then
                    If System.IO.File.Exists(AmdFolder & "\ocm_sgminer.exe") = True Then
                        AmdMiner = True
                    End If
                    mining_initialized = True
                    BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                ElseIf nvidia_check.Checked = True Then
                    If System.IO.File.Exists(NvidiaFolder & "\ocm_ccminer.exe") = True Then
                        NvidiaMiner = True
                    End If
                    mining_initialized = True
                    BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                ElseIf cpu_check.Checked = True Then
                    If System.IO.File.Exists(CPUFolder & "\ocm_cpuminer.exe") = True Then
                        CPUMiner = True
                    End If
                    mining_initialized = True
                    BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                End If
            End If
            If autostart_p2pool = True Then
                If System.IO.File.Exists(P2PoolFolder & "\ocm_p2pool.exe") = True Then
                    BeginInvoke(New MethodInvoker(AddressOf Start_P2Pool))
                End If
            End If
            Invoke(New MethodInvoker(AddressOf Update_P2Pool_Text))
            Invoke(New MethodInvoker(AddressOf Update_Miner_Text))
            BeginInvoke(New MethodInvoker(AddressOf Detected))
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main(), " & ex.Message)
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Loaded: OK, V:" & Application.ProductVersion)
        End Try

    End Sub

    Private Sub Main_Size_Change(sender As Object, e As EventArgs) Handles MyBase.SizeChanged

        NotifyIcon1.ContextMenu = New ContextMenu
        If Me.WindowState = FormWindowState.Minimized Then
            If start_minimized = True Then
                Me.ShowInTaskbar = False
                NotifyIcon1.Visible = True
                NotifyIcon1.ShowBalloonTip(5, "System", "Vertcoin One-Click Miner is minimized.", ToolTipIcon.Info)
                NotifyIcon1.ContextMenu.MenuItems.Add("Open", AddressOf Menu_Open)
                NotifyIcon1.ContextMenu.MenuItems.Add("Close", AddressOf Menu_Close)
            Else
                NotifyIcon1.Visible = False
                Me.Show()
            End If
        ElseIf Me.WindowState = FormWindowState.Normal Then
            NotifyIcon1.Visible = False
            Me.Show()
        End If

    End Sub

    Private Sub Menu_Close()

        Me.Close()

    End Sub

    Private Sub Menu_Open()

        Me.WindowState = FormWindowState.Normal

    End Sub

    Private Sub Main_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        NewLog = NewLog & Environment.NewLine
        NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main(), Closed: OK")
        NewLog = NewLog & Environment.NewLine
        NewLog = NewLog & ("================================================================================")
        File.WriteAllText(SysLog, NewLog)
        File.AppendAllText(SysLog, LogFileString)
        NotifyIcon1.Dispose()
        If amd_check.Checked = True Then
            DefaultMiner = "amd"
        ElseIf nvidia_check.Checked = True Then
            DefaultMiner = "nvidia"
        ElseIf cpu_check.Checked = True Then
            DefaultMiner = "cpu"
        End If
        Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Application.Exit()

    End Sub

    Public Sub Detected()

        Me.Text = "Vertcoin One-Click Miner - BETA V:" & Miner_Version
        If otherp2pool = True Then
            MsgBox("One-Click Miner has detected other p2pool software running.  Be aware of potential port conflicts.")
        End If
        If otherminer = True Then
            MsgBox("One-Click Miner has detected other miner software running." & Environment.NewLine & "It is not recommended to run multiple miners on the same machine simultaneously.")
        End If

    End Sub

    Public Sub UPnP()

        Try
            'using native upnp
            'Dim ipAddress As String = GetIPv4Address()
            'Dim upnpnat As New NATUPNPLib.UPnPNAT()
            'Dim mappings As NATUPNPLib.IStaticPortMappingCollection = upnpnat.StaticPortMappingCollection
            'mappings.Add(mining_port, "TCP", mining_port, ipAddress, True, "P2Pool Mining Server")
            'mappings.Add(p2pool_port, "TCP", p2pool_port, ipAddress, True, "P2Pool P2P")

            'using native upnp
            'Dim upnpnat As New NATUPNPLib.UPnPNAT()
            'Dim lMapper As NATUPNPLib.IStaticPortMappingCollection = upnpnat.StaticPortMappingCollection
            'Dim lMappedPort As NATUPNPLib.IStaticPortMapping
            'lMappedPort = DirectCast(lMapper.Add(mining_port, "TCP", mining_port, ipAddress, True, "P2Pool Mining Server"), NATUPNPLib.IStaticPortMapping)
            'lMappedPort = DirectCast(lMapper.Add(p2pool_port, "TCP", p2pool_port, ipAddress, True, "P2Pool P2P"), NATUPNPLib.IStaticPortMapping)

            'Using open.nat
            Dim discoverer As New NatDiscoverer()
            ' using SSDP protocol, it discovers NAT device.
            Dim device = discoverer.DiscoverDeviceAsync()
            ' create a New mapping in the router [external_ip1702 -> host_machine:1602]
            device.Equals(New Mapping(Protocol.Tcp, mining_port, mining_port, "P2Pool Mining Server"))
            device.Equals(New Mapping(Protocol.Tcp, p2pool_port, p2pool_port, "P2Pool P2P"))
            ' configure a TCP socket listening on port 1602
            Dim EndPoint As New IPEndPoint(IPAddress.Any, mining_port)
            Dim socket As New Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            socket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted)
            socket.Bind(EndPoint)
            socket.Listen(4)
            Dim EndPoint2 As New IPEndPoint(IPAddress.Any, p2pool_port)
            Dim socket2 As New Socket(EndPoint2.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            socket2.SetIPProtectionLevel(IPProtectionLevel.Unrestricted)
            socket2.Bind(EndPoint2)
            socket2.Listen(4)

            'Using Open.Nat
            'Dim discoverer As New Open.Nat.NatDiscoverer()
            'Dim cts As New System.Threading.CancellationTokenSource(10000)
            'Dim device = discoverer.DiscoverDeviceAsync(Open.Nat.PortMapper.Upnp, cts)
            'device.CreatePortMapAsync(New Open.Nat.Mapping(Open.Nat.Protocol.Tcp, 1600, 1700, "The mapping name"))


        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() UPnP: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() UPnP: SET OK. Ports set: " & mining_port & "," & p2pool_port)
        End Try

    End Sub

    'Private Function GetIPv4Address() As String

    '    GetIPv4Address = String.Empty
    '    Dim strHostName As String = System.Net.Dns.GetHostName()
    '    Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

    '    For Each ipheal As System.Net.IPAddress In iphe.AddressList
    '        If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
    '            GetIPv4Address = ipheal.ToString()
    '        End If
    '    Next

    'End Function

    Public Sub Download_Miner()

        Try
            progress.progress_text.Text = "Downloading Miner"
            progress.Show()
            downloadclient = New WebClient
            AddHandler downloadclient.DownloadProgressChanged, AddressOf Client_ProgressChanged
            AddHandler downloadclient.DownloadFileCompleted, AddressOf Client_MinerDownloadCompleted
            'Compares the current version of the AMD miner with the latest available.
            If AmdMiner = True Then
                If (newestversion > System.Version.Parse(AMD_Version)) Or mining_installed = False Then
                    If System.IO.Directory.Exists(AmdFolder) = False Then
                        'If AMD miner doesn't already exist, create folder and download
                        System.IO.Directory.CreateDirectory(AmdFolder)
                    Else
                        System.IO.Directory.Delete(AmdFolder, True)
                        System.Threading.Thread.Sleep(100)
                        System.IO.Directory.CreateDirectory(AmdFolder)
                    End If
                    downloadclient.DownloadFileAsync(New Uri(updatelink), SettingsFolder & "\amd\sgminer.zip", True)
                Else
                    progress.Close()
                End If
            End If
            'Compares the current version of the Nvidia miner with the latest available.
            If NvidiaMiner = True Then
                If newestversion > System.Version.Parse(Nvidia_Version) Or mining_installed = False Then
                    If System.IO.Directory.Exists(NvidiaFolder) = False Then
                        'If Nvidia miner doesn't already exist, create folder and download
                        System.IO.Directory.CreateDirectory(NvidiaFolder)
                    Else
                        System.IO.Directory.Delete(NvidiaFolder, True)
                        System.Threading.Thread.Sleep(100)
                        System.IO.Directory.CreateDirectory(NvidiaFolder)
                    End If
                    downloadclient.DownloadFileAsync(New Uri(updatelink), SettingsFolder & "\nvidia\ccminer.zip", True)
                Else
                    progress.Close()
                End If
            End If
            'Compares the current version of the CPU miner with the latest available.
            If CPUMiner = True Then
                If newestversion > System.Version.Parse(CPU_Version) Or mining_installed = False Then
                    If System.IO.Directory.Exists(CPUFolder) = False Then
                        'If CPU miner doesn't already exist, create folder and download
                        System.IO.Directory.CreateDirectory(CPUFolder)
                    Else
                        System.IO.Directory.Delete(CPUFolder, True)
                        System.Threading.Thread.Sleep(100)
                        System.IO.Directory.CreateDirectory(CPUFolder)
                    End If
                    downloadclient.DownloadFileAsync(New Uri(updatelink), SettingsFolder & "\cpu\cpuminer.zip", True)
                Else
                    progress.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Download_Miner: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Download_Miner: Downloaded OK.")
        End Try

    End Sub

    Public Sub Download_P2Pool()

        Try
            progress.progress_text.Text = "Downloading P2Pool"
            progress.Show()
            downloadclient = New WebClient
            AddHandler downloadclient.DownloadProgressChanged, AddressOf Client_ProgressChanged
            AddHandler downloadclient.DownloadFileCompleted, AddressOf Client_P2PoolDownloadCompleted
            If (newestversion > System.Version.Parse(P2Pool_Version)) Or p2pool_installed = False Then
                'Create p2pool directory and download/extract p2pool components into directory
                If System.IO.Directory.Exists(P2PoolFolder) = False Then
                    System.IO.Directory.CreateDirectory(P2PoolFolder)
                End If
                MsgBox("Please note, you must also run a Vertcoin Wallet to use P2Pool locally.")
                downloadclient.DownloadFileAsync(New Uri(updatelink), P2PoolFolder & "\p2pool.zip", True)
            End If
        Catch ex As Exception
            MsgBox("An issue occurred during the download.  Please try again.")
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Download_P2Pool: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Download_P2Pool: Downloaded OK.")
        End Try

    End Sub

    Public Sub Download_P2PoolInterface()

        Try
            'Better P2Pool Site UI
            Dim link As String = "http://github.com/justino/p2pool-ui-punchy/archive/master.zip"
            progress.progress_text.Text = "Downloading P2Pool UI"
            progress.Show()
            downloadclient = New WebClient
            AddHandler downloadclient.DownloadProgressChanged, AddressOf Client_ProgressChanged
            AddHandler downloadclient.DownloadFileCompleted, AddressOf Client_P2PoolInterfaceDownloadCompleted
            downloadclient.DownloadFileAsync(New Uri(link), P2PoolFolder & "\interface.zip")
        Catch ex As Exception
            MsgBox("An issue occurred during the download.  Please try again.")
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Download_P2PoolInterface: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Download_P2PoolInterface: Downloaded OK.")
        End Try

    End Sub

    Private Sub Client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)

        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100
        If Int32.Parse(Math.Truncate(percentage).ToString()) >= 0 Then
            progress.ProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())
        End If

    End Sub

    Private Sub Client_MinerDownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

        Try
            If canceldownloadasync = False Then
                'Download proper miner and extract into respective directories
                If AmdMiner = True Then
                    zipPath = SettingsFolder & "\amd\sgminer.zip"
                    exe = SettingsFolder & "\amd\ocm_sgminer.exe"
                    miner_config_file = SettingsFolder & "\amd\config.bat"
                ElseIf NvidiaMiner = True Then
                    zipPath = SettingsFolder & "\nvidia\ccminer.zip"
                    exe = SettingsFolder & "\nvidia\ocm_ccminer.exe"
                    dll = SettingsFolder & "\nvidia\msvcr120.dll"
                    miner_config_file = SettingsFolder & "\nvidia\config.bat"
                ElseIf CPUMiner = True Then
                    zipPath = SettingsFolder & "\cpu\cpuminer.zip"
                    exe = SettingsFolder & "\cpu\ocm_cpuminer.exe"
                    miner_config_file = SettingsFolder & "\cpu\config.bat"
                End If
                Using archive As ZipArchive = ZipFile.OpenRead(zipPath)
                    'If platform = True Then '64-bit
                    For Each entry As ZipArchiveEntry In archive.Entries
                        If NvidiaMiner = True Then 'Nvidia
                            If (entry.FullName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) Or entry.FullName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) Then
                                If entry.FullName.EndsWith(".exe") Then
                                    extractpath = exe
                                ElseIf entry.FullName.EndsWith(".dll") Then
                                    extractpath = dll
                                End If
                                entry.ExtractToFile(extractpath, True)
                            End If
                        ElseIf AmdMiner = True Then 'AMD
                            If (entry.FullName.Contains("kernel") And entry.FullName.EndsWith(".cl", StringComparison.OrdinalIgnoreCase)) Then
                                If System.IO.Directory.Exists(AmdFolder & "\kernel\") = False Then
                                    System.IO.Directory.CreateDirectory(AmdFolder & "\kernel\")
                                End If
                                entry.ExtractToFile(AmdFolder & "\kernel\" & entry.Name, True)
                            Else
                                If entry.FullName.EndsWith(".exe") Then
                                    entry.ExtractToFile(exe, True)
                                ElseIf entry.FullName.EndsWith(".dll") Then
                                    entry.ExtractToFile(AmdFolder & "\" & entry.Name, True)
                                End If
                            End If
                        ElseIf CPUMiner = True Then 'CPU
                            If (entry.FullName.EndsWith("corei7.exe", StringComparison.OrdinalIgnoreCase)) Then
                                If entry.FullName.EndsWith(".exe") Then
                                    extractpath = exe
                                ElseIf entry.FullName.EndsWith(".dll") Then
                                    extractpath = dll
                                End If
                                entry.ExtractToFile(extractpath, True)
                            End If
                        End If
                    Next
                    'Else '32-bit
                    '    For Each entry As ZipArchiveEntry In archive.Entries
                    '        If NvidiaMiner = True Then 'Nvidia
                    '            If (entry.FullName.Contains("32") And entry.FullName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) Or (entry.FullName.Contains("32") And entry.FullName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)) Then
                    '                If entry.FullName.EndsWith(".exe") Then
                    '                    extractpath = exe
                    '                ElseIf entry.FullName.EndsWith(".dll") Then
                    '                    extractpath = dll
                    '                End If
                    '                entry.ExtractToFile(extractpath, True)
                    '            End If
                    '        Else 'AMD
                    '            If (entry.FullName.Contains("kernel") And entry.FullName.EndsWith(".cl", StringComparison.OrdinalIgnoreCase)) Then
                    '                If System.IO.Directory.Exists(AmdFolder & "\kernel\") = False Then
                    '                    System.IO.Directory.CreateDirectory(AmdFolder & "\kernel\")
                    '                End If
                    '                entry.ExtractToFile(AmdFolder & "\kernel\" & entry.Name, True)
                    '            Else
                    '                If Not entry.Name = "" Then
                    '                    entry.ExtractToFile(AmdFolder & "\" & entry.Name, True)
                    '                End If
                    '            End If
                    '        End If
                    '       Next
                    'End If
                End Using
                'Create default miner config
                If System.IO.File.Exists(miner_config_file) = False Then
                    Dim objWriter As New System.IO.StreamWriter(miner_config_file)
                    objWriter.WriteLine(miner_config)
                    objWriter.Close()
                End If
                Dim result1 As DialogResult = MsgBox("Miner is ready to run", MessageBoxButtons.OK)
                If result1 = DialogResult.OK Then
                    progress.Close()
                    update_complete = True
                End If
                If AmdMiner = True Then
                    AMD_Version = System.Convert.ToString(newestversion)
                ElseIf NvidiaMiner = True Then
                    Nvidia_Version = System.Convert.ToString(newestversion)
                ElseIf CPUMiner = True Then
                    CPU_Version = System.Convert.ToString(newestversion)
                End If
                AmdMiner = False
                NvidiaMiner = False
                CPUMiner = False
            End If
        Catch ex As Exception
            MsgBox("An issue occurred during the download.  Please try again.")
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() MinerDownloadCompleted: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() MinerDownloadCompleted: OK.")
        End Try

    End Sub

    Private Sub Client_P2PoolDownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

        Try
            If canceldownloadasync = False Then
                'Download proper miner and extract into respective directories
                zipPath = P2PoolFolder & "\p2pool.zip"
                exe = P2PoolFolder & "\ocm_p2pool.exe"
                p2pool_config_file = P2PoolFolder & "\start_p2pool.bat"
                p2pool_config = "ocm_p2pool.exe" & Environment.NewLine & "exit /B"
                ZipFile.ExtractToDirectory(zipPath, P2PoolFolder)
                Dim folders() As String = IO.Directory.GetDirectories(P2PoolFolder)
                For Each folder As String In folders
                    Dim files() As String = IO.Directory.GetFiles(folder)
                    For Each file As String In files
                        If file.Contains("run_p2pool") Then
                            My.Computer.FileSystem.MoveFile(file, P2PoolFolder & "\" & "ocm_p2pool.exe", True)
                        Else
                            My.Computer.FileSystem.MoveFile(file, P2PoolFolder & "\" & System.IO.Path.GetFileName(file), True)
                        End If
                    Next
                    Dim subfolders() As String = IO.Directory.GetDirectories(folder)
                    For Each subfolder As String In subfolders
                        Dim split As String() = subfolder.Split("\")
                        Dim newfolder As String = split(split.Length - 1)
                        My.Computer.FileSystem.MoveDirectory(subfolder, P2PoolFolder & "\" & newfolder, True)
                    Next
                    System.IO.Directory.Delete(folder)
                Next
                If System.IO.File.Exists(P2PoolFolder & "\Start P2Pool.bat") = True Then
                    System.IO.File.Delete(P2PoolFolder & "\Start P2Pool.bat")
                End If
                'Create default p2pool config
                If System.IO.File.Exists(p2pool_config_file) = False Then
                    Dim objWriter As New System.IO.StreamWriter(p2pool_config_file)
                    objWriter.WriteLine(p2pool_config)
                    objWriter.Close()
                End If
                Invoke(New MethodInvoker(AddressOf Download_P2PoolInterface))
            End If
        Catch ex As Exception
            MsgBox("An issue occurred during the download.  Please try again.")
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() P2PoolDownloadCompleted: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() P2PoolDownloadCompleted: OK.")
        End Try

    End Sub

    Private Sub Client_P2PoolInterfaceDownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

        Try
            'Download proper miner and extract into respective directories
            zipPath = P2PoolFolder & "\interface.zip"
            ZipFile.ExtractToDirectory(zipPath, P2PoolFolder)
            Dim folders() As String = IO.Directory.GetDirectories(P2PoolFolder)
            If System.IO.Directory.Exists(P2PoolFolder & "\web-static") = True Then
                System.IO.Directory.Delete(P2PoolFolder & "\web-static", True)
            End If
            My.Computer.FileSystem.RenameDirectory(P2PoolFolder & "\p2pool-ui-punchy-master", "web-static")
            Dim result1 As DialogResult = MsgBox("P2Pool has been installed.", MessageBoxButtons.OK)
            If result1 = DialogResult.OK Then
                progress.Close()
                Invoke(New MethodInvoker(AddressOf Check_RPC_Settings))
                update_complete = True
            End If
            P2Pool_Version = System.Convert.ToString(newestversion)
        Catch ex As Exception
            MsgBox("An issue occurred during the download.  Please try again.")
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() P2PoolInterfaceDownloadCompleted: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() P2PoolInterfaceDownloadCompleted: OK.")
        End Try

    End Sub

    Public Sub StartWithWindows()

        Dim regKey As Microsoft.Win32.RegistryKey
        Dim KeyName As String = "VertcoinOneClick"
        Dim KeyValue As String = System.Windows.Forms.Application.StartupPath & "\Vertcoin One-Click Miner.exe"
        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        If start_with_windows = True Then
            'create key if it doesn't already exist
            If regKey.GetValue(KeyName) = Nothing Then
                regKey.SetValue(KeyName, KeyValue, Microsoft.Win32.RegistryValueKind.String)
            End If
        Else
            'Delete key if it exists
            If Not regKey.GetValue(KeyName) = Nothing Then
                regKey.DeleteValue(KeyName, False)
            End If
        End If

    End Sub

    Public Sub Update_P2Pool_Text()

        Try
            'If p2pool_running = True Then
            'P2Pool Info
            Node_Fee_Text.Text = P2P_Node_Fee & "%"
            Node_Donation_Text.Text = P2P_Donation & "%"
            TextBox4.Text = mining_port
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub Update_Miner_Text()

        Try
            'If mining_running = True Then
            'Miner Info
            Worker_Address_Text.Text = Worker
            Password_Text.Text = Password
            Pool_Address_Text.Text = Pool_Address
            Additional_Configuration_Text.Text = additional_config
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Public Sub Update_Settings()

        Try
            Dim Old_Settings As String = File.ReadAllText(SettingsIni)
            If Old_Settings.Contains("Wallet Address=") Then 'Wallet Address= has been deprecated.
                With System.Reflection.Assembly.GetExecutingAssembly.GetName.Version
                    '1.0.5 Release
                    If (.Major >= 1 And .Minor >= 0 And .Build >= 5) Then
                        Dim Reader As New StreamReader(SettingsIni, False)
                        Dim Buf As Object = Reader.ReadLine
                        Dim IntBuf As Integer
                        If Not Buf = "" Then
                            appdata = Buf
                        End If
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            start_minimized = "true"                        '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                            start_minimized = "false"                       '
                        End If                                              '
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            hide_windows = "true"                           '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                            hide_windows = "false"                          '
                        End If                                              '                                            
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            start_with_windows = "true"                     '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  'This section will use default variable values if field happens to be null
                            start_with_windows = "false"                    '
                        End If                                              '
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            autostart_p2pool = "true"                       '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                            autostart_p2pool = "false"                      '
                        End If                                              '
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            autostart_mining = "true"                       '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                            autostart_mining = "false"                      '
                        End If                                              '
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            start_mining_when_idle = "true"                 '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                            start_mining_when_idle = "false"                '
                        End If                                              '
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            Keep_Miner_Alive = "true"                       '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                            Keep_Miner_Alive = "false"                      '
                        End If                                              '
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            Keep_P2Pool_Alive = "true"                      '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                            Keep_P2Pool_Alive = "false"                     '
                        End If                                              '
                        Buf = Reader.ReadLine                               '
                        If Not Buf = "" And Buf.Contains("true") Then       '
                            use_UPnP = "true"                               '
                        ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                            use_UPnP = "false"                              '
                        End If                                              '
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("P2Pool Network=") = True Then
                            Buf = Buf.replace("P2Pool Network=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                P2P_Network = Buf
                            Else
                                MsgBox("'P2Pool Network' setting invalid. Using default P2Pool network 1.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("P2Pool Node Fee (%)=") = True Then
                            Buf = Buf.replace("P2Pool Node Fee (%)=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                P2P_Node_Fee = Buf
                            Else
                                MsgBox("'P2Pool Node Fee' setting invalid. Using default P2Pool node fee percentage of 0%.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("P2Pool Node Donation (%)=") = True Then
                            Buf = Buf.replace("P2Pool Node Donation (%)=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                P2P_Donation = Buf
                            Else
                                MsgBox("'P2Pool Donation' setting invalid. Using default P2Pool donation percentage of 1%.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Maximum P2Pool Connections=") = True Then
                            Buf = Buf.replace("Maximum P2Pool Connections=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                MaxConnections = Buf
                            Else
                                MsgBox("'Maximum P2Pool Connections' setting invalid. Using default maximum connections.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Mining Idle (s)=") = True Then
                            Buf = Buf.replace("Mining Idle (s)=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                MiningIdle = Buf
                            Else
                                MsgBox("'Mining Idle' setting invalid. Using default mining idle.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Mining Restart Delay (s)=") = True Then
                            Buf = Buf.replace("Mining Restart Delay (s)=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                RestartDelay = Buf
                            Else
                                MsgBox("'Mining Restart Delay' setting invalid. Using default mining restart delay.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("P2Pool Port=") = True Then
                            Buf = Buf.replace("P2Pool Port=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                p2pool_port = Buf
                            Else
                                MsgBox("'P2Pool Port' setting invalid. Using default P2Pool port.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Mining Port=") = True Then
                            Buf = Buf.replace("Mining Port=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                mining_port = Buf
                            Else
                                MsgBox("'Mining Port' setting invalid. Using default mining port.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Mining Intensity=") = True Then
                            Buf = Buf.replace("Mining Intensity=", "")
                            If Decimal.TryParse(Buf, IntBuf) = True Then
                                Intensity = Buf
                            Else
                                MsgBox("'Mining Intensity' setting invalid. Using default mining intensity.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Worker Name=") = True Then
                            Buf = Buf.replace("Worker Name=", "")
                            If Not Buf = "" Then
                                Worker = Buf
                            Else
                                MsgBox("'Worker Name' setting invalid. Using default worker name.")
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Worker Password=") = True Then
                            Buf = Buf.replace("Worker Password=", "")
                            If Not Buf = "" Then
                                Password = Buf
                            Else
                                MsgBox("'Worker Password' setting invalid. Using default worker password.")
                            End If
                        End If
                        Buf = Reader.ReadLine ' Wallet Address that has been deprecated
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("P2Pool Fee Address=") = True Then
                            Buf = Buf.replace("P2Pool Fee Address=", "")
                            If Not Buf = "" Then
                                P2P_Node_Fee_Address = Buf
                            Else
                                MsgBox("'P2Pool Fee Address' setting invalid. Using default P2Pool fee address. Please check your fee address for errors.")
                            End If
                        End If
                        Pool_Address = ""
                        Do
                            Buf = Reader.ReadLine
                            If Not Buf.contains("Pool URL=") And Not Buf = "" Then
                                Exit Do
                            Else
                                If Not Buf.replace("Pool URL=", "") = "" Then
                                    Buf = Buf.replace("Pool URL=", "")
                                    If Pool_Address = "" Then
                                        Pool_Address = Buf
                                    Else
                                        If Not Pool_Address = Buf Then
                                            Pool_Address = Pool_Address & Environment.NewLine & Buf
                                        End If
                                    End If
                                Else
                                    'MsgBox("'Pool Address' setting invalid. Using default pool address. Please check your pool address for errors.")
                                End If
                            End If
                        Loop
                        If Not Buf = "" And Buf.contains("One-Click Version=") = True Then
                            Buf = Buf.replace("One-Click Version=", "")
                            If Not Buf = "" Then
                                'Miner_Version = Buf 'Allow the OCM to determine this value on load
                                Dim Old_Miner_Version = Buf
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("P2Pool Version=") = True Then
                            Buf = Buf.replace("P2Pool Version=", "")
                            If Not Buf = "" Then
                                P2Pool_Version = Buf
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("AMD Miner Version=") = True Then
                            Buf = Buf.replace("AMD Miner Version=", "")
                            If Not Buf = "" Then
                                AMD_Version = Buf
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Nvidia Miner Version=") = True Then
                            Buf = Buf.replace("Nvidia Miner Version=", "")
                            If Not Buf = "" Then
                                Nvidia_Version = Buf
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("CPU Miner Version=") = True Then
                            Buf = Buf.replace("CPU Miner Version=", "")
                            If Not Buf = "" Then
                                CPU_Version = Buf
                            End If
                        End If
                        Buf = Reader.ReadLine
                        If Not Buf = "" And Buf.contains("Default Miner=") = True Then
                            Buf = Buf.replace("Default Miner=", "")
                            If Not Buf = "" Then
                                DefaultMiner = Buf
                            End If
                        End If
                        Buf = Reader.ReadToEnd
                        If Not Buf = "" And Buf.contains("Additional Miner Config=") = True Then
                            Buf = Buf.replace("Additional Miner Config=", "")
                            If Not Buf = "" And Not Buf.contains("Default") Then
                                additional_config = Buf
                            End If
                        End If
                        Reader.Close()
                        Invoke(New MethodInvoker(AddressOf Update_Miner_Text))
                        System.Threading.Thread.Sleep(100)
                        Do
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
                        Loop
                    End If
                End With
            End If
        Catch ex As IOException
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() UpdateSettings: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() UpdateSettings: OK.")
        End Try

    End Sub

    Public Sub LoadSettingsIni()

        Try
            Dim Reader As New StreamReader(SettingsIni, False)
            Dim Buf As Object = Reader.ReadLine
            Dim IntBuf As Integer
            If Not Buf = "" Then
                appdata = Buf
            End If
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                start_minimized = "true"                        '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                start_minimized = "false"                       '
            End If                                              '
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                hide_windows = "true"                           '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                hide_windows = "false"                          '
            End If                                              '                                            
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                start_with_windows = "true"                     '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  'This section will use default variable values if field happens to be null
                start_with_windows = "false"                    '
            End If                                              '
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                autostart_p2pool = "true"                       '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                autostart_p2pool = "false"                      '
            End If                                              '
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                autostart_mining = "true"                       '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                autostart_mining = "false"                      '
            End If                                              '
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                start_mining_when_idle = "true"                 '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                start_mining_when_idle = "false"                '
            End If                                              '
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                Keep_Miner_Alive = "true"                       '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                Keep_Miner_Alive = "false"                      '
            End If                                              '
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                Keep_P2Pool_Alive = "true"                      '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                Keep_P2Pool_Alive = "false"                     '
            End If                                              '
            Buf = Reader.ReadLine                               '
            If Not Buf = "" And Buf.Contains("true") Then       '
                use_UPnP = "true"                               '
            ElseIf Not Buf = "" And Buf.Contains("false") Then  '
                use_UPnP = "false"                              '
            End If                                              '
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("P2Pool Network=") = True Then
                Buf = Buf.replace("P2Pool Network=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    P2P_Network = Buf
                Else
                    MsgBox("'P2Pool Network' setting invalid. Using default P2Pool network 1.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("P2Pool Node Fee (%)=") = True Then
                Buf = Buf.replace("P2Pool Node Fee (%)=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    P2P_Node_Fee = Buf
                Else
                    MsgBox("'P2Pool Node Fee' setting invalid. Using default P2Pool node fee percentage of 0%.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("P2Pool Node Donation (%)=") = True Then
                Buf = Buf.replace("P2Pool Node Donation (%)=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    P2P_Donation = Buf
                Else
                    MsgBox("'P2Pool Donation' setting invalid. Using default P2Pool donation percentage of 1%.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Maximum P2Pool Connections=") = True Then
                Buf = Buf.replace("Maximum P2Pool Connections=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    MaxConnections = Buf
                Else
                    MsgBox("'Maximum P2Pool Connections' setting invalid. Using default maximum connections.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Mining Idle (s)=") = True Then
                Buf = Buf.replace("Mining Idle (s)=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    MiningIdle = Buf
                Else
                    MsgBox("'Mining Idle' setting invalid. Using default mining idle.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Mining Restart Delay (s)=") = True Then
                Buf = Buf.replace("Mining Restart Delay (s)=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    RestartDelay = Buf
                Else
                    MsgBox("'Mining Restart Delay' setting invalid. Using default mining restart delay.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("P2Pool Port=") = True Then
                Buf = Buf.replace("P2Pool Port=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    p2pool_port = Buf
                Else
                    MsgBox("'P2Pool Port' setting invalid. Using default P2Pool port.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Mining Port=") = True Then
                Buf = Buf.replace("Mining Port=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    mining_port = Buf
                Else
                    MsgBox("'Mining Port' setting invalid. Using default mining port.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Mining Intensity=") = True Then
                Buf = Buf.replace("Mining Intensity=", "")
                If Decimal.TryParse(Buf, IntBuf) = True Then
                    Intensity = Buf
                Else
                    MsgBox("'Mining Intensity' setting invalid. Using default mining intensity.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Worker Name=") = True Then
                Buf = Buf.replace("Worker Name=", "")
                If Not Buf = "" Then
                    Worker = Buf
                Else
                    MsgBox("'Worker Name' setting invalid. Using default worker name.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Worker Password=") = True Then
                Buf = Buf.replace("Worker Password=", "")
                If Not Buf = "" Then
                    Password = Buf
                Else
                    MsgBox("'Worker Password' setting invalid. Using default worker password.")
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("P2Pool Fee Address=") = True Then
                Buf = Buf.replace("P2Pool Fee Address=", "")
                If Not Buf = "" Then
                    P2P_Node_Fee_Address = Buf
                Else
                    MsgBox("'P2Pool Fee Address' setting invalid. Using default P2Pool fee address. Please check your fee address for errors.")
                End If
            End If
            Pool_Address = ""
            Do
                Buf = Reader.ReadLine
                If Not Buf.contains("Pool URL=") And Not Buf = "" Then
                    Exit Do
                Else
                    If Not Buf.replace("Pool URL=", "") = "" Then
                        Buf = Buf.replace("Pool URL=", "")
                        If Pool_Address = "" Then
                            Pool_Address = Buf
                        Else
                            If Not Pool_Address = Buf Then
                                Pool_Address = Pool_Address & Environment.NewLine & Buf
                            End If
                        End If
                    Else
                        'MsgBox("'Pool Address' setting invalid. Using default pool address. Please check your pool address for errors.")
                    End If
                End If
            Loop
            If Not Buf = "" And Buf.contains("One-Click Version=") = True Then
                Buf = Buf.replace("One-Click Version=", "")
                If Not Buf = "" Then
                    'Miner_Version = Buf 'Allow the OCM to determine this value on load
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("P2Pool Version=") = True Then
                Buf = Buf.replace("P2Pool Version=", "")
                If Not Buf = "" Then
                    P2Pool_Version = Buf
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("AMD Miner Version=") = True Then
                Buf = Buf.replace("AMD Miner Version=", "")
                If Not Buf = "" Then
                    AMD_Version = Buf
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Nvidia Miner Version=") = True Then
                Buf = Buf.replace("Nvidia Miner Version=", "")
                If Not Buf = "" Then
                    Nvidia_Version = Buf
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("CPU Miner Version=") = True Then
                Buf = Buf.replace("CPU Miner Version=", "")
                If Not Buf = "" Then
                    CPU_Version = Buf
                End If
            End If
            Buf = Reader.ReadLine
            If Not Buf = "" And Buf.contains("Default Miner=") = True Then
                Buf = Buf.replace("Default Miner=", "")
                If Not Buf = "" Then
                    DefaultMiner = Buf
                End If
            End If
            Buf = Reader.ReadToEnd
            If Not Buf = "" And Buf.contains("Additional Miner Config=") = True Then
                Buf = Buf.replace("Additional Miner Config=", "")
                If Not Buf = "" And Not Buf.contains("Default") Then
                    additional_config = Buf
                End If
            End If
            Reader.Close()
            Invoke(New MethodInvoker(AddressOf Update_Miner_Text))
        Catch ex As IOException
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() LoadSettings: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() LoadSettings: OK.")
        End Try

    End Sub

    Public Sub SaveSettingsIni()

        Do
            Try
                If Not Intensity_Text.Text = "" Then
                    Intensity = Convert.ToDecimal(Intensity_Text.Text)
                End If
                If Not Worker_Address_Text.Text = "" Then
                    Worker = Worker_Address_Text.Text
                End If
                If Not Password_Text.Text = "" Then
                    Password = Password_Text.Text
                End If
                If Not Pool_Address_Text.Text = "" Then
                    Pool_Address = Pool_Address_Text.Text
                End If
                If Not Additional_Configuration_Text.Text = "" Then
                    additional_config = Additional_Configuration_Text.Text
                End If
                If System.IO.File.Exists(SettingsIni) = True Then
                    Dim objWriter As New System.IO.StreamWriter(SettingsIni)
                    objWriter.WriteLine(appdata)
                    If start_minimized = True Then
                        objWriter.WriteLine("Start Minimized=true")
                    Else
                        objWriter.WriteLine("Start Minimized=false")
                    End If
                    If hide_windows = True Then
                        objWriter.WriteLine("Hide Windows=true")
                    Else
                        objWriter.WriteLine("Hide Windows=false")
                    End If
                    If start_with_windows = True Then
                        objWriter.WriteLine("Start With Windows=true")
                    Else
                        objWriter.WriteLine("Start With Windows=false")
                    End If
                    If autostart_p2pool = True Then
                        objWriter.WriteLine("Autostart P2Pool=true")
                    Else
                        objWriter.WriteLine("Autostart P2Pool=false")
                    End If
                    If autostart_mining = True Then
                        objWriter.WriteLine("Autostart Mining=true")
                    Else
                        objWriter.WriteLine("Autostart Mining=false")
                    End If
                    If start_mining_when_idle = True Then
                        objWriter.WriteLine("Mine When Idle=true")
                    Else
                        objWriter.WriteLine("Mine When Idle=false")
                    End If
                    If Keep_Miner_Alive = True Then
                        objWriter.WriteLine("Keep Miner Alive=true")
                    Else
                        objWriter.WriteLine("Keep Miner Alive=false")
                    End If
                    If Keep_P2Pool_Alive = True Then
                        objWriter.WriteLine("Keep P2Pool Alive=true")
                    Else
                        objWriter.WriteLine("Keep P2Pool Alive=false")
                    End If
                    If use_UPnP = True Then
                        objWriter.WriteLine("Use UPnP=true")
                    Else
                        objWriter.WriteLine("Use UPnP=false")
                    End If
                    objWriter.WriteLine("P2Pool Network=" & P2P_Network)
                    objWriter.WriteLine("P2Pool Node Fee (%)=" & P2P_Node_Fee)
                    objWriter.WriteLine("P2Pool Donation (%)=" & P2P_Donation)
                    objWriter.WriteLine("Maximum P2Pool Connections=" & MaxConnections)
                    objWriter.WriteLine("Mining Idle (s)=" & MiningIdle)
                    objWriter.WriteLine("Mining Restart Delay (s)=" & RestartDelay)
                    objWriter.WriteLine("P2Pool Port=" & p2pool_port)
                    objWriter.WriteLine("Mining Port=" & mining_port)
                    objWriter.WriteLine("Mining Intensity=" & Intensity)
                    objWriter.WriteLine("Worker Name=" & Worker)
                    objWriter.WriteLine("Worker Password=" & Password)
                    objWriter.WriteLine("P2Pool Fee Address=" & P2P_Node_Fee_Address)
                    If Pool_Address.Split(vbCrLf).Length > 1 Then
                        For Each line As String In Pool_Address.Split(vbCrLf)
                            objWriter.WriteLine("Pool URL=" & line.Trim())
                        Next
                        'objWriter.Write(Environment.NewLine)
                    Else
                        objWriter.WriteLine("Pool URL=" & Pool_Address)
                    End If
                    objWriter.WriteLine("One-Click Version=" & Miner_Version)
                    objWriter.WriteLine("P2Pool Version=" & P2Pool_Version)
                    objWriter.WriteLine("AMD Miner Version=" & AMD_Version)
                    objWriter.WriteLine("Nvidia Miner Version=" & Nvidia_Version)
                    objWriter.WriteLine("CPU Miner Version=" & CPU_Version)
                    objWriter.WriteLine("Default Miner=" & DefaultMiner)
                    objWriter.Write("Additional Miner Config=" & additional_config)
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
            End Try
        Loop

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            For Each line As String In Pool_Address.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                If Not line.Contains("http://") And Not line.Contains("stratum+tcp://") Then
                    line = "stratum+tcp://" & line
                Else
                    line = line.Replace("http://", "stratum+tcp://")
                End If
            Next
            If P2P_Network = 1 Then
                If mining_port = "9181" Or mining_port = "" Then
                    mining_port = "9171"
                End If
            ElseIf P2P_Network = 2 Then
                If mining_port = "9171" Or mining_port = "" Then
                    mining_port = "9181"
                End If
            End If
            If Not Pool_Address.Contains("localhost:" & mining_port) Then
                Pool_Address = "http://localhost:" & mining_port & Environment.NewLine & Pool_Address
            End If
            Pool_Address_Text.Text = Pool_Address
            Pool_Address_Text.SelectionStart = 0
            Pool_Address_Text.ScrollToCaret()
            Pool_Address_Text.Enabled = False
        Else
            Pool_Address_Text.Enabled = True
            'remove localhost address from pool list
            For Each line As String In Pool_Address_Text.Lines
                If line.Contains("localhost:" & mining_port) And Pool_Address_Text.Lines.Count > 1 Then
                    Pool_Address_Text.Text = Pool_Address_Text.Text.Replace(line, "")
                    Pool_Address_Text.Text = Pool_Address_Text.Text.Remove(0, 1)
                    Pool_Address = Pool_Address_Text.Text
                End If
            Next
        End If

    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click

        settings.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If amd_check.Checked = False Then
            amd_check.Checked = True
            DefaultMiner = "amd"
        End If
        Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        'Checks if AMD miner has already been downloaded and installed
        If System.IO.Directory.Exists(AmdFolder) = True Then
            For Each file As String In Directory.GetFiles(AmdFolder)
                If file.Contains("ocm_sgminer.exe") Then
                    mining_installed = True
                    Exit For
                Else
                    mining_installed = False
                End If
            Next
        End If
        'Starts mining if miner software is already detected.  If not, downloads miner software.
        If Button2.Text.Contains("AMD") Then
            AmdMiner = True
            NvidiaMiner = False
            CPUMiner = False
            If mining_installed = True Then
                mining_initialized = True
                nvidia_check.Enabled = False
                Button4.Enabled = False
                cpu_check.Enabled = False
                Button5.Enabled = False
                BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
            Else
                If Not Updater.IsBusy Then
                    amd_update = True
                    canceldownloadasync = False
                    Updater.RunWorkerAsync()
                End If
            End If
        Else
            AmdMiner = False
            mining_initialized = False
            BeginInvoke(New MethodInvoker(AddressOf Stop_Miner))
            BeginInvoke(New MethodInvoker(AddressOf Kill_Miner))
        End If
        mining_installed = False

    End Sub

    Public Sub Update_P2Pool_Config()

        Try
            exe = SettingsFolder & "\p2pool\ocm_p2pool.exe"
            p2pool_config_file = SettingsFolder & "\p2pool\start_p2pool.bat"
            Dim network As String = ""
            If P2P_Network = 0 Or P2P_Network = 1 Then
                network = " --net vertcoin"
                'Allows any custom port other than default ports.
                If p2pool_port = "9347" Then
                    p2pool_port = "9346"
                End If
                If mining_port = "9181" Then
                    mining_port = "9171"
                End If
            ElseIf P2P_Network = 2 Then
                network = " --net vertcoin2"
                'Allows any custom port other than default ports.
                If p2pool_port = "9346" Then
                    p2pool_port = "9347"
                End If
                If mining_port = "9171" Then
                    mining_port = "9181"
                End If
            End If
            If Not appdata.Contains("AppData") Then
                p2pool_config = "ocm_p2pool.exe" & network & " --give-author " & P2P_Donation & " --fee " & P2P_Node_Fee & " --address " & P2P_Node_Fee_Address & " --max-conns " & MaxConnections & " --worker-port " & mining_port & " --p2pool-port " & p2pool_port & "--bitcoind-config-path " & appdata & "\vertcoin.conf" & Environment.NewLine & "exit /B"
            Else
                p2pool_config = "ocm_p2pool.exe" & network & " --give-author " & P2P_Donation & " --fee " & P2P_Node_Fee & " --address " & P2P_Node_Fee_Address & " --max-conns " & MaxConnections & " --worker-port " & mining_port & " --p2pool-port " & p2pool_port & Environment.NewLine & "exit /B"
            End If
            If System.IO.File.Exists(p2pool_config_file) = True Then
                command = File.ReadAllText(p2pool_config_file)
            End If
            If Not p2pool_config = command Then
                command = p2pool_config
                File.WriteAllText(p2pool_config_file, command)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Update_P2Pool_Config: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Update_P2Pool_Config: OK.")
        End Try

    End Sub

    Public Sub Update_Miner_Config()

        Try
            Dim Pool As String = ""
            If Not Pool_Address = "" Then
                For Each line As String In Pool_Address.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                    If Not line.Contains("http://") And Not line.Contains("stratum+tcp://") Then
                        line = "stratum+tcp://" & line
                    Else
                        line = line.Replace("http://", "stratum+tcp://")
                    End If
                    If line.Contains("localhost:" & mining_port) Then
                        Pool_Address = Pool_Address.Replace(line & Environment.NewLine, "")
                    End If
                    Pool = Pool & "-o " & line.Replace(vbCr, "").Replace(vbLf, "") & " "
                Next
            Else
                Pool = "-o http://localhost:" & mining_port
            End If
            If Not Worker_Address_Text.Text = "" Then
                Worker = Worker_Address_Text.Text
            Else
                MsgBox("No valid address detected, using default address to developer fund.")
                Worker = "VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG"
            End If
            If Not Password_Text.Text = "" Then
                Password = Password_Text.Text
            Else
                Password = "x"
            End If
            Dim Intensity_Buffer As String = ""
            If Not Intensity_Text.Text = "" And Not Intensity_Text.Text = 0.ToString Then
                Intensity = Intensity_Text.Text
                If AmdMiner = True Then
                    Intensity_Buffer = " -I " & Intensity
                ElseIf NvidiaMiner = True Then
                    Intensity_Buffer = " -i " & Intensity
                End If
            ElseIf Intensity_Text.Text = "" Or Intensity = 0 Then
                Intensity_Buffer = ""
            End If
            additional_config = Additional_Configuration_Text.Text
            If AmdMiner = True Then
                miner_config_file = SettingsFolder & "\amd\config.bat"
                '"setx GPU_MAX_HEAP_SIZE 100" & Environment.NewLine & "setx GPU_MAX_ALLOC_PERCENT 100" & Environment.NewLine & 
                miner_config = "ocm_sgminer.exe --kernel Lyra2REv2 " & "-u " & Worker & " -p " & Password & Intensity_Buffer & " " & additional_config & " " & Pool & Environment.NewLine & "exit /B"
            ElseIf NvidiaMiner = True Then
                miner_config_file = SettingsFolder & "\nvidia\config.bat"
                miner_config = "ocm_ccminer.exe -a lyra2v2 " & "-u " & Worker & " -p " & Password & Intensity_Buffer & " " & additional_config & " " & Pool & Environment.NewLine & "exit /B"
            ElseIf CPUMiner = True Then
                miner_config_file = SettingsFolder & "\cpu\config.bat"
                miner_config = "ocm_cpuminer.exe -a lyra2rev2 " & "-u " & Worker & " -p " & Password & " " & additional_config & " " & Pool & Environment.NewLine & "exit /B"
            End If
            'Update miner config
            Dim objWriter As New System.IO.StreamWriter(miner_config_file)
            objWriter.WriteLine(miner_config)
            objWriter.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Update_Miner_Config: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Update_Miner_Config: OK.")
        End Try

    End Sub

    Public Sub Start_Miner()

        Try
            Invoke(New MethodInvoker(AddressOf Update_Miner_Config))
            If AmdMiner = True Then
                miner_config = AmdFolder & "\config.bat"
            ElseIf NvidiaMiner = True Then
                miner_config = NvidiaFolder & "\config.bat"
            ElseIf CPUMiner = True Then
                miner_config = CPUFolder & "\config.bat"
            End If
            cc = False
            sg = False
            cpu = False
            Dim pProcess() As Process = Process.GetProcesses
            For Each p As Process In pProcess
                If p.ProcessName.Contains("ocm_ccminer") Then
                    cc = True
                End If
                If p.ProcessName.Contains("ocm_sgminer") Then
                    sg = True
                End If
                If p.ProcessName.Contains("ocm_cpuminer") Then
                    cpu = True
                End If
            Next
            If cc = True Or sg = True Or cpu = True Then
                ' Process is running
                MsgBox("A miner is already running.")
                amd_check.Enabled = True
                Button2.Enabled = True
                nvidia_check.Enabled = True
                Button4.Enabled = True
                cpu_check.Enabled = True
                Button5.Enabled = True
                Pool_Address_Text.Enabled = True
                Worker_Address_Text.Enabled = True
                Worker_Address_Text.Enabled = True
                Password_Text.Enabled = True
                Intensity_Text.Enabled = True
                Additional_Configuration_Text.Enabled = True
                CheckBox1.Enabled = True
                PictureBox2.Image = VertcoinOneClickMiner.My.Resources.Resources.off_small
                TextBox2.Text = "Offline"
                Button2.Text = "AMD"
                PictureBox5.Image = VertcoinOneClickMiner.My.Resources.Resources.off_small
                TextBox2.Text = "Offline"
                Button4.Text = "Nvidia"
                PictureBox7.Image = VertcoinOneClickMiner.My.Resources.Resources.off_small
                TextBox2.Text = "Offline"
                Button5.Text = "CPU"
            Else
                ' Process is not running
                mining_running = True
                If Uptime_Timer.Enabled = False Then
                    Uptime_Timer.Start()
                End If
                command = File.ReadAllText(miner_config)
                command = command.Replace(Environment.NewLine, " & ")
                Dim psi As New ProcessStartInfo("cmd")
                If hide_windows = True Then
                    psi.WindowStyle = ProcessWindowStyle.Minimized
                Else
                    psi.WindowStyle = ProcessWindowStyle.Normal
                End If
                If AmdMiner = True Then
                    psi.Arguments = ("/K cd /d" & AmdFolder & " & " & command)
                    PictureBox2.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
                    Button2.Text = "Disable"
                ElseIf NvidiaMiner = True Then
                    psi.Arguments = ("/K cd /d" & NvidiaFolder & " & " & command)
                    PictureBox5.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
                    Button4.Text = "Disable"
                ElseIf CPUMiner = True Then
                    psi.Arguments = ("/K cd /d" & CPUFolder & " & " & command)
                    PictureBox7.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
                    Button5.Text = "Disable"
                End If
                Dim process As Process = Process.Start(psi)
                CheckBox1.Enabled = False
                TextBox2.Text = "Online"
                Pool_Address_Text.Enabled = False
                Worker_Address_Text.Enabled = False
                Worker_Address_Text.Enabled = False
                Password_Text.Enabled = False
                Intensity_Text.Enabled = False
                Additional_Configuration_Text.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Start_Miner: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Start_Miner: OK.")
        End Try

    End Sub

    Public Sub Kill_Miner()

        Try
            mining_running = False
            If p2pool_running = False Then
                Uptime_Timer.Stop()
                Uptime_Checker.CancelAsync()
            End If
            For Each p As Process In System.Diagnostics.Process.GetProcesses
                If p.ProcessName.Contains("ocm_ccminer") Or p.ProcessName.Contains("ocm_sgminer") Or p.ProcessName.Contains("ocm_cpuminer") Then
                    p.Kill()
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Kill_Miner: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Kill_Miner: OK.")
        End Try

    End Sub

    Public Sub Stop_Miner()

        Try
            amd_check.Enabled = True
            Button2.Enabled = True
            nvidia_check.Enabled = True
            Button4.Enabled = True
            cpu_check.Enabled = True
            Button5.Enabled = True
            Pool_Address_Text.Enabled = True
            Worker_Address_Text.Enabled = True
            Worker_Address_Text.Enabled = True
            Password_Text.Enabled = True
            Intensity_Text.Enabled = True
            Additional_Configuration_Text.Enabled = True
            CheckBox1.Enabled = True
            PictureBox2.Image = VertcoinOneClickMiner.My.Resources.Resources.off_small
            TextBox2.Text = "Offline"
            Button2.Text = "AMD"
            PictureBox5.Image = VertcoinOneClickMiner.My.Resources.Resources.off_small
            TextBox2.Text = "Offline"
            Button4.Text = "Nvidia"
            PictureBox7.Image = VertcoinOneClickMiner.My.Resources.Resources.off_small
            TextBox2.Text = "Offline"
            Button5.Text = "CPU"
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Stop_Miner: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Stop_Miner: OK.")
        End Try

    End Sub

    Public Sub Start_P2Pool()

        Try
            Invoke(New MethodInvoker(AddressOf Update_P2Pool_Config))
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
            If use_UPnP = True Then
                Invoke(New MethodInvoker(AddressOf UPnP))
            End If
            Dim P2Pool_Detected = False
            For Each p As Process In System.Diagnostics.Process.GetProcesses
                If p.ProcessName.Contains("ocm_p2pool") Then
                    'If P2Pool is already running
                    P2Pool_Detected = True
                Else
                    'If P2Pool is not running
                    p2pool_running = True
                End If
            Next
            If P2Pool_Detected = True Then
                ' Process is running
                MsgBox("P2Pool is already running.")
            Else
                ' Process is not running
                If Uptime_Timer.Enabled = False Then
                    Uptime_Timer.Start()
                End If
                Dim psi As New ProcessStartInfo("cmd")
                If hide_windows = True Then
                    psi.WindowStyle = ProcessWindowStyle.Minimized
                Else
                    psi.WindowStyle = ProcessWindowStyle.Normal
                End If
                command = File.ReadAllText(p2pool_config_file)
                command = command.Replace(Environment.NewLine, " & ")
                psi.Arguments = ("/K cd /d" & P2PoolFolder & " & " & command)
                Dim process As Process = Process.Start(psi)
                PictureBox1.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
                TextBox1.Text = "Online"
                Button1.Text = "Disable"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Start_P2Pool: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Start_P2Pool: OK.")
        End Try

    End Sub

    Public Sub Kill_P2Pool()

        Try
            p2pool_running = False
            If mining_running = False Then
                Uptime_Timer.Stop()
                Uptime_Checker.CancelAsync()
            End If
            For Each p As Process In System.Diagnostics.Process.GetProcesses
                If p.ProcessName.Contains("ocm_p2pool") Then
                    p.Kill()
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Kill_P2Pool: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Kill_P2Pool: OK.")
        End Try

    End Sub

    Public Sub Stop_P2Pool()

        Try
            PictureBox1.Image = VertcoinOneClickMiner.My.Resources.Resources.off_small
            TextBox1.Text = "Offline"
            Button1.Text = "Enable"
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Stop_P2Pool: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Stop_P2Pool: OK.")
        End Try

    End Sub

    Private Sub UpdateInterval_Tick(sender As Object, e As EventArgs) Handles UpdateInterval.Tick

        'Updates stats every 2 seconds
        UpdateStats.RunWorkerAsync()
        'Automatically checks for updates after launch and prompts user via label on top-right main screen.
        If autocheck_updates = False Then
            Auto_Update_Notify.RunWorkerAsync()
            autocheck_updates = True
        End If

    End Sub

    Private Sub Auto_Update_Notify_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Auto_Update_Notify.DoWork

        Dim url As New System.Uri("http://vtc.alwayshashing.com")
        Dim connection
        'Request connection
        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)
        Dim resp As System.Net.WebResponse
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            connection = True
        Catch ex As Exception
            req = Nothing
            connection = False
        End Try
        If connection = True Then
            Dim tempnewestversion As New Version
            Dim templink As String = ""
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://alwayshashing.com/ocm_versions.txt")
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            'Compares current One-Click Miner version with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("miner=", ""))
            templink = sr.ReadLine
            If tempnewestversion > System.Version.Parse(Miner_Version) Then
                update_needed = True
            End If
            'Compares the current version of P2Pool with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("p2pool=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(P2Pool_Version)) And Not (System.Version.Parse(P2Pool_Version) = System.Version.Parse("0.0.0.0")) Then
                update_needed = True
            End If
            'Compares the current version of the AMD miner with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("amd=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(AMD_Version)) And Not (System.Version.Parse(AMD_Version) = System.Version.Parse("0.0.0.0")) And (System.IO.File.Exists(AmdFolder & "\ocm_sgminer.exe") = True) Then
                update_needed = True
            End If
            'Compares the current version of the Nvidia miner with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("nvidia=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(Nvidia_Version)) And Not (System.Version.Parse(Nvidia_Version) = System.Version.Parse("0.0.0.0")) And (System.IO.File.Exists(NvidiaFolder & "\ocm_ccminer.exe") = True) Then
                update_needed = True
            End If
            'Compares the current version of the CPU miner with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("cpu=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(CPU_Version)) And Not (System.Version.Parse(CPU_Version) = System.Version.Parse("0.0.0.0")) And (System.IO.File.Exists(CPUFolder & "\ocm_cpuminer.exe") = True) Then
                update_needed = True
            End If
            Invoke(New MethodInvoker(AddressOf Update_Notification))
            response.Dispose()
            sr.Dispose()
        End If
        Auto_Update_Notify.CancelAsync()

    End Sub

    Public Sub Update_Notification()

        If update_needed = True Then
            Label7.Enabled = True
            Label7.Visible = True
        Else
            Label7.Enabled = False
            Label7.Visible = False
        End If
        update_needed = False

    End Sub

    Private Sub UpdateStats_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles UpdateStats.DoWork

        BeginInvoke(New MethodInvoker(AddressOf Update_P2Pool_Text))

    End Sub

    Private Sub FileDirectoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileDirectoryToolStripMenuItem.Click

        Try
            If amd_check.Checked = True Then
                miner_config_file = SettingsFolder & "\amd\config.bat"
            ElseIf nvidia_check.Checked = True Then
                miner_config_file = SettingsFolder & "\nvidia\config.bat"
            ElseIf cpu_check.Checked = True Then
                miner_config_file = SettingsFolder & "\cpu\config.bat"
            End If
            If System.IO.File.Exists(miner_config_file) = True Then
                Process.Start("notepad.exe", miner_config_file)
            Else
                MsgBox("No miner config file found.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "MinerConfigToolStripMenuItem(), Load Miner Config: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "MinerConfigToolStripMenuItem(), Load Miner Config: OK")
        End Try

    End Sub

    Private Sub SystemLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SystemLogToolStripMenuItem.Click

        Try
            Process.Start("notepad.exe", SysLog)
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "SystemLogToolStripMenuItem(), Load Miner Log: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "SystemLogToolStripMenuItem(), Load Miner Log: OK")
        End Try

    End Sub

    Private Sub P2PoolConfigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P2PoolConfigToolStripMenuItem.Click

        Try
            p2pool_config_file = SettingsFolder & "\p2pool\start_p2pool.bat"
            If System.IO.File.Exists(p2pool_config_file) = True Then
                Process.Start("notepad.exe", p2pool_config_file)
            Else
                MsgBox("No p2pool config file found.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "P2PoolConfigToolStripMenuItem(), Load P2Pool Config: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "P2PoolConfigToolStripMenuItem(), Load P2Pool Config: OK")
        End Try

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

        about.Show()

    End Sub

    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click

        Try
            Dim url As String = "http://slack.vtconline.org"
            Process.Start(url)
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Contact(), " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Contact(), Load Browser: OK")
        End Try

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        Application.Exit()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try
            Dim url As String = "http://localhost:" & mining_port
            Process.Start(url)
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Contact(), " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Contact(), Load Browser: OK")
        End Try

    End Sub

    Private Sub Uptime_Timer_Tick(sender As Object, e As EventArgs) Handles Uptime_Timer.Tick

        'Checks to ensure p2pool and miner are always running.
        If Not (Uptime_Checker.IsBusy) Then
            Uptime_Checker.RunWorkerAsync()
        End If

    End Sub

    Private Sub Uptime_Checker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Uptime_Checker.DoWork

        Try
            Dim ocmminer As Boolean
            Dim ocmp2pool As Boolean
            P2Pool_Detected = False
            AMD_Detected = False
            Nvidia_Detected = False
            CPU_Detected = False
            For Each p As Process In System.Diagnostics.Process.GetProcesses
                If p.ProcessName.Contains("ocm_p2pool") Then
                    P2Pool_Detected = True
                    ocmp2pool = True
                ElseIf p.ProcessName.Contains("ocm_sgminer") Then
                    AMD_Detected = True
                    ocmminer = True
                ElseIf p.ProcessName.Contains("ocm_ccminer") Then
                    Nvidia_Detected = True
                    ocmminer = True
                ElseIf p.ProcessName.Contains("ocm_cpuminer") Then
                    CPU_Detected = True
                    ocmminer = True
                ElseIf p.ProcessName.Contains("run_p2pool") Or p.ProcessName.Contains("p2pool") Then
                    otherp2pool = True
                ElseIf p.ProcessName.Contains("ccminer") Or p.ProcessName.Contains("sgminer") Or p.ProcessName.Contains("cpuminer") Then
                    otherminer = True
                End If
                BeginInvoke(New MethodInvoker(AddressOf update_stats))
            Next
            If ocmp2pool = True And ocmminer = True Then
                'P2Pool and Miner are running, do nothing
            Else
                If mining_running = True Then 'Miner initialized
                    If ocmminer = False Then
                        If Keep_Miner_Alive = True Then
                            BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                        Else
                            BeginInvoke(New MethodInvoker(AddressOf Stop_Miner))
                        End If
                    Else
                    End If
                Else 'Miner not initialized
                    'BeginInvoke(New MethodInvoker(AddressOf Stop_P2Pool))
                    'BeginInvoke(New MethodInvoker(AddressOf kill_Miner))
                End If
                If p2pool_running = True Then
                    If ocmp2pool = False Then
                        If Keep_P2Pool_Alive = True Then
                            BeginInvoke(New MethodInvoker(AddressOf Start_P2Pool))
                        Else
                            BeginInvoke(New MethodInvoker(AddressOf Stop_P2Pool))
                        End If
                    End If
                Else 'P2Pool not initialized
                    'BeginInvoke(New MethodInvoker(AddressOf Stop_P2Pool))
                    'BeginInvoke(New MethodInvoker(AddressOf kill_P2Pool))
                End If
            End If
            Uptime_Checker.CancelAsync()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub update_stats()
        'Miner is already running
        If AMD_Detected = True Or Nvidia_Detected = True Or CPU_Detected = True Then
            Additional_Configuration_Text.Enabled = False
            Pool_Address_Text.Enabled = False
            Worker_Address_Text.Enabled = False
            Worker_Address_Text.Enabled = False
            Password_Text.Enabled = False
            Intensity_Text.Enabled = False
            CheckBox1.Enabled = False
            Uptime_Timer.Start()
        End If
        If AMD_Detected = True Then
            PictureBox2.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
            TextBox2.Text = "Online"
            Button2.Text = "Disable"
        End If
        If Nvidia_Detected = True Then
            PictureBox5.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
            TextBox2.Text = "Online"
            Button4.Text = "Disable"
        End If
        If CPU_Detected = True Then
            PictureBox7.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
            TextBox2.Text = "Online"
            Button5.Text = "Disable"
        End If
        'P2Pool is already running
        If P2Pool_Detected = True Then
            PictureBox1.Image = VertcoinOneClickMiner.My.Resources.Resources.on_small
            TextBox1.Text = "Online"
            Button1.Text = "Disable"
            Uptime_Timer.Start()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        'See if P2Pool has already been downloaded/installed
        If System.IO.Directory.Exists(P2PoolFolder) = True Then
            For Each file As String In Directory.GetFiles(P2PoolFolder)
                If file.Contains("ocm_p2pool.exe") Then
                    p2pool_installed = True
                    Exit For
                Else
                    p2pool_installed = False
                End If
            Next
        End If
        'Starts p2pool if p2pool software is already detected.  If not, downloads p2pool software.
        If Button1.Text.Contains("Enable") Then
            If p2pool_installed = True Then
                Invoke(New MethodInvoker(AddressOf Check_RPC_Settings))
                If p2pool_initialized = True Then
                    BeginInvoke(New MethodInvoker(AddressOf Start_P2Pool))
                End If
            Else
                If Not Updater.IsBusy Then
                    p2pool_update = True
                    canceldownloadasync = False
                    Updater.RunWorkerAsync()
                End If
            End If
        Else
            p2pool_initialized = False
            BeginInvoke(New MethodInvoker(AddressOf Stop_P2Pool))
            BeginInvoke(New MethodInvoker(AddressOf Kill_P2Pool))
        End If
        p2pool_installed = False

    End Sub

    Public Sub Check_RPC_Settings()

        Try
            'Check for RPC information from Vertcoin Wallet
            If appdata = "" Then
                appdata = GetFolderPath(SpecialFolder.ApplicationData) & "\Vertcoin"
            End If
            Dim conf_check As Boolean = False
            Dim cancel As Boolean = False
            Dim buffer As String = ""
            If System.IO.Directory.Exists(appdata) = False Then
                Dim result1 As DialogResult = MsgBox("The default Vertcoin data directory was not found.  Would you like to select where it can be found?", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    Dim result2 As Windows.Forms.DialogResult = Select_Data_Dir.ShowDialog()
                    If result2 = Windows.Forms.DialogResult.OK Then
                        appdata = Select_Data_Dir.SelectedPath
                        p2pool_initialized = True
                    ElseIf result2 = Windows.Forms.DialogResult.Cancel Then
                        cancel = True
                    End If
                Else
                    cancel = True
                End If
            End If
            If cancel = False Then
                Dim files() As String = IO.Directory.GetFiles(appdata)
                For Each file As String In files
                    If conf_check = False Then
                        If file.Contains("vertcoin.conf") Then
                            Using reader As New System.IO.StreamReader(file)
                                While Not reader.EndOfStream
                                    buffer = reader.ReadLine
                                    If buffer.Contains("rpcuser") Then
                                        rpc_user = buffer.Replace("rpcuser=", "")
                                    ElseIf buffer.Contains("rpcpassword") Then
                                        rpc_password = buffer.Replace("rpcpassword=", "")
                                    ElseIf buffer.Contains("rpcport") Then
                                        rpc_port = buffer.Replace("rpcport=", "")
                                    ElseIf buffer.Contains("server=0") Then 'forces server=1
                                        config_server = ""
                                    ElseIf buffer.Contains("server=1") Then
                                        config_server = "server=1"
                                    ElseIf buffer = "server=" Then 'forces server=1
                                        config_server = ""
                                    ElseIf buffer.Contains("rpcallowip=") Then
                                        rpc_allowip = buffer.Replace("rpcallowip=", "")
                                    End If
                                End While
                            End Using
                            conf_check = True
                        End If
                    End If
                Next
                If (rpc_user = "" Or rpc_password = "" Or rpc_port = "" Or config_server = "" Or rpc_allowip = "") And conf_check = True Then
                    Dim result1 As DialogResult = MsgBox("RPC setting(s) not found in wallet configuration file. Would you like Vertcoin One-Click Miner to generate and append RPC settings to configuration file?", MessageBoxButtons.OKCancel)
                    If result1 = DialogResult.OK Then
                        MsgBox("If your Vertcoin wallet is currently running, please close it and click OK.")
                        Invoke(New MethodInvoker(AddressOf Generate_RPC_Settings))
                        Dim do_once_config_server As Boolean = False
                        Dim do_once_rpcallow As Boolean = False
                        Dim do_once_rpcuser As Boolean = False
                        Dim do_once_rpcpassword As Boolean = False
                        Dim do_once_rpcport As Boolean = False
                        Do
                            Try
                                Dim Config_Buffer As String = File.ReadAllText(appdata & "\vertcoin.conf")
                                Dim objWriter As New System.IO.StreamWriter(appdata & "\vertcoin_buffer.conf")
                                For Each Line As String In File.ReadLines(appdata & "\vertcoin.conf")
                                    If do_once_config_server = False Then
                                        config_server = "server=1"
                                        objWriter.WriteLine(config_server) 'ALWAYS Changes server= to server=1 regardless of setting so p2pool can connect
                                        do_once_config_server = True
                                    End If
                                    If do_once_rpcallow = False Then
                                        If (Line.Contains("rpcallowip=") And Line.Length >= 18) Or rpc_allowip = "" Then
                                            If Not Line = "rpcallowip=127.0.0.1" Then
                                                objWriter.WriteLine(Line) 'Moves previous rpcallowip's to new config and adds localhost
                                            End If
                                            objWriter.WriteLine("rpcallowip=127.0.0.1")
                                            do_once_rpcallow = True
                                        End If
                                    End If
                                    If do_once_rpcuser = False Then
                                        If Line.Contains("rpcuser=") And Line.Length >= 9 Then
                                            objWriter.WriteLine(Line)
                                            do_once_rpcuser = True
                                        Else
                                            objWriter.WriteLine("rpcuser=" & rpc_user)
                                            do_once_rpcuser = True
                                        End If
                                    End If
                                    If do_once_rpcpassword = False Then
                                        If Line.Contains("rpcpassword=") And Line.Length >= 13 Then
                                            objWriter.WriteLine(Line)
                                            do_once_rpcpassword = True
                                        Else
                                            objWriter.WriteLine("rpcpassword=" & rpc_password)
                                            do_once_rpcpassword = True
                                        End If
                                    End If
                                    If do_once_rpcport = False Then
                                        If Line.Contains("rpcport=") And Line.Length >= 9 Then
                                            objWriter.WriteLine(Line)
                                            do_once_rpcport = True
                                        Else
                                            objWriter.WriteLine("rpcport=" & rpc_port)
                                            do_once_rpcport = True
                                        End If
                                    End If
                                    If Not Line.Contains("rpcallowip=") And Not Line.Contains("server=") And Not Line.Contains("rpcuser=") And Not Line.Contains("rpcpassword=") And Not Line.Contains("rpcport=") Then
                                        objWriter.WriteLine(Line)
                                    End If
                                Next
                                My.Computer.FileSystem.MoveFile(appdata & "\vertcoin.conf", appdata & "\vertcoin_old.conf", True)
                                objWriter.Close()
                                My.Computer.FileSystem.MoveFile(appdata & "\vertcoin_buffer.conf", appdata & "\vertcoin.conf", True)
                                Exit Do
                            Catch ex As IOException
                                'Settings.ini is still in use so pause before trying again.
                                System.Threading.Thread.Sleep(100)
                            End Try
                        Loop
                        p2pool_initialized = True
                    End If
                End If
                If conf_check = False Then
                    Dim result2 As DialogResult = MsgBox("Wallet configuration file not found, would you like Vertcoin One-Click Miner to create one?", MessageBoxButtons.OKCancel)
                    If result2 = DialogResult.OK Then
                        If System.IO.File.Exists(appdata & "\vertcoin.conf") = False Then
                            File.Create(appdata & "\vertcoin.conf").Dispose()
                            Invoke(New MethodInvoker(AddressOf Generate_RPC_Settings))
                            Do
                                Try
                                    Dim objWriter As New System.IO.StreamWriter(appdata & "\vertcoin.conf")
                                    objWriter.WriteLine("server=1")
                                    objWriter.WriteLine("rpcallowip=127.0.0.1")
                                    objWriter.WriteLine("rpcuser=" & rpc_user)
                                    objWriter.WriteLine("rpcpassword=" & rpc_password)
                                    objWriter.WriteLine("rpcport=" & rpc_port)
                                    objWriter.Close()
                                    Exit Do
                                Catch ex As IOException
                                    'Settings.ini is still in use so pause before trying again.
                                    System.Threading.Thread.Sleep(100)
                                End Try
                            Loop
                        End If
                        p2pool_initialized = True
                    End If
                Else
                    p2pool_initialized = True
                End If
                rpc_user = ""
                rpc_password = ""
                rpc_port = ""
                config_server = ""
                rpc_allowip = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Check_RPC_Settings: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Check_RPC_Settings: OK.")
        End Try

    End Sub

    Public Sub Generate_RPC_Settings()

        Try
            'If no RPC settings are detected, generate a random user and password to save to append to the config file.
            Dim rnd As New Random
            Dim str As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()"
            If rpc_user = "" Then
                For value As Integer = 0 To 24
                    rpc_user &= str.Chars(rnd.Next(0, str.Length - 1))
                Next
            End If
            If rpc_password = "" Then
                For value As Integer = 0 To 24
                    rpc_password &= str.Chars(rnd.Next(0, str.Length - 1))
                Next
            End If
            If rpc_port = "" Then
                rpc_port = "5888"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Generate_RPC_Settings: " & ex.Message)
            Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "Main() Generate_RPC_Settings: OK.")
        End Try

    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click

        If Not Updater.IsBusy Then
            AmdMiner = True
            NvidiaMiner = True
            CPUMiner = True
            Updater.RunWorkerAsync()
        End If

    End Sub

    Private Sub Updater_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Updater.DoWork
        'Call url
        Dim url As New System.Uri("http://google.com")
        Dim connection
        Dim cancel = False
        'Request for connection
        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)
        Dim resp As System.Net.WebResponse
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            connection = True
        Catch ex As Exception
            req = Nothing
            connection = False
            MsgBox("No internet connection is present, please connect to the internet to check for updates.")
        End Try
        If connection = True Then
            update_needed = False
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://alwayshashing.com/ocm_versions.txt")
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            'Compares current One-Click Miner version with the latest available.
            newestversion = System.Version.Parse(sr.ReadLine.Replace("miner=", ""))
            updatelink = sr.ReadLine
            If (newestversion > System.Version.Parse(Miner_Version)) Then
                Dim result1 As DialogResult = MsgBox("Update found for One-Click Miner! Click OK to download." & Environment.NewLine & "Please close program before installing.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    Process.Start(updatelink)
                ElseIf result1 = DialogResult.Cancel Then
                    update_complete = True
                End If
            Else
                update_needed = False
            End If
            If update_needed = True Then
                Do Until update_complete = True
                    'Wait until previous update has finished.
                Loop
                update_complete = False
            End If
            'Compares the current version of P2Pool with the latest available.
            newestversion = System.Version.Parse(sr.ReadLine.Replace("p2pool=", ""))
            updatelink = sr.ReadLine
            If p2pool_update = True Then
                Dim result1 As DialogResult = MsgBox("Update found for P2Pool! Click OK to download and install.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    Invoke(New MethodInvoker(AddressOf Download_P2Pool))
                End If
            Else
                If newestversion > System.Version.Parse(P2Pool_Version) And amd_update = False And nvidia_update = False And cpu_update = False And Not (System.Version.Parse(P2Pool_Version) = System.Version.Parse("0.0.0.0")) Then
                    Dim result1 As DialogResult = MsgBox("Update found for P2Pool! Click OK to download and install.", MessageBoxButtons.OKCancel)
                    update_needed = True
                    If result1 = DialogResult.OK Then
                        Invoke(New MethodInvoker(AddressOf Download_P2Pool))
                    ElseIf result1 = DialogResult.Cancel Then
                        update_complete = True
                    End If
                Else
                    update_needed = False
                End If
            End If
            If update_needed = True Then
                Do Until update_complete = True
                    'Wait until previous update has finished.
                Loop
                update_complete = False
            End If
            'Compares the current version of the AMD miner with the latest available.
            newestversion = System.Version.Parse(sr.ReadLine.Replace("amd=", ""))
            updatelink = sr.ReadLine
            If amd_update = True Then
                Dim result1 As DialogResult = MsgBox("Update found for AMD Miner! Click OK to download.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    Invoke(New MethodInvoker(AddressOf Download_Miner))
                ElseIf result1 = DialogResult.Cancel Then
                    cancel = True
                End If
            Else
                If newestversion > System.Version.Parse(AMD_Version) And p2pool_update = False And nvidia_update = False And cpu_update = False And Not (System.Version.Parse(AMD_Version) = System.Version.Parse("0.0.0.0")) Then
                    Dim result1 As DialogResult = MsgBox("Update found for AMD Miner! Click OK to download.", MessageBoxButtons.OKCancel)
                    update_needed = True
                    If result1 = DialogResult.OK Then
                        Invoke(New MethodInvoker(AddressOf Download_Miner))
                    ElseIf result1 = DialogResult.Cancel Then
                        update_complete = True
                        cancel = True
                    End If
                Else
                    update_needed = False
                End If
            End If
            If update_needed = True Then
                Do Until update_complete = True
                    'Wait until previous update has finished.
                Loop
                update_complete = False
            End If
            'Compares the current version of the Nvidia miner with the latest available.
            newestversion = System.Version.Parse(sr.ReadLine.Replace("nvidia=", ""))
            updatelink = sr.ReadLine
            If nvidia_update = True Then
                Dim result1 As DialogResult = MsgBox("Update found for Nvidia Miner! Click OK to download.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    Invoke(New MethodInvoker(AddressOf Download_Miner))
                ElseIf result1 = DialogResult.Cancel Then
                    cancel = True
                End If
            Else
                If newestversion > System.Version.Parse(Nvidia_Version) And p2pool_update = False And amd_update = False And cpu_update = False And Not (System.Version.Parse(Nvidia_Version) = System.Version.Parse("0.0.0.0")) Then
                    Dim result1 As DialogResult = MsgBox("Update found for Nvidia Miner! Click OK to download.", MessageBoxButtons.OKCancel)
                    update_needed = True
                    If result1 = DialogResult.OK Then
                        Invoke(New MethodInvoker(AddressOf Download_Miner))
                    ElseIf result1 = DialogResult.Cancel Then
                        update_complete = True
                        cancel = True
                    End If
                Else
                    update_needed = False
                End If
            End If
            If update_needed = True Then
                Do Until update_complete = True
                    'Wait until previous update has finished.
                Loop
                update_complete = False
            End If
            'Compares the current version of the CPU miner with the latest available.
            newestversion = System.Version.Parse(sr.ReadLine.Replace("cpu=", ""))
            updatelink = sr.ReadLine
            If cpu_update = True Then
                Dim result1 As DialogResult = MsgBox("Update found for CPU Miner! Click OK to download.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    Invoke(New MethodInvoker(AddressOf Download_Miner))
                ElseIf result1 = DialogResult.Cancel Then
                    cancel = True
                End If
            Else
                If newestversion > System.Version.Parse(CPU_Version) And p2pool_update = False And amd_update = False And nvidia_update = False And Not (System.Version.Parse(CPU_Version) = System.Version.Parse("0.0.0.0")) Then
                    Dim result1 As DialogResult = MsgBox("Update found for CPU Miner! Click OK to download.", MessageBoxButtons.OKCancel)
                    update_needed = True
                    If result1 = DialogResult.OK Then
                        Invoke(New MethodInvoker(AddressOf Download_Miner))
                    ElseIf result1 = DialogResult.Cancel Then
                        cancel = True
                    End If
                Else
                    update_needed = False
                End If
            End If
            If update_needed = True Then
                Do Until update_complete = True
                    'Wait until previous update has finished.
                Loop
                update_complete = False
            End If
            If update_needed = False And p2pool_update = False And amd_update = False And nvidia_update = False And cpu_update = False And cancel = False Then
                MsgBox("There are no updates available.")
            End If
            sr.Close()
        End If
        p2pool_update = False
        amd_update = False
        nvidia_update = False
        cpu_update = False
        Invoke(New MethodInvoker(AddressOf Update_Notification))
        Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        Updater.CancelAsync()

    End Sub

    Private Sub Nvidia_CheckedChanged(sender As Object, e As EventArgs) Handles nvidia_check.CheckedChanged

        If nvidia_check.Checked = True Then
            amd_check.Checked = False
            cpu_check.Checked = False
        End If

    End Sub

    Private Sub Amd_CheckedChanged(sender As Object, e As EventArgs) Handles amd_check.CheckedChanged

        If amd_check.Checked = True Then
            nvidia_check.Checked = False
            cpu_check.Checked = False
        End If

    End Sub

    Private Sub Cpu_CheckedChanged(sender As Object, e As EventArgs) Handles cpu_check.CheckedChanged

        If cpu_check.Checked = True Then
            amd_check.Checked = False
            nvidia_check.Checked = False
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        If ComboBox1.SelectedItem = "1" Then
            P2P_Network = 1
        ElseIf ComboBox1.SelectedItem = "2" Then
            P2P_Network = 2
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If nvidia_check.Checked = False Then
            nvidia_check.Checked = True
            DefaultMiner = "nvidia"
        End If
        Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        'Checks if Nvidia miner has already been downloaded and installed
        If System.IO.Directory.Exists(NvidiaFolder) = True Then
            For Each file As String In Directory.GetFiles(NvidiaFolder)
                If file.Contains("ocm_ccminer.exe") Then
                    mining_installed = True
                    Exit For
                Else
                    mining_installed = False
                End If
            Next
        End If
        'Starts mining if miner software is already detected.  If not, downloads miner software.
        If Button4.Text.Contains("Nvidia") Then
            NvidiaMiner = True
            AmdMiner = False
            CPUMiner = False
            If mining_installed = True Then
                mining_initialized = True
                amd_check.Enabled = False
                Button2.Enabled = False
                cpu_check.Enabled = False
                Button5.Enabled = False
                BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
            Else
                If Not Updater.IsBusy Then
                    nvidia_update = True
                    canceldownloadasync = False
                    Updater.RunWorkerAsync()
                End If
            End If
        Else
            NvidiaMiner = False
            mining_initialized = False
            BeginInvoke(New MethodInvoker(AddressOf Stop_Miner))
            BeginInvoke(New MethodInvoker(AddressOf Kill_Miner))
        End If
        mining_installed = False

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        If cpu_check.Checked = False Then
            cpu_check.Checked = True
            DefaultMiner = "cpu"
        End If
        Invoke(New MethodInvoker(AddressOf SaveSettingsIni))
        'Checks if CPU miner has already been downloaded and installed
        If System.IO.Directory.Exists(CPUFolder) = True Then
            For Each file As String In Directory.GetFiles(CPUFolder)
                If file.Contains("ocm_cpuminer.exe") Then
                    mining_installed = True
                    Exit For
                Else
                    mining_installed = False
                End If
            Next
        End If
        'Starts mining if miner software is already detected.  If not, downloads miner software.
        If Button5.Text.Contains("CPU") Then
            CPUMiner = True
            AmdMiner = False
            NvidiaMiner = False
            If mining_installed = True Then
                mining_initialized = True
                amd_check.Enabled = False
                Button2.Enabled = False
                nvidia_check.Enabled = False
                Button4.Enabled = False
                BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
            Else
                If Not Updater.IsBusy Then
                    cpu_update = True
                    canceldownloadasync = False
                    Updater.RunWorkerAsync()
                End If
            End If
        Else
            CPUMiner = False
            mining_initialized = False
            BeginInvoke(New MethodInvoker(AddressOf Stop_Miner))
            BeginInvoke(New MethodInvoker(AddressOf Kill_Miner))
        End If
        mining_installed = False

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

        If Not Updater.IsBusy Then
            Updater.RunWorkerAsync()
        End If

    End Sub

    Private Sub Pool_Address_Text_TextChanged(sender As Object, e As EventArgs) Handles Pool_Address_Text.TextChanged

        'Pool_Address = Pool_Address_Text.Text

    End Sub

    Private Sub Intensity_Text_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Intensity_Text.KeyPress

        If e.KeyChar > "31" And (e.KeyChar < "0" Or e.KeyChar > "9") Then
            e.Handled = True
        End If

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
        NotifyIcon1.Visible = False

    End Sub

End Class



