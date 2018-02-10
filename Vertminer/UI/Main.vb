
Imports System.IO
Imports System.IO.Compression
Imports System.Environment
Imports System.Net
Imports System.Net.Sockets
'Imports Open.Nat
Imports System.Text
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Globalization
Imports VertcoinOneClickMiner.Core
Imports OpenHardwareMonitor.Hardware

Public Class Main

    Dim Pool_Click As New DataGridViewCellEventHandler(AddressOf dataGridView1_CellContentClick)
    Dim JSONConverter As JavaScriptSerializer = New JavaScriptSerializer()
    Private _logger As ILogger

#Region "Complete"
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            settingsfolder = My.Settings.settingsfolder
            If settingsfolder = "" Or System.IO.Directory.Exists(settingsfolder) = False Then
                Dim MainDialogBoxResult As DialogResult = MsgBox("Please select the location that you would like the OCM to store its settings, miner, and p2pool data." & Environment.NewLine & Environment.NewLine & "Click 'Cancel' to use the default location: My Documents\Vertcoin One-Click Miner", MessageBoxButtons.OKCancel)
                If MainDialogBoxResult = DialogResult.OK Then
                    Dim SubDialogBoxResult As Windows.Forms.DialogResult = Select_Data_Dir.ShowDialog()
                    If SubDialogBoxResult = Windows.Forms.DialogResult.OK Then
                        settingsfolder = Select_Data_Dir.SelectedPath
                    ElseIf SubDialogBoxResult = Windows.Forms.DialogResult.Cancel Then
                        settingsfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Vertcoin One-Click Miner"
                    End If
                ElseIf MainDialogBoxResult = DialogResult.Cancel Then
                    settingsfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Vertcoin One-Click Miner"
                    System.IO.Directory.CreateDirectory(settingsfolder)
                End If
                My.Settings.settingsfolder = settingsfolder
                My.Settings.Save()
            End If

            LoadSettings()

            _logger = New FileLogger(syslog)
            If System.IO.File.Exists(settingsfolder & "\Settings.ini") = True Then
                Update_Settings()
            Else
                If System.IO.File.Exists(settingsfile) = False Then
                    SaveSettingsJSON()
                End If
            End If
            If System.IO.File.Exists(syslog) = False Then
                File.Create(syslog).Dispose()
            End If


            LoadSettingsJSON()

            PoolDataCollectionBindingSource.DataSource = PoolDataEx
            'Check for default miner selected.  This is kept separate from autostart to allow user to see checkbox.
            If default_miner = "amd-sgminer" Then
                cbMinerSelect.SelectedItem = "AMD-sgminer"
            ElseIf default_miner = "nvidia-ccminer" Then
                cbMinerSelect.SelectedItem = "NVIDIA-ccminer"
            ElseIf default_miner = "nvidia-vertminer" Then
                cbMinerSelect.SelectedItem = "NVIDIA-vertminer"
            ElseIf default_miner = "cpu-cpuminer" Then
                cbMinerSelect.SelectedItem = "CPU-cpuminer"
            End If

            'Check if p2pool or miner are already running
            Process_Check()
            If p2pool_detected = True Then
                'P2Pool is already running
                cbRunOnLocalNode.Checked = True
            End If

            'Autostart variables
            If autostart_mining = True Then
                If default_miner = "amd-sgminer" Then SetDefaultMinerSettings("\ocm_sgminer.exe", amdminer)
            ElseIf default_miner = "nvidia-ccminer" Then
                SetDefaultMinerSettings("\ocm_ccminer.exe", nvidiaminer)
            ElseIf default_miner = "nvidia-vertminer" Then
                SetDefaultMinerSettings("\ocm_vertminer.exe", nvidiaminer)
            ElseIf default_miner = "cpu-cpuminer" Then
                SetDefaultMinerSettings("\ocm_cpuminer.exe", cpuminer)
            End If

            If autostart_p2pool = True Then
                If System.IO.File.Exists(p2poolfolder & "\ocm_p2pool.exe") = True Then
                    BeginInvoke(New MethodInvoker(AddressOf Start_P2Pool))
                End If
            End If

            Invoke(New MethodInvoker(AddressOf Uptime_Checker_Status_Text))
            Invoke(New MethodInvoker(AddressOf Update_Miner_Text))


            Me.Text = "Vertcoin OCM - BETA V:" & miner_version
            If otherp2pool = True Then
                MsgBox("One-Click Miner has detected other p2pool software running.  Be aware of potential port conflicts.")
            End If
            If otherminer = True Then
                MsgBox("One-Click Miner has detected other miner software running." & Environment.NewLine & "It is not recommended to run multiple miners on the same machine simultaneously.")
            End If

            UpdateStatsInterval.Start()
            Uptime_Timer.Start()
            Idle_Check.Start()
            Form_Load.Start()
        Catch ex As Exception
            ExitRoutine(,, True, True, ex)
        Finally
            _logger.Trace("Loaded: OK, V:" & Application.ProductVersion)
        End Try

    End Sub

    Private Sub SetDefaultMinerSettings(ByVal Miner As String, ByVal Type As Boolean)
        If System.IO.File.Exists(cpuminerfolder & Miner) = True Then
            Type = True
        End If
        mining_initialized = True
        BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
    End Sub

    Private Sub Main_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        _logger.Trace(Environment.NewLine)
        _logger.Trace("Closing: OK")

        NotifyIcon1.Dispose()
        If cbMinerSelect.SelectedItem = "AMD-sgminer" Then
            default_miner = "amd-sgminer"
        ElseIf cbMinerSelect.SelectedItem = "NVIDIA-ccminer" Then
            default_miner = "nvidia-ccminer"
        ElseIf cbMinerSelect.SelectedItem = "NVIDIA-vertminer" Then
            default_miner = "nvidia-vertminer"
        ElseIf cbMinerSelect.SelectedItem = "CPU-cpuminer" Then
            default_miner = "cpu-cpuminer"
        End If
        ExitRoutine(, True,,,)
        Kill_Miner()
        Kill_P2Pool()
        _logger.Trace("================================================================================")
        Application.Exit()
    End Sub

    Private Sub dataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles PoolDataGrid.CellContentClick

        If e.ColumnIndex = 2 Then
            System.Diagnostics.Process.Start(PoolDataGrid(2, e.RowIndex).Value.ToString.Replace("stratum+tcp", "http"))
        End If

    End Sub

#End Region

    Public Sub Process_Check()

        p2pool_detected = IsProcessPresent({"ocm_p2pool"})
        amd_detected = IsProcessPresent({"ocm_sgminer"})
        nvidia_detected = IsProcessPresent({"ocm_ccminer", "ocm_vertminer"})
        cpu_detected = IsProcessPresent({"ocm_cpuminer"})
        otherp2pool = IsProcessPresent({"run_p2pool", "p2pool"}, "ocm")
        otherminer = IsProcessPresent({"vertminer", "sgminer", "cpuminer"}, "ocm")

    End Sub

    Private Function IsProcessPresent(ByVal ProcessName() As String, Optional ByVal DoesNotContain As String = "") As Boolean
        For Each Name As String In ProcessName
            If DoesNotContain = "" Then
                If Process.GetProcessesByName(Name).Length > 0 Then Return True
            Else
                For Each p As Process In Process.GetProcessesByName(Name)
                    If Not p.ProcessName.Contains(DoesNotContain) Then Return True
                Next
            End If
        Next

        Return False
    End Function

    Public Sub UPnP()

        Try

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

            _logger.Trace("SET OK. Ports set: " & mining_port & "," & p2pool_port)

        Catch ex As Exception
            ExitRoutine(, True, True, True, ex)
        End Try

    End Sub

    Public Sub Download_Miner()

        Try
            progress.progress_text.Text = "Downloading Miner"
            progress.Show()
            downloadclient = New WebClient
            AddHandler downloadclient.DownloadProgressChanged, AddressOf Client_ProgressChanged
            AddHandler downloadclient.DownloadFileCompleted, AddressOf Client_MinerDownloadCompleted
            'Compares the current version of the AMD miner with the latest available.
            If amdminer = True Then
                If default_miner = "amd-sgminer" Then
                    If (sgminer_new_version > System.Version.Parse(sgminer_version)) Or mining_installed = False Then
                        If System.IO.Directory.Exists(sgminerfolder) = False Then
                            'If AMD miner doesn't already exist, create folder and download
                            System.IO.Directory.CreateDirectory(sgminerfolder)
                        Else
                            System.IO.Directory.Delete(sgminerfolder, True)
                            System.Threading.Thread.Sleep(100)
                            System.IO.Directory.CreateDirectory(sgminerfolder)
                        End If
                        downloadclient.DownloadFileAsync(New Uri(sgminer_updatelink), sgminerfolder & "\sgminer.zip", True)
                    Else
                        progress.Close()
                    End If
                End If
            End If
            'Compares the current version of the Nvidia miner with the latest available.
            If nvidiaminer = True Then
                If default_miner = "nvidia-ccminer" Then
                    If ccminer_new_version > System.Version.Parse(ccminer_version) Or mining_installed = False Then
                        If System.IO.Directory.Exists(ccminerfolder) = False Then
                            'If Nvidia miner doesn't already exist, create folder and download
                            System.IO.Directory.CreateDirectory(ccminerfolder)
                        Else
                            System.IO.Directory.Delete(ccminerfolder, True)
                            System.Threading.Thread.Sleep(100)
                            System.IO.Directory.CreateDirectory(ccminerfolder)
                        End If
                        downloadclient.DownloadFileAsync(New Uri(ccminer_updatelink), ccminerfolder & "\ccminer.zip", True)
                    Else
                        progress.Close()
                    End If
                ElseIf default_miner = "nvidia-vertminer" Then
                    If vertminer_new_version > System.Version.Parse(vertminer_version) Or mining_installed = False Then
                        If System.IO.Directory.Exists(vertminerfolder) = False Then
                            'If Nvidia miner doesn't already exist, create folder and download
                            System.IO.Directory.CreateDirectory(vertminerfolder)
                        Else
                            System.IO.Directory.Delete(vertminerfolder, True)
                            System.Threading.Thread.Sleep(100)
                            System.IO.Directory.CreateDirectory(vertminerfolder)
                        End If
                        downloadclient.DownloadFileAsync(New Uri(vertminer_updatelink), vertminerfolder & "\vertminer.zip", True)
                    Else
                        progress.Close()
                    End If
                End If
            End If
            'Compares the current version of the CPU miner with the latest available.
            If cpuminer = True Then
                If default_miner = "cpu-cpuminer" Then
                    If cpuminer_new_version > System.Version.Parse(cpuminer_version) Or mining_installed = False Then
                        If System.IO.Directory.Exists(cpuminerfolder) = False Then
                            'If CPU miner doesn't already exist, create folder and download
                            System.IO.Directory.CreateDirectory(cpuminerfolder)
                        Else
                            System.IO.Directory.Delete(cpuminerfolder, True)
                            System.Threading.Thread.Sleep(100)
                            System.IO.Directory.CreateDirectory(cpuminerfolder)
                        End If
                        downloadclient.DownloadFileAsync(New Uri(cpuminer_updatelink), cpuminerfolder & "\cpuminer.zip", True)
                    Else
                        progress.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            ExitRoutine(, True, True, True, ex)
        Finally
            _logger.Trace("Downloaded OK.")
        End Try

    End Sub

    Public Sub Download_P2Pool()

        Try
            progress.progress_text.Text = "Downloading P2Pool"
            progress.Show()
            downloadclient = New WebClient
            AddHandler downloadclient.DownloadProgressChanged, AddressOf Client_ProgressChanged
            AddHandler downloadclient.DownloadFileCompleted, AddressOf Client_P2PoolDownloadCompleted
            If (p2pool_new_version > System.Version.Parse(p2pool_version)) Or p2pool_installed = False Then
                'Create p2pool directory and download/extract p2pool components into directory
                If System.IO.Directory.Exists(p2poolfolder) = False Then
                    System.IO.Directory.CreateDirectory(p2poolfolder)
                End If
                MsgBox("Please note, you must also run a Vertcoin Wallet to use P2Pool locally.")
                downloadclient.DownloadFileAsync(New Uri(p2pool_updatelink), p2poolfolder & "\p2pool.zip", True)
            End If
        Catch ex As Exception
            ExitRoutine("An issue occurred during the download.  Please try again.", True, True,, ex)
        Finally
            _logger.Trace("Downloaded OK.")
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
            downloadclient.DownloadFileAsync(New Uri(link), p2poolfolder & "\interface.zip")
        Catch ex As Exception
            ExitRoutine("An issue occurred during the download.  Please try again.", True, True,, ex)
        Finally
            _logger.Trace("Downloaded OK.")
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
                If amdminer = True Then
                    If default_miner = "amd-sgminer" Then
                        zipPath = settingsfolder & "\amd\sgminer\sgminer.zip"
                        exe = settingsfolder & "\amd\sgminer\ocm_sgminer.exe"
                        miner_config_file = settingsfolder & "\amd\sgminer\config.bat"
                    End If
                ElseIf nvidiaminer = True Then
                    If default_miner = "nvidia-ccminer" Then
                        zipPath = settingsfolder & "\nvidia\ccminer\ccminer.zip"
                        exe = settingsfolder & "\nvidia\ccminer\ocm_ccminer.exe"
                        dll = settingsfolder & "\nvidia\ccminer\msvcr120.dll"
                        miner_config_file = settingsfolder & "\nvidia\ccminer\config.bat"
                    ElseIf default_miner = "nvidia-vertminer" Then
                        zipPath = settingsfolder & "\nvidia\vertminer\vertminer.zip"
                        exe = settingsfolder & "\nvidia\vertminer\ocm_vertminer.exe"
                        dll = settingsfolder & "\nvidia\vertminer\msvcr120.dll"
                        miner_config_file = settingsfolder & "\nvidia\vertminer\config.bat"
                    End If
                ElseIf cpuminer = True Then
                    zipPath = settingsfolder & "\cpu\cpuminer\cpuminer.zip"
                    exe = settingsfolder & "\cpu\cpuminer\ocm_cpuminer.exe"
                    miner_config_file = settingsfolder & "\cpu\cpuminer\config.bat"
                End If
                Using archive As ZipArchive = ZipFile.OpenRead(zipPath)
                    'If platform = True Then '64-bit
                    For Each entry As ZipArchiveEntry In archive.Entries
                        If nvidiaminer = True Then 'Nvidia
                            If (entry.FullName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) Or entry.FullName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) Then
                                If entry.FullName.EndsWith(".exe") Then
                                    extractpath = exe
                                ElseIf entry.FullName.EndsWith(".dll") Then
                                    extractpath = dll
                                End If
                                entry.ExtractToFile(extractpath, True)
                            End If
                        ElseIf amdminer = True Then 'AMD
                            'sgminer
                            If (entry.FullName.Contains("kernel") And entry.FullName.EndsWith(".cl", StringComparison.OrdinalIgnoreCase)) Then
                                If System.IO.Directory.Exists(amdfolder & "\sgminer\kernel\") = False Then
                                    System.IO.Directory.CreateDirectory(amdfolder & "\sgminer\kernel\")
                                End If
                                entry.ExtractToFile(amdfolder & "\sgminer\kernel\" & entry.Name, True)
                            Else
                                If entry.FullName.EndsWith(".exe") Then
                                    entry.ExtractToFile(exe, True)
                                ElseIf entry.FullName.EndsWith(".dll") Then
                                    entry.ExtractToFile(amdfolder & "\sgminer\" & entry.Name, True)
                                End If
                            End If
                        ElseIf cpuminer = True Then 'CPU
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
                If amdminer = True Then
                    If default_miner = "amd-sgminer" Then
                        sgminer_version = System.Convert.ToString(sgminer_new_version)
                    End If
                ElseIf nvidiaminer = True Then
                    If default_miner = "nvidia-ccminer" Then
                        ccminer_version = System.Convert.ToString(ccminer_new_version)
                    ElseIf default_miner = "nvidia-vertminer" Then
                        vertminer_version = System.Convert.ToString(vertminer_new_version)
                    End If
                ElseIf cpuminer = True Then
                    cpuminer_version = System.Convert.ToString(cpuminer_new_version)
                End If
                amdminer = False
                nvidiaminer = False
                cpuminer = False
                update_needed = False
            End If
        Catch ex As Exception
            ExitRoutine("An issue occurred during the download.  Please try again.", True, True,, ex)


        Finally
            _logger.Trace("MinerDownloadCompleted: OK.")
        End Try

    End Sub

    Private Sub Client_P2PoolDownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

        Try
            If canceldownloadasync = False Then
                'Download proper miner and extract into respective directories
                zipPath = p2poolfolder & "\p2pool.zip"
                exe = p2poolfolder & "\ocm_p2pool.exe"
                p2pool_config_file = p2poolfolder & "\start_p2pool.bat"
                p2pool_config = "ocm_p2pool.exe" & Environment.NewLine & "exit /B"
                ZipFile.ExtractToDirectory(zipPath, p2poolfolder)
                Dim folders() As String = IO.Directory.GetDirectories(p2poolfolder)
                For Each folder As String In folders
                    Dim files() As String = IO.Directory.GetFiles(folder)
                    For Each file As String In files
                        If file.Contains("run_p2pool") Then
                            My.Computer.FileSystem.MoveFile(file, p2poolfolder & "\" & "ocm_p2pool.exe", True)
                        Else
                            My.Computer.FileSystem.MoveFile(file, p2poolfolder & "\" & System.IO.Path.GetFileName(file), True)
                        End If
                    Next
                    Dim subfolders() As String = IO.Directory.GetDirectories(folder)
                    For Each subfolder As String In subfolders
                        Dim split As String() = subfolder.Split("\")
                        Dim newfolder As String = split(split.Length - 1)
                        My.Computer.FileSystem.MoveDirectory(subfolder, p2poolfolder & "\" & newfolder, True)
                    Next
                    System.IO.Directory.Delete(folder)
                Next
                If System.IO.File.Exists(p2poolfolder & "\Start P2Pool Network 1.bat") = True Then
                    System.IO.File.Delete(p2poolfolder & "\Start P2Pool Network 1.bat")
                End If
                If System.IO.File.Exists(p2poolfolder & "\Start P2Pool Network 2.bat") = True Then
                    System.IO.File.Delete(p2poolfolder & "\Start P2Pool Network 2.bat")
                End If
                'Create default p2pool config
                If System.IO.File.Exists(p2pool_config_file) = False Then
                    Dim objWriter As New System.IO.StreamWriter(p2pool_config_file)
                    objWriter.WriteLine(p2pool_config)
                    objWriter.Close()
                End If
                Invoke(New MethodInvoker(AddressOf Download_P2PoolInterface))
                p2pool_version = System.Convert.ToString(p2pool_new_version)
            End If
        Catch ex As Exception
            ExitRoutine("An issue occurred during the download.  Please try again.", True, True,, ex)
        Finally
            _logger.Trace("P2PoolDownloadCompleted: OK.")
        End Try

    End Sub

    Private Sub Client_P2PoolInterfaceDownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

        Try
            'Download proper miner and extract into respective directories
            zipPath = p2poolfolder & "\interface.zip"
            ZipFile.ExtractToDirectory(zipPath, p2poolfolder)
            Dim folders() As String = IO.Directory.GetDirectories(p2poolfolder)
            If System.IO.Directory.Exists(p2poolfolder & "\web-static") = True Then
                System.IO.Directory.Delete(p2poolfolder & "\web-static", True)
            End If
            My.Computer.FileSystem.RenameDirectory(p2poolfolder & "\p2pool-ui-punchy-master", "web-static")
            Dim result1 As DialogResult = MsgBox("P2Pool has been installed.", MessageBoxButtons.OK)
            If result1 = DialogResult.OK Then
                progress.Close()
                Invoke(New MethodInvoker(AddressOf Check_RPC_Settings))
                update_complete = True
                BeginInvoke(New MethodInvoker(AddressOf Start_P2Pool))
            End If
        Catch ex As Exception
            ExitRoutine("An issue occurred during the download.  Please try again.", True, True,, ex)
        Finally
            _logger.Trace("P2PoolInterfaceDownloadCompleted: OK.")
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

    Public Sub Uptime_Checker_Status_Text()

        'Miner Info
        If amd_detected = True Or nvidia_detected = True Or cpu_detected = True Then
            If miner_hashrate > 0 Then
                tbMinerStatus.Text = "Running"
            Else
                tbMinerStatus.Text = "Waiting for share"
            End If
            bStartButton.Text = "Stop"
        Else
            tbMinerStatus.Text = "Offline"
            bStartButton.Text = "Start"
            tbMinerHashrate.Text = "0 kh/s"
        End If
        'P2Pool Info
        If p2pool_detected = True Then
            If p2pool_loaded = True Then
                tbLocalPoolStatus.Text = "Running: Network " & p2pool_network
            Else
                tbLocalPoolStatus.Text = "Loading"
            End If
            cbRunOnLocalNode.Checked = True
        Else
            tbLocalPoolStatus.Text = "Offline"
            cbRunOnLocalNode.Checked = False
        End If

    End Sub

    Public Sub Update_Miner_Text()

        'Miner Hashrate
        If api_connected = True Then
            If miner_hashrate < 1000 Then
                miner_hashrate = Math.Round(miner_hashrate, 2)
                tbMinerHashrate.Text = miner_hashrate.ToString & " kh/s"
            ElseIf miner_hashrate >= 1000 And miner_hashrate < 1000000 Then
                miner_hashrate = Math.Round((miner_hashrate / 1000), 2)
                tbMinerHashrate.Text = miner_hashrate.ToString & " Mh/s"
            ElseIf miner_hashrate >= 1000000 And miner_hashrate < 1000000000 Then
                miner_hashrate = Math.Round((miner_hashrate / 1000000), 2)
                tbMinerHashrate.Text = miner_hashrate.ToString & " Gh/s"
            ElseIf miner_hashrate >= 1000000000 And miner_hashrate < 1000000000000 Then
                miner_hashrate = Math.Round((miner_hashrate / 1000000000), 2)
                tbMinerHashrate.Text = miner_hashrate.ToString & " Th/s"
            End If
        Else
            tbMinerHashrate.Text = "0 kh/s"
        End If

    End Sub


    Public Sub Update_Settings()

        Try
            System.IO.File.Delete(settingsfolder & "\settings.ini")
        Catch ex As IOException
            ExitRoutine(, True, True, True, ex)
        Finally
            _logger.Trace("UpdateSettings: OK.")
        End Try

    End Sub

    Public Sub LoadSettingsJSON()

        Try
            Dim settingsJSON As Settings_JSON = New Settings_JSON()
            Dim settings_string As String = File.ReadAllText(settingsfile)

            If Not String.IsNullOrEmpty(settings_string) Then
                settingsJSON = JsonConvert.DeserializeObject(Of Settings_JSON)(settings_string)
            End If

            appdata = settingsJSON.appdata
            start_minimized = settingsJSON.start_minimized
            start_with_windows = settingsJSON.start_with_windows
            autostart_p2pool = settingsJSON.autostart_p2pool
            autostart_mining = settingsJSON.autostart_mining
            mine_when_idle = settingsJSON.mine_when_idle
            keep_miner_alive = settingsJSON.keep_miner_alive
            keep_p2pool_alive = settingsJSON.keep_p2pool_alive
            use_upnp = settingsJSON.use_upnp
            show_cli = settingsJSON.show_cli
            p2pool_network = settingsJSON.p2pool_network
            p2pool_node_fee = settingsJSON.p2pool_node_fee
            p2pool_donation = settingsJSON.p2pool_donation
            max_connections = settingsJSON.max_connections
            p2pool_port = settingsJSON.p2pool_port
            mining_port = settingsJSON.mining_port
            mining_intensity = settingsJSON.mining_intensity
            p2pool_fee_address = settingsJSON.p2pool_fee_address
            p2pool_version = settingsJSON.p2pool_version
            sgminer_version = settingsJSON.sgminer_version
            ccminer_version = settingsJSON.ccminer_version
            vertminer_version = settingsJSON.vertminer_version
            cpuminer_version = settingsJSON.cpuminer_version
            default_miner = settingsJSON.default_miner
            devices = settingsJSON.devices

            PoolDataEx.Clear()
            Dim count = settingsJSON.pools.Count
            If Not count = 0 Then
                For x = 0 To count - 1
                    If Not settingsJSON.pools(x).Pool = "" And Not settingsJSON.pools(x).Worker = "" And Not settingsJSON.pools(x).Password = "" Then
                        Dim jsonstring = JSONConverter.Serialize(settingsJSON.pools(x))
                        Dim poolJSON = JSONConverter.Deserialize(Of PoolData)(jsonstring)
                        PoolDataEx.Add(poolJSON)
                    End If
                Next
            End If
            If String.IsNullOrEmpty(appdata) Then
                appdata = ""
            End If
            If String.IsNullOrEmpty(sgminer_version) Then
                sgminer_version = "0.0.0.0"
            End If
            If String.IsNullOrEmpty(ccminer_version) Then
                ccminer_version = "0.0.0.0"
            End If
            If String.IsNullOrEmpty(vertminer_version) Then
                vertminer_version = "0.0.0.0"
            End If
            If String.IsNullOrEmpty(cpuminer_version) Then
                cpuminer_version = "0.0.0.0"
            End If
            If String.IsNullOrEmpty(default_miner) Then
                default_miner = "0.0.0.0"
            End If
        Catch ex As IOException
            _logger.LogError(ex)
        Finally
            _logger.Trace("LoadSettings: OK.")
        End Try

    End Sub

    Public Sub SaveSettingsJSON()

        Try
            Dim newjson As Settings_JSON = New Settings_JSON()
            newjson.appdata = appdata
            newjson.start_minimized = start_minimized
            newjson.start_with_windows = start_with_windows
            newjson.autostart_p2pool = autostart_p2pool
            newjson.autostart_mining = autostart_mining
            newjson.mine_when_idle = mine_when_idle
            newjson.keep_miner_alive = keep_miner_alive
            newjson.keep_p2pool_alive = keep_p2pool_alive
            newjson.use_upnp = use_upnp
            newjson.show_cli = show_cli
            newjson.p2pool_network = p2pool_network
            newjson.p2pool_node_fee = p2pool_node_fee
            newjson.p2pool_donation = p2pool_donation
            newjson.max_connections = max_connections
            newjson.p2pool_port = p2pool_port
            newjson.mining_port = mining_port
            newjson.mining_intensity = mining_intensity
            newjson.p2pool_fee_address = p2pool_fee_address
            newjson.p2pool_version = p2pool_version
            newjson.sgminer_version = sgminer_version
            newjson.ccminer_version = ccminer_version
            newjson.vertminer_version = vertminer_version
            newjson.cpuminer_version = cpuminer_version
            newjson.default_miner = default_miner
            newjson.devices = devices
            newjson.pools.Clear()
            For Each p In PoolDataEx
                newjson.pools.Add(p)
            Next

            Dim jsonstring = JSONConverter.Serialize(newjson)
            If Not String.IsNullOrEmpty(jsonstring) Then
                Dim jsonFormatted As String = JValue.Parse(jsonstring).ToString(Formatting.Indented)
                File.WriteAllText(settingsfile, jsonFormatted)
            End If
        Catch ex As IOException
            _logger.LogError(ex)
        Finally
            _logger.Trace("SaveSettings: OK.")
        End Try

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles cbRunOnLocalNode.CheckedChanged

        If p2pool_network = "1" Then
            If mining_port = "9181" Or mining_port = "" Then
                mining_port = "9171"
            End If
        ElseIf p2pool_network = "2" Then
            If mining_port = "9171" Or mining_port = "" Then
                mining_port = "9181"
            End If
        End If

        Dim pool_list As String = ""
        PoolDataEx.ToList.ForEach(Sub(x) pool_list &= x.Pool & ",")
        pool_list.TrimEnd(",")

        If cbRunOnLocalNode.Checked = True Then
            If Not pool_list.Contains("stratum+tcp://localhost:") And Not pool_list.Contains("stratum+tcp://127.0.0.1:") Then
                Dim dialog = New AddPool(_logger)
                dialog.Show()
                dialog.Pool_Address.Text = "stratum+tcp://localhost:" & mining_port
            End If
        End If
        'See if P2Pool has already been downloaded/installed
        If System.IO.Directory.Exists(p2poolfolder) = True Then
            For Each file As String In Directory.GetFiles(p2poolfolder)
                If file.Contains("ocm_p2pool.exe") And Not (System.Version.Parse(p2pool_version) = System.Version.Parse("0.0.0.0")) Then
                    p2pool_installed = True
                    Exit For
                Else
                    p2pool_installed = False
                End If
            Next
        End If
        'Starts p2pool if p2pool software is already detected.  If not, downloads p2pool software.
        If cbRunOnLocalNode.Checked = True Then
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
            BeginInvoke(New MethodInvoker(AddressOf Kill_P2Pool))
        End If
        p2pool_installed = False

    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click

        Dim dialog = New settings(_logger)
        dialog.Show()

    End Sub

    Public Sub Update_P2Pool_Config()

        Try
            exe = settingsfolder & "\p2pool\ocm_p2pool.exe"
            p2pool_config_file = settingsfolder & "\p2pool\start_p2pool.bat"
            Dim network As String = ""
            If p2pool_network = "0" Or p2pool_network = "1" Then
                network = " --net vertcoin"
                'Allows any custom port other than default ports.
                If p2pool_port = "9347" Then
                    p2pool_port = "9346"
                End If
                If mining_port = "9181" Then
                    mining_port = "9171"
                End If
            ElseIf p2pool_network = "2" Then
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
                p2pool_config = "ocm_p2pool.exe" & network & " --give-author " & p2pool_donation & " --fee " & p2pool_node_fee & " --address " & p2pool_fee_address & " --max-conns " & max_connections & " --worker-port " & mining_port & " --p2pool-port " & p2pool_port & " --bitcoind-config-path """ & appdata & "\vertcoin.conf""" & " --address-share-rate 30" & Environment.NewLine & "exit /B"
            Else
                p2pool_config = "ocm_p2pool.exe" & network & " --give-author " & p2pool_donation & " --fee " & p2pool_node_fee & " --address " & p2pool_fee_address & " --max-conns " & max_connections & " --worker-port " & mining_port & " --p2pool-port " & p2pool_port & " --address-share-rate 30" & Environment.NewLine & "exit /B"
            End If
            If System.IO.File.Exists(p2pool_config_file) = True Then
                command = File.ReadAllText(p2pool_config_file)
            End If
            If Not p2pool_config = command Then
                command = p2pool_config
                File.WriteAllText(p2pool_config_file, command)
            End If
        Catch ex As Exception
            ExitRoutine(, True, True, True, ex)
        Finally
            _logger.Trace("Update_P2Pool_Config: OK.")
        End Try

    End Sub

    Public Sub Update_Miner_Config()

        Try

            Dim newjson As New Miner_Settings_JSON
            Dim jsonstring As String = ""
            If amdminer = True Then
                ' newjson = New AMD_Miner_Settings_JSON()
                If default_miner = "amd-sgminer" Then
                    minersettingsfile = sgminerfolder & "\sgminer.conf"
                End If
                newjson.pools.Add(PoolDataEx.First)

                newjson.algorithm = "Lyra2REv2"
                newjson.intensity = mining_intensity
                newjson.device = devices
                jsonstring = JSONConverter.Serialize(newjson)
                jsonstring = jsonstring.Insert(jsonstring.Length - 1, ",""no-extranonce""" & ": " & "true")
            ElseIf nvidiaminer = True Then
                '   newjson = New NVIDIA_Miner_Settings_JSON()
                If default_miner = "nvidia-ccminer" Then
                    minersettingsfile = ccminerfolder & "\ccminer.conf"
                ElseIf default_miner = "nvidia-vertminer" Then
                    minersettingsfile = vertminerfolder & "\vertminer.conf"
                End If
                newjson.algorithm = "lyra2v2"
                newjson.intensity = mining_intensity
                newjson.device = devices

                For Each p In PoolDataEx
                    newjson.pools.Add(p)
                Next


                jsonstring = JSONConverter.Serialize(newjson)
            ElseIf cpuminer = True Then
                '  newjson = New CPU_Miner_Settings_JSON()
                If default_miner = "cpu-cpuminer" Then
                    minersettingsfile = cpuminerfolder & "\cpuminer-conf.json"
                End If

                newjson.algorithm = "lyra2rev2"
                newjson.intensity = mining_intensity
                jsonstring = JSONConverter.Serialize(newjson)
            End If
            If Not String.IsNullOrEmpty(jsonstring) Then
                Dim jsonFormatted As String = JValue.Parse(jsonstring).ToString(Formatting.Indented)
                File.WriteAllText(minersettingsfile, jsonFormatted)
            End If
        Catch ex As Exception
            ExitRoutine(, True, True, True, ex)
        Finally
            _logger.Trace("Update_Miner_Config: OK.")
        End Try

    End Sub

    Public Sub Start_Miner()

        Try
            Update_Miner_Config()
            'End If
            If amd_detected = True Or nvidia_detected = True Or cpu_detected = True Then
                ' Process is running
            Else
                ' Process is not running
                'JSON Configuration
                psi = New ProcessStartInfo
                'minerprocess = New Process
                If amdminer = True Then
                    If default_miner = "amd-sgminer" Then
                        'miner_config = amdfolder & "\ocm_sgminer.exe"
                        psi = New ProcessStartInfo("cmd")
                        psi.Arguments = ("/K cd /d" & sgminerfolder & " & " & "setx GPU_MAX_ALLOC_PERCENT 100" & " & " & "setx GPU_SINGLE_ALLOC_PERCENT 100" & " & " & "ocm_sgminer.exe --api-listen --config " & "sgminer.conf" & " & " & " exit /B")
                    End If
                ElseIf nvidiaminer = True Then
                    If default_miner = "nvidia-ccminer" Then
                        'miner_config = nvidiafolder & "\ocm_vertminer.exe"
                        psi = New ProcessStartInfo(ccminerfolder & "\ocm_ccminer.exe")
                    ElseIf default_miner = "nvidia-vertminer" Then
                        'miner_config = nvidiafolder & "\ocm_vertminer.exe"
                        psi = New ProcessStartInfo(vertminerfolder & "\ocm_vertminer.exe")
                    End If
                ElseIf cpuminer = True Then
                    'miner_config = cpufolder & "\ocm_cpuminer.exe"
                    psi = New ProcessStartInfo(cpuminerfolder & "\ocm_cpuminer.exe")
                End If
                mining_running = True
                'Dim psi As New ProcessStartInfo(miner_config)
                If show_cli = True Then
                    psi.CreateNoWindow = False
                    psi.UseShellExecute = True
                Else
                    psi.CreateNoWindow = True
                    psi.UseShellExecute = False
                End If
                Dim minerprocess As Process = Process.Start(psi)
                miner_process = minerprocess.Id
                'Process.Start(psi)
            End If
        Catch ex As Exception
            _logger.LogError(ex)
        Finally
            _logger.Trace("Start_Miner: OK.")
        End Try

    End Sub

    Public Sub Stop_Miner()

        bStartButton.Text = "Start"
        miner_hashrate = 0

    End Sub

    Public Sub Stop_P2Pool()

        cbRunOnLocalNode.Checked = False
        tbLocalPoolStatus.Text = "Offline"

    End Sub

    Public Sub Kill_Miner()

        Try
            mining_running = False
            For Each p As Process In System.Diagnostics.Process.GetProcesses
                If p.ProcessName.Contains("ocm_sgminer") Then
                    p.Kill()
                End If
            Next
            Dim processes As Process = Process.GetProcessById(miner_process)
            processes.Kill()
            miner_hashrate = 0
        Catch ex As Exception
            ExitRoutine(, True, True,, ex)
        Finally
            _logger.Trace("Kill_Miner: OK.")
        End Try

    End Sub

    Public Sub Start_P2Pool()

        Try
            Invoke(New MethodInvoker(AddressOf Update_P2Pool_Config))
            Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
            If use_upnp = True Then
                Invoke(New MethodInvoker(AddressOf UPnP))
            End If
            If p2pool_detected = True Then
                ' Process is running
                'MsgBox("P2Pool is already running.")
            Else
                ' Process is not running
                Dim psi As New ProcessStartInfo("cmd")
                If show_cli = True Then
                    psi.CreateNoWindow = False
                    psi.UseShellExecute = True
                Else
                    psi.CreateNoWindow = True
                    psi.UseShellExecute = False
                End If
                command = File.ReadAllText(p2pool_config_file)
                command = command.Replace(Environment.NewLine, " & ")
                psi.Arguments = ("/K cd /d" & p2poolfolder & " & " & command)
                Dim p2poolprocess As Process = Process.Start(psi)
                p2pool_process = p2poolprocess.Id
                'Process.Start(psi)
            End If
        Catch ex As Exception
            ExitRoutine(, True, True,, ex)
        Finally
            _logger.Trace("Start_P2Pool: OK.")
        End Try

    End Sub

    Public Sub Kill_P2Pool()

        Try
            p2pool_running = False
            For Each p As Process In System.Diagnostics.Process.GetProcesses
                If p.ProcessName.Contains("ocm_p2pool") Then
                    p.Kill()
                End If
            Next
        Catch ex As Exception
            ExitRoutine(, True, True,, ex)
        Finally
            _logger.Trace("Kill_P2Pool: OK.")
        End Try

    End Sub

    Private Sub UpdateInterval_Tick(sender As Object, e As EventArgs) Handles UpdateStatsInterval.Tick

        'Updates stats every X seconds
        If Not (UpdateStats.IsBusy) Then
            UpdateStats.RunWorkerAsync()
        End If
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
            If tempnewestversion > System.Version.Parse(miner_version) Then
                update_needed = True
            End If
            'Compares the current version of P2Pool with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("p2pool=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(p2pool_version)) And Not (System.Version.Parse(p2pool_version) = System.Version.Parse("0.0.0.0")) Then
                update_needed = True
            End If
            'sgminer - Compares the current version of the AMD miner with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("amd-sgminer=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(sgminer_version)) And Not (System.Version.Parse(sgminer_version) = System.Version.Parse("0.0.0.0")) And (System.IO.File.Exists(sgminerfolder & "\ocm_sgminer.exe") = True) Then
                update_needed = True
            End If
            'ccminer - Compares the current version of the Nvidiaminer with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("nvidia-ccminer=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(ccminer_version)) And Not (System.Version.Parse(ccminer_version) = System.Version.Parse("0.0.0.0")) And (System.IO.File.Exists(ccminerfolder & "\ocm_ccminer.exe") = True) Then
                update_needed = True
            End If
            'vertminer - Compares the current version of the Nvidia miner with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("nvidia-vertminer=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(vertminer_version)) And Not (System.Version.Parse(vertminer_version) = System.Version.Parse("0.0.0.0")) And (System.IO.File.Exists(vertminerfolder & "\ocm_vertminer.exe") = True) Then
                update_needed = True
            End If
            'cpuminer - Compares the current version of the CPU miner with the latest available.
            tempnewestversion = System.Version.Parse(sr.ReadLine.Replace("cpu-cpuminer=", ""))
            templink = sr.ReadLine
            If (tempnewestversion > System.Version.Parse(cpuminer_version)) And Not (System.Version.Parse(cpuminer_version) = System.Version.Parse("0.0.0.0")) And (System.IO.File.Exists(cpuminerfolder & "\ocm_cpuminer.exe") = True) Then
                update_needed = True
            End If
            Update_Notification()
            response.Dispose()
            sr.Dispose()
        End If
        Auto_Update_Notify.CancelAsync()

    End Sub

    Public Sub Update_Notification()
        Invoke(Sub()
                   If update_needed = True Then
                       UpdateNotificationText.Enabled = True
                       UpdateNotificationText.Visible = True
                   Else
                       UpdateNotificationText.Enabled = False
                       UpdateNotificationText.Visible = False
                   End If
                   update_needed = False
               End Sub)
    End Sub

    Private Sub UpdateStats_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles UpdateStats.DoWork

        Try
            'Miner API
            System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture
            If amd_detected = True Or nvidia_detected = True Or cpu_detected = True Then
                Dim tcpClient As New System.Net.Sockets.TcpClient()
                If amd_detected = True Then
                    tcpClient.Connect("127.0.0.1", 4028)
                ElseIf nvidia_detected = True Then
                    tcpClient.Connect("127.0.0.1", 4068)
                ElseIf cpu_detected = True Then
                    tcpClient.Connect("127.0.0.1", 4048)
                End If
                Dim RawData() As String
                Dim networkStream As NetworkStream = tcpClient.GetStream()
                If networkStream.CanWrite And networkStream.CanRead Then
                    api_connected = True
                    Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes("summary")
                    networkStream.Write(sendBytes, 0, sendBytes.Length)
                    Dim bytes(tcpClient.ReceiveBufferSize) As Byte
                    networkStream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
                    Dim returndata As String = Encoding.ASCII.GetString(bytes)
                    If amd_detected = True Then
                        RawData = returndata.Split(",")
                    Else
                        RawData = returndata.Split(";")
                    End If
                    For Each line As String In RawData
                        If line.Contains("KHS=") And Not line.Contains("NETKHS=") Then
                            miner_hashrate = Convert.ToDecimal((line.Replace("KHS=", "")))
                        ElseIf line.Contains("KHS av=") And Not line.Contains("NETKHS=") Then
                            miner_hashrate = Convert.ToDecimal((line.Replace("KHS av=", "")))
                        End If
                    Next
                Else
                    If Not networkStream.CanRead Or Not networkStream.CanWrite Then
                        MsgBox("Miner API is refusing connection.")
                        tcpClient.Close()
                        networkStream.Close()
                    End If
                End If
                tcpClient.Close()
                networkStream.Close()
                BeginInvoke(New MethodInvoker(AddressOf Update_Miner_Text))
            End If
            'P2Pool API
            If p2pool_detected = True Then
                Dim url As New System.Uri("http://localhost:" & mining_port & "/rate")
                Dim cancel = False
                'Request for connection
                Dim req As System.Net.WebRequest
                req = System.Net.WebRequest.Create(url)
                Dim resp As System.Net.WebResponse
                Try
                    resp = req.GetResponse()
                    resp.Close()
                    req = Nothing
                    p2pool_loaded = True
                Catch ex As Exception
                    req = Nothing
                    p2pool_loaded = False
                End Try
            End If
            'BeginInvoke(New MethodInvoker(AddressOf Uptime_Checker_Status_Text))
        Catch ex As Exception
            SaveSettingsJSON()
        Finally
        End Try

    End Sub

    Private Sub FileDirectoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileDirectoryToolStripMenuItem.Click

        Try
            If default_miner = "amd-sgminer" Then
                miner_config_file = sgminerfolder & "\sgminer.conf"
            ElseIf default_miner = "nvidia-ccminer" Then
                miner_config_file = ccminerfolder & "\ccminer.conf"
            ElseIf default_miner = "nvidia-vertminer" Then
                miner_config_file = vertminerfolder & "\vertminer.conf"
            ElseIf default_miner = "cpu-cpuminer" Then
                miner_config_file = cpuminerfolder & "\cpuminer-conf.json"
            End If
            If System.IO.File.Exists(miner_config_file) = True Then
                Process.Start("notepad.exe", miner_config_file)
            Else
                MsgBox("No miner config file found.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
        Finally
            _logger.Trace("MinerConfigToolStripMenuItem(), Load Miner Config: OK")
        End Try

    End Sub

    Private Sub SystemLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SystemLogToolStripMenuItem.Click

        Try
            Process.Start("notepad.exe", syslog)
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
        Finally
            _logger.Trace("SystemLogToolStripMenuItem(), Load Miner Log: OK")
        End Try

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

        Dim dialog = New about(_logger)
        dialog.Show()

    End Sub

    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click

        Try
            Dim url As String = "https://discordapp.com/invite/Yb6EHNy"
            Process.Start(url)
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
        Finally
            _logger.Trace("Contact(), Load Browser: OK")
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
            If default_miner = "amd-sgminer" Then
                System.Threading.Thread.Sleep(5000) 'Temporary fix until amd mining is controlled via PID
            End If
            Invoke(New MethodInvoker(AddressOf Process_Check))
            If (amd_detected = True Or nvidia_detected = True Or cpu_detected = True) And p2pool_detected = True Then
                'P2Pool and Miner are running, do nothing
            Else
                If mining_running = True Then 'Miner initialized
                    If amd_detected = False And nvidia_detected = False And cpu_detected = False Then
                        If keep_miner_alive = True Then
                            BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                        Else
                            BeginInvoke(New MethodInvoker(AddressOf Stop_Miner))
                        End If
                    End If
                Else 'Miner not initialized
                End If
                If p2pool_running = True Then 'P2Pool initialized
                    If p2pool_detected = False Then
                        If keep_p2pool_alive = True Then
                            BeginInvoke(New MethodInvoker(AddressOf Start_P2Pool))
                        End If
                    End If
                Else 'P2Pool not initialized
                End If
            End If
            BeginInvoke(New MethodInvoker(AddressOf Uptime_Checker_Status_Text))
            Uptime_Checker.CancelAsync()
        Catch ex As Exception
        End Try

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
                Dim result1 As DialogResult = MsgBox("The default Vertcoin Core Wallet data directory was not found.  Would you like to select where it can be found?", MessageBoxButtons.OKCancel)
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
                'Checks for existing vertcoin.conf file and pulls any available settings.
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
                        MsgBox("If your Vertcoin Core wallet is currently running, please close it and click OK.")
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
                                        If (Line.Contains("server=") And Line.Length >= 8) Then
                                            config_server = "server=1"
                                            objWriter.WriteLine(config_server) 'ALWAYS Changes server= to server=1 regardless of setting so p2pool can connect
                                            do_once_config_server = True
                                        End If
                                    End If
                                    If do_once_rpcallow = False Then
                                        If (Line.Contains("rpcallowip=") And Line.Length >= 18) Then
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
                                        End If
                                    End If
                                    If do_once_rpcpassword = False Then
                                        If Line.Contains("rpcpassword=") And Line.Length >= 13 Then
                                            objWriter.WriteLine(Line)
                                            do_once_rpcpassword = True
                                        End If
                                    End If
                                    If do_once_rpcport = False Then
                                        If Line.Contains("rpcport=") And Line.Length >= 9 Then
                                            objWriter.WriteLine(Line)
                                            do_once_rpcport = True
                                        End If
                                    End If
                                    If Not Line.Contains("rpcallowip=") And Not Line.Contains("server=") And Not Line.Contains("rpcuser=") And Not Line.Contains("rpcpassword=") And Not Line.Contains("rpcport=") Then
                                        objWriter.WriteLine(Line)
                                    End If
                                Next
                                If do_once_config_server = False Then
                                    objWriter.WriteLine("server=1")
                                End If
                                If do_once_rpcallow = False Then
                                    objWriter.WriteLine("rpcallowip=127.0.0.1")
                                End If
                                If do_once_rpcuser = False Then
                                    objWriter.WriteLine("rpcuser=" & rpc_user)
                                End If
                                If do_once_rpcpassword = False Then
                                    objWriter.WriteLine("rpcpassword=" & rpc_password)
                                End If
                                If do_once_rpcport = False Then
                                    objWriter.WriteLine("rpcport=" & rpc_port)
                                End If
                                My.Computer.FileSystem.MoveFile(appdata & "\vertcoin.conf", appdata & "\vertcoin_old.conf", True)
                                objWriter.Close()
                                My.Computer.FileSystem.MoveFile(appdata & "\vertcoin_buffer.conf", appdata & "\vertcoin.conf", True)
                                Exit Do
                            Catch ex As IOException
                                'vertcoin.conf is still in use so pause before trying again.
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
                                    'vertcoin.conf is still in use so pause before trying again.
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
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
        Finally
            _logger.Trace("Main() Check_RPC_Settings: OK.")
        End Try

    End Sub

    Public Sub Generate_RPC_Settings()

        Try
            'If no RPC settings are detected, generate a random user and password to save to append to the config file.
            Dim rnd As New Random
            Dim str As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
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
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
        Finally
            _logger.Trace("Generate_RPC_Settings: OK.")
        End Try

    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click

        If Not Updater.IsBusy Then
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
            'Read versions and update links
            ocm_new_version = System.Version.Parse(sr.ReadLine.Replace("miner=", ""))
            ocm_updatelink = sr.ReadLine
            p2pool_new_version = System.Version.Parse(sr.ReadLine.Replace("p2pool=", ""))
            p2pool_updatelink = sr.ReadLine
            sgminer_new_version = System.Version.Parse(sr.ReadLine.Replace("amd-sgminer=", ""))
            sgminer_updatelink = sr.ReadLine
            ccminer_new_version = System.Version.Parse(sr.ReadLine.Replace("nvidia-ccminer=", ""))
            ccminer_updatelink = sr.ReadLine
            vertminer_new_version = System.Version.Parse(sr.ReadLine.Replace("nvidia-vertminer=", ""))
            vertminer_updatelink = sr.ReadLine
            cpuminer_new_version = System.Version.Parse(sr.ReadLine.Replace("cpu-cpuminer=", ""))
            cpuminer_updatelink = sr.ReadLine
            sr.Close()
            'Compares current One-Click Miner version with the latest available.
            If (ocm_new_version > System.Version.Parse(miner_version)) Then
                Dim result1 As DialogResult = MsgBox("Update found for One-Click Miner! Click OK to download." & Environment.NewLine & "Please close program before installing.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    Process.Start(ocm_updatelink)
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
            If p2pool_update = True Then
                Dim result1 As DialogResult = MsgBox("Update found for P2Pool! Click OK to download and install.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    Invoke(New MethodInvoker(AddressOf Download_P2Pool))
                End If
            Else
                If p2pool_new_version > System.Version.Parse(p2pool_version) And amd_update = False And nvidia_update = False And cpu_update = False And Not (System.Version.Parse(p2pool_version) = System.Version.Parse("0.0.0.0")) Then
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
                Do Until update_complete
                    Threading.Thread.Sleep(10)
                Loop
                update_complete = False
            End If


            If amd_update = True Then
                Dim result1 As DialogResult = MsgBox("Update found for AMD-sgminer! Click OK to download.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    ' Invoke(New MethodInvoker(AddressOf Download_Miner))
                    Download_Miner()

                ElseIf result1 = DialogResult.Cancel Then
                    cancel = True
                End If

            Else
                If sgminer_new_version > System.Version.Parse(sgminer_version) And p2pool_update = False And nvidia_update = False And cpu_update = False And Not (System.Version.Parse(sgminer_version) = System.Version.Parse("0.0.0.0")) Then
                    Dim result1 As DialogResult = MsgBox("Update found for AMD-sgminer! Click OK to download.", MessageBoxButtons.OKCancel)
                    update_needed = True
                    If result1 = DialogResult.OK Then
                        amdminer = True
                        Download_Miner()
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
            'Nvidia - Compares the current version of ccminer and vertminer with the latest available.
            If nvidia_update = True Then
                If default_miner = "nvidia-ccminer" Then
                    Dim result1 As DialogResult = MsgBox("Update found for Nvidia-ccminer! Click OK to download.", MessageBoxButtons.OKCancel)
                    If result1 = DialogResult.OK Then
                        update_needed = True
                        Download_Miner()
                    ElseIf result1 = DialogResult.Cancel Then
                        cancel = True
                    End If
                ElseIf default_miner = "nvidia-vertminer" Then
                    Dim result1 As DialogResult = MsgBox("Update found for Nvidia-vertminer! Click OK to download.", MessageBoxButtons.OKCancel)
                    If result1 = DialogResult.OK Then
                        update_needed = True
                        Invoke(New MethodInvoker(AddressOf Download_Miner))
                    ElseIf result1 = DialogResult.Cancel Then
                        cancel = True
                    End If
                End If
            Else
                If default_miner = "nvidia-ccminer" Then
                    If ccminer_new_version > System.Version.Parse(ccminer_version) And p2pool_update = False And amd_update = False And cpu_update = False And Not (System.Version.Parse(ccminer_version) = System.Version.Parse("0.0.0.0")) Then
                        Dim result1 As DialogResult = MsgBox("Update found for Nvidia-ccminer! Click OK to download.", MessageBoxButtons.OKCancel)
                        update_needed = True
                        If result1 = DialogResult.OK Then
                            nvidiaminer = True
                            Invoke(New MethodInvoker(AddressOf Download_Miner))
                        ElseIf result1 = DialogResult.Cancel Then
                            update_complete = True
                            cancel = True
                        End If
                    Else
                        update_needed = False
                    End If
                ElseIf default_miner = "nvidia-vertminer" Then
                    If vertminer_new_version > System.Version.Parse(vertminer_version) And p2pool_update = False And amd_update = False And cpu_update = False And Not (System.Version.Parse(vertminer_version) = System.Version.Parse("0.0.0.0")) Then
                        Dim result1 As DialogResult = MsgBox("Update found for Nvidia-vertminer! Click OK to download.", MessageBoxButtons.OKCancel)
                        update_needed = True
                        If result1 = DialogResult.OK Then
                            nvidiaminer = True
                            Invoke(New MethodInvoker(AddressOf Download_Miner))
                        ElseIf result1 = DialogResult.Cancel Then
                            update_complete = True
                            cancel = True
                        End If
                    Else
                        update_needed = False
                    End If
                End If
            End If
            If update_needed = True Then
                Do Until update_complete = True
                    'Wait until previous update has finished.
                Loop
                update_complete = False
            End If
            'cpuminer - Compares the current version of the CPU miner with the latest available.
            If cpu_update = True Then
                Dim result1 As DialogResult = MsgBox("Update found for CPU-cpuminer! Click OK to download.", MessageBoxButtons.OKCancel)
                If result1 = DialogResult.OK Then
                    update_needed = True
                    Invoke(New MethodInvoker(AddressOf Download_Miner))
                ElseIf result1 = DialogResult.Cancel Then
                    cancel = True
                End If
            Else
                If cpuminer_new_version > System.Version.Parse(cpuminer_version) And p2pool_update = False And amd_update = False And nvidia_update = False And Not (System.Version.Parse(cpuminer_version) = System.Version.Parse("0.0.0.0")) Then
                    Dim result1 As DialogResult = MsgBox("Update found for CPU-cpuminer! Click OK to download.", MessageBoxButtons.OKCancel)
                    update_needed = True
                    If result1 = DialogResult.OK Then
                        cpuminer = True
                        Download_Miner()
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
        End If
        p2pool_update = False
        amd_update = False
        nvidia_update = False
        cpu_update = False
        Update_Notification()
        SaveSettingsJSON()
        Updater.CancelAsync()

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles UpdateNotificationText.Click

        If Not Updater.IsBusy Then
            Updater.RunWorkerAsync()
        End If

    End Sub

    Private Sub Intensity_Text_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar > "31" And (e.KeyChar < "0" Or e.KeyChar > "9") Then
            e.Handled = True
        End If

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
        NotifyIcon1.Visible = False

    End Sub

    Private Sub P2PoolWebInterfaceToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Try
            Dim url As String = PoolDataEx(0).Pool.Replace("stratum+tcp", "http")
            Process.Start(url)
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
        Finally
            _logger.Trace("LoadP2PoolInterface(), Load Browser: OK")
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles bFindP2PoolNodes.Click

        Dim dialog = New P2Pool(_logger)
        dialog.Show()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMinerSelect.SelectedIndexChanged

        If cbMinerSelect.SelectedItem = "AMD-sgminer" Then
            default_miner = "amd-sgminer"
            VertMinerFeeLabel.Visible = False
        ElseIf cbMinerSelect.SelectedItem = "NVIDIA-ccminer" Then
            default_miner = "nvidia-ccminer"
            VertMinerFeeLabel.Visible = False
        ElseIf cbMinerSelect.SelectedItem = "NVIDIA-vertminer" Then
            default_miner = "nvidia-vertminer"
            VertMinerFeeLabel.Visible = True
        ElseIf cbMinerSelect.SelectedItem = "CPU-cpuminer" Then
            default_miner = "cpu-cpuminer"
            VertMinerFeeLabel.Visible = False
        End If

    End Sub

    'Checks if miner has already been downloaded and installed
    Private Function IsMinerInstalled(folder As String, exe As String, version As Object) As Boolean

        If System.IO.Directory.Exists(folder) Then
            For Each file As String In Directory.GetFiles(folder)
                If file.Contains(exe) And Not (System.Version.Parse(version) = System.Version.Parse("0.0.0.0")) Then
                    Return True
                End If
            Next
        End If
        Return False

    End Function

    Private Sub SetMinerBooleans(default_miner As String)

        cpuminer = (default_miner = "cpu-cpuminer")
        amdminer = (default_miner = "amd-sgminer")
        'nvidiaminer = (default_miner = "nvidia-ccminer") Or (default_miner = "nvidia-vertminer")
        If default_miner = "nvidia-ccminer" Or default_miner = "nvidia-vertminer" Then
            nvidiaminer = True
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles bStartButton.Click

        Dim checkcount = 0
        For Each row As DataGridViewRow In PoolDataGrid.Rows
            Dim chk As DataGridViewCheckBoxCell = row.Cells(PoolDataGrid.Columns(0).Name)
            If chk.Value = True Then
                checkcount += 1
            End If
        Next
        Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
        'Starts mining if miner software is already detected.  If not, downloads miner software.
        If bStartButton.Text = "Start" Then
            bStartButton.Text = "Stop"
            If checkcount > 0 Then 'If at least one pool is selected
                If default_miner = "amd-sgminer" Then
                    mining_installed = IsMinerInstalled(sgminerfolder, "ocm_sgminer.exe", sgminer_version)
                    If Not mining_installed Then
                        If Not Updater.IsBusy Then
                            SetMinerBooleans(default_miner)
                            amd_update = True
                            canceldownloadasync = False
                            Updater.RunWorkerAsync()
                        End If
                    Else
                        SetMinerBooleans(default_miner)
                        mining_initialized = True
                        BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                    End If
                ElseIf default_miner = "nvidia-ccminer" Then
                    mining_installed = IsMinerInstalled(ccminerfolder, "ocm_ccminer.exe", ccminer_version)
                    If Not mining_installed Then
                        If Not Updater.IsBusy Then
                            SetMinerBooleans(default_miner)
                            nvidia_update = True
                            canceldownloadasync = False
                            Updater.RunWorkerAsync()
                        End If
                    Else
                        SetMinerBooleans(default_miner)
                        mining_initialized = True
                        BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                    End If
                ElseIf default_miner = "nvidia-vertminer" Then
                    mining_installed = IsMinerInstalled(vertminerfolder, "ocm_vertminer.exe", vertminer_version)
                    If Not mining_installed Then
                        If Not Updater.IsBusy Then
                            SetMinerBooleans(default_miner)
                            nvidia_update = True
                            canceldownloadasync = False
                            Updater.RunWorkerAsync()
                        End If
                    Else
                        SetMinerBooleans(default_miner)
                        mining_initialized = True
                        BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                    End If
                ElseIf default_miner = "cpu-cpuminer" Then
                    mining_installed = IsMinerInstalled(cpuminerfolder, "ocm_cpuminer.exe", cpuminer_version)
                    If Not mining_installed Then
                        If Not Updater.IsBusy Then
                            SetMinerBooleans(default_miner)
                            cpu_update = True
                            canceldownloadasync = False
                            Updater.RunWorkerAsync()
                        End If
                    Else
                        SetMinerBooleans(default_miner)
                        mining_initialized = True
                        BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                    End If
                End If
                mining_installed = False
            Else
                MsgBox("Please select at least one pool before starting miner.")
            End If
        ElseIf bStartButton.Text = "Stop" Then
            bStartButton.Text = "Start"
            amdminer = False
            nvidiaminer = False
            cpuminer = False
            mining_initialized = False
            BeginInvoke(New MethodInvoker(AddressOf Kill_Miner))
        End If

    End Sub

    Private Sub Clock_Tick(sender As Object, e As EventArgs) Handles Clock.Tick

        Dim time As DateTime = DateTime.Now
        timenow = time.ToString("r", culture)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles bRemovePool.Click

        If PoolDataGrid.CurrentRow IsNot Nothing Then
            PoolDataEx.Remove(PoolDataGrid.CurrentRow.DataBoundItem)
        End If

        SaveSettingsJSON()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles bAddPool.Click

        Dim dialog = New AddPool(_logger)
        dialog.Show()

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles cbSelectAllPools.CheckedChanged

        If cbSelectAllPools.Checked = True Then
            For Each row As DataGridViewRow In PoolDataGrid.Rows
                Dim chk As DataGridViewCheckBoxCell = row.Cells(PoolDataGrid.Columns(0).Name)
                If chk.Value = False Then chk.Value = True
            Next
        Else
            For Each row As DataGridViewRow In PoolDataGrid.Rows
                Dim chk As DataGridViewCheckBoxCell = row.Cells(PoolDataGrid.Columns(0).Name)
                If chk.Value = True Then chk.Value = False
            Next
        End If

    End Sub

    Private Sub P2PoolConfigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P2PoolConfigToolStripMenuItem.Click

        Try
            Process.Start("notepad.exe", p2pool_config_file)
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf SaveSettingsJSON))
        Finally
            _logger.Trace("P2PoolConfigToolStripMenuItem(), Load P2Pool Config: OK")
        End Try

    End Sub

    Private Sub Idle_Check_Tick(sender As Object, e As EventArgs) Handles Idle_Check.Tick

        If mine_when_idle = True Then
            If Not (Idle_Worker.IsBusy) Then
                Idle_Worker.RunWorkerAsync()
                Idle_Timer.Start()
            End If
        End If

    End Sub

    Private Sub Idle_Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Idle_Worker.DoWork

        Dim cp As New Computer
        cp.Open()
        cp.GPUEnabled = True
        cp.CPUEnabled = True
        Dim Info As String = ""
        For i As Integer = 0 To cp.Hardware.Count() - 1
            If cp.Hardware(i).HardwareType = HardwareType.GpuAti Then
                If default_miner = "amd-sgminer" And amdminer = False Then
                    If cp.Hardware(i).Sensors(4).Value < 10 And idle_ticker >= 20 Then
                        SetMinerBooleans(default_miner)
                        mining_initialized = True
                        BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                    ElseIf cp.Hardware(i).Sensors(4).Value >= 10 Then
                        idle_ticker = 0
                    End If
                ElseIf amdminer = True Then
                    idle_ticker = 0
                End If
            ElseIf cp.Hardware(i).HardwareType = HardwareType.GpuNvidia Then
                If (default_miner = "nvidia-ccminer" Or default_miner = "nvidia-vertminer") And nvidiaminer = False Then
                    If cp.Hardware(i).Sensors(4).Value < 10 And idle_ticker >= 20 Then
                        SetMinerBooleans(default_miner)
                        mining_initialized = True
                        BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                    ElseIf cp.Hardware(i).Sensors(4).Value >= 10 Then
                        idle_ticker = 0
                    End If
                ElseIf nvidiaminer = True Then
                    idle_ticker = 0
                End If
            ElseIf cp.Hardware(i).HardwareType = HardwareType.CPU Then
                If default_miner = "cpu-cpuminer" And cpuminer = False Then
                    If cp.Hardware(i).Sensors(4).Value < 20 And idle_ticker >= 20 Then
                        SetMinerBooleans(default_miner)
                        mining_initialized = True
                        BeginInvoke(New MethodInvoker(AddressOf Start_Miner))
                    ElseIf cp.Hardware(i).Sensors(4).Value >= 20 Then
                        idle_ticker = 0
                    End If
                ElseIf cpuminer = True Then
                    idle_ticker = 0
                End If
            End If
        Next

    End Sub

    Private Sub Idle_Timer_Tick_1(sender As Object, e As EventArgs) Handles Idle_Timer.Tick

        idle_ticker += 1

    End Sub

    Private Sub Form_Load_Tick(sender As Object, e As EventArgs) Handles Form_Load.Tick

        'Window state on start
        If start_minimized = True Then
            Me.WindowState = FormWindowState.Minimized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
        Form_Load.Stop()

    End Sub

    Private Sub ExitRoutine(Optional ByVal Message As String = "", Optional ByVal SaveJson As Boolean = False, Optional ByVal LogError As Boolean = False, Optional ByVal bMsgBox As Boolean = False, Optional ByVal ex As Exception = Nothing)

        If Message <> "" Then
            MsgBox(Message)
        End If

        If bMsgBox AndAlso ex IsNot Nothing Then
            MsgBox(ex.Message)
        End If

        If (LogError AndAlso ex IsNot Nothing) Then
            _logger.LogError(ex)
        End If

        If SaveJson Then
            SaveSettingsJSON()
        End If

    End Sub

End Class
