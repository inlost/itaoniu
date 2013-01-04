Public Class _default5
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getCatList()
        Dim myClassifieds As New classifieds, strWite As String = "", deepOneCats As Hashtable()
        deepOneCats = myClassifieds.cat_get(1, -1)
        For Each deepOne As Hashtable In deepOneCats
            strWite += "<div class='topLeavel'>"
            strWite += "<h2><b>" & deepOne("catName") & "&nbsp;》</b></h2>"
            Dim deepTwoCats As Hashtable()
            deepTwoCats = myClassifieds.cat_get(2, deepOne("id"))
            Try
                For Each deepTwo As Hashtable In deepTwoCats
                    strWite += IIf(Len(deepTwo("catName")) > 3, "<a href='cat.aspx?id=" & deepTwo("id") & "'>" & deepTwo("catName") & "</a><br />", "<em><a href='cat.aspx?id=" & deepTwo("id") & "'>" & deepTwo("catName") & "</a></em>")
                Next
            Catch ex As Exception
            End Try
            strWite += "</div>"
        Next
        Response.Write(strWite)
    End Sub
End Class