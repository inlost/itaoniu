Public Class user_center
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("onTheWay") = Nothing Or Session("onTheWay") <> "loginSuccess" Then
            Response.Redirect("login.aspx")
        End If
    End Sub
    Public Sub setId(ByVal linkName As String)
        '输出用户名
        Select Case linkName
            Case "我的信息"
                If Request.Params("at") = Nothing Or Request.Params("at") = "home" Then
                    Response.Write("id='actLink'")
                End If
            Case "我的店铺"
                If Request.Params("at") = "myShop" Then
                    Response.Write("id='actLink'")
                End If
            Case "我要开店"
                If Request.Params("at") = "newShop" Then
                    Response.Write("id='actLink'")
                End If
            Case "店铺商品"
                If Request.Params("at") = "goods" Then
                    Response.Write("id='actLink'")
                End If
            Case "我的订单"
                If Request.Params("at") = "orders" Then
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
    Public Function hasShop() As Boolean
        Dim myShop As New shop
        Return myShop.hasShop(Session("id"))
    End Function
    Public Sub siteUrl()
        Dim myMain As New main
        Response.Write(myMain.siteUrl)
    End Sub
End Class