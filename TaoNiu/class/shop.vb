Public Class shop
    Public Function add(ByVal shopInfo As Hashtable, ByVal range As String) As Boolean
        '添加店铺
        Dim inQuery() As dbHelper.inQuery, myHelper As New dbHelper, myShop As New shop
        inQuery = myHelper.hashtableToInquery(shopInfo)
        Dim myDs As New DataSet
        Try
            myDs = myHelper.getQuery("shop_add", inQuery, "ids")
            Dim id = myDs.Tables("ids").Rows(0)(0)
            Return send_range_add(range, id)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function send_range_add(ByVal range As String, ByVal shopId As Integer) As Boolean
        '商店送货范围添加 range：城区-街道：送到时间|城区-街道：送到时间
        Dim myHelper As New dbHelper
        Dim strRange As String = range
        Dim ranges() As String = strRange.Split("|")
        Try
            For Each rg As String In ranges
                If Len(rg) < 2 Then Exit For
                Dim rTime As String = Right(rg, Len(rg) - rg.IndexOf(":") - 1)
                Dim newRg As String = Left(rg, Len(rg) - Len(rTime) - 1)
                Dim rId As String = Right(newRg, Len(newRg) - newRg.IndexOf("-") - 1)
                Dim newRange As New Hashtable
                newRange("@range") = rId
                newRange("@father") = Left(newRg, Len(newRg) - Len(rId) - 1)
                newRange("@relId") = shopId
                newRange("@relType") = 1
                newRange("@rangeTime") = rTime
                myHelper.insertQuery("range_add", myHelper.hashtableToInquery(newRange))
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function check(ByVal shopInfo As Hashtable) As Boolean
        '店铺信息检查
        Dim myMain As New main
        For Each haItem As DictionaryEntry In shopInfo
            If haItem.Key = "@shop_owner" Then
                If (myMain.checkInputMain(haItem.Value, "int") = False) Then
                    Return False
                End If
            Else
                If (myMain.checkInputMain(haItem.Value, "str") = False) Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function
    Public Function check_domain(ByVal domain As Hashtable) As Boolean
        Dim myMain As New main, myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuery("shop_domain_exit", myHelper.hashtableToInquery(domain), "ids")
        If myDs.Tables("ids").Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function hasShop(ByVal uid As Integer) As Boolean
        Dim myMain As New main, myDs As DataSet, myHelper As New dbHelper
        Dim strSql As String = "select * from shops where shop_owner=" & myMain.filterCheck(uid)
        myDs = myHelper.getQuerySql(strSql, "shopInfo")
        Try
            strSql = myDs.Tables("shopInfo").Rows(0)(0)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function hasShop(ByVal uid As Integer, ByVal shopId As Integer) As Boolean
        Dim myMain As New main, myDs As DataSet, myHelper As New dbHelper
        Dim inQuery(0 To 1) As dbHelper.inQuery
        inQuery(0).name = "@shopId"
        inQuery(0).value = shopId
        inQuery(1).name = "@shop_owner"
        inQuery(1).value = uid
        myDs = myHelper.getQuery("shop_has", inQuery, "shopInfo")
        Try
            Dim strSql As String = myDs.Tables("shopInfo").Rows(0)(0)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getShopList(ByVal uid As Integer) As dbHelper.inQuery()
        Dim myMain As New main, myDs As DataSet, myHelper As New dbHelper
        Dim strSql As String = "select id,shop_name from shops where shop_owner=" & myMain.filterCheck(uid)
        myDs = myHelper.getQuerySql(strSql, "shops")
        Dim rt(0 To myDs.Tables("shops").Rows.Count - 1) As dbHelper.inQuery
        For i = 0 To myDs.Tables("shops").Rows.Count - 1
            rt(i).name = myDs.Tables("shops").Rows(i)(1)
            rt(i).value = myDs.Tables("shops").Rows(i)(0)
        Next
        Return rt
    End Function
    Public Function getShopList_new(Optional ByVal number As Integer = 6) As Hashtable()
        Dim strSql As String = "select top " & number & " * from shops order by id DESC"
        Dim myDs As New DataSet, myHelper As New dbHelper
        myDs = myHelper.getQuerySql(strSql, "shops")
        Dim rt(0 To myDs.Tables("shops").Rows.Count - 1) As Hashtable
        Dim iCount As Integer = 0
        For Each tr As DataRow In myDs.Tables("shops").Rows
            rt(iCount) = New Hashtable
            For j = 0 To myDs.Tables("shops").Columns.Count - 1
                rt(iCount).Add(myDs.Tables("shops").Columns(j).ColumnName, tr(j).ToString)
            Next
            iCount += 1
        Next
        Return rt
    End Function
    Public Function getShopInfo(ByVal shopId As Integer) As Hashtable
        Dim rt As New Hashtable, strSql As String = "select * from shops where id=" & shopId, myDs As New DataSet, myHelper As New dbHelper
        Try
            myDs = myHelper.getQuerySql(strSql, "shopInfo")
            For i = 0 To myDs.Tables("shopInfo").Columns.Count - 1
                rt(myDs.Tables("shopInfo").Columns(i).ColumnName) = myDs.Tables("shopInfo").Rows(0)(i)
            Next
            rt("status") = "ok"
            Return rt
        Catch ex As Exception
            rt("status") = "error"
            Return rt
        End Try
    End Function
    Public Function add_shelf(ByVal name As String, ByVal shopId As Integer) As Integer
        Try
            Dim myHelper As New dbHelper, inQuery(0 To 1) As dbHelper.inQuery
            inQuery(0).name = "@shop_id"
            inQuery(0).value = shopId
            inQuery(1).name = "@shelf_name"
            inQuery(1).value = name
            Dim myDs As DataSet
            myDs = myHelper.getQuery("shop_shelf_add", inQuery, "shelfId")
            Return myDs.Tables("shelfId").Rows(0)(0)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function get_shelf_list(ByVal shopId As Integer) As dbHelper.inQuery()
        Dim strSql As String = "select id,shelf_name from shop_shelf where shop_id=" & shopId
        Dim myHelper As New dbHelper
        Dim myDs As DataSet = myHelper.getQuerySql(strSql, "list")
        Dim inQuery(0 To myDs.Tables("list").Rows.Count - 1) As dbHelper.inQuery
        For i = 0 To myDs.Tables("list").Rows.Count - 1
            inQuery(i).name = myDs.Tables("list").Rows(i)(1)
            inQuery(i).value = myDs.Tables("list").Rows(i)(0)
        Next
        Return inQuery
    End Function
    Public Function shelf_dell(ByVal sfId As Integer, ByVal moveTo As Integer) As Boolean
        Dim inQuery(0 To 1) As dbHelper.inQuery
        inQuery(0).name = "@stId"
        inQuery(0).value = sfId
        inQuery(1).name = "@moveTo"
        inQuery(1).value = moveTo
        Dim myHelper As New dbHelper
        Try
            myHelper.noneQuery("shop_shelf_dell", inQuery)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function shop_info_modify(ByVal shopInfo As Hashtable, ByVal range As String) As Boolean
        Dim myHelper As New dbHelper
        Try
            myHelper.noneQuery("shop_info_modify", myHelper.hashtableToInquery(shopInfo))
            If Len(Trim(range)) <> 0 Then
                send_range_add(Trim(range), shopInfo("@shopId"))
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function del_shop(ByVal shopId As Integer) As Boolean
        Dim strSql As String, myHelper As New dbHelper
        Try
            strSql = "delete from send_range where relType=2 and relId in(select id from goods_products where shopId=" & shopId & ")"
            myHelper.querySql(strSql)
            strSql = "delete from goods_products where shopId=" & shopId
            myHelper.querySql(strSql)
            strSql = "delete from shop_shelf where shop_id=" & shopId
            myHelper.querySql(strSql)
            strSql = "delete from shops where id=" & shopId
            myHelper.querySql(strSql)
            strSql = "delete from send_range where relType=1 and relId=" & shopId
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function has_showCase(ByVal shopId As Integer) As Boolean
        Dim strSql As String, myHelper As New dbHelper, myDs As New DataSet
        Dim myMain As New main, strTp As String = "", shopInfo As New Hashtable
        shopInfo = getShopInfo(shopId)
        strSql = "select id from goods_products where shopId=" & shopId & " and goods_tuijian=1"
        myDs = myHelper.getQuerySql(strSql, "count")
        If shopInfo("shop_showcase") >= myDs.Tables("count").Rows.Count Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function get_goodShop(ByVal gid As Integer) As Integer
        Dim strSql As String, myHelper As New dbHelper, myDs As New DataSet
        strSql = "select shopId from goods_products where id=" & gid
        Try
            myDs = myHelper.getQuerySql(strSql, "shop")
            Return myDs.Tables("shop").Rows(0)(0)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function get_liuYan(ByVal gid As Integer) As Hashtable()
        Dim shopId As Integer = get_goodShop(gid), rt() As Hashtable
        If shopId = -1 Then
            ReDim rt(0 To 0)
            rt(0) = New Hashtable
            rt(0)("status") = "error"
            Return rt
        End If
        Dim strSql As String, myHelper As New dbHelper, myDs As New DataSet, liuYanCount As Integer
        strSql = "select top 10 * from comments where shopId=" & shopId
        myDs = myHelper.getQuerySql(strSql, "liuYan")
        liuYanCount = myDs.Tables("liuYan").Rows.Count
        If liuYanCount < 1 Then
            ReDim rt(0 To 0)
            rt(0) = New Hashtable
            rt(0)("status") = "error"
            Return rt
        End If
        Dim myType As New typeHelper
        rt = myType.dsToHash(myDs, "liuYan")
        Return rt
    End Function
    Public Function liuYan_add(ByVal uid As Integer, ByVal comments As String, ByVal shopId As Integer) As Boolean
        Dim liuYan As New Hashtable, myHelper As New dbHelper
        liuYan("@sendBy") = uid
        liuYan("@comments") = comments
        liuYan("@shopId") = shopId
        liuYan("@recomId") = 0
        Try
            myHelper.noneQuery("liuYan_add", myHelper.hashtableToInquery(liuYan))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function lianMengAdd(ByVal shop As Integer, ByVal lianMeng As Integer) As Boolean
        Dim myHelper As New dbHelper, strSql As String = ""
        strSql = "delete from shop_union where shopId=" & shop & " and unionId=" & lianMeng
        Try
            myHelper.querySql(strSql)
            strSql = "insert into shop_union (shopId,unionId) values(" & shop & "," & lianMeng & ")"
            Return myHelper.querySql(strSql)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function lianMengDel(ByVal shop As Integer, ByVal lianMeng As Integer, ByVal uid As Integer) As Boolean
        Dim myHelper As New dbHelper, strSql As String, myShop As New shop
        If myShop.hasShop(uid, shop) = False Then Return False
        strSql = "delete from shop_union where shopId=" & shop & " and unionId=" & lianMeng
        Try
            Return myHelper.querySql(strSql)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function lianMengGet(ByVal shop As Integer) As Hashtable()
        Dim myHelper As New dbHelper, strSql As String = ""
        strSql = "select unionId from shop_union where shopId=" & shop
        Dim myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "ids")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "ids")
    End Function
    Public Function iconAdd(ByVal shop As Integer, ByVal icon As HttpPostedFile, ByVal serverMapPath As String) As Boolean
        'serverPath = Server.MapPath("~")
        Dim myImg As New images
        Dim rt As New Hashtable
        rt = myImg.upLoadImg(icon, "admin@site.com", serverMapPath)
        If rt("status") <> "ok" Then Return False
        Dim myHelper As New dbHelper, strSql As String
        strSql = "update shops set shop_pic='" & rt("smallPath") & "' where id=" & shop
        Try
            Return myHelper.querySql(strSql)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getCertifList(ByVal owonerId As Integer) As Hashtable()
        Dim strSql As String = "select * from shop_certion_apply where shopId in (select id from shops where shop_owner=" & owonerId & ")"
        Dim myHelper As New dbHelper, myDs As New DataSet, myType As New typeHelper
        myDs = myHelper.getQuerySql(strSql, "rt")
        Return myType.dsToHash(myDs, "rt")
    End Function
    Public Function getCeType() As Hashtable()
        Dim strSql As String = "select * from shop_certification_type"
        Dim myHelper As New dbHelper, myDs As New DataSet, myType As New typeHelper
        myDs = myHelper.getQuerySql(strSql, "rt")
        Return myType.dsToHash(myDs, "rt")
    End Function
    Public Function getCe(ByVal ceId As Integer) As Hashtable
        Dim strSql As String = "select * from shop_certification_type where id=" & ceId
        Dim myHelper As New dbHelper, myDs As New DataSet, myType As New typeHelper
        myDs = myHelper.getQuerySql(strSql, "rt")
        Return myType.dsToHashSingle(myDs, "rt")
    End Function
    Public Function hasThisCe(ByVal shopId As Integer, ByVal ceType As Integer) As Boolean
        Dim strSql As String = "select count(*) from shop_certion_apply where shopId=" & shopId & " and apply_type=" & ceType
        Dim myHelper As New dbHelper, myDs As New DataSet, myType As New typeHelper
        myDs = myHelper.getQuerySql(strSql, "count")
        If myDs.Tables("count").Rows(0)(0) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function addCeApply(ByVal shopId As Integer, ByVal ceType As Integer, ByVal ceContent As String) As Boolean
        Dim theCe As New Hashtable
        theCe("@shopId") = shopId
        theCe("@apply_content") = ceContent
        theCe("@apply_type") = ceType
        Dim myHelper As New dbHelper
        Try
            myHelper.noneQuery("shop_ce_apply", myHelper.hashtableToInquery(theCe))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getCeApply(ByVal ceId As Integer) As Hashtable
        Dim strSql As String = "select * from shop_certion_apply where id=" & ceId
        Dim myHelper As New dbHelper, myDs As New DataSet, myType As New typeHelper
        myDs = myHelper.getQuerySql(strSql, "rt")
        Return myType.dsToHashSingle(myDs, "rt")
    End Function
    Public Function addShopCe(ByVal shopId As Integer, ByVal ceId As Integer) As Boolean
        Dim theShop As New Hashtable, strSql As String
        theShop = getShopInfo(shopId)
        If Len(theShop("shop_certification")) = 0 Then
            strSql = ceId
        Else
            strSql = theShop("shop_certification") & "-" & ceId
        End If
        strSql = "update shops set shop_certification='" & strSql & "' where id=" & shopId
        Dim myHelper As New dbHelper
        Try
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
