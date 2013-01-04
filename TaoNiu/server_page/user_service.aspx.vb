Imports System.IO
Imports System.Globalization

Public Class user_service
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Request.Params("action")
            Case "reg"
                If Session("onTheWay") <> Nothing And Session("onTheWay") <> "" Then
                    If Request.Form("R_vdCode").ToUpper <> Session("CheckCode").ToString.ToUpper Then
                        Response.Redirect("~/reg.aspx?msg=vd_code_error")
                    End If
                    Dim userNew As user.userBasic
                    userNew.email = Request.Form("R_email")
                    userNew.niceName = Request.Form("R_id")
                    userNew.password = Request.Form("R_password")
                    userNew.power = 1
                    userNew.status = 2
                    userNew.ip = getUserIp()
                    If (addUser(userNew)) Then
                        bbsReg(userNew.niceName, userNew.email, userNew.password)
                        Response.Redirect("~/reg.aspx?type=regsuccess&step=2")
                    Else
                        Response.Redirect("~/reg.aspx?type=reg_fail")
                    End If
                Else
                    Response.Redirect("~/reg.aspx")
                End If
            Case "active"
                If (Request.Params("uid") = Nothing Or Request.Params("code") = Nothing) Then
                    Response.Redirect("~/reg.aspx?type=active_fail&step=2")
                Else
                    Dim myUser As New user
                    If (myUser.user_active(Request.Params("uid"), Request.Params("code"))) Then
                        Dim strRt As String = myUser.user_login(Request.Params("uid"), getUserIp)
                        Dim myMain As New main, inQuery() As dbHelper.inQuery
                        inQuery = myMain.stringToInQuery(strRt, "|")
                        For Each qItem As dbHelper.inQuery In inQuery
                            If Len(qItem.name) <> 0 And qItem.name <> "pass" Then
                                Session(qItem.name) = qItem.value
                            End If
                        Next
                        Session("uid") = Session("email")
                        Session("onTheWay") = "loginSuccess"
                        Response.Redirect("~/reg.aspx?type=active_success&step=3")
                    Else
                        Response.Redirect("~/reg.aspx?type=active_fail&step=2")
                    End If
                End If
            Case "login"
                If Session("onTheWay") <> Nothing And Session("onTheWay") = "login" Then
                    Dim uid As String = Request.Form("ctl00$ContentContent$uid")
                    Dim pass As String = Request.Form("ctl00$ContentContent$password")
                    Dim myUser As New user, strRt As String
                    If uid.IndexOf("@") > 0 Then
                        strRt = myUser.user_login(uid, getUserIp, pass)
                    Else
                        strRt = myUser.user_login(uid, pass, getUserIp, True)
                    End If
                    If strRt <> "error" Then
                        Dim myMain As New main, inQuery() As dbHelper.inQuery
                        inQuery = myMain.stringToInQuery(strRt, "|")
                        For Each qItem As dbHelper.inQuery In inQuery
                            If Len(qItem.name) <> 0 And qItem.name <> "pass" Then
                                Session(qItem.name) = qItem.value
                            End If
                        Next
                        Session("uid") = Session("email")
                        Session("onTheWay") = "loginSuccess"
                        bbs_login() '论坛登录
                        If Request.Form("redirectURL") <> Nothing Then
                            Response.Redirect("../" & Request.Params("redirectURL").Replace("----", "&"))
                        Else
                            Response.Redirect("~/user_center.aspx")
                        End If
                    Else
                        If Request.Form("redirectURL") <> Nothing Then
                            Response.Redirect("~/login.aspx?redirectURL=" & Request.Params("redirectURL"))
                        Else
                            Response.Redirect("~/login.aspx")
                        End If
                    End If
                Else
                    Response.Redirect("~/login.aspx")
                End If
            Case "bbsLogin"
                bbs_login()
            Case "modifyInfo"
                If Session("onTheWay") <> Nothing And Session("onTheWay") = "loginSuccess" Then
                    Dim userInfo As New Hashtable, myMain As New main, strTp As String
                    strTp = myMain.filterCheck(Session("id"))
                    userInfo("@uid") = strTp
                    strTp = myMain.filterCheck(Request.Form("real_name"))
                    If myMain.checkInputMain(strTp, "str") Then
                        userInfo("@real_name") = strTp
                    Else
                        Response.Redirect("~/user_center.aspx?action=modify&msg=r_name")
                    End If
                    strTp = myMain.filterCheck(Request.Form("address"))
                    If myMain.checkInputMain(strTp, "str") Then
                        userInfo("@address") = strTp
                    Else
                        Response.Redirect("~/user_center.aspx?action=modify&msg=address")
                    End If
                    strTp = myMain.filterCheck(Request.Form("mobile"))
                    If myMain.checkInputMain(strTp, "longInt") And Len(strTp) = 11 Then
                        userInfo("@mobile") = strTp
                    Else
                        Response.Redirect("~/user_center.aspx?action=modify&msg=mobile")
                    End If
                    strTp = myMain.filterCheck(Request.Form("id_code"))
                    If myMain.checkInputMain(strTp, "longInt") And Len(strTp) = 18 Then
                        userInfo("@id_code") = strTp
                    Else
                        Response.Redirect("~/user_center.aspx?action=modify&msg=id")
                    End If
                    strTp = myMain.filterCheck(Request.Form("qq"))
                    If myMain.checkInputMain(strTp, "int") Or myMain.checkInputMain(strTp, "longInt") Then
                        userInfo("@qq") = strTp
                    Else
                        Response.Redirect("~/user_center.aspx?action=modify&msg=qq")
                    End If
                    userInfo("@ipAddress") = getUserIp()
                    Dim myUser As New user
                    If myUser.user_info_modify(userInfo) Then
                        Response.Redirect("~/user_center.aspx")
                    Else
                        Response.Redirect("~/user_center.aspx?action=modify&msg=infoChangeError")
                    End If
                Else
                    Response.Redirect("~/login.aspx")
                End If
            Case "modifyPass"
                If Session("onTheWay") <> Nothing And Session("onTheWay") = "loginSuccess" Then
                    Dim userPass As New Hashtable
                    If Request.Form("new_pass") <> Request.Form("renew_pass") Then
                        Response.Redirect("~/user_center.aspx?action=modify&msg=passNotEq")
                    Else
                        userPass("@uid") = Session("id")
                        Dim strTp As String, myMain As New main
                        strTp = myMain.filterCheck(Request.Form("new_pass"))
                        If myMain.checkInputMain(strTp, "str") Then
                            userPass("@newPass") = strTp
                            userPass("@oldPass") = Request.Form("old_pass")
                            userPass("@ipAddress") = getUserIp()
                            Dim myUser As New user
                            If myUser.user_pass_change(userPass) Then
                                Response.Redirect("~/user_center.aspx")
                            Else
                                Response.Redirect("~/user_center.aspx?action=modify&msg=passChangeError")
                            End If
                        Else
                            Response.Redirect("~/user_center.aspx?action=modify&msg=passIsNull")
                        End If
                    End If
                Else
                    Response.Redirect("~/login.aspx")
                End If
            Case "checkReal"
                Dim strTp As String = upLoadImg()
                If Len(strTp) > 11 Then
                    Dim myUser As New user
                    If myUser.user_vd_add(Session("id"), strTp, getUserIp) Then
                        Response.Redirect("~/user_center.aspx?action=modify&msg=updateVdSuccess")
                    Else
                        Response.Redirect("~/user_center.aspx?action=modify&msg=updateVdError")
                    End If
                Else
                    Response.Redirect("~/user_center.aspx?action=modify&msg=upFall&wd=" & strTp)
                End If
            Case "quit"
                Session.Clear()
                Dim ds As Discuz.Toolkit.DiscuzSession
                ds = DiscuzSessionHelper.GetSession
                ds.Logout("")
                ds.session_info = Nothing
                Response.Redirect("~/default.aspx")
            Case "checkMail"
                Dim myUser As New user
                If myUser.user_check_mail(Request.Params("mail")) Then
                    Response.Write("ok")
                Else
                    Response.Write("error")
                End If
            Case "checkName"
                Dim myUser As New user
                If myUser.user_check_name(Request.Params("name")) Then
                    Response.Write("ok")
                Else
                    Response.Write("error")
                End If
            Case Else
                Response.Write("Good Luck~")
        End Select
    End Sub
    Protected Function addUser(ByVal userNew As user.userBasic) As Boolean
        Dim myUser As New user
        Return myUser.user_add(userNew)
    End Function
    Function getUserIp() As String
        Dim userIP As String
        If (Request.ServerVariables("HTTP_VIA") = Nothing) Then
            userIP = Request.UserHostAddress
        Else
            userIP = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        End If
        Return userIP
    End Function
    Function upLoadImg() As String
        Dim myImg As New images, filePath As String, fileName As String, maxSize As Integer = 1000000
        Dim imgFile As HttpPostedFile
        imgFile = Request.Files("imgFile")
        If Session("uid") Is Nothing Then
            Return "你没有权限上传文件"
        End If
        If (imgFile Is Nothing) Then
            Return "请选择文件"
        End If
        filePath = Server.MapPath("~") & "upload\userImages\" & Session("uid") & "\" & DateTime.Now.Year & "\" & DateTime.Now.Month & "\"
        If Directory.Exists(filePath) = False Then
            Directory.CreateDirectory(filePath)
        End If
        fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) & Path.GetExtension(imgFile.FileName).ToLower()
        If (imgFile.InputStream Is Nothing Or imgFile.InputStream.Length > maxSize) Then
            Return "文件大小超过限制"
        End If
        If imgFile.ContentType.ToLower().IndexOf("image") < 0 Then
            Return "不允许上传的文件类型"
        End If
        imgFile.SaveAs(filePath & fileName)
        Dim myMain As New main
        Return myMain.siteUrl & "upload/userImages/" & Session("uid") & "/" & DateTime.Now.Year & "/" & DateTime.Now.Month & "/" & fileName
    End Function
    Public Sub bbs_login()
        Dim dnSession As Discuz.Toolkit.DiscuzSession
        Session("redirectURL") = IIf(Request.Form("redirectURL") = Nothing, "default.aspx", Request.Form("redirectURL"))
        If Request.Cookies("dnt") Is Nothing Then
            dnSession = DiscuzSessionHelper.GetSession()
            dnSession.Login(dnSession.GetUserID(Session("niceName")), Request.Form("ctl00$ContentContent$password"), False, 100, "")
            Response.Redirect("~/server_page/bbs_callBack.aspx?next=user_service.aspx")
        Else
            dnSession = DiscuzSessionHelper.GetSession()
            Try
                dnSession.session_info = dnSession.GetSessionFromToken(Session("AuthToken").ToString())
            Catch
                Response.Redirect("~/server_page/bbs_callBack.aspx?next=user_service.aspx")
            End Try
            If Session("redirectURL") <> Nothing Then
                Dim redirectURL As String = Session("redirectURL")
                Session("redirectURL") = Nothing
                Response.Redirect("../" & redirectURL.Replace("----", "&"))
            Else
                Response.Redirect("~/user_center.aspx")
            End If
        End If
    End Sub
    Public Sub bbsReg(ByVal uName As String, ByVal uEmail As String, ByVal uPass As String)
        Dim ds As Discuz.Toolkit.DiscuzSession
        ds = DiscuzSessionHelper.GetSession
        ds.Register(uName, uPass, uEmail, False)
    End Sub
End Class