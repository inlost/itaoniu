Public Class user_center_sale_pf
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If shop.SelectedIndex = -1 Then
            shop.SelectedIndex = 0
        End If
        If Request.Params("msg") = "error" Then
            MsgBox("操作失败，请按表单tip检查您的输入")
        End If
    End Sub
    Public Sub getPfForm()
        Dim strWite As String = "", myMain As New main
        strWite += "<input type='hidden' name='sid' value='" & shop.SelectedValue & "' />"
        strWite = myMain.userCenterSetOutInfo("关联商品", "<a id='choseGood' href='#'>点击选择商品</a>", strWite)
        strWite += "<input type='hidden' name='gid' value='-1' />"
        strWite = myMain.userCenterSetOutInfo("tip", "批发数量需为整数，批发价格需比零售价低", strWite, False)
        strWite = myMain.userCenterSetOutInfo("开始数量", "<input type='text' name='nb' />", strWite, False)
        strWite = myMain.userCenterSetOutInfo("批发价格", "<input type='text' name='price'", strWite, False)
        strWite = myMain.userCenterSetOutInfo("", "<input class='subBt' type='submit' name='infoSub' value ='提交' /><a class='subBt' href='user_center_sale.aspx?at=sale'>返回</a>", strWite, False)
        Response.Write(strWite)
    End Sub
    Public Sub getPfList()
        Dim pfs As Hashtable(), mySale As New saleFunction, myPage As New pageLink
        pfs = mySale.getPfList(shop.SelectedValue, IIf(Request.Params("page") = Nothing, 1, Request.Params("page")), 10)
        If pfs(0)("count") = 0 Then Exit Sub
        Dim strWite As String = "", strTp As String, myMain As New main, myGood As New goods, theGood As New Hashtable
        For Each pf As Hashtable In pfs
            theGood = myGood.goods_get(pf("gid"))
            If Len(theGood("goods_title")) >= 5 Then
                strTp = "[…" & Right(theGood("goods_title"), 8) & "]"
            Else
                strTp = "[" & theGood("goods_title") & "]"
            End If
            strTp += "[起批数量（" & pf("nb") & "）]" & "[零售价（" & theGood("goods_prize") & "）]" & "[批发价（" & pf("pf") & "）]"
            strTp += "<a class='subBt' href='server_page/sale_fun.aspx?action=pfDel&pfId=" & pf("id") & "&sid=" & shop.SelectedValue & "'>删除</a>"
            strWite = myMain.userCenterSetOutInfo("批发", strTp, strWite, False)
        Next
        strWite += myPage.getPageLink(pfs(0)("count"), 10, IIf(Request.Params("page") = Nothing, 1, Request.Params("page")), "?at=sale")
        Response.Write(strWite)
    End Sub
End Class