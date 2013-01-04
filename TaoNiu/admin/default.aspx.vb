Imports System.IO
Imports System
Imports Microsoft.VisualBasic.CompilerServices
Public Class _default1
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Sw As StreamWriter
        Dim Bc As HttpBrowserCapabilities
        Dim Flag As Boolean
        Dim DownStr As String

        ServerName.Text = Server.MachineName.ToString()
        ServerVer.Text = Environment.OSVersion.ToString()
        ServerIP.Text = Request.ServerVariables("LOCAL_ADDR")
        ServerDomain.Text = Request.ServerVariables("SERVER_NAME")
        ServerOutTime.Text = Server.ScriptTimeout.ToString()
        ServerNow.Text = DateTime.Now.ToString()
        ServerSessionTotal.Text = Session.Contents.Count.ToString()
        ServerApplicationTotal.Text = Application.Contents.Count.ToString()

        NetVer.Text = System.Environment.Version.ToString()
        IISVer.Text = Request.ServerVariables("SERVER_SOFTWARE")

        ProPath.Text = Request.ServerVariables("PATH_INFO")
        ProPath_2.Text = Request.ServerVariables("APPL_PHYSICAL_PATH")

        ServerRunTime.Text = Math.Round(Environment.TickCount / 600 / 60) / 100


        Bc = Request.Browser
        Brower_IP.Text = Request.ServerVariables("REMOTE_ADDR")
        Brower_OSVer.Text = Bc.Platform.ToString()
        Brower_Brower.Text = Bc.Browser.ToString()
        Brower_BrowerVer.Text = Bc.Version.ToString()
        Brower_Javscript.Text = Bc.JavaScript.ToString()
        Brower_VBScript.Text = Bc.VBScript.ToString()
        Brower_JavaApplets.Text = Bc.JavaApplets.ToString()
        Brower_Cookies.Text = Bc.Cookies.ToString()
        Brower_Language.Text = Request.ServerVariables("HTTP_ACCEPT_LANGUAGE")
        Brower_Frame.Text = Bc.Frames.ToString()

        DownStr = "&#12288;&#91;<a href='&#104;&#116;&#116;&#112;&#58;&#47;&#47;&#119;&#119;&#119;&#46;&#113;&#113;&#99;&#102;&#46;&#99;&#111;&#109;&#47;&#63;&#97;&#99;&#116;&#105;&#111;&#110;&#61;&#100;&#111;&#119;&#110;' target=_blank style='&#102;&#111;&#110;&#116;&#45;&#115;&#105;&#122;&#101;&#58;&#49;&#50;&#112;&#120;&#59;&#99;&#111;&#108;&#111;&#114;&#58;&#35;&#102;&#102;&#48;&#48;&#48;&#48;&#59;&#116;&#101;&#120;&#116;&#45;&#100;&#101;&#99;&#111;&#114;&#97;&#116;&#105;&#111;&#110;&#58;&#117;&#110;&#100;&#101;&#114;&#108;&#105;&#110;&#101;'>&#19979;&#36733;</a>&#93;"
        DownStr = ""

        If ObjCheck("ADODB.RecordSet") Then
            Obj_Access.Text = "支持" & ObjVer("ADODB.RecordSet")
        Else
            Obj_Access.Text = "不支持"
        End If

        If ObjCheck("Scripting.FileSystemObject") Then
            Obj_Fso.Text = "支持"
        Else
            Obj_Fso.Text = "不支持"
        End If


        If ObjCheck("JMail.SmtpMail") Then
            Obj_Jmail.Text = "支持，版本：" & ObjVer("JMail.SmtpMail") & DownStr
        Else
            Obj_Jmail.Text = "不支持" & DownStr
        End If

        If ObjCheck("CDONTS.NewMail") Then
            Obj_Cdonts.Text = "支持，版本：" & ObjVer("CDONTS.NewMail")
        Else
            Obj_Cdonts.Text = "不支持"
        End If

        If ObjCheck("Persits.Jpeg") Then
            Obj_AspJpeg.Text = "支持，版本：" & ObjVer("Persits.Jpeg") & DownStr
        Else
            Obj_AspJpeg.Text = "不支持" & DownStr
        End If

        If ObjCheck("Persits.Upload.1") Then
            obj_aspupload.Text = "支持，版本：" & ObjVer("Persits.Upload.1") & DownStr
        Else
            obj_aspupload.Text = "不支持" & DownStr
        End If


        If ObjCheck("ADODB.RecordSet") Then
            Obj_Access.Text = "支持"
        Else
            Obj_Access.Text = "不支持"
        End If


        Try
            Sw = New StreamWriter(Server.MapPath("AspxCheck_Temp.htm"), False, System.Text.Encoding.GetEncoding("GB2312"))
            Sw.WriteLine(Now())
            Sw.Close()

            Flag = True
        Catch ex As Exception
            Flag = False
        End Try

        If Flag = True Then
            obj_write.Text = "<b>支持</b>"
        Else
            obj_write.Text = "<font color='ff0000'><b>不支持</b></font>"
        End If
        systitle.Text = "服务器基本信息"
    End Sub

    Public Sub SelfObjChk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ObjName As String
        ObjName = Trim(SelObj.Value)

        If ObjCheck(ObjName) Then
            Obj_SelfObj.Text = "支持，版本：" & ObjVer(ObjName)
        Else
            Obj_SelfObj.Text = "不支持"
        End If

    End Sub


    Public Function ObjCheck(ByVal a As String) As Boolean
        Dim b As Boolean
        Try
            Dim c = Server.CreateObject(a)
            b = True
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            b = False
            ProjectData.ClearProjectError()
        End Try
        Return b
    End Function

    Public Function ObjVer(ByVal a As String) As String
        Dim b As String
        Try
            Dim c = Server.CreateObject(a)
            b = c.version
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            ProjectData.ClearProjectError()
        End Try
        Return b
    End Function

End Class