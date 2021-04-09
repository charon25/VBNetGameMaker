Imports System.Drawing

Public Class Vector

    Public X As Double
    Public Y As Double

    'Constructeurs
    Sub New(ByVal X As Double, ByVal Y As Double)
        Me.X = X
        Me.Y = Y
    End Sub
    Sub New(ByVal P As Point)
        Me.X = P.X
        Me.Y = P.Y
    End Sub
    Sub New(P As PointF)
        Me.X = P.X
        Me.Y = P.Y
    End Sub
    Sub New()

    End Sub

    'Opérations
    Public Shared Operator +(ByVal V1 As Vector, ByVal V2 As Vector) As Vector
        Return New Vector(V1.X + V2.X, V1.Y + V2.Y)
    End Operator
    Public Shared Operator -(ByVal V1 As Vector, ByVal V2 As Vector) As Vector
        Return New Vector(V1.X - V2.X, V1.Y - V2.Y)
    End Operator
    Public Shared Operator *(ByVal V As Vector, ByVal k As Double) As Vector
        Return New Vector(k * V.X, k * V.Y)
    End Operator
    Public Shared Operator *(ByVal V As Vector, ByVal k As Integer) As Vector
        Return V * CDbl(k)
    End Operator
    Public Shared Operator *(ByVal k As Double, ByVal V As Vector) As Vector
        Return V * k
    End Operator
    Public Shared Operator *(ByVal k As Integer, ByVal V As Vector) As Vector
        Return V * CDbl(k)
    End Operator
    Public Shared Operator /(ByVal V As Vector, ByVal k As Double) As Vector
        If k = 0 Then
            Throw New System.DivideByZeroException("Tentative de division par zéro.")
        Else
            Return New Vector(V.X / k, V.Y / k)
        End If
    End Operator
    Public Shared Operator /(ByVal V As Vector, ByVal k As Integer) As Vector
        Return V / CDbl(k)
    End Operator
    Public Shared Operator =(ByVal V1 As Vector, ByVal V2 As Vector) As Boolean
        Return V1.X = V2.X AndAlso V1.Y = V2.Y
    End Operator
    Public Shared Operator <>(ByVal V1 As Vector, ByVal V2 As Vector) As Boolean
        Return V1.X <> V2.X OrElse V1.Y <> V2.Y
    End Operator

    'Méthodes d'instance
    Public Overrides Function ToString() As String
        Return "(" + CStr(X) + " ; " + CStr(Y) + ")"
    End Function
    Public Function GetNorm() As Double
        Return Math.Sqrt(X * X + Y * Y)
    End Function
    Public Function ToUnitVector() As Vector
        If X = 0 AndAlso Y = 0 Then
            Return New Vector(0, 0)
        Else
            Return Me / GetNorm()
        End If
    End Function
    Public Function ToPoint() As Point
        Return New Point(X, Y)
    End Function
    Public Function ToPointF() As PointF
        Return New PointF(X, Y)
    End Function
    Public Sub Nullify(threshold As Double)
        If Math.Abs(X) < threshold Then
            X = 0
        End If
        If Math.Abs(Y) < threshold Then
            Y = 0
        End If
    End Sub

    'Méthode de classe
    Public Shared Function NullVector() As Vector
        Return New Vector(0, 0)
    End Function


End Class
