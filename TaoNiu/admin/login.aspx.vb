Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("msg") = "vdcod_error" Then
            Response.Write("<script>alert('验证码错误')</script>")
        End If
        Try
            If (Session("admLoginTime") < 4) Then
                Session("admLoginTime") = Session("admLoginTime").ToString + 1
            Else
                btLogin.Enabled = False
            End If
        Catch ex As Exception
            Session("admLoginTime") = 1
        End Try
    End Sub

End Class