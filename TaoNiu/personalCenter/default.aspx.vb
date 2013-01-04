Public Class _default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getTimeLine()
        Dim myCenter As New pCenter, posts As Hashtable(), strWite As String = ""
        posts = myCenter.timeLine_main(Session("id"), IIf(Request.Params("page") = Nothing, 1, Request.Params("page")), 30)
        For Each post In posts
            strWite += myCenter.postShow(post)
        Next
        Response.Write(strWite)
    End Sub
End Class