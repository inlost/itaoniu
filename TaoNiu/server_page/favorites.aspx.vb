Public Class favorites1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("action") <> "del" Or Len(Request.Params("fid")) < 1 Or Session("onTheWay") <> "loginSuccess" Then
            Response.Redirect("../default.aspx")
        End If
        Dim myFavorite As New favorites
        myFavorite.del(Session("id"), Request.Params("fid"))
        If Len(Request.Params("redirectURL")) < 1 Then
            Response.Redirect("../default.aspx")
        Else
            Response.Redirect(Request.Params("redirectURL"))
        End If
    End Sub
End Class