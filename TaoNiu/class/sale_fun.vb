Public Class saleFunction
    Public Function shopJf_get(ByVal sid As Integer) As Integer
        Dim myHelper As New dbHelper
        Dim strSql As String = "select jf from sale_code where type=1 and sid=" & sid
        Dim myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "jf")
        Try
            Return myDs.Tables("jf").Rows(0)(0)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function getZqList(ByVal sId As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet, logs As Hashtable()
        Dim myGet As New Hashtable
        myGet("@sid") = sId
        myGet("@page") = page
        myGet("@prePage") = prePage
        myDs = myHelper.getQuery("sale_zqList_get", myHelper.hashtableToInquery(myGet), "lgList")
        Dim myType As New typeHelper
        logs = myType.dsToHash(myDs, "lgList")
        Dim strSql As String = "select count(*) from sale_code where type = 2 AND sid =" & sId
        myDs = myHelper.getQuerySql(strSql, "counts")
        If logs(0)("count") <> 0 Then
            logs(0)("count") = myDs.Tables("counts")(0)(0)
        End If
        Return logs
    End Function
    Public Function zqSpit(ByVal strZq As String) As dbHelper.inQuery
        Dim xf As String = Left(strZq, strZq.IndexOf("="))
        Dim je As String = Right(strZq, Len(strZq) - strZq.IndexOf("=") - 1)
        Dim rt As dbHelper.inQuery
        rt.name = xf
        rt.value = je
        Return rt
    End Function
    Public Function getPfList(ByVal sId As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet, pfs As Hashtable()
        Dim myGet As New Hashtable
        myGet("@sid") = sId
        myGet("@page") = page
        myGet("@prePage") = prePage
        myDs = myHelper.getQuery("sale_pfList_get", myHelper.hashtableToInquery(myGet), "pfList")
        Dim myType As New typeHelper
        pfs = myType.dsToHash(myDs, "pfList")
        Dim strSql As String = "select count(*) from sale_code where type = 3 AND sid =" & sId
        myDs = myHelper.getQuerySql(strSql, "counts")
        If pfs(0)("count") <> 0 Then
            pfs(0)("count") = myDs.Tables("counts")(0)(0)
        End If
        Return pfs
    End Function
    Public Function getTgList(ByVal uid As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet, tgs As Hashtable()
        Dim myGet As New Hashtable
        myGet("@uid") = uid
        myGet("@page") = page
        myGet("@prePage") = prePage
        myDs = myHelper.getQuery("sale_tgList_get", myHelper.hashtableToInquery(myGet), "tgList")
        Dim myType As New typeHelper
        tgs = myType.dsToHash(myDs, "tgList")
        Dim strSql As String = "select count(*) from sale_code where type = 4 AND sid in (select id from shops where shop_owner=" & uid & ")"
        myDs = myHelper.getQuerySql(strSql, "counts")
        If tgs(0)("count") <> 0 Then
            tgs(0)("count") = myDs.Tables("counts")(0)(0)
        End If
        Return tgs
    End Function
    Public Function getTg(ByVal tgId As Integer) As Hashtable
        Dim strSql As String = "select * from sale_code where id=" & tgId
        Dim myDs As New DataSet, myHelper As New dbHelper
        myDs = myHelper.getQuerySql(strSql, "tg")
        Dim myType As New typeHelper
        Return myType.dsToHashSingle(myDs, "tg")
    End Function
    Public Function getTgOrderCount(ByVal tid As Integer) As Integer
        Dim strSql As String = "select count(*) from orders where orderType=1 and tid=" & tid
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "ct")
        Return myDs.Tables("ct").Rows(0)(0)
    End Function
    Public Function changeOrderToTuan(ByVal oid As Integer, ByVal tid As Integer) As Boolean
        Dim strSql As String = "update orders set orderType=1,tid=" & tid & " where oid=" & oid
        Dim myHelper As New dbHelper
        Try
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function lianMengGoodsAdd(ByVal newLm As Hashtable) As Boolean
        Dim myHelper As New dbHelper
        Try
            myHelper.noneQuery("goods_lianMeng_add", myHelper.hashtableToInquery(newLm))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function lianMengGoodsDel(ByVal lmId As Integer) As Boolean
        Dim strSql As String = "delete from goods_lm where id=" & lmId
        Dim myHelper As New dbHelper
        Try
            Return myHelper.querySql(strSql)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function lianMeng_goods_get_byShop(ByVal sid As Integer) As Hashtable()
        Dim myHelper As New dbHelper, strSql As String = ""
        strSql = "select * from goods_lm where shopId=" & sid
        Try
            Dim myDs As New DataSet, lmList As Hashtable(), myType As New typeHelper
            myDs = myHelper.getQuerySql(strSql, "rt")
            lmList = myType.dsToHash(myDs, "rt")
            If lmList(0)("count") = 0 Then
                Return Nothing
            Else
                Dim goodsList(0 To lmList(0)("count") - 1) As Hashtable
                Dim myGoods As New goods
                For i = 0 To lmList(0)("count") - 1
                    goodsList(i) = New Hashtable
                    goodsList(i) = myGoods.goods_get(lmList(i)("gid"))
                    goodsList(i)("lm_id") = lmList(i)("id")
                    goodsList(i)("discuount") = lmList(i)("discuount")
                    goodsList(i)("needCat") = lmList(i)("needCat")
                Next
                Return goodsList
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function lianMeng_search(ByVal strCat As String) As Hashtable()
        Dim myHelper As New dbHelper, inQuery(0 To 0) As dbHelper.inQuery
        inQuery(0).name = "@cat"
        inQuery(0).value = strCat
        Dim myDs As New DataSet, myType As New typeHelper
        Try
            myDs = myHelper.getQuery("shop_lm_search", inQuery, "rt")
            Return myType.dsToHash(myDs, "rt")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
