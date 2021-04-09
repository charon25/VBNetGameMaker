Imports System.Text.RegularExpressions, System.Drawing

''' <summary>
''' Contient des fonctions utiles.
''' </summary>
''' <remarks></remarks>
Public Class Functions

    ''' <summary>
    ''' Fonctions générales.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Misc

        ''' <summary>
        ''' Fonction étendant String.Split() permettant d'utiliser des chaînes comme séparation.
        ''' </summary>
        ''' <param name="text">Chaîne à découper.</param>
        ''' <param name="separator">Séparateur (peut contenir plusieurs caractères).</param>
        ''' <returns>Renvoie un System.Array de String contenant les morceaux de la chaîne.</returns>
        ''' <remarks></remarks>
        Public Shared Function SplitString(text As String, separator As String) As String()
            Return Regex.Split(text, separator)
        End Function
        ''' <summary>
        ''' Renvoie un élément au hasard d'une liste d'objets.
        ''' </summary>
        ''' <param name="list">Liste source.</param>
        ''' <param name="rand">Instance de la classe System.Random.</param>
        ''' <returns>Un élément au hasard de la liste source.</returns>
        ''' <remarks></remarks>
        Public Shared Function GetRandomElementOfList(ByRef list As IEnumerable(Of Object), rand As Random) As Object
            Return list(rand.Next(list.Count))
        End Function

    End Class

    ''' <summary>
    ''' Fonctions concernant des actions graphiques.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Images

        ''' <summary>
        ''' Renvoi un Array de Bitmap consistant en un découpage vertical de l'image donnée selon la largeur spécifiée.
        ''' </summary>
        ''' <param name="spritesheet">Image contenant les sprites les uns à la suite des autres le ligne d'une seule ligne.</param>
        ''' <param name="spriteWidth">Largeur d'un sprite.</param>
        ''' <returns>System.Array contenant les sprites.</returns>
        ''' <remarks></remarks>
        Public Shared Function SplitSprites1DWidth(spritesheet As Bitmap, spriteWidth As Integer) As Bitmap()
            Dim spriteHeight As Integer = spritesheet.Height
            Dim N As Integer = spritesheet.Width / spriteWidth
            Dim output(N - 1) As Bitmap
            Dim g As Graphics
            For i As Integer = 0 To N - 1
                output(i) = New Bitmap(spriteWidth, spriteHeight)
                g = Graphics.FromImage(output(i))
                g.DrawImage(spritesheet, New Rectangle(0, 0, spriteWidth, spriteHeight), New Rectangle(i * spriteWidth, 0, spriteWidth, spriteHeight), GraphicsUnit.Pixel)
            Next
            Return output
        End Function

        ''' <summary>
        ''' Renvoi un Array de Bitmap consistant en un découpage horizontal de l'image donnée selon la hauteur spécifiée.
        ''' </summary>
        ''' <param name="spritesheet">Image contenant les sprites les uns à la suite des autres le ligne d'une seule ligne.</param>
        ''' <param name="spriteHeight">Hauteur d'un sprite.</param>
        ''' <returns>System.Array contenant les sprites.</returns>
        ''' <remarks></remarks>
        Public Shared Function SplitSprites1DHeight(spritesheet As Bitmap, spriteHeight As Integer) As Bitmap()
            Dim spriteWidth As Integer = spritesheet.Width
            Dim N As Integer = spritesheet.Height / spriteHeight
            Dim output(N - 1) As Bitmap
            Dim g As Graphics
            For j As Integer = 0 To N - 1
                output(j) = New Bitmap(spriteWidth, spriteHeight)
                g = Graphics.FromImage(output(j))
                g.DrawImage(spritesheet, New Rectangle(0, 0, spriteWidth, spriteHeight), New Rectangle(0, j * spriteHeight, spriteWidth, spriteHeight), GraphicsUnit.Pixel)
            Next
            Return output
        End Function

        ''' <summary>
        ''' Renvoie un Array de Bitmap consistant en un découpage dans les deux directions de l'image donnée selon la largeur et la hauteur spécifiées.
        ''' </summary>
        ''' <param name="spritesheet">Image contenant les sprites.</param>
        ''' <param name="spriteWidth">Largeur d'un sprite.</param>
        ''' <param name="spriteHeight">Hauteur d'un sprite.</param>
        ''' <returns>System.Array à deux dimensions contenant les sprites.</returns>
        ''' <remarks></remarks>
        Public Shared Function SplitSprites2D(spritesheet As Bitmap, spriteWidth As Integer, spriteHeight As Integer) As Bitmap(,)
            Dim Nx As Integer = spritesheet.Width / spriteWidth
            Dim Ny As Integer = spritesheet.Height / spriteHeight
            Dim output(Nx - 1, Ny - 1) As Bitmap
            Dim g As Graphics
            For i As Integer = 0 To Nx - 1
                For j As Integer = 0 To Ny - 1
                    output(i, j) = New Bitmap(spriteWidth, spriteHeight)
                    g = Graphics.FromImage(output(i, j))
                    g.DrawImage(spritesheet, New Rectangle(0, 0, spriteWidth, spriteHeight), New Rectangle(i * spriteWidth, j * spriteHeight, spriteWidth, spriteHeight), GraphicsUnit.Pixel)
                Next
            Next
            Return output
        End Function

    End Class

    ''' <summary>
    ''' Fonctions concernant des collisions.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Collision

        ''' <summary>
        ''' Indique si deux rectangles se touchent ou non.
        ''' </summary>
        ''' <param name="rect1">Premier rectangle.</param>
        ''' <param name="rect2">Second rectangle.</param>
        ''' <returns>Renvoie True si les deux ont une intersection, False sinon.</returns>
        ''' <remarks></remarks>
        Public Shared Function RectangleRectangle(rect1 As Rectangle, rect2 As Rectangle) As Boolean
            Return Not ((rect1.Right < rect2.Left) OrElse (rect1.Bottom < rect2.Top) OrElse (rect1.Left > rect2.Right) OrElse (rect1.Top > rect2.Bottom))
        End Function

        ''' <summary>
        ''' Indique si deux rectangles, donnés par leurs coins supérieurs gauches et tailles, se touchent ou non.
        ''' </summary>
        ''' <param name="corner1">Coin supérieur gauche du premier rectangle.</param>
        ''' <param name="size1">Taille du premier rectangle.</param>
        ''' <param name="corner2">Coin supérieur gauche du second rectangle.</param>
        ''' <param name="size2">Taille du second rectangle.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RectangleRectangle(corner1 As PointF, size1 As Size, corner2 As PointF, size2 As Size) As Boolean
            Return Not ((corner1.X + size1.Width < corner2.X) OrElse (corner1.Y + size1.Height < corner2.Y) OrElse (corner1.X > corner2.X + size2.Width) OrElse (corner1.Y > corner2.Y + size2.Height))
        End Function

        ''' <summary>
        ''' Indique si un point est à l'intérieur d'un rectangle.
        ''' </summary>
        ''' <param name="rect">Rectangle considéré.</param>
        ''' <param name="p">Point.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RectanglePoint(rect As Rectangle, p As PointF)
            Return Not (p.X < rect.X OrElse p.X > rect.X + rect.Width OrElse p.Y < rect.Y OrElse p.Y > rect.Y + rect.Height)
        End Function

        ''' <summary>
        ''' Indique si un point est à l'intérieur d'un rectangle donné par son coin supérieur gauche et sa taille
        ''' </summary>
        ''' <param name="corner">Coin supérieur gauche du rectangle considéré.</param>
        ''' <param name="size">Taille du rectangle considéré.</param>
        ''' <param name="p">Point</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RectanglePoint(corner As Point, size As Size, p As PointF)
            Return Not (p.X < corner.X OrElse p.X > corner.X + size.Width OrElse p.Y < corner.Y OrElse p.Y > corner.Y + size.Height)
        End Function

    End Class

End Class
