Imports System.IO, System.Threading

''' <summary>
''' Représente un gestionnaire de son. Permet d'ajouter, de lire et d'arrêter différents sons.
''' </summary>
''' <remarks></remarks>
Public Class SoundsManager

    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" _
(ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As _
Integer, ByVal hwndCallback As Integer) As Integer

    Private SoundFilesPath As String
    Private UUID As String
    Private Separator As String
    Private Rand As Random
    Private Const EXT As String = ".wav"

    Private Names As Dictionary(Of String, List(Of String))
    Private CurrentID As Integer
    Private Channels As List(Of String)

    'CONSTRUCTEURS
    ''' <summary>
    ''' Initialise une nouvelle instance de la classe SoundsManager sans inclure directement les sons.
    ''' </summary>
    ''' <param name="soundFilesPath">Répertoire qui contiendra les fichiers sons.</param>
    ''' <param name="separator">Séparateur préfixe-numéro.</param>
    ''' <param name="rand">Instance de la classe System.Random pour permettre de jouer un son aléatoire.</param>
    ''' <param name="uuid">ID unique.</param>
    ''' <remarks></remarks>
    Public Sub New(soundFilesPath As String, separator As String, ByRef rand As Random, uuid As String)
        If Not soundFilesPath.EndsWith("\") Then
            soundFilesPath &= "\"
        End If
        Me.SoundFilesPath = soundFilesPath
        Me.Separator = separator
        Me.Rand = rand
        Me.UUID = uuid
        Names = New Dictionary(Of String, List(Of String))
        Channels = New List(Of String)
        CreateDirectory()
    End Sub
    ''' <summary>
    ''' Initialise une nouvelle instance de la classe SoundsManager à l'aide d'une liste de ressources.
    ''' </summary>
    ''' <param name="soundFilesPath">Répertoire qui contiendra les fichiers sons.</param>
    ''' <param name="separator">Séparateur préfixe-numéro.</param>
    ''' <param name="rand">Instance de la classe System.Random pour permettre de jouer un son aléatoire.</param>
    ''' <param name="resourceSet">ResourceSet contenant, entre autres, les sons à ajouter.</param>
    ''' <param name="key">Chaîne par laquelle doivent commencer les noms de ressources pour qu'elles soient ajoutées.</param>
    ''' <param name="uuid">ID unique.</param>
    ''' <remarks></remarks>
    Public Sub New(soundFilesPath As String, separator As String, ByRef rand As Random, uuid As String, resourceSet As System.Resources.ResourceSet, key As String)
        Me.New(soundFilesPath, separator, rand, uuid)
        For Each resource As DictionaryEntry In resourceSet
            If resource.Key.ToString().StartsWith(key) Then
                AddSound(resource.Key.ToString().Substring(key.Length), resource.Value)
            End If
        Next
    End Sub

    ''METHODES

    Private Sub CreateDirectory()
        If Not Directory.Exists(SoundFilesPath) Then
            Directory.CreateDirectory(SoundFilesPath)
        End If
    End Sub
    ''' <summary>
    ''' Ajouter un son provenant d'une ressource.
    ''' </summary>
    ''' <param name="soundName">Nom de la ressource.</param>
    ''' <param name="sound">Ressource elle-même.</param>
    ''' <returns>Renvoie True si le son a bien été ajouté.</returns>
    ''' <remarks></remarks>
    Public Function AddSound(soundName As String, sound As UnmanagedMemoryStream)
        Try
            Dim tempMemoryStream As New MemoryStream()
            sound.CopyTo(tempMemoryStream)
            File.WriteAllBytes(SoundFilesPath & soundName & EXT, tempMemoryStream.ToArray())
            Dim prefix As String = Functions.Misc.SplitString(soundName, Separator)(0)
            If Not Names.ContainsKey(prefix) Then
                Names.Add(prefix, New List(Of String))
            End If
            Names(prefix).Add(SoundFilesPath & soundName & EXT)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'Gestionnaire des sons
    ''' <summary>
    ''' Arrête tous les sons en cours.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseAll()
        Dim channelsToDelete As New List(Of String)
        For Each channel As String In Channels
            StopSound(channel)
            channelsToDelete.Add(channel)
        Next
        For Each channel As String In channelsToDelete
            Channels.Remove(channel)
        Next
        channelsToDelete.Clear()
    End Sub
    ''' <summary>
    ''' Arrête tous les sons en cours sauf ceux dont le nom contient un chaîne spécifique.
    ''' </summary>
    ''' <param name="exceptions">Chaîne à exclure de l'arrêt.</param>
    ''' <remarks></remarks>
    Public Sub CloseAllButSomeExceptions(exceptions As String)
        Dim channelsToDelete As New List(Of String)
        For Each channel As String In Channels
            If Not channel.Contains(exceptions) Then
                StopSound(channel)
                channelsToDelete.Add(channel)
            End If
        Next
        For Each channel As String In channelsToDelete
            Channels.Remove(channel)
        Next
        channelsToDelete.Clear()
    End Sub
    Private Sub StopSound(name As String)
        If name <> "" Then
            mciSendString("stop " & name, Nothing, 0, 0)
            mciSendString("close " & name, Nothing, 0, 0)
        End If
    End Sub
    Private Function PlaySoundWithFileName(name As String) As String
        Dim nom As String = Path.GetFileNameWithoutExtension(name) & "_" & CStr(CurrentID)
        Channels.Add(nom)
        mciSendString("open " & Chr(34) & name & Chr(34) & " alias " & nom, Nothing, 0, 0)
        mciSendString("play " & nom, Nothing, 0, 0)
        CurrentID += 1
        Return nom
    End Function
    ''' <summary>
    ''' Joue un son au hasard parmi ceux partageant le préfixe envoyé en paramètre.
    ''' </summary>
    ''' <param name="soundPrefix">Préfixe à jouer.</param>
    ''' <remarks></remarks>
    Public Sub PlaySound(soundPrefix As String)
        'PlaySoundWithFileName(Functions.Misc.GetRandomElementOfList(Names(soundPrefix), Rand))
        Dim audioThread As Thread = New Thread(Sub() PlaySoundWithFileName(Functions.Misc.GetRandomElementOfList(Names(soundPrefix), Rand)))
        audioThread.Start()
    End Sub

End Class
