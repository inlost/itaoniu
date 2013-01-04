Public Class user_center_money_log
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub moneyLogGet()
        Dim strWite As String = "", strTp As String, myPageLink As New pageLink
        Dim pageNow As Integer = IIf(Request.Params("page") = Nothing, 1, Request.Params("page"))
        Dim myMoney As New tnPay.money, mLogs As Hashtable(), myMain As New main
        mLogs = myMoney.moneyLogGet(Session("id"), pageNow, 15)
        For Each mLog In mLogs
            strTp = "[时间：" & mLog("date") & "]"
            strTp += "[金额：" & mLog("money") & "]"
            strTp += "[" & mLog("remark") & "]"
            strWite = myMain.userCenterSetOutInfo(IIf(mLog("type") = "0", "收入", "支出"), strTp, strWite, False)
        Next
        strWite += myPageLink.getPageLink(myMoney.moneyLogCount(Session("id")), 15, pageNow, "user_center_money_log.aspx?at=money")
        Response.Write(strWite)
    End Sub
End Class