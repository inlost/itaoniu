Imports System.Web.Configuration
Public Class main
    Private s_siteName As String '站点名称
    Private s_url As String '站点地址
    Private s_web_support_email As String '技术支持邮箱
    Private s_smtp As String 'smtp服务器地址
    Private s_smtp_user As String 'smtp用户
    Private s_smtp_pass As String 'smtp密码
    Private s_icp As String 'icp备案号
    Public Property siteName() As String
        Get
            s_siteName = getConfig("siteName")
            Return s_siteName
        End Get
        Set(ByVal value As String)
            If (setConfig("siteName", value)) Then
                s_siteName = value
            End If
        End Set
    End Property
    Public Property siteUrl As String
        Get
            s_url = getConfig("siteUrl")
            Return s_url
        End Get
        Set(ByVal value As String)
            If (setConfig("siteUrl", value)) Then
                s_url = value
            End If
        End Set
    End Property
    Public Property siteSupportEmail As String
        Get
            s_web_support_email = getConfig("siteEmail")
            Return s_web_support_email
        End Get
        Set(ByVal value As String)
            If (setConfig("siteEmail", value)) Then
                s_web_support_email = value
            End If
        End Set
    End Property
    Public Property siteSmtp As String
        Get
            s_smtp = getConfig("siteSmtp")
            Return s_smtp
        End Get
        Set(ByVal value As String)
            If (setConfig("siteSmtp", value)) Then
                s_smtp = value
            End If
        End Set
    End Property
    Public Property siteSmtp_user As String
        Get
            s_smtp_user = getConfig("siteSmtp_user")
            Return s_smtp_user
        End Get
        Set(ByVal value As String)
            If (setConfig("siteSmtp_user", value)) Then
                s_smtp_user = value
            End If
        End Set
    End Property
    Public Property siteSmtp_pass As String
        Get
            s_smtp_pass = getConfig("siteSmtp_pass")
            Return s_smtp_pass
        End Get
        Set(ByVal value As String)
            If (setConfig("siteSmtp_pass", value)) Then
                s_smtp_pass = value
            End If
        End Set
    End Property
    Public Property siteIcp As String
        Get
            s_icp = getConfig("siteIcp")
            Return s_icp
        End Get
        Set(ByVal value As String)
            If (setConfig("siteIcp", value)) Then
                s_icp = value
            End If
        End Set
    End Property
    Public Function GetRndString(ByVal lngNum As Long) As String
        '随机字符函数 参数为随机字符长度
        If lngNum <= 0 Then Return ""
        Dim i As Long
        Dim intLength As Integer
        Const STRINGSOURCE = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        intLength = Len(STRINGSOURCE) - 1
        Randomize()
        GetRndString = ""
        For i = 1 To lngNum
            GetRndString = GetRndString & Mid(STRINGSOURCE, Int(Rnd() * intLength + 1), 1)
        Next
    End Function
    Private Function getConfig(ByVal key As String) As String
        Dim config = WebConfigurationManager.OpenWebConfiguration("~")
        Dim configSection As AppSettingsSection
        Try
            Return config.AppSettings.Settings(key).Value
        Catch ex As Exception
            configSection = config.AppSettings
            configSection.Settings.Add(key, "")
            config.Save(ConfigurationSaveMode.Modified)
            Return Nothing
        End Try
    End Function
    Private Function setConfig(ByVal key As String, ByVal value As String) As Boolean
        If getConfig(key) <> value Then
            Try
                Dim config = WebConfigurationManager.OpenWebConfiguration("~")
                Dim configSection As AppSettingsSection
                configSection = config.AppSettings
                configSection.Settings(key).Value = value
                config.Save(ConfigurationSaveMode.Modified)
                Return True
            Catch ex As Exception
                Return False
            End Try
        Else
            Return True
        End If
    End Function
    Public Function filterCheck(ByVal strIn As String) As String
        '提交字符串过滤
        Return Trim(strIn)
    End Function
    Public Function hashtableToJson(ByVal myHashtable As Hashtable) As String
        Dim strRt As String = "{", intTp As Integer = 1
        For Each haItem As DictionaryEntry In myHashtable
            If intTp = 1 Then
                If haItem.Key <> "error" Then
                    strRt += Chr(34) & haItem.Key & Chr(34) & ":" & Chr(34) & haItem.Value & Chr(34)
                Else
                    strRt += Chr(34) & haItem.Key & Chr(34) & ":" & haItem.Value
                End If
            Else
                If haItem.Key <> "error" Then
                    strRt += "," & Chr(34) & haItem.Key & Chr(34) & ":" & Chr(34) & haItem.Value & Chr(34)
                Else
                    strRt += "," & Chr(34) & haItem.Key & Chr(34) & ":" & haItem.Value
                End If
            End If
            intTp += 1
        Next
        strRt += "}"
        Return strRt
    End Function
    Public Function checkIsNotNull(ByVal strIn As String) As Boolean
        '非空检查
        If Trim(strIn) = "" Or Trim(strIn) = Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function checkListToString(ByVal checkList As CheckBoxList, ByVal spit As String) As String
        If checkList.Items.Count <> 0 Then
            Dim strRt As String = ""
            For Each ListItem As ListItem In checkList.Items
                If ListItem.Selected = True Then
                    If Len(strRt) = 0 Then
                        strRt += ListItem.Value
                    Else
                        strRt += spit & ListItem.Value
                    End If
                End If
            Next
            Return strRt
        Else
            Return Nothing
        End If
    End Function
    Public Function checkInputMain(ByVal strIn As String, ByVal checkType As String) As Boolean
        Select Case checkType
            Case "int"
                If checkIsNotNull(strIn) Then
                    Try
                        Convert.ToInt32(strIn)
                        Return True
                    Catch ex As Exception
                        Return False
                    End Try
                Else
                    Return False
                End If
            Case "longInt"
                If checkIsNotNull(strIn) Then
                    Try
                        Convert.ToInt64(strIn)
                        Return True
                    Catch ex As Exception
                        Return False
                    End Try
                Else
                    Return False
                End If
            Case "str"
                If checkIsNotNull(strIn) Then
                    Return True
                Else
                    Return False
                End If
            Case Else
                Return False
        End Select
    End Function
    Public Function dateSetToString(ByVal inSet As DataSet, ByVal tbName As String, ByVal connectStr As String) As String
        Dim strRt As String = ""
        For i = 0 To inSet.Tables(tbName).Columns.Count - 1
            If i = 0 Then
                strRt += inSet.Tables(tbName).Columns(i).ColumnName & "=" & inSet.Tables(tbName).Rows(0)(i)
            Else
                strRt += connectStr & inSet.Tables(tbName).Columns(i).ColumnName & "=" & inSet.Tables(tbName).Rows(0)(i)
            End If
        Next
        Return strRt
    End Function
    Function stringToInQuery(ByVal arrayIn As String, ByVal connectStr As String) As dbHelper.inQuery()
        Dim iCount As Integer = 0
        Dim arrayTmp() As String = arrayIn.Split(connectStr)
        Dim inQuery(0 To arrayTmp.Length) As dbHelper.inQuery
        For Each item As String In arrayTmp
            Dim arrayTemp() As String = arrayTmp(iCount).Split("=")
            inQuery(iCount).name = arrayTemp(0)
            inQuery(iCount).value = arrayTemp(1)
            iCount += 1
        Next
        Return inQuery
    End Function
    Public Function userCenterSetOutInfo(ByVal key As String, ByVal value As String, ByVal strIn As String, Optional ByVal halfWidth As Boolean = True) As String
        Dim strPClass As String = IIf(halfWidth, "class='halfWidth'", "class='fullWidth'")
        strIn += "<p " & strPClass & "><span class='infoTitle'>" & key & "</span><span class='info'>" & value & "</span></p>"
        Return strIn
    End Function
    Public Function announcement_get() As Hashtable()
        Dim myHelper As New dbHelper, strSql As String, myHash() As Hashtable
        strSql = "select top 20 * from announcement order by id desc"
        Dim myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "ans")
        If myDs.Tables("ans").Rows.Count < 1 Then
            ReDim myHash(0 To 0)
        Else
            ReDim myHash(0 To myDs.Tables("ans").Rows.Count - 1)
        End If
        myHash(0) = New Hashtable
        myHash(0)("count") = myDs.Tables("ans").Rows.Count
        For i = 0 To myDs.Tables("ans").Rows.Count - 1
            If myHash(0)("count") = 0 Then Exit For
            If i > 0 Then myHash(i) = New Hashtable
            myHash(i)("id") = myDs.Tables("ans").Rows(i)(0)
            myHash(i)("title") = myDs.Tables("ans").Rows(i)(1)
            myHash(i)("announcement") = myDs.Tables("ans").Rows(i)(2)
            myHash(i)("time") = myDs.Tables("ans").Rows(i)(3)
        Next
        Return myHash
    End Function
    Public Function announcement_get(ByVal iCount As Integer) As Hashtable
        Dim myHelper As New dbHelper, strSql As String, myHash As New Hashtable
        strSql = "select * from announcement where id=" & iCount & " order by id desc"
        Dim myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "ans")
        myHash("id") = myDs.Tables("ans").Rows(0)(0)
        myHash("title") = myDs.Tables("ans").Rows(0)(1)
        myHash("announcement") = myDs.Tables("ans").Rows(0)(2)
        myHash("time") = myDs.Tables("ans").Rows(0)(3)
        Return myHash
    End Function
End Class
