Public Class lianMeng
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("onTheWay") <> "loginSuccess" Then
            Response.Redirect("../default.aspx")
        End If
        Dim myShop As New shop
        Select Case Request.Params("action")
            Case "add"
                myShop.lianMengAdd(Request.Form("shop"), Request.Form("shopLm"))
            Case "del"
                myShop.lianMengDel(Request.Params("shop"), Request.Params("unId"), Session("id"))
            Case "icon"
                myShop.iconAdd(Request.Form("shop"), Request.Files("icon"), Server.MapPath("~"))
            Case Else
                Response.Write("Good Luck~")
        End Select
        Response.Redirect("../user_center_lianMeng.aspx?at=lianMeng")
    End Sub

End Class