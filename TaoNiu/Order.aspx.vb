Public Class Order
    Inherits System.Web.UI.Page
    Protected gShopInfo As Hashtable
    Protected gGoodsInfo As Hashtable
    Protected gUserInfo As Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Form("shop") = Nothing Or Request.Form("gid") = Nothing Then
            Response.Redirect("default.aspx")
        End If
        If Request.Params("msg") <> Nothing Then
            Response.Write("<script>alert('无法与服务器交换数据')</script>")
        End If
        Dim myOrder As New orders
        If myOrder.neddPj(Session("id")) = True Then
            Response.Redirect("user_center_orders.aspx?at=orders&msg=need")
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
            Response.Redirect("default.aspx")
        End Try
    End Sub
    Public Sub getGood()
        Dim strWite As String, strTp As String = ""
        strWite = "<h2>商品信息</h2>"
        strWite += "<img id='goodPic' src='" & gGoodsInfo("goods_picture") & "'/>"
        strWite += "<p><span class='title'>商品名称：</span><span class='info'>" & gGoodsInfo("goods_title") & "</span></p>"
        strWite += "<p><span class='title'>商品单价：</span><span class='info'>" & IIf(gGoodsInfo("goods_discount") > 0.1, gGoodsInfo("goods_prize") * gGoodsInfo("goods_discount") / 100, gGoodsInfo("goods_prize")) & "元</span></p>"
        strWite += "<p><span class='title'>商店：</span><span class='info'>" & gShopInfo("shop_name") & "</span></p>"
        strWite += "<p><span class='title'>商店地址：</span><span class='info'>" & gShopInfo("shop_address") & "</span></p>"
        Try
            Dim myUserInfo As New Hashtable, myUser As New user
            myUserInfo = myUser.getUserInfo(gGoodsInfo("ownerId"))
            strTp = myUserInfo("qq")
            strTp = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTp & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTp & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
        Catch ex As Exception
            strTp = ""
        End Try
        strWite += "<p><span class='title'></span><span class='info'>" & strTp & "</span></p>"
        Response.Write(strWite)
    End Sub
    Public Sub getForm()
        Dim strWite As String = ""
        strWite += "<h2>确认购买信息</h2>"
        Dim myMain As New main
        Dim strTp As String = ""
        If gGoodsInfo("goods_pay_type") = 0 Then
            strTp = "<p id='pay'>支付方式:<input type='radio' name='pay'checked value ='hdfk' >货到付款<input type='radio' name='pay' value ='zxzf'>在线支付</p>"
        ElseIf gGoodsInfo("goods_pay_type") = 1 Then
            strTp = "<p id='pay'>支付方式:<input type='radio' name='pay' checked value ='zxzf'>在线支付</p>"
        Else
            strTp = "<p id='pay'>支付方式:<input type='radio' name='pay'checked value ='hdfk' >货到付款</p>"
        End If
        strWite += strTp
        strWite = myMain.userCenterSetOutInfo("购买数量：", "<input id='number' type='text' name='number' value='" & Request.Form("number") & "' />", strWite, False)
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
End Class