'https://github.com/casascius/Bitcoin-Address-Utility
Imports Org.BouncyCastle.Crypto.Digests

Namespace Core
    Public Class VertcoinAddressUtility
        ''' <summary>
        ''' Converts a base-58 string to a byte array, checking the checksum, and
        ''' returning Nothing if it wasn't valid.  Appending "?" to the end of the string skips
        ''' the checksum calculation, but still strips the four checksum bytes from the
        ''' result.
        ''' </summary>
        Public Shared Function Base58CheckToByteArray(ByVal base58_string As String) As Byte()
            Dim IgnoreChecksum As Boolean = False
            If base58_string.EndsWith("?") Then
                IgnoreChecksum = True
                base58_string = base58_string.Substring(0, (base58_string.Length - 1))
            End If

            Dim bb() As Byte = Base58.ToByteArray(base58_string)
            If ((bb Is Nothing) OrElse (bb.Length < 4)) Then
                Return Nothing
            End If

            If Not IgnoreChecksum Then
                Dim bcsha256a = New Sha256Digest
                bcsha256a.BlockUpdate(bb, 0, (bb.Length - 4))
                Dim checksum() As Byte = New Byte((32) - 1) {}
                bcsha256a.DoFinal(checksum, 0)
                bcsha256a.BlockUpdate(checksum, 0, 32)
                bcsha256a.DoFinal(checksum, 0)
                Dim i As Integer = 0
                Do While (i < 4)
                    If (checksum(i) <> bb(((bb.Length - 4) + i))) Then
                        Return Nothing
                    End If

                    i = (i + 1)
                Loop

            End If

            Dim rv() As Byte = New Byte(((bb.Length - 4)) - 1) {}
            Array.Copy(bb, 0, rv, 0, (bb.Length - 4))
            Return rv
        End Function

        Public Shared Function IsWalletAddressValid(walletAddress As String) As Boolean
            Dim walletBytes = Base58CheckToByteArray(walletAddress)
            If walletBytes Is Nothing Then
                Return False
            End If
            Dim versionByte = walletBytes(0)
            Const P2PKH_VERBYTE = &H47
            Const P2SH_VERBYTE = &H5
            Return (versionByte = P2PKH_VERBYTE) Or (versionByte = P2SH_VERBYTE)
        End Function
    End Class
End Namespace