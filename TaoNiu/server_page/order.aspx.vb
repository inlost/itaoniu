Imports System.Net
Imports TaoNiu.AlipayClass
Imports System.Xml

Public Class order1
    Inherits System.Web.UI.Page
    Protected gShopInfo As Hashtable
    Protected gGoodsInfo As Hashtable
    Protected gUserInfo As Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("onTheWay") <> "loginSuccess" Then
            Response.Redirect("../default.aspx")
        End If
        Select Case Request.Params("action")
            Case "add"
                If Request.Form("shop") = Nothing Or Request.Form("gid") = Nothing Then
                    Response.Redirect("../default.aspx")
                End If
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
                order("@ownerId") = gShopInfo("shop_owner")
                order("@shop") = gShopInfo("id")
                order("@good") = Request.Form("gid")
                order("@customer") = Session("id")
                order("@message") = Request.Form("message")
                order("@number") = Request.Form("number")
                order("@rec_name") = Request.Form("name")
                order("@rec_phone") = Request.Form("phone")
                order("@rec_address") = Request.Form("address")
                Dim myOrder As New orders, rt As Integer
                rt = myOrder.add(order)
                If rt = 0 Then
                    Response.Redirect("../Order.aspx?msg=1")
                Else
                    If Request.Form("pay") = "zxzf" Then
                        myOrder.orderStatusChange(rt, 0, 1)
                        'Dim payorder As New tnPay.orde
                        'Dim orderpay As New Hashtable
                        'orderpay = buitPayOrder(rt)
                        'Dim strwite As String = payorder.add(orderpay)
                        'strwite = strwite.Replace("'", """")
                        'Dim struri = "../paysystem/payto.aspx?fm=" & strwite
                        'Response.Redirect(strUri)
                    End If
                    Response.Redirect("../user_center_orders.aspx?at=orders")
                End If
            Case "addList"
                addList()
            Case "ok"
                Dim myOrder As New orders
                If myOrder.hasOrder(Request.Form("oid"), Session("id")) Then
                    Dim order As New Hashtable
                    order = myOrder.order_get_byId(Request.Form("oid"))
                    If myOrder.order_ok(Session("id"), Request.Form("oid"), order("good"), Request.Form("sall_pj"), Request.Form("message")) Then
                        Response.Redirect("../user_center_orders.aspx?at=orders")
                    Else
                        Response.Redirect("../user_center_orders.aspx?at=orders&msg=okerror")
                    End If
                End If
            Case "tousu"
                Dim myOrder As New orders
                If myOrder.hasOrder(Request.Form("oid"), Session("id")) Then
                    Dim order As New Hashtable
                    order = myOrder.order_get_byId(Request.Form("oid"))
                    If myOrder.order_tousu(Session("id"), Request.Form("oid"), order("good"), Request.Form("message")) Then
                        Response.Redirect("../user_center_orders.aspx?at=orders")
                    Else
                        Response.Redirect("../user_center_orders.aspx?at=orders&msg=okerror")
                    End If
                End If
            Case "send"
                Dim myOrder As New orders
                If myOrder.hasOrder(Request.Form("oid"), Session("id")) Then
                    Dim order As New Hashtable
                    order = myOrder.order_get_byId(Request.Form("oid"))
                    Dim good As New Hashtable, myGood As New goods
                    good = myGood.goods_get(order("good"))
                    If myOrder.order_send(Session("id"), Request.Form("oid")) Then
                        Dim myMail As New mail, strMail As String, strTp As String, myMain As New main
                        strTp = "您在" & myMain.siteName & "订购的"
                        strMail = myMail.buidMailBody("", strTp)
                        strMail = myMail.buidMailBody(strMail, good("goods_title"), "a", myMain.siteUrl & "shopProduct.aspx?shop=" & order("shop") & "&gid=" & order("good"))
                        strMail = myMail.buidMailBody(strMail, "买家已经发货，请注意签收")
                        Dim user As Hashtable, myUser As New user
                        user = myUser.getUserInfo(order("customer"))
                        myMail.sendMail(user("email"), "您在" & myMain.siteName & "购买的" & good("goods_title") & "已经发货", strMail)
                        If order("payMent") <> 0 Then
                            Dim strFm As String = "", tnOrder As New TaoNiu.tnPay.orde
                            strFm = tnOrder.changeToSended(order("payId"))
                            Response.Redirect("../user_center_myOrders.aspx?at=myorders&action=order&id=" & Request.Form("oid"))
                        Else
                            Response.Redirect("../user_center_myOrders.aspx?at=myorders")
                        End If
                    Else
                        Response.Redirect("../user_center_myOrders.aspx?at=myorders&msg=okerror")
                    End If
                End If
            Case "modify_price"
                Dim myOrder As New orders
                Dim rst As Boolean = myOrder.modifyPrice(Request.Form("oid"), Request.Form("new_price"), Session("id"))
                Dim rdUrl As String = ""
                If rst Then
                    rdUrl = "../user_center_myOrders.aspx?at=myorders&msg=price_Ok"
                Else
                    rdUrl = "../user_center_myOrders.aspx?at=myorders&action=fun&msg=price_error&id=" & Request.Form("oid")
                End If
                Response.Redirect(rdUrl)
            Case Else
                Response.Write("Good luck")
        End Select
    End Sub
    Private Sub addList()
        Dim goodsList = Request.Form("goodList")
        Dim gIdList As String(), rt As Integer, separator As Char() = "-"
        gIdList = goodsList.Split(separator)
        For i = 0 To gIdList.Length - 1
            Dim gid As String, gnb As String
            separator = "^"
            gid = gIdList(i).Split(separator)(0)
            gnb = gIdList(i).Split(separator)(1)
            Try
                Dim myGood As New goods
                gGoodsInfo = myGood.goods_get(gid)
                Dim myShop As New shop
                gShopInfo = myShop.getShopInfo(gGoodsInfo("shopId"))
                If gGoodsInfo("status") = "error" Then Response.Redirect("shop.aspx?id=" & Request.Form("shop"))
                Dim myUser As New user
                gUserInfo = myUser.getUserInfo(Session("id"))
            Catch ex As Exception
                Response.Redirect("../default.aspx")
            End Try
            Dim order As New Hashtable
            order("@ownerId") = gShopInfo("shop_owner")
            order("@shop") = gShopInfo("id")
            order("@good") = gid
            order("@customer") = Session("id")
            order("@message") = Request.Form("message")
            order("@number") = gnb
            order("@rec_name") = Request.Form("name")
            order("@rec_phone") = Request.Form("phone")
            order("@rec_address") = Request.Form("address")
            Dim myOrder As New orders
            rt = myOrder.add(order)
        Next
        Response.Cookies("gwc")("count") = ""
        Response.Cookies("gwc")("goods") = ""
        Response.Redirect("../user_center_orders.aspx?at=orders&id=" & rt)
    End Sub
    Private Function buitPayOrder(ByVal id As Integer) As Hashtable
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
        order("price") = IIf(gGoodsInfo("goods_discount") > 0.1, gGoodsInfo("goods_prize") * gGoodsInfo("goods_discount") / 100, gGoodsInfo("goods_prize"))
        order("number") = Request.Form("number")
        order("name") = Request.Form("name")
        order("address") = Request.Form("address")
        order("zip") = 743000
        order("phone") = Request.Form("phone")
        Return order
    End Function
End Class