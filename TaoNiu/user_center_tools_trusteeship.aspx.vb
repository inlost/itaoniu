Public Class user_center_tools_trusteeship
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("msg") <> Nothing Then
            If Request.Params("msg") = "had" Then
                Response.Write("<script>alert('不能重复申请托管')</script>")
            End If
        End If
    End Sub
    Public Sub getShopList()
        Dim inQuery() As dbHelper.inQuery, myShop As New shop
        inQuery = myShop.getShopList(Session("id"))
        For Each inList As dbHelper.inQuery In inQuery
            Response.Write("<option value='" & inList.value & "'>" & inList.name & "</option>")
        Next
    End Sub
    Public Sub getTuType()
        Dim myTu As New trusteeships
        Response.Write(myTu.getTypeListAsSting)
    End Sub
    Public Sub getMember()
        Dim myTu As New trusteeships, theMembers As Hashtable(), myUser As New user
        theMembers = myTu.getMember
        If theMembers(0)("count") = 0 Then Exit Sub
        For Each item As Hashtable In theMembers
            Response.Write("<option value='" & item("id") & "'>" & item("taoniuId") & "</option>")
        Next
    End Sub
    Public Sub getTuList()
        Dim strWite As String = "", myTu As New trusteeships, theTuList As Hashtable(), myMain As New main
        Dim strTp As String = "", myShop As New shop, theShop As New Hashtable
        theTuList = myTu.getTuList(Session("id"))
        If theTuList(0)("count") = 0 Then Exit Sub
        For Each tu As Hashtable In theTuList
            theShop = myShop.getShopInfo(tu("shopId"))
            strTp = "<span class='tuspan'>" & theShop("shop_name") & "--日期从" & tu("startDate") & "到" & tu("endDate") & "&nbsp;时间" & tu("startTime") & ":00-" & tu("endTime") & ":00</span>"
            strTp += "<a class='cancel' href='server_page/shop_goods.aspx?action=delTu&id=" & tu("id") & "'>取消</a>"
            strWite = myMain.userCenterSetOutInfo("托管：", strTp, strWite, False)
        Next
        Response.Write(strWite)
    End Sub
End Class