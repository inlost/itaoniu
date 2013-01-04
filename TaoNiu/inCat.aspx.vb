Public Class inCat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("cat") = Nothing Then
            Response.Redirect("default.aspx")
        End If
    End Sub
    Public Sub get_nav()
        Dim strWite As String = ""
        Dim mySearch As New searchHelper
        Try
            Convert.ToInt16(Request.Params("cat"))
            Dim catDeep As Integer = mySearch.catDeep_get(Request.Params("cat"))
            If catDeep = -1 Then
                Response.Redirect("default.aspx")
            End If
            Dim fatherCat As Integer
            If catDeep = 3 Then
                fatherCat = mySearch.catFather_get(Request.Params("cat"))
            Else
                fatherCat = Request.Params("cat")
            End If
            catDeep = IIf(catDeep + 1 > 4, 4, catDeep + 1)
            Dim catNavList() As dbHelper.inQuery, myGoods As New goods
            catNavList = myGoods.cat_get(catDeep, fatherCat)
            For Each catItem As dbHelper.inQuery In catNavList
                strWite += "<li><a href='inCat.aspx?cat=" & catItem.value & "'>" & catItem.name & "</a></li>"
            Next
        Catch ex As Exception
            Response.Redirect("default.aspx")
        End Try
        Response.Write(strWite)
    End Sub
    Public Sub get_father_cat()
        Dim strWite As String = ""
        Dim mySearch As New searchHelper
        Try
            Convert.ToInt16(Request.Params("cat"))
            Dim catDeep As Integer = mySearch.catDeep_get(Request.Params("cat"))
            If catDeep = -1 Then
                Response.Redirect("default.aspx")
            End If
            Dim fatherCat As Integer
            fatherCat = mySearch.catFather_get(Request.Params("cat"))
            fatherCat = mySearch.catFather_get(fatherCat)
            catDeep -= 1
            Dim catNavList() As dbHelper.inQuery, myGoods As New goods
            catNavList = myGoods.cat_get(catDeep, fatherCat)
            For Each catItem As dbHelper.inQuery In catNavList
                strWite += "<li><a href='inCat.aspx?cat=" & catItem.value & "'>" & catItem.name & "</a></li>"
            Next
        Catch ex As Exception
            Response.Redirect("default.aspx")
        End Try
        Response.Write(strWite)
    End Sub

    Public Sub getNowPath()
        Dim strWite As String = ""
        Dim mySearch As New searchHelper
        Dim catNow As Integer = Request.Params("cat")
        Dim catDeep As Integer = mySearch.catDeep_get(catNow)
        Dim iCount As Integer = 1
        While catDeep >= 1
            Dim catInfo As New Hashtable
            catInfo = mySearch.catInfo_get(catNow)
            If iCount <> 1 Then
                strWite = "&gt;<a href='inCat.aspx?cat=" & catInfo("id") & "'>" & catInfo("catName") & "</a>" & strWite
            Else
                strWite = "&gt;<span>" & catInfo("catName") & "</span>" & strWite
            End If
            catDeep = catInfo("catDeep") - 1
            catNow = catInfo("fatherCat")
            iCount += 1
        End While
        strWite = "<a href='default.aspx'>首页</a>" & strWite
        Response.Write(strWite)
    End Sub
    Public Sub getOrderSrc(ByVal orderBy As String)
        Response.Write("?cat=" & Request.Params("cat") & "&orderBy=" & orderBy)
    End Sub
    Public Sub getOrderClass(ByVal orderBy As String)
        If Request.Params("orderBy") = Nothing Then
            If orderBy = "time-new" Then
                Response.Write("class='selected'")
            End If
        Else
            If Request.Params("orderBy") = orderBy Then
                Response.Write("class='selected'")
            End If
        End If
    End Sub
    Public Sub getGoodsList()
        Dim mySearch As New searchHelper, myGoods() As Hashtable
        If Request.Params("page") = Nothing Then
            myGoods = mySearch.goods_get_fromCat(Request.Params("cat"), IIf(Request.Params("orderBy") = Nothing, "time-new", Request.Params("orderBy")))
        Else
            myGoods = mySearch.goods_get_fromCat(Request.Params("cat"), IIf(Request.Params("orderBy") = Nothing, "time-new", Request.Params("orderBy")), Request.Params("page"))
        End If
        Dim strWite As String = "", strTp As String = "", myUser As New user
        Label1.Text = myGoods(0)("totalCount")
        If myGoods(0)("count") = 0 Then Exit Sub
        For Each goodsItem As Hashtable In myGoods
            strWite += "<li>"
            strTp = "shopProduct.aspx?shop=" & goodsItem("shopId") & "&gid=" & goodsItem("id")
            strWite += "<div class='goodsImg'><a href='" & strTp & "'><img src='" & goodsItem("goods_picture") & "' /></a></div>"
            strWite += "<div class='goodsTitle'><h2><a href='" & strTp & "'>" & goodsItem("goods_title") & "</a></h2>"
            Try
                Dim myUserInfo As New Hashtable
                myUserInfo = myUser.getUserInfo(goodsItem("ownerId"))
                strTp = myUserInfo("qq")
                strTp = "<a target='_blank' href='http://wpa.qq.com/msgrd?v=3&uin=" & strTp & "&site=qq&menu=yes'><img border='0' src='http://wpa.qq.com/pa?p=2:" & strTp & ":41' alt='点击这里给我发消息' title='点击这里给我发消息'></a>"
            Catch ex As Exception
                strTp = ""
            End Try
            strWite += strTp & "</div>"
            strWite += "<div class='price'><h3><span>￥</span>" & IIf(goodsItem("goods_discount") > 0, goodsItem("goods_prize") * goodsItem("goods_discount") * 0.01, goodsItem("goods_prize")) & "</h3>"
            strWite += "<p>折扣:<span>" & goodsItem("goods_discount") & "</span></p>" & IIf(goodsItem("goods_discount") > 0, "<h4><strike>" & goodsItem("goods_prize") & "</strike></h4>", "") & "</div>"
            strWite += "<div class='range'>送货范围：<br/>" & goodsItem("goods_range") & "</div>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getSideBar()
        Dim mySearch As New searchHelper, myGoods() As Hashtable
        myGoods = mySearch.getSideBar(Request.Params("cat"))
        Dim strWite As String = "", strTp As String = ""
        If myGoods(0)("count") = 0 Then Exit Sub
        For Each goodsItem As Hashtable In myGoods
            strWite += "<li>"
            strTp = "shopProduct.aspx?shop=" & goodsItem("shopId") & "&gid=" & goodsItem("id")
            strWite += "<a href='" & strTp & "'><img src='" & goodsItem("goods_picture") & "' /></a>"
            strWite += "<h3><span>￥</span>" & IIf(goodsItem("goods_discount") > 0, goodsItem("goods_prize") * goodsItem("goods_discount") * 0.01, goodsItem("goods_prize")) & "</h3>"
            strWite += "<h2><a href='" & strTp & "'>" & goodsItem("goods_title") & "</a></h2>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getPageLink()
        Dim strWite As String = "", perPage As Integer = 15
        Dim strTp As String = IIf(Request.Params("orderBy") <> Nothing, "?cat=" & Request.Params("cat") & "&orderBy=" & Request.Params("orderBy"), "?cat=" & Request.Params("cat"))
        Dim pageNow = IIf(Request.Params("page") <> Nothing, Request.Params("page"), 1)
        If Request.Params("page") <> Nothing And Request.Params("page") <> 1 Then
            strWite += "<li><a href='" & strTp & "&page=" & pageNow - 1 & "'>上一页</li>"
        End If
        Dim iPagesTp As Double = Label1.Text / perPage
        Dim iPages As String = ""
        If (iPagesTp.ToString.IndexOf(".") > 0) Then
            iPages = Left(iPagesTp, iPagesTp.ToString.IndexOf("."))
        Else
            iPages = iPagesTp
        End If
        iPages = IIf(Label1.Text Mod perPage <> 0, iPages + 1, iPages)
        If iPages < 2 Then Exit Sub
        For i As Integer = 1 To iPages
            If i = pageNow Then
                strWite += "<li><a class='pageNow' href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i > pageNow - 3 And i < pageNow + 5 Then
                strWite += "<li><a href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i = 1 Or i = iPages Then
                strWite += "<li><a href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i = pageNow - 3 Or i = pageNow + 5 Then
                strWite += "<li>…</li>"
            Else
                strWite = strWite
            End If
        Next
        If pageNow <> iPages Then
            strWite += "<li><a href='" & strTp & "&page=" & pageNow + 1 & "'>下一页</li>"
        End If
        Response.Write(strWite)
    End Sub
End Class