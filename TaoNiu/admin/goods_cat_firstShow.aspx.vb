Public Class goods_cat_firstShow
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub del_hd_home_Click(ByVal sender As Object, ByVal e As EventArgs) Handles del_hd_home.Click
        If (list_hd_home.SelectedIndex < 0) Then
            Response.Write("<script>alert('没有选中')</script>")
        Else
            Dim myAdmin As New adm, rst As New Hashtable
            If myAdmin.dell_hd(list_hd_home.SelectedValue) = False Then
                Response.Write("<script>alert('无法与服务器交换数据')</script>")
            Else
                Response.Redirect("goods_cat_firstShow.aspx")
            End If
        End If
    End Sub

    Protected Sub hd_add_Click(ByVal sender As Object, ByVal e As EventArgs) Handles hd_add.Click
        Dim myAdmin As New adm, rst As New Hashtable
        rst = myAdmin.add_hd(Server.MapPath("~"), home_hd_title.Text, home_hd_img.PostedFile, home_hd_src.Text)
        If rst("status") = "error" Then
            Response.Write("<script>alert('" & rst("message") & "')</script>")
        Else
            Response.Redirect("goods_cat_firstShow.aspx")
        End If
    End Sub

    Protected Sub del_hd_cat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles del_hd_cat.Click
        If (cat_hd.SelectedIndex < 0) Then
            Response.Write("<script>alert('没有选中')</script>")
        Else
            Dim myAdmin As New adm, rst As New Hashtable
            If myAdmin.dell_hd(cat_hd.SelectedValue) = False Then
                Response.Write("<script>alert('无法与服务器交换数据')</script>")
            Else
                Response.Redirect("goods_cat_firstShow.aspx")
            End If
        End If
    End Sub

    Protected Sub cat_hd_add_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cat_hd_add.Click
        If (list_cat.SelectedIndex < 0) Then
            Response.Write("<script>alert('没有选中分类')</script>")
        Else
            Dim myAdmin As New adm, rst As New Hashtable
            rst = myAdmin.add_hd(Server.MapPath("~"), cat_hd_title.Text, cat_hd_img.PostedFile, cat_hd_src.Text, list_cat.SelectedValue)
            If rst("status") = "error" Then
                Response.Write("<script>alert('" & rst("message") & "')</script>")
            Else
                Response.Redirect("goods_cat_firstShow.aspx")
            End If
        End If
    End Sub
End Class