Public Class progress

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        canceldownloadasync = True
        downloadclient.CancelAsync()
        update_complete = True
        'If p2pool_update = True Then
        '    If System.IO.Directory.Exists(P2PoolFolder) = True Then
        '        System.IO.Directory.Delete(P2PoolFolder, True)
        '    End If
        'ElseIf amd_update = True Then
        '    If System.IO.Directory.Exists(P2PoolFolder) = True Then
        '        System.IO.Directory.Delete(P2PoolFolder, True)
        '    End If
        'ElseIf nvidia_update = True Then
        '    If System.IO.Directory.Exists(P2PoolFolder) = True Then
        '        System.IO.Directory.Delete(P2PoolFolder, True)
        '    End If
        'ElseIf cpu_update = True Then
        '    If System.IO.Directory.Exists(P2PoolFolder) = True Then
        '        System.IO.Directory.Delete(P2PoolFolder, True)
        '    End If
        'End If
        Me.Close()

    End Sub

End Class