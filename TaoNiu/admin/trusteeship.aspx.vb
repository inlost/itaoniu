Public Class trusteeship
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex > -1 Then
            SqlDataSource1.DeleteCommandType = SqlDataSourceCommandType.Text
            SqlDataSource1.DeleteCommand = "delete from trusteeship where id=" & ListBox1.SelectedValue
            SqlDataSource1.Delete()
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Dim myMain As New main, strQQ As String = TextQQ.Text, strId As String = TextTn.Text
        strQQ = Trim(strQQ)
        strId = Trim(strId)
        If Len(strQQ) < 1 Or Len(strId) < 1 Then
            Response.Write("<script>alert('请输入正确的信息')</script>")
        End If
        Dim myHelper As New dbHelper, theTru As New Hashtable
        theTru("@qq") = strQQ
        theTru("@taoniuId") = strId
        myHelper.noneQuery("trusteeship_add", myHelper.hashtableToInquery(theTru))
        TextQQ.Text = ""
        TextTn.Text = ""
        Response.Redirect("trusteeship.aspx")
    End Sub
End Class