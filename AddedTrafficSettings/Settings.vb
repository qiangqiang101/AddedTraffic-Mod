Imports System.IO
Imports System.Xml.Serialization
Imports GTA
Imports GTA.Native

Public Structure Settings
    Public ReadOnly Property Instance As Settings
        Get
            Return ReadFromFile()
        End Get
    End Property

    <XmlIgnore>
    Public Property FileName() As String

    Public WaitTime As WaitTime
    Public CruiseSpeed As Single
    Public SpawnDistance As Single
    Public DrivingStyle As String
    Public EnableUpgrade As Boolean
    Public UpgradeChance As Integer
    Public RandomizeColor As Boolean
    Public SwapChance As Integer
    Public SwapDistance As Single
    Public Notify As Boolean
    Public ShowBlip As Boolean
    Public RoadType As String
    Public VehicleSwaps As List(Of VehicleSwap)
    Public Vehicles As Vehicles

    Public Sub New(_filename As String)
        FileName = _filename
    End Sub

    Public Sub Save()
        Dim ser = New XmlSerializer(GetType(Settings))
        Dim writer As TextWriter = New StreamWriter(FileName)
        ser.Serialize(writer, Me)
        writer.Close()
    End Sub

    Private Function GenerateVehicleList() As Vehicles
        Dim vehicles As New Vehicles()
        With vehicles
            'City Center
            .Downtown = New List(Of String) From {"sultanrs", "banshee2", "specter", "elegy", "comet3", "raptor", "tampa2", "tropos", "schafter3", "schafter4", "verlierer2", "kuruma", "tampa", "faction",
                                                  "chino", "moonbeam", "nightshade", "coquette3", "fcr", "diablous", "zombieb", "zombiea", "wolfsbane", "faggio3", "faggio", "esskey", "defiler", "daemon2",
                                                  "chimera", "avarus", "xls", "baller4", "baller3", "brioso", "tailgater", "buffalo2", "yosemite", "ellie", "dominator3", "clique", "deviant", "impaler", "tulip",
                                                  "vamos", "gauntlet3", "gauntlet4", "yosemite2", "sentinel3", "flashgt", "gb200", "issi7", "sugoi", "sultan2", "rapidgt3", "retinue", "deluxo", "hermes",
                                                  "hustler", "savestra", "z190", "cheburek", "jester3", "michelli", "fagaloa", "nebula", "zion3", "retinue2", "streiter", "issi3", "asbo", "kanjo"}
            'Rich place
            .Vinewood = New List(Of String) From {"tempesta", "penetrator", "nero2", "nero", "italigtb2", "italigtb", "le7b", "sheava", "tyrus", "reaper", "prototipo", "pfister811", "fmj", "t20", "osiris",
                                                  "fcr2", "diablous2", "vortex", "specter2", "sanctus", "nightblade", "hakuchou2", "vindicator", "xls", "baller4", "btype3", "mamba", "btype2", "feltzer3",
                                                  "casco", "coquette2", "btype", "omnis", "lynx", "seven70", "bestiagts", "baller3", "windsor2", "windsor", "cognoscenti", "cog55", "ruston", "comet5", "neon",
                                                  "pariah", "raiden", "revolter", "schlagen", "drafter", "jugular", "locust", "paragon", "imorgon", "komoda", "vstr", "oppressor", "oppressor2", "infernus2",
                                                  "turismo2", "ardent", "cheetah2", "torero", "gt500", "stromberg", "viseris", "stafford", "swinger", "dynasty", "jb7002", "patriot2", "toros", "novak", "rebla",
                                                  "voltic2", "gp1", "vagner", "xa21", "cyclone", "visione", "autarch", "sc1", "entity2", "tyrant", "tezeract", "taipan", "scramjet", "deveste", "italigto",
                                                  "emerus", "krieger", "neo", "s80", "thrax", "zorrusso", "furia"}
            'Gang place
            .SouthLosSantos = New List(Of String) From {"dubsta3", "raptor", "virgo3", "virgo2", "tornado5", "slamvan3", "sabregt2", "minivan2", "faction3", "voodoo", "primo2", "moonbeam2", "faction2",
                                                        "chino2", "buccaneer2"}
            'Port place
            .PortOfSouthLosSantos = New List(Of String) From {"rumpo3", "speedo4"}
            'Industrial
            .EastLosSantos = New List(Of String) From {"youga2", "zhaba", "speedo4"}
            'Beach, tourist attraction
            .Vespucci = New List(Of String) From {"sultanrs", "banshee2", "kalahari", "bifta", "elegy", "comet3", "raptor", "vagrant"}
            'Los santos city
            .LosSantos = New List(Of String) From {"sultanrs", "banshee2", "specter", "elegy", "comet3", "raptor", "tampa2", "tropos", "schafter3", "schafter4", "verlierer2", "kuruma", "tampa", "faction",
                                                   "chino", "moonbeam", "nightshade", "coquette3", "fcr", "diablous", "zombieb", "zombiea", "wolfsbane", "faggio3", "faggio", "esskey", "defiler", "daemon2",
                                                   "chimera", "avarus", "xls", "baller4", "baller3", "brioso", "tailgater", "buffalo2", "yosemite", "ellie", "dominator3", "clique", "deviant", "impaler", "tulip",
                                                   "vamos", "gauntlet3", "gauntlet4", "yosemite2", "sentinel3", "flashgt", "gb200", "issi7", "sugoi", "sultan2", "rapidgt3", "retinue", "deluxo",
                                                   "hermes", "hustler", "savestra", "z190", "cheburek", "jester3", "michelli", "fagaloa", "nebula", "zion3", "retinue2", "streiter", "issi3", "asbo", "kanjo"}
            'Offroad and dirty cars
            .GrandSenoraDesert = New List(Of String) From {"brawler", "dubsta3", "bifta", "youga2", "rumpo3", "zombieb", "zombiea", "wolfsbane", "tornado6", "ratbike", "manchez", "esskey", "bf400",
                                                           "cliffhanger", "gargoyle", "contender", "xls", "baller4", "baller3", "bodhi2", "trophytruck", "trophytruck2", "blazer4", "blazer5", "kamacho",
                                                           "riata", "freecrawler", "caracara2", "hellion", "everon", "outlaw", "vagrant", "peyote2", "rrocket", "stryder", "nebula", "comet4"}
            'Mountain area
            .SanChianskiMountainRange = New List(Of String) From {"brawler", "bifta", "manchez", "bf400", "cliffhanger", "gargoyle", "trophytruck", "trophytruck2", "blazer4", "blazer5", "stryder"}
            'Offroad and dirty cars
            .BlaineCounty = New List(Of String) From {"brawler", "dubsta3", "bifta", "youga2", "rumpo3", "zombieb", "zombiea", "wolfsbane", "tornado6", "ratbike", "manchez", "esskey", "bf400", "cliffhanger",
                                                      "gargoyle", "contender", "xls", "baller4", "baller3", "bodhi2", "trophytruck", "trophytruck2", "blazer4", "blazer5", "kamacho", "riata", "freecrawler",
                                                      "caracara2", "hellion", "everon", "outlaw", "vagrant", "peyote2", "comet4", "rrocket", "stryder", "nebula"}
        End With
        Return vehicles
    End Function

    Public Function GenerateVehicleSwapList() As List(Of VehicleSwap)
        Return New List(Of VehicleSwap) From {
                New VehicleSwap("panto", False), New VehicleSwap("issi2", False), New VehicleSwap("huntley", False), New VehicleSwap("seminole", False), New VehicleSwap("dominator", False),
                New VehicleSwap("penumbra", False), New VehicleSwap("sabregt", False), New VehicleSwap("warrener", False), New VehicleSwap("oracle2", False), New VehicleSwap("ruiner", False)}
    End Function

    Public Function ReadFromFile() As Settings
        If Not File.Exists(FileName) Then
            Return New Settings(FileName) With {.WaitTime = New WaitTime(15, 10, 15, 10, 20), .CruiseSpeed = 20.0F, .SpawnDistance = 100.0F, .DrivingStyle = "Normal", .EnableUpgrade = True,
                                                .UpgradeChance = 20, .RandomizeColor = True, .SwapChance = 50, .SwapDistance = 100.0F, .Notify = False, .ShowBlip = False, .RoadType = "AsphaltRoad",
                                                .Vehicles = GenerateVehicleList(), .VehicleSwaps = GenerateVehicleSwapList()}
        End If

        Try
            Dim ser = New XmlSerializer(GetType(Settings))
            Dim reader As TextReader = New StreamReader(FileName)
            Dim instance = CType(ser.Deserialize(reader), Settings)
            reader.Close()
            Return instance
        Catch ex As Exception
            Return New Settings(FileName) With {.WaitTime = New WaitTime(15, 10, 15, 10, 20), .CruiseSpeed = 20.0F, .SpawnDistance = 100.0F, .DrivingStyle = "Normal", .EnableUpgrade = True,
                                                .UpgradeChance = 20, .RandomizeColor = True, .SwapChance = 50, .SwapDistance = 100.0F, .Notify = False, .ShowBlip = False, .RoadType = "AsphaltRoad",
                                                .Vehicles = GenerateVehicleList(), .VehicleSwaps = GenerateVehicleSwapList()}
        End Try
    End Function

End Structure

Public Structure VehicleSwap

    Public OldVehicle As String
    Public NewVehicle As String
    Public Enable As Boolean

    Public Sub New(ov As String, nv As String, Optional en As Boolean = True)
        OldVehicle = ov
        NewVehicle = nv
        Enable = en
    End Sub

    Public Sub New(ov As String, Optional en As Boolean = True)
        OldVehicle = ov
        NewVehicle = Nothing
        Enable = en
    End Sub

End Structure

Public Structure Vehicles
    Public Downtown As List(Of String)
    Public Vinewood As List(Of String)
    Public SouthLosSantos As List(Of String)
    Public PortOfSouthLosSantos As List(Of String)
    Public EastLosSantos As List(Of String)
    Public Vespucci As List(Of String)
    Public LosSantos As List(Of String)
    Public GrandSenoraDesert As List(Of String)
    Public SanChianskiMountainRange As List(Of String)
    Public BlaineCounty As List(Of String)

    Public Sub New(dt As List(Of String), vw As List(Of String), sls As List(Of String), psls As List(Of String), els As List(Of String), ves As List(Of String), ls As List(Of String),
                   gsd As List(Of String), scmr As List(Of String), bc As List(Of String))
        Downtown = dt
        Vinewood = vw
        SouthLosSantos = sls
        PortOfSouthLosSantos = psls
        EastLosSantos = els
        Vespucci = ves
        LosSantos = ls
        GrandSenoraDesert = gsd
        SanChianskiMountainRange = scmr
        BlaineCounty = bc
    End Sub

End Structure

Public Structure WaitTime
    Public Morning As Integer
    Public Afternoon As Integer
    Public Evening As Integer
    Public Night As Integer
    Public Midnight As Integer

    Public Sub New(morn As Integer, noon As Integer, even As Integer, nite As Integer, midn As Integer)
        Morning = morn
        Afternoon = noon
        Evening = even
        Night = nite
        Midnight = midn
    End Sub
End Structure