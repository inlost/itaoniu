Public Class goods_property
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("nowCat") = getNowCat()
    End Sub

    Protected Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox1.SelectedIndexChanged
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        Button1.Text = "添加"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Protected Sub ListBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox2.SelectedIndexChanged
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        Button1.Text = "添加"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Protected Sub ListBox3_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox3.SelectedIndexChanged
        ListBox4.Items.Clear()
        Button1.Text = "添加"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim theQuery As New Hashtable
        theQuery("@catId") = getNowCat()
        theQuery("@name") = TextBox1.Text
        theQuery("@type") = ListBox6.SelectedValue
        theQuery("@property") = TextBox2.Text.Replace(vbCrLf, "|")
        Dim myHelper As New dbHelper
        If getNowCat() > -1 Then
            If ListBox5.Items.Count > 0 And ListBox5.SelectedIndex > -1 Then
                theQuery("@id") = ListBox5.SelectedValue
                myHelper.noneQuery("cat_property_update", myHelper.hashtableToInquery(theQuery))
            Else
                myHelper.noneQuery("cat_property_add", myHelper.hashtableToInquery(theQuery))
            End If
        End If
        TextBox1.Text = ""
        TextBox2.Text = ""
        Response.Redirect("goods_property.aspx")
    End Sub
    Private Function getNowCat() As Integer
        If ListBox4.SelectedIndex > -1 Then
            Return ListBox4.SelectedValue()
        End If
        If ListBox3.SelectedIndex > -1 Then
            Return ListBox3.SelectedValue()
        End If
        If ListBox2.SelectedIndex > -1 Then
            Return ListBox2.SelectedValue()
        End If
        If ListBox1.SelectedIndex > -1 Then
            Return ListBox1.SelectedValue()
        End If
        Return -1
    End Function

    Protected Sub ListBox5_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox5.SelectedIndexChanged
        If ListBox5.Items.Count > 0 And ListBox5.SelectedIndex > -1 Then
            Dim strSql As String = "select * from cat_property where id=" & ListBox5.SelectedValue
            Dim myHelper As New dbHelper, thePro As New Hashtable, myType As New typeHelper
            thePro = myType.dsToHashSingle(myHelper.getQuerySql(strSql, "rt"), "rt")
            TextBox1.Text = thePro("name")
            ListBox6.Items(thePro("type") - 1).Selected = True
            TextBox2.Text = thePro("property").ToString.Replace("|", vbCrLf)
            Button1.Text = "修改"
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        If ListBox5.Items.Count > 0 And ListBox5.SelectedIndex > -1 Then
            Dim strSql As String = "delete from cat_property where id=" & ListBox5.SelectedValue
            Dim myHelper As New dbHelper
            myHelper.querySql(strSql)
            Response.Redirect("goods_property.aspx")
        End If
    End Sub

    Protected Sub ListBox4_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox4.SelectedIndexChanged
        Button1.Text = "添加"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
End Class