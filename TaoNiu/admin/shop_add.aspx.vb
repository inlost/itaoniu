Public Class shop_add
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim myMain As New main, strCertification As String = myMain.checkListToString(shop_certification, "-")
        Dim shopNew As New Hashtable, myShop As New shop
        shopNew("@shop_owner") = shop_owner.Text
        shopNew("@shop_name") = shop_name.Text
        shopNew("@shop_introduce") = shop_introduce.Text
        shopNew("@shop_address") = shop_address.Text
        shopNew("@shop_certification") = myMain.checkListToString(shop_certification, "-")
        Dim strRt As Boolean = myShop.check(shopNew)
        If strRt = False Then
            Response.Write("<script>alert('信息输入不完整，请检查后重新提交')</script>")
        Else
            If myShop.add(shopNew, "") = False Then
                Response.Write("<script>alert('与服务器的信息交换未成功')</script>")
            Else
                Response.Write("<script>alert('店铺添加成功')</script>")
                shop_owner.Text = Nothing
                shop_name.Text = Nothing
                shop_introduce.Text = Nothing
                shop_address.Text = Nothing
            End If
        End If
    End Sub
End Class