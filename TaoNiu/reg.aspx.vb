Public Class reg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("onTheWay") = "reg"
    End Sub
    Public Sub setNav()
        Dim str_lilist As String
        If Request.Params("step") = 3 Then
            str_lilist = "<li class='fontNoAct'>1. 填写会员信息</li><li class='nearActiveLi'>2. 通过邮件确认</li><li id='steupActive' class='endAct'>3. 注册成功</li>"
        ElseIf Request.Params("step") = 2 Then
            str_lilist = "<li class='nearActiveLi fontNoAct'>1. 填写会员信息</li><li id='steupActive'>2. 通过邮件确认</li><li class='nearActiveLi endNoAct'>3. 注册成功</li>"
        Else
            str_lilist = "<li id='steupActive'>1. 填写会员信息</li><li>2. 通过邮件确认</li><li class='endNoAct'>3. 注册成功</li>"
        End If
        Response.Write(str_lilist)
    End Sub

End Class