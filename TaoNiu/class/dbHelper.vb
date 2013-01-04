Public Class dbHelper
    Private conStr As String
    Structure inQuery
        '存储过程键值数据结构
        Dim name As String
        Dim value As String
    End Structure
    Sub New()
        '解析函数
        conStr = ConfigurationManager.ConnectionStrings("conStr").ConnectionString
    End Sub
    Private Function getCon() As SqlClient.SqlConnection
        '获取连接对象
        Dim myCon As New SqlClient.SqlConnection
        myCon.ConnectionString = conStr
        Return myCon
    End Function
    Public Function noneQuery(ByVal stoName As String, ByVal queryStr() As inQuery) As Integer
        '返回记录集item条数的查询
        'stoName为存储过程名，queryStr为存储过程参数集合
        Dim myCmd As New SqlClient.SqlCommand, myCon As SqlClient.SqlConnection, dr As SqlClient.SqlDataReader, rt As Integer
        myCon = getCon()
        myCon.Open()
        myCmd.Connection = myCon
        myCmd.CommandType = CommandType.StoredProcedure
        myCmd.CommandText = stoName
        For i = 1 To queryStr.Length
            myCmd.Parameters.AddWithValue(queryStr(i - 1).name, queryStr(i - 1).value)
        Next
        dr = myCmd.ExecuteReader
        rt = 0
        While (dr.Read)
            rt = rt + 1
        End While
        dr.Close()
        myCon.Close()
        Return rt
    End Function
    Public Function insertQuery(ByVal stoName As String, ByVal queryStr() As inQuery) As Boolean
        '插入数据查询
        Try
            Dim myCmd As New SqlClient.SqlCommand, myCon As SqlClient.SqlConnection
            myCon = getCon()
            myCon.Open()
            myCmd.Connection = myCon
            myCmd.CommandType = CommandType.StoredProcedure
            myCmd.CommandText = stoName
            For i = 1 To queryStr.Length
                myCmd.Parameters.AddWithValue(queryStr(i - 1).name, queryStr(i - 1).value)
            Next
            myCmd.ExecuteNonQuery()
            myCon.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getQuery(ByVal stoName As String, ByVal queryStr() As inQuery, ByVal tableName As String) As DataSet
        '返回data表的查询
        Dim myCon As SqlClient.SqlConnection, myDa As New SqlClient.SqlDataAdapter, myCmd As New SqlClient.SqlCommand, dsTp As New DataSet
        myCon = getCon()
        myCon.Open()
        myCmd.Connection = myCon
        myCmd.CommandType = CommandType.StoredProcedure
        myCmd.CommandText = stoName
        For i = 1 To queryStr.Length
            myCmd.Parameters.AddWithValue(queryStr(i - 1).name, queryStr(i - 1).value)
        Next
        myDa.SelectCommand = myCmd
        myDa.Fill(dsTp, tableName)
        myCon.Close()
        Return dsTp
    End Function
    Public Function getQuery(ByVal stoName As String, ByVal tableName As String) As DataSet
        '返回data表的查询
        Dim myCon As SqlClient.SqlConnection, myDa As New SqlClient.SqlDataAdapter, myCmd As New SqlClient.SqlCommand, dsTp As New DataSet
        myCon = getCon()
        myCon.Open()
        myCmd.Connection = myCon
        myCmd.CommandType = CommandType.StoredProcedure
        myCmd.CommandText = stoName
        myDa.SelectCommand = myCmd
        myDa.Fill(dsTp, tableName)
        myCon.Close()
        Return dsTp
    End Function
    Public Function querySql(ByVal strSql As String) As Boolean
        '不返回结果的Sql查询
        Dim myCon As SqlClient.SqlConnection, myCmd As New SqlClient.SqlCommand
        Try
            myCon = getCon()
            myCon.Open()
            myCmd.Connection = myCon
            myCmd.CommandText = strSql
            myCmd.ExecuteNonQuery()
            myCon.Close()
            Return True
        Catch
            Return False
        End Try
    End Function
    Public Function getQuerySql(ByVal strSql As String, ByVal tableName As String) As DataSet
        '返回结果的Sql查询
        Dim myCon As SqlClient.SqlConnection, myDa As New SqlClient.SqlDataAdapter, myCmd As New SqlClient.SqlCommand, dsTp As New DataSet
        myCon = getCon()
        myCon.Open()
        myCmd.Connection = myCon
        myCmd.CommandType = CommandType.Text
        myCmd.CommandText = strSql
        myDa.SelectCommand = myCmd
        myDa.Fill(dsTp, tableName)
        myCon.Close()
        Return dsTp
    End Function
    Public Function hashtableToInquery(ByVal ht As Hashtable) As inQuery()
        Dim inQuery(0 To ht.Count - 1) As dbHelper.inQuery, myMain As New main
        Dim iCount As Integer = 0
        For Each htItem As DictionaryEntry In ht
            inQuery(iCount).name = htItem.Key
            inQuery(iCount).value = myMain.filterCheck(htItem.Value)
            iCount += 1
        Next
        Return inQuery
    End Function
End Class

