Public Class vdImg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateImage.DrawImage()
    End Sub
End Class