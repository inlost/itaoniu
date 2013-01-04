Public Class user_center_sale_zq
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
        Dim strWite As String = "", strTp As String = "", myMain As New main
        strWite += "<input type='hidden' name='sid' value='" & shop.SelectedValue & "' />"
        strTp = "<select name='type' id='shareType'>"
        strTp += "<option value='all'>全店</option>"
        strTp += "<option value='goods'>特定商品</option>"
        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("赠券产生类型", strTp, strWite)
        strWite = myMain.userCenterSetOutInfo("关联商品", "<a id='choseGood' href='#'>点击选择商品</a>", strWite)
        strWite += "<input type='hidden' name='gid' value='-1' />"
        strTp = "<select name='timeLong'>"
        For i = 5 To 90
            strTp += "<option value='" & i & "'>" & i & "</option>"
        Next
        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("有效期(天)", strTp, strWite)
        strTp = "消费<input type='text' name='xf' class='input_w' />(整)元，可得<input type='text' name='je'class='input_w' />（整）元赠券一张"
        strWite = myMain.userCenterSetOutInfo("赠券公式", strTp, strWite, False)
        strWite = myMain.userCenterSetOutInfo("", "<input class='subBt' type='submit' name='infoSub' value ='提交' /><a class='subBt' href='user_center_sale.aspx?at=sale'>返回</a>", strWite, False)
        Response.Write(strWite)
    End Sub
    Public Sub getLogs()
        Dim strWite As String = "", strTp As String = "", myMain As New main
        Dim mySale As New saleFunction, myPage As New pageLink
        Dim theLogs As Hashtable() = mySale.getZqList(shop.SelectedValue, IIf(Request.Params("page") = Nothing, 1, Request.Params("page")), 10)
        If theLogs(0)("count") = 0 Then Exit Sub
        Dim zq As dbHelper.inQuery, myGoods As New goods, theGood As New Hashtable
        For Each log In theLogs
            strTp = log("zq")
            zq = mySale.zqSpit(strTp)
            If log("gid") <> -1 Then
                theGood = myGoods.goods_get(log("gid"))
                If Len(theGood("goods_title")) >= 5 Then
                    strTp = "[商品赠券][…" & Right(theGood("goods_title"), 5) & "]"
                Else
                    strTp = "[商品赠券][" & theGood("goods_title") & "]"
                End If
            Else
                strTp = "[全店赠券]"
            End If
            strTp += "赠券公式：消费（" & zq.name & ")元，赠（" & zq.value & ")元券"
            strTp += "<a class='subBt' href='server_page/sale_fun.aspx?action=zqDel&zqId=" & log("id") & "&sid=" & shop.SelectedValue & "'>删除</a>"
            strWite = myMain.userCenterSetOutInfo("赠券:", strTp, strWite, False)
        Next
        strWite += myPage.getPageLink(theLogs(0)("count"), 10, IIf(Request.Params("page") = Nothing, 1, Request.Params("page")), "?at=sale")
        Response.Write(strWite)
    End Sub
End Class