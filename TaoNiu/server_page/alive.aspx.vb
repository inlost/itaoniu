Public Class alive
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Form("action") = "imOnLine" Then
            Try
                Convert.ToInt64(Session("id"))
                update()
            Catch ex As Exception
                Exit Sub
            End Try
        End If
    End Sub
    Private Sub update()
        Dim theUpdate As New Hashtable, myDb As New dbHelper
        theUpdate("@uid") = Session("id")
        theUpdate("@lastUpdate") = DateTime.Now.Minute.ToString
        Try
            myDb.noneQuery("user_steOnline", myDb.hashtableToInquery(theUpdate))
            Response.Write("ok")
        Catch ex As Exception
            Response.Write("error")
        End Try
    End Sub
End Class