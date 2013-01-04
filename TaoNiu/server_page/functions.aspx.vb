Public Class functions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getWite()
        Dim strWite As String = ""
        Select Case Request.Params("action")
            Case "announcements"
                Try
                    Convert.ToInt32(Request.Params("id"))
                    Dim myMain As New main, myAn As New Hashtable
                    myAn = myMain.announcement_get(Request.Params("id"))
                    strWite += "<h2>" & myAn("title") & "</h2>"
                    strWite += "<div>" & myAn("announcement") & "</div>"
                    strWite += "<p style='text-align:right;margin-right:10px;'>" & myAn("time") & "</p>"
                Catch ex As Exception
                    strWite = "Good Luck!"
                End Try
        End Select
        Response.Write(strWite)
    End Sub
End Class