Public Class shopProduct
    Inherits System.Web.UI.Page
    Protected gShopInfo As Hashtable
    Protected gGoodsInfo As Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("shop") = Nothing Or Request.Params("gid") = Nothing Then
            Response.Redirect("default.aspx")
        End If
        Try
            Convert.ToInt32(Request.Params("shop"))
            Dim myShop As New shop
            gShopInfo = myShop.getShopInfo(Request.Params("shop"))
            Dim myGood As New goods
            gGoodsInfo = myGood.goods_get(Request.Params("gid"))
            If gGoodsInfo("status") = "error" Then Response.Redirect("shop.aspx?id=" & Request.Params("shop"))
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
                strWite += "<li><a href='shop.aspx?id=" & Request.Params("shop") & "'>店铺首页</a></li>"
                Dim shopShelf() As dbHelper.inQuery, myShop As New shop
                shopShelf = myShop.get_shelf_list(Request.Params("shop"))
                For Each shelf As dbHelper.inQuery In shopShelf
                    strWite += "<li><a href='shop.aspx?id=" & Request.Params("shop") & "&shelf=" & shelf.value & "'>" & shelf.name & "</a></li>"
                Next
                strWite += "</ul>"
            Case "style"
                Dim myMain As New main
                strWite = "shop/" & gShopInfo("shop_style")
        End Select
        Response.Write(strWite)
    End Sub
    Public Sub good_info(ByVal wGet As String, Optional ByVal cssId As String = "")
        Dim strWite As String = ""
        Select Case wGet
            Case "title"
                strWite = gGoodsInfo("goods_title")
            Case "buyBox"
                strWite = "<div id='" & IIf(Len(cssId) = 0, "buyBox", cssId) & "'>"
                strWite += "<div class='jqzoom'><img src='" & gGoodsInfo("goods_picture") & "' alt='scarpa' jqimg='' /></div>"
                Dim strTp As String = "", mySearch As New searchHelper
                strWite += "<ul>"
                If gGoodsInfo("goods_sale_type") <> mySearch.goods_cat_mSha_get Then
                    strTp = "一口价："
                Else
                    strTp = "秒杀："
                End If
                strWite += "<li>" & strTp & "<span id='price'>" & IIf(gGoodsInfo("goods_discount") > 0.1, gGoodsInfo("goods_prize") * gGoodsInfo("goods_discount") / 100, gGoodsInfo("goods_prize")) & "</span>元</li>"
                If (gGoodsInfo("goods_discount") > 0.1) Then
                    strWite += "<li>原价：<span id='oldPrice'><strike>" & gGoodsInfo("goods_prize") & "</strike></span>  折扣：" & gGoodsInfo("goods_discount") & "%</span></li>"
                End If
                strWite += "<li>送货范围：<span id='range'>" & gGoodsInfo("goods_range") & "</span><li>"
                strWite += "<li>店铺地址：</span id='address'>" & gShopInfo("shop_address") & "</li>"
                strWite += "</ul>"
                Try
                    If Session("onTheWay") = "loginSuccess" And Convert.ToInt32(Session("id")) > 0 Then
                        strTp = "Order.aspx"
                    Else
                        strTp = "login.aspx?redirectURL=shopProduct.aspx?shop=" & Request.Params("shop") & "----gid=" & Request.Params("gid")
                    End If
                Catch ex As Exception
                    strTp = "login.aspx?redirectURL=shopProduct.aspx?shop=" & Request.Params("shop") & "&gid=" & Request.Params("gid")
                End Try
                strWite += "<form id='order' method='POST' action='" & strTp & "'>"
                Dim timeStarSall As Date = gGoodsInfo("goods_on_sale_date")
                If Date.Now < timeStarSall Then
                    strWite += "<p id='subForm'>开售时间：" & gGoodsInfo("goods_on_sale_date") & "</p>"
                Else
                    strWite += "<input type='hidden' name='gid' value='" & gGoodsInfo("id") & "' />"
                    strWite += "<input type='hidden' name='shop' value='" & Request.Params("shop") & "' />"
                    strWite += "<p><span class='pTitle'>我要买：</span><input id='number' rel='" & gGoodsInfo("goods_number_now") & "' type='text' name='number' value='1' />&nbsp;件&nbsp;（库存：" & gGoodsInfo("goods_number_now") & "件）</p>"
                    If gGoodsInfo("goods_number_now") > 0 Then
                        Dim strGwc As String = "server_page/buy.aspx?shop=" & Request.Params("shop") & "&gid=" & Request.Params("gid") & "&action=gwc_add"
                        strWite += "<div id='fun-bt'><button id='buyIt' type='submit'>提交</button><a href='" & strGwc & "' id='toBox'>加入购物车</a><div class='clear'></div></div>"
                    End If
                    Dim buyService As String = "server_page/buy.aspx?shop=" & Request.Params("shop") & "&gid=" & Request.Params("gid") & "&action="
                    strWite += "<p id='shouCang'><a id='sc_good' href='" & buyService & "shouC_good'>收藏商品</a><a id='sc_shop' href='" & buyService & "shouC_shop'>收藏店铺</a></p>"
                End If
                strWite += "</form>"
                strWite += "</div>"
                Dim myTu As New trusteeships
                Dim good As New Hashtable, myGoods As New goods
                good = myGoods.goods_get(Request.Params("gid"))
                If myTu.isTuUser(Session("id"), good("shopId")) Then
                    strWite += "<div id='tuInfo'>"
                    strWite += good("goods_tu")
                    strWite += "</div>"
                End If
            Case "info"
                Dim good As New Hashtable, myGoods As New goods
                good = myGoods.goods_get(Request.Params("gid"))
                Dim strTp As String = good("goods_info")
                strWite = "<ul id='infoList'>"
                Dim info() As String = strTp.Split("-")
                For Each infoItem As String In info
                    strWite += "<li>"
                    strWite += Left(infoItem, infoItem.IndexOf("=")) & ":"
                    strWite += Right(infoItem, Len(infoItem) - infoItem.IndexOf("=") - 1)
                    strWite += "</li>"
                Next
                strWite += "<div class='clear'></div></ul>" & good("goods_introduce")
                strWite += "<div class='clear'></div>"
        End Select
        Response.Write(strWite)
    End Sub
    Public Sub getSideBar()
        Dim strWite As String = "", shop As New Hashtable, myShop As New shop, user As New Hashtable, myUser As New user, strTp As String
        Try
            shop = myShop.getShopInfo(Request.Params("shop"))
            User = myUser.getUserInfo(shop("shop_owner"))
            strWite = "<h2 class='sideTitle'>商家信息</h2>"
            strTp = User("qq")
            strTp = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTp & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTp & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
            strWite += "<ul id='ownerInfo'><li><span class='title'>商家名称：</span><span class='info'>" & shop("shop_name") & "</span></li>"
            strWite += "<li><span class='title'>地址：</span><span class='info'>" & shop("shop_address") & "</span><div class='clear'></div></li>"
            strWite += "<li><span class='title'>信用：</span><span class='info'>" & User("sale_pj_good") & "</span><div class='clear'></div></li>"
            strWite += "<li><span class='title'>客服：</span><span class='info'>" & strTp & "</span><div class='clear'></div></li></ul>"
            strWite += "<h2 class='sideTitle'>当前货架</h2><ul id='shlfGoods'>"
            Dim goods() As Hashtable, mySearch As New searchHelper, good As New Hashtable, myGoods As New goods
            good = myGoods.goods_get(Request.Params("gid"))
            goods = mySearch.goods_get_fromShelf(Request.Params("shop"), good("shelf"))
            For Each gooder As Hashtable In goods
                strWite += "<li>"
                strTp = "shopProduct.aspx?shop=" & gooder("shopId") & "&gid=" & gooder("id")
                strWite += "<a href='" & strTp & "'><img src='" & gooder("goods_picture") & "' /></a>"
                strWite += "<h3><span>￥</span>" & IIf(gooder("goods_discount") > 0, gooder("goods_prize") * gooder("goods_discount") * 0.01, gooder("goods_prize")) & "</h3>"
                strWite += "<h2><a href='" & strTp & "'>" & gooder("goods_title") & "</a></h2>"
                strWite += "</li>"
            Next
            strWite += "</ul>"
        Catch ex As Exception

        End Try
        Response.Write(strWite)
    End Sub
    Public Sub getGid()
        Response.Write(Request.Params("gid"))
    End Sub
End Class