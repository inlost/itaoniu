Public Class admin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("adminId") = Nothing) Then
            'Response.Redirect("login.aspx")
        End If
    End Sub

End Class