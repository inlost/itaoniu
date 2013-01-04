Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.IO
Imports System.Globalization

Class images
    Public Function imgUpload(ByVal fileUpload As FileUpload, ByVal strMapPath As String, ByVal userId As String) As String()
        '上传图片 参数一：fileupload控件 参数二：网站根目录物理路径Server.MapPath("~") 参数三：上传用户标识
        Dim strRt(0 To 1) As String
        '检查上传文件的格式是否有效 
        If fileUpload.PostedFile.ContentType.ToLower().IndexOf("image") < 0 Or fileUpload.PostedFile.ContentLength > 512000 Then
            strRt(0) = "error"
            strRt(1) = "error"
            Return strRt
        End If

        '生成原图 
        Dim oFileByte() As [Byte] = New Byte(fileUpload.PostedFile.ContentLength) {}
        Dim oStream As System.IO.Stream = fileUpload.PostedFile.InputStream
        Dim oImage As System.Drawing.Image = System.Drawing.Image.FromStream(oStream)
        Dim oWidth As Integer = oImage.Width '原图宽度 
        Dim oHeight As Integer = oImage.Height '原图高度 
        Dim tWidth As Integer = 200 '设置缩略图初始宽度 
        Dim tHeight As Integer = 200 '设置缩略图初始高度
        '按比例计算出缩略图的宽度和高度 
        If oWidth >= oHeight Then
            tHeight = CInt(Math.Floor((Convert.ToDouble(oHeight) * (Convert.ToDouble(tWidth) / Convert.ToDouble(oWidth)))))
        Else
            tWidth = CInt(Math.Floor((Convert.ToDouble(oWidth) * (Convert.ToDouble(tHeight) / Convert.ToDouble(oHeight)))))
        End If
        '生成缩略原图 
        Dim tImage As New Bitmap(tWidth, tHeight)
        Dim g As Graphics = Graphics.FromImage(tImage)
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High '设置高质量插值法 
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality '设置高质量,低速度呈现平滑程度 
        g.Clear(Color.Transparent) '清空画布并以透明背景色填充 
        g.DrawImage(oImage, New Rectangle(0, 0, tWidth, tHeight), New Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel)
        strMapPath += "upload\images\" & DateTime.Now.Year & "\" & DateTime.Now.Month & "\" & DateTime.Now.Day & "\"
        If (Directory.Exists(strMapPath) = False) Then
            Directory.CreateDirectory(strMapPath)
        End If
        If (userId.Length < 7) Then
            userId = userId * Math.Pow(10, 7 - userId.Length)
        End If
        userId = DateTime.Now.Millisecond.ToString + userId
        Dim myMain As New main, strUrl As String
        strUrl = myMain.siteUrl & "upload/images/" & DateTime.Now.Year & "/" & DateTime.Now.Month & "/" & DateTime.Now.Day & "/"
        Dim oFullName As String = strMapPath + "o" + DateTime.Now.ToFileTime.ToString + userId + ".jpg" '保存原图的物理路径 
        Dim tFullName As String = strMapPath + "t" + DateTime.Now.ToFileTime.ToString + userId + ".jpg" '保存缩略图的物理路径
        Try
            '以JPG格式保存图片 
            oImage.Save(oFullName, System.Drawing.Imaging.ImageFormat.Jpeg)
            tImage.Save(tFullName, System.Drawing.Imaging.ImageFormat.Jpeg)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            '释放资源 
            oImage.Dispose()
            g.Dispose()
            tImage.Dispose()
        End Try
        strRt(0) = strUrl + "o" + DateTime.Now.ToFileTime.ToString + userId + ".jpg" '保存原图的绝对地址
        strRt(1) = strUrl + "t" + DateTime.Now.ToFileTime.ToString + userId + ".jpg" '保存缩略图的绝对地址
        Return strRt
    End Function
    Function upLoadImg(ByVal imgFile As HttpPostedFile, ByVal user_email As String, ByVal serverPath As String) As Hashtable
        'serverPath = Server.MapPath("~")
        Dim myImg As New images, filePath As String, fileName As String, maxSize As Integer = 1000000, myHash As New Hashtable
        Dim userId As String = user_email
        myHash("status") = "error"
        If user_email Is Nothing Then
            myHash("message") = "你没有权限上传文件"
            Return myHash
        End If
        If (imgFile Is Nothing) Then
            myHash("message") = "请选择文件"
            Return myHash
        End If
        filePath = serverPath & "upload\userImages\" & user_email & "\" & DateTime.Now.Year & "\" & DateTime.Now.Month & "\"
        If Directory.Exists(filePath) = False Then
            Directory.CreateDirectory(filePath)
        End If
        fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) & Path.GetExtension(imgFile.FileName).ToLower()
        If (imgFile.InputStream Is Nothing Or imgFile.InputStream.Length > maxSize) Then
            myHash("message") = "文件大小超过限制"
            Return myHash
        End If
        If imgFile.ContentType.ToLower().IndexOf("image") < 0 Then
            myHash("message") = "不允许上传的文件类型"
            Return myHash
        End If
        '生成原图 
        Dim oFileByte() As [Byte] = New Byte(imgFile.ContentLength) {}
        Dim oStream As System.IO.Stream = imgFile.InputStream
        Dim oImage As System.Drawing.Image = System.Drawing.Image.FromStream(oStream)
        Dim oWidth As Integer = oImage.Width '原图宽度 
        Dim oHeight As Integer = oImage.Height '原图高度 
        Dim tWidth As Integer = 200 '设置缩略图初始宽度 
        Dim tHeight As Integer = 200 '设置缩略图初始高度
        '按比例计算出缩略图的宽度和高度 
        If oWidth >= oHeight Then
            tHeight = CInt(Math.Floor((Convert.ToDouble(oHeight) * (Convert.ToDouble(tWidth) / Convert.ToDouble(oWidth)))))
        Else
            tWidth = CInt(Math.Floor((Convert.ToDouble(oWidth) * (Convert.ToDouble(tHeight) / Convert.ToDouble(oHeight)))))
        End If
        '生成缩略原图 
        Dim tImage As New Bitmap(tWidth, tHeight)
        Dim g As Graphics = Graphics.FromImage(tImage)
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High '设置高质量插值法 
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality '设置高质量,低速度呈现平滑程度 
        g.Clear(Color.Transparent) '清空画布并以透明背景色填充 
        g.DrawImage(oImage, New Rectangle(0, 0, tWidth, tHeight), New Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel)
        serverPath += "upload\images\" & DateTime.Now.Year & "\" & DateTime.Now.Month & "\" & DateTime.Now.Day & "\"
        If (Directory.Exists(serverPath) = False) Then
            Directory.CreateDirectory(serverPath)
        End If
        If (userId.Length < 7) Then
            userId = userId * Math.Pow(10, 7 - userId.Length)
        End If
        userId = DateTime.Now.Millisecond.ToString + userId
        Dim myMain As New main, strUrl As String
        strUrl = myMain.siteUrl & "upload/images/" & DateTime.Now.Year & "/" & DateTime.Now.Month & "/" & DateTime.Now.Day & "/"
        Dim bigFileName As String = "o" + DateTime.Now.ToFileTime.ToString + userId + ".jpg"
        Dim smallFileName As String = "t" + DateTime.Now.ToFileTime.ToString + userId + ".jpg"
        Dim oFullName As String = serverPath + bigFileName  '保存原图的物理路径 
        Dim tFullName As String = serverPath + smallFileName '保存缩略图的物理路径
        Try
            '以JPG格式保存图片 
            oImage.Save(oFullName, System.Drawing.Imaging.ImageFormat.Jpeg)
            tImage.Save(tFullName, System.Drawing.Imaging.ImageFormat.Jpeg)
        Catch ex As Exception
            MsgBox(ex.Message)
            myHash("message") = "无法与服务器交换数据"
        Finally
            '释放资源 
            oImage.Dispose()
            g.Dispose()
            tImage.Dispose()
        End Try
        myHash("status") = "ok"
        myHash("bigPath") = strUrl + bigFileName  '保存原图的绝对地址
        myHash("smallPath") = strUrl + smallFileName  '保存缩略图的绝对地址
        Return myHash
    End Function
End Class
