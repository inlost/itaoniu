Public Class user_center_lm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If shop.SelectedIndex = -1 Then
            shop.SelectedIndex = 0
        End If
    End Sub
    Public Sub getLmForm()
        Dim strWite As String = "", myMain As New main, strTp As String, strTp2 As String = ""
        strWite += "<input type='hidden' name='sid' value='" & shop.SelectedValue & "' />"
        strWite = myMain.userCenterSetOutInfo("关联商品", "<a id='choseGood' href='#'>点击选择商品</a>", strWite)
        strWite += "<input type='hidden' name='gid' value='-1' />"
        strTp = "<select name='discount_1' class='discount' id='discount_1'>"
        For i = 0 To 9
            If i = 0 Then
                strTp2 += "<option value='" & i & "' selected>" & i & "</option>"
            Else
                strTp2 += "<option value='" & i & "'>" & i & "</option>"
            End If
        Next
        strTp += strTp2 & "</select>"
        strTp += "<select name='discount_2' class='discount' id='discount_2'>"
        strTp += strTp2 & "</select>"
        strWite = myMain.userCenterSetOutInfo("联盟折扣：", strTp, strWite)
        strWite = myMain.userCenterSetOutInfo("", "<input class='subBt' type='submit' name='infoSub' value ='提交' /><a class='subBt' href='user_center_sale.aspx?at=sale'>返回</a>", strWite, False)
        Response.Write(strWite)
    End Sub
End Class