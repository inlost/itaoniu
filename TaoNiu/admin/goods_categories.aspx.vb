Public Class goods_categories
    Inherits System.Web.UI.Page
    Dim myMain As New main
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myGoods As New goods
        Dim inQuery() As dbHelper.inQuery
        inQuery = myGoods.cat_get(1)
        setListBox(ListBox1, inQuery)
        setListBox(DropDownList1, inQuery)
        inQuery = myGoods.cat_get(2)
        setListBox(DropDownList2, inQuery)
    End Sub

    Protected Sub addCat1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addCat1.Click
        Dim strCat As String = myMain.filterCheck(TextBox1.Text.ToString)
        If strCat <> "" And strCat <> Nothing Then
            Dim myGoods As New goods, strRt As String
            strRt = myGoods.cat_add(strCat, 1)
            If strRt = "error" Then
                Response.Write("<script>alert('与服务器交换数据时发生错误！')</script>")
            Else
                Dim myListItem As New ListItem
                myListItem.Value = strRt
                myListItem.Text = strCat
                ListBox1.Items.Add(myListItem)
                DropDownList1.Items.Add(myListItem)
                TextBox1.Text = Nothing
            End If
        End If
    End Sub

    Protected Sub addCat2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addCat2.Click
        Try
            If ListBox1.SelectedValue > 0 Then
                Dim strCat As String = myMain.filterCheck(TextBox2.Text.ToString)
                If strCat <> "" And strCat <> Nothing Then
                    Dim myGoods As New goods, strRt As String
                    strRt = myGoods.cat_add(strCat, 2, ListBox1.SelectedValue)
                    If strRt = "error" Then
                        Response.Write("<script>alert('与服务器交换数据时发生错误！')</script>")
                    Else
                        Dim myListItem As New ListItem
                        myListItem.Value = strRt
                        myListItem.Text = strCat
                        ListBox2.Items.Add(myListItem)
                        DropDownList2.Items.Add(myListItem)
                        TextBox2.Text = Nothing
                    End If
                End If
            Else
                Response.Write("<script>alert('请选择上级分类！')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('请选择上级分类！')</script>")
        End Try
    End Sub

    Protected Sub addCat3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addCat3.Click
        Try
            If ListBox2.SelectedValue > 0 Then
                Dim strCat As String = myMain.filterCheck(TextBox3.Text.ToString)
                If strCat <> "" And strCat <> Nothing Then
                    Dim myGoods As New goods, strRt As String
                    strRt = myGoods.cat_add(strCat, 3, ListBox2.SelectedValue)
                    If strRt = "error" Then
                        Response.Write("<script>alert('与服务器交换数据时发生错误！')</script>")
                    Else
                        Dim myListItem As New ListItem
                        myListItem.Value = strRt
                        myListItem.Text = strCat
                        ListBox3.Items.Add(myListItem)
                        TextBox3.Text = Nothing
                    End If
                End If
            Else
                Response.Write("<script>alert('请选择上级分类！')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('请选择上级分类！')</script>")
        End Try
    End Sub
    Protected Sub setListBox(ByVal sListBox As ListBox, ByVal inQuery() As dbHelper.inQuery)
        If sListBox.Items.Count = 0 Then
            For i = 0 To inQuery.Length - 1
                Dim itemAdd As New ListItem
                itemAdd.Value = inQuery(i).value
                itemAdd.Text = inQuery(i).name
                sListBox.Items.Add(itemAdd)
            Next
        End If
    End Sub
    Protected Sub setListBox(ByVal sDropListBox As DropDownList, ByVal inQuery() As dbHelper.inQuery)
        If sDropListBox.Items.Count = 0 Then
            For i = 0 To inQuery.Length - 1
                Dim itemAdd As New ListItem
                itemAdd.Value = inQuery(i).value
                itemAdd.Text = inQuery(i).name
                sDropListBox.Items.Add(itemAdd)
            Next
            sDropListBox.Items.Add("删除下级目录")
        End If
    End Sub

    Protected Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim myGoods As New goods
        Dim inQuery() As dbHelper.inQuery
        inQuery = myGoods.cat_get(2, ListBox1.SelectedValue)
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        setListBox(ListBox2, inQuery)
    End Sub

    Protected Sub ListBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox2.SelectedIndexChanged
        Dim myGoods As New goods
        Dim inQuery() As dbHelper.inQuery
        inQuery = myGoods.cat_get(3, ListBox2.SelectedValue)
        ListBox3.Items.Clear()
        setListBox(ListBox3, inQuery)
    End Sub

    Protected Sub removeCat1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeCat1.Click
        Try
            If ListBox1.SelectedValue > 0 Then
                Dim myGoods As New goods
                If DropDownList1.SelectedItem.Text = "删除下级目录" Then
                    If myGoods.cat_remove(ListBox1.SelectedValue) Then
                        Response.Redirect("goods_categories.aspx")
                    Else
                        Response.Write("<script>alert('与服务器交换数据时出错！')</script>")
                    End If
                Else
                    If myGoods.cat_remove(ListBox1.SelectedValue, DropDownList1.SelectedValue) Then
                        Response.Redirect("goods_categories.aspx")
                    Else
                        Response.Write("<script>alert('与服务器交换数据时出错！')</script>")
                    End If
                End If
            Else
                Response.Write("<script>alert('没有选择分类！')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('没有选择分类！')</script>")
        End Try
    End Sub

    Protected Sub removeCat2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeCat2.Click
        Try
            If ListBox2.SelectedValue > 0 Then
                Dim myGoods As New goods
                If DropDownList2.SelectedItem.Text = "删除下级目录" Then
                    If myGoods.cat_remove(ListBox2.SelectedValue) Then
                        Response.Redirect("goods_categories.aspx")
                    Else
                        Response.Write("<script>alert('与服务器交换数据时出错！')</script>")
                    End If
                Else
                    If myGoods.cat_remove(ListBox2.SelectedValue, DropDownList2.SelectedValue) Then
                        Response.Redirect("goods_categories.aspx")
                    Else
                        Response.Write("<script>alert('与服务器交换数据时出错！')</script>")
                    End If
                End If
            Else
                Response.Write("<script>alert('没有选择分类！')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('没有选择分类！')</script>")
        End Try
    End Sub

    Protected Sub removeCat3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeCat3.Click
        Try
            If ListBox3.SelectedValue > 0 Then
                Dim myGoods As New goods
                If myGoods.cat_remove(ListBox3.SelectedValue) Then
                    Response.Redirect("goods_categories.aspx")
                Else
                    Response.Write("<script>alert('与服务器交换数据时出错！')</script>")
                End If
            Else
                Response.Write("<script>alert('没有选择分类！')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('没有选择分类！')</script>")
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        catUp(ListBox1)
    End Sub
    Protected Sub catUp(ByVal listBo As ListBox)
        If listBo.SelectedIndex < 0 Then Exit Sub
        If listBo.SelectedIndex = 0 Then
            Response.Write("<script>alert('不能再向上移动了！')</script>")
        Else
            Dim strSql As String, up As Integer, down As Integer
            up = listBo.SelectedValue
            down = listBo.Items(listBo.SelectedIndex - 1).Value
            strSql = "update goods_cat set orderby=-8 where orderby=" & up
            Dim myHelper As New dbHelper
            myHelper.querySql(strSql)
            strSql = "update goods_cat set orderby=" & up & " where orderby=" & down
            myHelper.querySql(strSql)
            strSql = "update goods_cat set orderby=" & down & " where orderby=-8"
            myHelper.querySql(strSql)
            Response.Redirect("goods_categories.aspx")
        End If
    End Sub
    Protected Sub catDown(ByVal listBo As ListBox)
        If listBo.SelectedIndex < 0 Then Exit Sub
        If listBo.SelectedIndex = listBo.Items.Count - 1 Then
            Response.Write("<script>alert('不能再向下移动了！')</script>")
        Else
            Dim strSql As String, up As Integer, down As Integer
            up = listBo.Items(listBo.SelectedIndex + 1).Value
            down = listBo.SelectedValue
            strSql = "update goods_cat set orderby=-8 where orderby=" & up
            Dim myHelper As New dbHelper
            myHelper.querySql(strSql)
            strSql = "update goods_cat set orderby=" & up & " where orderby=" & down
            myHelper.querySql(strSql)
            strSql = "update goods_cat set orderby=" & down & " where orderby=-8"
            myHelper.querySql(strSql)
            Response.Redirect("goods_categories.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        catDown(ListBox1)
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        catUp(ListBox2)
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        catDown(ListBox2)
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        catUp(ListBox3)
    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button6.Click
        catDown(ListBox3)
    End Sub
End Class