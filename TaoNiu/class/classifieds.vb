Public Class classifieds
    Public Function cat_add(theCat As Hashtable) As Integer
        Dim myHelper As New dbHelper, myDs As New DataSet
        Try
            myDs = myHelper.getQuery("classifieds_cat_add", myHelper.hashtableToInquery(theCat), "rt")
            Return myDs.Tables("rt").Rows(0)(0)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function cat_order_set(order As Integer, catId As Integer) As Boolean
        Dim myHelper As New dbHelper
        Dim strSql As String = "update classifieds_cats set catOrder=" & order & " where id=" & catId
        Try
            Return myHelper.querySql(strSql)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function cat_get(catDeep As Integer, catFather As Integer) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet
        Dim inQuery As New Hashtable
        inQuery("@catDeep") = catDeep
        inQuery("@catFather") = catFather
        myDs = myHelper.getQuery("classifieds_cat_count_get", myHelper.hashtableToInquery(inQuery), "rt")
        Dim iCount As Integer = myDs.Tables("rt").Rows(0)(0)
        If iCount = 0 Then Return Nothing
        myDs = myHelper.getQuery("classifieds_cat_get", myHelper.hashtableToInquery(inQuery), "rt")
        Dim rt As Hashtable(), myType As New typeHelper
        rt = myType.dsToHash(myDs, "rt")
        Return rt
    End Function
    Public Function post_add(thePost As Hashtable) As Integer
        Dim myHelper As New dbHelper, myDs As New DataSet
        Try
            myDs = myHelper.getQuery("classifieds_post_add", myHelper.hashtableToInquery(thePost), "rt")
            Return myDs.Tables("rt").Rows(0)(0)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function cat_get(id As Integer) As Hashtable
        Dim myHelper As New dbHelper, myDs As New DataSet, strSql As String
        strSql = "select * from classifieds_cats where id=" & id
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHashSingle(myDs, "rt")
    End Function
    Public Function posts_get(catId As Integer, page As Integer, prePage As Integer) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet
        Dim inQuery(0 To 0) As dbHelper.inQuery
        inQuery(0).name = "@cat"
        inQuery(0).value = "-" & catId
        myDs = myHelper.getQuery("classifieds_post_count_get", inQuery, "rt")
        Dim iCount As Integer = myDs.Tables("rt").Rows(0)(0)
        If iCount = 0 Then
            Dim rt(0 To 0) As Hashtable
            rt(0) = New Hashtable
            rt(0)("count") = 0
            Return rt
        End If
        Dim theQuery As New Hashtable
        theQuery("@page") = page
        theQuery("@prePage") = prePage
        theQuery("@cat") = inQuery(0).value
        myDs = myHelper.getQuery("classifieds_post_get_fromCat", myHelper.hashtableToInquery(theQuery), "rt")
        Dim rst As Hashtable(), myType As New typeHelper
        rst = myType.dsToHash(myDs, "rt")
        rst(0)("count") = iCount
        Return rst
    End Function
    Public Function post_get(pid As Integer) As Hashtable
        Dim thePost As New Hashtable, myHelper As New dbHelper, strSql As String
        strSql = "select * from classifieds_post where id=" & pid
        Try
            Dim myDs As New DataSet
            myDs = myHelper.getQuerySql(strSql, "rt")
            Dim myType As New typeHelper
            Return myType.dsToHashSingle(myDs, "rt")
        Catch ex As Exception
            thePost("id") = -1
            Return thePost
        End Try
    End Function
End Class
