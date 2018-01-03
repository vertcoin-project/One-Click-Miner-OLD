Imports NUnit.Framework
Imports VertcoinOneClickMiner.Core

<TestFixture>
Public Class VertcoinAddressUtilityTests
    <Test>
    <TestCase("VnkLyFgSnZShC9d52Xf9YTRPcUXyoypT4u")>
    <TestCase("VfukW89WKT9h3YjHZdSAAuGNVGELY31wyj")>
    <TestCase("VoCJWKjFc8PFTMhDarKwCYGdggpgHn6L43")>
    Public Sub IsWalletAddressValid_WhenCalledWithValidWalletAddress_ShouldReturnTrue(walletAddress As String)
        Assert.IsTrue(VertcoinAddressUtility.IsWalletAddressValid(walletAddress))
    End Sub

    <Test>
    <TestCase("x")>
    <TestCase("16rCmCmbuWDhPjWTrpQGaU3EPdZF7MTdUk")> 'Bitcoin address
    <TestCase("")>
    Public Sub IsWalletAddressValid_WhenCalledWithInvalidWalletAddress_ShouldReturnFalse(walletAddress As String)
        Assert.IsFalse(VertcoinAddressUtility.IsWalletAddressValid(walletAddress))
    End Sub
End Class
