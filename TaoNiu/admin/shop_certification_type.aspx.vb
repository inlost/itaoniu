Public Class shop_certification_type
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myMain As New main
        SqlDataSource1.InsertCommand = "insert into shop_certification_type (name,tip,fee) values('" & myMain.filterCheck(TextBox1.Text) & "','" & myMain.filterCheck(TextBox2.Text) & "'," & Trim(TextBox3.Text) & ")"
        SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.Text
        SqlDataSource1.DeleteCommand = "delete from shop_certification_type where id=" & ListBox1.SelectedValue
        SqlDataSource1.DeleteCommandType = SqlDataSourceCommandType.Text
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim myMain As New main
        If myMain.checkIsNotNull(myMain.filterCheck(TextBox1.Text)) And myMain.checkIsNotNull(myMain.filterCheck(TextBox2.Text)) Then
            Try
                Convert.ToInt32(TextBox3.Text)
                SqlDataSource1.Insert()
                TextBox1.Text = Nothing
                TextBox2.Text = Nothing
                TextBox3.Text = Nothing
            Catch ex As Exception
                Response.Write("<script>alert('分类与介绍不能为空')</script>")
            End Try

        Else
            Response.Write("<script>alert('分类与介绍不能为空')</script>")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Try
            If ListBox1.SelectedValue > 0 Then
                SqlDataSource1.Delete()
            End If
        Catch ex As Exception
            Response.Write("<script>alert('没有选中任何类型')</script>")
        End Try
    End Sub
End Class