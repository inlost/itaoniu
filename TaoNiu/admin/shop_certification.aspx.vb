Public Class shop_certification
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex > -1 Then
            Dim myShop As New shop, theCe As New Hashtable, theApply As New Hashtable
            theCe = myShop.getCe(ListBox1.SelectedItem.Text)
            theApply = myShop.getCeApply(ListBox1.SelectedValue)
            Label2.Text = theCe("name")
            Label4.Text = theApply("apply_content")
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex > -1 Then
            Dim myShop As New shop, theCe As New Hashtable, theApply As New Hashtable
            theCe = myShop.getCe(ListBox1.SelectedItem.Text)
            theApply = myShop.getCeApply(ListBox1.SelectedValue)
            Dim strSql As String = "update shop_certion_apply set apply_status=2 where id=" & ListBox1.SelectedValue
            Dim myHelper As New dbHelper
            myHelper.querySql(strSql)
            myShop.addShopCe(theApply("shopId"), theCe("id"))
            Response.Redirect("shop_certification.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex > -1 Then
            Dim strSql As String = "delete from shop_certion_apply where id=" & ListBox1.SelectedValue
            Dim myHelper As New dbHelper
            myHelper.querySql(strSql)
            Response.Redirect("shop_certification.aspx")
        End If
    End Sub
End Class