Public Class advertisement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If adType.SelectedIndex < 0 Then
            Response.Write("<script>alert('请选择广告类型')</script>")
            Exit Sub
        End If
        Dim myMain As New main, strTitle As String, strAd As String
        strTitle = myMain.filterCheck(adTitle.Text)
        strAd = myMain.filterCheck(ad.Text)
        If Len(strTitle) = 0 Or Len(strAd) = 0 Then
            Response.Write("<script>alert('必填项目为空，请检查输入')</script>")
            Exit Sub
        End If
        Dim myAd As New Ad, strType As Integer
        Select Case adType.SelectedValue
            Case "uCenter"
                strType = 1
            Case "sideBar"
                strType = 2
        End Select
        If myAd.ad_add(strTitle, strAd, strType) Then
            Response.Redirect("advertisement.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex < 0 Then
            Response.Write("<script>alert('没有选中的广告')</script>")
            Exit Sub
        End If
        Dim myAd As New Ad
        myAd.ad_del(ListBox1.SelectedValue)
        Response.Redirect("advertisement.aspx")
    End Sub
End Class