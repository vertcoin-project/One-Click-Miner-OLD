Imports System.IO

Namespace Core
    Public Interface ISystemWrapper
        ReadOnly Property UtcNow As DateTime
        Sub AppendAllLines(path As String, lines As IEnumerable(Of String))
    End Interface


    Public Class SystemWrapper : Implements ISystemWrapper

        Public ReadOnly Property UtcNow As Date Implements ISystemWrapper.UtcNow
            Get
                Return DateTime.UtcNow
            End Get
        End Property

        Public Sub AppendAllLines(path As String, lines As IEnumerable(Of String)) Implements ISystemWrapper.AppendAllLines
            File.AppendAllLines(path, lines)
        End Sub
    End Class
End Namespace