Imports System.IO
Imports System.Text
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Threading
Imports VertcoinOneClickMiner.Core

Public Class P2Pool
    Private ReadOnly _logger As ILogger
    Dim JSONConverter As JavaScriptSerializer = New JavaScriptSerializer()
    Dim scanner1 As Node_JSON = New Node_JSON()
    Dim scanner2 As Node_JSON = New Node_JSON()
    Dim network1_counter As Integer
    Dim network2_counter As Integer
    Dim network1_count As Integer
    Dim network2_count As Integer
    Dim scanner1worker As Thread
    Dim scanner2worker As Thread
    Dim stopthread As New Boolean

    Public Sub New(logger As ILogger)
        InitializeComponent()
        _logger = logger
    End Sub

    Private Sub P2Pool_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Invoke(New MethodInvoker(AddressOf Style))
            If networkdata IsNot Nothing AndAlso networkdata.Tables.Count >= 1 Then
                Dim chk As New DataGridViewCheckBoxColumn()
                DataGridView1.Columns.Add(chk)
                chk.HeaderText = "Select"
                chk.Name = "Select"
                DataGridView1.DataSource = networkdata.Tables(0)
            End If
            If networkdata IsNot Nothing AndAlso networkdata.Tables.Count >= 2 Then
                Dim chk As New DataGridViewCheckBoxColumn()
                DataGridView2.Columns.Add(chk)
                chk.HeaderText = "Select"
                chk.Name = "Select"
                DataGridView2.DataSource = networkdata.Tables(1)
            End If
            If System.IO.Directory.Exists(scannerfolder) = False Then
                System.IO.Directory.CreateDirectory(scannerfolder)
            End If
            If scanner1worker Is Nothing AndAlso Not networkdata Is Nothing Then
                scanner1worker = New Thread(AddressOf Scanner1Thread)
                scanner1worker.Start()
            End If
            If scanner2worker Is Nothing AndAlso Not networkdata Is Nothing Then
                scanner2worker = New Thread(AddressOf Scanner2Thread)
                scanner2worker.Start()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
        Finally
            _logger.Trace("Loaded: OK")
        End Try

    End Sub

    Private Sub P2Pool_Closing(sender As Object, e As EventArgs) Handles MyBase.Closed

        Try
            stopthread = True
            JSONConverter = Nothing
            scanner1 = Nothing
            scanner2 = Nothing
            scanner1worker = Nothing
            scanner2worker = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
            Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
        Finally
            _logger.Trace("Loaded: OK")
        End Try

    End Sub

    'SAVE THIS FUNCTION IN CASE NEEDED
    'Private Function SaveScanner(ByVal dgv As DataGridView, network As Integer) As DataSet

    '    Dim ds As New DataSet
    '    Try
    '        ' Add Table
    '        ds.Tables.Add("Network" & network)
    '        ' Add Columns
    '        Dim col As DataColumn
    '        For Each dgvCol As DataGridViewColumn In dgv.Columns
    '            col = New DataColumn(dgvCol.Name)
    '            ds.Tables("Network" & network).Columns.Add(col)
    '        Next
    '        'Add Rows from the datagridview
    '        Dim row As DataRow
    '        Dim colcount As Integer = dgv.Columns.Count - 1
    '        For i As Integer = 0 To dgv.Rows.Count - 1
    '            row = ds.Tables("Network" & network).Rows.Add
    '            For Each column As DataGridViewColumn In dgv.Columns
    '                row.Item(column.Index) = dgv.Rows.Item(i).Cells(column.Index).Value
    '            Next
    '        Next
    '        Return ds
    '    Catch ex As Exception
    '        MsgBox("CRITICAL ERROR : Exception caught while converting dataGridView to DataSet. " & Chr(10) & ex.Message)
    '        Return Nothing
    '    End Try

    'End Function

    Public Sub Get_P2Pool_API()

        JSONConverter.MaxJsonLength = jsonmaxlength 'set to 200000000 characters
        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
        End If
        If DataGridView2.Rows.Count > 0 Then
            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()
        End If
        Dim connection = False
        Dim host = ""
        'Network 1
        For Each url In api_network1_hosts
            'Request for connection
            Dim req As System.Net.WebRequest
            req = System.Net.WebRequest.Create(url)
            Dim resp As System.Net.WebResponse
            Try
                resp = req.GetResponse()
                resp.Close()
                req = Nothing
                connection = True
                host = url
                Exit For
            Catch ex As Exception
                req = Nothing
                connection = False
            End Try
        Next
        If connection = False Then
            MsgBox("Unable to connect to P2Pool Network 1 Scanner API at this time.")
        Else
            Try
                Dim p2pool_api As Object = Nothing
                Dim newjson_sb As StringBuilder = New StringBuilder("")
                Dim jsonformatted As String = ""
                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(host)
                Dim response As System.Net.HttpWebResponse = request.GetResponse()
                Using sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                    p2pool_api = p2pool_api & sr.ReadLine
                End Using
                If Not String.IsNullOrEmpty(p2pool_api) Then
                    jsonformatted = JValue.Parse(p2pool_api).ToString(Formatting.Indented)
                    File.WriteAllText(scannerfolder & "\network1.json", jsonformatted)
                    jsonformatted = ""
                End If
                System.Threading.Thread.Sleep(200)
                p2pool_api = File.ReadAllLines(scannerfolder & "\network1.json")
                For Each line As String In p2pool_api
                    If line.Contains(": {") Then
                        If IsIpAddressValid(line.Replace(": {", "").Replace(ControlChars.Quote, "").Trim()) Then
                            line = "{"
                        End If
                    End If
                    newjson_sb.Append(line)
                Next
                newjson_sb.Insert(1, Environment.NewLine & """nodes"":[")
                newjson_sb.Insert(newjson_sb.Length - 1, "]")
                newjson_sb.Replace(" ", "")
                newjson_sb.Replace(vbCr, "")
                newjson_sb.Replace(vbLf, "")
                Dim newjson As String = newjson_sb.ToString().Trim
                If Not String.IsNullOrEmpty(newjson) Then
                    jsonformatted = JValue.Parse(newjson).ToString(Formatting.Indented)
                    File.WriteAllText(scannerfolder & "\network1.json", jsonformatted)
                End If
                scanner1 = JSONConverter.Deserialize(Of Node_JSON)(jsonformatted)
                Dim count = scanner1.nodes.Count - 1
                Dim chk As New DataGridViewCheckBoxColumn()
                DataGridView1.Columns.Add(chk)
                chk.HeaderText = "Select"
                chk.Name = "Select"
                chk.ReadOnly = False
                networkdata = New DataSet
                networkdata.Tables.Add("Network1")
                Dim columns As String() = {"IP", "Version", "Fee (%)", "Uptime(days)", "Located", "Latency(ms)"}
                Dim column As DataColumn
                For Each col As String In columns
                    column = New DataColumn(col)
                    networkdata.Tables("Network1").Columns.Add(column)
                Next
                For x As Integer = 0 To count - 1
                    Dim uptime As Decimal = (scanner1.nodes(x).stats.uptime / 60 / 60 / 24)
                    uptime = Math.Round(uptime, 1)
                    networkdata.Tables("Network1").Rows.Add((scanner1.nodes(x).ip & ":9171"), scanner1.nodes(x).stats.version, scanner1.nodes(x).fee, uptime, scanner1.nodes(x).geo.country, Nothing)
                Next
                DataGridView1.DataSource = networkdata.Tables(0)
            Catch ex As Exception
                _logger.LogError(ex)
                Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
            Finally
                _logger.Trace("Scan Completed: OK")
            End Try
        End If
        'Network 2
        For Each url In api_network2_hosts
            'Request for connection
            Dim req As System.Net.WebRequest
            req = System.Net.WebRequest.Create(url)
            Dim resp As System.Net.WebResponse
            Try
                resp = req.GetResponse()
                resp.Close()
                req = Nothing
                connection = True
                host = url
                Exit For
            Catch ex As Exception
                req = Nothing
                connection = False
            End Try
        Next
        If connection = False Then
            MsgBox("Unable to connect to P2Pool Network 2 Scanner API at this time.")
        Else
            Try
                Dim p2pool_api As Object = Nothing
                Dim newjson_sb As StringBuilder = New StringBuilder("")
                Dim jsonformatted As String = ""
                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(host)
                Dim response As System.Net.HttpWebResponse = request.GetResponse()
                Using sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                    p2pool_api = p2pool_api & sr.ReadLine
                End Using
                If Not String.IsNullOrEmpty(p2pool_api) Then
                    jsonformatted = JValue.Parse(p2pool_api).ToString(Formatting.Indented)
                    File.WriteAllText(scannerfolder & "\network2.json", jsonformatted)
                    jsonformatted = ""
                End If
                System.Threading.Thread.Sleep(200)
                p2pool_api = File.ReadAllLines(scannerfolder & "\network2.json")
                For Each line As String In p2pool_api
                    If line.Contains(": {") Then
                        If IsIpAddressValid(line.Replace(": {", "").Replace(ControlChars.Quote, "").Trim()) Then
                            line = "{"
                        End If
                    End If
                    newjson_sb.Append(line)
                Next
                newjson_sb.Insert(1, Environment.NewLine & """nodes"":[")
                newjson_sb.Insert(newjson_sb.Length - 1, "]")
                newjson_sb.Replace(" ", "")
                newjson_sb.Replace(vbCr, "")
                newjson_sb.Replace(vbLf, "")
                Dim newjson As String = newjson_sb.ToString().Trim
                If Not String.IsNullOrEmpty(newjson) Then
                    jsonformatted = JValue.Parse(newjson).ToString(Formatting.Indented)
                    File.WriteAllText(scannerfolder & "\network2.json", jsonformatted)
                End If
                scanner2 = New Node_JSON()
                scanner2 = JSONConverter.Deserialize(Of Node_JSON)(jsonformatted)
                Dim count = scanner2.nodes.Count - 1
                Dim chk As New DataGridViewCheckBoxColumn()
                DataGridView2.Columns.Add(chk)
                chk.HeaderText = "Select"
                chk.Name = "Select"
                chk.ReadOnly = False
                networkdata.Tables.Add("Network2")
                Dim columns As String() = {"IP", "Version", "Fee (%)", "Uptime(days)", "Located", "Latency(ms)"}
                Dim column As DataColumn
                For Each col As String In columns
                    column = New DataColumn(col)
                    networkdata.Tables("Network2").Columns.Add(column)
                Next
                For x As Integer = 0 To count
                    Dim uptime As Decimal = (scanner2.nodes(x).stats.uptime / 60 / 60 / 24)
                    uptime = Math.Round(uptime, 1)
                    networkdata.Tables("Network2").Rows.Add((scanner2.nodes(x).ip & ":9181"), scanner2.nodes(x).stats.version, scanner2.nodes(x).fee, uptime, scanner2.nodes(x).geo.country, Nothing)
                Next
                DataGridView2.DataSource = networkdata.Tables(1)
            Catch ex As Exception
                _logger.LogError(ex)
                Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
            Finally
                _logger.Trace("Scan Completed: OK")
            End Try
        End If
        BeginInvoke(New MethodInvoker(AddressOf Loading_Stop))

    End Sub

    Private Sub MyTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

        Dim indexOfSelectedTab As Integer = TabControl1.SelectedIndex
        Dim selectedTab As System.Windows.Forms.TabPage = TabControl1.SelectedTab
        If indexOfSelectedTab = 0 Then
            Label4.Text = "Network 1 is recommended for miners with 100MH and above."
        ElseIf indexOfSelectedTab = 1 Then
            Label4.Text = "Network 2 is recommended for miners with 100MH and below."
        End If

    End Sub

    Public Function IsIpAddressValid(Address As String) As Boolean

        Return System.Net.IPAddress.TryParse(Address, Nothing)

    End Function

    Private Sub AddSelectedPoolsButton_Click(sender As Object, e As EventArgs) Handles AddSelectedPoolsButton.Click

        Dim checkcount = 0
        Dim checkcount2 = 0
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim chk As DataGridViewCheckBoxCell = row.Cells(DataGridView1.Columns(0).Name)
            If chk.Value IsNot Nothing AndAlso chk.Value = True Then
                checkcount += 1
            End If
        Next
        For Each row As DataGridViewRow In DataGridView2.Rows
            Dim chk As DataGridViewCheckBoxCell = row.Cells(DataGridView2.Columns(0).Name)
            If chk.Value IsNot Nothing AndAlso chk.Value = True Then
                checkcount2 += 1
            End If
        Next
        checkcount = checkcount + checkcount2
        Dim pool_list = String.Join(",", pools.ToArray())
        If VertcoinAddressUtility.IsWalletAddressValid(Wallet_Address.Text) And checkcount > 0 Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim chk As DataGridViewCheckBoxCell = row.Cells(DataGridView1.Columns(0).Name)
                If chk.Value IsNot Nothing AndAlso chk.Value = True Then
                    If Not pool_list.Contains(DataGridView1.Rows(chk.RowIndex).Cells(1).Value) Then
                        descriptions.Add("")
                        pools.Add(DataGridView1.Rows(chk.RowIndex).Cells(1).Value)
                        workers.Add(Wallet_Address.Text)
                        passwords.Add("x")
                    End If
                End If
            Next
            For Each row As DataGridViewRow In DataGridView2.Rows
                Dim chk As DataGridViewCheckBoxCell = row.Cells(DataGridView2.Columns(0).Name)
                If chk.Value IsNot Nothing AndAlso chk.Value = True Then
                    If Not pool_list.Contains(DataGridView2.Rows(chk.RowIndex).Cells(1).Value) Then
                        descriptions.Add("")
                        pools.Add(DataGridView2.Rows(chk.RowIndex).Cells(1).Value)
                        workers.Add(Wallet_Address.Text)
                        passwords.Add("x")
                    End If
                End If
            Next
            Invoke(New MethodInvoker(AddressOf Main.Update_Pool_Info))
            Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
            MsgBox("Pool(s) added successfully.")
            Me.Close()
        ElseIf checkcount = 0 Then
            MsgBox("Please select a pool to add.")
        Else
            MsgBox("Please enter a Valid Wallet Address before adding pools.")
        End If

    End Sub

    Public Sub Loading_Start()

        Label1.Visible = True
        Label2.Visible = True
        Button2.Text = "Loading"
        Button2.Enabled = False

    End Sub

    Public Sub Loading_Stop()

        System.Threading.Thread.Sleep(3000)
        If scanner1worker Is Nothing AndAlso Not networkdata.Tables("Network1") Is Nothing Then
            scanner1worker = New Thread(AddressOf Scanner1Thread)
            scanner1worker.Start()
        End If
        If scanner2worker Is Nothing AndAlso Not networkdata.Tables("Network2") Is Nothing Then
            scanner2worker = New Thread(AddressOf Scanner2Thread)
            scanner2worker.Start()
        End If
        Label1.Visible = False
        Label2.Visible = False
        Button2.Text = "Scan"
        Button2.Enabled = True

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        stopthread = True
        If Not scanner1worker Is Nothing Then
            Do Until (scanner1worker.ThreadState = ThreadState.Stopped)
            Loop
            scanner1worker = Nothing
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
        End If
        If Not scanner2worker Is Nothing Then
            Do Until (scanner2worker.ThreadState = ThreadState.Stopped)
            Loop
            scanner2worker = Nothing
            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()
        End If
        networkdata = Nothing
        stopthread = False
        BeginInvoke(New MethodInvoker(AddressOf Loading_Start))
        BeginInvoke(New MethodInvoker(AddressOf Get_P2Pool_API))

    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click

        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click

        Cursor.Current = Cursors.WaitCursor
        stopthread = True
        If Not scanner1worker Is Nothing Then
            Do Until (scanner1worker.ThreadState = ThreadState.Stopped)
            Loop
            scanner1worker = Nothing
        End If
        If Not scanner2worker Is Nothing Then
            Do Until (scanner2worker.ThreadState = ThreadState.Stopped)
            Loop
            scanner2worker = Nothing
        End If
        stopthread = False
        Cursor.Current = Cursors.Arrow
        Me.Close()

    End Sub

    Dim drag As Boolean = False
    Dim mousex As Integer, mousey As Integer

    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown, Panel2.MouseDown, PictureBox11.MouseDown, Label6.MouseDown

        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove, Panel2.MouseMove, PictureBox11.MouseMove, Label6.MouseMove

        If drag Then
            Me.Left = Cursor.Position.X - mousex
            Me.Top = Cursor.Position.Y - mousey
        End If

    End Sub

    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp, Panel2.MouseUp, PictureBox11.MouseUp, Label6.MouseUp

        drag = False

    End Sub

    Public Sub Style()

        Panel1.BackColor = Color.FromArgb(27, 92, 46)
        AddSelectedPoolsButton.BackColor = Color.FromArgb(27, 92, 46)
        Button2.BackColor = Color.FromArgb(27, 92, 46)
        Panel2.BackColor = Color.FromArgb(41, 54, 61)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

    End Sub

    Private Sub DataGridView_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError, DataGridView2.DataError

        'do nothing to ignore error

    End Sub

    Sub Scanner1Thread()
        'Network 1
        If DataGridView1.Rows.Count >= 1 AndAlso networkdata.Tables("Network1").Rows.Count >= 1 Then
            For x As Integer = 0 To networkdata.Tables("Network1").Rows.Count - 1
                Try
                    Dim clientSocket As New Net.Sockets.TcpClient()
                    Dim node = networkdata.Tables("Network1").Rows(x).Item(0).ToString
                    node = node.Substring(0, node.IndexOf(":"))
                    Dim result = clientSocket.BeginConnect(node, "9171", Nothing, Nothing)
                    Dim stopWatch As New Stopwatch()
                    Dim StopWatchTimeMs As Int32
                    If stopthread = False And scanner1worker.ThreadState = ThreadState.Running Then
                        Try
                            stopWatch.Start()
                            IIf(result IsNot Nothing, Nothing, result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(999)))
                            clientSocket.Close()
                            stopWatch.Stop()
                            StopWatchTimeMs = stopWatch.ElapsedMilliseconds
                            Dim rowcount = DataGridView1.Rows.Count
                            networkdata.Tables("Network1").Rows(x).Item(5) = StopWatchTimeMs
                        Catch ex As IOException
                            networkdata.Tables("Network1").Rows(x).Item(5) = 999
                        End Try
                    Else
                        Exit Sub
                    End If
                Catch ex As Exception
                End Try
            Next
        End If

    End Sub

    Sub Scanner2Thread()

        'Network 2
        If DataGridView2.Rows.Count >= 1 AndAlso networkdata.Tables("Network2").Rows.Count >= 1 Then
            For x As Integer = 0 To networkdata.Tables("Network2").Rows.Count - 1
                Try
                    Dim clientSocket As New Net.Sockets.TcpClient()
                    Dim node = networkdata.Tables("Network2").Rows(x).Item(0).ToString
                    node = node.Substring(0, node.IndexOf(":"))
                    Dim result = clientSocket.BeginConnect(node, "9181", Nothing, Nothing)
                    Dim stopWatch As New Stopwatch()
                    Dim StopWatchTimeMs As Int32
                    If stopthread = False And scanner2worker.ThreadState = ThreadState.Running Then
                        Try
                            stopWatch.Start()
                            IIf(result IsNot Nothing, Nothing, result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(999)))
                            clientSocket.Close()
                            stopWatch.Stop()
                            StopWatchTimeMs = stopWatch.ElapsedMilliseconds
                            networkdata.Tables("Network2").Rows(x).Item(5) = StopWatchTimeMs
                        Catch ex As Exception
                            networkdata.Tables("Network2").Rows(x).Item(5) = 999
                        End Try
                    Else
                        Exit Sub
                    End If
                Catch ex As Exception
                End Try
            Next
        End If

    End Sub

End Class



