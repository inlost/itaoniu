Imports System.Collections.Generic
Imports System.Text
Imports Discuz.Toolkit
Public Class DiscuzSessionHelper
    Private Shared apikey As String, secret As String, url As String
    Private Shared ds As DiscuzSession
    Shared Sub New()
        apikey = "dc3d5dcdbeba3ed544ce2784fa785b0c"
        secret = "ad5fd830063241cfd11da1a0b7ce5d5c"
        url = "http://niuchang.itaoniu.com/"
        ds = New DiscuzSession(apikey, secret, url)
    End Sub

    Public Shared Function GetSession() As DiscuzSession
        Return ds
    End Function
End Class
