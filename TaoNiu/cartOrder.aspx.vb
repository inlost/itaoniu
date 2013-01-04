Public Class cartOrder
    Inherits System.Web.UI.Page
    Protected gShopInfo As Hashtable
    Protected gGoodsInfo As Hashtable()
    Protected gUserInfo As Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.Cookies("gwc")("count") < 1 Then
                Response.Redirect("default.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("cart.aspx")
        End Try
        If Session("onTheWay") <> "loginSuccess" Then
            Dim strUrl As String
            strUrl = "login.aspx?redirectURL=cartOrder.aspx&msg=需要登录才能提交订单"
            Response.Redirect(strUrl)
        End If
        Dim myOrder As New orders
        If myOrder.neddPj(Session("id")) = True Then
            Response.Redirect("user_center_orders.aspx?at=orders&msg=need")
        End If
        Dim myUser As New user
        gUserInfo = myUser.getUserInfo(Session("id"))
        getGoodsInfo()
    End Sub
    Public Sub getGood()
        Dim strWite As String = "", strTp As Integer = 0
        For Each good As Hashtable In gGoodsInfo
            strWite += "<p><span class='title'>商品名称</span><span class='info'>" & good("goods_title") & "</span></p>"
            strWite += "<p><span class='title'>商品单价：</span><span class='info'>" & IIf(good("goods_discount") > 0.1, good("goods_prize") * good("goods_discount") / 100, good("goods_prize")) & "元</span></p>"
            strTp += IIf(good("goods_discount") > 0.1, good("goods_prize") * good("goods_discount") / 100, good("goods_prize"))
        Next
        strWite += "<hr/><p><span class='title'>合计</span><span class='info'>" & strTp & "元</span></p>"
        Response.Write(strWite)
    End Sub
    Public Sub getForm()
        Dim strWite As String = ""
        strWite += "<h2>确认购买信息</h2>"
        Dim myMain As New main
        strWite = "<input type='hidden' name='goodList' value='" & Request.Cookies("gwc")("goods") & "'/>"
        strWite = myMain.userCenterSetOutInfo("给卖家留言：", "<textarea name='message'></textarea>", strWite, False)
        strWite += "<h2>确认收获信息</h2>"
        strWite = myMain.userCenterSetOutInfo("收获人：", "<input type='text' id='recName' name='name' value='" & gUserInfo("real_name") & "' />", strWite, False)
        strWite = myMain.userCenterSetOutInfo("收获地址：", "<input type='text' id='recAddress' name='address' value='" & gUserInfo("address") & "' />", strWite, False)
        strWite = myMain.userCenterSetOutInfo("联系电话：", "<input type='text' id='recPhone' name='phone' value='" & gUserInfo("mobile") & "' />", strWite, False)
        strWite += "<button type='submit'>提交</button>"
        strWite += "<input type='hidden' name='shop' value='" & Request.Form("shop") & "' />"
        strWite += "<input type='hidden' name='gid' value='" & Request.Form("gid") & "' />"
        Response.Write(strWite)
    End Sub
    Public Sub getGoodsInfo()
        Dim myCart As New carts
        gGoodsInfo = myCart.getGoods(Request.Cookies("gwc")("goods"))
        If gGoodsInfo(0)("count") = 0 Then Exit Sub
    End Sub
End Class