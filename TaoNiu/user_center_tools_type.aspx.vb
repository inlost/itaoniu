Public Class user_center_tools_type
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getShopList()
        Dim inQuery() As dbHelper.inQuery, myShop As New shop
        inQuery = myShop.getShopList(Session("id"))
        For Each inList As dbHelper.inQuery In inQuery
            Response.Write("<option value='" & inList.value & "'>" & inList.name & "</option>")
        Next
    End Sub
    Public Sub getWeiTuoList()
        Dim strWite As String = "", myTu As New weituo, theTuList As Hashtable(), myMain As New main
        Dim strTp As String = "", myShop As New shop, theShop As New Hashtable, myUser As New user
        theTuList = myTu.getWTlist(Session("id"))
        If theTuList(0)("count") = 0 Then Exit Sub
        For Each tu As Hashtable In theTuList
            theShop = myShop.getShopInfo(tu("shopId"))
            strTp = "<span class='tuspan'>将店铺：【" & theShop("shop_name") & "】的录入权限委托给【" & myUser.getUserInfo(tu("uid"))("niceName") & "】</span>"
            strTp += "<a class='cancel' href='server_page/shop_goods.aspx?action=delWt&id=" & tu("id") & "'>取消</a>"
            strWite = myMain.userCenterSetOutInfo("委托：", strTp, strWite, False)
        Next
        Response.Write(strWite)
    End Sub
End Class