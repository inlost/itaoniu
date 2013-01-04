Public Class header
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Request.Params("place")
            Case "home"
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='style/main.css'/>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='Scripts/jquery-1.4.1.js'></script>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='Scripts/main.js'></script>")
            Case "mall"
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/main.css'/>")
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/mall.css' />")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/jquery-1.4.1.js'></script>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/main.js'></script>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/mall.js'></script>")
            Case "admin"
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/admin.css'/>")
            Case "center"
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='style/main.css'/>")
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='style/user_center.css'/>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='Scripts/jquery-1.4.1.js'></script>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='Scripts/main.js'></script>")
            Case "pCenter"
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/main.css'/>")
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/user_center.css'/>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/jquery-1.4.1.js'></script>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/main.js'></script>")
            Case "agent"
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/main.css'/>")
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/user_center.css'/>")
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/agent.css'/>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/jquery-1.4.1.js'></script>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/main.js'></script>")
            Case "classified"
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/main.css'/>")
                Response.Write(vbCrLf & "<link type='text/css' rel='stylesheet' href='../style/classified.css'/>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/jquery-1.4.1.js'></script>")
                Response.Write(vbCrLf & "<script language='javascript' type='text/javascript' src='../Scripts/main.js'></script>")
        End Select
    End Sub

End Class