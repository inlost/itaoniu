Public Class goods_type
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myMain As New main
        SqlDataSource1.DeleteCommand = "delete from goods_type where id=" & listGoodType.SelectedValue
        SqlDataSource1.DeleteCommandType = SqlDataSourceCommandType.Text
        SqlDataSource2.DeleteCommand = "delete from goods_sale_type where id=" & listSaleType.SelectedValue
        SqlDataSource2.DeleteCommandType = SqlDataSourceCommandType.Text
        SqlDataSource1.InsertCommand = "insert into goods_type (name) values ('" & myMain.filterCheck(TextBox1.Text) & "')"
        SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.Text
        SqlDataSource2.InsertCommand = "insert into goods_sale_type (name) values ('" & myMain.filterCheck(TextBox2.Text) & "')"
        SqlDataSource2.InsertCommandType = SqlDataSourceCommandType.Text
    End Sub

    Protected Sub rmGoodType_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rmGoodType.Click
        Try
            If listGoodType.SelectedValue > 0 Then
                SqlDataSource1.Delete()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Response.Write("<script>alert('没有选中的条目')</script>")
        End Try
    End Sub

    Protected Sub rmSaleType_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rmSaleType.Click
        Try
            If listSaleType.SelectedValue > 0 Then
                SqlDataSource2.Delete()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Response.Write("<script>alert('没有选中的条目')</script>")
        End Try
    End Sub

    Protected Sub addGoodType_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addGoodType.Click
        Dim myGood As New goods
        If (myGood.checkIsNotNull(TextBox1.Text)) Then
            SqlDataSource1.Insert()
            TextBox1.Text = ""
        Else
            Response.Write("<script>alert('商品类型不能为空')</script>")
        End If
    End Sub

    Protected Sub addSaleType_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addSaleType.Click
        Dim myGood As New goods
        If (myGood.checkIsNotNull(TextBox2.Text)) Then
            SqlDataSource2.Insert()
            TextBox2.Text = ""
        Else
            Response.Write("<script>alert('商品销售类型不能为空')</script>")
        End If
    End Sub
End Class