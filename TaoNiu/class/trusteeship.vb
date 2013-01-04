Public Class trusteeships
    Public Function getTypeList() As Hashtable()
        Dim strSql As String = "select * from trusteeship_type"
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "rt")
    End Function
    Public Function getTuList(ByVal uid As Integer) As Hashtable()
        Dim strSql As String = "select * from trusteeship_list where shopId in (select id from shops where shop_owner=" & uid & ")"
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "rt")
    End Function
    Public Function getTypeListAsSting() As String
        Dim theType As Hashtable() = getTypeList(), strRt As String = ""
        If theType(0)("count") = 0 Then Return ""
        For Each item As Hashtable In theType
            strRt += "<option value='" & item("id") & "'>" & item("typeName") & "</option>"
        Next
        Return strRt
    End Function
    Public Function getMember() As Hashtable()
        Dim strSql As String = "select * from trusteeship"
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHash(myDs, "rt")
    End Function
    Public Function getTheMember(ByVal memberId As Integer) As Hashtable
        Dim strSql As String = "select * from trusteeship where id=" & memberId
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHashSingle(myDs, "rt")
    End Function
    Public Function getTu(ByVal id As Integer) As Hashtable
        Dim strSql As String = "select * from trusteeship_list where id=" & id
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHashSingle(myDs, "rt")
    End Function
    Public Function getTuType(ByVal id As Integer) As Hashtable
        Dim strSql As String = "select * from trusteeship_type where id=" & id
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        Return myType.dsToHashSingle(myDs, "rt")
    End Function
    Public Function hasTu(ByVal shopId As Integer) As Boolean
        Dim strSql As String = "select id from trusteeship_list where shopId=" & shopId
        Dim myHelper As New dbHelper, myDs As New DataSet
        myDs = myHelper.getQuerySql(strSql, "rt")
        Dim myType As New typeHelper
        If myType.dsToHash(myDs, "rt")(0)("count") = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function addTu(ByVal shopId As Integer, ByVal type As Integer, ByVal memberId As Integer) As Boolean
        Dim myUser As New user, theMember As New Hashtable, theTutype As New Hashtable
        theTutype = getTuType(type)
        Try
            theMember = getTheMember(memberId)
            Dim theMemberId As Integer = myUser.getUserInfoByName(theMember("taoniuId"))("uid")
            Dim theTu As New Hashtable
            theTu("@shopId") = shopId
            theTu("@endDate") = DateTime.Now.AddMonths(1)
            theTu("@startTime") = theTutype("startTime")
            theTu("@endTime") = theTutype("endTime")
            theTu("@memberId") = theMemberId
            theTu("@memberQQ") = theMember("qq")
            Dim myHeper As New dbHelper
            myHeper.noneQuery("trusteeship_list_add", myHeper.hashtableToInquery(theTu))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function delTu(ByVal id As Integer) As Boolean
        Dim strSql As String = "delete from trusteeship_list where id=" & id
        Dim myHelper As New dbHelper
        Try
            myHelper.querySql(strSql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function isTuUser(ByVal uid As Integer, ByVal shopId As Integer) As Boolean
        If hasTu(shopId) Then
            Dim strSql As String = "select count(*) from trusteeship_list where shopId=" & shopId & " and memberId=" & uid
            Dim myHelper As New dbHelper, myDs As New DataSet
            myDs = myHelper.getQuerySql(strSql, "rt")
            If myDs.Tables("rt").Rows(0)(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Function getOnTimeQQ(ByVal shopId As Integer) As String
        If hasTu(shopId) = False Then Return ""
        Dim strSql As String = "select * from trusteeship_list where shopId=" & shopId
        Dim theTu As New Hashtable, theType As New typeHelper, myDs As New DataSet, myHelper As New dbHelper
        myDs = myHelper.getQuerySql(strSql, "rt")
        theTu = theType.dsToHashSingle(myDs, "rt")
        If theTu("endDate") < DateTime.Now Then Return ""
        Dim startTime As Integer = theTu("startTime")
        Dim endTime As Integer = theTu("endTime")
        While endTime > 0
            If endTime < startTime Then Exit While
            If endTime = DateTime.Now.Hour Then Return theTu("memberQQ")
            If endTime - 1 < 0 Then
                endTime = 24
            Else
                endTime -= 1
            End If
        End While
        Return ""
    End Function
End Class
