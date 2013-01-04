Public Class user_center_Goods
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myShop As New shop
        If myShop.hasShop(Session("id")) = False Then
            Response.Redirect("user_center_newShop.aspx?at=newShop")
        End If
        If Request.Params("msg") <> Nothing Then
            Response.Write("<script>alert('" & Request.Params("msg") & "')</script>")
        End If
    End Sub
    Public Sub getShopList()
        Dim inQuery() As dbHelper.inQuery, myShop As New shop
        inQuery = myShop.getShopList(Session("id"))
        For Each inList As dbHelper.inQuery In inQuery
            Response.Write("<option value='" & inList.value & "'>" & inList.name & "</option>")
        Next
    End Sub
    Public Sub getAddForm()
        Dim strWite As String = "", myMain As New main, strTp As String, strTp2 As String = ""
        strWite = myMain.userCenterSetOutInfo("商品名称：<span class='red_star'>*</span>", "<input name='goods_title' type='text' id='goods_title' />", strWite, False)
        strWite = myMain.userCenterSetOutInfo("商品价格：<span class='red_star teshu'>*</span>", "<input name='goods_prize' type='text' id='goods_prize' />", strWite)
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
        strWite = myMain.userCenterSetOutInfo("商品折扣：", strTp, strWite)
        strWite = myMain.userCenterSetOutInfo("商品图片：<span class='red_star'>*</span>", "<input type='file' id='imgFile' name='imgFile' />", strWite)

        Dim myGood As New goods
        strWite += "<div id='goods_property'></div>"

        strWite = myMain.userCenterSetOutInfo("商品介绍：<span class='red_star'>*</span>", "", strWite, False)
        strWite += "<textarea id='goods_introduce' name='goods_introduce' cols='80' rows='8' style='width:620px;height:300px;'></textarea><br/>"
        strWite = myMain.userCenterSetOutInfo("商品数量：", "<span class='red_star'>*</span><input type='text' name='goods_number' id='goods_number' />", strWite)
        strTp = "<select name='on_sale_date_type' id='on_sale_date_type'>"
        strTp += "<option value='now' selected>立即</option>"
        strTp += "<option value='datetime'>选择时间</option>"
        strTp += "</select>"
        strTp2 = "<select class='hidden saleTime' name='sale_year' id='sale_year'>"
        For i = 0 To 3
            If i = 0 Then
                strTp2 += "<option value='" & DateTime.Now.Year + i & "' selected>" & DateTime.Now.Year + i & "</option>"
            Else
                strTp2 += "<option value='" & DateTime.Now.Year + i & "'>" & DateTime.Now.Year + i & "</option>"
            End If
        Next
        strTp2 += "</select>"
        strTp2 += "<span class='info tips hidden'>年</span><select class='hidden saleTime' name='sale_month' id='sale_month'>"
        For i = 1 To 12
            If i = 1 Then
                strTp2 += "<option value='" & i & "' selected>" & i & "</option>"
            Else
                strTp2 += "<option value='" & i & "'>" & i & "</option>"
            End If
        Next
        strTp2 += "</select>"
        strTp2 += "<span class='info tips hidden'>月</span><select class='hidden saleTime' name='sale_day' id='sale_day'>"
        For i = 1 To 31
            If i = 1 Then
                strTp2 += "<option value='" & i & "' selected>" & i & "</option>"
            Else
                strTp2 += "<option value='" & i & "'>" & i & "</option>"
            End If
        Next
        strTp2 += "</select>"
        strTp2 += "<span class='info tips hidden'>日</span><select class='hidden saleTime' name='sale_hour' id='sale_hour'>"
        For i = 1 To 24
            If i = 1 Then
                strTp2 += "<option value='" & i & "' selected>" & i & "</option>"
            Else
                strTp2 += "<option value='" & i & "'>" & i & "</option>"
            End If
        Next
        strTp2 += "</select>"
        strTp2 += "<span class='info tips hidden'>时</span><select class='hidden saleTime' name='sale_min' id='sale_min'>"
        For i = 0 To 59
            If i = 0 Then
                strTp2 += "<option value='" & i & "' selected>" & i & "</option>"
            Else
                strTp2 += "<option value='" & i & "'>" & i & "</option>"
            End If
        Next
        strTp2 += "</select><span class='info tips hidden'>分</span>"
        strTp += strTp2
        strWite = myMain.userCenterSetOutInfo("开售时间：", strTp, strWite, False)
        strTp = "<select name='goods_tuijian' id='goods_tuijian'>"
        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("橱窗位：", strTp, strWite)

        strTp = "<select name='pay_type' id='pay_type'>"
        strTp += "<option value='0'>全部</option>"
        strTp += "<option value='1'>仅在线支付</option>"
        strTp += "<option value='2'>仅货到付款</option>"
        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("付款方式：", strTp, strWite)

        strTp = "<select id='range1'>"
        strTp += myGood.good_send_range_get_option
        strTp += "<input type='text' id='range2' name='range2' style='width:140px;'/>"
        strTp += "</select>送到时间：<select id='rangeTime'>"
        For i = 0.5 To 23 Step 0.5
            strTp += "<option value='" & i & "'>" & i & "小时</option>"
        Next
        strTp += "<option value='48'>第二天</option>"
        strTp += "<option value='72'>第三天</option>"
        strTp += "<option value='100'>大于三天</option>"
        strTp += "</select>"
        strTp += "<input type='button' value ='添加' style='width:40px;' id='range_add' class='subBt' />"
        strTp += "<input type='button' value ='清空' style='width:40px;' id='range_reset' class='subBt' />"
        strWite = myMain.userCenterSetOutInfo("送货范围：<span class='red_star'>*</span>", strTp, strWite, False)
        strWite += "<textarea id='goods_range' name='goods_range'></textarea><p id='range_show'></p>"
        strTp = "<select name='send_price'>"
        For i = 0 To 10
            strTp += "<option value='" & i & "'>￥ " & i & "</option>"
        Next
        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("送货费用：", strTp, strWite, False)
        strTp2 = "<select name='goods_Period' id='goods_Period'>"
        For i = 7 To 60
            If i = 7 Then
                strTp2 += "<option value='" & i & "' selected>" & i & "</option>"
            Else
                strTp2 += "<option value='" & i & "'>" & i & "</option>"
            End If
        Next
        strTp2 += "</select>"
        strWite = myMain.userCenterSetOutInfo("有效期(天)：", strTp2, strWite)

        strTp = "<select name='goods_invoice' id='goods_invoice'>"
        strTp += "<option value='1'>有</option>"
        strTp += "<option value='0' selected>没有</option>"
        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("有无发票：", strTp, strWite)

        strTp = "<select name='goods_warranty_type' id='goods_warranty_type'>"
        strTp += "<option value='1' selected>店保</option>"
        strTp += "<option value='2'>联保</option>"
        strTp += "<option value='0'>无保</option>"
        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("保修类型：", strTp, strWite)

        strTp2 = "<select name='goods_warranty_time' id='goods_warranty_time'>"
        For i = 1 To 60
            If i = 12 Then
                strTp2 += "<option value='" & i & "' selected>" & i & "</option>"
            Else
                strTp2 += "<option value='" & i & "'>" & i & "</option>"
            End If
        Next
        strTp2 += "</select>"
        strWite = myMain.userCenterSetOutInfo("保修时长(月)：", strTp2, strWite)

        strWite = myMain.userCenterSetOutInfo("保修条款：<span class='red_star'>*</span>", "", strWite, False)
        strWite += "<textarea id='goods_warranty_other' name='goods_warranty_other'></textarea>"
        strWite = myMain.userCenterSetOutInfo("托管事项：", "(只有导购可见，无托管业务可不填写。)", strWite, False)
        strWite += "<textarea id='goods_tu' name='goods_tu'></textarea>"

        strWite += "<input type='hidden' name='goods_on_sale_date'id='goods_on_sale_date' value='" & DateTime.Now.ToString & "' />"
        strWite += "<input type='submit' name='subForm' value='发布' id='subForm' class='subBt' />"
        Response.Write(strWite)
    End Sub
    Public Sub getLuRuList()
        Dim prePage As Integer = 10, page As Integer = IIf(Request.Params("page") = Nothing, 1, Request.Params("page"))
        Dim goodsList As Hashtable(), myWeituo As New weituo
        goodsList = myWeituo.getWtListByOwner(1, Session("id"), page, prePage)
        If goodsList(0)("count") < 1 Then Exit Sub
        Dim strWite As String = "", myMain As New main, strTp As String
        For Each good As Hashtable In goodsList
            strTp = "<a href='../shopProduct.aspx?shop=-1&gid=" & good("gid") & "'>【" & good("goodName") & "】</a>--" & IIf(good("status") = 1, "[未审核]", "[通过]") & "---提交日期：" & good("luruDate")
            strWite = myMain.userCenterSetOutInfo("<input type='checkbox' value='" & good("id") & "' name='passed' />", strTp, strWite, False)
        Next
        Dim pageHelper As New pageLink
        strWite += "<input type='submit' name='subForm' value='通过' class='subBt'>"
        Response.Write(strWite)
    End Sub
End Class