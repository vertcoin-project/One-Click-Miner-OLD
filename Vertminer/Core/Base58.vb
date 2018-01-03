'https://github.com/casascius/Bitcoin-Address-Utility
Namespace Core
    Public Class Base58

        ''' <summary>
        ''' Converts a base-58 string to a byte array, returning Nothing if it wasn't valid.
        ''' </summary>
        Public Shared Function ToByteArray(ByVal base58 As String) As Byte()
            Dim bi2 = New Org.BouncyCastle.Math.BigInteger("0")
            Dim b58 As String = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz"
            For Each c As Char In base58
                If (b58.IndexOf(c) <> -1) Then
                    bi2 = bi2.Multiply(New Org.BouncyCastle.Math.BigInteger("58"))
                    bi2 = bi2.Add(New Org.BouncyCastle.Math.BigInteger(b58.IndexOf(c).ToString))
                Else
                    Return Nothing
                End If

            Next
            Dim bb() As Byte = bi2.ToByteArrayUnsigned
            ' interpret leading '1's as leading zero bytes
            For Each c As Char In base58
                If (c <> Microsoft.VisualBasic.ChrW(49)) Then
                    Exit For
                End If

                Dim bbb() As Byte = New Byte(bb.Length) {}
                Array.Copy(bb, 0, bbb, 1, bb.Length)
                bb = bbb
            Next
            Return bb
        End Function
    End Class
End Namespace