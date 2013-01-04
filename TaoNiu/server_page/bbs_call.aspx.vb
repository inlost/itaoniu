Public Class bbs_call
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strTp As String = ""
        For i = 0 To Request.Params.Count - 1
            strTp += Request.Params(i) & vbCrLf
            '            MsgBox(Request.Params.GetKey(i) & "=" & Request.Params(i))
        Next
        Select Case Request.Params("action")
            Case "newtopic"
                newTopic()
            Case "reply"
                reply()
            Case Else
                Exit Sub
        End Select
    End Sub
    Private Sub newTopic()
        userLogin(Request.Params("author"))
        If Session("onTheWay") <> "loginSuccess" Then
            Exit Sub
        End If
        Dim myCenter As New pCenter
        Dim newText As New Hashtable
        newText("@uid") = Session("id")
        newText("@short") = "<div class='bbsPost'>发表帖子：<h3><a href='http://localhost:8000/bbs/showtopic-" & Request.Params("tid") & ".aspx' target='_blank'>《" & Request.Params("title") & "》</a></h3></div>"
        newText("@oldPost") = Nothing
        newText("@sourceId") = Nothing
        newText("@relyId") = Nothing
        newText("@title") = Nothing
        newText("@pic") = Nothing
        newText("@see") = 1
        Dim rt As Integer = myCenter.new_bbs(newText)
    End Sub
    Private Sub reply()
        userLogin(Request.Params("poster"))
        If Session("onTheWay") <> "loginSuccess" Then
            Exit Sub
        End If
        Dim myCenter As New pCenter
        Dim newText As New Hashtable
        newText("@uid") = Session("id")
        newText("@short") = "<div class='bbsPost'>回复帖子：<h3><a href='http://localhost:8000/bbs/showtopic-" & Request.Params("tid") & ".aspx' target='_blank'>《" & Request.Params("topic_title") & "》</a></h3></div>"
        newText("@oldPost") = Nothing
        newText("@sourceId") = Nothing
        newText("@relyId") = Nothing
        newText("@title") = Nothing
        newText("@pic") = Nothing
        newText("@see") = 1
        Dim rt As Integer = myCenter.new_bbs(newText)
    End Sub
    Private Sub userLogin(ByVal userName)
        Dim myUser As New user, theUser As New Hashtable, strRt As String = ""
        theUser = myUser.getUserInfoByName(userName)
        strRt = myUser.user_login(theUser("uid"), "255.255.255.255")
        If strRt <> "error" Then
            Dim myMain As New main, inQuery() As dbHelper.inQuery
            inQuery = myMain.stringToInQuery(strRt, "|")
            For Each qItem As dbHelper.inQuery In inQuery
                If Len(qItem.name) <> 0 And qItem.name <> "pass" Then
                    Session(qItem.name) = qItem.value
                End If
            Next
            Session("uid") = Session("email")
            Session("onTheWay") = "loginSuccess"
        Else
            Exit Sub
        End If
    End Sub
End Class