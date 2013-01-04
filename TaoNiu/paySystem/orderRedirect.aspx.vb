Public Class orderRedirect
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strUri As String = Request.Url.AbsolutePath
        Dim pos = strUri.LastIndexOf("/")
        Try
            pos = Right(strUri, Len(strUri) - pos - 1)
            Dim myGood As New goods, good As New Hashtable
            good = myGood.goods_get(pos)
            strUri = "../shopProduct.aspx?shop=" & good("shopId") & "&gid=" & pos
            Response.Redirect(strUri)
        Catch ex As Exception
            pos = strUri.IndexOf("-")
            Dim money As String = Right(strUri, Len(strUri) - pos - 1)
            MsgBox("向桃牛账户充值" & money & "元")
        End Try
    End Sub
End Class