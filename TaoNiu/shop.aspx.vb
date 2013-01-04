Public Class shop1
    Inherits System.Web.UI.Page
    Protected gShopInfo As Hashtable
    Protected gShopShelf() As dbHelper.inQuery
    Protected itemTotle As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("id") = Nothing Then
            Response.Redirect("default.aspx")
        End If
        Try
            Convert.ToInt32(Request.Params("id"))
            Dim myShop As New shop
            gShopInfo = myShop.getShopInfo(Request.Params("id"))
        Catch ex As Exception
            Response.Redirect("default.aspx")
        End Try
    End Sub
    Public Sub shoop_Info(ByVal wGet As String)
        Dim strWite As String = ""
        Select Case wGet
            Case "name"
                strWite = gShopInfo("shop_name")
            Case "introduce"
                strWite = gShopInfo("shop_introduce")
            Case "link"
                strWite = "shop.aspx?id=" & Request.Params("shop")
            Case "nav"
                strWite = "<ul id='shop_style' class='afterClear'>"
                strWite += "<li><a href='shop.aspx?id=" & Request.Params("id") & "'>店铺首页</a></li>"
                Dim shopShelf() As dbHelper.inQuery, myShop As New shop
                shopShelf = myShop.get_shelf_list(Request.Params("id"))
                gShopShelf = shopShelf
                For Each shelf As dbHelper.inQuery In shopShelf
                    strWite += "<li><a href='shop.aspx?id=" & Request.Params("id") & "&shelf=" & shelf.value & "'>" & shelf.name & "</a></li>"
                Next
                strWite += "<li><a href='http://bbs.itaoniu.com/guanggao/list-1.aspx'>做广告</a></li>"
                strWite += "</ul>"
            Case "style"
                Dim myMain As New main
                strWite = "shop/" & gShopInfo("shop_style")
        End Select
        Response.Write(strWite)
    End Sub
    Public Sub getSideBar()
        Dim strWite As String = "", shop As New Hashtable, myShop As New shop, user As New Hashtable, myUser As New user, strTp As String
        shop = myShop.getShopInfo(Request.Params("id"))
        user = myUser.getUserInfo(shop("shop_owner"))
        strWite = "<h2 class='sideTitle'>商家信息</h2>"
        strTp = user("qq")
        strTp = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTp & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTp & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
        strWite += "<ul id='ownerInfo'><li><span class='title'>商家名称：</span><span class='info'>" & shop("shop_name") & "</span></li>"
        strWite += "<li><span class='title'>地址：</span><span class='info'>" & shop("shop_address") & "</span><div class='clear'></div></li>"
        strWite += "<li><span class='title'>信用：</span><span class='info'>" & user("sale_pj_good") & "</span><div class='clear'></div></li>"
        strWite += "<li><span class='title'>客服：</span><span class='info'>" & strTp & "</span><div class='clear'></div></li></ul>"
        strWite += "<h2 class='sideTitle'>掌柜推荐</h2><ul id='shlfGoods'>"
        Dim goods() As Hashtable, mySearch As New searchHelper, myGoods As New goods
        goods = mySearch.goods_get_fromShopTui(Request.Params("id"))
        For Each gooder As Hashtable In goods
            strWite += "<li>"
            strTp = "shopProduct.aspx?shop=" & gooder("shopId") & "&gid=" & gooder("id")
            strWite += "<a href='" & strTp & "'><img src='" & gooder("goods_picture") & "' /></a>"
            strWite += "<h3><span>￥</span>" & IIf(gooder("goods_discount") > 0, gooder("goods_prize") * gooder("goods_discount") * 0.01, gooder("goods_prize")) & "</h3>"
            strWite += "<h2><a href='" & strTp & "'>" & gooder("goods_title") & "</a></h2>"
            strWite += "</li>"
        Next
        strWite += "</ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getShelfList()
        Dim strWite As String = "", mySearch As New searchHelper, goods() As Hashtable, strTp As String
        For Each shelf As dbHelper.inQuery In gShopShelf
            goods = mySearch.goods_get_fromShelf(Request.Params("id"), shelf.value, 6)
            If goods(0)("count") <> 0 Then
                strWite += "<div class='shelf'><h2>" & shelf.name & "</h2>"
                strWite += "<ul class='shelfGoods'>"
                For Each gooder As Hashtable In goods
                    strWite += "<li>"
                    strTp = "shopProduct.aspx?shop=" & gooder("shopId") & "&gid=" & gooder("id")
                    strWite += "<a href='" & strTp & "'><img src='" & gooder("goods_picture") & "' /></a>"
                    strWite += "<h3 class='prize'><span>￥</span>" & IIf(gooder("goods_discount") > 0, gooder("goods_prize") * gooder("goods_discount") * 0.01, gooder("goods_prize")) & "</h3>"
                    strWite += "<h3 class='goodsTitle'><a href='" & strTp & "'>" & gooder("goods_title") & "</a></h3>"
                    strWite += "</li>"
                Next
                strWite += "<div class='clear'></div></ul></div>"
            End If
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getShelfList(ByVal shelfId As Integer)
        Dim strWite As String = "", mySearch As New searchHelper, goods() As Hashtable, strTp As String
        goods = mySearch.goods_get_fromShelf(Request.Params("id"), shelfId, 12, IIf(Request.Params("page") = Nothing, 1, Request.Params("page")))
        If goods(0)("count") <> 0 Then
            itemTotle = goods(0)("count")
            strWite += "<div class='shelf'><h2>" & "当前货架下的商品" & "</h2>"
            strWite += "<ul class='shelfGoods'>"
            For Each gooder As Hashtable In goods
                strWite += "<li>"
                strTp = "shopProduct.aspx?shop=" & gooder("shopId") & "&gid=" & gooder("id")
                strWite += "<a href='" & strTp & "'><img src='" & gooder("goods_picture") & "' /></a>"
                strWite += "<h3 class='prize'><span>￥</span>" & IIf(gooder("goods_discount") > 0, gooder("goods_prize") * gooder("goods_discount") * 0.01, gooder("goods_prize")) & "</h3>"
                strWite += "<h3 class='goodsTitle'><a href='" & strTp & "'>" & gooder("goods_title") & "</a></h3>"
                strWite += "</li>"
            Next
            strWite += "<div class='clear'></div></ul></div>"
        End If
        Response.Write(strWite)
    End Sub
    Public Sub getPageLink()
        Dim pageHelper As New pageLink
        Dim strWite As String = "?id=" & Request.Params("id") & "&shelf=" & Request.Params("shelf")
        strWite = pageHelper.getPageLink(itemTotle, 12, Request.Params("page"), strWite)
        Response.Write(strWite)
    End Sub
    Public Sub getLianMengAdd()
        If Session("onTheWay") <> "loginSuccess" Then Exit Sub
        Dim myShop As New shop
        If myShop.hasShop(Session("id")) = False Then Exit Sub
        Dim strWite As String
        strWite = "<a href='user_center_lianMeng.aspx?at=tools&action=add&shop=" & Request.Params("id") & "'>添加为联盟店铺</a>"
        Response.Write(strWite)
    End Sub
    Protected Sub getLianMengList()
        Dim strWite As String = "", myShop As New shop
        Dim lianMengId As Hashtable()
        lianMengId = myShop.lianMengGet(Request.Params("id"))
        If lianMengId(0)("count") = 0 Then
            Response.Write("没有联盟店铺")
            Exit Sub
        End If
        Dim strTp As String = "", shop As New Hashtable
        For Each shopId As Hashtable In lianMengId
            shop = myShop.getShopInfo(shopId("unionId"))
            strWite += "<li>"
            strTp = "<a href='shop.aspx?id=" & shop("id") & "' title='" & shop("shop_name") & "'>"
            If shop("shop_pic") <> "no" Then
                strWite += "<div class='inner'>" & strTp & "<img class='lmImg' src='" & shop("shop_pic") & "' title='" & shop("shop_name") & "' rel='" & shop("shop_name") & "'/></a></div>"
            End If
            strWite += "<div class='inner'>" & strTp & shop("shop_name") & "</a></div>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
End Class