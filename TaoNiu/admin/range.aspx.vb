Public Class range
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If Trim(TextBox1.Text) <> "" Then
            Dim myMain As New main
            SqlDataSource1.InsertCommand = "insert into send_range (range) values ('" & myMain.filterCheck(TextBox1.Text) & "')"
            SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.Text
            SqlDataSource1.Insert()
            Response.Redirect("range.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex >= 0 Then
            SqlDataSource1.DeleteCommandType = SqlDataSourceCommandType.Text
            SqlDataSource1.DeleteCommand = "delete from send_range where id=" & ListBox1.SelectedValue
            SqlDataSource1.Delete()
            Response.Redirect("range.aspx")
        End If
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        If ListBox1.SelectedIndex < 0 Then
            MsgBox("请选择父分类")
        Else
            If Trim(TextBox2.Text) <> "" Then
                Dim myMain As New main
                SqlDataSource2.InsertCommand = "insert into send_range (range,father) values ('" & myMain.filterCheck(TextBox2.Text) & "'," & ListBox1.SelectedValue & ")"
                SqlDataSource2.InsertCommandType = SqlDataSourceCommandType.Text
                SqlDataSource2.Insert()
                TextBox2.Text = ""
            End If
        End If
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        If ListBox2.SelectedIndex >= 0 Then
            SqlDataSource2.DeleteCommandType = SqlDataSourceCommandType.Text
            SqlDataSource2.DeleteCommand = "delete from send_range where id=" & ListBox2.SelectedValue
            SqlDataSource2.Delete()
        End If
    End Sub
End Class