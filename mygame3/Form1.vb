Public Class Form1
    Function Collision(p As String, t As String, Optional ByRef other As Object = vbNull)
        For Each c In Controls
            If c.name.toupper = p.ToUpper Then
                Return Collision(c, t, other)
            End If
        Next
        Return False
    End Function
    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall","wall2","wall3","wall4","wall5","wall6","wall7","wall8","wall9","wall10","wall11","wall12","wall13","wall14","wall15","wall16","wall17","wall18","wall19","wall20","wall21","wall22","wall23","wall24","wall25","wall26","wall27","wall28","wall29","wall30","wall31","wall32","wall33","wall34","wall35","wall36","wall37","wall38","wall39","wall40","wall41", and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If
        Dim other As Object = Nothing
        If Collision("picturebox1", "WIN", other) Then

            Me.BackColor = Color.Green
            other.visible = True
            Timer1.Enabled = False
            Dim f As New Form3
            f.ShowDialog()
            Return
        End If
        If Collision("zombie1", "picturebox1", other) Then
            Timer1.Enabled = False
            Dim f As New Form2
            Me.BackColor = Color.Red
            f.ShowDialog()
        End If


    End Sub

    Sub MoveTo(p As String, distx As Integer, disty As Integer)
        For Each c In Controls
            If c.name.toupper = p.ToUpper Then
                MoveTo(c, distx, disty)
            End If
        Next
    End Sub
    Sub CreateNew(name As String, pic As PictureBox, location As Point)
        Dim p As New PictureBox
        p.Location = location
        p.Image = pic.Image
        p.Name = name
        p.Width = pic.Width
        p.Height = pic.Height
        p.SizeMode = PictureBoxSizeMode.StretchImage
        Controls.Add(p)

    End Sub
    Public Sub chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > picturebox1.Location.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < picturebox1.Location.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(p, x, y)
    End Sub
    Sub follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(picturebox1.Location)
        headstart = headstart + 1
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MoveTo("bullet", 20, 0)
        follow(zombie1)


    End Sub





    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.W
                MoveTo("Picturebox1", 0, -5)
            Case Keys.S
                MoveTo("Picturebox1", 0, 5)
            Case Keys.A
                MoveTo("Picturebox1", -5, 0)
            Case Keys.D
                MoveTo("Picturebox1", 5, 0)
            Case Keys.Space
                CreateNew("Bullet", bullet, picturebox1.Location)
            Case Else

        End Select
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles zombie1.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox48_Click(sender As Object, e As EventArgs) Handles wall21.Click

    End Sub

    Private Sub wall26_Click(sender As Object, e As EventArgs) Handles wall26.Click

    End Sub
End Class
