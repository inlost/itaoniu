Public Class login3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("../login.aspx?redirectURL=agent/default.aspx")
    End Sub

End Class