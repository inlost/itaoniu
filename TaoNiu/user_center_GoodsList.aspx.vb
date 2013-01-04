Public Class user_center_GoodsList
    Inherits System.Web.UI.Page
    Dim pageNow As Integer = 0
    Dim iCount As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pageNow = IIf(Request.Params("page") = Nothing, 1, Request.Params("page"))
    End Sub
    ''' <summary>
    ''' 获取出售商品列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getGoodsList()
        If DropDownList1.Items.Count > 0 And DropDownList1.SelectedIndex < 0 Then
            DropDownList1.SelectedIndex = 0
            Response.Redirect("user_center_GoodsList.aspx?at=goods&action=management")
        End If

        Dim goods As Hashtable(), mySearch As New searchHelper, myDs As New dbHelper, sql As String

        goods = mySearch.goods_get_fromShop(DropDownList1.SelectedValue, pageNow, 3)
        If goods.Length = 0 Then
            Return
        End If
        sql = "select * from dbo.goods_products where shopId=" & goods(0)("shopId")
        iCount = myDs.getQuerySql(sql, "goods_products").Tables("goods_products").Rows.Count


        Dim strWite As String = "", strTp As String = "", myUser As New user
        For Each goodsItem As Hashtable In goods
            strWite += "<li>"
            strTp = "shopProduct.aspx?shop=" & goodsItem("shopId") & "&gid=" & goodsItem("id")
            strWite += "<div class='goodsImg'><a href='" & strTp & "'><img src='" & goodsItem("goods_picture") & "' /></a></div>"
            strWite += "<div class='goodsTitle'><h2><a href='" & strTp & "'>" & goodsItem("goods_title") & "</a></h2></div>"
            strWite += "<div class='price'><h3><span>￥</span>" & IIf(goodsItem("goods_discount") > 0, goodsItem("goods_prize") * goodsItem("goods_discount") * 0.01, goodsItem("goods_prize")) & "</h3>"
            strWite += "<p>折扣:<span>" & goodsItem("goods_discount") & "</span></p>" & IIf(goodsItem("goods_discount") > 0, "<h4><strike>" & goodsItem("goods_prize") & "</strike></h4>", "") & "</div>"
            strWite += "<div>" & goodsItem("goods_on_sale_date") & "</div>"
            strWite += "<div><a href='user_center_GoodsEdit.aspx?id=" & goodsItem("id") & "&shopId=" & goodsItem("shopId") & "'>编辑宝贝</a></div>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
    ''' <summary>
    ''' 分页处理
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getPageLink()
        Dim strWite As String = "", perPage As Integer = 3
        Dim myPage As New pageLink
        Try
            strWite = myPage.getPageLink(iCount, perPage, pageNow, "?at=goods&action=management")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Response.Write(strWite)
    End Sub
End Class