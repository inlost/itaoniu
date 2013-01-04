Public Class tuan
    Inherits System.Web.UI.Page
    Private theGoods As New Hashtable, theTuan As New Hashtable, myGoods As New goods, mySale As New saleFunction
    Private theUser As New Hashtable, myUser As New user, theShop As New Hashtable, myShop As New shop, tid As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Len(Trim(Request.Params("Id"))) = 0 Then
            Response.Redirect("default.aspx")
        Else
            tid = Request.Params("id")
            theTuan = mySale.getTg(tid)
            theGoods = myGoods.goods_get(theTuan("gid"))
            theShop = myShop.getShopInfo(theTuan("sid"))
            theUser = myUser.getUserInfo(theShop("shop_owner"))
        End If
    End Sub
    Public Sub getTuan(ByVal what As String)
        Select Case what
            Case "title"
                Response.Write(theGoods("goods_title"))
            Case "price"
                Response.Write(theTuan("pf"))
            Case "save"
                Response.Write(theGoods("goods_prize") - theTuan("pf"))
            Case "oPrice"
                Response.Write(theGoods("goods_prize"))
            Case "discount"
                Response.Write(100 - Convert.ToInt32((theGoods("goods_prize") - theTuan("pf")) / theGoods("goods_prize") * 100))
            Case "pic"
                Response.Write(theGoods("goods_picture"))
            Case "introduce"
                Response.Write(theGoods("goods_introduce"))
            Case "nb"
                Response.Write(theTuan("nb"))
            Case "nbNow"
                Dim mySale As New saleFunction
                Response.Write(mySale.getTgOrderCount(theTuan("id")))
        End Select
    End Sub
    Public Sub getSubForm()
        Dim endTime As Date = theTuan("addTime"), day As Integer
        endTime = endTime.AddDays(5)
        If Date.Now.Hour - endTime.Hour < 0 Then
            endTime = endTime.AddDays(-1)
            Day = Date.Now.DayOfYear - endTime.DayOfYear
        Else
            Day = Date.Now.DayOfYear - endTime.DayOfYear
        End If
        Dim strWite As String = ""
        If day >= 0 Then
            strWite = "<form method='post' action='server_page/sale_fun.aspx?action=tuanOrderAdd'>"
            strWite += "<input type='hidden' name='gid' value='" & theGoods("id") & "'/>"
            strWite += "<input type='hidden' name='sid' value='" & theShop("id") & "'/>"
            strWite += "<input type='hidden' name='tid' value='" & Request.Params("id") & "'/>"
            strWite += "<input type='submit' value='抢购'/>"
            strWite += "</form>"
        Else
            strWite = "<form method='post' action='#'>"
            strWite += "<input type='submit' value='已结束'/>"
            strWite += "</form>"
        End If
        Response.Write(strWite)
    End Sub
    Public Sub getTime(ByVal what As String)
        Dim endTime As Date = theTuan("addTime"), day As Integer, hour As Integer, minute As Integer
        endTime = endTime.AddDays(5)
        If Date.Now.Hour - endTime.Hour < 0 Then
            endTime = endTime.AddDays(-1)
            day = Date.Now.DayOfYear - endTime.DayOfYear
            hour = 24 - endTime.Hour
        Else
            day = Date.Now.DayOfYear - endTime.DayOfYear
            hour = Date.Now.Hour - endTime.Hour
        End If
        If day < 0 Then
            day = 0
            hour = 0
            minute = 0
        End If
        minute = IIf(Date.Now.Minute - endTime.Minute >= 0, Date.Now.Minute - endTime.Minute, 60 - endTime.Second)
        Select Case what
            Case "day"
                Response.Write(day)
            Case "hour"
                Response.Write(hour)
            Case "minute"
                Response.Write(minute)
        End Select
    End Sub
    Public Sub getShopInfo()
        Dim strWite As String = "", shop As New Hashtable, myShop As New shop, user As New Hashtable, myUser As New user, strTp As String
        shop = theShop
        user = theUser
        strTp = user("qq")
        strTp = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTp & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTp & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
        strWite += "<ul id='ownerInfo'><li><span class='title'>商家名称：</span><span class='info'>" & shop("shop_name") & "</span></li>"
        strWite += "<li><span class='title'>地址：</span><span class='info'>" & shop("shop_address") & "</span><div class='clear'></div></li>"
        strWite += "<li><span class='title'>信用：</span><span class='info'>" & user("sale_pj_good") & "</span><div class='clear'></div></li>"
        strWite += "<li><span class='title'>客服：</span><span class='info'>" & strTp & "</span><div class='clear'></div></li></ul>"
        strWite += "</ul>"
        Response.Write(strWite)
    End Sub
End Class