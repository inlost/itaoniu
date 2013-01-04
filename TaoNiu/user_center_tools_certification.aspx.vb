Public Class user_center_tools_certification
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("msg") <> Nothing Then
            If Request.Params("msg") = "had" Then
                Response.Write("<script>alert('不能重复申请认证')</script>")
            End If
        End If
    End Sub
    Public Sub getCeType()
        Dim myShop As New shop, myLists As Hashtable()
        myLists = myShop.getCeType
        For Each item As Hashtable In myLists
            Response.Write("<option value='" & item("id") & "'>" & item("name") & "</option>")
        Next
    End Sub
    Public Sub getIsFinishCeList()
        Dim myShop As New shop, myLists As Hashtable()
        myLists = myShop.getCeType
        For Each item As Hashtable In myLists
            Response.Write("<option value='" & item("id") & "'>" & item("name") & "</option>")
        Next
    End Sub
    Public Sub getCeList()
        Dim myShop As New shop, myLists As Hashtable(), theCe As New Hashtable, myMain As New main, strWite As String
        Dim theShp As New Hashtable
        myLists = myShop.getCertifList(Session("id"))
        If myLists(0)("count") <> 0 Then
            For Each item As Hashtable In myLists
                theCe = myShop.getCe(item("apply_type"))
                theShp = myShop.getShopInfo(item("shopId"))
                strWite = myMain.userCenterSetOutInfo("认证:", theShp("shop_name") & "---" & theCe("name") & "---" & IIf(item("apply_status") = 2, "审核通过", "审核中"), strWite, False)
            Next
        End If
        Response.Write(strWite)
    End Sub
    Public Sub getShopList()
        Dim inQuery() As dbHelper.inQuery, myShop As New shop
        inQuery = myShop.getShopList(Session("id"))
        For Each inList As dbHelper.inQuery In inQuery
            Response.Write("<option value='" & inList.value & "'>" & inList.name & "</option>")
        Next
    End Sub
End Class