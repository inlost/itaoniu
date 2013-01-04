Public Class video
    Public Function getVideoInfo(ByVal strUri As String, ByVal strResponse As String) As Hashtable
        Dim rt As New Hashtable
        Dim site As String = getVideoSite(strUri)
        If site = Nothing Then Return rt
        Select Case site
            Case "youku"
                Return getYouku(strResponse, strUri)
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function getVideoSite(ByVal strUri As String) As String
        Try
            If strUri.ToLower.IndexOf("youku.com") > 1 Then
                Return "youku"
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function getYouku(ByVal re As String, ByVal strUri As String) As Hashtable
        Dim rt As New Hashtable
        rt("id") = getCutInfo(re, "var videoid = '", "';")
        rt("img") = getCutInfo(re, "utf8&pic=", """").ToUpper
        rt("title") = getCutInfo(re, "<title>", "</title>")
        rt("title") = Left(rt("title"), Len(rt("title")) - 18)
        rt("uri") = strUri
        rt("player") = getPlayer("http://player.youku.com/player.php/sid/" & rt("id") & "/v.swf&amp;isAutoPlay=true")
        Return rt
    End Function
    Private Function getCutInfo(ByVal str As String, ByVal strStart As String, ByVal strEnd As String) As String
        str = str.ToLower
        Dim totle As Integer = Len(str)
        Dim posStart As Integer = str.IndexOf(strStart)
        Dim strTp As String = Right(str, totle - posStart - Len(strStart))
        Dim posEnd As String = strTp.IndexOf(strEnd)
        Return Left(strTp, posEnd)
    End Function
    Private Function getPlayer(ByVal strSwf As String) As String
        Return "<embed width='500' height='416' type='application/x-shockwave-flash' wmode='transparent' allowscriptaccess='sameDomain' src='" & strSwf & "' flashvars='isAutoPlay=true'>"
    End Function
End Class
