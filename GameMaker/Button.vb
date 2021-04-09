Imports System.Drawing

Public Class Button

    Private Rect As Rectangle
    Private ActionSub As Action

    'CONSTRUCTEURS
    ''' <summary>
    ''' Initialise une instance de la classe Button avec le rectangle du bouton et la méthode à appeler en cas de clic.
    ''' </summary>
    ''' <param name="buttonRect">Rectangle représentant le bouton.</param>
    ''' <param name="action">Méthode (sans arguments) à appeler lors du clic.</param>
    ''' <remarks></remarks>
    Sub New(buttonRect As Rectangle, action As Action)
        Me.Rect = buttonRect
        Me.ActionSub = action
    End Sub
    ''' <summary>
    ''' Initialise une instance de la classe Button avec la position du coin supérieur gauche du bouton, sa taille et la méthode à appeler en cas de clic.
    ''' </summary>
    ''' <param name="cornerLocation">Coin supérieur gauche du bouton.</param>
    ''' <param name="size">Taille du bouton.</param>
    ''' <param name="action">Méthode (sans arguments) à appeler lors du clic.</param>
    ''' <remarks></remarks>
    Sub New(cornerLocation As Point, size As Size, action As Action)
        Me.Rect = New Rectangle(cornerLocation, size)
        Me.ActionSub = action
    End Sub

    'METHODES
    ''' <summary>
    ''' Appelle la méthode si les coordonnées sont à l'intérieur du bouton.
    ''' </summary>
    ''' <param name="mouseLocation">Position de la souris.</param>
    ''' <remarks></remarks>
    Public Sub Click(mouseLocation As Point)
        If Rect.Contains(mouseLocation) Then
            ActionSub()
        End If
    End Sub

    'FONCTIONS
    ''' <summary>
    ''' Indique si les coordonnées sont à l'intérieur du bouton.
    ''' </summary>
    ''' <param name="mouseLocation">Position de la souris.</param>
    ''' <returns>Renvoie True si la souris est sur le bouton, False sinon.</returns>
    ''' <remarks></remarks>
    Public Function IsClicking(mouseLocation As Point)
        Return Rect.Contains(mouseLocation)
    End Function

End Class
