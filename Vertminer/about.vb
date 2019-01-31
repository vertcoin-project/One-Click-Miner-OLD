Imports VertcoinOneClickMiner.Core

Public Class about
    Private ReadOnly _logger As ILogger

    Public Sub New(logger As ILogger)
        _logger = logger
        InitializeComponent()
    End Sub

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
            Dim url As String = "https://vertcoin.org/donate/"
            Process.Start(url)
        Catch ex As Exception
            MsgBox(ex.Message)
            _logger.LogError(ex)
        Finally
            _logger.Trace("Loaded: OK")
        End Try

    End Sub

    Private Sub about_Close(sender As Object, e As EventArgs) Handles MyBase.FormClosed

        _logger.Trace("Closed: OK")
    End Sub

End Class