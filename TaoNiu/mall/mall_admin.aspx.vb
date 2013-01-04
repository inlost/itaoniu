Public Class mall_admin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Function getMallInfo(ByVal w As String)
        Dim strRt As String = w
        Return strRt
    End Function
    Public Sub witeMallInfo(ByVal title As String, ByVal w As String, Optional ByVal halfWide As Boolean = True)
        Dim myMain As New main, strWite As String
        strWite = myMain.userCenterSetOutInfo(title, getMallInfo(w), "", halfWide)
        Response.Write(strWite)
    End Sub

End Class