Public Class shop_goods1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Request.Params("action")
            Case "goods_cat"
                Dim myGoods As New goods, myMain As New main, inQuery() As dbHelper.inQuery
                Dim catDeep As Integer = myMain.filterCheck(Request.Form("deep"))
                If Request.Form("father") <> 0 Then
                    Dim catFather As Integer
                    catFather = myMain.filterCheck(Request.Form("father"))
                    inQuery = myGoods.cat_get(catDeep, catFather)
                Else
                    inQuery = myGoods.cat_get(catDeep)
                End If
                For Each queryItem As dbHelper.inQuery In inQuery
                    Response.Write("<option value='" & queryItem.value & "'>" & queryItem.name & "</option>")
                Next
            Case "goods_type"
                Dim inQuery() As dbHelper.inQuery, myGoods As New goods, strWite As String = "<p stype='font-weight:bold;'>商品类型："
                Try
                    inQuery = myGoods.get_goods_type
                    Dim iCount As Integer = 1
                    For Each inQueryItem As dbHelper.inQuery In inQuery
                        If iCount = 1 Then
                            strWite += "<p><input type='radio' name='goods_type' checked value='" & inQueryItem.value & "' /><span>" & inQueryItem.name & "</span></p>"
                        Else
                            strWite += "<p><input type='radio' name='goods_type' value='" & inQueryItem.value & "' /><span>" & inQueryItem.name & "</span></p>"
                        End If
                        iCount += 1
                    Next
                Catch ex As Exception
                    strWite = ex.Message
                End Try
                Response.Write(strWite)
            Case "goods_sale_type"
                Dim inQuery() As dbHelper.inQuery, myGoods As New goods, strWite As String = "<p stype='font-weight:bold;'>商品销售类型：</p>"
                Try
                    inQuery = myGoods.get_goods_sale_type
                    Dim iCount As Integer = 1
                    For Each inQueryItem As dbHelper.inQuery In inQuery
                        If iCount = 1 Then
                            strWite += "<p><input type='radio' name='goods_sale_type' checked value='" & inQueryItem.value & "' /><span>" & inQueryItem.name & "</span></p>"
                        Else
                            strWite += "<p><input type='radio' name='goods_sale_type' value='" & inQueryItem.value & "' /><span>" & inQueryItem.name & "</span></p>"
                        End If
                        iCount += 1
                    Next
                Catch ex As Exception
                    strWite = ex.Message
                End Try
                Response.Write(strWite)
            Case "shelfList"
                Dim myMain As New main, myShop As New shop, strTp As String = "<option value='0' selected>默认货架</option>"
                Dim shopId As String = myMain.filterCheck(Request.Form("shopId"))
                Dim inQuery() As dbHelper.inQuery = myShop.get_shelf_list(shopId)
                For Each ListItem As dbHelper.inQuery In inQuery
                    strTp += "<option value='" & ListItem.value & "'>" & ListItem.name & "</option>"
                Next
                Response.Write(strTp)
            Case "cat_ready_add"
                Dim myGoods As New goods
                Response.Write(myGoods.cat_ready_add(Request.Form("catName"), 4, Request.Form("father")))
            Case "chuchuang"
                Dim myMain As New main, myShop As New shop, strTp As String = ""
                Dim shopId As String = myMain.filterCheck(Request.Form("shopId"))
                If myShop.has_showCase(shopId) Then
                    strTp = "<option value='0'>不使用</option><option value='1' selected>使用</option>"
                Else
                    strTp = "<option value='0'>不使用</option>"
                End If
                Response.Write(strTp)
            Case "getProperty"
                Dim myGoods As New goods
                Try
                    Response.Write(myGoods.cat_property_get(Request.Form("cat")))
                Catch ex As Exception
                    Response.Write("")
                End Try
            Case "getRange"
                Dim myGoods As New goods
                Dim strWite As String = myGoods.good_send_range_get_str(Request.Params("father"))
                Response.Write(strWite)
            Case "goods_add"
                goods_add()
        End Select
    End Sub
    Private Sub goods_add()
        Dim myGoods As New goods
        Dim newGoods As New Hashtable
        newGoods("@shopId") = "-1"
        newGoods("@ownerId") = Session("id")
        newGoods("@goods_cat") = Request.Form("goods_cat")
        newGoods("@goods_type") = Request.Form("goods_type")
        newGoods("@goods_sale_type") = Request.Form("goods_sale_type")
        newGoods("@goods_title") = Request.Form("goods_title")
        Dim myImage As New images, hashRt As New Hashtable
        hashRt = myImage.upLoadImg(Request.Files("imgFile"), Session("email"), Server.MapPath("~"))
        If hashRt("status") = "ok" Then
            newGoods("@goods_picture") = hashRt("smallPath")
        Else
            Response.Redirect("~/user_center_Goods.aspx?at=goods&action=add---0&msg=" & hashRt("message"))
        End If
        newGoods("@goods_prize") = Request.Form("goods_prize")
        newGoods("@goods_discount") = Request.Form("goods_discount")
        newGoods("@goods_info") = Request.Form("goods_info")
        newGoods("@goods_introduce") = Request.Form("goods_introduce")
        newGoods("@goods_number") = Request.Form("goods_number")
        newGoods("@goods_on_sale_date") = Convert.ToDateTime(Request.Form("goods_on_sale_date"))
        newGoods("@goods_tuijian") = Request.Form("goods_tuijian")
        newGoods("@goods_range") = Request.Form("goods_range")
        newGoods("@goods_period") = Convert.ToDateTime(newGoods("@goods_on_sale_date")).AddDays(Request.Form("goods_Period"))
        newGoods("@goods_invoice") = Request.Form("goods_invoice")
        newGoods("@goods_warranty_type") = Request.Form("goods_warranty_type")
        newGoods("@goods_warranty_time") = Request.Form("goods_warranty_time")
        newGoods("@goods_warranty_other") = Request.Form("goods_warranty_other")
        newGoods("@shelf") = Request.Form("shelf")
        newGoods("@send_price") = Request.Form("send_price")
        newGoods("@goods_tu") = Request.Form("goods_tu")
        newGoods("@goods_pay_type") = Request.Form("pay_type")
        hashRt = myGoods.checkProduct(newGoods)
        If hashRt("result") = False Then
            Response.Redirect("~/user_center_Goods.aspx?at=goods&action=add---0&msg=" & hashRt("message"))
        Else
            Dim goodId As Integer = myGoods.goods_add(newGoods)
            If (goodId > 0) Then
                Dim theLuru As New Hashtable, myWeiTuo As New weituo
                theLuru("@goodName") = newGoods("@goods_title")
                theLuru("@gid") = goodId
                theLuru("@uid") = Session("id")
                theLuru("@shopId") = Request.Form("shopId")
                myWeiTuo.addWtGood(theLuru)
            End If
        End If
        Response.Redirect("../taskList.aspx?at=tl")
    End Sub
End Class