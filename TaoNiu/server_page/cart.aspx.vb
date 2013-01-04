Public Class cart1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Request.Params("action")
            Case "clear_cart"
                Response.Cookies("gwc")("count") = ""
                Response.Cookies("gwc")("goods") = ""
            Case "modify"
                Dim myCart As New carts, goodsListNew As String, count As String
                count = Request.Cookies("gwc")("count")
                goodsListNew = myCart.changeNumber(Request.Cookies("gwc")("goods"), Request.Params("id"), Request.Form("number"))
                Response.Cookies("gwc")("goods") = goodsListNew
                Response.Cookies("gwc")("count") = count
                Response.Write("success")
            Case "del"
                Dim myCart As New carts, goodsListNew As String, count As String
                count = Request.Cookies("gwc")("count") - 1
                goodsListNew = myCart.del(Request.Cookies("gwc")("goods"), Request.Params("id"))
                Response.Cookies("gwc")("goods") = goodsListNew
                Response.Cookies("gwc")("count") = count
                Response.Write("success")
            Case Else
                Response.Write("Good Luck~")
        End Select
        If Request.Params("by") <> "ajax" Then
            Response.Redirect("../cart.aspx")
        End If
    End Sub

End Class