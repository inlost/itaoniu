Public Class Site_front
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getFunLeft()
        Dim strWite As String, myMain As New main
        If Session("onTheWay") <> "loginSuccess" Then
            strWite = "您好，欢迎来到<a href='" & myMain.siteUrl & "'>" & myMain.siteName & "</a>!&nbsp;<a style='color:red;' href='default.aspx' target='_parent' >首页</a>&nbsp;<a href='login.aspx?redirectURL=default.aspx'>请登录</a>&nbsp;<a href='reg.aspx'>免费注册</a>"
        Else
            strWite = "您好," & Session("niceName") & "!&nbsp;<a style='color:red;' href='default.aspx'>首页</a>&nbsp;<a href='user_center.aspx'>我的用户中心</a>&nbsp;<a href='server_page/user_service.aspx?action=quit'>退出</a>"
        End If
        Response.Write(strWite)
    End Sub
    Public Sub getFunRight()
        Dim strWite As String
        Dim gwcCount As String
        Try
            gwcCount = Request.Cookies("gwc")("count")
        Catch ex As Exception
            gwcCount = ""
        End Try
        strWite = "<a id='gwc' href='cart.aspx'>购物车<span>" & gwcCount & "</span></a>"
        Response.Write(strWite)
    End Sub
End Class