Imports Discuz.Toolkit
Public Class bbs_CallBack
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdateAuthToken()
    End Sub
    Private Sub UpdateAuthToken()
        Dim ds As DiscuzSession = DiscuzSessionHelper.GetSession()
        Dim errorcode As Integer = 0
        Dim [next] As String = Nothing
        Try
            [next] = Request.QueryString("next").ToString()
            Session("AuthToken") = Request.QueryString("auth_token").ToString()
        Catch
        End Try
        If ValidateAuthToken(ds, errorcode) Then
            RedirectPage([next])
        Else
            GetAuthToken([next], ds)
        End If
    End Sub

    '获取AuthToken
    Public Sub GetAuthToken(ByVal n As String, ByVal ds As DiscuzSession)
        Response.Redirect(ds.CreateToken().ToString() & "&next=" & n)
    End Sub

    '成功更新AuthToken后进行页面转向
    Public Sub RedirectPage(ByVal n As String)
        If Session("redirectURL") <> Nothing Then
            Dim redirectURL As String = Session("redirectURL")
            Session("redirectURL") = Nothing
            Response.Redirect("../" & redirectURL.Replace("----", "&"))
        Else
            Response.Redirect("~/user_center.aspx")
        End If
    End Sub

    '验证当前的AuthToken是否可用
    Public Function ValidateAuthToken(ByVal ds As DiscuzSession, ByRef Errorcode As Integer) As Boolean
        Try
            ds.session_info = ds.GetSessionFromToken(Session("AuthToken").ToString())
            Errorcode = 0
            Return True
        Catch d As DiscuzException
            Errorcode = d.ErrorCode
            Return False
        Catch generatedExceptionName As NullReferenceException
            Errorcode = 0
            Return False
        End Try
    End Function
End Class