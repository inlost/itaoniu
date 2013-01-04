Public Class user_center_pc
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("onTheWay") = Nothing Or Session("onTheWay") <> "loginSuccess" Then
            Response.Redirect("login.aspx")
        End If
    End Sub

End Class