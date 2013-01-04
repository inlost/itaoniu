Public Class login1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("onTheWay") = "login"
        If Request.Params("msg") <> Nothing Then
            Response.Write("<script>alert('" & Request.Params("msg") & "')</script>")
        End If
    End Sub
    Public Sub getRedirectURL()
        If Request.Params("redirectURL") = Nothing Then Exit Sub
        Dim strWite As String = "<input type='hidden' name='redirectURL' value='"
        strWite += IIf(Request.Params("redirectURL") = Nothing, "", Request.Params("redirectURL"))
        strWite += "' />"
        Response.Write(strWite)
    End Sub
End Class