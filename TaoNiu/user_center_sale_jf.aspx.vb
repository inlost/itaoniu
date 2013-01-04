Public Class user_center_sale_jf
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If shop.SelectedIndex = -1 Then
            shop.SelectedIndex = 0
        End If
        If Request.Params("msg") = "ok" Then
            MsgBox("修改成功")
        ElseIf Request.Params("msg") = "error" Then
            MsgBox("修改失败，请检查输入，或者您是否是店主")
        End If
    End Sub
    Public Sub getForm()
        Dim strWite As String = ""
        strWite += "<input type='hidden' name='sid' value='" & shop.SelectedValue & "' />"
        Dim myMain As New main, mySale As New saleFunction
        strWite = myMain.userCenterSetOutInfo("tip", "请输入多少钱（整数）等于一个积分，单位（元）", strWite, False)
        strWite = myMain.userCenterSetOutInfo("钱数", "<input type='text' name='money' value='" & mySale.shopJf_get(shop.SelectedValue) & "' />", strWite, False)
        strWite = myMain.userCenterSetOutInfo("", "<input class='subBt' type='submit' name='infoSub' value ='提交' /><a class='subBt' href='user_center_sale.aspx?at=sale'>返回</a>", strWite, False)
        Response.Write(strWite)
    End Sub
End Class