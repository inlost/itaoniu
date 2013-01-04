Public Class pCenter
    Private Function post_add(ByVal post As Hashtable) As Integer
        Dim myHelper As New dbHelper, myDs As New DataSet
        Try
            myDs = myHelper.getQuery("pCenter_new", myHelper.hashtableToInquery(post), "rt")
            Return myDs.Tables("rt").Rows(0)(0)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function new_text(ByVal newText As Hashtable) As Integer
        newText("@type") = "1"
        Return post_add(newText)
    End Function
    Public Function new_image(ByVal newText As Hashtable) As Integer
        newText("@type") = "2"
        Return post_add(newText)
    End Function
    Public Function new_music(ByVal newText As Hashtable) As Integer
        newText("@type") = "3"
        Return post_add(newText)
    End Function
    Public Function new_link(ByVal newText As Hashtable) As Integer
        newText("@type") = "4"
        Return post_add(newText)
    End Function
    Public Function new_video(ByVal newText As Hashtable) As Integer
        newText("@type") = "5"
        Return post_add(newText)
    End Function
    Public Function new_bbs(ByVal newText As Hashtable) As Integer
        newText("@type") = "7"
        Return post_add(newText)
    End Function
    Public Function timeLine_main(ByVal uid As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet
        Dim myQuery(0 To 2) As dbHelper.inQuery
        myQuery(0).name = "@uid"
        myQuery(0).value = uid
        myQuery(1).name = "@page"
        myQuery(1).value = page
        myQuery(2).name = "@prePage"
        myQuery(2).value = prePage
        myDs = myHelper.getQuery("pCenter_timeLine_main", myQuery, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "rt")
    End Function
    Public Function timeLine_default(ByVal number As Integer) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet
        Dim myQuery(0 To 0) As dbHelper.inQuery
        myQuery(0).name = "@number"
        myQuery(0).value = number
        myDs = myHelper.getQuery("pCenter_timeLine_default", myQuery, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "rt")
    End Function
    Public Function postShow(ByVal post As Hashtable) As String
        Select Case post("type")
            Case "1"
                Return textShow(post)
            Case "2"
                Return imgShow(post)
            Case "3"
                Return musicShow(post)
            Case "4"
                Return linkShow(post)
            Case "5"
                Return videoShow(post)
            Case Else
                Return ""
        End Select
    End Function
    Private Function textShow(ByVal post As Hashtable) As String
        Dim strRt As String = "<div class='post_body'>"
        If post("title") <> "" Then
            strRt += "<h2 class='post_title'>" & post("title") & "</h2>"
        End If
        strRt += "<div class='post_content'>"
        If post("pic") <> "" Then
            strRt += "<img src='" & post("pic") & "'/>"
        End If
        strRt += post("short")
        strRt += "</div>"
        If Len(post("oldPost")) > 0 Then
            strRt += "<div class='rely_post'>"
            strRt += post("oldPost")
            strRt += "</div>"
        End If
        strRt += "</div>"
        Return setPost(strRt, post("id"))
    End Function
    Private Function imgShow(ByVal post As Hashtable) As String
        Dim strRt As String = "<div class='post_body'>"
        If post("title") <> "" Then
            strRt += "<h2 class='post_title'>" & post("title") & "</h2>"
        End If
        strRt += "<div class='post_content'><div class='images'>"
        strRt += post("short")
        strRt += "</div></div>"
        If Len(post("oldPost")) > 0 Then
            strRt += "<div class='rely_post'>"
            strRt += post("oldPost")
            strRt += "</div>"
        End If
        strRt += "</div>"
        Return setPost(strRt, post("id"))
    End Function
    Private Function musicShow(ByVal post As Hashtable) As String
        Dim strRt As String = "<div class='post_body'>"
        If post("title") <> "" Then
            strRt += "<h2 class='post_title'>" & post("title") & "</h2>"
        End If
        strRt += "<div class='post_content'>"
        If post("pic") <> "" Then
            strRt += "<img src='" & post("pic") & "'/>"
        End If
        strRt += post("short")
        strRt += "</div>"
        If Len(post("oldPost")) > 0 Then
            strRt += "<div class='rely_post'>"
            strRt += post("oldPost")
            strRt += "</div>"
        End If
        strRt += "</div>"
        Return setPost(strRt, post("id"))
    End Function
    Private Function linkShow(ByVal post As Hashtable) As String
        Dim strRt As String = "<div class='post_body'>"
        If post("title") <> "" Then
            strRt += "<h2 class='post_title'>" & post("title") & "</h2>"
        End If
        strRt += "<div class='post_content'>"
        strRt += "<h3><a href='" & post("short") & "'>" & post("short") & "</a></h3>"
        strRt += "</div>"
        If Len(post("oldPost")) > 0 Then
            strRt += "<div class='rely_post'>"
            strRt += post("oldPost")
            strRt += "</div>"
        End If
        strRt += "</div>"
        Return setPost(strRt, post("id"))
    End Function
    Private Function videoShow(ByVal post As Hashtable) As String
        Dim strRt As String = "<div class='post_body'>"
        If post("title") <> "" Then
            strRt += "<h2 class='post_title'>" & post("title") & "</h2>"
        End If
        strRt += "<div class='post_content'>"
        If post("pic") <> "" Then
            strRt += "<img src='" & post("pic") & "'/>"
        End If
        strRt += "<div class='video_hidden'>" & post("short") & "</div>"
        strRt += "</div>"
        If Len(post("oldPost")) > 0 Then
            strRt += "<div class='rely_post'>"
            strRt += post("oldPost")
            strRt += "</div>"
        End If
        strRt += "</div>"
        Return setPost(strRt, post("id"))
    End Function
    Private Function setPost(ByVal inner As String, ByVal pid As Integer) As String
        Dim strRt As String = "<div class='postBox'>"
        strRt += "<div class='pb-avatar'><a class='blog-avatar' href='http://inlost.diandian.com' style='background-image:url(http://farm1.img.libdd.com/156/C87DB09F2BDD86536F79B35F1FFB169C_63_63.jpg)'>那个白</a></div>"
        strRt += inner
        strRt += "<div class='postFunction'><a class='p_hy' href='" & pid & "'>回应</a><a class='p_zz' href='" & pid & "'>转载</a><a class='p_sc' href='" & pid & "'>收藏</a></div>"
        strRt += "</div>"
        Return strRt
    End Function
End Class
