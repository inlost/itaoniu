Public Class announcement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex >= 0 Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim inQuery(0 To 0) As dbHelper.inQuery
        inQuery(0).name = "@anId"
        inQuery(0).value = ListBox1.SelectedValue
        Dim myHelper As New dbHelper
        myHelper.noneQuery("announcement_del", inQuery)
        Response.Redirect("announcement.aspx")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Dim inQuery(0 To 1) As dbHelper.inQuery
        If Len(Trim(TextBox1.Text)) = 0 Or Len(Trim(TextBox2.Text)) = 0 Then
            Response.Write("<script>alert('必填项为空！请检查后重新提交')</script>")
            Exit Sub
        End If
        Dim myMain As New main
        inQuery(0).name = "@title"
        inQuery(0).value = myMain.filterCheck(TextBox1.Text)
        inQuery(1).name = "@announcement"
        inQuery(1).value = myMain.filterCheck(TextBox2.Text)
        Dim myHelper As New dbHelper
        myHelper.noneQuery("announcement_add", inQuery)
        Response.Redirect("announcement.aspx")
    End Sub
End Class