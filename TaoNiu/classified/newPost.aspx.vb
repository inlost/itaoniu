Public Class newPost
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("onTheWay") <> "loginSuccess") Then
            Response.Redirect("../login.aspx?edirectURL=classified/newPost.aspx")
        End If
    End Sub

End Class