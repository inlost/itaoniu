Public Class carts
    Private myHelper As New dbHelper
    '购物车
    Public Function getGoods(ByVal goodsList As String) As Hashtable()
        If Len(goodsList) = 0 Then
            Dim rt(0 To 0) As Hashtable
            rt(0) = New Hashtable
            rt(0)("count") = 0
            Return rt
        End If
        Dim gIdList As String(), separator As Char() = "-"
        gIdList = goodsList.Split(separator)
        Dim rtGoods(0 To gIdList.Length - 1) As Hashtable
        Dim myGoods As New goods
        separator = "^"
        For i = 0 To gIdList.Length - 1
            rtGoods(i) = New Hashtable
            rtGoods(i) = myGoods.goods_get(gIdList(i).Split(separator)(0))
            rtGoods(i)("number") = gIdList(i).Split(separator)(1)
        Next
        rtGoods(0)("count") = gIdList.Length
        Return rtGoods
    End Function
    Public Function changeNumber(ByVal goodsList As String, ByVal mid As String, ByVal nember As String) As String
        Dim gIdList As String(), separator As Char() = "-", strNew As String = ""
        gIdList = goodsList.Split(separator)
        separator = "^"
        For i = 0 To gIdList.Length - 1
            If gIdList(i).Split(separator)(0) = mid Then
                gIdList(i) = gIdList(i).Split(separator)(0) & "^" & nember
            End If
            strNew += IIf(Len(strNew) = 0, gIdList(i), "-" + gIdList(i))
        Next
        Return strNew
    End Function
    Public Function del(ByVal goodsList As String, ByVal dId As String) As String
        Dim gIdList As String(), separator As Char() = "-", strNew As String = ""
        gIdList = goodsList.Split(separator)
        separator = "^"
        For i = 0 To gIdList.Length - 1
            If gIdList(i).Split(separator)(0) <> dId Then
                strNew += IIf(Len(strNew) = 0, gIdList(i), "-" + gIdList(i))
            End If
        Next
        Return strNew
    End Function
End Class
