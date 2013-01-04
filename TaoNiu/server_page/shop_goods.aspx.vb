Imports System.IO
Imports System.Globalization

Public Class shop_goods
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Select Case Request.Params("action")
            Case "newShop"
                Dim newShop As New Hashtable
                Dim myShop As New shop
                newShop("@shop_owner") = Session("id")
                newShop("@shop_name") = Request.Form("name")
                newShop("@shop_introduce") = Request.Form("introduce")
                newShop("@shop_address") = Request.Form("range1") & "-" & Request.Form("range2")
                newShop("@shop_domain") = Request.Form("domain")
                Dim domainCheck As New Hashtable
                domainCheck("@domain") = Request.Form("domain")
                If myShop.check_domain(domainCheck) = False Then
                    Response.Redirect("~/user_center_newShop.aspx?at=newShop&msg=dmError")
                End If
                If myShop.check(newShop) Then
                    newShop("@shop_certification") = ""
                    newShop("@start_time") = Request.Form("timeStart")
                    newShop("@end_time") = Request.Form("timeEnd")
                    newShop("@send_fee") = Request.Form("send_fee")
                    If (myShop.add(newShop, Request.Form("goods_range"))) Then
                        Response.Redirect("~/user_center_myShop.aspx?at=myShop&msg=add_success")
                    Else
                        Response.Redirect("~/user_center_newShop.aspx?at=newShop&msg=error")
                    End If
                Else
                    Response.Redirect("~/user_center_newShop.aspx?at=newShop&msg=infoError")
                End If
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
            Case "getShopInfo"
                Dim myMain As New main, strRt As String = "", rt As New Hashtable, myShop As New shop
                Dim shopId As String = myMain.filterCheck(Request.Form("shopId"))
                rt = myShop.getShopInfo(shopId)
                If rt("status") = "ok" Then
                    Dim strWite As String = ""
                    strWite = myMain.userCenterSetOutInfo("店铺名称：", rt("shop_name"), strWite)
                    Dim strTp As String
                    If rt("shop_status") = 1 Then
                        strTp = "正常"
                    ElseIf rt("shop_status") = 2 Then
                        strTp = "关闭"
                    Else
                        strTp = "封停"
                    End If
                    strWite = myMain.userCenterSetOutInfo("店铺状态：", strTp, strWite)
                    strWite = myMain.userCenterSetOutInfo("店铺地址：", rt("shop_address"), strWite, False)
                    strWite = myMain.userCenterSetOutInfo("店铺介绍：", rt("shop_introduce"), strWite, False)
                    strWite = myMain.userCenterSetOutInfo("店铺橱窗数：", rt("shop_showcase") & "个", strWite)
                    Response.Write(strWite)
                Else
                    Response.Write("<h3>无法与服务器交换数据</h3>")
                End If
            Case "getShopShelf"
                Dim myMain As New main, myShop As New shop, strTp As String = "", strLink As String
                Dim shopId As String = myMain.filterCheck(Request.Params("shopId"))
                Dim inQuery() As dbHelper.inQuery = myShop.get_shelf_list(shopId)
                For Each ListItem As dbHelper.inQuery In inQuery
                    strLink = "<span  class='delShelf'>" & ListItem.name & "</span>"
                    strLink += "<a class='subBt' href='server_page/shop_goods.aspx?action=del_shop_shelf&shopId=" & shopId & "&shelf=" & ListItem.value & "&moveTo='>删除，并将货架下商品移动到：</a>"
                    strLink += "<select class='moveToShelf'>"
                    For Each lItem As dbHelper.inQuery In inQuery
                        If ListItem.value <> lItem.value Then
                            strLink += "<option value='" & lItem.value & "'>" & lItem.name & "</option>"
                        End If
                    Next
                    strLink += "</select>"
                    strTp = myMain.userCenterSetOutInfo("货架名称：", strLink, strTp, False)
                Next
                Response.Write(strTp)
            Case "add_shop_shelf"
                Dim myShop As New shop, myMain As New main, rt As Integer
                Dim shopId As Integer = myMain.filterCheck(Request.Form("shopId"))
                Dim shelfName As String = myMain.filterCheck(Request.Form("shelfName"))
                If myMain.checkIsNotNull(shopId) = False Or myMain.checkIsNotNull(shelfName) = False Then
                    Response.Write("infoError")
                Else
                    Dim shelfId As String = myShop.add_shelf(shelfName, shopId), strLink As String, strTp As String = ""
                    If shelfId = 0 Then
                        Response.Write(0)
                        Exit Sub
                    End If
                    strLink = "<span  class='delShelf'>" & shelfName & "</span>"
                    strLink += "<a class='subBt' href='server_page/shop_goods.aspx?action=del_shop_shelf&shopId=" & shopId & "&shelf=" & shelfId & "&moveTo='>删除，并将货架下商品移动到：</a>"
                    strLink += "<select class='moveToShelf'>"
                    Dim inQuery() As dbHelper.inQuery = myShop.get_shelf_list(shopId)
                    For Each lItem As dbHelper.inQuery In inQuery
                        If shelfId <> lItem.value Then
                            strLink += "<option value='" & lItem.value & "'>" & lItem.name & "</option>"
                        End If
                    Next
                    strLink += "</select>"
                    strTp = myMain.userCenterSetOutInfo("货架名称：", strLink, strTp, False)
                    Response.Write(strTp)
                End If
            Case "del_shop_shelf"
                Dim myMain As New main, myShop As New shop
                Dim shopId = myMain.filterCheck(Request.Params("shopId"))
                Dim shelf = myMain.filterCheck(Request.Params("shelf"))
                Dim moveTo = myMain.filterCheck(Request.Params("moveTo"))
                If myShop.hasShop(Session("id"), shopId) Then
                    If myShop.shelf_dell(shelf, moveTo) Then
                        Response.Redirect("~/user_center_Myshop.aspx?at=myShop&msg=dell_shelf_ok")
                    Else
                        Response.Redirect("~/user_center_Myshop.aspx?at=myShop&msg=error")
                    End If
                Else
                    Response.Redirect("~/user_center_Myshop.aspx?at=myShop&msg=noPower")
                End If
            Case "shopModify"
                Dim shopId As Integer, myMain As New main, shopAddress As String, shopStatus As Integer, shopIntroduce As String
                Dim start_time As Integer, end_time As Integer, send_fee As Integer
                shopId = myMain.filterCheck(Request.Form("shopId"))
                Dim shopOwner As Integer = myMain.filterCheck(Session("id"))
                Dim myShop As New shop
                If myShop.hasShop(shopOwner, shopId) Then
                    shopAddress = myMain.filterCheck(Request.Form("range1")) & "-" & myMain.filterCheck(Request.Form("range2"))
                    shopIntroduce = myMain.filterCheck(Request.Form("shop_introduce"))
                    shopStatus = myMain.filterCheck(Request.Form("shop_status"))
                    start_time = myMain.filterCheck(Request.Form("timeStart"))
                    end_time = myMain.filterCheck(Request.Form("timeEnd"))
                    send_fee = myMain.filterCheck(Request.Form("send_price"))
                    If shopStatus = 3 Then
                        If (myShop.del_shop(shopId)) Then
                            Response.Redirect("~/user_center_Myshop.aspx?at=myShop&msg=dellSuccess")
                        Else
                            Response.Redirect("~/user_center_Myshop.aspx?at=myShop&msg=dellError")
                        End If
                    End If
                    Dim shopInfo As New Hashtable, shopInfoNow As New Hashtable
                    shopInfo = myShop.getShopInfo(shopId)
                    If Len(myMain.filterCheck(Request.Form("range2"))) = 0 Then
                        shopAddress = shopInfo("shop_address")
                    End If
                    If myMain.checkIsNotNull(shopIntroduce) = False Then
                        shopIntroduce = shopInfo("shop_introduce")
                    End If
                    If myMain.checkIsNotNull(shopStatus) = False Then
                        shopStatus = shopInfo("shop_status")
                    End If
                    If send_fee = -1 Then
                        send_fee = shopInfo("send_fee")
                    End If
                    If start_time = -1 Then
                        start_time = shopInfo("start_time")
                    End If
                    If end_time = -1 Then
                        end_time = shopInfo("end_time")
                    End If
                    shopInfoNow("@shopId") = shopId
                    shopInfoNow("@shop_introduce") = shopIntroduce
                    shopInfoNow("@shop_address") = shopAddress
                    shopInfoNow("@shop_status") = shopStatus
                    shopInfoNow("@start_time") = start_time
                    shopInfoNow("@end_time") = end_time
                    shopInfoNow("@send_fee") = send_fee
                    If myShop.shop_info_modify(shopInfoNow, Request.Form("goods_range")) Then
                        Response.Redirect("~/user_center_Myshop.aspx?at=myShop")
                    Else
                        Response.Redirect("~/user_center_Myshop.aspx?at=myShop&msg=error")
                    End If
                Else
                    Response.Redirect("~/user_center_Myshop.aspx?at=myShop&msg=noPower")
                End If
            Case "shelfList"
                Dim myMain As New main, myShop As New shop, strTp As String = "<option value='0' selected>默认货架</option>"
                Dim shopId As String = myMain.filterCheck(Request.Form("shopId"))
                Dim inQuery() As dbHelper.inQuery = myShop.get_shelf_list(shopId)
                For Each ListItem As dbHelper.inQuery In inQuery
                    strTp += "<option value='" & ListItem.value & "'>" & ListItem.name & "</option>"
                Next
                Response.Write(strTp)
            Case "goodList"
                Dim strWite As String = "", mySearch As New searchHelper, goodsList As Hashtable()
                goodsList = mySearch.goods_get_fromShelf(Request.Form("shopId"), Request.Form("shelfId"))
                For Each gItem In goodsList
                    strWite += "<option value='" & gItem("id") & "'>" & gItem("goods_title") & "</option>"
                Next
                Response.Write(strWite)
            Case "goods_add"
                Dim myGoods As New goods
                Dim newGoods As New Hashtable
                newGoods("@shopId") = Request.Form("shopId")
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
                    If (myGoods.goods_add(newGoods) > 0) Then
                        Response.Redirect("~/user_center_Goods.aspx?at=goods&action=add---0&msg=商品添加成功")
                    Else
                        Response.Redirect("~/user_center_Goods.aspx?at=goods&action=add---0&msg=无法与服务器交换数据")
                    End If
                End If
            Case "pj"
                Dim strWite As String
                strWite = "<ul id='pj'>"
                Dim myGoods As New goods, comments() As Hashtable
                Dim myOrder As New orders, order As New Hashtable
                comments = myGoods.goods_pj_get(Request.Params("gid"))
                If comments(0)("count") = 0 Then Exit Sub
                For Each comment As Hashtable In comments
                    order = myOrder.order_get_byId(comment("oid"))
                    strWite += "<li><p>" & comment("comments") & "</p>"
                    strWite += "<p class='commentTime'>" & order("time") & "</p></li>"
                Next
                strWite += "</ul>"
                Response.Write(strWite)
            Case "orders"
                Dim strWite As String
                strWite = "<ul id='orders'>"
                Dim myOrder As New orders, orders() As Hashtable
                Dim myUser As New user, user As New Hashtable, strTp As String = ""
                orders = myOrder.order_get_byGood(Request.Params("gid"))
                If orders(0)("count") = 0 Then Exit Sub
                For Each order As Hashtable In orders
                    user = myUser.getUserInfo(order("customer"))
                    strWite += "<li>日期："
                    If order("buy_pj") = 1 Then
                        strTp = "好评"
                    ElseIf order("buy_pj") = 2 Then
                        strTp = "中评"
                    ElseIf order("buy_pj") = 0 Then
                        strTp = "未评价"
                    Else
                        strWite = "差评"
                    End If
                    strWite += order("time") & "&nbsp;评价：" & strTp & "&nbsp;买家：" & user("niceName")
                    strWite += "</li>"
                Next
                strWite += "</ul>"
                Response.Write(strWite)
            Case "services"
                Dim strWite As String
                Dim good As New Hashtable, myGoods As New goods
                good = myGoods.goods_get(Request.Params("gid"))
                Dim strTp As String = ""
                If good("goods_warranty_type") = 0 Then
                    strWite = "<ul id='warranty'><li>保修类型：无保修</li></ul>"
                Else
                    If good("goods_warranty_type") = 1 Then
                        strTp = "店保"
                    Else
                        strTp = "全国联保"
                    End If
                    strWite = "<ul id='warranty'><li>保修类型：" & strTp & "</li>"
                    strWite += "<li>保修时长(月)：" & good("goods_warranty_time") & "</li></ul>"
                    strWite += "<div>保修补充：" & good("goods_warranty_other") & "</div>"
                End If
                strWite += "<div class='clear'></div>"
                Response.Write(strWite)
            Case "info"
                Dim strWite As String
                Dim good As New Hashtable, myGoods As New goods
                good = myGoods.goods_get(Request.Params("gid"))
                Dim strTp As String = good("goods_info")
                strWite = "<ul id='infoList'>"
                Dim info() As String = strTp.Split("-")
                For Each infoItem As String In info
                    strWite += "<li>"
                    strWite += Left(infoItem, infoItem.IndexOf("=")) & ":"
                    strWite += Right(infoItem, Len(infoItem) - infoItem.IndexOf("=") - 1)
                    strWite += "</li>"
                Next
                strWite += "<div class='clear'></div></ul>" & good("goods_introduce")
                strWite += "<div class='clear'></div>"
                Response.Write(strWite)
            Case "chuchuang"
                Dim myMain As New main, myShop As New shop, strTp As String = ""
                Dim shopId As String = myMain.filterCheck(Request.Form("shopId"))
                If myShop.has_showCase(shopId) Then
                    strTp = "<option value='0'>不使用</option><option value='1' selected>使用</option>"
                Else
                    strTp = "<option value='0'>不使用</option>"
                End If
                Response.Write(strTp)
            Case "liuYan"
                Dim myMain As New main, myShop As New shop, strWite As String = ""
                Dim myLiuYan() As Hashtable = myShop.get_liuYan(Request.Params("gid"))
                strWite += "<ul id='pj'>"
                Dim myUser As New user
                For Each liuYan As Hashtable In myLiuYan
                    If myLiuYan(0)("count") = 0 Then Exit For
                    strWite += "<li><p style='color:red;'>"
                    strWite += myUser.getUserInfo(liuYan("sendBy"))("niceName") & "&nbsp;says:</p>"
                    strWite += "<p>" & liuYan("comments") & "</p>"
                    strWite += "<p style='color:#999;'>" & liuYan("comDate") & "</p></li>"
                Next
                strWite += "</ul>"
                Dim shopId As Integer = myShop.get_goodShop(Request.Params("gid"))
                Dim strRedirectURL As String = "shopProduct.aspx?shop=" & shopId & "&gid=" & Request.Params("gid")
                strRedirectURL = strRedirectURL.Replace("&", "----")
                If Session("onTheWay") <> "loginSuccess" Then
                    strWite += "<p><a href='login.aspx?redirectURL=" & strRedirectURL & "'>登录</a>，给卖家留言</p>"
                Else
                    strWite += "<h2>发布新评论：</h2>"
                    strWite += "<div id='messageForm'><form method='post' action='server_page/shop_goods.aspx?action=liuYanPost&gid=" & _
                        Request.Params("gid") & "&redirectURL=" & myMain.siteUrl & strRedirectURL & "' >"
                    strWite += "<textarea name='message'></textarea>"
                    strWite += "<input type='hidden' name='uid' value='" & Session("id") & "'/>"
                    strWite += "<input type='hidden' name='shopId' value='" & shopId & "'><br/>"
                    strWite += "<input type='submit' value ='提交' id='catOk' class='subBt' />"
                    strWite += "</form></div>"
                End If
                Response.Write(strWite)
            Case "liuYanPost"
                Dim myShop As New shop
                myShop.liuYan_add(Session("id"), Request.Form("message"), Request.Form("shopId"))
                Response.Redirect(Request.Params("redirectURL").ToString.Replace("----", "&"))
            Case "getRange"
                Dim myGoods As New goods
                Dim strWite As String = myGoods.good_send_range_get_str(Request.Params("father"))
                Response.Write(strWite)
            Case "cat_ready_add"
                Dim myGoods As New goods
                Response.Write(myGoods.cat_ready_add(Request.Form("catName"), 4, Request.Form("father")))
            Case "addCe"
                addCe()
            Case "addTu"
                addTu()
            Case "delTu"
                delTu()
            Case "getProperty"
                getProperty()
            Case "addWt"
                addWt()
            Case "delWt"
                delWt()
            Case "weituo_pass"
                weituo_pass()
            Case Else
                Response.Write("Good Luck~")
        End Select
    End Sub
    Private Sub addCe()
        Dim theCe As New Hashtable, myShop As New shop, myMoney As New tnPay.money, theMoney As New Hashtable
        theCe = myShop.getCe(Request.Form("ceType"))
        If myShop.hasThisCe(Request.Form("shopId"), Request.Form("ceType")) Then
            Response.Redirect("~/user_center_tools_certification.aspx?at=tools&msg=had")
        End If
        If theCe("fee") > 0 Then
            Try
                theMoney = myMoney.getMoney(Session("id"))
                If theCe("fee") > theMoney("money") Then
                    Response.Redirect("~/user_center_money.aspx?at=money&msg=need&price=" & theCe("fee"))
                Else
                    ''扣款操作
                End If
            Catch ex As Exception
                Response.Redirect("~/user_center_money.aspx?at=money&msg=need&price=" & theCe("fee"))
            End Try
        End If
        myShop.addCeApply(Request.Form("shopId"), Request.Form("ceType"), Request.Form("ceContent"))
        Response.Redirect("~/user_center_tools_certification.aspx?at=tools&msg=ok")
    End Sub
    Private Sub addTu()
        Dim theCe As New Hashtable, myTu As New trusteeships, myMoney As New tnPay.money, theMoney As New Hashtable
        theCe = myTu.getTuType(Request.Form("tuType"))
        If myTu.hasTu(Request.Form("shopId")) Then
            Response.Redirect("~/user_center_tools_trusteeship.aspx?at=tools&msg=had")
        End If
        If theCe("fee") > 0 Then
            Try
                theMoney = myMoney.getMoney(Session("id"))
                If theCe("fee") > theMoney("money") Then
                    Response.Redirect("~/user_center_money.aspx?at=money&msg=need&price=" & theCe("fee"))
                Else
                    ''扣款操作
                End If
            Catch ex As Exception
                Response.Redirect("~/user_center_money.aspx?at=money&msg=need&price=" & theCe("fee"))
            End Try
        End If
        myTu.addTu(Request.Form("shopId"), Request.Form("tuType"), Request.Form("tuId"))
        Response.Redirect("~/user_center_tools_trusteeship.aspx?at=tools&msg=ok")
    End Sub
    Private Sub delTu()
        Dim myShop As New shop, myTu As New trusteeships
        Dim theTu As New Hashtable
        theTu = myTu.getTu(Request.Params("id"))
        If myShop.hasShop(Session("id"), theTu("shopId")) = False Then Response.Redirect("~/user_center_tools_trusteeship.aspx?at=tools")
        myTu.delTu(Request.Params("id"))
        Response.Redirect("~/user_center_tools_trusteeship.aspx?at=tools&msg=ok")
    End Sub
    Private Sub getProperty()
        Dim myGoods As New goods
        Try
            Response.Write(myGoods.cat_property_get(Request.Form("cat")))
        Catch ex As Exception
            Response.Write("")
        End Try
    End Sub
    Private Sub addWt()
        Dim uName As String = Request.Form("uName")
        Dim shopId As Integer = Request.Form("shopId")
        Dim myWeiTuo As New weituo
        myWeiTuo.addWT(uName, shopId)
        Response.Redirect("../user_center_tools_type.aspx?at=tools")
    End Sub
    Private Sub delWt()
        Dim myShop As New shop, myTu As New weituo
        Dim theTu As New Hashtable
        theTu = myTu.getWt(Request.Params("id"))
        If myShop.hasShop(Session("id"), theTu("shopId")) = False Then Response.Redirect("~/user_center_tools_trusteeship.aspx?at=tools")
        myTu.delWT(Request.Params("id"))
        Response.Redirect("../user_center_tools_type.aspx?at=tools")
    End Sub
    Private Sub weituo_pass()
        Dim myWeiTuo As New weituo
        Dim tpStr As String()
        tpStr = Request.Params("passed").Split(New [Char]() {","c})
        For Each itm As String In tpStr
            myWeiTuo.setWtGoodsPass(itm)
        Next
        Response.Redirect("~/user_center_Goods.aspx?at=goods")
    End Sub
End Class