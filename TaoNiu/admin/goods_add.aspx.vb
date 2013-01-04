Public Class goods_add
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        For i = 2010 To 2099
            sale_year.Items.Add(i)
        Next
        For i = 1 To 12
            sale_month.Items.Add(i)
        Next
        For i = 1 To 31
            sale_day.Items.Add(i)
        Next
        For i = 1 To 24
            If i < 10 Then
                sale_hour.Items.Add("0" & i)
            Else
                sale_hour.Items.Add(i)
            End If
        Next
        For i = 0 To 59
            If i < 10 Then
                sale_min.Items.Add("0" & i)
            Else
                sale_min.Items.Add(i)
            End If
        Next
        For i = 1 To 60
            goods_warranty_time.Items.Add(i)
        Next
    End Sub
    Protected Sub info_add_Click(ByVal sender As Object, ByVal e As EventArgs) Handles info_add.Click
        If (goods_info_add.Text.ToLower().IndexOf("=") < 0) Then
            Response.Write("<script>alert('属性格式不正确，正确格式应为：属性=值')</script>")
        Else
            goods_info_list.Items.Add(goods_info_add.Text)
            goods_info_add.Text = Nothing
        End If
    End Sub

    Protected Sub btPub_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPub.Click
        Dim myProduct As New Hashtable, strTp As String = "", myMain As New main, tableRst As New Hashtable, myGoods As New goods
        If goods_picture.HasFile = False Then
            Response.Write("<script>alert('请选择商品图片')</script>")
            Exit Sub
        End If
        myProduct("@shopId") = shopId.Text
        myProduct("@ownerId") = ownerId.Text
        myProduct("@goods_cat") = cat1.SelectedValue & "-" & cat2.SelectedValue & "-" & cat3.SelectedValue
        myProduct("@goods_type ") = goods_type.SelectedValue
        myProduct("@goods_sale_type") = goods_sale_type.SelectedValue
        myProduct("@goods_title") = goods_title.Text
        Try
            Dim myImg As New images
            myProduct("@goods_picture") = myImg.imgUpload(goods_picture, Server.MapPath("~"), ownerId.Text)(1)
        Catch ex As Exception
            myProduct("@goods_picture") = ""
        End Try
        myProduct("@goods_prize") = goods_prize.Text
        myProduct("@goods_discount") = discount1.SelectedValue * 10 + discount2.SelectedValue

        For i = 0 To goods_info_list.Items.Count - 1
            If i = 0 Then
                strTp += goods_info_list.Items(i).Text
            Else
                strTp += "-" & goods_info_list.Items(i).Text
            End If
        Next
        myProduct("@goods_info") = strTp

        myProduct("@goods_introduce") = goods_introduce.Text
        myProduct("@goods_number") = goods_number.Text

        If goods_on_sale_date.SelectedValue = "now" Then
            myProduct("@goods_on_sale_date") = DateTime.Now.ToString
        Else
            myProduct("@goods_on_sale_date") = sale_year.SelectedValue & "/" & sale_month.SelectedValue & "/" & sale_day.SelectedValue & " " _
                & sale_hour.SelectedValue & ":" & sale_min.SelectedValue & ":00"
        End If

        myProduct("@goods_tuijian") = goods_tuijian.SelectedValue
        myProduct("@goods_range") = goods_range.Text
        myProduct("@goods_period") = Convert.ToDateTime(myProduct("@goods_on_sale_date")).AddDays(goods_Period.SelectedValue)
        myProduct("@goods_invoice") = goods_invoice.SelectedValue
        myProduct("@goods_warranty_type") = goods_warranty_type.SelectedValue
        myProduct("@goods_warranty_time") = goods_warranty_time.SelectedValue
        myProduct("@goods_warranty_other") = goods_warranty_other.Text
        myProduct("@shelf") = 0
        tableRst = myGoods.checkProduct(myProduct)
        Dim o As Object = tableRst("result")
        If o.ToString = False Then
            o = tableRst("message")
            Response.Write("<script>alert('" & o.ToString & "')</script>")
        Else
            If myGoods.goods_add(myProduct) Then
                Response.Write("<script>alert('商品添加成功')</script>")
            Else
                Response.Write("<script>alert('商品添加失败')</script>")
            End If
        End If
    End Sub
End Class