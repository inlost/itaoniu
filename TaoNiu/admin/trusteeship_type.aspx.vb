Public Class trusteeship_type
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex > -1 Then
            SqlDataSource1.DeleteCommandType = SqlDataSourceCommandType.Text
            SqlDataSource1.DeleteCommand = "delete from trusteeship_type where id=" & ListBox1.SelectedValue
            SqlDataSource1.Delete()
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Dim theType As New Hashtable, myHelper As New dbHelper
        If (Len(Trim(TextStart.Text)) < 1 Or Len(Trim(TextEnd.Text)) < 1 Or Len(Trim(TextFee.Text)) < 1) Then
            Response.Write("<script>alert('请输入正确的信息')</script>")
        Else
            theType("@typeName") = Trim(TextName.Text)
            theType("@startTime") = Trim(TextStart.Text)
            theType("@endTime") = Trim(TextEnd.Text)
            theType("@fee") = Trim(TextFee.Text)
            Try
                Convert.ToInt32(theType("@startTime"))
                Convert.ToInt32(theType("@endTime"))
                myHelper.noneQuery("trusteeship_type_add", myHelper.hashtableToInquery(theType))
                TextName.Text = ""
                TextStart.Text = ""
                TextEnd.Text = ""
                Response.Redirect("trusteeship_type.aspx")
            Catch ex As Exception
                Response.Write("<script>alert('请输入正确的信息')</script>")
            End Try
        End If
    End Sub
End Class