Public Class searchHelper
    Public ReadOnly Property goods_msCat As Integer
        Get
            Return goods_cat_mSha_get()
        End Get
    End Property
    Public Function goods_cat_mSha_get() As Integer
        Dim strSql As String = "select id from goods_sale_type where name='秒杀'"
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "id")
        Try
            Return myDs.Tables("id").Rows(0)(0)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function goods_get_mSha(Optional ByVal page As Integer = 1, Optional ByVal perPage As Integer = 10) As Hashtable()
        '秒杀
        Dim mShaCat = goods_msCat
        Dim strSql As String = "select top " & perPage & " * from goods_products where goods_sale_type=" & mShaCat
        strSql += " and id not in (select top " & (page - 1) * perPage & " id from goods_products where goods_sale_type=" & mShaCat & " order by id DESC)"
        strSql += " order by id DESC"
        Dim myDs As New DataSet, myHelper As New dbHelper
        myDs = myHelper.getQuerySql(strSql, "goods")
        Dim rt(0 To myDs.Tables("goods").Rows.Count - 1) As Hashtable
        Dim iCount As Integer = 0
        For Each tr As DataRow In myDs.Tables("goods").Rows
            rt(iCount) = New Hashtable
            For j = 0 To myDs.Tables("goods").Columns.Count - 1
                rt(iCount).Add(myDs.Tables("goods").Columns(j).ColumnName, tr(j).ToString)
            Next
            iCount += 1
        Next
        Return rt
    End Function
    Public Function goods_get_TeJia(Optional ByVal page As Integer = 1, Optional ByVal perPage As Integer = 10) As Hashtable()
        '折扣特价
        Dim mShaCat = goods_msCat
        Dim strSql As String = "select top " & perPage & " * from goods_products where goods_discount > 0"
        strSql += " and id not in (select top " & (page - 1) * perPage & " id from goods_products where  goods_discount > 0 order by id DESC)"
        strSql += " order by id DESC"
        Dim myDs As New DataSet, myHelper As New dbHelper
        myDs = myHelper.getQuerySql(strSql, "goods")
        Dim rt(0 To myDs.Tables("goods").Rows.Count - 1) As Hashtable
        Dim iCount As Integer = 0
        For Each tr As DataRow In myDs.Tables("goods").Rows
            rt(iCount) = New Hashtable
            For j = 0 To myDs.Tables("goods").Columns.Count - 1
                rt(iCount).Add(myDs.Tables("goods").Columns(j).ColumnName, tr(j).ToString)
            Next
            iCount += 1
        Next
        Return rt
    End Function
    Public Function goods_get_fromShop(ByVal shopId As Integer, Optional ByVal page As Integer = 1, Optional ByVal perPage As Integer = 10) As Hashtable()
        '商店货物
        Dim mShaCat = goods_msCat
        Dim strSql As String = "select top " & perPage & " * from goods_products where shopId=" & shopId
        strSql += " and id not in (select top " & (page - 1) * perPage & " id from goods_products where shopId=" & shopId & " order by id DESC)"
        strSql += " order by id DESC"
        Dim myDs As New DataSet, myHelper As New dbHelper
        myDs = myHelper.getQuerySql(strSql, "goods")
        Dim rt(0 To myDs.Tables("goods").Rows.Count - 1) As Hashtable
        Dim iCount As Integer = 0
        For Each tr As DataRow In myDs.Tables("goods").Rows
            rt(iCount) = New Hashtable
            For j = 0 To myDs.Tables("goods").Columns.Count - 1
                rt(iCount).Add(myDs.Tables("goods").Columns(j).ColumnName, tr(j).ToString)
            Next
            iCount += 1
        Next
        Return rt
    End Function
    Public Function goods_get_fromShopTui(ByVal shopId As Integer) As Hashtable()
        Dim strSql As String
        strSql = "select top 5 * from goods_products where shopId=" & shopId & " and goods_tuijian=1"
        Dim myDs As New DataSet, myHelper As New dbHelper
        myDs = myHelper.getQuerySql(strSql, "goods")
        Dim rt(0 To myDs.Tables("goods").Rows.Count - 1) As Hashtable
        Dim iCount As Integer = 0
        For Each tr As DataRow In myDs.Tables("goods").Rows
            rt(iCount) = New Hashtable
            For j = 0 To myDs.Tables("goods").Columns.Count - 1
                rt(iCount).Add(myDs.Tables("goods").Columns(j).ColumnName, tr(j).ToString)
            Next
            iCount += 1
        Next
        Return rt
    End Function
    Public Function goods_get_fromShelf(ByVal shopId As Integer, ByVal shelfId As Integer, Optional ByVal perPage As Integer = 5, Optional ByVal page As Integer = 1) As Hashtable()
        Dim haQuery As New Hashtable
        haQuery("@shelfId") = shelfId
        haQuery("@shopId") = shopId
        haQuery("@page") = page
        haQuery("@prePage") = perPage
        Dim myDs As New DataSet, myHelper As New dbHelper, rt() As Hashtable
        myDs = myHelper.getQuery("goods_get_fromShelf", myHelper.hashtableToInquery(haQuery), "goods")
        If myDs.Tables("goods").Rows.Count = 0 Then
            ReDim rt(0 To 0)
            rt(0) = New Hashtable
            rt(0)("count") = 0
        Else
            ReDim rt(0 To myDs.Tables("goods").Rows.Count - 1)
            rt(0) = New Hashtable
            Dim newDs As New DataSet
            Dim qr(0 To 0) As dbHelper.inQuery
            qr(0).name = "@shId"
            qr(0).value = shelfId
            newDs = myHelper.getQuery("goods_count_fromShelf", qr, "ct")
            rt(0)("count") = newDs.Tables("ct").Rows(0)(0)
        End If
        Dim iCount As Integer = 0
        For Each tr As DataRow In myDs.Tables("goods").Rows
            If iCount <> 0 Then rt(iCount) = New Hashtable
            For j = 0 To myDs.Tables("goods").Columns.Count - 1
                rt(iCount).Add(myDs.Tables("goods").Columns(j).ColumnName, tr(j).ToString)
            Next
            iCount += 1
        Next
        Return rt
    End Function
    Public Function catDeep_get(ByVal catId As Integer) As Integer
        '获取分类层级
        Dim strSql As String, myHelper As New dbHelper, myDs As New DataSet
        strSql = "select catDeep from goods_cat where id=" & catId
        myDs = myHelper.getQuerySql(strSql, "rt")
        Try
            Return myDs.Tables("rt").Rows(0)(0)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function catFather_get(ByVal catId As Integer) As Integer
        '获取分类父级
        Dim strSql As String, myHelper As New dbHelper, myDs As New DataSet
        strSql = "select fatherCat from goods_cat where id=" & catId
        myDs = myHelper.getQuerySql(strSql, "rt")
        Try
            Return myDs.Tables("rt").Rows(0)(0)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function catInfo_get(ByVal catId As Integer) As Hashtable
        '分类信息查询
        Dim strSql As String = "select * from goods_cat where id=" & catId
        Dim myHelper As New dbHelper, myDs As New DataSet, rt As New Hashtable
        myDs = myHelper.getQuerySql(strSql, "rt")
        For i = 0 To myDs.Tables("rt").Columns.Count - 1
            rt(myDs.Tables("rt").Columns(i).ColumnName) = myDs.Tables("rt").Rows(0)(i)
        Next
        Return rt
    End Function
    Public Function goods_get_fromCat(ByVal cat As Integer, ByVal orderBy As String, Optional ByVal page As Integer = 1, Optional ByVal prePage As Integer = 15) As Hashtable()
        Dim orderByIs As String = "", orderByDesc As Integer = 0, iCount As Integer
        Dim myDs As New DataSet, myHelper As New dbHelper
        Dim countQyery(0 To 0) As dbHelper.inQuery
        countQyery(0).name = "@catId"
        countQyery(0).value = cat
        myDs = myHelper.getQuery("goods_count_get_fromCat", countQyery, "count")
        iCount = myDs.Tables("count").Rows(0)(0)
        Select Case orderBy
            Case "time-new"
                orderByIs = "id"
                orderByDesc = 0
            Case "time-old"
                orderByIs = "id"
                orderByDesc = 1
            Case "price-cheap"
                orderByIs = "goods_prize"
                orderByDesc = 1
            Case "place-near"
                orderByIs = "time"
                orderByDesc = 1
            Case "price-expensive"
                orderByIs = "goods_prize"
                orderByDesc = 0
            Case "hot"
                orderByIs = "goods_pj_good"
                orderByDesc = 0
        End Select
        Dim inQuery As New Hashtable
        inQuery("@catId") = cat
        inQuery("@page") = page
        inQuery("@prePage") = prePage
        inQuery("@orderByIs") = orderByIs
        inQuery("@orderDesc") = orderByDesc
        myDs = myHelper.getQuery("goods_get_fromCat", myHelper.hashtableToInquery(inQuery), "goods")
        Dim rt() As Hashtable
        If myDs.Tables("goods").Rows.Count - 1 >= 0 Then
            ReDim rt(0 To myDs.Tables("goods").Rows.Count - 1)
        Else
            ReDim rt(0 To 0)
        End If
        rt(0) = New Hashtable
        rt(0)("totalCount") = iCount
        rt(0)("count") = myDs.Tables("goods").Rows.Count
        iCount = 0
        For Each rowItem As DataRow In myDs.Tables("goods").Rows
            If iCount <> 0 Then rt(iCount) = New Hashtable
            For i = 0 To myDs.Tables("goods").Columns.Count - 1
                rt(iCount)(myDs.Tables("goods").Columns(i).ColumnName) = rowItem(i)
            Next
            iCount += 1
        Next
        Return rt
    End Function
    Public Function getSideBar(ByVal catId As Integer) As Hashtable()
        Dim strSql As String, myHelper As New dbHelper
        strSql = "select top 8 * from goods_products where goods_tuijian=1 and goods_cat like '%" & catId & "%' order by id desc"
        Dim myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "goods")
        Return goodsToHashtable(myDs, "goods")
    End Function
    Public Function goodsToHashtable(ByVal ds As DataSet, ByVal tableName As String) As Hashtable()
        Dim goodsList() As Hashtable
        If ds.Tables(tableName).Rows.Count - 1 >= 0 Then
            ReDim goodsList(0 To ds.Tables(tableName).Rows.Count - 1)
        Else
            ReDim goodsList(0 To 0)
        End If
        Dim iCount As Integer = 0
        goodsList(0) = New Hashtable
        goodsList(0)("count") = ds.Tables(tableName).Rows.Count
        For Each good As DataRow In ds.Tables(tableName).Rows
            If iCount <> 0 Then goodsList(iCount) = New Hashtable
            For i = 0 To ds.Tables(tableName).Columns.Count - 1
                goodsList(iCount)(ds.Tables(tableName).Columns(i).ColumnName) = good(i)
            Next
            iCount += 1
        Next
        Return goodsList
    End Function
    Public Function goods_get_fromSearch(ByVal theSearch As Hashtable) As Hashtable()
        Dim myHelper As New dbHelper, theCountQuery As New Hashtable, rt As Hashtable()
        theCountQuery("@strSearch") = theSearch("@strSearch")
        theCountQuery("@payType") = theSearch("@payType")
        theCountQuery("@shop_certification") = theSearch("@shop_certification")
        theCountQuery("@isOnLine") = theSearch("@isOnLine")
        theCountQuery("@goods_range") = theSearch("@goods_range")
        Dim totleCount As Integer = myHelper.getQuery("goods_count_get_fromSearch", myHelper.hashtableToInquery(theCountQuery), "rt").Tables("rt").Rows(0)(0)
        If totleCount = 0 Then
            ReDim rt(0 To 0)
            rt(0) = New Hashtable
            rt(0)("count") = 0
            Return rt
        End If
        Dim myDs As New DataSet, myType As New typeHelper
        myDs = myHelper.getQuery("goods_search", myHelper.hashtableToInquery(theSearch), "rt")
        rt = myType.dsToHash(myDs, "rt")
        rt(0)("count") = totleCount
        Return rt
    End Function
    Public Function goods_get_fromFree(Optional ByVal page As Integer = 1, Optional ByVal prePage As Integer = 12)
        Dim theSearch As New Hashtable, myHelper As New dbHelper
        theSearch("@page") = page
        theSearch("@prePage") = prePage
        Dim myDs As New DataSet
        myDs = myHelper.getQuery("goods_get_free", myHelper.hashtableToInquery(theSearch), "goods")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "goods")
    End Function
    Public Function goods_get_fromZq(Optional ByVal page As Integer = 1, Optional ByVal prePage As Integer = 12)
        Dim theSearch As New Hashtable, myHelper As New dbHelper
        theSearch("@page") = page
        theSearch("@prePage") = prePage
        Dim myDs As New DataSet
        myDs = myHelper.getQuery("goods_get_zq", myHelper.hashtableToInquery(theSearch), "goods")
        Dim myRt() As Hashtable, gidList() As Hashtable
        Dim myType As New typeHelper
        gidList = myType.dsToHash(myDs, "goods")
        If gidList(0)("count") = 0 Then
            ReDim myRt(0 To 0)
            myRt(0) = New Hashtable
            myRt(0)("count") = 0
            Return myRt(0)
        End If
        ReDim myRt(0 To gidList.Count - 1)
        Dim myGoods As New goods
        For i = 0 To gidList.Count - 1
            myRt(i) = New Hashtable
            myRt(i) = myGoods.goods_get(gidList(i)("gid"))
            myRt(i)("zq") = gidList(i)("zq")
            myRt(i)("zid") = gidList(i)("id")
            myRt(i)("timeLength") = gidList(i)("timeLength")
        Next
        myRt(0)("count") = myRt.Count
        Return myRt
    End Function
    Public Function goods_get_fromShuai(Optional ByVal page As Integer = 1, Optional ByVal prePage As Integer = 12)
        Dim theSearch As New Hashtable, myHelper As New dbHelper
        theSearch("@page") = page
        theSearch("@prePage") = prePage
        Dim myDs As New DataSet
        myDs = myHelper.getQuery("goods_get_shuai", myHelper.hashtableToInquery(theSearch), "goods")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "goods")
    End Function
    Public Function goods_get_fromTuan(Optional ByVal page As Integer = 1, Optional ByVal prePage As Integer = 12)
        Dim theSearch As New Hashtable, myHelper As New dbHelper
        theSearch("@page") = page
        theSearch("@prePage") = prePage
        Dim myDs As New DataSet
        myDs = myHelper.getQuery("goods_get_tuan", myHelper.hashtableToInquery(theSearch), "goods")
        Dim myRt() As Hashtable, gidList() As Hashtable
        Dim myType As New typeHelper
        gidList = myType.dsToHash(myDs, "goods")
        If gidList(0)("count") = 0 Then
            ReDim myRt(0 To 0)
            myRt(0) = New Hashtable
            myRt(0)("count") = 0
            Return myRt(0)
        End If
        ReDim myRt(0 To gidList.Count - 1)
        Dim myGoods As New goods
        For i = 0 To gidList.Count - 1
            myRt(i) = New Hashtable
            myRt(i) = myGoods.goods_get(gidList(i)("gid"))
            myRt(i)("pf") = gidList(i)("pf")
            myRt(i)("tid") = gidList(i)("id")
        Next
        myRt(0)("count") = myRt.Count
        Return myRt
    End Function
    Public Function goods_get_fromPi(Optional ByVal page As Integer = 1, Optional ByVal prePage As Integer = 12)
        Dim theSearch As New Hashtable, myHelper As New dbHelper
        theSearch("@page") = page
        theSearch("@prePage") = prePage
        Dim myDs As New DataSet
        myDs = myHelper.getQuery("goods_get_pi", myHelper.hashtableToInquery(theSearch), "goods")
        Dim myRt() As Hashtable, gidList() As Hashtable
        Dim myType As New typeHelper
        gidList = myType.dsToHash(myDs, "goods")
        If gidList(0)("count") = 0 Then
            ReDim myRt(0 To 0)
            myRt(0) = New Hashtable
            myRt(0)("count") = 0
            Return myRt(0)
        End If
        ReDim myRt(0 To gidList.Count - 1)
        Dim myGoods As New goods
        For i = 0 To gidList.Count - 1
            myRt(i) = New Hashtable
            myRt(i) = myGoods.goods_get(gidList(i)("gid"))
            myRt(i)("pf") = gidList(i)("pf")
            myRt(i)("tid") = gidList(i)("id")
        Next
        myRt(0)("count") = myRt.Count
        Return myRt
    End Function
End Class
