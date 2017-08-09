Imports System.Net

Module variables

    'General
    Public Property Miner_Version = Application.ProductVersion
    Public Property P2Pool_Version = "0.0.0.0"
    Public Property AMD_Version = "0.0.0.0"
    Public Property Nvidia_Version = "0.0.0.0"
    Public Property CPU_Version = "0.0.0.0"
    Public Property SettingsFolder As String = ""
    Public Property appdata As String = ""
    Public Property SettingsIni As String = ""
    Public Property SysLog As String = ""
    Public Property AmdFolder As String = ""
    Public Property NvidiaFolder As String = ""
    Public Property CPUFolder As String = ""
    Public Property P2PoolFolder As String = ""
    Public Property command As String
    Public Property NewLog As String
    Public Property LogFileString As String
    Public Property platform As Boolean = False 'true=64-bit false=32-bit
    Public Property otherminer As Boolean = False
    Public Property otherp2pool As Boolean = False
    Public Property P2Pool_Detected = False
    Public Property AMD_Detected = False
    Public Property Nvidia_Detected = False
    Public Property CPU_Detected = False

    'Update Variables
    Public Property update_needed As Boolean = False
    Public Property autocheck_updates As Boolean = False
    Public Property update_complete As Boolean = False
    Public Property newestversion As New Version
    Public Property updatelink As String
    Public Property p2pool_update As Boolean = False
    Public Property amd_update As Boolean = False
    Public Property nvidia_update As Boolean = False
    Public Property cpu_update As Boolean = False
    Public Property downloadclient As New WebClient
    Public Property canceldownloadasync As Boolean = False

    'Settings
    Public Property start_minimized As Boolean = False
    Public Property hide_windows As Boolean = False
    Public Property start_with_windows As Boolean = False
    Public Property autostart_p2pool As Boolean = False
    Public Property autostart_mining As Boolean = False
    Public Property autostart_miner As String = ""
    Public Property start_mining_when_idle As Boolean = False
    Public Property P2P_Network As Integer = 1
    Public Property P2P_Node_Fee As Integer = 0
    Public Property P2P_Node_Fee_Address As String = "VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG"
    Public Property P2P_Donation As Integer = 1
    Public Property MaxConnections As Integer = 50
    Public Property MiningIdle As Integer = 60
    Public Property RestartDelay As Integer = 1

    'P2Pool Variables
    Public Property p2pool_running As Boolean = False
    Public Property p2pool_port As String = "9346"
    Public Property Keep_P2Pool_Alive As Boolean = False
    Public Property p2pool_installed As Boolean = False
    Public Property p2pool_initialized As Boolean = False
    Public Property use_UPnP As Boolean = False
    Public Property p2pool_config_file As String = ""
    Public Property p2pool_config As String = ""
    Public Property config_server As String = ""
    Public Property rpc_allowip As String = ""
    Public Property rpc_user As String = ""
    Public Property rpc_password As String = ""
    Public Property rpc_port As String = ""

    'Process Variables
    Public Property cc As Boolean = False
    Public Property sg As Boolean = False
    Public Property cpu As Boolean = False

    'Miner Variables
    Public Property zipPath As String = ""
    Public Property extractpath As String = ""
    Public Property exe As String = ""
    Public Property dll As String = ""
    Public Property miner_config_file As String = ""
    Public Property miner_config As String = ""
    Public Property additional_config As String = ""
    Public Property mining_installed As Boolean = False
    Public Property mining_initialized As Boolean = False

    'Miner Settings
    Public Property mining_running As Boolean = False
    Public Property DefaultMiner As String = ""
    Public Property AmdMiner As Boolean = False
    Public Property NvidiaMiner As Boolean = False
    Public Property CPUMiner As Boolean = False
    Public Property NvidiaDownload As String
    Public Property AmdDownload As String
    Public Property MinerPrompt As Boolean = False
    Public Property mining_port As String = "9171"
    Public Property Keep_Miner_Alive As Boolean = False

    'Miner Configuration
    Public Property Intensity As Decimal = 0
    Public Property Worker As String = "VpBsRnN749jYHE9hT8dZreznHfmFMdE1yG"
    Public Property Password As String = "x"
    Public Property Pool_Address As String = "stratum+tcp://vtc.alwayshashing.com:9171"
    Public Property Config_Setting As String = "default"

End Module
