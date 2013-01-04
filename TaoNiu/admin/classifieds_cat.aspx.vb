Public Class classifieds_cat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DropDownList1.Items.Add("直接删除")
    End Sub

    Protected Sub addCat1_Click(sender As Object, e As EventArgs) Handles addCat1.Click
        Dim myClassified As New classifieds, theCat As New Hashtable
        theCat("@catName") = TextBox1.Text
        theCat("@catDeep") = 1
        theCat("@catFather") = -1
        theCat("@catImg") = 0
        theCat("@catOrder") = 0
        Dim catId As Integer = myClassified.cat_add(theCat)
        If catId = -1 Then Exit Sub
        myClassified.cat_order_set(catId, catId)
        Response.Redirect("classifieds_cat.aspx")
    End Sub

    Protected Sub addCat2_Click(sender As Object, e As EventArgs) Handles addCat2.Click
        If ListBox1.SelectedIndex > -1 Then
            Dim myClassified As New classifieds, theCat As New Hashtable
            theCat("@catName") = TextBox2.Text
            theCat("@catDeep") = 2
            theCat("@catFather") = ListBox1.SelectedValue
            theCat("@catImg") = 0
            theCat("@catOrder") = 0
            Dim catId As Integer = myClassified.cat_add(theCat)
            If catId = -1 Then Exit Sub
            myClassified.cat_order_set(catId, catId)
            Response.Redirect("classifieds_cat.aspx")
        End If
    End Sub
End Class