Public Class user_center_lm_list
    Inherits System.Web.UI.Page
    Dim goodsList As Hashtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If shop.SelectedIndex = -1 Then
            shop.SelectedIndex = 0
        End If
    End Sub
    Public Sub displayGoods()
        Dim myLianMeng As New saleFunction
        goodsList = myLianMeng.lianMeng_goods_get_byShop(shop.SelectedValue)
        If goodsList Is Nothing Then Exit Sub
        Dim strWite As String = "", myMain As New main, strTp As String = "", myCat As New goods
        For Each good As Hashtable In goodsList
            strTp = "<li>"
            strTp += "<div class='listInner'><div class='order-goods'><img src='" & good("goods_picture") & "'/></div>"
            strTp += "<div class='order-title'>" & good("goods_title") & "</div>"

            strTp += "<div class='order-span'>折扣：" & good("discuount") & "折</div>"
            strTp += "<div class='order-fun'>"
            strTp += "<a  class='subBt' href='server_page/sale_fun.aspx?action=goodsLmDel&sid=" & good("shopId") & "&id=" & good("lm_id") & "' >删除</a>"
            strTp += "</div>"
            strTp += "</div>"
            strTp += "</li>"
            strWite += strTp
        Next
        Response.Write(strWite)
    End Sub
End Class