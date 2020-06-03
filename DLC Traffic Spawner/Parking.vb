Imports System.IO
Imports System.Xml.Serialization
Imports GTA.Math

Public Structure Parking

    Public ReadOnly Property Instance As Parking
        Get
            Return ReadFromFile()
        End Get
    End Property

    <XmlIgnore>
    Public Property FileName() As String

    Public Coords As List(Of Vector4)

    Public Sub New(_filename As String)
        FileName = _filename
    End Sub

    Public Sub Save()
        Dim ser = New XmlSerializer(GetType(Parking))
        Dim writer As TextWriter = New StreamWriter(FileName)
        ser.Serialize(writer, Me)
        writer.Close()
    End Sub

    Public Function ReadFromFile() As Parking
        If Not File.Exists(FileName) Then
            Return New Parking(FileName) With {.Coords = New List(Of Vector4)}
        End If

        Try
            Dim ser = New XmlSerializer(GetType(Parking))
            Dim reader As TextReader = New StreamReader(FileName)
            Dim instance = CType(ser.Deserialize(reader), Parking)
            reader.Close()
            Return instance
        Catch ex As Exception
            Return New Parking(FileName) With {.Coords = New List(Of Vector4)}
        End Try
    End Function

End Structure

Public Structure Vector4

    Public Position As Vector3
    Public Heading As Single

    Public Sub New(pos As Vector3, head As Single)
        Position = pos
        Heading = head
    End Sub

    Public Sub New(x As Single, y As Single, z As Single, h As Single)
        Position = New Vector3(x, y, z)
        Heading = h
    End Sub

End Structure