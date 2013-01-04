Public Class taskList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getShList()
        Dim prePage As Integer = 10, page As Integer = IIf(Request.Params("page") = Nothing, 1, Request.Params("page"))
        Dim goodsList As Hashtable(), myWeituo As New weituo
        goodsList = myWeituo.getWtListByTyper(1, Session("id"), page, prePage)
        If goodsList(0)("count") < 1 Then Exit Sub
        Dim strWite As String = "", myMain As New main, strTp As String
        For Each good As Hashtable In goodsList
            strTp = "<a href='../shopProduct.aspx?shop=-1&gid=" & good("gid") & "'>【" & good("goodName") & "】</a>--" & IIf(good("status") = 1, "[未审核]", "[通过]") & "---提交日期：" & good("luruDate")
            strWite = myMain.userCenterSetOutInfo("录入", strTp, strWite, False)
        Next
        Dim pageHelper As New pageLink

        Response.Write(strWite)
    End Sub
    Public Sub getShList2()
        Dim prePage As Integer = 10, page As Integer = IIf(Request.Params("page") = Nothing, 1, Request.Params("page"))
        Dim goodsList As Hashtable(), myWeituo As New weituo
        goodsList = myWeituo.getWtListByTyper(2, Session("id"), page, prePage)
        If goodsList(0)("count") < 1 Then Exit Sub
        Dim strWite As String = "", myMain As New main, strTp As String
        For Each good As Hashtable In goodsList
            strTp = "<a href='../shopProduct.aspx?shop=-1&gid=" & good("gid") & "'>【" & good("goodName") & "】</a>--" & IIf(good("status") = 1, "[未审核]", "[通过]") & "---提交日期：" & good("luruDate")
            strWite = myMain.userCenterSetOutInfo("录入", strTp, strWite, False)
        Next
        Response.Write(strWite)
    End Sub
End Class