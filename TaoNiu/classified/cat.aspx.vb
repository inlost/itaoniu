Public Class cat
    Inherits System.Web.UI.Page
    Dim myClassified As New classifieds, theCat As New Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            theCat = myClassified.cat_get(Request.Params("id"))
        Catch ex As Exception
            Response.Redirect("default.aspx")
        End Try
    End Sub
    Public Sub witeId()
        Response.Write(Request.Params("id"))
    End Sub
    Public Sub witeCatName()
        Response.Write(theCat("catName"))
    End Sub
    Public Sub getThirdLevelCact()
        Dim catFather As Integer = IIf(theCat("catDeep") = 3, theCat("catFather"), theCat("id"))
        Dim catDeep As Integer = IIf(theCat("catDeep") = 3, 3, theCat("catDeep") + 1)
        Dim myClassifieds As New classifieds,strWite As String = ""
        Try
            Dim cats As Hashtable() = myClassifieds.cat_get(catDeep, catFather)
            For Each cat As Hashtable In cats
                Dim strClass As String = IIf(cat("id") = Request.Params("id"), "class='nowAct'", "class='normal'")
                strWite += "<a " & strClass & " href='cat.aspx?id=" & cat("id") & ">" & cat("catName") & "</a>"
            Next
        Catch ex As Exception
            Exit Sub
        End Try
        Response.Write(strWite)
    End Sub
    Public Sub getList()
        Dim myClassifieds As New classifieds, items As Hashtable()
        items = myClassifieds.posts_get(Request.Params("id"), IIf(Request.Params("page") = Nothing, 1, Request.Params("page")), 14)
        If items(0)("count") <= 0 Then Exit Sub
        Dim strWite As String = ""
        For Each item As Hashtable In items
            strWite += "<div class='item afterClear'>"
            Dim strImg As String = ""
            Try
                If Len(item("img")) = 0 Then
                    strImg = "http://pic2.58.com/n/images/list/noimg.gif"
                Else
                    strImg = item("img")
                End If
            Catch ex As Exception
                strImg = "http://pic2.58.com/n/images/list/noimg.gif"
            End Try
            strWite += "<div class='itemImg'><a href='post.aspx?id=" & item("id") & "'><img src='" & strImg & "'/></a></div>"
            strWite += "<div class='itemPrice'>" & item("price") & "</div>"
            strWite += "<div class='itemTitle'><a href='post.aspx?id=" & item("id") & "'>" & item("title") & "</a></div>"
            strWite += "<div class='itemDate'>" & item("postDate") & "</div>"
            strWite += "</div>"
        Next
        Response.Write(strWite)
    End Sub
    Public Sub getPageLink()
        Dim myPage As New pageLink
        Dim strWite As String = ""
        strWite = myPage.getPageLink(theCat("count"), 14, If(Request.Params("page") = Nothing, 1, Request.Params("page")), "?id=" & Request.Params("id"))
        Response.Write(strWite)
    End Sub
End Class