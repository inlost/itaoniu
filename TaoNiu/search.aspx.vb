Public Class search
    Inherits System.Web.UI.Page
    Protected goodsList() As Hashtable
    Protected goodsCatList As Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("q") = Nothing Or Request.Params("q") = "输入您想寻找的宝贝" Then
            Response.Redirect("default.aspx")
        End If
    End Sub
    Public Sub get_nav()

    End Sub
    Public Sub getNowPath()

    End Sub
    Public Sub getGoodsList()
        Dim theSearch As New Hashtable

        theSearch("@strSearch") = Request.Params("q")

        If Request.Params("p") = "" Or Request.Params("p") = Nothing Or Request.Params("p") = "地点" Then
            theSearch("@goods_range") = ""
        Else
            theSearch("@goods_range") = Request.Params("p")
        End If

        If Request.Params("online") = Nothing Then
            theSearch("@isOnLine") = ""
        Else
            theSearch("@isOnLine") = "1"
        End If

        If Request.Params("ce") = "" Or Request.Params("ce") = Nothing Then
            theSearch("@shop_certification") = ""
        Else
            theSearch("@shop_certification") = ""
            Dim tpStr As String()
            tpStr = Request.Params("ce").Split(New [Char]() {","c})
            Array.Sort(tpStr)
            For i = 0 To tpStr.Length - 1
                theSearch("@shop_certification") += IIf(Len(theSearch("@shop_certification")) = 0, theSearch("@shop_certification"), "-" & theSearch("@shop_certification"))
            Next
        End If

        If Request.Params("goods_pay_type") = "" Or Request.Params("goods_pay_type") = Nothing Then
            theSearch("@payType") = ""
        Else
            theSearch("@payType") = Request.Params("goods_pay_type")
        End If

        If Request.Params("orderByWay") = "" Or Request.Params("orderByWay") = Nothing Then
            theSearch("@orderByIs") = "id"
            theSearch("@orderDesc") = "desc"
        Else
            theSearch("@orderByIs") = Request.Params("orderBy")
            theSearch("@orderDesc") = Request.Params("orderByWay")
        End If

        theSearch("@page") = getNowPage()
        theSearch("@prePage") = 15

        Dim mySearch As New searchHelper
        goodsList = mySearch.goods_get_fromSearch(theSearch)


        If goodsList(0)("count") = 0 Then Exit Sub
        Dim strWite As String = "", strTp As String = "", myUser As New user
        Dim display As String = IIf(Request.Params("display") = Nothing, " class='list-mode' ", " class='" & Request.Params("display") & "' ")
        For Each goodsItem As Hashtable In goodsList
            strWite += "<li " & display & ">"
            strTp = "shopProduct.aspx?shop=" & goodsItem("shopId") & "&gid=" & goodsItem("id")
            strWite += "<div class='goodsImg'><a href='" & strTp & "'><img src='" & goodsItem("goods_picture") & "' /></a></div>"
            strWite += "<div class='goodsTitle'><h2><a href='" & strTp & "'>" & goodsItem("goods_title") & "</a></h2>"
            Try
                Dim myUserInfo As New Hashtable
                myUserInfo = myUser.getUserInfo(goodsItem("ownerId"))
                strTp = myUserInfo("qq")
                strTp = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTp & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTp & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
                Dim myTu As New trusteeships, tuQQ As String = myTu.getOnTimeQQ(goodsItem("shopId"))
                If tuQQ <> "" Then
                    strTp += "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & tuQQ & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & tuQQ & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
                End If
            Catch ex As Exception
                strTp = ""
            End Try
            strWite += strTp & "</div>"
            strWite += "<div class='price'><h3><span>￥</span>" & IIf(goodsItem("goods_discount") > 0, goodsItem("goods_prize") * goodsItem("goods_discount") * 0.01, goodsItem("goods_prize")) & "</h3>"
            strWite += "<p>折扣:<span>" & goodsItem("goods_discount") & "</span></p>" & IIf(goodsItem("goods_discount") > 0, "<h4><strike>" & goodsItem("goods_prize") & "</strike></h4>", "") & "</div>"
            strWite += "<div class='range'>送货范围：<br/>" & goodsItem("goods_range").ToString.Replace("|", "<br/>").Replace(":", " 时间（小时）") & "送货费用（元）:" & goodsItem("send_price") & "</div>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getPageLink()
        Dim strWite As String = "", perPage As Integer = 15
        Dim myPageLink As New pageLink
        strWite = myPageLink.getPageLink(goodsList(0)("count"), perPage, getNowPage, pageUrl)
        Response.Write(strWite)
    End Sub
    Public Sub getSideBar()

    End Sub
    Private Function getNowPage() As Integer
        If Request.Params("page") = Nothing Then
            Return 1
        Else
            Return Request.Params("page")
        End If
    End Function
    Private Function pageUrl() As String
        Dim strWite As String = "search.aspx?s=b"
        If Request.Params("p") <> Nothing Then strWite += "&p=" & Request.Params("p")
        If Request.Params("q") <> Nothing Then strWite += "&q=" & Request.Params("q")
        If Request.Params("ce") <> Nothing Then strWite += "&ce=" & Request.Params("ce")
        If Request.Params("goods_pay_type") <> Nothing Then strWite += "&goods_pay_type=" & Request.Params("goods_pay_type")
        If Request.Params("online") <> Nothing Then strWite += "&online=" & Request.Params("online")
        Return strWite
    End Function
    Public Sub getUrl()
        Dim strWite As String = "search.aspx?s=b"
        If Request.Params("p") <> Nothing Then strWite += "&p=" & Request.Params("p")
        If Request.Params("q") <> Nothing Then strWite += "&q=" & Request.Params("q")
        If Request.Params("ce") <> Nothing Then strWite += "&ce=" & Request.Params("ce")
        If Request.Params("display") <> Nothing Then strWite += "&display=" & Request.Params("display")
        If Request.Params("goods_pay_type") <> Nothing Then strWite += "&goods_pay_type=" & Request.Params("goods_pay_type")
        If Request.Params("online") <> Nothing Then strWite += "&online=" & Request.Params("online")
        Response.Write(strWite)
    End Sub
    Public Sub getParams(ByVal w As String)
        Response.Write(Request.Params(w))
    End Sub
    Public Sub getBaoZhang()
        Dim myShop As New shop, ceList As Hashtable(), strWite As String = ""
        ceList = myShop.getCeType
        Dim strTp As String = ""
        For Each item As Hashtable In ceList
            If Request.Params("ce") <> Nothing Then
                strTp = IIf(Request.Params("ce").IndexOf(item("id")) > -1, "checked", "")
            End If
            strWite += "<td>"
            strWite += "<input type='checkbox' " & strTp & " value='" & item("id") & "' name='ce'><label for='filterServiceOversea'>" & item("name") & "</label>"
            strWite += "</td>"
        Next
        If Request.Params("online") = 1 Then
            strTp = "checked"
        Else
            strTp =""
        End If
        strWite += "<td><input type='checkbox' " & strTp & " value='1' name='online'><label for='filterServiceOversea'>在线卖家</label></td>"
        Response.Write(strWite)
    End Sub
    Public Sub setPayCheck(ByVal w As String)
        Select Case Request.Params("goods_pay_type")
            Case Nothing
                If w = "pay_type_sy" Then Response.Write("checked='checked'")
            Case "0"
                If w = "pay_type_sy" Then Response.Write("checked='checked'")
            Case "1"
                If w = "pay_type_xs" Then Response.Write("checked='checked'")
            Case "2"
                If w = "pay_type_hdfk" Then Response.Write("checked='checked'")
        End Select
    End Sub
    Public Sub getDisplay()
        If Request.Params("display") = Nothing Or Request.Params("display") = "list-mode" Then
            Response.Write("<a title='大图展示' href='" & pageUrl() & "&display=thumb-mode'><span>切换到大图</span></a>")
        Else
            Response.Write("<a title='列表展示' href='" & pageUrl() & "&display=list-mode'><span>切换到列表</span></a>")
        End If
    End Sub
End Class