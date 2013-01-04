Public Class agent
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("onTheWay") = Nothing Or Session("onTheWay") <> "loginSuccess" Then
            Response.Redirect("login.aspx")
        End If
    End Sub
    Public Sub siteUrl()
        Dim myMain As New main
        Response.Write(myMain.siteUrl)
    End Sub
    Public Sub setId(ByVal linkName As String)
        '输出用户名
        Select Case linkName
            Case "商品录入"
                If Request.Params("at") = Nothing Or Request.Params("at") = "goods" Then
                    Response.Write("id='actLink'")
                End If
            Case "录入列表"
                If Request.Params("at") = "tl" Then
                    Response.Write("id='actLink'")
                End If
        End Select
    End Sub
End Class