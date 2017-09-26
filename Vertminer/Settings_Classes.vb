Public Class Settings_JSON

    'Settings
    Public Property appdata As String
    Public Property start_minimized As Boolean
    Public Property start_with_windows As Boolean
    Public Property autostart_p2pool As Boolean
    Public Property autostart_mining As Boolean
    Public Property mine_when_idle As Boolean
    Public Property keep_miner_alive As Boolean
    Public Property keep_p2pool_alive As Boolean
    Public Property use_upnp As Boolean = False
    Public Property p2pool_network As String
    Public Property p2pool_node_fee As Integer
    Public Property p2pool_donation As Integer
    Public Property max_connections As Integer
    Public Property p2pool_port As String
    Public Property mining_port As String
    Public Property mining_intensity As Decimal
    'Public Property workers As String
    'Public Property password As String
    Public Property p2pool_fee_address As String
    Public Property miner_version
    Public Property p2pool_version
    Public Property amd_version
    Public Property nvidia_version
    Public Property cpu_version
    Public Property default_miner As String
    'Public Property additional_miner_config As String
    Public Property pools As New List(Of Pools_JSON)

    'CHANGE GENERAL JSON SETTINGS TO ACCOUNT FOR MINER POOL JSON SETTINGS
    'use Public Property pools As New List(Of Pools_JSON) instead of pools arraylist and worker/password strings
End Class

Public Class Pools_JSON

    Public Property url As String
    Public Property user As String
    Public Property pass As String

End Class

Public Class AMD_Miner_Settings_JSON

    Public Property pools As New List(Of AMD_Pools_JSON)
    Public Property algorithm As String
    Public Property intensity As Decimal

End Class

Public Class AMD_Pools_JSON

    Public Property url As String
    Public Property user As String
    Public Property userpass As String

End Class

Public Class NVIDIA_Miner_Settings_JSON

    Public Property pools As New List(Of Pools_JSON)
    Public Property algo As String
    Public Property intensity As Decimal

End Class

Public Class CPU_Miner_Settings_JSON

    Public Property url As String
    Public Property user As String
    Public Property pass As String
    Public Property algo As String
    Public Property intensity As Decimal

End Class

'JSON direct from scanner server
Public Class Node_JSON

    Public Property nodes As New List(Of Node_Info_JSON)

End Class

'Nested "Info" JSON from scanner server
Public Class Node_Info_JSON

    Public Property ip As String
    Public Property port As UInt64?
    Public Property fee As Decimal?
    Public Property stats As Node_Stats_JSON
    Public Property geo As Node_Geo_JSON

End Class

'Nested "Stats" JSON from scanner server
Public Class Node_Stats_JSON

    Public Property efficiency_if_miner_perfect As Decimal?
    Public Property attempts_to_block As UInt64?
    'Public Property warnings As New ArrayList()
    Public Property block_value As Decimal?
    'Public Property miner_dead_hash_rates As New ArrayList()
    'Public Property peers As New ArrayList()
    Public Property efficiency As Decimal?
    Public Property fee As Decimal?
    'Public Property my_share_counts_in_last_hour As New ArrayList()
    'Public Property miner_hash_rates As New ArrayList()
    Public Property donation_proportion As Decimal?
    Public Property uptime As Decimal?
    'Public Property my_stale_proportions_in_last_hour As New ArrayList()
    'Public Property miner_last_difficulties As New ArrayList()
    'Public Property shares As New ArrayList()
    Public Property version As String
    Public Property protocol_version As String
    'Public Property my_hash_rates_in_last_hour As New ArrayList()
    Public Property attempts_to_share As UInt64?

End Class

'Nested "Geo" JSON from scanner server
Public Class Node_Geo_JSON

    Public Property country As String
    Public Property img As String

End Class
