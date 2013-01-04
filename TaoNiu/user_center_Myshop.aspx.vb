Public Class user_center_Myshop
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myShop As New shop
        If myShop.hasShop(Session("id")) = False Then
            Response.Redirect("user_center_newShop.aspx?at=newShop")
        End If
        If Request.Params("msg") <> Nothing Then
            If Request.Params("msg") = "noPower" Then
                Response.Write("<script>alert('你没有权限进行此操作')</script>")
            ElseIf Request.Params("msg") = "dell_shelf_ok" Then
                Response.Write("<script>alert('已经成功删除货架')</script>")
            ElseIf Request.Params("msg") = "dellSuccess" Then
                Response.Write("<script>alert('店铺删除成功')</script>")
            ElseIf Request.Params("msg") = "add_success" Then
                Response.Write("<script>alert('店铺添加成功')</script>")
            ElseIf Request.Params("msg") = "error" Then
                Response.Write("<script>alert('无法与服务器交换数据')</script>")
            End If
        End If
    End Sub
    Public Sub getShopList()
        Dim inQuery() As dbHelper.inQuery, myShop As New shop
        inQuery = myShop.getShopList(Session("id"))
        For Each inList As dbHelper.inQuery In inQuery
            Response.Write("<option value='" & inList.value & "'>" & inList.name & "</option>")
        Next
    End Sub
    Public Sub setModfyForm()
        Dim strWrite As String = "", myMain As New main
        Dim myGoods As New goods
        Dim strTp As String = "<select name='range1'>"
        strTp += myGoods.good_send_range_get_str
        strTp += "</select>街道地址：<input type='text' name='range2' style='width:280px;'/>"
        strWrite = myMain.userCenterSetOutInfo("tips:", "开店时添加的地址将成为店铺的主配送区域", strWrite, False)
        strWrite = myMain.userCenterSetOutInfo("店铺地址：", strTp, strWrite, False)

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
        strWrite = myMain.userCenterSetOutInfo("主配送范围：", strTp, strWrite, False)
        strWrite += "<textarea id='goods_range' name='goods_range'></textarea><p id='range_show'></p>"
        strTp = "<select name='send_price'><option value='-1'>不修改</option>"
        For i = 0 To 10
            strTp += "<option value='" & i & "'>￥ " & i & "</option>"
        Next
        strTp += "</select>"
        strWrite = myMain.userCenterSetOutInfo("送货费用：", strTp, strWrite, False)

        strTp = "从<select name='timeStart'><option value='-1'>不修改</option>"
        For i = 0 To 24
            strTp += "<option value='" & i & "'>" & i & "点</option>"
        Next
        strTp += "<select>开始，到<select name='timeEnd'><option value='-1'>不修改</option>"
        For i = 0 To 24
            strTp += "<option value='" & i & "'>" & i & "点</option>"
        Next
        strTp += "<select>结束"
        strWrite = myMain.userCenterSetOutInfo("店铺营业时间：", strTp, strWrite, False)
        Response.Write(strWrite)
    End Sub
End Class