Public Class user_center_sale_tg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If shop.SelectedIndex = -1 Then
            shop.SelectedIndex = 0
        End If
    End Sub
    Public Sub getTgForm()
        Dim strWite As String = "", myMain As New main
        strWite += "<input type='hidden' name='sid' value='" & shop.SelectedValue & "' />"
        strWite = myMain.userCenterSetOutInfo("关联商品", "<a id='choseGood' href='#'>点击选择商品</a>", strWite)
        strWite += "<input type='hidden' name='gid' value='-1' />"
        strWite = myMain.userCenterSetOutInfo("tip", "团购数量需为整数，团购价格需比零售价低", strWite, False)
        strWite = myMain.userCenterSetOutInfo("开团数量", "<input type='text' name='nb' />", strWite, False)
        strWite = myMain.userCenterSetOutInfo("团购价格", "<input type='text' name='price'", strWite, False)
        strWite = myMain.userCenterSetOutInfo("tip", "发布团购收取开团数量*团购价格*4%的手续费", strWite, False)
        strWite = myMain.userCenterSetOutInfo("tip", "团购商品每销售一件收取团购价格*2%的手续费", strWite, False)
        strWite = myMain.userCenterSetOutInfo("", "<input class='subBt' type='submit' name='infoSub' value ='提交' /><a class='subBt' href='user_center_sale.aspx?at=sale'>返回</a>", strWite, False)
        Response.Write(strWite)
    End Sub
    Public Sub getTgList()
        Dim tgs As Hashtable(), mySale As New saleFunction, myPage As New pageLink
        tgs = mySale.getTgList(Session("id"), IIf(Request.Params("page") = Nothing, 1, Request.Params("page")), 10)
        If tgs(0)("count") = 0 Then Exit Sub
        Dim strWite As String = "", strTp As String, myMain As New main, myGood As New goods, theGood As New Hashtable
        For Each tg As Hashtable In tgs
            theGood = myGood.goods_get(tg("gid"))
            If Len(theGood("goods_title")) >= 5 Then
                strTp = "[…" & Right(theGood("goods_title"), 6) & "]"
            Else
                strTp = "[" & theGood("goods_title") & "]"
            End If
            strTp += "[起团数量（" & tg("nb") & "）]" & "[零售价（" & theGood("goods_prize") & "）]" & "[团购价（" & tg("pf") & "）]"
            strTp += "<a class='subBt' href='tuan.aspx?Id=" & tg("id") & "'>查看</a>"
            strWite = myMain.userCenterSetOutInfo("团购", strTp, strWite, False)
        Next
        strWite += myPage.getPageLink(tgs(0)("count"), 10, IIf(Request.Params("page") = Nothing, 1, Request.Params("page")), "?at=sale")
        Response.Write(strWite)
    End Sub
End Class