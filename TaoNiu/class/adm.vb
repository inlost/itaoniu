Public Class adm
    Public Function admin(ByVal uid As String, ByVal pass As String) As Boolean
        Dim myHelper As New dbHelper, inQuery(0 To 1) As dbHelper.inQuery, rst As Integer
        inQuery(0).name = "@uid"
        inQuery(0).value = uid
        inQuery(1).name = "@pass"
        inQuery(1).value = pass
        rst = myHelper.noneQuery("adm_login", inQuery)
        If (rst > 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function errorLogAdd(ByVal uid As String, ByVal ip As String) As Boolean
        Dim inQuery(0 To 3) As dbHelper.inQuery, myHelper As New dbHelper
        inQuery(0).name = "@error_date"
        inQuery(0).value = Date.Now.Date
        inQuery(1).name = "@error_time"
        inQuery(1).value = Date.Now.TimeOfDay.ToString
        inQuery(2).name = "@error_type"
        inQuery(2).value = "管理员登录认证失败"
        inQuery(3).name = "@ip"
        inQuery(3).value = ip
        Try
            myHelper.insertQuery("errorLog_add", inQuery)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function loginLogAdd(ByVal uid As String, ByVal ip As String) As Boolean
        Dim inQuery(0 To 3) As dbHelper.inQuery, myHelper As New dbHelper
        inQuery(0).name = "@uid"
        inQuery(0).value = uid
        inQuery(1).name = "@logDate"
        inQuery(1).value = Date.Now.Date
        inQuery(2).name = "@note"
        inQuery(2).value = "成功登录管理后台系统"
        inQuery(3).name = "@ip"
        inQuery(3).value = ip
        Try
            myHelper.insertQuery("user_log_insert", inQuery)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function dell_hd(ByVal id As Integer) As Boolean
        Dim strSql As String, myHelper As New dbHelper
        strSql = "delete from activities where id=" & id
        Try
            Return myHelper.querySql(strSql)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function add_hd(ByVal serverPath As String, ByVal title As String, ByVal postFile As HttpPostedFile, ByVal link As String, Optional ByVal cat As Integer = 0) As Hashtable
        Dim rst As New Hashtable, myImg As New images
        rst = myImg.upLoadImg(postFile, "admin@tn.com", serverPath)
        If rst("status") = "error" Then
            Return rst
        Else
            Dim newHd As New Hashtable
            newHd("@cat") = cat
            newHd("@title") = title
            newHd("@imgSrc") = rst("bigPath")
            newHd("@link") = link
            Dim myHelper As New dbHelper
            Try
                Dim inQuery() As dbHelper.inQuery = myHelper.hashtableToInquery(newHd)
                myHelper.noneQuery("activities_add", inQuery)
                rst("status") = "ok"
                Return rst
            Catch ex As Exception
                rst("status") = "error"
                rst("message") = "无法与服务器交换数据"
                Return rst
            End Try
        End If
    End Function
    Public Function hd_get(ByVal getTitle As Boolean, Optional ByVal cat As Integer = 0) As dbHelper.inQuery()
        Dim rt() As dbHelper.inQuery, myHelper As New dbHelper, strSql As String, myDs As DataSet
        If getTitle Then
            If cat = 0 Then
                strSql = "select top 4 title,link from activities where cat=0"
            Else
                strSql = "select top 4 title,link from activities where cat=" & cat
            End If
        Else
            If cat = 0 Then
                strSql = "select top 4 imgSrc,link from activities where cat=0"
            Else
                strSql = "select top 4 imgSrc,link from activities where cat=" & cat
            End If
        End If
        myDs = myHelper.getQuerySql(strSql, "hd")
        ReDim rt(0 To myDs.Tables("hd").Rows.Count - 1)
        For i = 0 To myDs.Tables("hd").Rows.Count - 1
            rt(i).name = myDs.Tables("hd").Rows(i)(0)
            rt(i).value = myDs.Tables("hd").Rows(i)(1)
        Next
        Return rt
    End Function
End Class
