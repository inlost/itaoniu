Public Class post
    Inherits System.Web.UI.Page
    Dim thePost As New Hashtable, myClassified As New classifieds
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("id") = Nothing Then
            Response.Redirect("default.aspx")
        End If
        thePost = myClassified.post_get(Request.Params("id"))

    End Sub
    Public Sub getPost(w As String)
        Select Case w
            Case "title"
                Response.Write(thePost("title"))
            Case "phone"
                Response.Write(thePost("phone"))
            Case "name"
                Response.Write(thePost("name"))
            Case "date"
                Response.Write(thePost("postDate"))
            Case "content"
                Response.Write(thePost("thePost"))
            Case "other"
                Response.Write(thePost("ShowInfow"))
        End Select
    End Sub
End Class