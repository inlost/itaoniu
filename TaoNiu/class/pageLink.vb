Public Class pageLink
    Public Function getPageLink(ByVal countTotle As Integer, ByVal prePage As Integer, Optional ByVal nowPage As Integer = 1, Optional ByVal strUrl As String = "?") As String
        Dim strWite As String = "", perPage As Integer = prePage
        Dim strTp As String = strUrl
        Dim pageNow = nowPage
        strWite += "<ul id='pageLink'>"
        If nowPage > 1 Then
            strWite += "<li><a href='" & strTp & "&page=" & pageNow - 1 & "'>上一页</li>"
        End If
        Dim iPagesTp As Double = countTotle / perPage
        Dim iPages As String = ""
        If (iPagesTp.ToString.IndexOf(".") > 0) Then
            iPages = Left(iPagesTp, iPagesTp.ToString.IndexOf("."))
        Else
            iPages = iPagesTp
        End If
        If pageNow = 0 Then pageNow = 1
        iPages = IIf(countTotle Mod perPage <> 0, iPages + 1, iPages)
        If iPages < 2 Then Return ""
        For i As Integer = 1 To iPages
            If i = pageNow Then
                strWite += "<li><a class='pageNow' href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i > pageNow - 3 And i < pageNow + 5 Then
                strWite += "<li><a href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i = 1 Or i = iPages Then
                strWite += "<li><a href='" & strTp & "&page=" & i & "'>" & i & "</a></li>"
            ElseIf i = pageNow - 3 Or i = pageNow + 5 Then
                strWite += "<li>…</li>"
            Else
                strWite = strWite
            End If
        Next
        If pageNow <> iPages Then
            strWite += "<li><a href='" & strTp & "&page=" & pageNow + 1 & "'>下一页</a></li>"
        End If
        strWite += "</ul>"
        Return strWite
    End Function
End Class
