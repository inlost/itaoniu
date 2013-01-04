Public Class mall
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub setId(ByVal linkName As String)
        '输出用户名
        Select Case linkName
            Case "信息概览"
                If Request.Params("at") = Nothing Or Request.Params("at") = "info" Then
                    Response.Write("id='actLink'")
                End If
            Case "商城店铺"
                If Request.Params("at") = "shops" Then
                    Response.Write("id='actLink'")
                End If
            Case "商城设置"
                If Request.Params("at") = "set" Then
                    Response.Write("id='actLink'")
                End If
            Case "报表系统"
                If Request.Params("at") = "report" Then
                    Response.Write("id='actLink'")
                End If
            Case "广告系统"
                If Request.Params("at") = "ad" Then
                    Response.Write("id='actLink'")
                End If
            Case "店铺订单"
                If Request.Params("at") = "myorders" Then
                    Response.Write("id='actLink'")
                End If
            Case "我的收藏"
                If Request.Params("at") = "fav" Then
                    Response.Write("id='actLink'")
                End If
            Case "我的社区"
                If Request.Params("at") = "pc" Then
                    Response.Write("id='actLink'")
                End If
            Case "营销平台"
                If Request.Params("at") = "sale" Then
                    Response.Write("id='actLink'")
                End If
            Case "资金管理"
                If Request.Params("at") = "money" Then
                    Response.Write("id='actLink'")
                End If
            Case "店铺工具"
                If Request.Params("at") = "tools" Then
                    Response.Write("id='actLink'")
                End If
        End Select
    End Sub
    Public Sub siteUrl()
        Dim myMain As New main
        Response.Write(myMain.siteUrl)
    End Sub
End Class