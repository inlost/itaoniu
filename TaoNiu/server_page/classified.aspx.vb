Public Class classified1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myClassified As New classifieds, rt As Hashtable()
        Select Case Request.Params("action")
            Case "getCat"
                Try
                    Convert.ToInt32(Request.Params("father"))
                    Convert.ToInt32(Request.Params("deep"))
                    rt = myClassified.cat_get(Request.Params("deep"), Request.Params("father"))
                    If rt(0)("count") > 0 Then
                        For Each item In rt
                            Response.Write("<option value='" & item("id") & "'>" & item("catName") & "</option>")
                        Next
                    End If
                Catch ex As Exception
                End Try
            Case "newPost"
                Dim thePost As New Hashtable
                    thePost("@uid") = Session("id")
                thePost("@cat") = Request.Form("cat1") & "-" & Request.Form("cat2") & "-" & Request.Form("cat3")
                thePost("@title") = Request.Form("title")
                    thePost("@thePost") = Request.Form("postContent")
                    thePost("@phone") = Request.Form("phone")
                    thePost("@name") = Request.Form("name")
                thePost("@price") = IIf(Len(Trim(Request.Form("price"))) = 0, -1, Trim(Request.Form("price")))
                If Request.Files.Count > 0 Then
                    Dim myImage As New images, hashRt As New Hashtable
                    hashRt = myImage.upLoadImg(Request.Files("img"), Session("email"), Server.MapPath("~"))
                    If hashRt("status") = "ok" Then
                        thePost("@img") = hashRt("smallPath")
                    Else
                        thePost("@img") = ""
                    End If
                End If
                Dim myClassifieds As New classifieds, pId = myClassified.post_add(thePost)
                If pId > 0 Then
                    Response.Redirect("../classified/post.aspx?id=" & pId)
                Else
                    Response.Redirect("../classified/default.aspx")
                End If
        End Select
    End Sub

End Class