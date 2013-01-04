Public Class user_center_lm_search
    Inherits System.Web.UI.Page
    Private rtGoods As Hashtable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If shop.SelectedIndex = -1 Then
                shop.SelectedIndex = 0
            End If
            If shif.SelectedIndex = -1 Then
                shif.SelectedIndex = 0
            End If
            If goods.SelectedIndex = -1 Then
                goods.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Sub getGoodsList()
        Dim mySale As New saleFunction, myGoods As New goods, theGood As New Hashtable, tpGood As New Hashtable
        If goods.SelectedIndex < 0 Then Exit Sub
        theGood = myGoods.goods_get(goods.SelectedValue)
        Dim strCat = theGood("goods_cat")
        rtGoods = mySale.lianMeng_search(strCat)
        Dim strWite As String = ""
        If rtGoods(0)("count") = 0 Then Exit Sub
        For Each goodItem As Hashtable In rtGoods
            tpGood = myGoods.goods_get(goodItem("gid"))
            strWite += "<li>"
            strWite += "<div class='listInner'><div class='order-goods'><img src='" & tpGood("goods_picture") & "'/></div>"
            strWite += "<div class='order-title'>" & tpGood("goods_title") & "</div>"

            strWite += "<div class='order-span'>折扣：" & goodItem("discuount") & "折</div>"
            strWite += "<div class='order-fun'>"
            strWite += "<a  class='subBt' href='server_page/sale_fun.aspx?action=goodsLmDel&sid=" & tpGood("shopId") & "&id=" & goodItem("id") & "' >删除</a>"
            strWite += "</div>"
            strWite += "</div>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
End Class