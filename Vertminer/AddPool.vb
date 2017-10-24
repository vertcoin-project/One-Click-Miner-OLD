Imports VertcoinOneClickMiner.Core

Public Class AddPool
    Private ReadOnly _logger As ILogger

    Public Sub New(logger As ILogger)
        InitializeComponent()
        _logger = logger
    End Sub

    Private Sub AddPool_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Invoke(New MethodInvoker(AddressOf Style))

    End Sub

    Public Sub Style()

        Panel1.BackColor = Color.FromArgb(27, 92, 46)
        Button1.BackColor = Color.FromArgb(27, 92, 46)
        Panel2.BackColor = Color.FromArgb(41, 54, 61)

    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click

        Me.Close()

    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click

        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not Pool_Address.Text = "" And Not Wallet_Address.Text = "" And Not Password_Address.Text = "" Then
            'Perform a simple check on the wallet address
            If Wallet_Address.Text.Count = 34 Then
            Else
                MsgBox("Vertcoin addresses are atleast 34 characters long, yours is not, please verify")
                Exit Sub
            End If

            If Wallet_Address.Text.StartsWith("V") Then
            Else
                MsgBox("Vertcoin addresses start with a V, yours does not, please verify")
                Exit Sub

            End If

            pools.Insert(0, Pool_Address.Text)
            workers.Insert(0, Wallet_Address.Text)
            passwords.Insert(0, Password_Address.Text)
            Invoke(New MethodInvoker(AddressOf Main.Update_Pool_Info))
            Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
            MsgBox("Pool(s) added successfully.")
            Me.Close()
        Else
            MsgBox("Not all fields are complete.")
        End If

    End Sub

    Dim drag As Boolean = False
    Dim mousex As Integer, mousey As Integer

    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown, Panel2.MouseDown, PictureBox11.MouseDown, Label6.MouseDown

        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove, Panel2.MouseMove, PictureBox11.MouseMove, Label6.MouseMove

        If drag Then
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
        End If

    End Sub

    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp, Panel2.MouseUp, PictureBox11.MouseUp, Label6.MouseUp

        drag = False

    End Sub

End Class
