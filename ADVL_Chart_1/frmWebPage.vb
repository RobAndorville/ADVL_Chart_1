﻿Imports System.Security.Permissions
<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
<System.Runtime.InteropServices.ComVisibleAttribute(True)>
Public Class frmWebPage
    'This form displays a Web view of the HTML Code.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Public WithEvents XSeq As New ADVL_Utilities_Library_1.XSequence
    'To run and XSequence:
    '  XSeq.RunXSequence(xDoc, Status) 'ImportStatus in Import
    '    Handle events:
    '      XSeq.ErrorMsg
    '      XSeq.Instruction(Info, Locn)

    Private Status As New System.Collections.Specialized.StringCollection

    'Variables used to restore Item values on a web page.
    Private FormName As String
    Private ItemName As String
    Private SelectId As String


    'Private SettingsFileName As String 'The file name used to store the web page settings. 
    'If there is no ParentWebPage, this is FileName & "Settings".
    'If there is a ParentWebPage, this is Filename & "Settings" & ParentWebPageName.GetHashCode


#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    'PROPERTIES:
    'FormNo - the index number of the web page form in WebPageFormList().
    'FileName - the file name of the displayed web document.
    'Description - a description of the displayed web page.
    'FileLocationType - Project or FileSystem - In this application, the web page file will always be located in the Project.
    'FileDirectory - the path of the directory for a file in the FileSystem - Not used in this application.
    'ParentWebPageFileName - 'The file name of the web page that opened this web page.
    'ParentWebPageFormNo - 'The form index number of the web page that opened this web page.

    Private _formNo As Integer = -1 'Multiple instances of this form can be displayed. FormNo is the index number of the form in XmlDisplayFormList.
    'If the form is included in Main.WebViewFormList() then FormNo will be > -1 --> when exiting set Main.ClosedFormNo and call Main.WebViewFormClosed(). 
    Public Property FormNo As Integer
        Get
            Return _formNo
        End Get
        Set(ByVal value As Integer)
            _formNo = value
            Debug.Print("FormNo = " & _formNo)
        End Set
    End Property

    Private _fileName As String = "" 'The file name of the displayed document.
    Public Property FileName As String
        Get
            Return _fileName
        End Get
        Set(value As String)
            _fileName = value
            txtDocumentFile.Text = _fileName 'Display the document filename on the form.
            Me.Text = Main.ApplicationInfo.Name & " - Workflow - " & IO.Path.GetFileNameWithoutExtension(_fileName) 'Update the text at the top of the window.
            RestoreFormSettings() 'Resore the form settings used to display this web page.
        End Set
    End Property

    Private _description As String = "" 'A description of the displayed document.
    Public Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property

    Enum LocationTypes
        Project
        FileSystem
    End Enum

    Private _fileLocationType As LocationTypes = LocationTypes.Project 'The location type of the Document File. (Either the current project or the file system.)
    Property FileLocationType As LocationTypes
        Get
            Return _fileLocationType
        End Get
        Set(value As LocationTypes)
            _fileLocationType = value
        End Set
    End Property

    Private _fileDirectory As String = "" 'The path of the directory containing the current file.
    Property FileDirectory As String
        Get
            Return _fileDirectory
        End Get
        Set(value As String)
            _fileDirectory = value
        End Set
    End Property

    'Private _parentWebPageName As String = "" 'The name of the web page that opened this web page.
    Private _parentWebPageFileName As String = "" 'The file name of the web page that opened this web page.
    'Property ParentWebPageName As String
    Property ParentWebPageFileName As String
        Get
            Return _parentWebPageFileName
        End Get
        Set(value As String)
            _parentWebPageFileName = value
        End Set
    End Property

    Private _parentWebPageFormNo As Integer = -2 'The form index number of the web page that opened this web page.
    Property ParentWebPageFormNo As Integer
        Get
            Return _parentWebPageFormNo
        End Get
        Set(value As Integer)
            _parentWebPageFormNo = value
        End Set
    End Property


#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"

        'NOTE: After a workflow is loaded, Me.Text is changed to AppName - Workflow - WorkflowName
        Dim SettingsFileName As String = "FormSettings_" & Me.Text & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"

        'NOTE: After a workflow is loaded, Me.Text is changed to AppName - Workflow - WorkflowName
        Dim SettingsFileName As String = "FormSettings_" & Me.Text & ".xml"

        If Main.Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Main.Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            'Add code to read other saved setting here:

            CheckFormPos()
        End If
    End Sub

    Private Sub CheckFormPos()
        'Check that the form can be seen on a screen.

        Dim MinWidthVisible As Integer = 192 'Minimum number of X pixels visible. The form will be moved if this many form pixels are not visible.
        Dim MinHeightVisible As Integer = 64 'Minimum number of Y pixels visible. The form will be moved if this many form pixels are not visible.

        Dim FormRect As New Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)
        Dim WARect As Rectangle = Screen.GetWorkingArea(FormRect) 'The Working Area rectangle - the usable area of the screen containing the form.

        ''Check if the top of the form is less than zero:
        'If Me.Top < 0 Then Me.Top = 0

        'Check if the top of the form is above the top of the Working Area:
        If Me.Top < WARect.Top Then
            Me.Top = WARect.Top
        End If

        'Check if the top of the form is too close to the bottom of the Working Area:
        If (Me.Top + MinHeightVisible) > (WARect.Top + WARect.Height) Then
            Me.Top = WARect.Top + WARect.Height - MinHeightVisible
        End If

        'Check if the left edge of the form is too close to the right edge of the Working Area:
        If (Me.Left + MinWidthVisible) > (WARect.Left + WARect.Width) Then
            Me.Left = WARect.Left + WARect.Width - MinWidthVisible
        End If

        'Check if the right edge of the form is too close to the left edge of the Working Area:
        If (Me.Left + Me.Width - MinWidthVisible) < WARect.Left Then
            Me.Left = WARect.Left - Me.Width + MinWidthVisible
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message) 'Save the form settings before the form is minimised:
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Display Methods - Code used to display this form." '============================================================================================================================

    'Private Sub frmTemplate_Load(sender As Object, e As EventArgs) Handles Me.Load
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        'RestoreFormSettings()   'Restore the form settings

        Me.WebBrowser1.ObjectForScripting = Me

        'Add page title:
        'Me.Text = Main.ApplicationInfo.Name & " - Workflow Web Page"
        Me.Text = Main.ApplicationInfo.Name & " - Workflow"
        RestoreFormSettings()   'Restore the form settings
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        SaveWebPageSettings() 'Save the web page settings.

        If FormNo > -1 Then
            Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the RtfDisplayFormClosed method to select the correct form to set to nothing.
        End If

        Me.Close() 'Close the form
    End Sub

    'Private Sub frmTemplate_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub

    'Private Sub frmWebView_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    '    If FormNo > -1 Then
    '        Main.WebViewFormClosed()
    '    End If

    'End Sub

    Private Sub frmWebPage_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.WebPageFormClosed()
        End If
    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    'Private Sub WebBrowser1_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles WebBrowser1.Navigating

    '    Dim Document As System.Windows.Forms.HtmlDocument = WebBrowser1.Document

    '    If Document Is Nothing Then
    '        'Main.Message.AddWarning("WebBrowser1.Document = Nothing." & vbCrLf)
    '    Else
    '        'Document.All.Item(0).Name = 
    '        Dim I As Integer
    '        Dim ItemCount As Integer = Document.All.Count
    '        Main.Message.Add("ItemCount = " & ItemCount & vbCrLf)
    '        'For I = 0 To ItemCount - 1
    '        '    Main.Message.Add("Item( " & I & ") = " & Document.All.Item(I).Name & vbCrLf)

    '        'Next

    '        Main.Message.Add("Link: " & e.Url.ToString & vbCrLf)

    '    End If

    '    'https://msdn.microsoft.com/en-us/library/system.windows.forms.webbrowser.document(v=vs.110).aspx


    'End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        AddHandler WebBrowser1.Document.MouseOver, AddressOf Me.DisplayHyperlinks
    End Sub

    Public Sub DisplayHyperlinks(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs)
        txtLink.Text = e.ToElement.GetAttribute("href")
    End Sub

    Public Sub OpenDocument()
        'Open the document specified by FileName, FileLocationType and FileDirectory.

        If FileLocationType = LocationTypes.Project Then
            'Dim xmlDoc As New System.Xml.XmlDocument
            'Main.Project.ReadXmlDocData(FileName, xmlDoc)
            'XmlHtmDisplay1.Rtf = XmlHtmDisplay1.XmlToRtf(xmlDoc, True) 'Test XmlHtmDisplay1

            'Dim htmText As String
            'Main.Project.ReadData()

            Dim rtbData As New IO.MemoryStream
            Main.Project.ReadData(FileName, rtbData)
            'XmlHtmDisplay1.Clear()
            rtbData.Position = 0
            'XmlHtmDisplay1.LoadFile(rtbData, RichTextBoxStreamType.PlainText)
            Dim sr As New IO.StreamReader(rtbData)

            'WebBrowser1.DocumentText = rtbData.ToString
            WebBrowser1.DocumentText = sr.ReadToEnd()

            'Dim htmText As String = XmlHtmDisplay1.Text

            'XmlHtmDisplay1.Rtf = XmlHtmDisplay1.HmlToRtf(htmText)
            'PlainTextDisplay = False

            'DocTextChanged = False
            'LastFileName = FileName
            'LastFileLocationType = LocationTypes.Project
            'LastFileDirectory = ""
        Else

            'XmlHtmDisplay1.LoadFile(FileDirectory & "\" & FileName, RichTextBoxStreamType.PlainText)
            'DocTextChanged = False
            'LastFileName = FileName
            'LastFileLocationType = LocationTypes.FileSystem
            'LastFileDirectory = FileDirectory

            WebBrowser1.Navigate("file:///" & FileDirectory & "\" & FileName)

        End If

    End Sub

    'Private Sub WebBrowser1_CursorChanged(sender As Object, e As EventArgs) Handles WebBrowser1.CursorChanged
    '    If WebBrowser1.Visible Then
    '        Main.Message.Add("Link: " & e.ToString & vbCrLf)
    '    End If

    'End Sub

#Region " Methods Called by JavaScript - A collection of methods that can be called by JavaScript in a web page shown in WebBrowser1" '========================================================

    Public Sub JSMethodTest1()
        'Test method that is called from JavaScript.
        Main.Message.Add("JSMethodTest1 called OK." & vbCrLf)
    End Sub

    Public Sub JSMethodTest2(ByVal Var1 As String, ByVal Var2 As String)
        'Test method that is called from JavaScript.
        Main.Message.Add("Var1 = " & Var1 & " Var2 = " & Var2 & vbCrLf)
    End Sub

    Public Sub JSDisplayXml(ByRef XDoc As XDocument)
        Main.Message.Add(XDoc.ToString & vbCrLf & vbCrLf)
    End Sub

    'Public Sub JSShowMessage(ByVal Msg As String)
    Public Sub ShowMessage(ByVal Msg As String)
        Main.Message.Add(Msg)
    End Sub

    Public Sub SaveHtmlSettings_Old2(ByVal xSettings As String, ByVal FileName As String)
        'Save the Html settings for a web page.

        'Convert the XSettings to XML format:

        'Dim XmlSettings As New System.Xml.XmlDocument
        Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
        'XmlSettings.LoadXml(XmlHeader & vbCrLf & xSettings)

        Dim XDocSettings As New System.Xml.Linq.XDocument

        Try
            'XDocSettings = System.Xml.Linq.XDocument.Parse(XmlSettings.OuterXml)
            XDocSettings = System.Xml.Linq.XDocument.Parse(XmlHeader & vbCrLf & xSettings)
        Catch ex As Exception
            Main.Message.AddWarning("Error saving HTML settings file. " & ex.Message & vbCrLf)
        End Try

        ' Project.SaveXmlData(FileName, XmlSettings)
        Main.Project.SaveXmlData(FileName, XDocSettings)

    End Sub

    Public Sub SaveHtmlSettings(ByVal xSettings As String)
        'Save the Html settings for a web page.

        Dim SettingsFileName As String = FileName & "Settings"

        'If ParentWebPageName <> "" Then
        If ParentWebPageFileName <> "" Then
            'SettingsFileName = SettingsFileName & ParentWebPageName.GetHashCode
            SettingsFileName = SettingsFileName & ParentWebPageFileName.GetHashCode
        End If

        'Convert the XSettings to XML format:

        'Dim XmlSettings As New System.Xml.XmlDocument
        Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
        'XmlSettings.LoadXml(XmlHeader & vbCrLf & xSettings)

        Dim XDocSettings As New System.Xml.Linq.XDocument

        Try
            'XDocSettings = System.Xml.Linq.XDocument.Parse(XmlSettings.OuterXml)
            XDocSettings = System.Xml.Linq.XDocument.Parse(XmlHeader & vbCrLf & xSettings)
        Catch ex As Exception
            Main.Message.AddWarning("Error saving HTML settings file. " & ex.Message & vbCrLf)
        End Try

        ' Project.SaveXmlData(FileName, XmlSettings)
        'Main.Project.SaveXmlData(FileName, XDocSettings)
        Main.Project.SaveXmlData(SettingsFileName, XDocSettings)

    End Sub

    Public Sub RestoreHtmlSettings_Old2(ByVal FileName As String)
        'Restore the Html settings for a web page.

        Dim XDocSettings As New System.Xml.Linq.XDocument
        Main.Project.ReadXmlData(FileName, XDocSettings)

        If XDocSettings Is Nothing Then
            Main.Message.Add("No HTML Settings file : " & FileName & vbCrLf)
        Else
            'Main.Message.Add("HTML Settings stored in " & FileName & vbCrLf)
            'Main.Message.Add(XDocSettings.ToString & vbCrLf & vbCrLf)

            Dim XSettings As New System.Xml.XmlDocument
            Try
                XSettings.LoadXml(XDocSettings.ToString)

                'Run the Settings file:
                XSeq.RunXSequence(XSettings, Status)
            Catch ex As Exception
                Main.Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try

        End If
    End Sub

    Public Sub RestoreHtmlSettings()
        'Restore the Html settings for a web page.

        Dim SettingsFileName As String = FileName & "Settings"

        'If ParentWebPageName <> "" Then
        If ParentWebPageFileName <> "" Then
            'SettingsFileName = SettingsFileName & ParentWebPageName.GetHashCode
            SettingsFileName = SettingsFileName & ParentWebPageFileName.GetHashCode
        End If

        Dim XDocSettings As New System.Xml.Linq.XDocument
        'Main.Project.ReadXmlData(FileName, XDocSettings)
        Main.Project.ReadXmlData(SettingsFileName, XDocSettings)

        If XDocSettings Is Nothing Then
            'Main.Message.Add("No HTML Settings file : " & FileName & vbCrLf)
            Main.Message.Add("No HTML Settings file : " & SettingsFileName & vbCrLf)
        Else
            'Main.Message.Add("HTML Settings stored in " & FileName & vbCrLf)
            'Main.Message.Add(XDocSettings.ToString & vbCrLf & vbCrLf)

            Dim XSettings As New System.Xml.XmlDocument
            Try
                XSettings.LoadXml(XDocSettings.ToString)

                'Run the Settings file:
                XSeq.RunXSequence(XSettings, Status)
            Catch ex As Exception
                Main.Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try

        End If
    End Sub

    Private Sub XSeq_ErrorMsg(ErrMsg As String) Handles XSeq.ErrorMsg
        Main.Message.AddWarning(ErrMsg & vbCrLf)
    End Sub

    Public Sub RunClipboardXSeq()
        'Run the XSequence instructions in the clipboard.

        Dim XDocSeq As System.Xml.Linq.XDocument
        Try
            XDocSeq = XDocument.Parse(My.Computer.Clipboard.GetText)
        Catch ex As Exception
            Main.Message.AddWarning("Error reading Clipboard data. " & ex.Message & vbCrLf)
            Exit Sub
        End Try

        If IsNothing(XDocSeq) Then
            Main.Message.Add("No XSequence instructions were found in the clipboard.")
            'Exit Sub
        Else
            Dim XmlSeq As New System.Xml.XmlDocument
            Try
                XmlSeq.LoadXml(XDocSeq.ToString) 'Convert XDocSeq to an XmlDocument to process with XSeq.

                'Run the sequence:
                XSeq.RunXSequence(XmlSeq, Status)
            Catch ex As Exception
                Main.Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If

    End Sub


    Private Sub XSeq_Instruction(Data As String, Locn As String) Handles XSeq.Instruction
        'Execute each instruction produced by running the XSeq file.

        Select Case Locn
            Case "Settings:SendData:LatDegrees" 'REDUNDANT!
                RestoreSetting("SendData", "LatDegrees", Data)

            Case "Settings:SendData:LongDegrees" 'REDUNDANT!
                RestoreSetting("SendData", "LongDegrees", Data)

            Case "Settings:Form:Name"
                FormName = Data

            Case "Settings:Form:Item:Name"
                ItemName = Data

            Case "Settings:Form:Item:Value"
                RestoreSetting(FormName, ItemName, Data)

            Case "Settings:Form:SelectId"
                SelectId = Data

            Case "Settings:Form:OptionText"
                'RestoreOption(FormName, SelectId, Data)
                RestoreOption(SelectId, Data)

            Case "Settings"

            Case "EndOfSequence"
                'Main.Message.Add("End of processing sequence" & Data & vbCrLf)

            Case Else
                'Main.Message.AddWarning("Unknown location: " & Locn & "  Data: " & Data & vbCrLf)

                'If the instructions are not saved web page settings identified above, send them directly to the web page:
                XMsgInstruction(Data, Locn) 'The JavaScript function (also called XMsgInstruction) will attempt to process this instruction.

        End Select
    End Sub

    Public Sub RestoreSetting(ByVal FormName As String, ByVal ItemName As String, ByVal ItemValue As String)
        'Restore the setting value with the specified Form Name and Item Name.
        Me.WebBrowser1.Document.InvokeScript("RestoreSetting", New String() {FormName, ItemName, ItemValue})
    End Sub

    'Public Sub RestoreOption(ByVal FormName As String, ByVal SelectId As String, ByVal OptionText As String)
    Public Sub RestoreOption(ByVal SelectId As String, ByVal OptionText As String)
        'Restore the Option text in the Select control with the Id SelectId.
        'Me.WebBrowser1.Document.InvokeScript("RestoreOption", New String() {FormName, SelectId, OptionText})
        Me.WebBrowser1.Document.InvokeScript("RestoreOption", New String() {SelectId, OptionText})
    End Sub

    Private Sub SaveWebPageSettings()
        'Call the SaveSettings JavaScript function:
        Try
            'Me.WebBrowser1.Document.InvokeScript("SaveSettingsXml")
            Me.WebBrowser1.Document.InvokeScript("SaveSettings")
        Catch ex As Exception
            Main.Message.AddWarning("Web page settings not saved: " & ex.Message & vbCrLf)
        End Try

    End Sub

    'Public Sub JSSendXMessage(ByVal XMsg As String, ByVal Destination As String)
    '    'Send an XMessage to the specified destination.
    '    If IsNothing(Main.client) Then
    '        Main.Message.Add("No client connection available!" & vbCrLf)
    '    Else
    '        Main.client.SendMessage(Destination, XMsg)
    '    End If

    'End Sub

    Public Function GetFormNo() As String
        'Return the Form Number of the current instance of the WebView form.
        Return FormNo.ToString
    End Function

    Public Function GetParentFormNo() As String
        'Return the Form Number of the Parent Form (that called this form).
        Return ParentWebPageFormNo.ToString
    End Function

    'Public Function GetAppNetName() As String
    '    'Return the Application Network Name of the application.
    '    Return Main.AppNetName
    'End Function

    Public Function GetProNetName() As String
        'Return the Application Network Name of the application.
        Return Main.ProNetName
    End Function

    Public Sub RunXMessage(ByVal XMsg As String)
        'Run the XMessage by sending it to Main.InstrReceived.
        Main.InstrReceived = XMsg
    End Sub

    Public Function GetConnectionName() As String
        'Return the Connection Name of the Document Library application.
        Return Main.ConnectionName
    End Function

    'Public Sub SendXMessage(ByVal ConnName As String, ByVal XMsg As String)
    'Public Sub SendXMessage(ByVal AppNetName As String, ByVal ConnName As String, ByVal XMsg As String) 'UPDATED 2Feb19
    'Public Sub SendXMessage(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
    '    'Send the XMsg to the application with the connection name ConnName.
    '    If IsNothing(Main.client) Then
    '        Main.Message.Add("No client connection available!" & vbCrLf)
    '    Else
    '        If Main.client.State = ServiceModel.CommunicationState.Faulted Then
    '            Main.Message.Add("client state is faulted. Message not sent!" & vbCrLf)
    '        Else
    '            'Main.client.SendMessageAsync(ConnName, XMsg)
    '            'Main.client.SendMessageAsync(AppNetName, ConnName, XMsg) 'UPDATED 2Feb19
    '            Main.client.SendMessageAsync(ProNetName, ConnName, XMsg)
    '            'Main.Message.XAddText("Message sent to " & ConnName & ":" & vbCrLf, "XmlSentNotice")
    '            'Main.Message.XAddText("Message sent to " & ConnName & " (AppNet: " & AppNetName & ") " & ":" & vbCrLf, "XmlSentNotice") 'UPDATED 2Feb19
    '            'Main.Message.XAddText("Message sent to " & ConnName & " (ProNet: " & ProNetName & ") " & ":" & vbCrLf, "XmlSentNotice")
    '            'Main.Message.XAddText("Message sent to [" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
    '            'Main.Message.XAddXml(XMsg)
    '            'Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
    '            If Main.ShowXMessages Then
    '                Main.Message.XAddText("Message sent to " & "[" & Main.ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
    '                Main.Message.XAddXml(XMsg)
    '                Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
    '            End If
    '        End If
    '    End If

    'End Sub

    Public Sub SendXMessage(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName.
        If IsNothing(Main.client) Then
            Main.Message.Add("No client connection available!" & vbCrLf)
        Else
            If Main.client.State = ServiceModel.CommunicationState.Faulted Then
                Main.Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                If Main.bgwSendMessage.IsBusy Then
                    Main.Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    Dim SendMessageParams As New Main.clsSendMessageParams
                    SendMessageParams.ProjectNetworkName = Main.ProNetName
                    SendMessageParams.ConnectionName = ConnName
                    SendMessageParams.Message = XMsg
                    Main.bgwSendMessage.RunWorkerAsync(SendMessageParams)
                    'Main.Message.XAddText("Message sent to " & "[" & Main.ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                    'Main.Message.XAddXml(XMsg)
                    'Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    If Main.ShowXMessages Then
                        Main.Message.XAddText("Message sent to " & "[" & Main.ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        Main.Message.XAddXml(XMsg)
                        Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SendXMessageExt(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName and Project Network Name ProNetname.
        'This version can send the XMessage to a connection external to the current Project Network.
        If IsNothing(Main.client) Then
            Main.Message.Add("No client connection available!" & vbCrLf)
        Else
            If Main.client.State = ServiceModel.CommunicationState.Faulted Then
                Main.Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                If Main.bgwSendMessage.IsBusy Then
                    Main.Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    Dim SendMessageParams As New Main.clsSendMessageParams
                    SendMessageParams.ProjectNetworkName = ProNetName
                    SendMessageParams.ConnectionName = ConnName
                    SendMessageParams.Message = XMsg
                    Main.bgwSendMessage.RunWorkerAsync(SendMessageParams)
                    'Main.Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                    'Main.Message.XAddXml(XMsg)
                    'Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    If Main.ShowXMessages Then
                        Main.Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        Main.Message.XAddXml(XMsg)
                        Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SendXMessageWait(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName.
        'Wait for the connection to be made.
        If IsNothing(Main.client) Then
            Main.Message.Add("No client connection available!" & vbCrLf)
        Else
            Try
                Application.DoEvents()

                If Main.client.State = ServiceModel.CommunicationState.Faulted Then
                    Main.Message.Add("client state is faulted. Message not sent!" & vbCrLf)
                Else
                    Dim StartTime As Date = Now
                    Dim Duration As TimeSpan
                    'Wait up to 16 seconds for the connection ConnName to be established
                    While Main.client.ConnectionExists(Main.ProNetName, ConnName) = False 'Wait until the required connection is made.
                        System.Threading.Thread.Sleep(1000) 'Pause for 1000ms
                        Duration = Now - StartTime
                        If Duration.Seconds > 16 Then Exit While
                    End While

                    If Main.client.ConnectionExists(Main.ProNetName, ConnName) = False Then
                        Main.Message.AddWarning("Connection not available: " & ConnName & " in application network: " & Main.ProNetName & vbCrLf)
                    Else
                        If Main.bgwSendMessage.IsBusy Then
                            Main.Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                        Else
                            Dim SendMessageParams As New Main.clsSendMessageParams
                            SendMessageParams.ProjectNetworkName = Main.ProNetName
                            SendMessageParams.ConnectionName = ConnName
                            SendMessageParams.Message = XMsg
                            Main.bgwSendMessage.RunWorkerAsync(SendMessageParams)
                            'Main.Message.XAddText("Message sent to " & "[" & Main.ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                            'Main.Message.XAddXml(XMsg)
                            'Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
                            If Main.ShowXMessages Then
                                Main.Message.XAddText("Message sent to " & "[" & Main.ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                                Main.Message.XAddXml(XMsg)
                                Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Main.Message.AddWarning(ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub SendXMessageExtWait(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName and Project Network Name ProNetName.
        'Wait for the connection to be made.
        'This version can send the XMessage to a connection external to the current Project Network.
        If IsNothing(Main.client) Then
            Main.Message.Add("No client connection available!" & vbCrLf)
        Else
            If Main.client.State = ServiceModel.CommunicationState.Faulted Then
                Main.Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                Dim StartTime As Date = Now
                Dim Duration As TimeSpan
                'Wait up to 16 seconds for the connection ConnName to be established
                While Main.client.ConnectionExists(ProNetName, ConnName) = False
                    System.Threading.Thread.Sleep(1000) 'Pause for 1000ms
                    Duration = Now - StartTime
                    If Duration.Seconds > 16 Then Exit While
                End While

                If Main.client.ConnectionExists(ProNetName, ConnName) = False Then
                    Main.Message.AddWarning("Connection not available: " & ConnName & " in application network: " & ProNetName & vbCrLf)
                Else
                    If Main.bgwSendMessage.IsBusy Then
                        Main.Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                    Else
                        Dim SendMessageParams As New Main.clsSendMessageParams
                        SendMessageParams.ProjectNetworkName = ProNetName
                        SendMessageParams.ConnectionName = ConnName
                        SendMessageParams.Message = XMsg
                        Main.bgwSendMessage.RunWorkerAsync(SendMessageParams)
                        'Main.Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        'Main.Message.XAddXml(XMsg)
                        'Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
                        If Main.ShowXMessages Then
                            Main.Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                            Main.Message.XAddXml(XMsg)
                            Main.Message.XAddText(vbCrLf, "Normal") 'Add extra line
                        End If
                    End If
                End If
            End If
        End If
    End Sub



    Public Sub AddText(ByVal Msg As String, ByVal TextType As String)
        Main.Message.AddText(Msg, TextType)
    End Sub

    Public Sub AddMessage(ByVal Msg As String)
        Main.Message.Add(Msg)
    End Sub

    Public Sub AddWarning(ByVal Msg As String)
        Main.Message.AddWarning(Msg)
    End Sub

    Public Sub XMsgInstruction(ByVal Info As String, ByVal Locn As String)
        'Send the XMessage Instruction to the JavaScript function XMsgInstruction for processing.
        Me.WebBrowser1.Document.InvokeScript("XMsgInstruction", New String() {Info, Locn})
    End Sub

    'Public Sub OpenWebPage(ByVal WebPageFileName As String, ByVal ParentWebPageName As String, ByVal ParentWebPageFormNo As Integer)
    Public Sub OpenWebPage(ByVal WebPageFileName As String)
        'Open a Web Page from the WebPageFileName.
        '  Pass the ParentName Property to the new web page. The is the name of this web page that is opening the new page.
        '  Pass the ParentWebPageFormNo Property to the new web page. This is the FormNo of this web page that is opening the new page.
        '    A hash code is generated from the ParentName. This is used to define a file name to save and restore the Web Page settings.
        '    The new web page can pass instructions back to the ParentWebPage using its ParentWebPageFormNo.

        'Dim FormNo As Integer = Main.NewWebView()
        'Dim NewFormNo As Integer = Main.NewWebView()
        Dim NewFormNo As Integer = Main.OpenNewWebPage()

        'Main.WebViewFormList(FormNo).ParentWebPageName = FileName 'Set the Parent Web Page property.
        'Main.WebViewFormList(FormNo).ParentWebPageFormNo = FormNo 'Set the Parent Form Number property.
        'Main.WebViewFormList(FormNo).Description = ""             'The web page description can be blank.
        'Main.WebViewFormList(FormNo).FileDirectory = ""           'Only Web files in the Project directory can be opened from another Web Page Form.
        'Main.WebViewFormList(FormNo).FileLocationType = LocationTypes.Project 'Only Web files in the Project directory can be opened from another Web Page Form.
        'Main.WebViewFormList(FormNo).FileName = WebPageFileName  'Set the web page file name to be opened.
        'Main.WebViewFormList(FormNo).OpenDocument                'Open the web page file name.

        'Main.WebViewFormList(NewFormNo).ParentWebPageName = FileName 'Set the Parent Web Page property.
        'Main.WebPageFormList(NewFormNo).ParentWebPageName = FileName 'Set the Parent Web Page property.
        Main.WebPageFormList(NewFormNo).ParentWebPageFileName = FileName 'Set the Parent Web Page property.
        Main.WebPageFormList(NewFormNo).ParentWebPageFormNo = FormNo 'Set the Parent Form Number property.
        Main.WebPageFormList(NewFormNo).Description = ""             'The web page description can be blank.
        Main.WebPageFormList(NewFormNo).FileDirectory = ""           'Only Web files in the Project directory can be opened from another Web Page Form.
        Main.WebPageFormList(NewFormNo).FileLocationType = LocationTypes.Project 'Only Web files in the Project directory can be opened from another Web Page Form.
        Main.WebPageFormList(NewFormNo).FileName = WebPageFileName  'Set the web page file name to be opened.
        Main.WebPageFormList(NewFormNo).OpenDocument                'Open the web page file name.

    End Sub

    Public Sub ParentProjectName(ByVal FormName As String, ByVal ItemName As String)
        'Return the Parent Project name:
        RestoreSetting(FormName, ItemName, Main.Project.ParentProjectName)
    End Sub

    Public Sub ParentProjectPath(ByVal FormName As String, ByVal ItemName As String)
        'Return the Parent Project path:
        RestoreSetting(FormName, ItemName, Main.Project.ParentProjectPath)
    End Sub

    Public Sub ParentProjectParameterValue(ByVal FormName As String, ByVal ItemName As String, ByVal ParameterName As String)
        'Return the specified Parent Project parameter value:
        RestoreSetting(FormName, ItemName, Main.Project.ParentParameter(ParameterName).Value)
    End Sub

    Public Sub ProjectParameterValue(ByVal FormName As String, ByVal ItemName As String, ByVal ParameterName As String)
        'Return the specified Project parameter value:
        RestoreSetting(FormName, ItemName, Main.Project.Parameter(ParameterName).Value)
    End Sub

    'Public Sub ApplicationNetworkName(ByVal FormName As String, ByVal ItemName As String)
    '    'Return the name of the Application Network:
    '    RestoreSetting(FormName, ItemName, Main.Project.Parameter("AppNetName").Value)
    'End Sub

    Public Sub ProjectNetworkName(ByVal FormName As String, ByVal ItemName As String)
        'Return the name of the Project Network:
        RestoreSetting(FormName, ItemName, Main.Project.Parameter("ProNetName").Value)
    End Sub

    'Public Sub OpenProjectAtAppNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
    Public Sub OpenProjectAtProNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Open the Project at the specified Path (relative to the ProNet Path) using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            'If Main.Project.ParameterExists("AppNetPath") Then
            If Main.Project.ParameterExists("ProNetPath") Then
                'ProjectPath = Main.Project.GetParameter("AppNetPath") & RelativePath
                ProjectPath = Main.Project.GetParameter("ProNetPath") & RelativePath
                Main.client.StartProjectAtPath(ProjectPath, ConnectionName)
            Else
                'Main.Message.AddWarning("The Application Network Path is not known." & vbCrLf)
                Main.Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        Else
            'If Main.Project.ParameterExists("AppNetPath") Then
            If Main.Project.ParameterExists("ProNetPath") Then
                'ProjectPath = Main.Project.GetParameter("AppNetPath") & "\" & RelativePath
                ProjectPath = Main.Project.GetParameter("ProNetPath") & "\" & RelativePath
                Main.client.StartProjectAtPath(ProjectPath, ConnectionName)
            Else
                'Main.Message.AddWarning("The Application Network Path is not known." & vbCrLf)
                Main.Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        End If
    End Sub

    'Public Sub CheckOpenProjectAtAppNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
    Public Sub CheckOpenProjectAtProNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Check if the project at the specified Path (relative to the ProNet Path) is open.
        'Open it if it is not already open.
        'Open the Project at the specified Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            'If Main.Project.ParameterExists("AppNetPath") Then
            If Main.Project.ParameterExists("ProNetPath") Then
                'ProjectPath = Main.Project.GetParameter("AppNetPath") & RelativePath
                ProjectPath = Main.Project.GetParameter("ProNetPath") & RelativePath
                Main.client.StartProjectAtPath(ProjectPath, ConnectionName)
                If Main.client.ProjectOpen(ProjectPath) Then
                    'Project is already open.
                Else
                    Main.client.StartProjectAtPath(ProjectPath, ConnectionName)
                End If
            Else
                'Main.Message.AddWarning("The Application Network Path is not known." & vbCrLf)
                Main.Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        Else
            'If Main.Project.ParameterExists("AppNetPath") Then
            If Main.Project.ParameterExists("ProNetPath") Then
                'ProjectPath = Main.Project.GetParameter("AppNetPath") & "\" & RelativePath
                ProjectPath = Main.Project.GetParameter("ProNetPath") & "\" & RelativePath
                Main.client.StartProjectAtPath(ProjectPath, ConnectionName)
                If Main.client.ProjectOpen(ProjectPath) Then
                    'Project is already open.
                Else
                    Main.client.StartProjectAtPath(ProjectPath, ConnectionName)
                End If
            Else
                'Main.Message.AddWarning("The Application Network Path is not known." & vbCrLf)
                Main.Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        'Edit the html file.

        Dim FileName As String
        FileName = txtDocumentFile.Text

        If FileName = "" Then
            Main.Message.AddWarning("No page selected." & vbCrLf)
        Else
            Dim FormNo As Integer = Main.OpenNewHtmlDisplayPage()
            Main.HtmlDisplayFormList(FormNo).FileName = FileName
            Main.HtmlDisplayFormList(FormNo).OpenDocument
        End If
    End Sub


#End Region 'Methods Called by JavaScript -----------------------------------------------------------------------------------------------------------------------------------------------------

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class