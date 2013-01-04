Public Class user_center_newShop
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myUser As New user
        If myUser.getUserInfo(Session("id")) Is Nothing Then
            Response.Redirect("user_center.aspx?action=modify&msg=needInfo")
        ElseIf myUser.getUserInfo(Session("id"))("id_status") = 0 Then
            Response.Redirect("user_center.aspx?action=real&msg=needId")
        End If
        If Request.Params("msg") <> Nothing Then
            Select Case Request.Params("msg")
                Case "success"
                    Response.Write("<script>alert('店铺添加成功！')</script>")
                Case "infoError"
                    Response.Write("<script>alert('店铺信息不完整或不合乎规范，请检查后重新提交。')</script>")
                Case "dmError"
                    Response.Write("<script>alert('店铺域名已经被别人注册了，请重新选择域名后提交。')</script>")
                Case "error"
                    Response.Write("<script>alert('无法与服务器交换数据。')</script>")
            End Select
        End If
    End Sub
    Public Sub writeNewShopForm()
        Dim strWrite As String = "", myMain As New main
        strWrite = setInfo("店铺名称：", "<span class='red_star'>*</span><input type='text' name='name'/>", strWrite)
        strWrite = setInfo("店铺介绍：", "<span class='red_star'>*</span>", strWrite, False)
        strWrite += "<textarea name='introduce' id='shopIntro' style='width:95%;height:400px;'></textarea>"
        Dim myGoods As New goods
        Dim strTp As String = "<select name='range1'>"
        strTp += myGoods.good_send_range_get_str
        strTp += "</select>街道地址：<span class='red_star'>*</span><input type='text' name='range2' style='width:280px;'/>"
        strWrite = setInfo("tips:", "开店时添加的地址将成为店铺的主配送区域", strWrite, False)
        strWrite = setInfo("店铺地址：", strTp, strWrite, False)

        strTp = "<select id='range1'>"
        Dim myGood As New goods
        strTp += myGood.good_send_range_get_str
        strTp += "<input type='text' id='range2' name='range2' style='width:140px;'/>送到时间：<select id='rangeTime'>"
        For i = 0.5 To 23 Step 0.5
            strTp += "<option value='" & i & "'>" & i & "小时</option>"
        Next
        strTp += "<option value='48'>第二天</option>"
        strTp += "<option value='72'>第三天</option>"
        strTp += "<option value='100'>大于三天</option>"
        strTp += "</select>"
        strTp += "<input type='button' value ='添加' style='width:40px;' id='range_add' class='subBt' />"
        strTp += "<input type='button' value ='清空' style='width:40px;' id='range_reset' class='subBt' />"
        strWrite = myMain.userCenterSetOutInfo("主配送范围：<span class='red_star'>*</span>", strTp, strWrite, False)
        strWrite += "<textarea id='goods_range' name='goods_range'></textarea><p id='range_show'></p>"
        strTp = "<select name='send_price'>"
        For i = 0 To 10
            strTp += "<option value='" & i & "'>￥ " & i & "</option>"
        Next
        strTp += "</select>"
        strWrite = myMain.userCenterSetOutInfo("送货费用：", strTp, strWrite, False)

        strTp = "从<select name='timeStart'>"
        For i = 0 To 24
            strTp += "<option value='" & i & "'>" & i & "点</option>"
        Next
        strTp += "<select>开始，到<select name='timeEnd'>"
        For i = 0 To 24
            strTp += "<option value='" & i & "'>" & i & "点</option>"
        Next
        strTp += "<select>结束"
        strWrite = setInfo("店铺营业时间：", strTp, strWrite, False)
        strWrite = setInfo("店铺域名：<span class='red_star'>*</span>", "<input style='text-align:right;' type='text' name='domain'/>.iTaoNiu.com", strWrite, False)
        strWrite = setInfo("tips:", "开店成功后，您可以在我的店铺中管理您的店铺", strWrite, False)
        strWrite += "<input class='subBt' type='submit' name='infoSub' value ='开店' />"
        Response.Write(strWrite)
    End Sub
    Private Function setInfo(ByVal key As String, ByVal value As String, ByVal strIn As String, Optional ByVal halfWidth As Boolean = True) As String
        Dim strPClass As String = IIf(halfWidth, "class='halfWidth'", "class='fullWidth'")
        strIn += "<p " & strPClass & "><span class='infoTitle'>" & key & "</span><span class='info'>" & value & "</span></p>"
        Return strIn
    End Function
End Class