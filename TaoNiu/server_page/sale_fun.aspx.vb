Public Class sale_fun
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Request.Params("action")
            Case "jf"
                If setJf() Then
                    Response.Redirect("../user_center_sale_jf.aspx?at=sale&msg=ok")
                Else
                    Response.Redirect("../user_center_sale_jf.aspx?at=sale&msg=error")
                End If
            Case "zq"
                If setZq() Then
                    Response.Redirect("../user_center_sale_zq.aspx?at=sale&msg=ok")
                Else
                    Response.Redirect("../user_center_sale_zq.aspx?at=sale&msg=error")
                End If
            Case "zqDel"
                If zqDel() Then
                    Response.Redirect("../user_center_sale_zq.aspx?at=sale&msg=ok")
                Else
                    Response.Redirect("../user_center_sale_zq.aspx?at=sale&msg=error")
                End If
            Case "pf"
                If pfAdd() Then
                    Response.Redirect("../user_center_sale_pf.aspx?at=sale&msg=ok")
                Else
                    Response.Redirect("../user_center_sale_pf.aspx?at=sale&msg=error")
                End If
            Case "pfDel"
                If pfDel() Then
                    Response.Redirect("../user_center_sale_pf.aspx?at=sale&msg=ok")
                Else
                    Response.Redirect("../user_center_sale_pf.aspx?at=sale&msg=error")
                End If
            Case "tg"
                Dim rst As Integer = tgAdd()
                If rst = 1 Then
                    Response.Redirect("../user_center_sale_tg.aspx?at=sale&msg=ok")
                ElseIf rst = 0 Then
                    Response.Redirect("../user_center_sale_tg.aspx?at=sale&msg=error")
                ElseIf rst = -1 Then
                    Response.Redirect("../user_center_money.aspx?at=money&msg=need&price=" & Request.Form("price") * Request.Form("nb") * 0.04)
                End If
            Case "goodsLmAdd"
                goodsLmAdd()
            Case "goodsLmDel"
                goodsLmDel()
            Case "tuanOrderAdd"
                Call tuanOrderAdd()
            Case Else
                Response.Write("Good Luck~")
        End Select
    End Sub
    Protected Function setJf() As Boolean
        Dim myHelper As New dbHelper, myShop As New shop
        Try
            Dim uid As Integer = Session("id"), money As Integer = Request.Form("money")
            Dim sId As Integer = Request.Form("sid")
            If myShop.hasShop(uid, sId) = False Then Return False
            Dim strSql As String = "select id from sale_code where type=1 and sid=" & sId
            Dim myDs As New DataSet
            myDs = myHelper.getQuerySql(strSql, "ct")
            If myDs.Tables("ct").Rows.Count <> 1 Then
                Dim myJf As New Hashtable
                myJf("@sid") = sId
                myJf("@jf") = money
                Return myHelper.insertQuery("sale_code_add", myHelper.hashtableToInquery(myJf))
            Else
                strSql = "update sale_code set jf=" & money & " where  type=1 and sid=" & sId
                Return myHelper.querySql(strSql)
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function setZq() As Boolean
        Try
            Dim myShop As New shop
            If myShop.hasShop(Session("id"), Request.Form("sid")) = False Then Return False
            Dim myHelper As New dbHelper
            Dim sid As Integer = Request.Form("sid")
            Dim gid As Integer = Request.Form("gid")
            Dim timeLong As Integer = Request.Form("timeLong")
            Dim xf As Integer = Request.Form("xf")
            Dim je As Integer = Request.Form("je")
            Dim zq As String = xf & "=" & je
            Dim myZq As New Hashtable
            myZq("@sid") = sid
            myZq("@gid") = gid
            myZq("@timeLength") = timeLong
            myZq("@zq") = zq
            myHelper.noneQuery("sale_zq_add", myHelper.hashtableToInquery(myZq))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function zqDel() As Boolean
        Try
            Dim myShop As New shop, myHelper As New dbHelper
            If myShop.hasShop(Session("id"), Request.Params("sid")) = False Then Return False
            Dim strSql As String = "delete from sale_code where id=" & Request.Params("zqId")
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function pfAdd() As Boolean
        Try
            Dim myGood As New goods, theGoods As New Hashtable, myShop As New shop
            Dim gid As Integer = Request.Form("gid")
            Dim sid As Integer = Request.Form("sid")
            Dim number As Integer = Request.Form("nb")
            Dim price As Double = Request.Form("price")
            If gid = -1 Then Return False
            theGoods = myGood.goods_get(gid)
            If theGoods("shopId") <> sid Then Return False
            If myShop.hasShop(Session("id"), sid) = False Then Return False
            If theGoods("goods_prize") < price Then Return False
            Dim myHelper As New dbHelper
            Dim thePf As New Hashtable
            thePf("@sid") = sid
            thePf("@gid") = gid
            thePf("@nb") = number
            thePf("@pf") = price
            myHelper.noneQuery("sale_pf_add", myHelper.hashtableToInquery(thePf))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function pfDel() As Boolean
        Try
            Dim myShop As New shop, myHelper As New dbHelper
            If myShop.hasShop(Session("id"), Request.Params("sid")) = False Then Return False
            Dim strSql As String = "delete from sale_code where id=" & Request.Params("pfId")
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function tgAdd() As Integer
        '返回值 -1 账户余额不足交付手续费 1成功 2失败
        Try
            Dim myHelper As New dbHelper, myGoods As New goods, myShop As New shop
            Dim sid As Integer = Request.Form("sid")
            Dim gid As Integer = Request.Form("gid")
            Dim number As Integer = Request.Form("nb")
            Dim price As Double = Request.Form("price")
            Dim theGoods As New Hashtable
            theGoods = myGoods.goods_get(gid)
            If theGoods("shopId") <> sid Then Return False
            If myShop.hasShop(Session("id"), sid) = False Then Return False
            If theGoods("goods_prize") < price Then Return False
            Dim myMoney As New tnPay.money
            If myMoney.getMoney(Session("id"))("money") < price * number * 0.04 Then
                Return -1
            End If
            Dim myTg As New Hashtable
            myTg("@sid") = sid
            myTg("@gid") = gid
            myTg("@nb") = number
            myTg("@pf") = price
            myHelper.noneQuery("sale_tg_add", myHelper.hashtableToInquery(myTg))
            Return 1
        Catch ex As Exception
            Return 2
        End Try
    End Function
    Public Sub tuanOrderAdd()
        Try
            Dim sId As Integer = Request.Form("sid"), gid As Integer = Request.Form("gid")
            Dim tId As Integer = Request.Form("tid")
            If Session("onTheWay") <> "loginSuccess" Then
                Response.Redirect("../login.aspx?redirectURL=tuan.aspx?id=" & tId)
            End If
            Dim mySale As New saleFunction, myGoods As New goods, myShop As New shop, myUser As New user, myPay As New tnPay.orde
            Dim theTuan As New Hashtable, theMoney As New Hashtable, theUser As New Hashtable, theShop As New Hashtable, theGood As New Hashtable
            theShop = myShop.getShopInfo(sId)
            theUser = myUser.getUserInfo(Session("id"))
            theTuan = mySale.getTg(tId)
            Dim strMsg As String = "确认一下收货信息信息是否正确" & vbCrLf & "收货人姓名：" & theUser("real_name") & vbCrLf
            strMsg += "收货人联系电话：" & theUser("mobile") & vbCrLf
            strMsg += "收货人地址：" & theUser("address") & vbCrLf
            strMsg += "点【是】将转至订单支付页面，点【否】将转至用户中心个人信息修改页面。" & vbCrLf
            strMsg += vbCrLf & "！tip:团购订单情选择即时到账交易，否则可能造成付款无法收到。"
            Dim msgRst As Integer = MsgBox(strMsg, vbYesNo, "确认收货信息")
            If msgRst = vbYes Then
                Dim payorder As New tnPay.orde
                Dim orderpay As New Hashtable
                Dim theOrder As New Hashtable
                Dim myOrder As New orders
                theOrder("@ownerId") = theShop("shop_owner")
                theOrder("@shop") = theShop("id")
                theOrder("@good") = gid
                theOrder("@customer") = Session("id")
                theOrder("@message") = "桃牛团购订单"
                theOrder("@number") = 1
                theOrder("@rec_name") = theUser("real_name")
                theOrder("@rec_phone") = theUser("mobile")
                theOrder("@rec_address") = theUser("address")
                Dim rt As Integer = myOrder.add(theOrder)
                myOrder.orderStatusChange(rt, 0, 1)
                mySale.changeOrderToTuan(rt, tId)
                Dim thePayOrder As New Hashtable
                theGood = myGoods.goods_get(gid)
                thePayOrder("id") = rt
                thePayOrder("title") = "[团购]" & theGood("goods_title")
                thePayOrder("body") = "[团购]" & theGood("goods_title")
                thePayOrder("price") = theTuan("pf")
                thePayOrder("number") = 1
                thePayOrder("name") = theOrder("@rec_name")
                thePayOrder("address") = theOrder("@rec_address")
                thePayOrder("zip") = 545006
                thePayOrder("phone") = theOrder("@rec_phone")

                Dim strwite As String = payorder.add(thePayOrder)
                strwite = strwite.Replace("'", """")
                Dim struri = "../paysystem/payto.aspx?fm=" & strwite
                Response.Redirect(struri)
            Else
                Response.Redirect("../user_center.aspx?action=modify")
            End If
        Catch ex As Exception
            Response.Redirect("../user_center.aspx")
        End Try
    End Sub
    Public Sub goodsLmAdd()
        '联盟商品发布
        Dim mySale As New saleFunction, theSale As New Hashtable
        theSale("@gid") = Request.Form("gid")
        theSale("@shopId") = Request.Form("sid")
        theSale("@discuount") = Request.Form("discount_1") * 10 + Request.Form("discount_2")
        theSale("@needCat") = goodsLmAdd_cat_get()
        mySale.lianMengGoodsAdd(theSale)
        Response.Redirect("../user_center_lm.aspx?at=tools")
    End Sub
    Private Function goodsLmAdd_cat_get() As String
        Dim strCat As String = ""
        If Request.Form("cat1") = Nothing Then
            Return ""
        Else
            strCat = Request.Form("cat1")
        End If
        If Request.Form("cat2") = Nothing Then
            Return strCat
        Else
            strCat += "-" & Request.Form("cat2")
        End If
        If Request.Form("cat3") = Nothing Then
            Return strCat
        Else
            strCat += "-" & Request.Form("cat3")
        End If
        If Request.Form("cat4") = Nothing Then
            Return strCat
        Else
            strCat += "-" & Request.Form("cat4")
        End If
        Return strCat
    End Function
    Public Sub goodsLmDel()
        Dim myShop As New shop, mySale As New saleFunction
        If myShop.hasShop(Session("id"), Request.Params("sid")) Then
            mySale.lianMengGoodsDel(Request.Params("id"))
        End If
        Response.Redirect("../user_center_lm_list.aspx?at=tools")
    End Sub
End Class