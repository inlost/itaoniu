Public Class user_center_orders
    Inherits System.Web.UI.Page
    Public itemTotle As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("msg") <> Nothing Then
            Select Case Request.Params("msg")
                Case "okerror"
                    Response.Write("<script>alert('未能与服务器交换数据')</script>")
                Case "need"
                    Response.Write("<script>alert('有几笔交易正在等待你的评价哦，请对已经完成的交易进行评价，再购买新的商品。')</script>")
            End Select
        End If
        If Request.Form("payIt") = "yes" Then
            Dim payorder As New tnPay.orde
            Dim orderpay As New Hashtable
            orderpay = buitPayOrder(Request.Form("oid"))
            Dim strwite As String = payorder.add(orderpay)
            strwite = strwite.Replace("'", """")
            Dim struri = "../paysystem/payto.aspx?fm=" & strwite
            Response.Redirect(struri)
        End If
    End Sub
    Public Sub getOrderById()
        Dim order As New Hashtable, myOrder As New orders, strWite As String = ""
        If myOrder.hasOrder(Request.Params("id"), Session("id")) = False Then Exit Sub
        order = myOrder.order_get_byId(Request.Params("id"))
        If order("statu") = "ok" Then
            Dim myMain As New main, saller As New Hashtable, myUser As New user, shop As New Hashtable, myShop As New shop
            Dim strTp As String, good As New Hashtable, myGoods As New goods
            good = myGoods.goods_get(order("good"))
            shop = myShop.getShopInfo(order("shop"))
            saller = myUser.getUserInfo(order("ownerId"))
            strWite = myMain.userCenterSetOutInfo("商品：", "<a href='shopProduct.aspx?shop=" & shop("id") & "&gid=" & order("good") & "'>" & good("goods_title") & "</a>", strWite, False)
            strTp = "<div style='float:left;width:200px;text-align:center;'><img style='height:100px;width:100px;' src='" & good("goods_picture") & "' /></div>"
            strWite += strTp
            strTp = saller("qq")
            strTp = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTp & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTp & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
            strWite = myMain.userCenterSetOutInfo("卖家：", saller("real_name") & strTp, strWite)
            strWite = myMain.userCenterSetOutInfo("店铺：", "<a href='../shop.axp?id=" & order("shop") & "'>" & shop("shop_name") & "</a>", strWite)
            strWite = myMain.userCenterSetOutInfo("下单时间：", order("time"), strWite)
            strWite = myMain.userCenterSetOutInfo("收货人：", order("rec_name"), strWite)
            strWite = myMain.userCenterSetOutInfo("联系电话：", order("rec_phone"), strWite)
            If order("status") = 0 Then
                strTp = "已经通知卖家送货"
            ElseIf order("status") = 1 Then
                strTp = "卖家已经发货"
            ElseIf order("status") = 2 Then
                strTp = "交易成功"
            ElseIf order("status") = 3 Then
                strTp = "投诉调查中"
            ElseIf order("status") = 4 Then
                strTp = "投诉已经解决"
            End If
            strWite = myMain.userCenterSetOutInfo("订单状态：", strTp, strWite)
            strWite = myMain.userCenterSetOutInfo("收货地址：", order("rec_address"), strWite, False)
            If Request.Params("action") = Nothing Then
                strWite += "<a class='subLink' href='?at=orders&action=order&id=" & order("id") & "' >订单操作（投诉、评价、确认收货）</a>"
            End If
            strWite += "<br/>"
            Response.Write(strWite)
        End If
    End Sub
    Public Sub getOrders(ByVal isComp As Boolean)
        Dim myOrder As New orders
        Dim orders() As Hashtable
        Dim strWite As String = "", myMain As New main, strTp As String = ""
        If Request.Params("page") = Nothing Then
            orders = myOrder.order_get_byBuyer(Session("id"), isComp)
        Else
            orders = myOrder.order_get_byBuyer(Session("id"), isComp, Request.Params("page"))
        End If
        itemTotle = orders(0)("count")
        If itemTotle = 0 Then Exit Sub
        Dim myGood As New goods, good As New Hashtable, status As New Hashtable, prices As String
        Dim myUser As New user, myShop As New shop
        Dim theUser As New Hashtable, strTpe As String = ""
        For Each Order As Hashtable In orders
            good = myGood.goods_get(Order("good"))
            status = myOrder.getOrderStatusAsString(Order)
            strTp = "<li>"
            strTp += "<div class='listTitle'><span class='order-id'>订单编号：" & Order("id") & "</span><span class='order-time'>下单时间：" & Order("time") & "</span><div>"
            strTp += "<div class='listInner'><div class='order-goods'><img src='" & good("goods_picture") & "'/></div>"
            strTp += "<div class='order-title'>" & good("goods_title") & "</div>"
            prices = IIf(good("goods_discount") > 0.1, good("goods_prize") * good("goods_discount") / 100, good("goods_prize"))
            strTp += "<div class='order-price'>价格：" & IIf(Order("price") = 0, prices, Order("price")) & "</div>"

            strTp += "<div class='order-fun'><ul>"
            If Order("payMent") = 1 Then
                strTp += "<li><form method='POST' action='user_center_orders.aspx?at=orders'>"
                strTp += "<input type='hidden' name='payIt' value='yes' />"
                strTp += "<input type='hidden' name='shop' value='" & Order("shop") & "' />"
                strTp += "<input type='hidden' name='gid' value='" & Order("good") & "' />"
                strTp += "<input type='hidden' name='number' value='" & Order("number") & "' />"
                strTp += "<input type='hidden' name='name' value='" & Order("rec_name") & "' />"
                strTp += "<input type='hidden' name='address' value='" & Order("rec_address") & "' />"
                strTp += "<input type='hidden' name='phone' value='" & Order("rec_phone") & "' />"
                strTp += "<input type='hidden' name='oid' value='" & Order("id") & "' />"
                strTp += "<input type='submit'class='subBt' value='支付订单' id='post_pay' />"
                strTp += "</form></li>"
            Else
                strTp += "<li>订单状态" & status("status") & "</li>"
            End If

            strTp += "<li><a class='order-fun_red' href='user_center_orders.aspx?at=orders&id=" & Order("id") & "&action=fun'>投诉维权</a></li>"
            strTp += "<li><a class='order-fun_blue' href='user_center_orders.aspx?at=orders&id=" & Order("id") & "&action=fun'>订单详情</a>"
            strTp += "<li><a class='order-fun_green' href='user_center_orders.aspx?at=orders&id=" & Order("id") & "&action=fun'>订单评价</a>"
            strTp += "</ul></div>"

            theUser = myUser.getUserInfo(Order("ownerId"))
            strTpe = theUser("qq")
            strTpe = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTpe & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTpe & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
            strTp += "<div class='order-call'><div class='the-qq'>" & strTpe & "</div><div calss='the-address'>" & myShop.getShopInfo(Order("shop"))("shop_address") & "</div></div>"

            strTp += "<div class='order-span'></div>"
            strTp += "</div>"
            strTp += "</li>"
            strWite += strTp
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getPageLink()
        Dim strWite As String = "<ul id='pageLink'>", perPage As Integer = 10
        Dim strTp As String = "user_center_orders.aspx?at=orders"
        Dim pageNow = IIf(Request.Params("page") <> Nothing, Request.Params("page"), 1)
        If Request.Params("page") <> Nothing And Request.Params("page") <> 1 Then
            strWite += "<li><a href='" & strTp & "&page=" & pageNow - 1 & "'>上一页</li>"
        End If
        Dim iPagesTp As Double = itemTotle / perPage
        Dim iPages As String = ""
        If (iPagesTp.ToString.IndexOf(".") > 0) Then
            iPages = Left(iPagesTp, iPagesTp.ToString.IndexOf("."))
        Else
            iPages = iPagesTp
        End If
        iPages = IIf(itemTotle Mod perPage <> 0, iPages + 1, iPages)
        If iPages < 2 Then Exit Sub
        For i As Integer = 1 To iPages
            If i = pageNow Then
                strWite += "<li><a class='pageNow' href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i > pageNow - 3 And i < pageNow + 5 Then
                strWite += "<li><a href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i = 1 Or i = iPages Then
                strWite += "<li><a href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i = pageNow - 3 Or i = pageNow + 5 Then
                strWite += "<li>…</li>"
            Else
                strWite = strWite
            End If
        Next
        If pageNow <> iPages Then
            strWite += "<li><a href='" & strTp & "&page=" & pageNow + 1 & "'>下一页</li>"
        End If
        strWite += "</ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getGoodFun()
        Dim strWite As String, myMain As New main
        Dim order As New Hashtable, myOrder As New orders, strTp As String = ""
        order = myOrder.order_get_byId(Request.Params("id"))
        Dim status As New Hashtable
        status = myOrder.getOrderStatusAsString(order)
        strTp = status("status")
        If order("status") <> 1 Then
            Response.Write(myMain.userCenterSetOutInfo("订单状态：", strTp & " （不能进行确认收货操作）", "", False))
            Exit Sub
        End If
        strWite = "<input type='hidden' name='oid' value='" & Request.Params("id") & "' />"
        strTp = "<select name='sall_pj'><option value='1' selected>好评</option><option value='2'>中评</option><option value='3'>差评</option></select>"
        strWite = myMain.userCenterSetOutInfo("对卖家的评价：", strTp, strWite)
        strWite = myMain.userCenterSetOutInfo("对卖家的评价：", "<input type='text' name='message' />", strWite, False)
        strTp = "<input class='subBt' id='ok' type='submit' value ='确认收货' />"
        strWite += strTp
        Response.Write(strWite)
    End Sub
    Public Sub getGoodTousu()
        Dim strWite As String, myMain As New main
        Dim order As New Hashtable, myOrder As New orders, strTp As String
        order = myOrder.order_get_byId(Request.Params("id"))
        Order = myOrder.order_get_byId(Request.Params("id"))
        If order("status") < 3 Then
            strWite = "<input type='hidden' name='oid' value='" & Request.Params("id") & "' />"
            strWite = myMain.userCenterSetOutInfo("投诉理由：", "请在下面填写您的投诉理由", strWite)
            strWite += "<textarea name='message'></textarea>"
            strTp = "<input class='subBt' id='ok' type='submit' value ='提交投诉' />"
            strWite += strTp
            Response.Write(strWite)
        Else
            Response.Write(myMain.userCenterSetOutInfo("投诉状态：", IIf(order("status") = 3, "投诉处理中", "投诉处理完成，请登录您的邮箱查看处理结果"), "", False))
        End If
    End Sub
    Private Function buitPayOrder(ByVal id As Integer) As Hashtable
        Dim gShopInfo As New Hashtable, gGoodsInfo As New Hashtable, gUserInfo As New Hashtable
        Try
            Convert.ToInt32(Request.Form("shop"))
            Dim myShop As New shop
            gShopInfo = myShop.getShopInfo(Request.Form("shop"))
            Dim myGood As New goods
            gGoodsInfo = myGood.goods_get(Request.Form("gid"))
            If gGoodsInfo("status") = "error" Then Response.Redirect("shop.aspx?id=" & Request.Form("shop"))
            Dim myUser As New user
            gUserInfo = myUser.getUserInfo(Session("id"))
        Catch ex As Exception
            Response.Redirect("../default.aspx")
        End Try
        Dim order As New Hashtable
        order("gid") = Request.Form("gid")
        order("id") = id
        order("title") = gGoodsInfo("goods_title")
        order("body") = "桃牛订单"
        order("number") = Request.Form("number")
        order("price") = IIf(gGoodsInfo("goods_discount") > 0.1, gGoodsInfo("goods_prize") * gGoodsInfo("goods_discount") / 100, gGoodsInfo("goods_prize")) * order("number")
        order("name") = Request.Form("name")
        order("address") = Request.Form("address")
        order("zip") = 743000
        order("phone") = Request.Form("phone")
        Return order
    End Function
End Class