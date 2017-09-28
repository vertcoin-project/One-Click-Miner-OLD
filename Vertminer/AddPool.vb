Public Class AddPool

    Private Sub AddPool_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Invoke(New MethodInvoker(AddressOf Style))

    End Sub

    Public Sub Style()

        Panel1.BackColor = Color.FromArgb(27, 92, 46)
        Button1.BackColor = Color.FromArgb(27, 92, 46)
        Panel2.BackColor = Color.FromArgb(41, 54, 61)
        'TextBox3.BackColor = Color.FromArgb(41, 54, 61)
        'MenuStrip.BackColor = Color.FromArgb(27, 92, 46)

    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click

        Me.Close()

    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click

        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not Pool_Address.Text = "" And Not Wallet_Address.Text = "" And Not Password_Address.Text = "" Then
            pools.Insert(0, Pool_Address.Text)
            workers.Insert(0, Wallet_Address.Text)
            passwords.Insert(0, Password_Address.Text)
            Invoke(New MethodInvoker(AddressOf Main.Update_Pool_Info))
            Invoke(New MethodInvoker(AddressOf Main.SaveSettingsJSON))
            MsgBox("Pool entered successfully.")
            Me.Close()
        Else
            MsgBox("Not all fields are complete.")
        End If

    End Sub

End Class