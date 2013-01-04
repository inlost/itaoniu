Public Class weituo
    Public Function addWT(ByVal uName As String, ByVal shopId As Integer) As Boolean
        Dim theWT As New Hashtable, myHelper As New dbHelper, myUser As New user
        Dim uid As Integer = myUser.getUserInfoByName(uName)("uid")
        theWT("@uid") = uid
        theWT("@shopId") = shopId
        Try
            myHelper.noneQuery("shop_wt_add", myHelper.hashtableToInquery(theWT))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getWTlist(ByVal uid As Integer) As Hashtable()
        Dim strSql As String = "select * from shop_luru where shopId in (select id from shops where shop_owner=" & uid & ")"
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "rt")
    End Function
    Public Function delWT(ByVal wId As Integer) As Boolean
        Dim strSql As String = "delete from shop_luru where id=" & wId
        Dim myHelper As New dbHelper
        Try
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getWt(ByVal wId As Integer) As Hashtable
        Dim strSql As String = "select * from shop_luru where id=" & wId
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHashSingle(myDs, "rt")
    End Function
    Public Function getWtShopList(ByVal uid As Integer) As Hashtable()
        Dim strSql As String = "select * from shop_luru where uid=" & uid
        Dim myHelper As New dbHelper, myType As New typeHelper
        Return myType.dsToHash(myHelper.getQuerySql(strSql, "rt"), "rt")
    End Function
    Public Function addWtGood(ByVal goodInfo As Hashtable) As Boolean
        Dim myHelper As New dbHelper
        Try
            myHelper.noneQuery("shop_wt_goods_add", myHelper.hashtableToInquery(goodInfo))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getWtListByTyper(ByVal status As Integer, ByVal uid As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
        Dim myHelper As New dbHelper, theQuery As New Hashtable
        theQuery("@uid") = uid
        theQuery("@status") = status
        theQuery("@page") = page
        theQuery("@prePage") = prePage
        Dim typeHelper As New typeHelper
        Return typeHelper.dsToHash(myHelper.getQuery("shop_luru_get_typer", myHelper.hashtableToInquery(theQuery), "rt"), "rt")
    End Function
    Public Function getWtListByOwner(ByVal status As Integer, ByVal uid As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
        Dim myHelper As New dbHelper, theQuery As New Hashtable
        theQuery("@uid") = uid
        theQuery("@status") = status
        theQuery("@page") = page
        theQuery("@prePage") = prePage
        Dim typeHelper As New typeHelper
        Return typeHelper.dsToHash(myHelper.getQuery("shop_luru_get_owner", myHelper.hashtableToInquery(theQuery), "rt"), "rt")
    End Function
    Public Function setWtGoodsPass(ByVal wId As Integer) As Boolean
        Dim theQuery(0 To 0) As dbHelper.inQuery
        theQuery(0).name = "@wid"
        theQuery(0).value = wId
        Dim myHeper As New dbHelper
        Try
            myHeper.noneQuery("shop_luru_goods_pass", theQuery)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
