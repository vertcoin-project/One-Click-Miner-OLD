Imports System.Runtime.CompilerServices

Namespace Core

    Public Interface ILogger

        Sub Trace(message As String, <CallerMemberName> Optional caller As String = Nothing)
        Sub LogError(exception As Exception, <CallerMemberName> Optional caller As String = Nothing)

    End Interface

    Public Class FileLogger : Implements ILogger

        Private ReadOnly _path As String
        Private ReadOnly _logFormat As String
        Private ReadOnly _lock As Object = New Object
        Private ReadOnly _systemCalls As ISystemWrapper

        Public Sub New(path As String, Optional systemWrapper As ISystemWrapper = Nothing)
            If systemWrapper Is Nothing Then systemWrapper = New SystemWrapper()

            _systemCalls = systemWrapper

            If path Is Nothing then throw new ArgumentNullException(Nameof(path))
            if String.IsNullOrWhiteSpace(path) Then Throw new ArgumentException("Invalid log file path", nameof(path))

            _path = path
            _logFormat = "- {0}, {1}: {2}" 'timestamp, caller, message
        End Sub

        Public Sub Trace(message As String, <CallerMemberName> Optional caller As String = Nothing) Implements ILogger.Trace
            WriteLogFileRow(String.Format(_logFormat, _systemCalls.UtcNow.ToString("O"), caller, message))
        End Sub

        Public Sub LogError(exception As Exception, <CallerMemberName> Optional caller As String = Nothing) Implements ILogger.LogError
            Trace(exception.ToString(), caller)
        End Sub

        Private Sub WriteLogFileRow(message As String)
            Try
                SyncLock _lock
                    _systemCalls.AppendAllLines(_path, {message})
                End SyncLock
            Catch ex As Exception
                ' Intentionally empty
            End Try
        End Sub
    End Class

End Namespace