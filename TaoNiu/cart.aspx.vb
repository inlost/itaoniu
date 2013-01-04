Public Class cart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Public Sub getGoodsList()
        Dim myCart As New carts, myGoods As Hashtable()
        Try
            If Request.Cookies("gwc")("count") = "" Then Exit Sub
            myGoods = myCart.getGoods(Request.Cookies("gwc")("goods"))
        Catch ex As Exception
            Exit Sub
        End Try
        Dim strWite As String = "", strTp As String, myUser = New user
        For Each goodsItem As Hashtable In myGoods
            strWite += "<li>"
            strTp = "shopProduct.aspx?shop=" & goodsItem("shopId") & "&gid=" & goodsItem("id")
            strWite += "<div class='goodsImg'><a href='" & strTp & "'><img src='" & goodsItem("goods_picture") & "' /></a></div>"
            strWite += "<div class='goodsTitle'><h2><a href='" & strTp & "'>" & goodsItem("goods_title") & "</a></h2>"
            Try
                Dim myUserInfo As New Hashtable
                myUserInfo = myUser.getUserInfo(goodsItem("ownerId"))
                strTp = myUserInfo("qq")
                strTp = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTp & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTp & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
            Catch ex As Exception
                strTp = ""
            End Try
            strWite += strTp & "</div>"
            strWite += "<div class='price'><h3><span>￥</span>" & IIf(goodsItem("goods_discount") > 0, goodsItem("goods_prize") * goodsItem("goods_discount") * 0.01, goodsItem("goods_prize")) & "</h3>"
            strWite += "<p>折扣:<span>" & goodsItem("goods_discount") & "</span></p>" & IIf(goodsItem("goods_discount") > 0, "<h4><strike>" & goodsItem("goods_prize") & "</strike></h4>", "")
            strTp = "server_page/cart.aspx?action=modify&by=ajax&id=" & goodsItem("id")
            strWite += "<p class='number'>数量:<input type='text' value='" & goodsItem("number") & "'/>" & "<br/><a href='#' uri='" & strTp & "'>修改</a></p></div>"
            strTp = "server_page/cart.aspx?action=del&by=ajax&id=" & goodsItem("id")
            strWite += "<div class='range'>送货范围：<br/>" & goodsItem("goods_range") & "<br/><a class='gwc_del' href='#' uri='" & strTp & "'>从购物车删除</a></div>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
End Class