Public Class about

    Private Sub about_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim version As String = Application.ProductVersion
        version = version
        Label3.Text = version

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Close()

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        Try
            Dim url As String = "http://www.vtcfund.com"
            Process.Start(url)
        Catch ex As Exception
            MsgBox(ex.Message)
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "About(), " & ex.Message)
        Finally
            NewLog = NewLog & Environment.NewLine
            NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "About(), Loaded: OK")
        End Try

    End Sub

    Private Sub about_Close(sender As Object, e As EventArgs) Handles MyBase.FormClosed

        NewLog = NewLog & Environment.NewLine
        NewLog = NewLog & ("- " & Date.Parse(Now) & ", " & "About(), Closed: OK")

    End Sub

End Class