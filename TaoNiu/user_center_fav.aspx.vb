Public Class user_center_fav
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getGoods()
        Try
            Dim myPageLink As New pageLink, myFav As New favorites
            Dim myGoods As Hashtable()
            Dim page As Integer = IIf(Request.Params("page") = Nothing, 1, Request.Params("page"))
            myGoods = myFav.getGoods(Session("id"), page, 6)
            Dim strWite As String = ""
            If myGoods(0)("count") = 0 Then
                strWite = "<p>您还没有收藏的宝贝。</p>"
                Response.Write(strWite)
                Exit Sub
            End If
            Dim strTp As String, myMain As New main
            strWite += "<ul id='fav_goods'>"
            For Each good As Hashtable In myGoods
                strWite += "<li><div>"
                strTp = myMain.siteUrl & "shopProduct.aspx?shop=" & good("shopId") & "&gid=" & good("id")
                strWite += "<a href='" & strTp & "'><img src='" & good("goods_picture") & "'/></a>"
                strWite += "<a href='" & strTp & "' class='subli'>" & good("goods_title") & "</a>"
                strTp = IIf(good("goods_discount") > 0, good("goods_prize") * good("goods_discount") * 0.01, good("goods_prize"))
                strWite += "<p>￥" & strTp
                strTp = "../server_page/favorites.aspx?action=del&fid=" & good("favId") & "&redirectURL=../user_center_fav.aspx?at=fav"
                strTp = "<a class='delSc' href='" & strTp & "'>删除</a></p>"
                strWite += strTp
                strWite += "</div></li>"
            Next
            strWite += "<div class='clear'></div></ul>"
            strWite += myPageLink.getPageLink(myGoods(0)("count"), 6, page, "?at=fav")
            Response.Write(strWite)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub getLianMengList()
        Dim strWite As String
        Try
            strWite = "<ul id='lianMengList'>"
            Dim myShop As New shop, myFav As New favorites
            Dim lianMengId As Hashtable()
            Dim page As Integer = IIf(Request.Params("page") = Nothing, 1, Request.Params("page"))
            lianMengId = myFav.getShop(Session("id"), page, 8)
            If lianMengId(0)("count") = 0 Then
                Response.Write("没有收藏的店铺")
                Exit Sub
            End If
            Dim strTp As String = ""
            For Each shopNow As Hashtable In lianMengId
                strWite += "<li>"
                strTp = "<a href='shop.aspx?id=" & shopNow("id") & "' title='" & shopNow("shop_name") & "'>"
                If shopNow("shop_pic") <> "no" Then
                    strWite += "<div class='inner'>" & strTp & "<img class='lmImg' src='" & shopNow("shop_pic") & "' title='" & shopNow("shop_name") & "' rel='" & shopNow("shop_name") & "'/></a></div>"
                End If
                strWite += "<div class='inner'>" & strTp & shopNow("shop_name") & "</a></div>"
                strTp = "../server_page/favorites.aspx?action=del&fid=" & shopNow("favId") & "&redirectURL=../user_center_fav.aspx?at=fav"
                strTp = "<a class='delSc' href='" & strTp & "'>删除</a></p>"
                strWite += strTp
                strWite += "</li>"
            Next
            strWite += "<div class='clear'></div></ul>"
            Dim myPageLink As New pageLink
            strWite += myPageLink.getPageLink(lianMengId(0)("count"), 6, page, "?at=fav")
        Catch ex As Exception
            strWite = ""
        End Try
        Response.Write(strWite)
    End Sub
End Class