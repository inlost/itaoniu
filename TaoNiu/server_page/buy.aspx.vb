Public Class buy
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Request.Params("action")
            Case "shouC_good"
                need_login()
                Dim myFavorites As New favorites
                Try
                    If myFavorites.add(Session("id"), "good", Request.Params("gid")) Then
                        Response.Write("success")
                    Else
                        Response.Write("error")
                    End If
                Catch ex As Exception
                    Response.Write("error")
                End Try
                If Request.Params("by") <> "ajax" Then
                    Response.Redirect("../shopProduct.aspx?shop=" & Request.Params("shop") & "&gid=" & Request.Params("gid"))
                End If
            Case "shouC_shop"
                need_login()
                Dim myFavorites As New favorites
                Try
                    If myFavorites.add(Session("id"), "shop", Request.Params("shop")) Then
                        Response.Write("success")
                    Else
                        Response.Write("error")
                    End If
                Catch ex As Exception
                    Response.Write("error")
                End Try
                If Request.Params("by") <> "ajax" Then
                    Response.Redirect("../shopProduct.aspx?shop=" & Request.Params("shop") & "&gid=" & Request.Params("gid"))
                End If
            Case "gwc_add"
                gwc_add()
                If Request.Params("by") <> "ajax" Then
                    Response.Redirect("../shopProduct.aspx?shop=" & Request.Params("shop") & "&gid=" & Request.Params("gid"))
                End If
            Case Else
                Response.Write("Good luck")
        End Select
    End Sub
    Public Sub need_login()
        If Session("onTheWay") <> "loginSuccess" Then
            Dim strUrl As String
            strUrl = "../login.aspx?msg=请先登录，登录后才能进行收藏操作&redirectURL=server_page/buy.aspx"
            strUrl += Request.Url.Query.Replace("&", "----")
            Response.Redirect(strUrl)
        End If
    End Sub
    Public Sub gwc_add()
        Try
            Len(Request.Cookies("gwc")("count"))
            Dim old As String = Request.Cookies("gwc")("goods")
            Dim count As String = Request.Cookies("gwc")("count")
            If count >= 1 Then
                old += "-" & Request.Params("gid") & "^" & Request.Form("number")
            Else
                count = 0
                old = Request.Params("gid") & "^" & Request.Form("number")
            End If
            Response.Cookies("gwc")("goods") = old
            Response.Cookies("gwc")("count") = count + 1
        Catch ex As Exception
            Response.Cookies("gwc")("count") = 1
            Response.Cookies("gwc")("goods") = Request.Params("gid") & "^" & Request.Form("number")
        End Try
        Response.Write("success")
    End Sub
End Class