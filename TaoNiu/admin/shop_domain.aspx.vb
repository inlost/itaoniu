Public Class shop_domain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex < 0 Then
            Response.Write("<script>alert('没有选中的条目')</script>")
        Else
            SqlDataSource1.UpdateCommandType = SqlDataSourceCommandType.Text
            SqlDataSource1.UpdateCommand = "update shops set shop_domain_status=1 where id=" & ListBox1.SelectedValue
            SqlDataSource1.Update()
            Response.Redirect("shop_domain.aspx")
        End If
    End Sub

    Protected Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim myMain As New main
        HyperLink1.NavigateUrl = myMain.siteUrl & "shop.aspx?id=" & ListBox1.SelectedValue
        HyperLink1.Text = myMain.siteUrl & "shop.aspx?id=" & ListBox1.SelectedValue
    End Sub
End Class