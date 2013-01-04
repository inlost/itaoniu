Public Class favorites
    Public myHelper As New dbHelper
    Public Const type_goods = 1
    Public Const type_shop = 2
    Public Function add(ByVal uid As Integer, ByVal type As String, ByVal fid As Integer) As Boolean
        Dim myFav As New Hashtable
        myFav("@uid") = uid
        myFav("@type") = IIf(type = "good", 1, 2)
        myFav("@fid") = fid
        Try
            myHelper.noneQuery("favorites_add", myHelper.hashtableToInquery(myFav))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function del(ByVal uid As Integer, ByVal id As Integer) As Boolean
        Dim myFav As New Hashtable
        myFav("@uid") = uid
        myFav("@id") = id
        Try
            myHelper.noneQuery("favorites_del", myHelper.hashtableToInquery(myFav))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getCount(ByVal uid As Integer, ByVal type As Integer) As Integer
        Dim strSql As String
        strSql = "select count(*) from favorites where uid=" & uid & " and type=" & type
        Dim myDs As New DataSet
        Try
            myDs = myHelper.getQuerySql(strSql, "count")
            Try
                Return myDs.Tables("count").Rows(0)(0)
            Catch ex As Exception
                Return 0
            End Try
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function getGoods(ByVal uid As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
        Dim count As Integer = getCount(uid, type_goods)
        Dim rt() As Hashtable
        If count = 0 Then
            ReDim rt(0 To 0)
            rt(0) = New Hashtable
            rt(0)("count") = 0
            Return rt
        End If
        Dim myQuery As New Hashtable, myDs As New DataSet
        myQuery("@uid") = uid
        myQuery("@type") = type_goods
        myQuery("@page") = page
        myQuery("@prePage") = prePage
        myDs = myHelper.getQuery("favorites_get", myHelper.hashtableToInquery(myQuery), "goods")
        ReDim rt(0 To myDs.Tables("goods").Rows.Count - 1)
        Dim myGoods As New goods
        For i = 0 To rt.Length - 1
            rt(i) = New Hashtable
            rt(i) = myGoods.goods_get(myDs.Tables("goods").Rows(i)(1))
            rt(i)("favId") = myDs.Tables("goods").Rows(i)(0)
        Next
        rt(0)("count") = count
        Return rt
    End Function
    Public Function getShop(ByVal uid As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
        Dim count As Integer = getCount(uid, type_shop)
        Dim rt() As Hashtable
        If count = 0 Then
            ReDim rt(0 To 0)
            rt(0) = New Hashtable
            rt(0)("count") = 0
            Return rt
        End If
        Dim myQuery As New Hashtable, myDs As New DataSet
        myQuery("@uid") = uid
        myQuery("@type") = type_shop
        myQuery("@page") = page
        myQuery("@prePage") = prePage
        myDs = myHelper.getQuery("favorites_get", myHelper.hashtableToInquery(myQuery), "goods")
        ReDim rt(0 To myDs.Tables("goods").Rows.Count - 1)
        Dim myShop As New shop
        For i = 0 To rt.Length - 1
            rt(i) = New Hashtable
            rt(i) = myShop.getShopInfo(myDs.Tables("goods").Rows(i)(1))
            rt(i)("favId") = myDs.Tables("goods").Rows(i)(0)
        Next
        rt(0)("count") = count
        Return rt
    End Function
End Class
