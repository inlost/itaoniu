Imports System.Net
Imports System.IO

Public Class uCenter_pc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim act As String = Request.Params("action")
        Select Case act
            Case "getVideo"
                getVideo()
            Case "new_text"
                newText()
            Case "new_images"
                newImage()
            Case "new_music"
                newMusic()
            Case "new_link"
                newLink()
            Case "new_video"
                new_video()
            Case Else
                Response.Write("Good Luck~")
        End Select
    End Sub
    Public Sub getVideo()
        Dim strAdderss As String = Request.Form("uri")
        Dim theRequest As WebRequest = WebRequest.Create(strAdderss)
        Dim theResponse As WebResponse = theRequest.GetResponse()
        Dim reader As New StreamReader(theResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"))
        Dim strResponse As String = reader.ReadToEnd()
        reader.Close()
        reader.Dispose()
        theResponse.Close()
        Dim myVideo As New video, rtHa As New Hashtable
        rtHa = myVideo.getVideoInfo(strAdderss, strResponse)
        Dim myType As New typeHelper
        Response.Write(IIf(Request.Params("callBack") = Nothing, myType.hashToJosion(rtHa), Request.Params("callBack") & "(" & myType.hashToJosion(rtHa) & ");"))
    End Sub
    Public Sub newText()
        Dim myCenter As New pCenter, theText As New Hashtable
        If Session("onTheWay") <> "loginSuccess" Then Response.Redirect("../personalCenter/login.aspx")
        theText("@uid") = Session("id")
        theText("@see") = Request.Form("privacy-select")
        theText("@short") = Request.Form("pb-text-textarea")
        theText("@oldPost") = Nothing
        theText("@sourceId") = Nothing
        theText("@relyId") = Nothing
        If Len(Trim(Request.Form("title"))) = 0 Then
            theText("@title") = Nothing
        Else
            theText("@title") = Trim(Request.Form("title"))
        End If
        If Request.Files.Count = 0 Then
            theText("@pic") = Nothing
        Else
            Try
                Dim myImg As New images
                theText("@pic") = myImg.upLoadImg(Request.Files("imge"), Session("email"), Server.MapPath("~"))("smallPath")
            Catch ex As Exception
                theText("@pic") = Nothing
            End Try
        End If
        Dim rt As Integer = myCenter.new_text(theText)
        If rt <> 0 Then
            Response.Redirect("../personalCenter/default.aspx?at=pc&id=" & rt)
        Else
            Response.Redirect("../personalCenter/default.aspx?at=pc")
        End If
    End Sub
    Public Sub newImage()
        Dim myCenter As New pCenter, theText As New Hashtable
        If Session("onTheWay") <> "loginSuccess" Then Response.Redirect("../personalCenter/login.aspx")
        theText("@uid") = Session("id")
        theText("@see") = Request.Form("privacy-select")
        theText("@oldPost") = Nothing
        theText("@sourceId") = Nothing
        theText("@relyId") = Nothing
        If Len(Trim(Request.Form("title"))) = 0 Then
            theText("@title") = Nothing
        Else
            theText("@title") = Trim(Request.Form("title"))
        End If
        If Request.Files.Count = 0 Then
            Response.Redirect("../personalCenter/new_img.aspx?at=pc")
        Else
            Try
                Dim myImg As New images
                theText("@pic") = myImg.upLoadImg(Request.Files("images"), Session("email"), Server.MapPath("~"))("smallPath")
                theText("@short") = "<img src='" & theText("@pic") & "' />"
            Catch ex As Exception
                Response.Redirect("../personalCenter/new_img.aspx?at=pc")
            End Try
        End If
        Dim rt As Integer = myCenter.new_image(theText)
        If rt <> 0 Then
            Response.Redirect("../personalCenter/default.aspx?at=pc&id=" & rt)
        Else
            Response.Redirect("../personalCenter/default.aspx?at=pc")
        End If
    End Sub
    Public Sub newMusic()
        Dim myCenter As New pCenter, theText As New Hashtable
        If Session("onTheWay") <> "loginSuccess" Then Response.Redirect("../personalCenter/login.aspx")
        theText("@uid") = Session("id")
        theText("@see") = Request.Form("privacy-select")
        theText("@oldPost") = Nothing
        theText("@sourceId") = Nothing
        theText("@relyId") = Nothing
        If Len(Trim(Request.Form("m_title"))) = 0 Then
            Response.Redirect("../personalCenter/new_music.aspx?at=pc")
        Else
            theText("@title") = Trim(Request.Form("m_title"))
        End If
        theText("@pic") = Request.Form("m_img")
        theText("@short") = Request.Form("m_palyer") & "<br/>" & Request.Form("pb-text-textarea")
        Dim rt As Integer = myCenter.new_music(theText)
        If rt <> 0 Then
            Response.Redirect("../personalCenter/default.aspx?at=pc&id=" & rt)
        Else
            Response.Redirect("../personalCenter/default.aspx?at=pc")
        End If
    End Sub
    Public Sub newLink()
        Dim myCenter As New pCenter, theText As New Hashtable
        If Session("onTheWay") <> "loginSuccess" Then Response.Redirect("../personalCenter/login.aspx")
        theText("@uid") = Session("id")
        theText("@see") = Request.Form("privacy-select")
        theText("@oldPost") = Nothing
        theText("@sourceId") = Nothing
        theText("@relyId") = Nothing
        If Len(Trim(Request.Form("pb-link-title-input"))) = 0 Then
            theText("@title") = Nothing
        Else
            theText("@title") = Trim(Request.Form("pb-link-title-input"))
        End If
        If Len(Trim(Request.Form("link_address"))) = 0 Then
            Response.Redirect("../personalCenter/new_link.aspx?at=pc")
        Else
            theText("@short") = Trim(Request.Form("link_address"))
        End If
        theText("@pic") = Nothing
        Dim rt As Integer = myCenter.new_link(theText)
        If rt <> 0 Then
            Response.Redirect("../personalCenter/default.aspx?at=pc&id=" & rt)
        Else
            Response.Redirect("../personalCenter/default.aspx?at=pc")
        End If
    End Sub
    Public Sub new_video()
        Dim myCenter As New pCenter, theText As New Hashtable
        If Session("onTheWay") <> "loginSuccess" Then Response.Redirect("../personalCenter/login.aspx")
        theText("@uid") = Session("id")
        theText("@see") = Request.Form("privacy-select")
        theText("@oldPost") = Nothing
        theText("@sourceId") = Nothing
        theText("@relyId") = Nothing
        If Len(Trim(Request.Form("m_title"))) = 0 Then
            Response.Redirect("../personalCenter/new_video.aspx?at=pc")
        Else
            theText("@title") = Trim(Request.Form("m_title"))
        End If
        theText("@pic") = Request.Form("m_img")
        theText("@short") = Request.Form("m_palyer") & "<br/>" & Request.Form("pb-text-textarea")
        Dim rt As Integer = myCenter.new_video(theText)
        If rt <> 0 Then
            Response.Redirect("../personalCenter/default.aspx?at=pc&id=" & rt)
        Else
            Response.Redirect("../personalCenter/default.aspx?at=pc")
        End If
    End Sub
End Class