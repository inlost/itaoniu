Public Class user_center_myOrders
    Inherits System.Web.UI.Page
    Public itemTotle As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("msg") <> Nothing Then
            Select Case Request.Params("msg")
                Case "okerror"
                    Response.Write("<script>alert('未能与服务器交换数据')</script>")
                Case "price_Ok"
                    Response.Write("<script>alert('价格修改成功')</script>")
                Case "price_error"
                    Response.Write("<script>alert('价格修改失败，请检查您是否有权限修改订单并检查您的输入')</script>")
            End Select
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
        If itemTotle < 1 Then Exit Sub
        Dim myGood As New goods, good As New Hashtable, status As New Hashtable
        For Each Order As Hashtable In orders
            good = myGood.goods_get(Order("good"))
            status = myOrder.getOrderStatusAsString(Order)
            strTp = "<a class='subLink' " & status("color") & " href='user_center_myOrders.aspx?at=myorders&action=fun&id=" & Order("id") & IIf(Request.Params("page") = Nothing, "", "&page=" & Request.Params("page")) & "'>" & status("status") & good("goods_title") & "</a>"
            strWite = myMain.userCenterSetOutInfo("订单：", strTp, strWite, False)
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
            strWite = myMain.userCenterSetOutInfo("店铺：", "<a href='../shop.axp?id=" & order("shop") & "'>" & shop("shop_name") & "</a>", strWite)
            strWite = myMain.userCenterSetOutInfo("下单时间：", order("time"), strWite)
            strWite = myMain.userCenterSetOutInfo("收货人：", order("rec_name"), strWite)
            strWite = myMain.userCenterSetOutInfo("联系电话：", order("rec_phone"), strWite)
            Dim status As New Hashtable
            status = myOrder.getOrderStatusAsString(order)
            strTp = status("status")
            strWite = myMain.userCenterSetOutInfo("订单状态：", strTp, strWite)
            strWite = myMain.userCenterSetOutInfo("收货地址：", order("rec_address"), strWite, False)
            If Request.Params("action") = Nothing Then
                strWite += "<a class='subLink' href='user_center_myOrders.aspx?at=myorders&action=order&id=" & order("id") & "' >订单操作（投诉、评价、发货）</a>"
            End If
            strWite += "<br/>"
            Response.Write(strWite)
        End If
    End Sub
    Public Sub getGoodTousu()
        Dim strWite As String, myMain As New main
        Dim order As New Hashtable, myOrder As New orders, strTp As String
        order = myOrder.order_get_byId(Request.Params("id"))
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
    Public Sub getGoodFun()
        Dim strWite As String, myMain As New main
        Dim order As New Hashtable, myOrder As New orders, strTp As String = ""
        order = myOrder.order_get_byId(Request.Params("id"))
        Dim status As New Hashtable
        status = myOrder.getOrderStatusAsString(order)
        strTp = status("status")
        Dim canSend As Boolean
        If order("status") = 0 And order("payMent") = 0 Then
            canSend = True
        ElseIf order("status") = 0 And order("payMent") = 2 Then
            canSend = True
        Else
            canSend = False
        End If
        If canSend = False Then
            Response.Write(myMain.userCenterSetOutInfo("订单状态：", strTp & " （不能进行确认发货操作）", "", False))
            Exit Sub
        End If
        strWite = "<input type='hidden' name='oid' value='" & Request.Params("id") & "' />"
        strTp = "<input class='subBt' id='ok' type='submit' value ='已经发货' />"
        strWite += strTp
        Response.Write(strWite)
    End Sub
    Public Sub getModifyPrice()
        Dim strWite As String, myMain As New main
        Dim order As New Hashtable, myOrder As New orders, strTp As String
        Dim myGoods As New goods, theGoods As New Hashtable
        order = myOrder.order_get_byId(Request.Params("id"))
        theGoods = myGoods.goods_get(order("good"))
        If order("status") = 0 Then
            strWite = "<input type='hidden' name='oid' value='" & Request.Params("id") & "' />"
            Dim prices As String = IIf(theGoods("goods_discount") > 0.1, theGoods("goods_prize") * theGoods("goods_discount") / 100, theGoods("goods_prize"))
            strWite = myMain.userCenterSetOutInfo("原价格：", IIf(order("price") = 0, prices, order("price")), strWite)
            strWite = myMain.userCenterSetOutInfo("新价格：", "<input type='text' name='new_price' />", strWite, False)
            strTp = "<input class='subBt' id='ok' type='submit' value ='提交' />"
            strWite += strTp
            Response.Write(strWite)
        Else
            Response.Write(myMain.userCenterSetOutInfo("不能改价：", "买家已经支付，不能改价。", "", False))
        End If

    End Sub
End Class