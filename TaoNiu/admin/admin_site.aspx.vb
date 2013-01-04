Public Class admin_site
    Inherits System.Web.UI.Page
    Dim myMain As New main
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        site_icp.Text = myMain.siteIcp
        site_name.Text = myMain.siteName
        site_smtp.Text = myMain.siteSmtp
        site_smtp_p.Text = myMain.siteSmtp_pass
        site_smtp_u.Text = myMain.siteSmtp_user
        site_support.Text = myMain.siteSupportEmail
        site_url.Text = myMain.siteUrl
    End Sub
End Class