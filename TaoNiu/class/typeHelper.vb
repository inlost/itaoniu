Public Class typeHelper
    Public Function dsToHash(ByVal myDs As DataSet, ByVal tName As String) As Hashtable()
        Dim rt() As Hashtable
        Dim rCount As Integer = myDs.Tables(tName).Rows.Count
        Dim cCount As Integer = myDs.Tables(tName).Columns.Count
        If rCount < 1 Then
            ReDim rt(0 To 0)
            rt(0) = New Hashtable
            rt(0)("count") = 0
            Return rt
        End If
        ReDim rt(0 To rCount - 1)
        For i = 0 To rCount - 1
            rt(i) = New Hashtable
            For j = 0 To cCount - 1
                rt(i)(myDs.Tables(tName).Columns(j).ColumnName) = myDs.Tables(tName).Rows(i)(j)
            Next
        Next
        rt(0)("count") = rCount
        Return rt
    End Function
    Public Function dsToHashSingle(ByVal myDs As DataSet, ByVal tName As String) As Hashtable
        Dim rt As New Hashtable
        Dim rCount As Integer = myDs.Tables(tName).Rows.Count
        Dim cCount As Integer = myDs.Tables(tName).Columns.Count
        If rCount < 1 Then
            rt("status") = "noData"
            Return rt
        End If
            For j = 0 To cCount - 1
            rt(myDs.Tables(tName).Columns(j).ColumnName) = myDs.Tables(tName).Rows(0)(j)
            Next
        rt("status") = "success"
        Return rt
    End Function
    Public Function hashToJosion(ByVal ha As Hashtable) As String
        Dim strRt As String = "{"
        For Each i As DictionaryEntry In ha
            strRt += i.Key & ":""" & i.Value & ""","
        Next
        strRt = Left(strRt, Len(strRt) - 1)
        strRt += "}"
        Return strRt
    End Function
End Class
