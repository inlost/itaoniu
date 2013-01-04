Public Class goods
    Public Function cat_add(ByVal catName As String, ByVal catDeep As String, Optional ByVal catFather As String = "0") As String
        '分类添加 catName=分类名称 catDeep=几级分类（1，2，3） catFather=父级分类
        Dim inQuery(0 To 2) As dbHelper.inQuery
        inQuery(0).name = "@catDeep"
        inQuery(0).value = catDeep
        inQuery(1).name = "@catName"
        inQuery(1).value = catName
        inQuery(2).name = "@fatherCat"
        inQuery(2).value = catFather
        Dim myHelper As New dbHelper, myDs As DataSet
        Try
            myDs = myHelper.getQuery("goods_cat_add", inQuery, "rtCat")
            Dim id As Integer = myDs.Tables("rtCat").Rows(0)(0).ToString
            Dim strSql As String = "update goods_cat set orderby=" & id & " where id=" & id
            myHelper.querySql(strSql)
            Return id
        Catch ex As Exception
            Return "error"
        End Try
    End Function
    Public Function cat_ready_add(ByVal catName As String, ByVal catDeep As Integer, ByVal catFather As Integer) As Integer
        Dim myCat As New Hashtable, myHelper As New dbHelper
        myCat("@catName") = catName
        myCat("@catDeep") = catDeep
        myCat("@fatherCat") = catFather
        Try
            Dim catId As Integer = myHelper.getQuery("goods_cat_ready_add", myHelper.hashtableToInquery(myCat), "rtCat").Tables("rtCat").Rows(0)(0)
            Dim strSql As String = "select catName from goods_cat where id=" & catId
            Try
                If myHelper.getQuerySql(strSql, "rt").Tables("rt").Rows(0)(0) = catName Then
                    Return catId
                End If
                Return -1
            Catch ex As Exception
                Return -1
            End Try
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function cat_get(ByVal catDeep As Integer) As dbHelper.inQuery()
        '获取指定层级的分类
        Dim strSql As String
        strSql = "select id,catName from goods_cat where catDeep=" & catDeep & " order by orderby asc"
        Return cat_Query(strSql)
    End Function
    Public Function cat_get(ByVal catDeep As Integer, ByVal catFather As Integer) As dbHelper.inQuery()
        '获取指定层级指定父分类下的所有子分类
        Dim strSql As String
        strSql = "select id,catName from goods_cat where catDeep=" & catDeep & " and fatherCat=" & catFather & " order by orderby asc"
        Return cat_Query(strSql)
    End Function
    Public Function cat_Query(ByVal strSql As String) As dbHelper.inQuery()
        Dim myHelper As New dbHelper, myDs As DataSet
        Try
            myDs = myHelper.getQuerySql(strSql, "table")
            Dim inQuery(0 To myDs.Tables("table").Rows.Count - 1) As dbHelper.inQuery
            For i = 0 To myDs.Tables("table").Rows.Count - 1
                inQuery(i).name = myDs.Tables("table").Rows(i)(1)
                inQuery(i).value = myDs.Tables("table").Rows(i)(0)
            Next
            Return inQuery
        Catch ex As Exception
            Dim inQuery(0 To 0) As dbHelper.inQuery
            inQuery(0).name = "error"
            inQuery(0).value = ex.Message
            Return inQuery
        End Try
    End Function
    Public Function cat_remove(ByVal catId As Integer) As Boolean
        '删除分类以及所有该分类下的子分类
        Dim myHelper As New dbHelper, strSql As String
        Try
            strSql = "delete from goods_cat where id=" & catId
            myHelper.querySql(strSql)
            strSql = "delete from goods_cat where fatherCat in (select id from goods_cat where fatherCat=" & catId & ")"
            myHelper.querySql(strSql)
            strSql = "delete from goods_cat where fatherCat=" & catId
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function cat_remove(ByVal catId As Integer, ByVal newFather As Integer) As Boolean
        '删除分类并将子分类的父分类指定为newFather
        Dim myHelper As New dbHelper, strSql As String
        Try
            strSql = "delete from goods_cat where id=" & catId
            myHelper.querySql(strSql)
            strSql = "update goods_cat set fatherCat=" & newFather & " where fatherCat=" & catId
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function checkProduct(ByVal myProduct As Hashtable) As Hashtable
        '检查商品信息是否正确
        Dim checkRst As New Hashtable
        For Each haItem As DictionaryEntry In myProduct
            Select Case haItem.Key
                Case "@shopId"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商店编号输入不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商店编号不能为空"
                        Return checkRst
                    End If
                Case "@ownerId"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "店主编号输入不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "店主编号不能为空"
                        Return checkRst
                    End If
                Case "@goods_cat"
                    Dim arrayTp As String() = haItem.Value.ToString.Split("-")
                    For Each strTp As String In arrayTp
                        If checkIsNotNull(strTp) Then
                            Try
                                Convert.ToInt32(strTp)
                            Catch ex As Exception
                                checkRst("result") = False
                                checkRst("message") = "商品分类选择不正确"
                                Return checkRst
                            End Try
                        Else
                            checkRst("result") = False
                            checkRst("message") = "商品分类没有选择"
                            Return checkRst
                        End If
                    Next
                Case "@goods_type"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品类型选择不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商品类型没有选择"
                        Return checkRst
                    End If
                Case "@goods_sale_type"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品销售类型选择不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商品销售类型没有选择"
                        Return checkRst
                    End If
                Case "@goods_title"
                    If checkIsNotNull(haItem.Value) = False Then
                        checkRst("result") = False
                        checkRst("message") = "商品名称不能为空"
                        Return checkRst
                    End If
                Case "@goods_picture"
                    If checkIsNotNull(haItem.Value) = False Then
                        checkRst("result") = False
                        checkRst("message") = "商品图片不能为空"
                        Return checkRst
                    End If
                Case "@goods_prize"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品价格输入不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商品价格不能为空"
                        Return checkRst
                    End If
                Case "@goods_discount"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品折扣输入不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商品折扣不能为空"
                        Return checkRst
                    End If
                Case "@goods_info"
                    Dim arrayTp As String() = haItem.Value.ToString.Split("-")
                    For Each strTp As String In arrayTp
                        If checkIsNotNull(strTp) Then
                            If strTp.ToLower().IndexOf("=") < 0 Then
                                checkRst("result") = False
                                checkRst("message") = "商品属性输入不正确"
                                Return checkRst
                            End If
                        Else
                            checkRst("result") = False
                            checkRst("message") = "商品属性输入不正确"
                            Return checkRst
                        End If
                    Next
                Case "@goods_introduce"
                    If checkIsNotNull(haItem.Value) = False Then
                        checkRst("result") = False
                        checkRst("message") = "商品介绍不能为空"
                        Return checkRst
                    End If
                Case "@goods_number"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品数量输入不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商品数量不能为空"
                        Return checkRst
                    End If
                Case "@goods_on_sale_date"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToDateTime(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品开售日期输入不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商店开售日期不能为空"
                        Return checkRst
                    End If
                Case "@goods_tuijian"
                    If checkIsNotNull(haItem.Value) Then
                        If haItem.Value <> 1 And haItem.Value <> 0 Then
                            checkRst("result") = False
                            checkRst("message") = "橱窗设置不正确"
                            Return checkRst
                        End If
                    Else
                        checkRst("result") = False
                        checkRst("message") = "橱窗设置不能为空"
                        Return checkRst
                    End If
                Case "@goods_range"
                    If checkIsNotNull(haItem.Value) = False Then
                        checkRst("result") = False
                        checkRst("message") = "商品配送范围不能为空"
                        Return checkRst
                    End If
                Case "@goods_period"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToDateTime(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品有效期输入不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商店有效期不能为空"
                        Return checkRst
                    End If
                Case "@goods_invoice"
                    If checkIsNotNull(haItem.Value) Then
                        If haItem.Value <> 1 And haItem.Value <> 0 Then
                            checkRst("result") = False
                            checkRst("message") = "商品发票类型不正确"
                            Return checkRst
                        End If
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商品发票类型不能为空"
                        Return checkRst
                    End If
                Case "@goods_warranty_type"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品保修类型不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商品保修类型不能为空"
                        Return checkRst
                    End If
                Case "@goods_warranty_time"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "商品保修时长不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "商品保修时长不能为空"
                        Return checkRst
                    End If
                Case "@goods_warranty_other"
                    If checkIsNotNull(haItem.Value) = False Then
                        checkRst("result") = False
                        checkRst("message") = "商品保修补充不能为空"
                        Return checkRst
                    End If
                Case "@shelf"
                    If checkIsNotNull(haItem.Value) Then
                        Try
                            Convert.ToInt32(haItem.Value)
                        Catch ex As Exception
                            checkRst("result") = False
                            checkRst("message") = "货架不正确"
                            Return checkRst
                        End Try
                    Else
                        checkRst("result") = False
                        checkRst("message") = "货架不能为空"
                        Return checkRst
                    End If
            End Select
        Next
        checkRst("result") = True
        Return checkRst
    End Function
    Public Function goods_add(ByVal ht As Hashtable) As Integer
        '添加商品
        Dim inQuery() As dbHelper.inQuery, myHelper As New dbHelper
        inQuery = myHelper.hashtableToInquery(ht)
        Return goods_add(inQuery)
    End Function
    Public Function goods_add(ByVal inQuery() As dbHelper.inQuery) As Integer
        '添加商品的重载
        Dim myHelper As New dbHelper
        Dim ids As New DataSet
        Try
            ids = myHelper.getQuery("goods_products_add", inQuery, "ids")
            Dim id As String = ids.Tables("ids").Rows(0)(0)
            For Each gInfo As dbHelper.inQuery In inQuery
                If gInfo.name = "@goods_range" Then
                    Dim strRange As String = gInfo.value
                    Dim ranges() As String = strRange.Split("|")
                    For Each rg As String In ranges
                        If Len(rg) < 2 Then Exit For
                        Dim rTime As String = Right(rg, Len(rg) - rg.IndexOf(":") - 1)
                        Dim newRg As String = Left(rg, Len(rg) - Len(rTime) - 1)
                        Dim rId As String = Right(newRg, Len(newRg) - newRg.IndexOf("-") - 1)
                        Dim newRange As New Hashtable
                        newRange("@range") = rId
                        newRange("@father") = Left(newRg, Len(newRg) - Len(rId) - 1)
                        newRange("@relId") = id
                        newRange("@relType") = 2
                        newRange("@rangeTime") = rTime
                        myHelper.insertQuery("range_add", myHelper.hashtableToInquery(newRange))
                    Next
                    Exit For
                End If
            Next
            Return ids.Tables("ids").Rows(0)(0)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function get_goods_type() As dbHelper.inQuery()
        Dim rt() As dbHelper.inQuery, strSql As String, myHelper As New dbHelper, myDs As DataSet
        strSql = "select * from goods_type"
        myDs = myHelper.getQuerySql(strSql, "goodsType")
        ReDim rt(0 To myDs.Tables("goodsType").Rows.Count - 1)
        For i = 0 To myDs.Tables("goodsType").Rows.Count - 1
            rt(i).name = myDs.Tables("goodsType").Rows(i)(1)
            rt(i).value = myDs.Tables("goodsType").Rows(i)(0)
        Next
        Return rt
    End Function
    Public Function get_goods_sale_type() As dbHelper.inQuery()
        Dim rt() As dbHelper.inQuery, strSql As String, myHelper As New dbHelper, myDs As DataSet
        strSql = "select * from goods_sale_type"
        myDs = myHelper.getQuerySql(strSql, "goodsType")
        ReDim rt(0 To myDs.Tables("goodsType").Rows.Count - 1)
        For i = 0 To myDs.Tables("goodsType").Rows.Count - 1
            rt(i).name = myDs.Tables("goodsType").Rows(i)(1)
            rt(i).value = myDs.Tables("goodsType").Rows(i)(0)
        Next
        Return rt
    End Function
    Public Function checkIsNotNull(ByVal strIn As String) As Boolean
        '非空检查
        If Trim(strIn) = "" Or Trim(strIn) = Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function goods_get(ByVal gid As Integer) As Hashtable
        Dim inQuery(0 To 0) As dbHelper.inQuery, good As New Hashtable
        inQuery(0).name = "@gid"
        inQuery(0).value = gid
        Dim myHelper As New dbHelper, myDs As New DataSet
        Try
            myDs = myHelper.getQuery("goods_get", inQuery, "good")
            For i = 0 To myDs.Tables("good").Columns.Count - 1
                good(myDs.Tables("good").Columns(i).ColumnName) = myDs.Tables("good").Rows(0)(i)
            Next
            good("status") = "ok"
        Catch ex As Exception
            good("status") = "error"
        End Try
        Return good
    End Function
    Public Function goods_pj_get(ByVal gid As Integer) As Hashtable()
        Dim strSql As String = "select * from orders_comments where gid=" & gid
        Dim myHelper As New dbHelper, myDs As New DataSet, rt() As Hashtable
        myDs = myHelper.getQuerySql(strSql, "comments")
        If myDs.Tables("comments").Rows.Count = 0 Then
            ReDim rt(0 To 0)
            rt(0) = New Hashtable
            rt(0)("count") = 0
        Else
            ReDim rt(0 To myDs.Tables("comments").Rows.Count - 1)
            rt(0) = New Hashtable
            rt(0)("count") = myDs.Tables("comments").Rows.Count
        End If
        Dim iCount As Integer = 0
        For Each tr As DataRow In myDs.Tables("comments").Rows
            If iCount <> 0 Then rt(iCount) = New Hashtable
            For j = 0 To myDs.Tables("comments").Columns.Count - 1
                rt(iCount).Add(myDs.Tables("comments").Columns(j).ColumnName, tr(j).ToString)
            Next
            iCount += 1
        Next
        Return rt
    End Function
    Public Function good_send_range_get(Optional ByVal father = -1) As Hashtable()
        '配送地域获取
        Dim strSql As String = "select * from send_range where father=" & father
        Dim myHelper As New dbHelper, myRange() As Hashtable, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "range")
        Dim myType As New typeHelper
        myRange = myType.dsToHash(myDs, "range")
        Return myRange
    End Function
    Public Function good_send_range_get_str(Optional ByVal father = -1) As String
        '配送地域获取 返回下拉列表项
        Dim rg() As Hashtable = good_send_range_get(father)
        If rg(0)("count") < 1 Then
            Return ""
        Else
            Dim rt As String = ""
            For Each r As Hashtable In rg
                rt += "<li value='" & r("range") & "'>" & r("range") & "</li>"
            Next
            Return rt
        End If
    End Function
    Public Function good_send_range_get_option(Optional ByVal father = -1) As String
        '配送地域获取 返回下拉列表项
        Dim rg() As Hashtable = good_send_range_get(father)
        If rg(0)("count") < 1 Then
            Return ""
        Else
            Dim rt As String = ""
            For Each r As Hashtable In rg
                rt += "<option value='" & r("range") & "'>" & r("range") & "</option>"
            Next
            Return rt
        End If
    End Function
    Public Function cat_property_get(ByVal cat As String) As String
        '类别属性获取
        Dim strWite As String = ""
        Dim separator As Char() = {"-"c}
        Dim separator2 As Char() = {"|"c}
        Dim cats As String() = cat.Split(separator)
        For Each it As String In cats
            Try
                Convert.ToInt64(it)
                Dim myPros As Hashtable()
                myPros = cat_property_get(it, True)
                If myPros(0)("count") <> 0 Then
                    For Each pros As Hashtable In myPros
                        If pros("type") = 1 Then
                            '下拉列表
                            strWite += "<div class='goodInfos'>"
                            strWite += "<input type='hidden' value='" & pros("name") & "'/>" & pros("name")
                            strWite += "<select>"
                            Dim propertyList As String() = pros("property").ToString.Split(separator2)
                            For Each prItem As String In propertyList
                                strWite += "<option value='" & prItem & "'>" & prItem & "</option>"
                            Next
                            strWite += "</select>"
                            strWite += "</div>"
                        Else
                            '复选框
                            strWite += "<div class='goodInfos'>"
                            strWite += "<input type='hidden' value='" & pros("name") & "/'>" & pros("name")
                            Dim propertyList As String() = pros("property").ToString.Split(separator2)
                            For Each prItem As String In propertyList
                                strWite += "<input type='checkbox' value='" & prItem & "'/>" & prItem
                            Next
                            strWite += "</div>"
                        End If
                    Next
                End If
            Catch ex As Exception
                Return ""
            End Try
        Next
        Return strWite
    End Function
    Private Function cat_property_get(ByVal cat As Integer, ByVal getAsHashtable As Boolean) As Hashtable()
        Dim strSql As String = "select * from cat_property where catId=" & cat
        Dim myHelpr As New dbHelper, myType As New typeHelper
        Return myType.dsToHash(myHelpr.getQuerySql(strSql, "rt"), "rt")
    End Function
End Class
