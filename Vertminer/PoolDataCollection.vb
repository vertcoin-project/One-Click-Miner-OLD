Imports System.ComponentModel

Public Class PoolDataCollection
    Inherits BindingList(Of PoolData)

    Public Sub ClearSelected()
        For Each Obj In Me
            Obj.IsSelected = False
        Next
    End Sub
End Class

Public Class PoolData

    Public Property IsSelected As Boolean
    Public Property Worker As String
    Public Property Description As String
    Public Property Pool As String
    Public Property Password As String


    Public Sub New(ByVal _worker As String, ByVal _description As String, ByVal _pool As String, ByVal _password As String, Optional ByVal _isselected As Boolean = False)
        Worker = _worker
        Description = _description
        Pool = _pool
        Password = _password
        IsSelected = _isselected
    End Sub

    Public Sub New()

    End Sub

End Class
