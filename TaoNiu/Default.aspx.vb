Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getCatList()
        Dim myGoods As New goods, inQuery() As dbHelper.inQuery, inQueryDeep2() As dbHelper.inQuery, inQueryDeep3() As dbHelper.inQuery
        inQuery = myGoods.cat_get(1)
        Dim strTp As String = ""
        For Each inQueryItem As dbHelper.inQuery In inQuery
            strTp += "<li class='catItems'><span>》</span>"
            strTp += "<a class='firstCat' href='inCat.aspx?cat=" & inQueryItem.value & "'>" & inQueryItem.name & "</a>"
            strTp += "<div class='catNavShow'><div class='catNavMainLeft'><span>》</span><a class='firstCat' href='inCat.aspx?cat=" & inQueryItem.value & "'>" & inQueryItem.name & "</a></div><div class='catNavMainRight'><div class='catNavShowLeft'>"
            inQueryDeep2 = myGoods.cat_get(2, inQueryItem.value)
            For Each inQueryItem2 As dbHelper.inQuery In inQueryDeep2
                strTp += "<dl class='catItemDeep2'>"
                strTp += "<dt><a href='inCat.aspx?cat=" & inQueryItem2.value & "'>" & inQueryItem2.name & "</a></dt><dd><ul>"
                inQueryDeep3 = myGoods.cat_get(3, inQueryItem2.value)
                Dim iCount As Integer = 1
                For Each inQueryItem3 As dbHelper.inQuery In inQueryDeep3
                    strTp += "<li><a href='inCat.aspx?cat=" & inQueryItem3.value & "'>" & inQueryItem3.name & "</a></li>"
                    If iCount > 4 Then
                        strTp += "<li><a href='inCat.aspx?cat=" & inQueryItem2.value & "' style='color:red;'>更多…</a></li>"
                        Exit For
                    End If
                    iCount += 1
                Next
                strTp += "</ul></dd><div class='clear'></div></dl>"
            Next
            strTp += "</div><div class='catNavShowRight'></div><div class='clear'></div></div></div>"
            strTp += "</li>"
        Next
        Response.Write(strTp)
    End Sub
    Public Sub getHdImgList()
        Dim rt() As dbHelper.inQuery, myAdm As New adm
        rt = myAdm.hd_get(False)
        Dim strWite As String = ""
        For Each rtItem As dbHelper.inQuery In rt
            strWite += "<li><a href='" & rtItem.value & "'><img src='" & rtItem.name & "'/></a></li>"
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getHdTitleList()
        Dim rt() As dbHelper.inQuery, myAdm As New adm
        rt = myAdm.hd_get(True)
        Dim strWite As String = "", iCount As Integer = 1
        For Each rtItem As dbHelper.inQuery In rt
            strWite += "<li><a tohere='" & iCount & "' href='" & rtItem.value & "'>" & rtItem.name & "</a></li>"
            iCount += 1
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getSallMshaBd()
        Dim mySearch As New searchHelper, myHash() As Hashtable
        Dim strWite As String = "<ul>"
        myHash = mySearch.goods_get_mSha(1, 12)
        For Each ht As Hashtable In myHash
            strWite += "<li><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'><img alt=" & ht("goods_title") & " src='" & ht("goods_picture") & "' /></a>"
            strWite += "<h3><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'>" & ht("goods_title") & "</a></h3>"
            strWite += "<span>￥" & ht("goods_prize") & "</span></li>"
        Next
        strWite += "<p class='clear'></p></ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getSallTejia()
        Dim mySearch As New searchHelper, myHash() As Hashtable
        myHash = mySearch.goods_get_TeJia(1, 12)
        Dim strWite As String = "<ul>"
        For Each ht As Hashtable In myHash
            strWite += "<li><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'><img alt=" & ht("goods_title") & " src='" & ht("goods_picture") & "' /></a>"
            strWite += "<h3><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'>" & ht("goods_title") & "</a></h3>"
            strWite += "<span>" & ht("goods_discount") & "折</span></li>"
        Next
        strWite += "<p class='clear'></p></ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getNewShops()
        Dim myShop As New shop, myHash() As Hashtable, mySearchHelper As New searchHelper
        myHash = myShop.getShopList_new(9)
        Dim strWite As String = "<ul>"
        Dim shopPic As String, shopGood() As Hashtable
        For Each ht As Hashtable In myHash
            shopGood = mySearchHelper.goods_get_fromShop(ht("id"), 1, 1)
            If shopGood.Length > 0 Then
                shopPic = shopGood(0)("goods_picture")
            Else
                shopPic = "images/shop.jpg"
            End If
            strWite += "<li><a href='shop.aspx?id=" & ht("id") & "'><img alt=" & ht("shop_name") & " src='" & shopPic & "' /></a><h3><a href='shop.aspx?id=" & ht("id") & "'>" & ht("shop_name") & "</a></h3></li>"
        Next
        strWite += "<p class='clear'></p></ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getGg()
        Dim strWite As String = "", myHash() As Hashtable, myMain As New main
        myHash = myMain.announcement_get
        If myHash(0)("count") = 0 Then Exit Sub
        For Each haItem As Hashtable In myHash
            strWite += "<li><a target='_blank' href='server_page/functions.aspx?action=announcements&id=" & haItem("id") & "' class='announcements'>" & haItem("title") & "</a></li>"
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getFree()
        Dim strWite As String = "<ul>", myGoods() As Hashtable
        Dim mySearch As New searchHelper
        myGoods = mySearch.goods_get_fromFree
        If myGoods(0)("count") = 0 Then Exit Sub
        For Each ht As Hashtable In myGoods
            strWite += "<li><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'><img alt=" & ht("goods_title") & " src='" & ht("goods_picture") & "' /></a>"
            strWite += "<h3><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'>" & ht("goods_title") & "</a></h3>"
            strWite += "<span>$0.00</span></li>"
        Next
        strWite += "<p class='clear'></p></ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getZq()
        Dim strWite As String = "<ul>", myGoods() As Hashtable
        Dim mySearch As New searchHelper
        myGoods = mySearch.goods_get_fromZq
        If myGoods(0)("count") = 0 Then Exit Sub
        Dim mySale As New saleFunction, zq As dbHelper.inQuery
        For Each ht As Hashtable In myGoods
            zq = mySale.zqSpit(ht("zq"))
            strWite += "<li><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'><img alt=" & ht("goods_title") & " src='" & ht("goods_picture") & "' /></a>"
            strWite += "<h3><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'>" & ht("goods_title") & "</a></h3>"
            strWite += "<span>满" & zq.name & "赠" & zq.value & "</span></li>"
        Next
        strWite += "<p class='clear'></p></ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getShuai()
        Dim strWite As String = "<ul>", myGoods() As Hashtable
        Dim mySearch As New searchHelper
        myGoods = mySearch.goods_get_fromShuai
        If myGoods(0)("count") = 0 Then Exit Sub
        For Each ht As Hashtable In myGoods
            strWite += "<li><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'><img alt=" & ht("goods_title") & " src='" & ht("goods_picture") & "' /></a>"
            strWite += "<h3><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'>" & ht("goods_title") & "</a></h3>"
            strWite += "<span>" & ht("goods_discount") & "折</span></li>"
        Next
        strWite += "<p class='clear'></p></ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getTuan()
        Dim strWite As String = "<ul>", myGoods() As Hashtable
        Dim mySearch As New searchHelper
        myGoods = mySearch.goods_get_fromTuan
        If myGoods(0)("count") = 0 Then Exit Sub
        For Each ht As Hashtable In myGoods
            strWite += "<li><a href='tuan.aspx?id=" & ht("tid") & "'><img alt=" & ht("goods_title") & " src='" & ht("goods_picture") & "' /></a>"
            strWite += "<h3><a href='tuan.aspx?id=" & ht("tid") & "'>" & ht("goods_title") & "</a></h3>"
            strWite += "<span>￥" & ht("goods_prize") & "</span></li>"
        Next
        strWite += "<p class='clear'></p></ul>"
        Response.Write(strWite)
    End Sub
    Public Sub getPi()
        Dim strWite As String = "<ul>", myGoods() As Hashtable
        Dim mySearch As New searchHelper
        myGoods = mySearch.goods_get_fromPi
        If myGoods(0)("count") = 0 Then Exit Sub
        For Each ht As Hashtable In myGoods
            strWite += "<li><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'><img alt=" & ht("goods_title") & " src='" & ht("goods_picture") & "' /></a>"
            strWite += "<h3><a href='shopProduct.aspx?shop=" & ht("shopId") & "&gid=" & ht("id") & "'>" & ht("goods_title") & "</a></h3>"
            strWite += "<span>￥" & ht("goods_prize") & "</span></li>"
        Next
        strWite += "<p class='clear'></p></ul>"
    End Sub
    Public Sub getSnsPosts()
        Dim myPcenter As New pCenter, strWite As String = ""
        Dim posts As Hashtable() = myPcenter.timeLine_default("10")
        If posts(0)("count") = 0 Then Exit Sub
        Dim myUser As New user
        For Each post As Hashtable In posts
            strWite += "<li>"
            strWite += "<div class='avatar'><img src='"
            strWite += myUser.getBbsIcon(myUser.getUserInfo(post("uid"))("niceName"))
            strWite += "'/></div>"
            strWite += "<div class='posts'>" & post("short") & "</div>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getUserList()
        Dim myUser As New user, users As Hashtable(), strWite As String = ""
        users = myUser.getUserList("10")
        If users(0)("count") = 0 Then Exit Sub
        For Each u As Hashtable In users
            strWite += "<div class='userList'>"
            strWite += "<div class='userList-user'>"
            strWite += "<img src='" & myUser.getBbsIcon(u("niceName")) & "'/>"
            strWite += "<h3>" & u("niceName") & "</h3>"
            strWite += "</div>"
            strWite += "</div>"
        Next
        Response.Write(strWite)
    End Sub
    Private Sub userLogin(ByVal uid As Integer)
        Dim myUser As New user, theUser As New Hashtable, strRt As String = ""
        strRt = myUser.user_login(uid, "255.255.255.255")
        If strRt <> "error" Then
            Dim myMain As New main, inQuery() As dbHelper.inQuery
            inQuery = myMain.stringToInQuery(strRt, "|")
            For Each qItem As dbHelper.inQuery In inQuery
                If Len(qItem.name) <> 0 And qItem.name <> "pass" Then
                    Session(qItem.name) = qItem.value
                End If
            Next
            Session("uid") = Session("email")
            Session("onTheWay") = "loginSuccess"
        Else
            Exit Sub
        End If
    End Sub
End Class