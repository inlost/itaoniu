Public Class weibo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getSnsPosts()
        Dim myPcenter As New pCenter, strWite As String = ""
        Dim posts As Hashtable() = myPcenter.timeLine_default("10")
        If posts(0)("count") = 0 Then Exit Sub
        Dim myUser As New user
        For Each post As Hashtable In posts
            strWite += "<li class='afterClear weibo-P'>"
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
        users = myUser.getUserList("12")
        If users(0)("count") = 0 Then Exit Sub
        For Each u As Hashtable In users
            strWite += "<div class='weiboList'>"
            strWite += "<div class='weiboList-user'>"
            strWite += "<img src='" & myUser.getBbsIcon(u("niceName")) & "'/>"
            strWite += "<h3>" & u("niceName") & "</h3>"
            strWite += "</div>"
            strWite += "</div>"
        Next
        Response.Write(strWite)
    End Sub
End Class