Public Class user_center_GoodsEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Dim myDs As New dbHelper, sql As String, rows As DataRow
    ''' <summary>
    ''' 获取商品类型
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getGoodsType()
        Dim inQuery() As dbHelper.inQuery, myGoods As New goods, strWite As String = "<p stype='font-weight:bold;'>商品类型："

        sql = "select * from dbo.goods_products where Id=" & Request.QueryString("id")
        rows = myDs.getQuerySql(sql, "goods_products").Tables("goods_products").Rows(0)
        Try
            inQuery = myGoods.get_goods_type
            'Dim iCount As Integer = 1
            For Each inQueryItem As dbHelper.inQuery In inQuery

                If rows("goods_type") = inQueryItem.value Then
                    strWite += "<p><input type='radio' name='goods_type' checked value='" & inQueryItem.value & "' /><span>" & inQueryItem.name & "</span></p>"
                Else
                    strWite += "<p><input type='radio' name='goods_type' value='" & inQueryItem.value & "' /><span>" & inQueryItem.name & "</span></p>"
                End If
            Next
        Catch ex As Exception
            strWite = ex.Message
        End Try
        Response.Write(strWite)
    End Sub
    ''' <summary>
    ''' 获取商品销售类型
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getGoodsSaleType()
        Dim inQuery() As dbHelper.inQuery, myGoods As New goods, strWite As String = "<p stype='font-weight:bold;'>商品销售类型：</p>"

        Try
            inQuery = myGoods.get_goods_sale_type
            For Each inQueryItem As dbHelper.inQuery In inQuery
                If rows("goods_sale_type") = inQueryItem.value Then
                    strWite += "<p><input type='radio' name='goods_sale_type' checked value='" & inQueryItem.value & "' /><span>" & inQueryItem.name & "</span></p>"
                Else
                    strWite += "<p><input type='radio' name='goods_sale_type' value='" & inQueryItem.value & "' /><span>" & inQueryItem.name & "</span></p>"
                End If
            Next

        Catch ex As Exception
            strWite = ex.Message
        End Try
        Response.Write(strWite)
    End Sub
    ''' <summary>
    ''' 获取店铺
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getShopList()
        Dim inQuery() As dbHelper.inQuery, myShop As New shop, strWite As String = ""
        inQuery = myShop.getShopList(Session("id"))
        For Each inList As dbHelper.inQuery In inQuery
            If rows("shopId") = inList.value Then
                strWite += ("<option value='" & inList.value & "' checked>" & inList.name & "</option>")
            Else
                strWite += ("<option value='" & inList.value & "'>" & inList.name & "</option>")
            End If
        Next
        Response.Write(strWite)
    End Sub
    ''' <summary>
    ''' 获取货架
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getShelf()
        Dim myMain As New main, myShop As New shop, strTp As String = "<option value='0'>默认货架</option>"
        Dim shopId As String = myMain.filterCheck(Request.QueryString("shopId"))
        Dim inQuery() As dbHelper.inQuery = myShop.get_shelf_list(shopId)
        For Each ListItem As dbHelper.inQuery In inQuery
            If rows("shelf") = ListItem.value Then
                strTp += "<option value='" & ListItem.value & "' checked>" & ListItem.name & "</option>"
            Else
                strTp += "<option value='" & ListItem.value & "'>" & ListItem.name & "</option>"
            End If
        Next
        Response.Write(strTp)
    End Sub
    ''' <summary>
    ''' 获取商品信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getAddForm()
        Dim strWite As String = "", myMain As New main, strTp As String, strTp2 As String = ""
        strWite = myMain.userCenterSetOutInfo("商品名称：<span class='red_star'>*</span>", "<input name='goods_title' type='text' id='goods_title' value='" & rows("goods_title") & "' />", strWite, False)
        strWite = myMain.userCenterSetOutInfo("商品价格：<span class='red_star teshu'>*</span>", "<input name='goods_prize' type='text' id='goods_prize' value='" & rows("goods_prize") & "' />", strWite)
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
        strWite += "<textarea id='goods_introduce' name='goods_introduce' cols='80' rows='8' style='width:620px;height:300px;'>" & rows("goods_introduce") & "</textarea><br/>"
        strWite = myMain.userCenterSetOutInfo("商品数量：", "<span class='red_star'>*</span><input type='text' name='goods_number' id='goods_number' value='" & rows("goods_number") & "' />", strWite)
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
        If rows("goods_tuijian") = 0 Then
            strTp += "<option value='0' selected>不使用</option>"
        Else
            strTp += "<option value='0' >不使用</option>"
        End If
        If rows("goods_tuijian") = 1 Then
            strTp += "<option value='1' selected>使用</option>"
        Else
            strTp += "<option value='1' >使用</option>"
        End If
        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("橱窗位：", strTp, strWite)

        strTp = "<select name='pay_type' id='pay_type'>"
        If rows("goods_pay_type") = 0 Then
            strTp += "<option value='0' selected>全部</option>"
        Else
            strTp += "<option value='0'>全部</option>"
        End If
        If rows("goods_pay_type") = 1 Then
            strTp += "<option value='1' selected>仅在线支付</option>"
        Else
            strTp += "<option value='1'>仅在线支付</option>"
        End If
        If rows("goods_pay_type") = 2 Then
            strTp += "<option value='2' selected>仅货到付款</option>"
        Else
            strTp += "<option value='2'>仅货到付款</option>"
        End If

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
            If rows("send_price") = i Then
                strTp += "<option value='" & i & "' selected>￥ " & i & "</option>"
            Else
                strTp += "<option value='" & i & "'>￥ " & i & "</option>"
            End If

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
        If rows("goods_invoice") = 1 Then
            strTp += "<option value='1 selected>有</option>"
        Else
            strTp += "<option value='1'>有</option>"
        End If
        If rows("goods_invoice") = 0 Then
            strTp += "<option value='0' selected>没有</option>"
        Else
            strTp += "<option value='0'>没有</option>"
        End If

        strTp += "</select>"
        strWite = myMain.userCenterSetOutInfo("有无发票：", strTp, strWite)

        strTp = "<select name='goods_warranty_type' id='goods_warranty_type'>"
        If rows("goods_warranty_type") = 1 Then
            strTp += "<option value='1' selected>店保</option>"
        Else
            strTp += "<option value='1'>店保</option>"
        End If
        If rows("goods_warranty_type") = 2 Then
            strTp += "<option value='2' selected>联保</option>"
        Else
            strTp += "<option value='2'>联保</option>"
        End If
        If rows("goods_warranty_type") = 0 Then
            strTp += "<option value='0' selected>无保</option>"
        Else
            strTp += "<option value='0'>无保</option>"
        End If

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
        strWite += "<textarea id='goods_warranty_other' name='goods_warranty_other' >" & rows("goods_warranty_other") & "</textarea>"
        strWite = myMain.userCenterSetOutInfo("托管事项：", "(只有导购可见，无托管业务可不填写。)", strWite, False)
        strWite += "<textarea id='goods_tu' name='goods_tu' >" & rows("goods_tu") & "</textarea>"

        strWite += "<input type='hidden' name='goods_on_sale_date'id='goods_on_sale_date' value='" & DateTime.Now.ToString & "' />"
        strWite += "<input type='submit' name='subForm' value='发布' id='subForm' class='subBt' />"
        Response.Write(strWite)
    End Sub
End Class