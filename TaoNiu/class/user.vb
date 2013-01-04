Imports System.Net

Public Class user
    Public Structure userBasic
        Dim email As String
        Dim password As String
        Dim niceName As String
        Dim power As String
        Dim status As String
        Dim ip As String
    End Structure
    Public Function user_add(ByVal userNew As userBasic) As Boolean
        Dim myHelper As New dbHelper, myMain As New main, strCode As String
        strCode = myMain.GetRndString(15)
        Dim inQuery(0 To 5) As dbHelper.inQuery
        inQuery(0).name = "@email"
        inQuery(0).value = userNew.email
        inQuery(1).name = "@pass"
        inQuery(1).value = userNew.password
        inQuery(2).name = "@niceName"
        inQuery(2).value = userNew.niceName
        inQuery(3).name = "@code"
        inQuery(3).value = strCode
        inQuery(4).name = "@regDate"
        inQuery(4).value = Date.Now.Date
        inQuery(5).name = "@regIp"
        inQuery(5).value = userNew.ip
        Try
            Dim myDs As DataSet = myHelper.getQuery("user_add", inQuery, "uid")
            If (myDs.Tables("uid").Rows(0)(0) >= 0) Then
                Dim myMail As New mail, mailBody As String = ""
                mailBody = myMail.buidMailBody(mailBody, "桃桃牛用户注册验证", "h2")
                mailBody = myMail.buidMailBody(mailBody, "亲爱的" & userNew.niceName & ",您在桃桃牛注册的账户还差一步就激活啦！")
                mailBody = myMail.buidMailBody(mailBody, "如果不是您本人操作，请直接删除此邮件，不要点击下面的连接")
                mailBody = myMail.buidMailBody(mailBody, "如果是您本人操作，请点击")
                mailBody = myMail.buidMailBody(mailBody, "这里完成激活", "a", myMain.siteUrl & "server_page/user_service.aspx?action=active&uid=" & myDs.Tables("uid").Rows(0)(0) & "&code=" & strCode)
                mailBody = myMail.buidMailBody(mailBody, "如果邮箱拦截了上面的连接，请尝试复制下面的地址到浏览器地址栏完成激活：")
                mailBody = myMail.buidMailBody(mailBody, myMain.siteUrl & "server_page/user_service.aspx?action=active&uid=" & myDs.Tables("uid").Rows(0)(0) & "&code=" & strCode)
                mailBody = myMail.buidMailBody(mailBody, "<hr/>")
                mailBody = myMail.buidMailBody(mailBody, "tip:该邮件由系统自动发送，请勿直接回复。")
                myMail.sendMail(userNew.email, "桃桃牛用户激活", mailBody)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function user_active(ByVal uid As String, ByVal code As String) As Boolean
        Dim inQuery(0) As dbHelper.inQuery, mySet As New DataSet, myHelper As New dbHelper
        inQuery(0).name = "@userId"
        inQuery(0).value = uid
        Dim coder As String
        mySet = myHelper.getQuery("user_getActCode", inQuery, "code")
        Try
            coder = mySet.Tables("code").Rows(0)(0).ToString
        Catch ex As Exception
            Return False
        End Try
        If code = coder Then
            Dim strSql As String
            strSql = "update users set status=1 where id=" & uid
            myHelper.querySql(strSql)
            strSql = "delete from vd_code where userId=" & uid & " and codeType='reg'"
            myHelper.querySql(strSql)
            Return True
        Else
            Return False
        End If
    End Function
    Public Function user_login(ByVal uid As String, ByVal ipAddress As String, ByVal pass As String) As String
        Dim inQuery(0 To 2) As dbHelper.inQuery
        inQuery(0).name = "@userId"
        inQuery(0).value = uid
        inQuery(1).name = "@userPass"
        inQuery(1).value = pass
        inQuery(2).name = "@ipAddress"
        inQuery(2).value = ipAddress
        Dim myHelper As New dbHelper
        Try
            Dim mySet As DataSet = myHelper.getQuery("user_login", inQuery, "userBasic")
            Dim myMain As New main, strRt As String
            strRt = myMain.dateSetToString(mySet, "userBasic", "|")
            Return strRt
        Catch ex As Exception
            Return "error"
        End Try
    End Function
    Public Function user_login(ByVal uid As String, ByVal ipAddress As String) As String
        Dim inQuery(0 To 1) As dbHelper.inQuery
        inQuery(0).name = "@userId"
        inQuery(0).value = uid
        inQuery(1).name = "@ipAddress"
        inQuery(1).value = ipAddress
        Dim myHelper As New dbHelper
        Try
            Dim mySet As DataSet = myHelper.getQuery("user_login_byId", inQuery, "userBasic")
            Dim myMain As New main, strRt As String
            strRt = myMain.dateSetToString(mySet, "userBasic", "|")
            Return strRt
        Catch ex As Exception
            Return "error"
        End Try
    End Function
    Public Function user_login(ByVal niceName As String, ByVal pass As String, ByVal ipAddress As String, ByVal oter As Boolean) As String
        Dim inQuery(0 To 2) As dbHelper.inQuery
        inQuery(0).name = "@niceName"
        inQuery(0).value = niceName
        inQuery(1).name = "@pass"
        inQuery(1).value = pass
        inQuery(2).name = "@ipAddress"
        inQuery(2).value = ipAddress
        Dim myHelper As New dbHelper
        Try
            Dim mySet As DataSet = myHelper.getQuery("user_login_byIdPass", inQuery, "userBasic")
            Dim myMain As New main, strRt As String
            strRt = myMain.dateSetToString(mySet, "userBasic", "|")
            Return strRt
        Catch ex As Exception
            Return "error"
        End Try
    End Function
    Public Function getUserInfo(ByVal uid As Integer) As Hashtable
        Dim myDs As DataSet, myHelper As New dbHelper, strSql As String = "select * from users_info where uid=" & uid
        myDs = myHelper.getQuerySql(strSql, "userInfo")
        Dim myInfo As New Hashtable
        Try
            For i = 0 To myDs.Tables("userInfo").Columns.Count - 1
                myInfo(myDs.Tables("userInfo").Columns.Item(i).ColumnName) = myDs.Tables("userInfo").Rows(0)(i).ToString
            Next
            strSql = "select email,niceName from users where id=" & uid
            myDs = myHelper.getQuerySql(strSql, "email")
            myInfo("email") = myDs.Tables("email").Rows(0)(0)
            myInfo("niceName") = myDs.Tables("email").Rows(0)(1)
            Return myInfo
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function getUserInfoByName(ByVal userName As String) As Hashtable
        Return getUserInfo(getIdByName(userName))
    End Function
    Private Function getIdByName(ByVal name As String) As Integer
        Dim myHelper As New dbHelper, strSql As String = "select id from users where niceName='" & name & "'"
        Try
            Return myHelper.getQuerySql(strSql, "id").Tables("id").Rows(0)(0)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function user_info_modify(ByVal userInfo As Hashtable) As Boolean
        Dim inQuery() As dbHelper.inQuery, myHelper As New dbHelper
        inQuery = myHelper.hashtableToInquery(userInfo)
        Try
            myHelper.noneQuery("user_info_modify", inQuery)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function user_pass_change(ByVal userPass As Hashtable) As Boolean
        Dim myHelper As New dbHelper, inQuery() As dbHelper.inQuery
        inQuery = myHelper.hashtableToInquery(userPass)
        Try
            myHelper.noneQuery("user_pass_change", inQuery)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Public Function user_vd_add(ByVal uid As Integer, ByVal imgPath As String, ByVal ip As String) As Boolean
        Dim myVd As New Hashtable, myHelper As New dbHelper
        myVd("@uid") = uid
        myVd("@id_img") = imgPath
        myVd("@ipAddress") = ip
        Dim inQuery() As dbHelper.inQuery = myHelper.hashtableToInquery(myVd)
        Try
            myHelper.noneQuery("user_id_vd_add", inQuery)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function user_check_mail(ByVal email As String) As Boolean
        Dim myHelper As New dbHelper, myDs As New DataSet, inQuery(0 To 0) As dbHelper.inQuery
        inQuery(0).name = "@email"
        inQuery(0).value = email
        myDs = myHelper.getQuery("user_check_mail", inQuery, "rst")
        If myDs.Tables("rst").Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function user_check_name(ByVal name As String) As Boolean
        Dim myHelper As New dbHelper, myDs As New DataSet, inQuery(0 To 0) As dbHelper.inQuery
        inQuery(0).name = "@niceName"
        inQuery(0).value = name
        myDs = myHelper.getQuery("user_check_name", inQuery, "rst")
        If myDs.Tables("rst").Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function getBbsIcon(ByVal name As String) As String
        '获取用户论坛头像
        Dim ds As Discuz.Toolkit.DiscuzSession
        ds = DiscuzSessionHelper.GetSession
        Dim iconUrl As String = ds.GetUserInfo(ds.GetUserID(name)).Avatar
        Try
            Dim rq As WebRequest, rp As WebResponse
            rq = WebRequest.Create(iconUrl)
            rp = rq.GetResponse
            rp.Close()
        Catch ex As Exception
            iconUrl = Left(iconUrl, iconUrl.IndexOf("avatars")) & "images/common/noavatar_medium.gif"
        End Try
        Return iconUrl
    End Function
    Public Function getUserList(ByVal number As String) As Hashtable()
        Dim strSql As String = "select top " & number & " * from users"
        Dim myHeper As New dbHelper, myType As New typeHelper, myDs As New DataSet
        myDs = myHeper.getQuerySql(strSql, "rt")
        Return myType.dsToHash(myDs, "rt")
    End Function
End Class
