Public Class orders
    Public Function add(ByVal order As Hashtable) As Integer
        '成功返回订单编号，失败返回0
        Try
            Dim inQuery() As dbHelper.inQuery, myHelper As New dbHelper
            inQuery = myHelper.hashtableToInquery(order)
            Dim myDs As New DataSet
            myDs = myHelper.getQuery("order_add", inQuery, "od")
            Return myDs.Tables("od").Rows(0)(0)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function order_get_byId(ByVal orderId As Integer) As Hashtable
        Dim order As New Hashtable, myHelper As New dbHelper, myDs As New DataSet
        Dim inQuery(0 To 0) As dbHelper.inQuery
        inQuery(0).name = "@id"
        inQuery(0).value = orderId
        myDs = myHelper.getQuery("order_get_byId", inQuery, "order")
        Try
            For i = 0 To myDs.Tables("order").Columns.Count - 1
                order(myDs.Tables("order").Columns(i).ColumnName) = myDs.Tables("order").Rows(0)(i)
            Next
            order("statu") = "ok"
        Catch ex As Exception
            order("statu") = "error"
        End Try
        Return order
    End Function
    Public Function order_get_byBuyer(ByVal userId As Integer, Optional ByVal isComp As Boolean = False, Optional ByVal pange As Integer = 1, Optional ByVal perPage As Integer = 10) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet
        Dim inQuery(0 To 2) As dbHelper.inQuery
        inQuery(0).name = "@uid"
        inQuery(0).value = userId
        inQuery(1).name = "@page"
        inQuery(1).value = pange
        inQuery(2).name = "@prePage"
        inQuery(2).value = perPage
        If isComp = False Then
            myDs = myHelper.getQuery("order_get_buy", inQuery, "orders")
        Else
            myDs = myHelper.getQuery("order_get_buy_complete", inQuery, "orders")
        End If

        Dim rt() As Hashtable
        If myDs.Tables("orders").Rows.Count > 0 Then
            ReDim rt(0 To myDs.Tables("orders").Rows.Count - 1)
        Else
            ReDim rt(0 To 0)
        End If
        rt(0) = New Hashtable
        Dim iCount As Integer = 0
        For Each rowItem As DataRow In myDs.Tables("orders").Rows
            If iCount <> 0 Then rt(iCount) = New Hashtable
            For i = 0 To myDs.Tables("orders").Columns.Count - 1
                rt(iCount)(myDs.Tables("orders").Columns(i).ColumnName) = rowItem(i)
            Next
            iCount += 1
        Next
        ReDim inQuery(0 To 0)
        inQuery(0).name = "@uid"
        inQuery(0).value = userId
        If isComp = False Then
            myDs = myHelper.getQuery("order_count_get_buy", inQuery, "orders")
        Else
            myDs = myHelper.getQuery("order_count_get_buy_compelete", inQuery, "orders")
        End If
        rt(0)("count") = myDs.Tables("orders").Rows(0)(0)
        Return rt
    End Function
    Public Function order_get_byGood(ByVal gid As Integer) As Hashtable()
        Dim strSql As String
        Dim myHelper As New dbHelper, myDs As New DataSet
        strSql = "select * from orders where good=" & gid
        myDs = myHelper.getQuerySql(strSql, "orders")
        Dim rt() As Hashtable
        If myDs.Tables("orders").Rows.Count > 0 Then
            ReDim rt(0 To myDs.Tables("orders").Rows.Count - 1)
        Else
            ReDim rt(0 To 0)
        End If
        rt(0) = New Hashtable
        Dim iCount As Integer = 0
        For Each rowItem As DataRow In myDs.Tables("orders").Rows
            If iCount <> 0 Then rt(iCount) = New Hashtable
            For i = 0 To myDs.Tables("orders").Columns.Count - 1
                rt(iCount)(myDs.Tables("orders").Columns(i).ColumnName) = rowItem(i)
            Next
            iCount += 1
        Next
        rt(0)("count") = myDs.Tables("orders").Rows.Count
        Return rt
    End Function
    Public Function order_get_by_saler(ByVal userId As Integer, Optional ByVal isComp As Boolean = False, Optional ByVal pange As Integer = 1, Optional ByVal perPage As Integer = 10) As Hashtable()
        Dim myHelper As New dbHelper, myDs As New DataSet
        Dim inQuery(0 To 2) As dbHelper.inQuery
        inQuery(0).name = "@uid"
        inQuery(0).value = userId
        inQuery(1).name = "@page"
        inQuery(1).value = pange
        inQuery(2).name = "@prePage"
        inQuery(2).value = perPage
        If isComp = False Then
            myDs = myHelper.getQuery("order_get_sale", inQuery, "orders")
        Else
            myDs = myHelper.getQuery("order_get_sale_complete", inQuery, "orders")
        End If
        Dim rt() As Hashtable
        If myDs.Tables("orders").Rows.Count > 0 Then
            ReDim rt(0 To myDs.Tables("orders").Rows.Count - 1)
        Else
            ReDim rt(0 To 0)
        End If
        rt(0) = New Hashtable
        Dim iCount As Integer = 0
        For Each rowItem As DataRow In myDs.Tables("orders").Rows
            If iCount <> 0 Then rt(iCount) = New Hashtable
            For i = 0 To myDs.Tables("orders").Columns.Count - 1
                rt(iCount)(myDs.Tables("orders").Columns(i).ColumnName) = rowItem(i)
            Next
            iCount += 1
        Next
        ReDim inQuery(0 To 0)
        inQuery(0).name = "@uid"
        inQuery(0).value = userId
        If isComp = False Then
            myDs = myHelper.getQuery("order_count_get_sale", inQuery, "orders")
        Else
            myDs = myHelper.getQuery("order_count_get_sale_compelete", inQuery, "orders")
        End If
        rt(0)("count") = myDs.Tables("orders").Rows(0)(0)
        Return rt
    End Function
    Public Function hasOrder(ByVal orderId As Integer, ByVal uid As Integer) As Boolean
        Dim inQuery(0 To 1) As dbHelper.inQuery
        inQuery(0).name = "@uid"
        inQuery(0).value = uid
        inQuery(1).name = "@oid"
        inQuery(1).value = orderId
        Try
            Dim myDs As New DataSet, myHelper As New dbHelper
            myDs = myHelper.getQuery("order_has", inQuery, "rt")
            If myDs.Tables("rt").Rows(0)(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function order_ok(ByVal uid As Integer, ByVal oid As Integer, ByVal gid As Integer, ByVal pj As Integer, ByVal comments As String) As Boolean
        Dim order As New Hashtable, myHelper As New dbHelper
        order("@uid") = uid
        order("@gid") = gid
        order("@oid") = oid
        order("@sall_pj") = pj
        order("@comments") = comments
        Try
            myHelper.noneQuery("order_ok", myHelper.hashtableToInquery(order))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function order_tousu(ByVal uid As Integer, ByVal oid As Integer, ByVal gid As Integer, ByVal comments As String) As Boolean
        Dim order As New Hashtable, myHelper As New dbHelper
        order("@uid") = uid
        order("@gid") = gid
        order("@oid") = oid
        order("@message") = comments
        Try
            myHelper.noneQuery("order_tousu", myHelper.hashtableToInquery(order))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function order_send(ByVal uid As Integer, ByVal oid As Integer) As Boolean
        Dim order As New Hashtable, myHelper As New dbHelper
        order("@uid") = uid
        order("@oid") = oid
        Try
            myHelper.noneQuery("order_send", myHelper.hashtableToInquery(order))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function neddPj(ByVal uid As Integer) As Boolean
        Dim myHelper As New dbHelper, myDs As New DataSet, inQuery(0 To 0) As dbHelper.inQuery
        inQuery(0).name = "@uid"
        inQuery(0).value = uid
        Try
            myDs = myHelper.getQuery("order_need_pj", inQuery, "orders")
            If myDs.Tables("orders").Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getOrderStatusAsString(ByVal theOrder As Hashtable) As Hashtable
        Dim rt As New Hashtable
        Dim myGood As New goods, good As New Hashtable
        good = myGood.goods_get(theOrder("good"))
        If theOrder("payMent") = 0 Then
            If theOrder("status") = 0 Then
                rt("color") = "style='color:#f98e26;'"
                rt("status") = "[买家已下单，已通知卖家发货]"
            ElseIf theOrder("status") = 1 Then
                rt("color") = "style='color:#a05c35;'"
                rt("status") = "[卖家已发货，等待买家确认]"
            ElseIf theOrder("status") = 2 Then
                rt("color") = "style='color:#ff6600;'"
                rt("status") = "[交易成功]"
            ElseIf theOrder("status") = 3 Then
                rt("color") = ""
                rt("status") = "[投诉调查中]"
            ElseIf theOrder("status") = 4 Then
                rt("color") = ""
                rt("status") = "[投诉已经解决]"
            End If
        Else
            If theOrder("payMent") = 1 Then
                rt("color") = "style='color:#f98e26;'"
                rt("status") = "[等待买家付款]"
            ElseIf theOrder("payMent") = 2 Then
                rt("color") = "style='color:#f98e26;'"
                rt("status") = "[买家已下单付款，已通知卖家发货]"
            ElseIf theOrder("payMent") = 3 Then
                rt("color") = "style='color:#a05c35;'"
                rt("status") = "[卖家已发货，等待买家确认]"
            ElseIf theOrder("payMent") = 4 Then
                rt("color") = "style='color:#ff6600;'"
                rt("status") = "[交易成功]"
            ElseIf theOrder("status") = 3 Then
                rt("color") = ""
                rt("status") = "[投诉调查中]"
            ElseIf theOrder("status") = 4 Then
                rt("color") = ""
                rt("status") = "[投诉已经解决]"
            End If
        End If
        Return rt
    End Function
    Public Function orderStatusChange(ByVal oid As Integer, ByVal orderStatus As Integer, Optional ByVal orderPayment As Integer = 0) As Boolean
        Dim status As New Hashtable, myHelper As New dbHelper
        status("@oid") = oid
        status("@status") = orderStatus
        status("@payMent") = orderPayment
        Try
            myHelper.noneQuery("order_status_change", myHelper.hashtableToInquery(status))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function setPayId(ByVal oid As Integer, ByVal payId As String) As Boolean
        Dim strSql As String, myHelper As New dbHelper
        strSql = "update orders set payId=" & payId & " where id=" & oid
        Try
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function modifyPrice(ByVal oid As Integer, ByVal price As String, ByVal uid As Integer) As Boolean
        Dim inQuery(0 To 1) As dbHelper.inQuery
        inQuery(0).name = "@id"
        inQuery(1).name = "@price"
        inQuery(0).value = oid
        inQuery(1).value = price
        Dim myHelper As New dbHelper
        If order_get_byId(oid)("ownerId") <> uid Then Return False
        Try
            Convert.ToInt16(price)
        Catch ex As Exception
            Return False
        End Try
        Try
            myHelper.noneQuery("order_price_modify", inQuery)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
