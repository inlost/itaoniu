Public Class main1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("action") = "admLogin" Then
            If Session("admLoginTime") <> Nothing Then
                If (Session("admLoginTime") < 4) Then
                    If (Session("CheckCode").ToString.ToUpper = Request.Form("vdCode").ToUpper) Then
                        Dim myAdm As New adm, myMain As New main
                        If (myAdm.admin(Request.Form("id"), Request.Form("pass"))) And checkSafe(Request.Form("safe")) Then
                            Session("adminId") = Request.Form("id")
                            Session("uid") = Request.Form("id")
                            myAdm.loginLogAdd(Request.Form("id"), getUserIp)
                            Response.Redirect("../default.aspx")
                        Else
                            myAdm.errorLogAdd(Request.Form("id"), getUserIp)
                            Session("admLoginTime") = Session("admLoginTime") + 1
                            Response.Redirect("../login.aspx")
                        End If
                    Else
                        Response.Redirect("../login.aspx?msg=vdcod_error")
                    End If
                End If
            End If
        ElseIf Request.Params("action") = "siteInfo" Then
            Dim myMain As New main
            myMain.siteIcp = myMain.filterCheck(Request.Form("ctl00$ContentContent$site_icp"))
            myMain.siteName = myMain.filterCheck(Request.Form("ctl00$ContentContent$site_name"))
            myMain.siteSmtp = myMain.filterCheck(Request.Form("ctl00$ContentContent$site_smtp"))
            If Request.Form("ctl00$ContentContent$site_smtp_p").ToString <> "" Then
                myMain.siteSmtp_pass = myMain.filterCheck(Request.Form("ctl00$ContentContent$site_smtp_p"))
            End If
            myMain.siteSmtp_user = myMain.filterCheck(Request.Form("ctl00$ContentContent$site_smtp_u"))
            myMain.siteSupportEmail = myMain.filterCheck(Request.Form("ctl00$ContentContent$site_support"))
            myMain.siteUrl = myMain.filterCheck(Request.Form("ctl00$ContentContent$site_url"))
            Response.Redirect("../admin_site.aspx")
        End If
    End Sub
    Function getUserIp() As String
        Dim userIP As String
        If (Request.ServerVariables("HTTP_VIA") = Nothing) Then
            userIP = Request.UserHostAddress
        Else
            userIP = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        End If
        Return userIP
    End Function
    Function checkSafe(ByVal strSafe As String) As Boolean
        If strSafe Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
End Class