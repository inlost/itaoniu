Imports System.IO
Imports System.Globalization

Public Class img_upload
    Inherits System.Web.UI.Page
    Private maxSize As Integer = 1000000
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myImg As New images, filePath As String, fileName As String
        Dim imgFile As HttpPostedFile
        imgFile = Request.Files("imgFile")
        If Session("uid") Is Nothing Then
            showError("你没有权限上传文件")
            Response.Redirect("../../../../default.aspx")
        End If
        If (imgFile Is Nothing) Then
            showError("请选择文件")
        End If
        filePath = Server.MapPath("~") & "upload\userImages\" & Session("uid") & "\" & DateTime.Now.Year & "\" & DateTime.Now.Month & "\"
        If Directory.Exists(filePath) = False Then
            Directory.CreateDirectory(filePath)
        End If
        fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) & Path.GetExtension(imgFile.FileName).ToLower()
        If (imgFile.InputStream Is Nothing Or imgFile.InputStream.Length > maxSize) Then
            showError("文件大小超过限制")
        End If
        If imgFile.ContentType.ToLower().IndexOf("image") < 0 Then
            showError("不允许上传的文件类型")
        End If
        imgFile.SaveAs(filePath & fileName)
        Dim myHash As New Hashtable, myMain As New main
        myHash("url") = myMain.siteUrl & "upload/userImages/" & Session("uid") & "/" & DateTime.Now.Year & "/" & DateTime.Now.Month & "/" & fileName
        myHash("error") = 0
        Response.AddHeader("Content-Type", "text/html; charset=UTF-8")
        fileName = myMain.hashtableToJson(myHash)
        Response.Write(fileName)
    End Sub
    Private Sub showError(ByVal msg As String)
        Dim myHash As New Hashtable, myMain As New main, fileName As String
        myHash("error") = 1
        myHash("message") = msg
        Response.AddHeader("Content-Type", "text/html; charset=UTF-8")
        fileName = myMain.hashtableToJson(myHash)
        Response.Write(fileName)
    End Sub
End Class