Public Class login2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("../login.aspx?redirectURL=personalCenter/default.aspx?at=pc")
    End Sub

End Class