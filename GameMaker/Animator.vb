Imports System.Drawing

''' <summary>
''' Représente un gestionnaire d'animation qui permet d'automatiser la transition entre plusieurs sprites.
''' </summary>
''' <remarks></remarks>
Public Class Animator

    Private Sprites As Bitmap()
    Private Cooldown As Integer

    Private TimeLeft As Integer
    Private SpritesCount As Integer
    Private CurrentIndex As Integer
    Private IsAnimationGoing As Boolean

    'CONSTRUCTEUR
    ''' <summary>
    ''' Initialise une nouvelle instance de la classe Animator à l'aide d'un Array d'images et d'un cooldown.
    ''' </summary>
    ''' <param name="sprites">Array d'images constituant l'animation.</param>
    ''' <param name="cooldown">Nombre de ticks entre le changement d'image.</param>
    ''' <remarks></remarks>
    Sub New(sprites As Bitmap(), cooldown As Integer)
        Me.Sprites = sprites
        Me.SpritesCount = Me.Sprites.Length
        Me.Cooldown = cooldown
        Me.TimeLeft = Me.Cooldown
        Me.CurrentIndex = 0
        Me.IsAnimationGoing = 1
    End Sub

    'METHODES
    ''' <summary>
    ''' Fait progresser l'animation du nombre de ticks spécifié.
    ''' </summary>
    ''' <param name="tickCount">Nombre de ticks à faire passer (par défaut : 1).</param>
    ''' <remarks></remarks>
    Public Sub Age(Optional tickCount As Integer = 1)
        If IsAnimationGoing Then
            TimeLeft -= tickCount
            If TimeLeft <= 0 Then
                If CurrentIndex = SpritesCount - 1 Then
                    CurrentIndex = 0
                Else
                    CurrentIndex += 1
                End If
                TimeLeft = Cooldown
            End If
        End If
    End Sub
    ''' <summary>
    ''' Met l'animation en pause.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Pause()
        IsAnimationGoing = False
    End Sub
    ''' <summary>
    ''' Redémarre l'animation.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Unpause()
        IsAnimationGoing = True
    End Sub

    'FONCTIONS
    ''' <summary>
    ''' Renvoie la texture actuelle de l'animation.
    ''' </summary>
    ''' <returns>Texture de l'animation au temps actuel.</returns>
    ''' <remarks></remarks>
    Public Function GetCurrentSprite() As Bitmap
        Return Sprites(CurrentIndex)
    End Function
    ''' <summary>
    ''' Renvoie une valeur indiquant si l'animation est en pause ou non.
    ''' </summary>
    ''' <returns>True si l'animation est en pause, False sinon.</returns>
    ''' <remarks></remarks>
    Public Function IsPaused() As Boolean
        Return Not IsAnimationGoing
    End Function


End Class
