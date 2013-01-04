Public Class user_center_money
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("msg") <> "" Then
            If Request.Params("msg") = "ok" Then
                MsgBox("操作成功")
            ElseIf Request.Params("msg") = "error" Then
                MsgBox("操作失败，请检查输入是否正确和账户的可用余额")
            ElseIf Request.Params("msg") = "need" Then
                MsgBox("账户可用余额不足，请充值，本次操作需账户可用余额：" & Request.Params("price") & "元")
            End If
        End If
    End Sub
    Public Sub getMoneyInfo()
        Dim uid As Integer = Session("id")
        Dim myMoney As New tnPay.money
        Dim theMoney As New Hashtable
        theMoney = myMoney.getMoney(uid)
        Dim strWite As String = "", myMain As New main
        strWite = myMain.userCenterSetOutInfo("账户余额：", theMoney("money") + theMoney("wait_money"), strWite)
        strWite = myMain.userCenterSetOutInfo("可用余额：", theMoney("money"), strWite)
        strWite = myMain.userCenterSetOutInfo("正在提现：", myMoney.waitSendMoney(uid), strWite)
        Response.Write(strWite)
    End Sub
    Public Sub getMoneyMoveForm()
        Dim strWite As String = "", myMain As New main
        strWite = myMain.userCenterSetOutInfo("tip", "产生1.2%的手续费", strWite, False)
        strWite = myMain.userCenterSetOutInfo("支付宝", "<input type='text' name='payId' />", strWite, False)
        strWite = myMain.userCenterSetOutInfo("提取金额", "<input type='text' name='money' />（整数）", strWite, False)
        strWite = myMain.userCenterSetOutInfo("", "<input class='subBt' type='submit' name='infoSub' value ='提交' />", strWite)
        Response.Write(strWite)
    End Sub
    Public Sub getMoneyAdd()
        Dim strWite As String = "", myMain As New main
        strWite = myMain.userCenterSetOutInfo("tip", "充值时请选择即时到账交易，否则可能会造成充值失败", strWite, False)
        strWite = myMain.userCenterSetOutInfo("充值金额", "<input type='text' name='money' />（整数）", strWite, False)
        strWite = myMain.userCenterSetOutInfo("", "<input class='subBt' type='submit' name='infoSub' value ='提交' />", strWite)
        Response.Write(strWite)
    End Sub
End Class