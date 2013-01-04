Public Class user_center_lianMeng
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub getShopInfo()
        Dim myShop As New shop, strWite As String, myUser As New user
        Dim shop As Hashtable = myShop.getShopInfo(Request.Params("shop"))
        Dim user As Hashtable = myUser.getUserInfo(shop("shop_owner"))
        Dim myMain As New main
        strWite = ""
        strWite = myMain.userCenterSetOutInfo("店铺名称:", shop("shop_name"), strWite)
        strWite = myMain.userCenterSetOutInfo("店铺老板:", user("niceName"), strWite)
        Dim strTp As String = "shop.aspx?id=" & Request.Params("shop")
        strWite = myMain.userCenterSetOutInfo("店铺连接:", "<a href='" & strTp & "' target='_blank'>" & strTp & "</a>", strWite)
        strWite += "<div class='clear'></div>"
        Response.Write(strWite)
    End Sub
    Protected Sub getAddForm()
        Dim inQuery() As dbHelper.inQuery, myShop As New shop
        inQuery = myShop.getShopList(Session("id"))
        Dim strWite As String = "<select name='shop' id='shopList'>"
        For Each inList As dbHelper.inQuery In inQuery
            If inList.value <> Request.Params("shop") Then
                strWite += "<option value='" & inList.value & "'>" & inList.name & "</option>"
            End If
        Next
        strWite += "</select>"
        Dim myMain As New main
        strWite = myMain.userCenterSetOutInfo("选择店铺：", strWite, "")
        strWite += "<input type='hidden' name='shopLm' value='" & Request.Params("shop") & "'>"
        Dim strTp As String = "<input class='subBt' type='submit' name='infoSub' value ='确认添加' />"
        strWite = myMain.userCenterSetOutInfo("", strTp, strWite)
        strWite += "<div class='clear'></div>"
        Response.Write(strWite)
    End Sub
    Protected Sub getLianMengList()
        Dim strWite As String = "", myShop As New shop
        Dim lianMengId As Hashtable()
        If ListBox1.SelectedValue = "" Then
            lianMengId = myShop.lianMengGet(ListBox1.Items(0).Value)
        Else
            lianMengId = myShop.lianMengGet(ListBox1.SelectedValue)
        End If
        If lianMengId(0)("count") = 0 Then
            Response.Write("没有联盟店铺")
            Exit Sub
        End If
        Dim strTp As String = "", shop As New Hashtable
        For Each shopId As Hashtable In lianMengId
            shop = myShop.getShopInfo(shopId("unionId"))
            strWite += "<li>"
            strTp = "<a href='shop.aspx?id=" & shop("id") & "' title='" & shop("shop_name") & "'>"
            If shop("shop_pic") <> "no" Then
                strWite += "<div class='inner'>" & strTp & "<img class='lmImg' src='" & shop("shop_pic") & "' title='" & shop("shop_name") & "' rel='" & shop("shop_name") & "'/></a></div>"
            End If
            strWite += "<div class='inner'>" & strTp & shop("shop_name") & "</a></div>"
            strTp = "server_page/lianMeng.aspx?action=del&shop=" & IIf(ListBox1.SelectedValue = "", ListBox1.Items(0).Value, ListBox1.SelectedValue) & "&unId=" & shopId("unionId")
            strWite += "<div class='fun'><a href='" & strTp & "'>删除</a></div>"
            strWite += "</li>"
        Next
        Response.Write(strWite)
    End Sub
    Protected Sub getShopIcon()
        Dim strWite As String, myShop As New shop, shopId As Integer
        shopId = IIf(ListBox2.SelectedValue = "", ListBox2.Items(0).Value, ListBox2.SelectedValue)
        Dim theShop As New Hashtable
        theShop = myShop.getShopInfo(shopId)
        strWite = IIf(theShop("shop_pic") = "no", "该店铺还没有店标", "<img class='lmImg' src='" & theShop("shop_pic") & "'/>")
        strWite += "<input type='hidden' name='shop' value='" & shopId & "'/>"
        Response.Write(strWite)
    End Sub
End Class