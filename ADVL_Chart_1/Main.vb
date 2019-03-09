'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
'
'Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at
'
'http://www.apache.org/licenses/LICENSE-2.0
'
'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
''WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'See the License for the specific language governing permissions and
'limitations under the License.
'
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Imports System.Windows.Forms.DataVisualization 'Add reference Assemblies, Framework, System.Windows.Forms.DataVisualization
Imports System.Security.Permissions
<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
<System.Runtime.InteropServices.ComVisibleAttribute(True)>
Public Class Main
    'The ADVL_Chart produces a variety of charts from a range of input data sources.

#Region " Coding Notes - Notes on the code used in this class." '------------------------------------------------------------------------------------------------------------------------------

    'ADD THE SYSTEM UTILITIES REFERENCE: ==========================================================================================
    'The following references are required by this software: 
    'Project \ Add Reference... \ ADVL_Utilities_Library_1.dll
    'The Utilities Library is used for Project Management, Archive file management, running XSequence files and running XMessage files.
    'If there are problems with a reference, try deleting it from the references list and adding it again.

    'ADD THE SERVICE REFERENCE: ===================================================================================================
    'A service reference to the Message Service must be added to the source code before this service can be used.
    'This is used to connect to the Application Network.

    'Adding the service reference to a project that includes the WcfMsgServiceLib project: -----------------------------------------
    'Project \ Add Service Reference
    'Press the Discover button.
    'Expand the items in the Services window and select IMsgService.
    'Press OK.
    '------------------------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------------------
    'Adding the service reference to other projects that dont include the WcfMsgServiceLib project: -------------------------------
    'Run the ADVL_Application_Network_1 application to start the Application Network message service.
    'In Microsoft Visual Studio select: Project \ Add Service Reference
    'Enter the address: http://localhost:8733/ADVLService
    'Press the Go button.
    'MsgService is found.
    'Press OK to add ServiceReference1 to the project.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE MsgServiceCallback CODE: =============================================================================================
    'This is used to connect to the Application Network.
    'In Microsoft Visual Studio select: Project \ Add Class
    'MsgServiceCallback.vb
    'Add the following code to the class:
    'Imports System.ServiceModel
    'Public Class MsgServiceCallback
    '    Implements ServiceReference1.IMsgServiceCallback
    '    Public Sub OnSendMessage(message As String) Implements ServiceReference1.IMsgServiceCallback.OnSendMessage
    '        'A message has been received.
    '        'Set the InstrReceived property value to the message (usually in XMessage format). This will also apply the instructions in the XMessage.
    '        Main.InstrReceived = message
    '    End Sub
    'End Class
    '------------------------------------------------------------------------------------------------------------------------------

#End Region 'Coding Notes ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Variable Declarations - All the variables and class objects used in this form and this application." '-------------------------------------------------------------------------------------------------

    Public WithEvents ApplicationInfo As New ADVL_Utilities_Library_1.ApplicationInfo 'This object is used to store application information.
    Public WithEvents Project As New ADVL_Utilities_Library_1.Project 'This object is used to store Project information.
    Public WithEvents Message As New ADVL_Utilities_Library_1.Message 'This object is used to display messages in the Messages window.
    Public WithEvents ApplicationUsage As New ADVL_Utilities_Library_1.Usage 'This object stores application usage information.

    'Declare Forms used by the application:
    Public WithEvents DesignQuery As frmDesignQuery
    Public WithEvents ViewDatabaseData As frmViewDatabaseData
    Public WithEvents Chart As frmChart 'Form to display a new chart
    Public ChartList As New ArrayList 'Used for displaying multiple Chart forms.

    Public WithEvents WebPageList As frmWebPageList

    Public WithEvents NewHtmlDisplay As frmHtmlDisplay
    Public HtmlDisplayFormList As New ArrayList 'Used for displaying multiple HtmlDisplay forms.

    Public WithEvents NewWebPage As frmWebPage
    Public WebPageFormList As New ArrayList 'Used for displaying multiple WebView forms.


    'Declare objects used to connect to the Application Network:
    Public client As ServiceReference1.MsgServiceClient
    Public WithEvents XMsg As New ADVL_Utilities_Library_1.XMessage
    Dim XDoc As New System.Xml.XmlDocument
    Public Status As New System.Collections.Specialized.StringCollection
    Dim ClientAppNetName As String = "" 'The name of thge client Application Network requesting service. ADDED 2Feb19.
    Dim ClientAppName As String = "" 'The name of the client requesting service
    Dim ClientConnName As String = "" 'The name of the client connection requesting service
    'Dim ClientAppLocn As String = "" 'The location in the Client application requesting service
    Dim MessageXDoc As System.Xml.Linq.XDocument
    Dim xmessage As XElement 'This will contain the message. It will be added to MessageXDoc.
    Dim xlocns As New List(Of XElement) 'A list of locations. Each location forms part of the reply message. The information in the reply message will be sent to the specified location in the client application.
    Dim MessageText As String = "" 'The text of a message sent through the Application Network.

    'Dim ConnectionName As String = "" 'The name of the connection used to connect this application to the ComNet.
    Public ConnectionName As String = "" 'The name of the connection used to connect this application to the ComNet.
    Public AppNetName As String = "" 'Added 2Feb19

    Public MsgServiceAppPath As String = "" 'The application path of the Message Service application (ComNet). This is where the "Application.Lock" file will be while ComNet is running
    Public MsgServiceExePath As String = "" 'The executable path of the Message Service.


    'Dataset used to hold points for plotting:
    Public ds As New DataSet

    Dim cboFieldSelections As New DataGridViewComboBoxColumn 'Used for selecting Y Value fields in the Chart Settings tab

    'Other properties
    'Public StockChart As clsStockChartProperties = New clsStockChartProperties 'Stock Chart properties
    Public StockChart As New StockChart
    'Public ChartLabel As clsChartLabelProperties = New clsChartLabelProperties 'Chart label properties
    'Public XAxis As clsAxisProperties = New clsAxisProperties
    'Public YAxis As clsAxisProperties = New clsAxisProperties
    'Public PointChart As clsPointChartProperties = New clsPointChartProperties 'Point Chart properties
    Public PointChart As New PointChart

    'New Chart variables:
    'A New Chart is created by instructions passed through the Application Network.
    Public NewStockChart As StockChart
    Public NewPointChart As PointChart
    Public NewInputDataType As String = "Database" 'Database or Dataset
    Public NewInputDatabasePath As String = ""     'The path to the database containing the input data for the New Chart.
    Public NewInputQuery As String = ""            'The SQL query used to extract the data used in the New Chart.
    Public NewChartType As DataVisualization.Charting.SeriesChartType 'The chart type of the New Chart

    Public LastUsedCharts As New Dictionary(Of String, String) 'Stores the last used chart file name for each chart type. eg. dictLastUsedCharts("StockChart") returns "ANZ Prices.StockChart"

    Dim WithEvents Zip As ADVL_Utilities_Library_1.ZipComp

    'Main.Load variables:
    Dim ProjectSelected As Boolean = False 'If True, a project has been selected using Command Arguments. Used in Main.Load.
    Dim StartupConnectionName As String = "" 'If not "" the application will be connected to the AppNet using this connection name in  Main.Load.


    'The following variables are used to run JavaScript in Web Pages loaded into the Document View: -------------------
    Public WithEvents XSeq As New ADVL_Utilities_Library_1.XSequence
    'To run an XSequence:
    '  XSeq.RunXSequence(xDoc, Status) 'ImportStatus in Import
    '    Handle events:
    '      XSeq.ErrorMsg
    '      XSeq.Instruction(Info, Locn)

    Private XStatus As New System.Collections.Specialized.StringCollection

    'Variables used to restore Item values on a web page.
    Private FormName As String
    Private ItemName As String
    Private SelectId As String

    'StartProject variables:
    Private StartProject_AppName As String  'The application name
    Private StartProject_ConnName As String 'The connection name
    Private StartProject_ProjID As String   'The project ID





#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Properties - All the properties used in this form and this application" '------------------------------------------------------------------------------------------------------------

    Private _connectionHashcode As Integer 'The Application Network connection hashcode. This is used to identify a connection in the Application Netowrk when reconnecting.
    Property ConnectionHashcode As Integer
        Get
            Return _connectionHashcode
        End Get
        Set(value As Integer)
            _connectionHashcode = value
        End Set
    End Property


    'Private _connectedToAppNet As Boolean = False  'True if the application is connected to the Application Network.
    Private _connectedToComNet As Boolean = False  'True if the application is connected to the Communication Network (Message Service).
    'Property ConnectedToAppnet As Boolean
    Property ConnectedToComNet As Boolean
        Get
            Return _connectedToComNet
        End Get
        Set(value As Boolean)
            _connectedToComNet = value
        End Set
    End Property

    'Private _instrReceived As String = "" 'Contains Instructions received from the Application Network message service.
    'Property InstrReceived As String
    '    Get
    '        Return _instrReceived
    '    End Get
    '    Set(value As String)
    '        If value = Nothing Then
    '            Message.Add("Empty message received!")
    '        Else
    '            _instrReceived = value

    '            'Add the message to the XMessages window:
    '            Message.XAddText("Message received: " & vbCrLf, "XmlReceivedNotice")
    '            If _instrReceived.StartsWith("<XMsg>") Then 'This is an XMessage set of instructions.
    '                Try
    '                    'Inititalise the reply message:
    '                    Dim Decl As New XDeclaration("1.0", "utf-8", "yes")
    '                    MessageXDoc = New XDocument(Decl, Nothing) 'Reply message - this will be sent to the Client App.
    '                    xmessage = New XElement("XMsg")
    '                    xlocns.Add(New XElement("Main")) 'Initially set the location in the Client App to Main.

    '                    'Run the received message:
    '                    Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
    '                    XDoc.LoadXml(XmlHeader & vbCrLf & _instrReceived)


    '                    Message.XAddXml(XDoc)
    '                    'Message.XAddText(vbCrLf, "Message") 'Add extra line
    '                    Message.XAddText(vbCrLf, "Normal") 'Add extra line

    '                    XMsg.Run(XDoc, Status)
    '                Catch ex As Exception
    '                    Message.Add("Error running XMsg: " & ex.Message & vbCrLf)
    '                End Try

    '                'XMessage has been run.
    '                'Reply to this message:
    '                'Add the message reply to the XMessages window:
    '                'Complete the MessageXDoc:
    '                xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the last location reply instructions to the message.
    '                MessageXDoc.Add(xmessage)
    '                MessageText = MessageXDoc.ToString

    '                If ClientAppName = "" Then
    '                    'No client to send a message to!
    '                Else
    '                    'Message.Color = Color.Red
    '                    'Message.FontStyle = FontStyle.Bold
    '                    'Message.XAdd("Message sent to " & ClientAppName & ":" & vbCrLf)
    '                    'Message.SetNormalStyle()
    '                    'Message.XAdd(MessageText & vbCrLf & vbCrLf)
    '                    Message.XAddText("Message sent to " & ClientAppName & ":" & vbCrLf, "XmlSentNotice")
    '                    'Message.XAddText(MessageText & vbCrLf & vbCrLf, "Message")
    '                    Message.XAddXml(MessageText)
    '                    'Message.XAddText(vbCrLf, "Message") 'Add extra line
    '                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
    '                    'SendMessage sends the contents of MessageText to MessageDest.
    '                    SendMessage() 'This subroutine triggers the timer to send the message after a short delay.
    '                End If
    '            Else

    '            End If
    '        End If

    '    End Set
    'End Property

    Private _instrReceived As String = "" 'Contains Instructions received from the Application Network message service.
    Property InstrReceived As String
        Get
            Return _instrReceived
        End Get
        Set(value As String)
            If value = Nothing Then
                Message.Add("Empty message received!")
            Else
                _instrReceived = value
                ProcessInstructions(_instrReceived)
            End If
        End Set
    End Property

    Private Sub ProcessInstructions(ByVal Instructions As String)
        'Process the XMessage instructions.

        'Add the message header to the XMessages window:
        Message.XAddText("Message received: " & vbCrLf, "XmlReceivedNotice")
        If Instructions.StartsWith("<XMsg>") Then 'This is an XMessage set of instructions.
            Try
                'Inititalise the reply message:
                Dim Decl As New XDeclaration("1.0", "utf-8", "yes")
                MessageXDoc = New XDocument(Decl, Nothing) 'Reply message - this will be sent to the Client App.
                xmessage = New XElement("XMsg")
                xlocns.Add(New XElement("Main")) 'Initially set the location in the Client App to Main.

                'Run the received message:
                Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
                XDoc.LoadXml(XmlHeader & vbCrLf & Instructions.Replace("&", "&amp;")) 'Replace "&" with "&amp:" before loading the XML text.
                Message.XAddXml(XDoc)  'Add the message to the XMessages window.
                Message.XAddText(vbCrLf, "Normal") 'Add extra line
                XMsg.Run(XDoc, Status)
            Catch ex As Exception
                Message.Add("Error running XMsg: " & ex.Message & vbCrLf)
            End Try

            'XMessage has been run.
            'Reply to this message:
            'Add the message reply to the XMessages window:
            'Complete the MessageXDoc:
            xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the last location reply instructions to the message.
            MessageXDoc.Add(xmessage)
            MessageText = MessageXDoc.ToString

            If ClientConnName = "" Then
                'No client to send a message to!
            Else
                Message.XAddText("Message sent to " & ClientConnName & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(MessageText)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line
                SendMessage() 'This subroutine triggers the timer to send the message after a short delay.
            End If
        Else 'This is not an XMessage!
            Message.XAddText("The message is not an XMessage: " & Instructions & vbCrLf, "Normal")
        End If
    End Sub

    Private _closedFormNo As Integer 'Temporarily holds the number of the form that is being closed. 
    Property ClosedFormNo As Integer
        Get
            Return _closedFormNo
        End Get
        Set(value As Integer)
            _closedFormNo = value
        End Set
    End Property


    'CHART SETTINGS -----------------------------------------------------------------------------------------------------------------------------------

    Private _inputDataType As String = "Database" 'Database or Dataset
    Property InputDataType As String
        Get
            Return _inputDataType
        End Get
        Set(value As String)
            _inputDataType = value
        End Set
    End Property

    Private _inputDatabaseDirectory As String = "" 'The directory of the Input Database. When the Find database button is pressed, the Open File Dialog will open in this directory.
    Property InputDatabaseDirectory As String
        Get
            Return _inputDatabaseDirectory
        End Get
        Set(value As String)
            _inputDatabaseDirectory = value
        End Set
    End Property

    Private _inputDatabasePath As String = ""
    Property InputDatabasePath As String
        Get
            Return _inputDatabasePath
        End Get
        Set(value As String)
            _inputDatabasePath = value
            txtDatabasePath.Text = _inputDatabasePath
            FillLstTables()
        End Set
    End Property

    Private _inputQuery As String = ""
    Property InputQuery As String
        Get
            Return _inputQuery
        End Get
        Set(value As String)
            _inputQuery = value
            txtInputQuery.Text = _inputQuery
            ApplyQuery()
        End Set
    End Property

    Private _inputDataDescr As String = "" 'A description of the data selected for charting.
    Property InputDataDescr As String
        Get
            Return _inputDataDescr
        End Get
        Set(value As String)
            _inputDataDescr = value
            txtDataDescription.Text = _inputDataDescr
        End Set
    End Property

    Private _chartType As DataVisualization.Charting.SeriesChartType = Charting.SeriesChartType.Bar
    'Area (13) Bar (7) BoxPlot (28) Bubble (2) Candlestick (20) Column (10) Doughnut (18) ErrorBar (27) FastLine (6) FastPoint (1) Funnel (33) Kagi (31) Line (3) Pie (17) 
    'Point (0) PointAndFigure (32) Polar (26) Pyramid (34) Radar (25) Range (21) RangeBar (23) RangeColumn (24) Renko (29) Spline (4) SplineArea (14) SplineRange (22) 
    'StackedArea (15) StackedArea100 (16) StackedBar (8) StackedBar100 (9) StackedColumn (11) StackedColumn100 (12) StepLine (5) Stock (19) ThreeLineBreak (30)

    Property ChartType As DataVisualization.Charting.SeriesChartType
        Get
            Return _chartType
        End Get
        Set(value As DataVisualization.Charting.SeriesChartType)
            _chartType = value
            txtChartType.Text = _chartType.ToString
            cmbChartType.SelectedIndex = cmbChartType.FindStringExact(_chartType.ToString)
        End Set
    End Property

    Private _chartWindow As String = "Preview" '(Preview or New Window) Chart can be drawn in the Preview window or a New Window.
    Property ChartWindow As String
        Get
            Return _chartWindow
        End Get
        Set(value As String)
            _chartWindow = value
            If _chartWindow = "Preview" Then
                rbPreviewChart.Checked = True
            ElseIf _chartWindow = "New Window" Then
                rbNewWindowChart.Checked = True
            End If
        End Set
    End Property

    Private _startPageFileName As String = "" 'The file name of the html document displayed in the Start Page tab.
    Public Property StartPageFileName As String
        Get
            Return _startPageFileName
        End Get
        Set(value As String)
            _startPageFileName = value
        End Set
    End Property

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML Files - Read and write XML files." '-------------------------------------------------------------------------------------------------------------------------------------

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Form settings for Main form.-->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <MsgServiceAppPath><%= MsgServiceAppPath %></MsgServiceAppPath>
                               <MsgServiceExePath><%= MsgServiceExePath %></MsgServiceExePath>
                               <!---->
                               <SelectedTabIndex><%= TabControl1.SelectedIndex %></SelectedTabIndex>
                               <InputDataType><%= InputDataType %></InputDataType>
                               <InputDatabasePath><%= InputDatabasePath %></InputDatabasePath>
                               <InputDatabaseDirectory><%= InputDatabaseDirectory %></InputDatabaseDirectory>
                               <InputDataDescription><%= InputDataDescr %></InputDataDescription>
                               <InputQuery><%= InputQuery %></InputQuery>
                               <ChartType><%= ChartType.ToString %></ChartType>
                               <ChartWindow><%= ChartWindow %></ChartWindow>
                               <AutoDraw><%= chkAutoDraw.Checked %></AutoDraw>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & ApplicationInfo.Name & "_" & Me.Text & ".xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            If Settings.<FormSettings>.<MsgServiceAppPath>.Value <> Nothing Then MsgServiceAppPath = Settings.<FormSettings>.<MsgServiceAppPath>.Value
            If Settings.<FormSettings>.<MsgServiceExePath>.Value <> Nothing Then MsgServiceExePath = Settings.<FormSettings>.<MsgServiceExePath>.Value



            'Add code to read other saved setting here:
            If Settings.<FormSettings>.<SelectedTabIndex>.Value <> Nothing Then TabControl1.SelectedIndex = Settings.<FormSettings>.<SelectedTabIndex>.Value

            If Settings.<FormSettings>.<InputDataType>.Value <> Nothing Then
                InputDataType = Settings.<FormSettings>.<InputDataType>.Value
                If InputDataType = "Database" Then
                    rbDatabase.Checked = True
                ElseIf InputDataType = "Dataset" Then
                    rbDataset.Checked = True
                End If
            End If

            If Settings.<FormSettings>.<InputDatabasePath>.Value <> Nothing Then
                InputDatabasePath = Settings.<FormSettings>.<InputDatabasePath>.Value
                txtDatabasePath.Text = InputDatabasePath
            End If

            If Settings.<FormSettings>.<InputDatabaseDirectory>.Value <> Nothing Then
                InputDatabaseDirectory = Settings.<FormSettings>.<InputDatabaseDirectory>.Value
            End If

            If Settings.<FormSettings>.<InputQuery>.Value <> Nothing Then InputQuery = Settings.<FormSettings>.<InputQuery>.Value
            If Settings.<FormSettings>.<InputDataDescription>.Value <> Nothing Then InputDataDescr = Settings.<FormSettings>.<InputDataDescription>.Value

            If Settings.<FormSettings>.<ChartType>.Value <> Nothing Then
                ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), Settings.<FormSettings>.<ChartType>.Value)
            End If
            If Settings.<FormSettings>.<ChartWindow>.Value <> Nothing Then ChartWindow = Settings.<FormSettings>.<ChartWindow>.Value
            If Settings.<FormSettings>.<AutoDraw>.Value <> Nothing Then chkAutoDraw.Checked = Settings.<FormSettings>.<AutoDraw>.Value
        End If
    End Sub

    Private Sub ReadApplicationInfo()
        'Read the Application Information.

        If ApplicationInfo.FileExists Then
            ApplicationInfo.ReadFile()
        Else
            'There is no Application_Info.xml file.
            DefaultAppProperties() 'Create a new Application Info file with default application properties:
        End If
    End Sub

    Private Sub DefaultAppProperties()
        'These properties will be saved in the Application_Info.xml file in the application directory.
        'If this file is deleted, it will be re-created using these default application properties.

        'Change this to show your application Name, Description and Creation Date.
        ApplicationInfo.Name = "ADVL_Chart_1"

        'ApplicationInfo.ApplicationDir is set when the application is started.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath

        ApplicationInfo.Description = "Andorville™ charting application."
        ApplicationInfo.CreationDate = "4-Dec-2016 12:00:00"

        'Author -----------------------------------------------------------------------------------------------------------
        'Change this to show your Name, Description and Contact information.
        ApplicationInfo.Author.Name = "Signalworks Pty Ltd"
        ApplicationInfo.Author.Description = "Signalworks Pty Ltd" & vbCrLf &
            "Australian Proprietary Company" & vbCrLf &
            "ABN 26 066 681 598" & vbCrLf &
            "Registration Date 05/10/1994"

        ApplicationInfo.Author.Contact = "http://www.andorville.com.au/"

        'File Associations: -----------------------------------------------------------------------------------------------
        'Add any file associations here.
        'The file extension and a description of files that can be opened by this application are specified.
        'The example below specifies a coordinate system parameter file type with the file extension .ADVLCoord.
        'Dim Assn1 As New ADVL_System_Utilities.FileAssociation
        'Assn1.Extension = "ADVLCoord"
        'Assn1.Description = "Andorville™ software coordinate system parameter file"
        'ApplicationInfo.FileAssociations.Add(Assn1)

        'Version ----------------------------------------------------------------------------------------------------------
        ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision

        'Copyright --------------------------------------------------------------------------------------------------------
        'Add your copyright information here.
        ApplicationInfo.Copyright.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.Copyright.PublicationYear = "2016"

        'Trademarks -------------------------------------------------------------------------------------------------------
        'Add your trademark information here.
        Dim Trademark1 As New ADVL_Utilities_Library_1.Trademark
        Trademark1.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark1.Text = "Andorville"
        Trademark1.Registered = False
        Trademark1.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark1)
        Dim Trademark2 As New ADVL_Utilities_Library_1.Trademark
        Trademark2.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark2.Text = "AL-H7"
        Trademark2.Registered = False
        Trademark2.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark2)

        'License -------------------------------------------------------------------------------------------------------
        'Add your license information here.
        ApplicationInfo.License.CopyrightOwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.License.PublicationYear = "2016"

        'License Links:
        'http://choosealicense.com/
        'http://www.apache.org/licenses/
        'http://opensource.org/

        'Apache License 2.0 ---------------------------------------------
        ApplicationInfo.License.Code = ADVL_Utilities_Library_1.License.Codes.Apache_License_2_0
        ApplicationInfo.License.Notice = ApplicationInfo.License.ApacheLicenseNotice 'Get the pre-defined Aapche license notice.
        ApplicationInfo.License.Text = ApplicationInfo.License.ApacheLicenseText     'Get the pre-defined Apache license text.

        'Code to use other pre-defined license types is shown below:

        'GNU General Public License, version 3 --------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.GNU_GPL_V3_0
        'ApplicationInfo.License.Notice = 'Add the License Notice to ADVL_Utilities_Library_1 License class.
        'ApplicationInfo.License.Text = 'Add the License Text to ADVL_Utilities_Library_1 License class.

        'The MIT License ------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.MIT_License
        'ApplicationInfo.License.Notice = ApplicationInfo.License.MITLicenseNotice
        'ApplicationInfo.License.Text = ApplicationInfo.License.MITLicenseText

        'No License Specified -------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.None
        'ApplicationInfo.License.Notice = ""
        'ApplicationInfo.License.Text = ""

        'The Unlicense --------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.The_Unlicense
        'ApplicationInfo.License.Notice = ApplicationInfo.License.UnLicenseNotice
        'ApplicationInfo.License.Text = ApplicationInfo.License.UnLicenseText

        'Unknown License ------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.Unknown
        'ApplicationInfo.License.Notice = ""
        'ApplicationInfo.License.Text = ""

        'Source Code: --------------------------------------------------------------------------------------------------
        'Add your source code information here if required.
        'THIS SECTION WILL BE UPDATED TO ALLOW A GITHUB LINK.
        ApplicationInfo.SourceCode.Language = "Visual Basic 2015"
        ApplicationInfo.SourceCode.FileName = ""
        ApplicationInfo.SourceCode.FileSize = 0
        ApplicationInfo.SourceCode.FileHash = ""
        ApplicationInfo.SourceCode.WebLink = ""
        ApplicationInfo.SourceCode.Contact = ""
        ApplicationInfo.SourceCode.Comments = ""

        'ModificationSummary: -----------------------------------------------------------------------------------------
        'Add any source code modification here is required.
        ApplicationInfo.ModificationSummary.BaseCodeName = ""
        ApplicationInfo.ModificationSummary.BaseCodeDescription = ""
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Major = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Minor = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Build = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Revision = 0
        ApplicationInfo.ModificationSummary.Description = "This is the first released version of the application. No earlier base code used."

        'Library List: ------------------------------------------------------------------------------------------------
        'Add the ADVL_Utilties_Library_1 library:
        Dim NewLib As New ADVL_Utilities_Library_1.LibrarySummary
        NewLib.Name = "ADVL_System_Utilities"
        NewLib.Description = "System Utility classes used in Andorville™ software development system applications"
        NewLib.CreationDate = "7-Jan-2016 12:00:00"
        NewLib.LicenseNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598" & vbCrLf &
                               vbCrLf &
                               "Licensed under the Apache License, Version 2.0 (the ""License"");" & vbCrLf &
                               "you may not use this file except in compliance with the License." & vbCrLf &
                               "You may obtain a copy of the License at" & vbCrLf &
                               vbCrLf &
                               "http://www.apache.org/licenses/LICENSE-2.0" & vbCrLf &
                               vbCrLf &
                               "Unless required by applicable law or agreed to in writing, software" & vbCrLf &
                               "distributed under the License is distributed on an ""AS IS"" BASIS," & vbCrLf &
                               "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied." & vbCrLf &
                               "See the License for the specific language governing permissions and" & vbCrLf &
                               "limitations under the License." & vbCrLf

        NewLib.CopyrightNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598"

        NewLib.Version.Major = 1
        NewLib.Version.Minor = 0
        NewLib.Version.Build = 1
        NewLib.Version.Revision = 0

        NewLib.Author.Name = "Signalworks Pty Ltd"
        NewLib.Author.Description = "Signalworks Pty Ltd" & vbCrLf &
            "Australian Proprietary Company" & vbCrLf &
            "ABN 26 066 681 598" & vbCrLf &
            "Registration Date 05/10/1994"

        NewLib.Author.Contact = "http://www.andorville.com.au/"

        Dim NewClass1 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass1.Name = "ZipComp"
        NewClass1.Description = "The ZipComp class is used to compress files into and extract files from a zip file."
        NewLib.Classes.Add(NewClass1)
        Dim NewClass2 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass2.Name = "XSequence"
        NewClass2.Description = "The XSequence class is used to run an XML property sequence (XSequence) file. XSequence files are used to record and replay processing sequences in Andorville™ software applications."
        NewLib.Classes.Add(NewClass2)
        Dim NewClass3 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass3.Name = "XMessage"
        NewClass3.Description = "The XMessage class is used to read an XML Message (XMessage). An XMessage is a simplified XSequence used to exchange information between Andorville™ software applications."
        NewLib.Classes.Add(NewClass3)
        Dim NewClass4 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass4.Name = "Location"
        NewClass4.Description = "The Location class consists of properties and methods to store data in a location, which is either a directory or archive file."
        NewLib.Classes.Add(NewClass4)
        Dim NewClass5 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass5.Name = "Project"
        NewClass5.Description = "An Andorville™ software application can store data within one or more projects. Each project stores a set of related data files. The Project class contains properties and methods used to manage a project."
        NewLib.Classes.Add(NewClass5)
        Dim NewClass6 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass6.Name = "ProjectSummary"
        NewClass6.Description = "ProjectSummary stores a summary of a project."
        NewLib.Classes.Add(NewClass6)
        Dim NewClass7 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass7.Name = "DataFileInfo"
        NewClass7.Description = "The DataFileInfo class stores information about a data file."
        NewLib.Classes.Add(NewClass7)
        Dim NewClass8 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass8.Name = "Message"
        NewClass8.Description = "The Message class contains text properties and methods used to display messages in an Andorville™ software application."
        NewLib.Classes.Add(NewClass8)
        Dim NewClass9 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass9.Name = "ApplicationSummary"
        NewClass9.Description = "The ApplicationSummary class stores a summary of an Andorville™ software application."
        NewLib.Classes.Add(NewClass9)
        Dim NewClass10 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass10.Name = "LibrarySummary"
        NewClass10.Description = "The LibrarySummary class stores a summary of a software library used by an application."
        NewLib.Classes.Add(NewClass10)
        Dim NewClass11 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass11.Name = "ClassSummary"
        NewClass11.Description = "The ClassSummary class stores a summary of a class contained in a software library."
        NewLib.Classes.Add(NewClass11)
        Dim NewClass12 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass12.Name = "ModificationSummary"
        NewClass12.Description = "The ModificationSummary class stores a summary of any modifications made to an application or library."
        NewLib.Classes.Add(NewClass12)
        Dim NewClass13 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass13.Name = "ApplicationInfo"
        NewClass13.Description = "The ApplicationInfo class stores information about an Andorville™ software application."
        NewLib.Classes.Add(NewClass13)
        Dim NewClass14 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass14.Name = "Version"
        NewClass14.Description = "The Version class stores application, library or project version information."
        NewLib.Classes.Add(NewClass14)
        Dim NewClass15 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass15.Name = "Author"
        NewClass15.Description = "The Author class stores information about an Author."
        NewLib.Classes.Add(NewClass15)
        Dim NewClass16 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass16.Name = "FileAssociation"
        NewClass16.Description = "The FileAssociation class stores the file association extension and description. An application can open files on its file association list."
        NewLib.Classes.Add(NewClass16)
        Dim NewClass17 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass17.Name = "Copyright"
        NewClass17.Description = "The Copyright class stores copyright information."
        NewLib.Classes.Add(NewClass17)
        Dim NewClass18 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass18.Name = "License"
        NewClass18.Description = "The License class stores license information."
        NewLib.Classes.Add(NewClass18)
        Dim NewClass19 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass19.Name = "SourceCode"
        NewClass19.Description = "The SourceCode class stores information about the source code for the application."
        NewLib.Classes.Add(NewClass19)
        Dim NewClass20 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass20.Name = "Usage"
        NewClass20.Description = "The Usage class stores information about application or project usage."
        NewLib.Classes.Add(NewClass20)
        Dim NewClass21 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass21.Name = "Trademark"
        NewClass21.Description = "The Trademark class stored information about a trademark used by the author of an application or data."
        NewLib.Classes.Add(NewClass21)

        ApplicationInfo.Libraries.Add(NewLib)

        'Add other library information here: --------------------------------------------------------------------------

    End Sub

    'Save the form settings if the form is being minimised:
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub SaveProjectSettings()
        'Save the project settings in an XML file.
        'Add any Project Settings to be saved into the settingsData XDocument.

        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <ProjectSettings>
                               <LastUsedCharts>
                                   <%= From item In LastUsedCharts
                                       Select
                                       <Chart>
                                           <Type><%= item.Key %></Type>
                                           <FileName><%= item.Value %></FileName>
                                       </Chart>
                                   %>
                               </LastUsedCharts>
                           </ProjectSettings>

        Dim SettingsFileName As String = "ProjectSettings_" & ApplicationInfo.Name & "_" & ".xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)

    End Sub

    Private Sub RestoreProjectSettings()
        'Restore the project settings from an XML document.

        Dim SettingsFileName As String = "ProjectSettings_" & ApplicationInfo.Name & "_" & ".xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore saved settings.
            Dim ChartsList = From item In Settings.<ProjectSettings>.<LastUsedCharts>.<Chart>

            LastUsedCharts.Clear()
            For Each ChartItem In ChartsList
                'LastUsedCharts.Add(ChartItem.<Type>.Value, ChartItem.<LastUsed>.Value) 'eg. LastUsedCharts.Add("StockChart", "ANZ Stock Prices.StockChart")
                LastUsedCharts.Add(ChartItem.<Type>.Value, ChartItem.<FileName>.Value) 'eg. LastUsedCharts.Add("StockChart", "ANZ Stock Prices.StockChart")
            Next

        End If

    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Form Display Methods - Code used to display this form." '----------------------------------------------------------------------------------------------------------------------------

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Loading the Main form.

        ''Write the startup messages in a stringbuilder object.
        ''Messages cannot be written using Message.Add until this is set up later in the startup sequence.
        'Dim sb As New System.Text.StringBuilder
        'sb.Append("------------------- Starting Application: ADVL Chart Application ---------------------------------------------------------------- " & vbCrLf)

        'Set the Application Directory path: ------------------------------------------------
        Project.ApplicationDir = My.Application.Info.DirectoryPath.ToString

        'Read the Application Information file: ---------------------------------------------
        ApplicationInfo.ApplicationDir = My.Application.Info.DirectoryPath.ToString 'Set the Application Directory property

        If ApplicationInfo.ApplicationLocked Then
            MessageBox.Show("The application is locked. If the application is not already in use, remove the 'Application_Info.lock file from the application directory: " & ApplicationInfo.ApplicationDir, "Notice", MessageBoxButtons.OK)
            Dim dr As System.Windows.Forms.DialogResult
            dr = MessageBox.Show("Press 'Yes' to unlock the application", "Notice", MessageBoxButtons.YesNo)
            If dr = System.Windows.Forms.DialogResult.Yes Then
                ApplicationInfo.UnlockApplication()
            Else
                Application.Exit()
                'System.Windows.Forms.Application.Exit()
            End If
        End If

        ReadApplicationInfo()
        'ApplicationInfo.LockApplication()

        'Read the Application Usage information: --------------------------------------------
        ApplicationUsage.StartTime = Now
        ApplicationUsage.SaveLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        ApplicationUsage.SaveLocn.Path = Project.ApplicationDir
        ApplicationUsage.RestoreUsageInfo()
        'sb.Append("Application usage: Total duration = " & Format(ApplicationUsage.TotalDuration.TotalHours, "#0.##") & " hours" & vbCrLf)

        'Restore Project information: -------------------------------------------------------
        Project.ApplicationName = ApplicationInfo.Name
        'Project.ReadLastProjectInfo()
        'Project.ReadProjectInfoFile()
        'Project.Usage.StartTime = Now

        'Project.ReadProjectInfoFile()

        'Set up Message object:
        Message.ApplicationName = ApplicationInfo.Name

        'Set up a temporary initial settings location:
        Dim TempLocn As New ADVL_Utilities_Library_1.FileLocation
        TempLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        TempLocn.Path = ApplicationInfo.ApplicationDir
        Message.SettingsLocn = TempLocn

        Me.Show() 'Show this form before showing the Message form - This will show the App icon on top in the TaskBar.

        'Start showing messages here - Message system is set up.
        Message.AddText("------------------- Starting Application: ADVL Application Template ----------------- " & vbCrLf, "Heading")
        Message.AddText("Application usage: Total duration = " & Format(ApplicationUsage.TotalDuration.TotalHours, "#.##") & " hours" & vbCrLf, "Normal")



        'https://msdn.microsoft.com/en-us/library/z2d603cy(v=vs.80).aspx#Y550
        'Process any command line arguments:
        Try
            For Each s As String In My.Application.CommandLineArgs
                Message.Add("Command line argument: " & vbCrLf)
                Message.AddXml(s & vbCrLf & vbCrLf)
                InstrReceived = s
            Next
        Catch ex As Exception
            Message.AddWarning("Error processing command line arguments: " & ex.Message & vbCrLf)
        End Try

        If ProjectSelected = False Then
            'Read the Settings Location for the last project used:
            Project.ReadLastProjectInfo()
            'The Last_Project_Info.xml file contains:
            '  Project Name and Description. Settings Location Type and Settings Location Path.
            Message.Add("Last project info has been read." & vbCrLf)
            'Message.Add("Project.SettingsLocn.Type  " & Project.SettingsLocn.Type.ToString & vbCrLf)
            Message.Add("Project.Type.ToString  " & Project.Type.ToString & vbCrLf)
            'Message.Add("Project.SettingsLocn.Path  " & Project.SettingsLocn.Path & vbCrLf)
            Message.Add("Project.Path  " & Project.Path & vbCrLf)


            'At this point read the application start arguments, if any.
            'The selected project may be changed here.

            'Check if the project is locked:
            If Project.ProjectLocked Then
                Message.AddWarning("The project is locked: " & Project.Name & vbCrLf)
                Dim dr As System.Windows.Forms.DialogResult
                dr = MessageBox.Show("Press 'Yes' to unlock the project", "Notice", MessageBoxButtons.YesNo)
                If dr = System.Windows.Forms.DialogResult.Yes Then
                    'ApplicationInfo.UnlockApplication()
                    Project.UnlockProject()
                    Message.AddWarning("The project has been unlocked: " & Project.Name & vbCrLf)
                    'Read the Project Information file: -------------------------------------------------
                    Message.Add("Reading project info." & vbCrLf)
                    Project.ReadProjectInfoFile()                 'Read the file in the SettingsLocation: ADVL_Project_Info.xml

                    'ADDED 2Feb19:
                    Project.ReadParameters()
                    Project.ReadParentParameters()
                    If Project.ParentParameterExists("AppNetName") Then
                        'Project.Parameter("AppNetName") = Project.ParentParameter("AppNetName")
                        Project.AddParameter("AppNetName", Project.ParentParameter("AppNetName").Value, Project.ParentParameter("AppNetName").Description) 'AddParameter will update the parameter if it already exists.
                        AppNetName = Project.Parameter("AppNetName").Value
                    Else
                        'AppNetName = ""
                        AppNetName = Project.GetParameter("AppNetName")
                    End If

                    Project.LockProject() 'Lock the project while it is open in this application.
                    'Set the project start time. This is used to track project usage.
                    Project.Usage.StartTime = Now
                    ApplicationInfo.SettingsLocn = Project.SettingsLocn
                    'Set up the Message object:
                    'Message.ApplicationName = ApplicationInfo.Name
                    Message.SettingsLocn = Project.SettingsLocn
                Else
                    'Application.Exit()
                    'Continue without any project selected.
                    Project.Name = ""
                    Project.Type = ADVL_Utilities_Library_1.Project.Types.None
                    Project.Description = ""
                    Project.SettingsLocn.Path = ""
                    Project.DataLocn.Path = ""
                End If

            Else
                'Read the Project Information file: -------------------------------------------------
                Message.Add("Reading project info." & vbCrLf)
                Project.ReadProjectInfoFile()                 'Read the file in the SettingsLocation: ADVL_Project_Info.xml

                'ADDED 2Feb19:
                Project.ReadParameters()
                Project.ReadParentParameters()
                If Project.ParentParameterExists("AppNetName") Then
                    'Project.Parameter("AppNetName") = Project.ParentParameter("AppNetName")
                    Project.AddParameter("AppNetName", Project.ParentParameter("AppNetName").Value, Project.ParentParameter("AppNetName").Description) 'AddParameter will update the parameter if it already exists.
                    AppNetName = Project.Parameter("AppNetName").Value
                Else
                    'AppNetName = ""
                    AppNetName = Project.GetParameter("AppNetName")
                End If

                Project.LockProject() 'Lock the project while it is open in this application.
                'Set the project start time. This is used to track project usage.
                Project.Usage.StartTime = Now
                ApplicationInfo.SettingsLocn = Project.SettingsLocn
                'Set up the Message object:
                'Message.ApplicationName = ApplicationInfo.Name
                Message.SettingsLocn = Project.SettingsLocn
            End If

        Else 'Project has been opened using Command Line arguments.
            'ADDED 2Feb19:
            Project.ReadParameters()
            Project.ReadParentParameters()
            If Project.ParentParameterExists("AppNetName") Then
                'Project.Parameter("AppNetName") = Project.ParentParameter("AppNetName")
                Project.AddParameter("AppNetName", Project.ParentParameter("AppNetName").Value, Project.ParentParameter("AppNetName").Description) 'AddParameter will update the parameter if it already exists.
                AppNetName = Project.Parameter("AppNetName").Value
            Else
                'AppNetName = ""
                AppNetName = Project.GetParameter("AppNetName")
            End If

            Project.LockProject() 'Lock the project while it is open in this application.

            ProjectSelected = False 'Reset the Project Selected flag.
        End If


        'ApplicationInfo.SettingsLocn = Project.SettingsLocn

        ''Set up the Message object:
        'Message.ApplicationName = ApplicationInfo.Name
        'Message.SettingsLocn = Project.SettingsLocn


        'START Initialise the form: ===============================================================

        Me.WebBrowser1.ObjectForScripting = Me

        'Initialise Input Data Tab ----------------------------------------------------------
        cmbDatabaseType.Items.Add("Access2007To2013")
        cmbDatabaseType.SelectedIndex = 0 'Select the first item

        If InputDatabasePath = "" Then
        Else
            txtDatabasePath.Text = InputDatabasePath
            FillLstTables()
        End If

        'Initialise Chart Settings Tab ------------------------------------------------------
        'Set up the Y Values grid:
        DataGridView1.ColumnCount = 1
        DataGridView1.RowCount = 1
        DataGridView1.Columns(0).HeaderText = "Y Value"
        DataGridView1.Columns(0).Width = 120
        DataGridView1.Columns.Insert(1, cboFieldSelections)
        DataGridView1.Columns(1).HeaderText = "Field"
        DataGridView1.Columns(1).Width = 120
        DataGridView1.AllowUserToResizeColumns = True

        'Set up the Custom Attributes grid:
        DataGridView2.ColumnCount = 3
        DataGridView2.RowCount = 1
        DataGridView2.Columns(0).HeaderText = "Custom Attribute"
        DataGridView2.Columns(0).Width = 120
        DataGridView2.Columns(1).HeaderText = "Value Range"
        DataGridView2.Columns(1).Width = 120
        DataGridView2.Columns(2).HeaderText = "Value"
        DataGridView2.Columns(2).Width = 120
        DataGridView2.AllowUserToResizeColumns = True

        'Show the list of SeriesChartType enumerations in the cmbChartType combobox:
        cmbChartType.Items.Clear()

        'Debug.Print("Setting up cmbChartType")
        'cmbChartType.DrawMode = DrawMode.OwnerDrawFixed
        'cmbChartType.DrawMode = DrawMode.OwnerDrawVariable
        Dim myString As String
        For Each item In System.Enum.GetValues(GetType(DataVisualization.Charting.SeriesChartType))
            'cmbChartType.Items.Add(item)
            If item = DataVisualization.Charting.SeriesChartType.Stock Then
                cmbChartType.Items.Add(item)
            ElseIf item = DataVisualization.Charting.SeriesChartType.Point Then
                cmbChartType.Items.Add(item)
            Else 'These chart types have not yet been coded!!!
                myString = "  (" & CType(item, DataVisualization.Charting.SeriesChartType).ToString & ")"
                cmbChartType.Items.Add(myString)
            End If

        Next

        'For Each item As Object In cmbChartType.Items
        '    'If item = "Stock" Then
        '    If item = DataVisualization.Charting.SeriesChartType.Stock Then
        '        'cmbChartType.BackColor = System.Drawing.Color.Gray

        '    End If
        'Next


        cmbChartType.SelectedIndex = cmbChartType.FindStringExact(ChartType.ToString)

        'Set up Titles tab:  ------------------------------------------------------------------------------------------------------
        'BottomCenter BottomLeft BottomRight MiddleCenter MiddleLeft MiddleRight TopCenter TopLeft TopRight
        cmbAlignment.Items.Clear()

        'Show the list of ContentAlignment enumerations in the cmbAlignment combobox:
        Dim Alignment As String = [Enum].GetName(GetType(ContentAlignment), StockChart.ChartLabel.Alignment) 'MODIFY LATER TO SHOW SETTING FOR CURRENT CHART TYPE.
        For Each item In System.Enum.GetValues(GetType(ContentAlignment))
            cmbAlignment.Items.Add(item)
            If item.ToString = Alignment Then
                cmbAlignment.SelectedIndex = cmbAlignment.Items.Count - 1
            End If
        Next

        'Set up the X Axis tab: ---------------------------------------------------------------------------------------------------
        cmbXAxisTitleAlignment.Items.Clear()
        Dim XAxisTitleAlignment As String = [Enum].GetName(GetType(StringAlignment), StockChart.XAxis.TitleAlignment) 'MODIFY LATER TO SHOW SETTING FOR CURRENT CHART TYPE.
        For Each item In System.Enum.GetValues(GetType(StringAlignment))
            cmbXAxisTitleAlignment.Items.Add(item)
            If item.ToString = XAxisTitleAlignment Then
                cmbXAxisTitleAlignment.SelectedIndex = cmbXAxisTitleAlignment.Items.Count - 1
            End If
        Next

        'Set up the Y Axis tab: ---------------------------------------------------------------------------------------------------
        cmbYAxisTitleAlignment.Items.Clear()
        Dim YAxisTitleAlignment As String = [Enum].GetName(GetType(StringAlignment), StockChart.YAxis.TitleAlignment) 'MODIFY LATER TO SHOW SETTING FOR CURRENT CHART TYPE.
        For Each item In System.Enum.GetValues(GetType(StringAlignment))
            cmbYAxisTitleAlignment.Items.Add(item)
            If item.ToString = YAxisTitleAlignment Then
                cmbYAxisTitleAlignment.SelectedIndex = cmbYAxisTitleAlignment.Items.Count - 1
            End If
        Next

        StockChart.DataLocation = Project.DataLocn
        PointChart.DataLocation = Project.DataLocn

        SetUpChartForm()
        'UpdateCurrentSettings() 'This gets the chart settings from StockChart, ChartLabel, XAxis and YAxis
        UpdateChartForm() 'Update the chart settings on the form.

        'OpenLastUsedChart()


        InitialiseForm() 'Initialise the form for a new project.

        'END   Initialise the form: ---------------------------------------------------------------


        RestoreFormSettings() 'Restore the form settings
        RestoreProjectSettings() 'Restore the Project settings

        OpenLastUsedChart()

        ShowProjectInfo() 'Show the project information.

        'Show the project information: ------------------------------------------------------
        'txtProjectName.Text = Project.Name
        'txtProjectDescription.Text = Project.Description
        'Select Case Project.Type
        '    Case ADVL_Utilities_Library_1.Project.Types.Directory
        '        txtProjectType.Text = "Directory"
        '    Case ADVL_Utilities_Library_1.Project.Types.Archive
        '        txtProjectType.Text = "Archive"
        '    Case ADVL_Utilities_Library_1.Project.Types.Hybrid
        '        txtProjectType.Text = "Hybrid"
        '    Case ADVL_Utilities_Library_1.Project.Types.None
        '        txtProjectType.Text = "None"
        'End Select
        'txtCreationDate.Text = Format(Project.Usage.FirstUsed, "d-MMM-yyyy H:mm:ss")
        'txtLastUsed.Text = Format(Project.Usage.LastUsed, "d-MMM-yyyy H:mm:ss")
        'Select Case Project.SettingsLocn.Type
        '    Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
        '        txtSettingsLocationType.Text = "Directory"
        '    Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
        '        txtSettingsLocationType.Text = "Archive"
        'End Select
        'txtSettingsLocationPath.Text = Project.SettingsLocn.Path
        'Select Case Project.DataLocn.Type
        '    Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
        '        txtDataLocationType.Text = "Directory"
        '    Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
        '        txtDataLocationType.Text = "Archive"
        'End Select
        'txtDataLocationPath.Text = Project.DataLocn.Path
        'txtTotalDuration.Text = Format(Project.Usage.TotalDuration.TotalHours, "#.##")
        ''txtCurrentDuration.Text = Format(Project.Usage.CurrentDuration.TotalHours, "#.###")
        'txtCurrentDuration.Text = Format(Project.Usage.CurrentDuration.TotalHours, "0.000")


        'sb.Append("------------------- Started OK ------------------------------------------------------------------------------------------------------------------------ " & vbCrLf & vbCrLf)
        Message.AddText("------------------- Started OK -------------------------------------------------------------------------- " & vbCrLf & vbCrLf, "Heading")

        'Me.Show() 'Show this form before showing the Message form
        'Message.Add(sb.ToString)

        If StartupConnectionName = "" Then
            'Dont connect to the AppNet

            'UPDATE 20Feb18:
            If Project.ConnectOnOpen Then
                ConnectToComNet() 'The Project is set to connect when it is opened.
            ElseIf ApplicationInfo.ConnectOnStartup Then
                ConnectToComNet() 'The Application is set to connect when it is started.
            Else
                'Don't connect to ComNet.
            End If

        Else
            'Connect to AppNet using the connection name StartupConnectionName.
            ConnectToComNet(StartupConnectionName)
        End If

        'Start the timer to keep the connection awake:
        'Timer3.Interval = 5000 '5 seconds - for testing
        Timer3.Interval = TimeSpan.FromMinutes(55).TotalMilliseconds '55 minute interval
        Timer3.Enabled = True
        Timer3.Start()

    End Sub


    Private Sub InitialiseForm()
        'Initialise the form for a new project.
        OpenStartPage()
    End Sub

    Private Sub ShowProjectInfo()
        'Show the project information:

        txtParentProject.Text = Project.ParentProjectName
        txtAppNetName.Text = Project.GetParameter("AppNetName")
        txtProjectName.Text = Project.Name
        txtProjectDescription.Text = Project.Description
        'txtProjectPath.Text = Project.Path
        Select Case Project.Type
            Case ADVL_Utilities_Library_1.Project.Types.Directory
                txtProjectType.Text = "Directory"
            Case ADVL_Utilities_Library_1.Project.Types.Archive
                txtProjectType.Text = "Archive"
            Case ADVL_Utilities_Library_1.Project.Types.Hybrid
                txtProjectType.Text = "Hybrid"
            Case ADVL_Utilities_Library_1.Project.Types.None
                txtProjectType.Text = "None"
        End Select
        txtCreationDate.Text = Format(Project.Usage.FirstUsed, "d-MMM-yyyy H:mm:ss")
        txtLastUsed.Text = Format(Project.Usage.LastUsed, "d-MMM-yyyy H:mm:ss")
        txtProjectPath.Text = Project.Path

        Select Case Project.SettingsLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSettingsLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSettingsLocationType.Text = "Archive"
        End Select
        txtSettingsLocationPath.Text = Project.SettingsLocn.Path

        Select Case Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtDataLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtDataLocationType.Text = "Archive"
        End Select
        txtDataLocationPath.Text = Project.DataLocn.Path

        Select Case Project.SystemLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSystemLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSystemLocationType.Text = "Archive"
        End Select
        txtSystemLocationPath.Text = Project.SystemLocn.Path

        If Project.ConnectOnOpen Then
            chkConnect.Checked = True
        Else
            chkConnect.Checked = False
        End If

        'txtTotalDuration.Text = Format(Project.Usage.TotalDuration.TotalHours, "#.##")
        txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                                Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                                Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                                Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c)

        'txtCurrentDuration.Text = Format(Project.Usage.CurrentDuration.TotalHours, "0.000")
        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                                Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                                Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                                Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)
    End Sub

    'Private Sub cmbChartType_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbChartType.DrawItem

    '    'e.DrawBackground()

    '    'Dim myString As String = sender.Index.Items(e.Index).ToString()

    '    'Debug.Print(myString)


    '    'e.ToString()

    '    'e.Graphics.DrawString()

    '    If e.Index = 3 Then
    '        'e.DrawBackground()
    '        'e.Graphics.DrawString("Test", e.Font, )
    '    End If

    'End Sub



    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Application

        DisconnectFromComNet() 'Disconnect from the Application Network.

        'SaveFormSettings() 'Save the settings of this form. 'THESE ARE SAVED WHEN THE FORM_CLOSING EVENT TRIGGERS.
        SaveProjectSettings() 'Save project settings.

        ApplicationInfo.WriteFile() 'Update the Application Information file.
        ApplicationInfo.UnlockApplication()

        Project.SaveLastProjectInfo() 'Save information about the last project used.
        Project.SaveParameters() 'ADDED 3Feb19

        'Project.SaveProjectInfoFile() 'Update the Project Information file. This is not required unless there is a change made to the project.

        Project.Usage.SaveUsageInfo() 'Save Project usage information.
        Project.UnlockProject() 'Unlock the project.

        ApplicationUsage.SaveUsageInfo() 'Save Application usage information.

        Application.Exit()

    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Save the form settings if the form state is normal. (A minimised form will have the incorrect size and location.)
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        End If
    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Open and Close Forms - Code used to open and close other forms." '-------------------------------------------------------------------------------------------------------------------

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        'Show the Messages form.
        Message.ApplicationName = ApplicationInfo.Name
        Message.SettingsLocn = Project.SettingsLocn
        Message.Show()
        Message.MessageForm.BringToFront()
    End Sub

    Private Sub btnDesignQuery_Click(sender As Object, e As EventArgs) Handles btnDesignQuery.Click
        'Open the Design Query form:
        If IsNothing(DesignQuery) Then
            DesignQuery = New frmDesignQuery
            DesignQuery.Show()
            DesignQuery.DatabasePath = InputDatabasePath
        Else
            DesignQuery.Show()
            DesignQuery.DatabasePath = InputDatabasePath
        End If
    End Sub

    Private Sub DesignQuery_FormClosed(sender As Object, e As FormClosedEventArgs) Handles DesignQuery.FormClosed
        DesignQuery = Nothing
    End Sub

    Private Sub btnViewData_Click(sender As Object, e As EventArgs) Handles btnViewData.Click
        'Open the View Database Data form:
        If IsNothing(ViewDatabaseData) Then
            ViewDatabaseData = New frmViewDatabaseData
            ViewDatabaseData.Show()
            'ViewDatabaseData.ApplyQuery()
            ViewDatabaseData.Update()
        Else
            ViewDatabaseData.Show()
            'ViewDatabaseData.ApplyQuery()
            ViewDatabaseData.Update()
        End If
    End Sub

    Private Sub ViewDatabaseData_FormClosed(sender As Object, e As FormClosedEventArgs) Handles ViewDatabaseData.FormClosed
        ViewDatabaseData = Nothing
    End Sub

    Private Sub btnWebPages_Click(sender As Object, e As EventArgs) Handles btnWebPages.Click
        'Open the Web Pages form.

        If IsNothing(WebPageList) Then
            WebPageList = New frmWebPageList
            WebPageList.Show()
        Else
            WebPageList.Show()
            WebPageList.BringToFront()
        End If
    End Sub

    Private Sub WebPageList_FormClosed(sender As Object, e As FormClosedEventArgs) Handles WebPageList.FormClosed
        WebPageList = Nothing
    End Sub

    Public Function OpenNewWebPage() As Integer
        'Open a new HTML Web View window, or reuse an existing one if avaiable.
        'The new forms index number in WebViewFormList is returned.

        NewWebPage = New frmWebPage
        If WebPageFormList.Count = 0 Then
            WebPageFormList.Add(NewWebPage)
            WebPageFormList(0).FormNo = 0
            WebPageFormList(0).Show
            Return 0 'The new HTML Display is at position 0 in WebViewFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To WebPageFormList.Count - 1 'Check if there are closed forms in WebViewFormList. They can be re-used.
                If IsNothing(WebPageFormList(I)) Then
                    WebPageFormList(I) = NewWebPage
                    WebPageFormList(I).FormNo = I
                    WebPageFormList(I).Show
                    FormAdded = True
                    Return I 'The new Html Display is at position I in WebViewFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to WebViewFormList
                Dim FormNo As Integer
                WebPageFormList.Add(NewWebPage)
                FormNo = WebPageFormList.Count - 1
                WebPageFormList(FormNo).FormNo = FormNo
                WebPageFormList(FormNo).Show
                Return FormNo 'The new WebPage is at position FormNo in WebPageFormList()
            End If
        End If
    End Function

    Public Sub WebPageFormClosed()
        'This subroutine is called when the Web Page form has been closed.
        'The subroutine is usually called from the FormClosed event of the WebPage form.
        'The WebPage form may have multiple instances.
        'The ClosedFormNumber property should contains the number of the instance of the WebPage form.
        'This property should be updated by the WebPage form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in WebPageList should be set to Nothing.

        If WebPageFormList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in WebPageFormList
            Exit Sub
        End If

        If IsNothing(WebPageFormList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            WebPageFormList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Function OpenNewHtmlDisplayPage() As Integer
        'Open a new HTML display window, or reuse an existing one if avaiable.
        'The new forms index number in HtmlDisplayFormList is returned.

        NewHtmlDisplay = New frmHtmlDisplay
        If HtmlDisplayFormList.Count = 0 Then
            HtmlDisplayFormList.Add(NewHtmlDisplay)
            HtmlDisplayFormList(0).FormNo = 0
            HtmlDisplayFormList(0).Show
            Return 0 'The new HTML Display is at position 0 in HtmlDisplayFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To HtmlDisplayFormList.Count - 1 'Check if there are closed forms in HtmlDisplayFormList. They can be re-used.
                If IsNothing(HtmlDisplayFormList(I)) Then
                    HtmlDisplayFormList(I) = NewHtmlDisplay
                    HtmlDisplayFormList(I).FormNo = I
                    HtmlDisplayFormList(I).Show
                    FormAdded = True
                    Return I 'The new Html Display is at position I in HtmlDisplayFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to HtmlDisplayFormList
                Dim FormNo As Integer
                HtmlDisplayFormList.Add(NewHtmlDisplay)
                FormNo = HtmlDisplayFormList.Count - 1
                HtmlDisplayFormList(FormNo).FormNo = FormNo
                HtmlDisplayFormList(FormNo).Show
                Return FormNo 'The new HtmlDisplay is at position FormNo in HtmlDisplayFormList()
            End If
        End If
    End Function

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Form Methods - The main actions performed by this form." '---------------------------------------------------------------------------------------------------------------------------


    Public Sub UpdateWebPage(ByVal FileName As String)
        'Update the web page in WebPageFormList if the Web file name is FileName.

        Dim NPages As Integer = WebPageFormList.Count
        Dim I As Integer

        'For I = 0 To NPages - 1
        '    If WebPageFormList(I).FileName = FileName Then
        '        WebPageFormList(I).OpenDocument
        '    End If
        'Next
        Try
            For I = 0 To NPages - 1
                If IsNothing(WebPageFormList(I)) Then
                    'Web page has been deleted!
                Else
                    If WebPageFormList(I).FileName = FileName Then
                        WebPageFormList(I).OpenDocument
                    End If
                End If
            Next
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub btnParameters_Click(sender As Object, e As EventArgs) Handles btnParameters.Click
        Project.ShowParameters()
    End Sub

    Private Sub btnOpenProject_Click(sender As Object, e As EventArgs) Handles btnOpenProject.Click
        If Project.Type = ADVL_Utilities_Library_1.Project.Types.Archive Then

        Else
            Process.Start(Project.Path)
        End If
    End Sub

    Private Sub btnOpenSettings_Click(sender As Object, e As EventArgs) Handles btnOpenSettings.Click
        If Project.SettingsLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.SettingsLocn.Path)
        End If
    End Sub

    Private Sub btnOpenData_Click(sender As Object, e As EventArgs) Handles btnOpenData.Click
        If Project.DataLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.DataLocn.Path)
        End If
    End Sub

    Private Sub btnOpenSystem_Click(sender As Object, e As EventArgs) Handles btnOpenSystem.Click
        If Project.SystemLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.SystemLocn.Path)
        End If
    End Sub

    Private Sub btnOpenAppDir_Click(sender As Object, e As EventArgs) Handles btnOpenAppDir.Click
        Process.Start(ApplicationInfo.ApplicationDir)
    End Sub

    Private Sub chkConnect_Layout(sender As Object, e As LayoutEventArgs) Handles chkConnect.Layout
        If chkConnect.Checked Then
            Project.ConnectOnOpen = True
        Else
            Project.ConnectOnOpen = False
        End If
        Project.SaveProjectInfoFile()

    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        'Keet the connection awake with each tick:

        If ConnectedToComNet = True Then
            Try
                If client.IsAlive() Then
                    Message.Add(Format(Now, "HH:mm:ss") & " Connection OK." & vbCrLf)
                    Timer3.Interval = TimeSpan.FromMinutes(55).TotalMilliseconds '55 minute interval
                Else
                    Message.Add(Format(Now, "HH:mm:ss") & " Connection Fault." & vbCrLf)
                    Timer3.Interval = TimeSpan.FromMinutes(55).TotalMilliseconds '55 minute interval
                End If
            Catch ex As Exception
                Message.AddWarning(ex.Message & vbCrLf)
                'Set interval to five minutes - try again in five minutes:
                Timer3.Interval = TimeSpan.FromMinutes(5).TotalMilliseconds '5 minute interval
            End Try
        Else
            Message.Add(Format(Now, "HH:mm:ss") & " Not connected." & vbCrLf)
        End If

    End Sub


#Region " Start Page Code" '=========================================================================================================================================

    Public Sub OpenStartPage()
        'Open the StartPage.html file and display in the Start Page tab.

        If Project.DataFileExists("StartPage.html") Then
            StartPageFileName = "StartPage.html"
            DisplayStartPage()
        Else
            CreateStartPage()
            StartPageFileName = "StartPage.html"
            DisplayStartPage()
        End If

    End Sub

    Public Sub DisplayStartPage()
        'Display the StartPage.html file in the Start Page tab.

        'If Project.DataFileExists("StartPage.html") Then
        If Project.DataFileExists(StartPageFileName) Then
            Dim rtbData As New IO.MemoryStream
            'Project.ReadData("StartPage.html", rtbData)
            Project.ReadData(StartPageFileName, rtbData)
            rtbData.Position = 0
            Dim sr As New IO.StreamReader(rtbData)
            WebBrowser1.DocumentText = sr.ReadToEnd()
            'StartPageFileName = "StartPage.html"
        Else
            Message.AddWarning("Web page file not found: " & StartPageFileName & vbCrLf)
            'StartPageFileName = ""
        End If
    End Sub

    Private Sub CreateStartPage()
        'Create a new default StartPage.html file.

        Dim htmData As New IO.MemoryStream
        Dim sw As New IO.StreamWriter(htmData)
        sw.Write(DefaultHtmlString("Start Page"))
        sw.Flush()
        Project.SaveData("StartPage.html", htmData)
    End Sub

    Public Function DefaultHtmlString(ByVal DocumentTitle As String) As String
        'Create a blank HTML Web Page.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf & "<head>" & vbCrLf & "<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("</head>" & vbCrLf & "<body>" & vbCrLf & vbCrLf)
        'sb.Append("<h1>Start Page</h1>" & vbCrLf & vbCrLf)
        sb.Append("<h1>" & DocumentTitle & "</h1>" & vbCrLf & vbCrLf)

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        sb.Append("</body>" & vbCrLf & "</html>" & vbCrLf)

        Return sb.ToString

    End Function

#End Region 'Start Page Code ------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Methods Called by JavaScript - A collection of methods that can be called by JavaScript in a web page shown in WebBrowser1" '==================================
    'These methods are used to display HTML pages in the Document tab.
    'The same methods can be found in the WebView form, which displays web pages on seprate forms.

    Public Sub JSMethodTest1()
        'Test method that is called from JavaScript.
        Message.Add("JSMethodTest1 called OK." & vbCrLf)
    End Sub

    Public Sub JSMethodTest2(ByVal Var1 As String, ByVal Var2 As String)
        'Test method that is called from JavaScript.
        Message.Add("Var1 = " & Var1 & " Var2 = " & Var2 & vbCrLf)
    End Sub

    Public Sub JSDisplayXml(ByRef XDoc As XDocument)
        Message.Add(XDoc.ToString & vbCrLf & vbCrLf)
    End Sub

    Public Sub ShowMessage(ByVal Msg As String)
        Message.Add(Msg)
    End Sub

    Public Sub SaveHtmlSettings(ByVal xSettings As String, ByVal FileName As String)
        'Save the Html settings for a web page.

        'Convert the XSettings to XML format:

        Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"

        Dim XDocSettings As New System.Xml.Linq.XDocument

        Try
            XDocSettings = System.Xml.Linq.XDocument.Parse(XmlHeader & vbCrLf & xSettings)
        Catch ex As Exception
            Message.AddWarning("Error saving HTML settings file. " & ex.Message & vbCrLf)
        End Try

        Project.SaveXmlData(FileName, XDocSettings)

    End Sub

    Public Sub RestoreHtmlSettings_Old(ByVal FileName As String)
        'Restore the Html settings for a web page.

        Dim XDocSettings As New System.Xml.Linq.XDocument
        Project.ReadXmlData(FileName, XDocSettings)

        If XDocSettings Is Nothing Then
            'Message.Add("No HTML Settings file : " & FileName & vbCrLf)
        Else
            Dim XSettings As New System.Xml.XmlDocument
            Try
                XSettings.LoadXml(XDocSettings.ToString)

                'Run the Settings file:
                XSeq.RunXSequence(XSettings, XStatus)
            Catch ex As Exception
                Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub RestoreHtmlSettings()
        'Restore the Html settings for a web page.

        'Dim SettingsFileName As String = txtNodeKey.Text & "Settings"
        Dim SettingsFileName As String = StartPageFileName & "Settings"

        Dim XDocSettings As New System.Xml.Linq.XDocument
        Project.ReadXmlData(SettingsFileName, XDocSettings)

        If XDocSettings Is Nothing Then
            'Message.Add("No HTML Settings file : " & SettingsFileName & vbCrLf)
        Else
            Dim XSettings As New System.Xml.XmlDocument
            Try
                XSettings.LoadXml(XDocSettings.ToString)
                'Run the Settings file:
                XSeq.RunXSequence(XSettings, Status)
            Catch ex As Exception
                Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Private Sub XSeq_ErrorMsg(ErrMsg As String) Handles XSeq.ErrorMsg
        Message.AddWarning(ErrMsg & vbCrLf)
    End Sub


    Private Sub XSeq_Instruction(Info As String, Locn As String) Handles XSeq.Instruction
        'Execute each instruction produced by running the XSeq file.

        Select Case Locn
            Case "Settings:Form:Name"
                FormName = Info

            Case "Settings:Form:Item:Name"
                ItemName = Info

            Case "Settings:Form:Item:Value"
                RestoreSetting(FormName, ItemName, Info)

            Case "Settings:Form:SelectId"
                SelectId = Info

            Case "Settings:Form:OptionText"
                RestoreOption(SelectId, Info)

            ''Start Project commands: ----------------------------------------------------
            'Case "StartProject:AppName"
            '    StartProject_AppName = Info

            'Case "StartProject:ConnectionName"
            '    StartProject_ConnName = Info

            'Case "StartProject:ProjectID"
            '    StartProject_ProjID = Info

            'Case "StartProject:Command"
            '    Select Case Info
            '        Case "Apply"
            '            StartApp_ProjectID(StartProject_AppName, StartProject_ProjID, StartProject_ConnName)
            '        Case Else
            '            Message.AddWarning("Unknown Start Project command : " & Info & vbCrLf)
            '    End Select

            ''END Start project commands ---------------------------------------------



            Case "Settings"

            Case "EndOfSequence"
                'Main.Message.Add("End of processing sequence" & Info & vbCrLf)

            Case Else
                'Main.Message.AddWarning("Unknown location: " & Locn & "  Info: " & Info & vbCrLf)
                Message.AddWarning("Unknown location: " & Locn & "  Info: " & Info & vbCrLf)

        End Select
    End Sub


    Public Sub RestoreSetting(ByVal FormName As String, ByVal ItemName As String, ByVal ItemValue As String)
        'Restore the setting value with the specified Form Name and Item Name.

        Me.WebBrowser1.Document.InvokeScript("RestoreSetting", New String() {FormName, ItemName, ItemValue})

    End Sub

    Public Sub RestoreOption(ByVal SelectId As String, ByVal OptionText As String)
        'Restore the Option text in the Select control with the Id SelectId.

        Me.WebBrowser1.Document.InvokeScript("RestoreOption", New String() {SelectId, OptionText})
    End Sub

    Private Sub SaveWebPageSettings()
        'Call the SaveSettings JavaScript function:
        Try
            Me.WebBrowser1.Document.InvokeScript("SaveSettings")
        Catch ex As Exception
            Message.AddWarning("Web page settings not saved: " & ex.Message & vbCrLf)
        End Try

    End Sub

    Public Function GetFormNo() As String
        'Return FormNo.ToString
        Return "-1"
    End Function

    Public Sub AddText(ByVal Msg As String, ByVal TextType As String)
        Message.AddText(Msg, TextType)
    End Sub

    Public Sub AddMessage(ByVal Msg As String)
        Message.Add(Msg)
    End Sub

    Public Sub AddWarning(ByVal Msg As String)
        Message.AddWarning(Msg)
    End Sub


    Public Sub SendXMessage(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMessage to the application with the connection name ConnName.

        'myMsgService.SendMessage(ConnName, XMsg) 'ERROR myMsgService is Nothing
        '   myMsgService.SendMessage(ConnName, XMsg)

        'myHost.
        'myHost.

        'myHost.

        '  xxx

    End Sub

    Public Sub RunXSequence(ByVal XSequence As String)
        'Run the XMSequence
        Dim XmlSeq As New System.Xml.XmlDocument
        XmlSeq.LoadXml(XSequence)
        XSeq.RunXSequence(XmlSeq, Status)

    End Sub


#End Region 'Methods Called by JavaScript -------------------------------------------------------------------------------------------------------------------------------




    Private Sub btnAppInfo_Click(sender As Object, e As EventArgs) Handles btnAppInfo.Click
        ApplicationInfo.ShowInfo()
    End Sub

    Private Sub btnAndorville_Click(sender As Object, e As EventArgs) Handles btnAndorville.Click
        ApplicationInfo.ShowInfo()
    End Sub

#Region "Project Information Tab" '----------------------------------------------------------------------------------------------------------------------------------------------------

    Private Sub btnProject_Click(sender As Object, e As EventArgs) Handles btnProject.Click
        Project.SelectProject()
    End Sub

#End Region 'Project Information Tab --------------------------------------------------------------------------------------------------------------------------------------------------

#Region "Chart Sequence Tab" '---------------------------------------------------------------------------------------------------------------------------------------------------------

#End Region 'Chart Sequence Tab -------------------------------------------------------------------------------------------------------------------------------------------------------

#Region "Input Data Tab" '-------------------------------------------------------------------------------------------------------------------------------------------------------------

    Private Sub FillLstTables()
        'Fill the lstSelectTable listbox with the availalble tables in the selected database.

        If InputDatabasePath = "" Then Exit Sub

        'Database access for MS Access:
        Dim connectionString As String 'Declare a connection string for MS Access - defines the database or server to be used.
        Dim conn As System.Data.OleDb.OleDbConnection 'Declare a connection for MS Access - used by the Data Adapter to connect to and disconnect from the database.
        Dim dt As DataTable

        lstTables.Items.Clear()

        'Specify the connection string:
        'Access 2003
        'connectionString = "provider=Microsoft.Jet.OLEDB.4.0;" + _
        '"data source = " + txtDatabase.Text

        'Access 2007:
        connectionString = "provider=Microsoft.ACE.OLEDB.12.0;" +
        "data source = " + txtDatabasePath.Text

        'Connect to the Access database:
        conn = New System.Data.OleDb.OleDbConnection(connectionString)

        conn.Open()

        Dim restrictions As String() = New String() {Nothing, Nothing, Nothing, "TABLE"} 'This restriction removes system tables
        dt = conn.GetSchema("Tables", restrictions)

        'Fill lstTables
        Dim dr As DataRow
        Dim I As Integer 'Loop index
        Dim MaxI As Integer

        MaxI = dt.Rows.Count
        For I = 0 To MaxI - 1
            dr = dt.Rows(0)
            lstTables.Items.Add(dt.Rows(I).Item(2).ToString)
        Next I

        conn.Close()

    End Sub

    Private Sub FillLstFields()
        'Fill the lstFields listbox with the availalble fields in the selected table.

        'Database access for MS Access:
        Dim connectionString As String 'Declare a connection string for MS Access - defines the database or server to be used.
        Dim conn As System.Data.OleDb.OleDbConnection 'Declare a connection for MS Access - used by the Data Adapter to connect to and disconnect from the database.
        Dim commandString As String 'Declare a command string - contains the query to be passed to the database.
        Dim ds As DataSet 'Declate a Dataset.
        Dim dt As DataTable

        If lstTables.SelectedIndex = -1 Then 'No item is selected
            lstFields.Items.Clear()

        Else 'A table has been selected. List its fields:
            lstFields.Items.Clear()

            'Specify the connection string (Access 2007):
            connectionString = "provider=Microsoft.ACE.OLEDB.12.0;" +
            "data source = " + txtDatabasePath.Text

            'Connect to the Access database:
            conn = New System.Data.OleDb.OleDbConnection(connectionString)
            conn.Open()

            'Specify the commandString to query the database:
            commandString = "SELECT TOP 500 * FROM " + lstTables.SelectedItem.ToString
            Dim dataAdapter As New System.Data.OleDb.OleDbDataAdapter(commandString, conn)
            ds = New DataSet
            dataAdapter.Fill(ds, "SelTable") 'ds was defined earlier as a DataSet
            dt = ds.Tables("SelTable")

            Dim NFields As Integer
            NFields = dt.Columns.Count
            Dim I As Integer
            For I = 0 To NFields - 1
                lstFields.Items.Add(dt.Columns(I).ColumnName.ToString)
            Next

            conn.Close()

        End If
    End Sub

    Private Sub lstTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTables.SelectedIndexChanged
        FillLstFields()
    End Sub

    Private Sub btnDatabase_Click(sender As Object, e As EventArgs) Handles btnDatabase.Click
        'Select the database file:

        OpenFileDialog1.Filter = "Access Database |*.accdb"
        OpenFileDialog1.FileName = ""

        If InputDatabaseDirectory <> "" Then
            OpenFileDialog1.InitialDirectory = InputDatabaseDirectory
        End If


        'If txtDatabasePath.Text <> "" Then
        '    Dim fInfo As New System.IO.FileInfo(txtDatabasePath.Text)
        '    OpenFileDialog1.InitialDirectory = fInfo.DirectoryName
        '    OpenFileDialog1.FileName = fInfo.Name
        'End If

        OpenFileDialog1.ShowDialog()
        'txtDatabase.Text = OpenFileDialog1.FileName
        'txtDatabasePath.Text = OpenFileDialog1.FileName 'txtDatabasePath.Text is now updated when the InputDatabasePath property is set.
        'InputDatabasePath = txtDatabasePath.Text
        InputDatabasePath = OpenFileDialog1.FileName
        InputDatabaseDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        Message.Add("InputDatabaseDirectory = " & InputDatabaseDirectory & vbCrLf)

        FillLstTables()
    End Sub

    Private Sub rbDatabase_CheckedChanged(sender As Object, e As EventArgs) Handles rbDatabase.CheckedChanged
        If rbDatabase.Checked Then
            TabControl2.SelectedIndex = 0 'Select Database Input Data
            InputDataType = "Database"
        End If
    End Sub

    Private Sub rbDataset_CheckedChanged(sender As Object, e As EventArgs) Handles rbDataset.CheckedChanged
        If rbDataset.Checked Then
            TabControl2.SelectedIndex = 1 'Select Dataset Input Data
            InputDataType = "Dataset"
        End If
    End Sub

    Private Sub txtDataDescription_LostFocus(sender As Object, e As EventArgs) Handles txtDataDescription.LostFocus
        _inputDataDescr = txtDataDescription.Text
    End Sub

    Private Sub ApplyQuery()
        'Use the query to fill the ds dataset

        If InputDatabasePath = "" Then
            Message.AddWarning("InputDatabasePath is not defined!" & vbCrLf)
            Exit Sub
        End If

        'Database access for MS Access:
        Dim connectionString As String 'Declare a connection string for MS Access - defines the database or server to be used.
        Dim conn As System.Data.OleDb.OleDbConnection 'Declare a connection for MS Access - used by the Data Adapter to connect to and disconnect from the database.
        Dim commandString As String 'Declare a command string - contains the query to be passed to the database.
        'Dim dt As DataTable

        'Specify the connection string (Access 2007):
        connectionString = "provider=Microsoft.ACE.OLEDB.12.0;" +
        "data source = " + InputDatabasePath

        'Connect to the Access database:
        conn = New System.Data.OleDb.OleDbConnection(connectionString)
        conn.Open()

        'Specify the commandString to query the database:
        commandString = InputQuery
        Dim dataAdapter As New System.Data.OleDb.OleDbDataAdapter(commandString, conn)

        ds.Clear()
        ds.Reset()

        dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

        Try
            dataAdapter.Fill(ds, "SelTable")
            UpdateChartQuery()
        Catch ex As Exception
            Message.AddWarning("Error applying query." & vbCrLf)
            Message.AddWarning(ex.Message & vbCrLf)
        End Try

        conn.Close()

    End Sub

    Private Sub btnApplyQuery_Click(sender As Object, e As EventArgs) Handles btnApplyQuery.Click
        InputQuery = txtInputQuery.Text

    End Sub

#End Region 'Input Data Tab -----------------------------------------------------------------------------------------------------------------------------------------------------------


#Region "Chart Settings Tab" '---------------------------------------------------------------------------------------------------------------------------------------------------------

    'New chart - default settings.
    Private Sub btnNewChart_Click(sender As Object, e As EventArgs) Handles btnNewChart.Click
        ClearChart()
    End Sub

    Public Sub ClearChart()
        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                'PointChart = New PointChart
                'SetUpPointChartForm()
                PointChart.Clear()
                UpdatePointChartForm()
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                'StockChart = New StockChart
                'SetUpStockChartForm()
                StockChart.Clear()
                UpdateStockChartForm()
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select
    End Sub

    'Set up the settings forms for the chart type selected.
    Public Sub SetUpChartForm()
        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                SetUpPointChartForm()
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                SetUpStockChartForm()
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select
    End Sub

#Region " Area Chart Settings" '-------------------------------------------------------------------------------------------------------------------------------------------------------

    Private Sub DrawAreaChart()

    End Sub

    'Set up the settings forms for an Area Chart
    Private Sub SetUpAreaChartForm()
        txtChartDescr.Text = "Area Chart" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "The Bar chart type illustrates comparisons among individual items. Categories are organized horizontally while values are displayed vertically in order to place more emphasis on comparing values and less emphasis on time. " & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "Custom Attributes:" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "BarLabelStyle - Specifies the placement of the data point label. (Outside, Left, Right, Center)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "DrawingStyle - Specifies the drawing style of data points. (Outside, Left, Right, Center)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "DrawSideBySide - Specifies whether series of the same chart type are drawn next to each other instead of overlapping each other. (Auto, True, False)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "EmptyPointValue - Specifies the value to be used for empty points. This property determines how an empty point is treated when the chart is drawn. (Average, Zero)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "MaxPixelPointWidth - Specifies the maximum width of the data point in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "MinPixelPointWidth - Specifies the minimum data point width in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PixelPointDepth - Specifies the 3D series depth in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PixelPointGapDepth - Specifies the 3D gap depth in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PixelPointWidth - Specifies the data point width in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PointWidth - data point width. (0 - 2)" & vbCrLf
    End Sub

    Private Sub UpdateAreaChartSettings()

    End Sub

#End Region 'Area Chart Settings ------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Stock Chart Settings" '======================================================================================================================================================

    'Draw the Stock Chart using the settings specified in StockChart
    Private Sub DrawStockChart()
        'Draw the Stock Chart:

        Try
            Chart1.Series.Clear()
            'Chart1.Series.Add("Series1")
            Chart1.Series.Add(StockChart.SeriesName)
            Chart1.Series(StockChart.SeriesName).YValuesPerPoint = 4
            Chart1.Series(StockChart.SeriesName).Points.DataBindXY(ds.Tables(0).DefaultView, StockChart.XValuesFieldName, ds.Tables(0).DefaultView, StockChart.YValuesHighFieldName & "," & StockChart.YValuesLowFieldName & "," & StockChart.YValuesOpenFieldName & "," & StockChart.YValuesCloseFieldName)
            Chart1.Series(StockChart.SeriesName).ChartType = DataVisualization.Charting.SeriesChartType.Stock
            If StockChart.LabelValueType <> "" Then
                Chart1.Series(StockChart.SeriesName).SetCustomProperty("LabelValueType", StockChart.LabelValueType)
            End If

            Chart1.Series(StockChart.SeriesName).SetCustomProperty("MaxPixelPointWidth", StockChart.MaxPixelPointWidth)
            Chart1.Series(StockChart.SeriesName).SetCustomProperty("MinPixelPointWidth", StockChart.MinPixelPointWidth)
            Chart1.Series(StockChart.SeriesName).SetCustomProperty("OpenCloseStyle", StockChart.OpenCloseStyle)
            Chart1.Series(StockChart.SeriesName).SetCustomProperty("PixelPointDepth", StockChart.PixelPointDepth)
            Chart1.Series(StockChart.SeriesName).SetCustomProperty("PixelPointGapDepth", StockChart.PixelPointGapDepth)
            Chart1.Series(StockChart.SeriesName).SetCustomProperty("PixelPointWidth", StockChart.PixelPointWidth)
            Chart1.Series(StockChart.SeriesName).SetCustomProperty("PointWidth", StockChart.PointWidth)
            Chart1.Series(StockChart.SeriesName).SetCustomProperty("ShowOpenClose", StockChart.ShowOpenClose)

            'Specify Y Axis range: -------------------------------------------------------------------------------
            If StockChart.YAxis.AutoMinimum = True Then
                Chart1.ChartAreas(0).AxisY.Minimum = [Double].NaN
            Else
                Chart1.ChartAreas(0).AxisY.Minimum = StockChart.YAxis.Minimum
            End If
            If StockChart.YAxis.AutoMaximum = True Then
                Chart1.ChartAreas(0).AxisY.Maximum = [Double].NaN
            Else
                Chart1.ChartAreas(0).AxisY.Maximum = StockChart.YAxis.Maximum
            End If

            'Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = 5 'Set the grid interval 
            'Chart1.ChartAreas(0).AxisY.Interval = 5 'Set the annotation interval

            'Specify Y Axis annotation and major grid intervals: -----------------------------------------------------
            If StockChart.YAxis.AutoInterval = True Then
                Chart1.ChartAreas(0).AxisY.Interval = 0
            Else
                Chart1.ChartAreas(0).AxisY.Interval = StockChart.YAxis.Interval
            End If

            If StockChart.YAxis.AutoMajorGridInterval = True Then
                Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = 0
                Message.Add("Y Axis major grid interval is automatic." & vbCrLf)
            Else
                Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = StockChart.YAxis.MajorGridInterval
            End If


            'Specify X Axis range: ------------------------------------------------------------------------------
            If StockChart.XAxis.AutoMinimum = True Then
                Chart1.ChartAreas(0).AxisX.Minimum = [Double].NaN
            Else
                Chart1.ChartAreas(0).AxisX.Minimum = StockChart.XAxis.Minimum
            End If
            If StockChart.XAxis.AutoMaximum = True Then
                Chart1.ChartAreas(0).AxisX.Maximum = [Double].NaN
            Else
                Chart1.ChartAreas(0).AxisX.Maximum = StockChart.XAxis.Maximum
            End If

            'Specify X Axis annotation and major grid intervals: -----------------------------------------------------
            Chart1.ChartAreas(0).AxisX.IntervalType = Charting.DateTimeIntervalType.Auto

            If StockChart.XAxis.AutoInterval = True Then
                Chart1.ChartAreas(0).AxisX.Interval = 0
            Else
                Chart1.ChartAreas(0).AxisX.Interval = StockChart.XAxis.Interval
            End If

            If StockChart.XAxis.AutoMajorGridInterval = True Then
                Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = 0
                Message.Add("X Axis major grid interval is automatic." & vbCrLf)
            Else
                Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = StockChart.XAxis.MajorGridInterval
            End If

            'Chart1.ChartAreas(0).RecalculateAxesScale()

            Chart1.ChartAreas(0).AxisX.LabelStyle.IsEndLabelVisible = True

            'Specify X Axis label: ------------------------------------------------------------------------------------
            Chart1.ChartAreas(0).AxisX.TitleAlignment = StockChart.XAxis.TitleAlignment

            Dim myFontStyle As FontStyle = FontStyle.Regular
            If StockChart.XAxis.Title.Bold Then
                myFontStyle = myFontStyle Or FontStyle.Bold
            End If
            If StockChart.XAxis.Title.Italic Then
                myFontStyle = myFontStyle Or FontStyle.Italic
            End If
            If StockChart.XAxis.Title.Strikeout Then
                myFontStyle = myFontStyle Or FontStyle.Strikeout
            End If
            If StockChart.XAxis.Title.Underline Then
                myFontStyle = myFontStyle Or FontStyle.Underline
            End If

            Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", StockChart.XAxis.Title.Size, myFontStyle)
            Chart1.ChartAreas(0).AxisX.Title = StockChart.XAxis.Title.Text

            'Specify Y Axis label: ------------------------------------------------------------------------------------
            Chart1.ChartAreas(0).AxisY.TitleAlignment = StockChart.YAxis.TitleAlignment
            myFontStyle = FontStyle.Regular
            If StockChart.YAxis.Title.Bold Then
                myFontStyle = myFontStyle Or FontStyle.Bold
            End If
            If StockChart.YAxis.Title.Italic Then
                myFontStyle = myFontStyle Or FontStyle.Italic
            End If
            If StockChart.YAxis.Title.Strikeout Then
                myFontStyle = myFontStyle Or FontStyle.Strikeout
            End If
            If StockChart.YAxis.Title.Underline Then
                myFontStyle = myFontStyle Or FontStyle.Underline
            End If

            Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", StockChart.YAxis.Title.Size, myFontStyle)
            Chart1.ChartAreas(0).AxisY.Title = StockChart.YAxis.Title.Text

            'Draw Chart Label:
            'Check if "Label1" is already in the list of titles:
            If Chart1.Titles.IndexOf("Label1") = -1 Then 'Label "Label1" doesnt exist
                Chart1.Titles.Add("Label1").Name = "Label1" 'The name needs to be explicitly declared!
            End If

            Chart1.Titles("Label1").Text = StockChart.ChartLabel.Text

            Dim myFontStyle2 As FontStyle = FontStyle.Regular
            If StockChart.ChartLabel.Bold Then
                myFontStyle2 = myFontStyle2 Or FontStyle.Bold
            End If
            If StockChart.ChartLabel.Italic Then
                myFontStyle2 = myFontStyle2 Or FontStyle.Italic
            End If
            If StockChart.ChartLabel.Strikeout Then
                myFontStyle2 = myFontStyle2 Or FontStyle.Strikeout
            End If
            If StockChart.ChartLabel.Underline Then
                myFontStyle2 = myFontStyle2 Or FontStyle.Underline
            End If

            Chart1.Titles("Label1").Font = New Font("Arial", StockChart.ChartLabel.Size, myFontStyle2)
            Chart1.Titles("Label1").Alignment = StockChart.ChartLabel.Alignment

            Chart1.ChartAreas(0).AxisX.LabelStyle.IsEndLabelVisible = True

            'Display selected chart information:
            Message.Add(vbCrLf & "Main.Chart1.ChartAreas.Count: " & Chart1.ChartAreas.Count & vbCrLf) '1
            Message.Add("Main.Chart1.ChartAreas(0).Name: " & Chart1.ChartAreas(0).Name & vbCrLf) 'ChartArea1
            'Message.Add("Main.Chart1.ChartAreas(0).AxisX.Minimum: " & Chart1.ChartAreas(0).AxisX. & vbCrLf)
            Message.Add("Main.Chart1.ChartAreas(0).AxisX.Minimum: " & Chart1.ChartAreas(0).AxisX.Minimum & vbCrLf) '0
            Message.Add("Main.Chart1.ChartAreas(0).AxisX.Maximum: " & Chart1.ChartAreas(0).AxisX.Maximum & vbCrLf) '1
            Message.Add("Main.Chart1.ChartAreas(0).AxisY.Minimum: " & Chart1.ChartAreas(0).AxisY.Minimum & vbCrLf) 'NaN
            Message.Add("Main.Chart1.ChartAreas(0).AxisY.Maximum: " & Chart1.ChartAreas(0).AxisY.Maximum & vbCrLf) 'NaN
            Message.Add("Main.Chart1.Series(0).Name: " & Chart1.Series(0).Name & vbCrLf) 'Series1
            Message.Add("Main.Chart1.Series(0).Legend: " & Chart1.Series(0).Legend & vbCrLf) 'Legend1
            Message.Add("Main.Chart1.Series(0).YValueType: " & Chart1.Series(0).YValueType & vbCrLf) '2
            'Main.MessageAdd( "Main.Chart1.Series(0).AxisLabel(0): " & Main.Chart1.Series(0).AxisLabel(0) & vbCrLf) 'Index out of range
            'Main.MessageAdd( "Main.Chart1.Series(0).AxisLabel(1): " & Main.Chart1.Series(0).AxisLabel(1) & vbCrLf) 'Index out of range
            Message.Add("Main.Chart1.ChartAreas(0).AxisY2.Minimum: " & Chart1.ChartAreas(0).AxisY2.Minimum & vbCrLf) 'NaN unless specified prior
            Message.Add("Main.Chart1.ChartAreas(0).AxisY2.Maximum: " & Chart1.ChartAreas(0).AxisY2.Maximum & vbCrLf) 'NaN unless specified prior
            Message.Add("Main.Chart1.ChartAreas(0).AxisY.AxisName: " & Chart1.ChartAreas(0).AxisY.AxisName & vbCrLf) '1
            Message.Add("Main.Chart1.ChartAreas(0).AxisX.MajorGrid.Interval: " & Chart1.ChartAreas(0).AxisX.MajorGrid.Interval & vbCrLf) '1
            Message.Add("Main.Chart1.ChartAreas(0).AxisY.MajorGrid.Interval: " & Chart1.ChartAreas(0).AxisY.MajorGrid.Interval & vbCrLf) 'NaN unless specified prior

        Catch ex As Exception
            Message.AddWarning("Error drawing stock chart: " & ex.Message & vbCrLf)
        End Try

    End Sub

    'Set up the settings forms for a Stock Chart
    Private Sub SetUpStockChartForm()
        'Set up stock chart:
        ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), "Stock")
        txtChartDescr.Text = "Stock Chart" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "A Stock chart is typically used to illustrate significant stock price points including a stock's open, close, high, and low price points. However, this type of chart can also be used to analyze scientific data, because each series of data displays high, low, open, and close values, which are typically lines or triangles. The opening values are shown by the markers on the left, and the closing values are shown by the markers on the right." & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "Custom Attributes:" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "LabelValueType - Specifies the Y value to use as the data point label. (High, Low, Open, Close)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "MaxPixelPointWidth - Specifies the maximum width of the data point in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "MinPixelPointWidth - Specifies the minimum data point width in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "OpenCloseStyle - Specifies the marker style for open and close values. (Triangle, Line, Candlestick)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PixelPointDepth - Specifies the 3D series depth in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PixelPointGapDepth - Specifies the 3D gap depth in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PixelPointWidth - Specifies the data point width in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PointWidth - Specifies data point width. (0 to 2)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "ShowOpenClose - Specifies whether markers for open and close prices are displayed. (Both, Open, Close)" & vbCrLf

        'Y Values:
        DataGridView1.Rows.Clear()
        DataGridView1.Rows.Add(4)
        DataGridView1.Rows(0).Cells(0).Value = "High" 'First Y Value Parameter Name
        'DataGridView1.Rows(0).Cells(1).Value = Main.StockChart.YValuesHighFieldName 'Saved selection 'THIS STATEMENT CAUSES AN INTERMITTENT ERROR + ERROR WHENEVER MOUSE PASSES OVER THE CELL!!!
        DataGridView1.Rows(1).Cells(0).Value = "Low" 'Second Y Value parameter name
        DataGridView1.Rows(2).Cells(0).Value = "Open" 'Third Y Value parameter name
        DataGridView1.Rows(3).Cells(0).Value = "Close" 'Fourth Y value parameter name

        'Custom Attributes:
        DataGridView2.Rows.Clear()
        DataGridView2.Rows.Add(9)
        DataGridView2.Rows(0).Cells(0).Value = "LabelValueType"
        DataGridView2.Rows(0).Cells(1).Value = "High, Low, Open, Close"
        Dim cbc0 As New DataGridViewComboBoxCell
        cbc0.Items.Add(" ")
        cbc0.Items.Add("High")
        cbc0.Items.Add("Low")
        cbc0.Items.Add("Open")
        cbc0.Items.Add("Close")
        DataGridView2.Rows(0).Cells(2) = cbc0
        DataGridView2.Rows(1).Cells(0).Value = "MaxPixelPointWidth"
        DataGridView2.Rows(1).Cells(1).Value = "Any integer > 0"
        DataGridView2.Rows(2).Cells(0).Value = "MinPixelPointWidth"
        DataGridView2.Rows(2).Cells(1).Value = "Any integer > 0"
        DataGridView2.Rows(3).Cells(0).Value = "OpenCloseStyle"
        DataGridView2.Rows(3).Cells(1).Value = "Triangle, Line, Candlestick"
        Dim cbc3 As New DataGridViewComboBoxCell
        cbc3.Items.Add(" ")
        cbc3.Items.Add("Triangle")
        cbc3.Items.Add("Line")
        cbc3.Items.Add("Candlestick")
        DataGridView2.Rows(3).Cells(2) = cbc3
        DataGridView2.Rows(4).Cells(0).Value = "PixelPointDepth"
        DataGridView2.Rows(4).Cells(1).Value = "Any integer > 0"
        DataGridView2.Rows(5).Cells(0).Value = "PixelPointGapDepth"
        DataGridView2.Rows(5).Cells(1).Value = "Any integer > 0"
        DataGridView2.Rows(6).Cells(0).Value = "PixelPointWidth"
        DataGridView2.Rows(6).Cells(1).Value = "Any integer > 0"
        DataGridView2.Rows(7).Cells(0).Value = "PointWidth"
        DataGridView2.Rows(7).Cells(1).Value = "0 to 2"
        DataGridView2.Rows(8).Cells(0).Value = "ShowOpenClose"
        DataGridView2.Rows(8).Cells(1).Value = "Both, Open, Close"
        Dim cbc8 As New DataGridViewComboBoxCell
        cbc8.Items.Add(" ")
        cbc8.Items.Add("Both")
        cbc8.Items.Add("Open")
        cbc8.Items.Add("Close")
        DataGridView2.Rows(8).Cells(2) = cbc8

        'UpdateCurrentSettings()
        UpdateChartForm()
    End Sub

    'Update StockChart with the settings selected on the Chart Type, Titles, X Axis and Y Axis tabs.
    Private Sub UpdateStockChartSettings()

        'Update Input Data settings:
        StockChart.InputDataType = InputDataType
        StockChart.InputDatabasePath = InputDatabasePath
        StockChart.InputDataDescr = InputDataDescr
        StockChart.InputQuery = InputQuery

        'Update Chart Properties:

        If txtSeriesName.Text <> "" Then
            StockChart.SeriesName = Trim(txtSeriesName.Text)
        End If

        If IsNothing(cmbXValues.SelectedItem) Then
            Message.AddWarning("The Field containing the XValues for the chart has not been selected." & vbCrLf)
        Else
            StockChart.XValuesFieldName = cmbXValues.SelectedItem.ToString
        End If

        If Trim(DataGridView1.Rows(0).Cells(1).Value) = "" Then
            Message.AddWarning("The Field containing the YValues High for the chart has not been selected." & vbCrLf)
        Else
            StockChart.YValuesHighFieldName = DataGridView1.Rows(0).Cells(1).Value
        End If
        If Trim(DataGridView1.Rows(1).Cells(1).Value) = "" Then
            Message.AddWarning("The Field containing the YValues Low for the chart has not been selected." & vbCrLf)
        Else
            StockChart.YValuesLowFieldName = DataGridView1.Rows(1).Cells(1).Value
        End If
        If Trim(DataGridView1.Rows(2).Cells(1).Value) = "" Then
            Message.AddWarning("The Field containing the YValues Open for the chart has not been selected." & vbCrLf)
        Else
            StockChart.YValuesOpenFieldName = DataGridView1.Rows(2).Cells(1).Value
        End If
        If Trim(DataGridView1.Rows(3).Cells(1).Value) = "" Then
            Message.AddWarning("The Field containing the YValues Close for the chart has not been selected." & vbCrLf)
        Else
            StockChart.YValuesCloseFieldName = DataGridView1.Rows(3).Cells(1).Value
        End If
        If Trim(DataGridView2.Rows(0).Cells(2).Value) = "" Then 'LabelValueType not specified
        Else
            StockChart.LabelValueType = DataGridView2.Rows(0).Cells(2).Value
        End If
        If Trim(DataGridView2.Rows(1).Cells(2).Value) = "" Then 'MaxPixelPointWidth not specified
        Else
            StockChart.MaxPixelPointWidth = DataGridView2.Rows(1).Cells(2).Value
        End If
        If Trim(DataGridView2.Rows(2).Cells(2).Value) = "" Then 'MinPixelPointWidth not specified
        Else
            StockChart.MinPixelPointWidth = DataGridView2.Rows(2).Cells(2).Value
        End If
        If Trim(DataGridView2.Rows(3).Cells(2).Value) = "" Then 'OpenCloseStyle not specified
        Else
            StockChart.OpenCloseStyle = DataGridView2.Rows(3).Cells(2).Value
        End If
        If Trim(DataGridView2.Rows(4).Cells(2).Value) = "" Then 'PixelPointDepth not specified
        Else
            StockChart.PixelPointDepth = DataGridView2.Rows(4).Cells(2).Value
        End If
        If Trim(DataGridView2.Rows(5).Cells(2).Value) = "" Then 'PixelPointGapDepth not specified
        Else
            StockChart.PixelPointGapDepth = DataGridView2.Rows(5).Cells(2).Value
        End If
        If Trim(DataGridView2.Rows(6).Cells(2).Value) = "" Then 'PixelPointWidth not specified
        Else
            StockChart.PixelPointWidth = DataGridView2.Rows(6).Cells(2).Value
        End If
        If Trim(DataGridView2.Rows(7).Cells(2).Value) = "" Then 'PointWidth not specified
        Else
            StockChart.PointWidth = DataGridView2.Rows(7).Cells(2).Value
        End If
        If Trim(DataGridView2.Rows(8).Cells(2).Value) = "" Then 'ShowOpenClose not specified
        Else
            StockChart.ShowOpenClose = DataGridView2.Rows(8).Cells(2).Value
        End If

        'Update Stock Chart label settings:
        'StockChart.ChartLabel.FontName = FontDialog1.Font.Name
        'StockChart.ChartLabel.Size = FontDialog1.Font.Size
        'StockChart.ChartLabel.Bold = FontDialog1.Font.Bold
        'StockChart.ChartLabel.Italic = FontDialog1.Font.Italic
        'StockChart.ChartLabel.Strikeout = FontDialog1.Font.Strikeout
        'StockChart.ChartLabel.Underline = FontDialog1.Font.Underline
        'StockChart.ChartLabel.Text = txtChartTitle.Text

        StockChart.ChartLabel.FontName = txtChartTitle.Font.Name
        StockChart.ChartLabel.Size = txtChartTitle.Font.Size
        StockChart.ChartLabel.Bold = txtChartTitle.Font.Bold
        StockChart.ChartLabel.Italic = txtChartTitle.Font.Italic
        StockChart.ChartLabel.Strikeout = txtChartTitle.Font.Strikeout
        StockChart.ChartLabel.Underline = txtChartTitle.Font.Underline
        StockChart.ChartLabel.Text = txtChartTitle.Text
        'StockChart.ChartLabel.Color = Color.FromName(txtChartTitle.ForeColor) 'Color.FromName(StockChart.ChartLabel.Color)
        StockChart.ChartLabel.Color = txtChartTitle.ForeColor.ToString

        If IsNothing(cmbAlignment.SelectedItem) Then
        Else
            StockChart.ChartLabel.Alignment = [Enum].Parse(GetType(ContentAlignment), cmbAlignment.SelectedItem.ToString)
        End If

        'Update X Axis settings:
        StockChart.XAxis.Title.FontName = txtXAxisTitle.Font.Name
        StockChart.XAxis.Title.Size = txtXAxisTitle.Font.Size
        StockChart.XAxis.Title.Bold = txtXAxisTitle.Font.Bold
        StockChart.XAxis.Title.Italic = txtXAxisTitle.Font.Italic
        StockChart.XAxis.Title.Strikeout = txtXAxisTitle.Font.Strikeout
        StockChart.XAxis.Title.Underline = txtXAxisTitle.Font.Underline
        StockChart.XAxis.Title.Text = txtXAxisTitle.Text

        If chkXAxisAutoMin.Checked = True Then
            StockChart.XAxis.AutoMinimum = True
        Else
            StockChart.XAxis.AutoMinimum = False
        End If

        If chkXAxisAutoMax.Checked = True Then
            StockChart.XAxis.AutoMaximum = True
        Else
            StockChart.XAxis.AutoMaximum = False
        End If

        StockChart.XAxis.Minimum = Val(txtXAxisMin.Text)
        StockChart.XAxis.Maximum = Val(txtXAxisMax.Text)

        If chkXAxisAutoAnnotInt.Checked = True Then
            StockChart.XAxis.Interval = 0 '0 indicates auto annotation.
            StockChart.XAxis.AutoInterval = True
        Else
            StockChart.XAxis.Interval = Val(txtXAxisAnnotInt.Text)
            StockChart.XAxis.AutoInterval = False
        End If

        If chkXAxisAutoMajGridInt.Checked = True Then
            StockChart.XAxis.MajorGridInterval = 0
            StockChart.XAxis.AutoMajorGridInterval = True
            'Message.Add("X Axis major grid interval set to auto." & vbCrLf)
        Else
            StockChart.XAxis.MajorGridInterval = Val(txtXAxisMajGridInt.Text)
            StockChart.XAxis.AutoMajorGridInterval = False
            'Message.Add("X Axis major grid interval set to: " & txtXAxisMajGridInt.Text & vbCrLf)
        End If


        'Update Y Axis settings:
        StockChart.YAxis.Title.FontName = txtYAxisTitle.Font.Name
        StockChart.YAxis.Title.Size = txtYAxisTitle.Font.Size
        StockChart.YAxis.Title.Bold = txtYAxisTitle.Font.Bold
        StockChart.YAxis.Title.Italic = txtYAxisTitle.Font.Italic
        StockChart.YAxis.Title.Strikeout = txtYAxisTitle.Font.Strikeout
        StockChart.YAxis.Title.Underline = txtYAxisTitle.Font.Underline

        StockChart.YAxis.Title.Text = txtYAxisTitle.Text

        If chkYAxisAutoMin.Checked = True Then
            StockChart.YAxis.AutoMinimum = True
        Else
            StockChart.YAxis.AutoMinimum = False
        End If

        If chkYAxisAutoMax.Checked = True Then
            StockChart.YAxis.AutoMaximum = True
        Else
            StockChart.YAxis.AutoMaximum = False
        End If

        StockChart.YAxis.Minimum = Val(txtYAxisMin.Text)
        StockChart.YAxis.Maximum = Val(txtYAxisMax.Text)

        If chkYAxisAutoAnnotInt.Checked = True Then
            StockChart.YAxis.Interval = 0 '0 indicates auto annotation.
            StockChart.YAxis.AutoInterval = True
        Else
            StockChart.YAxis.Interval = Val(txtYAxisAnnotInt.Text)
            StockChart.YAxis.AutoInterval = False
        End If

        If chkYAxisAutoMajGridInt.Checked = True Then
            StockChart.YAxis.MajorGridInterval = 0
            StockChart.YAxis.AutoMajorGridInterval = True
            'Message.Add("Y Axis major grid interval set to auto." & vbCrLf)
        Else
            StockChart.YAxis.MajorGridInterval = Val(txtYAxisMajGridInt.Text)
            StockChart.YAxis.AutoMajorGridInterval = False
            'Message.Add("Y Axis major grid interval set to:" & txtYAxisMajGridInt.Text & vbCrLf)
        End If


    End Sub

    'Update the Chart Type, Titles, X Axis and Y Axis tabs with the settings stored in StockChart.
    Private Sub UpdateStockChartForm()

        'ChartType = ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), StockChart.ChartType)

        'Apply Input Data settings:
        InputDataType = StockChart.InputDataType
        InputDatabasePath = StockChart.InputDatabasePath
        InputQuery = StockChart.InputQuery
        '_inputQuery = StockChart.InputQuery
        InputDataDescr = StockChart.InputDataDescr

        txtSeriesName.Text = StockChart.SeriesName

        'Apply X Values Field Name:
        cmbXValues.SelectedIndex = cmbXValues.FindStringExact(StockChart.XValuesFieldName)

        'Apply Y Values settings:
        For I = 1 To cboFieldSelections.Items.Count
            If StockChart.YValuesHighFieldName = cboFieldSelections.Items(I - 1) Then
                DataGridView1.Rows(0).Cells(1).Value = cboFieldSelections.Items(I - 1)
            End If
            If StockChart.YValuesLowFieldName = cboFieldSelections.Items(I - 1) Then
                DataGridView1.Rows(1).Cells(1).Value = cboFieldSelections.Items(I - 1)
            End If
            If StockChart.YValuesOpenFieldName = cboFieldSelections.Items(I - 1) Then
                DataGridView1.Rows(2).Cells(1).Value = cboFieldSelections.Items(I - 1)
            End If
            If StockChart.YValuesCloseFieldName = cboFieldSelections.Items(I - 1) Then
                DataGridView1.Rows(3).Cells(1).Value = cboFieldSelections.Items(I - 1)
            End If
        Next
        'Apply Custom Attributes Settings:
        'LabelValueType (High, Low, Open, Close)
        If StockChart.LabelValueType <> "" Then
            DataGridView2.Rows(0).Cells(2).Value = StockChart.LabelValueType 'This produces an error
        End If
        'MaxPixelPointWidth (Any integer > 0)
        DataGridView2.Rows(1).Cells(2).Value = StockChart.MaxPixelPointWidth
        'MinPixelPointWidth (Any integer > 0)
        DataGridView2.Rows(2).Cells(2).Value = StockChart.MinPixelPointWidth
        'OpenCloseStyle (Triangle, Line, Candle)
        If StockChart.OpenCloseStyle <> "" Then
            DataGridView2.Rows(3).Cells(2).Value = StockChart.OpenCloseStyle
        End If
        'PixelPointDepth (Any integer > 0)
        DataGridView2.Rows(4).Cells(2).Value = StockChart.PixelPointDepth
        'PixelPointGapDepth (Any integer > 0)
        DataGridView2.Rows(5).Cells(2).Value = StockChart.PixelPointGapDepth
        'PixelPointWidth (Any integer > 0)
        DataGridView2.Rows(6).Cells(2).Value = StockChart.PixelPointWidth
        'PointWidth (0 to 2)
        DataGridView2.Rows(7).Cells(2).Value = StockChart.PointWidth
        'ShowOpenClose (Both, Open, Close)
        If StockChart.ShowOpenClose <> "" Then
            DataGridView2.Rows(8).Cells(2).Value = StockChart.ShowOpenClose
        End If

        'Update the ChartLabel settings: -------------------------------------------------------------------------
        txtChartTitle.Text = StockChart.ChartLabel.Text
        txtChartTitle.ForeColor = Color.FromName(StockChart.ChartLabel.Color)

        txtChartTitle.Text = StockChart.ChartLabel.Text
        txtChartTitle.ForeColor = Color.FromName(StockChart.ChartLabel.Color)
        Dim myFontStyle As FontStyle = FontStyle.Regular
        If StockChart.ChartLabel.Bold Then
            myFontStyle = myFontStyle Or FontStyle.Bold
        End If
        If StockChart.ChartLabel.Italic Then
            myFontStyle = myFontStyle Or FontStyle.Italic
        End If
        If StockChart.ChartLabel.Strikeout Then
            myFontStyle = myFontStyle Or FontStyle.Strikeout
        End If
        If StockChart.ChartLabel.Underline Then
            myFontStyle = myFontStyle Or FontStyle.Underline
        End If

        txtChartTitle.Font = New Font("Arial", StockChart.ChartLabel.Size, myFontStyle)

        'Update the XAxis settings: -------------------------------------------------------------------------
        txtXAxisTitle.Text = StockChart.XAxis.Title.Text
        txtXAxisTitle.ForeColor = Color.FromName(StockChart.XAxis.Title.Color)

        myFontStyle = FontStyle.Regular
        If StockChart.XAxis.Title.Bold Then
            myFontStyle = myFontStyle Or FontStyle.Bold
        End If
        If StockChart.XAxis.Title.Italic Then
            myFontStyle = myFontStyle Or FontStyle.Italic
        End If
        If StockChart.XAxis.Title.Strikeout Then
            myFontStyle = myFontStyle Or FontStyle.Strikeout
        End If
        If StockChart.XAxis.Title.Underline Then
            myFontStyle = myFontStyle Or FontStyle.Underline
        End If

        txtXAxisTitle.Font = New Font(StockChart.XAxis.Title.FontName, StockChart.XAxis.Title.Size, myFontStyle)

        chkXAxisAutoMin.Checked = StockChart.XAxis.AutoMinimum
        chkXAxisAutoMax.Checked = StockChart.XAxis.AutoMaximum

        txtXAxisMin.Text = StockChart.XAxis.Minimum
        txtXAxisMax.Text = StockChart.XAxis.Maximum

        chkXAxisAutoAnnotInt.Checked = StockChart.XAxis.AutoInterval
        chkXAxisAutoMajGridInt.Checked = StockChart.XAxis.AutoMajorGridInterval

        txtXAxisAnnotInt.Text = StockChart.XAxis.Interval
        txtXAxisMajGridInt.Text = StockChart.XAxis.MajorGridInterval

        'Update the YAxis settings: -----------------------------------------------------------------------------
        txtYAxisTitle.Text = StockChart.YAxis.Title.Text
        txtYAxisTitle.ForeColor = Color.FromName(StockChart.YAxis.Title.Color)

        myFontStyle = FontStyle.Regular
        If StockChart.YAxis.Title.Bold Then
            myFontStyle = myFontStyle Or FontStyle.Bold
        End If
        If StockChart.YAxis.Title.Italic Then
            myFontStyle = myFontStyle Or FontStyle.Italic
        End If
        If StockChart.YAxis.Title.Strikeout Then
            myFontStyle = myFontStyle Or FontStyle.Strikeout
        End If
        If StockChart.YAxis.Title.Underline Then
            myFontStyle = myFontStyle Or FontStyle.Underline
        End If

        txtYAxisTitle.Font = New Font(StockChart.YAxis.Title.FontName, StockChart.YAxis.Title.Size, myFontStyle)

        chkYAxisAutoMin.Checked = StockChart.YAxis.AutoMinimum
        chkYAxisAutoMax.Checked = StockChart.YAxis.AutoMaximum

        txtYAxisMin.Text = StockChart.YAxis.Minimum
        txtYAxisMax.Text = StockChart.YAxis.Maximum

        chkYAxisAutoAnnotInt.Checked = StockChart.YAxis.AutoInterval
        chkYAxisAutoMajGridInt.Checked = StockChart.YAxis.AutoMajorGridInterval

        txtYAxisAnnotInt.Text = StockChart.YAxis.Interval
        txtYAxisMajGridInt.Text = StockChart.YAxis.MajorGridInterval

        'Update chart File Name:
        txtChartFileName.Text = StockChart.FileName

    End Sub

    Private Sub SaveStockChart(ByVal FileName As String)
        'Save the stock chart with the name FileName.

        If Trim(FileName) = "" Then
            Message.AddWarning("No file name specified." & vbCrLf)
            Exit Sub
        End If

        Dim myFileName As String = Trim(FileName)

        If myFileName.EndsWith(".StockChart") Then
            StockChart.SaveFile(myFileName)
        Else
            If myFileName.Contains(".") Then
                Message.AddWarning("File does not have the extension '.StockChart" & vbCrLf)
                Exit Sub
            Else
                myFileName = myFileName & ".StockChart"
                StockChart.SaveFile(myFileName)
                txtChartFileName.Text = myFileName
            End If
        End If

    End Sub

    Private Sub OpenStockChart()
        'Find and open a Stock chart file.
        Select Case Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a Stock Chart file from the project directory:
                OpenFileDialog1.InitialDirectory = Project.DataLocn.Path
                OpenFileDialog1.Filter = "Stock Chart | *.StockChart"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    txtChartFileName.Text = FileName
                    StockChart.LoadFile(FileName)
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                'Select a Stock Chart file from the project archive:
                'Show the zip archive file selection form:
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Project.DataLocn.Path
                Zip.SelectFile()
                Zip.SelectFileForm.ApplicationName = Project.ApplicationName
                Zip.SelectFileForm.SettingsLocn = Project.SettingsLocn
                Zip.SelectFileForm.Show()
                Zip.SelectFileForm.RestoreFormSettings()
                Zip.SelectFileForm.FileExtension = ".StockChart"
                Zip.SelectFileForm.GetFileList()
        End Select
    End Sub

#End Region 'Stock Chart Settings -----------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Point Chart Settings" '======================================================================================================================================================

    'Draw the Point Chart using the settings specified in PointChart
    Private Sub DrawPointChart()
        'Draw the Point Chart:

        Try

            Chart1.Series.Clear()
            'Chart1.Series.Add("Series1")
            Chart1.Series.Add(PointChart.SeriesName)
            Chart1.Series(PointChart.SeriesName).YValuesPerPoint = 1
            Chart1.Series(PointChart.SeriesName).Points.DataBindXY(ds.Tables(0).DefaultView, PointChart.XValuesFieldName, ds.Tables(0).DefaultView, PointChart.YValuesFieldName)
            Chart1.Series(PointChart.SeriesName).ChartType = DataVisualization.Charting.SeriesChartType.Point

            'Chart1.Series(PointChart.SeriesName).Points(200).Label = "Test"
            'Chart1.Series(PointChart.SeriesName).Points(200).MarkerColor = Color.PaleGreen
            'Chart1.Series(PointChart.SeriesName).Points(210).Label = "Test"
            'Chart1.Series(PointChart.SeriesName).Points(210).MarkerColor = Color.Azure
            'Chart1.Series(PointChart.SeriesName).Points(220).Label = "Test"
            'Chart1.Series(PointChart.SeriesName).Points(220).MarkerColor = Color.Bisque
            'Chart1.Series(PointChart.SeriesName).Points(230).Label = "Test"
            'Chart1.Series(PointChart.SeriesName).Points(230).MarkerColor = Color.Black
            'Chart1.Series(PointChart.SeriesName).Points(240).Label = "Test"
            'Chart1.Series(PointChart.SeriesName).Points(240).MarkerColor = Color.BlueViolet
            'Chart1.Series(PointChart.SeriesName).Points(250).Label = "Test"
            'Chart1.Series(PointChart.SeriesName).Points(250).MarkerColor = Color.CadetBlue

            ''Chart1.Series(PointChart.SeriesName).Points(300).Label = 

            'For Each item In Chart1.Series(PointChart.SeriesName).Points()

            'Next

            ''NOTE: This code was used to color code the points according to the "Total_Profit_pct" data field in the Greenblatt analysis table.
            'Dim NRows As Integer = ds.Tables(0).Rows.Count
            'Dim I As Integer
            'For I = 0 To NRows - 1
            '    'If ds.Tables(0).Rows(I).Item("Total_Profit_pct") = DBNull Then
            '    If IsDBNull(ds.Tables(0).Rows(I).Item("Total_Profit_pct")) Then
            '    Else
            '        'If ds.Tables(0).Rows(I).Item("Total_Profit_pct") > 0 Then
            '        '    Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.Black
            '        'Else
            '        '    Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.Red
            '        'End If
            '        If ds.Tables(0).Rows(I).Item("Total_Profit_pct") > 100 Then
            '            Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.Black
            '        ElseIf ds.Tables(0).Rows(I).Item("Total_Profit_pct") > 50 Then
            '            Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.DarkGray
            '        ElseIf ds.Tables(0).Rows(I).Item("Total_Profit_pct") > 10 Then
            '            Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.Gray
            '        ElseIf ds.Tables(0).Rows(I).Item("Total_Profit_pct") > 0 Then
            '            Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.LightGray
            '        ElseIf ds.Tables(0).Rows(I).Item("Total_Profit_pct") > -10 Then
            '            Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.LightPink
            '        ElseIf ds.Tables(0).Rows(I).Item("Total_Profit_pct") > -50 Then
            '            Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.Pink
            '        Else
            '            Chart1.Series(PointChart.SeriesName).Points(I).MarkerColor = Color.Red
            '        End If
            '    End If
            'Next

            If PointChart.EmptyPointValue <> "" Then Chart1.Series(PointChart.SeriesName).SetCustomProperty("EmptyPointValue", PointChart.EmptyPointValue)
            If PointChart.LabelStyle <> "" Then Chart1.Series(PointChart.SeriesName).SetCustomProperty("LabelStyle", PointChart.LabelStyle)
            Chart1.Series(PointChart.SeriesName).SetCustomProperty("PixelPointDepth", PointChart.PixelPointDepth)
            Chart1.Series(PointChart.SeriesName).SetCustomProperty("PixelPointGapDepth", PointChart.PixelPointGapDepth)

            'Specify Y Axis range: -------------------------------------------------------------------------------
            If PointChart.YAxis.AutoMinimum = True Then
                Chart1.ChartAreas(0).AxisY.Minimum = [Double].NaN
            Else
                Chart1.ChartAreas(0).AxisY.Minimum = PointChart.YAxis.Minimum
            End If
            If PointChart.YAxis.AutoMaximum = True Then
                Chart1.ChartAreas(0).AxisY.Maximum = [Double].NaN
            Else
                Chart1.ChartAreas(0).AxisY.Maximum = PointChart.YAxis.Maximum
            End If

            'Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = 5 'Set the grid interval 
            'Chart1.ChartAreas(0).AxisY.Interval = 5 'Set the annotation interval

            'Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = [Double].NaN 'Default?
            'Chart1.ChartAreas(0).AxisY.Interval = [Double].NaN 'Default?

            'Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = 0 'Default
            'Chart1.ChartAreas(0).AxisY.Interval = 0 'Default

            'Specify Y Axis annotation and major grid intervals: -----------------------------------------------------
            If PointChart.YAxis.AutoInterval = True Then
                Chart1.ChartAreas(0).AxisY.Interval = 0
            Else
                Chart1.ChartAreas(0).AxisY.Interval = PointChart.YAxis.Interval
            End If

            If PointChart.YAxis.AutoMajorGridInterval = True Then
                Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = 0
                Message.Add("Y Axis major grid interval is automatic." & vbCrLf)
            Else
                Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = PointChart.YAxis.MajorGridInterval
            End If

            Chart1.ChartAreas(0).AxisY.LineWidth = 2 'Increase the line width of the Y Axis.
            'Chart1.ChartAreas(0).AxisY(0).LineWidth = 2
            'Chart1.ChartAreas(0).AxisY("0").Line = 2
            'Chart1.ChartAreas(0).AxisY.CustomLabels.
            Chart1.ChartAreas(0).AxisY.Crossing = 0

            'Add a bold origin axis line:
            'Dim am1 As New Charting.


            'Specify X Axis range: ------------------------------------------------------------------------------
            If PointChart.XAxis.AutoMinimum = True Then
                Chart1.ChartAreas(0).AxisX.Minimum = [Double].NaN
            Else
                Chart1.ChartAreas(0).AxisX.Minimum = PointChart.XAxis.Minimum
            End If
            If PointChart.XAxis.AutoMaximum = True Then
                Chart1.ChartAreas(0).AxisX.Maximum = [Double].NaN
            Else
                Chart1.ChartAreas(0).AxisX.Maximum = PointChart.XAxis.Maximum
            End If

            'Specify X Axis annotation and major grid intervals: -----------------------------------------------------
            Chart1.ChartAreas(0).AxisX.IntervalType = Charting.DateTimeIntervalType.Auto

            If PointChart.XAxis.AutoInterval = True Then
                Chart1.ChartAreas(0).AxisX.Interval = 0
            Else
                Chart1.ChartAreas(0).AxisX.Interval = PointChart.XAxis.Interval
            End If

            If PointChart.XAxis.AutoMajorGridInterval = True Then
                Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = 0
                Message.Add("X Axis major grid interval is automatic." & vbCrLf)
            Else
                Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = PointChart.XAxis.MajorGridInterval
            End If

            Chart1.ChartAreas(0).AxisX.LineWidth = 2 'Increase the line width of the X Axis.
            Chart1.ChartAreas(0).AxisX.Crossing = 0

            'Specify X Axis label: ------------------------------------------------------------------------------------
            Chart1.ChartAreas(0).AxisX.TitleAlignment = PointChart.XAxis.TitleAlignment

            Dim myFontStyle As FontStyle = FontStyle.Regular
            If PointChart.XAxis.Title.Bold Then
                myFontStyle = myFontStyle Or FontStyle.Bold
            End If
            If PointChart.XAxis.Title.Italic Then
                myFontStyle = myFontStyle Or FontStyle.Italic
            End If
            If PointChart.XAxis.Title.Strikeout Then
                myFontStyle = myFontStyle Or FontStyle.Strikeout
            End If
            If PointChart.XAxis.Title.Underline Then
                myFontStyle = myFontStyle Or FontStyle.Underline
            End If

            Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", PointChart.XAxis.Title.Size, myFontStyle)
            Chart1.ChartAreas(0).AxisX.Title = PointChart.XAxis.Title.Text

            'Specify Y Axis label: ------------------------------------------------------------------------------------
            Chart1.ChartAreas(0).AxisY.TitleAlignment = PointChart.YAxis.TitleAlignment
            myFontStyle = FontStyle.Regular
            If PointChart.YAxis.Title.Bold Then
                myFontStyle = myFontStyle Or FontStyle.Bold
            End If
            If PointChart.YAxis.Title.Italic Then
                myFontStyle = myFontStyle Or FontStyle.Italic
            End If
            If PointChart.YAxis.Title.Strikeout Then
                myFontStyle = myFontStyle Or FontStyle.Strikeout
            End If
            If PointChart.YAxis.Title.Underline Then
                myFontStyle = myFontStyle Or FontStyle.Underline
            End If

            Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", PointChart.YAxis.Title.Size, myFontStyle)
            Chart1.ChartAreas(0).AxisY.Title = PointChart.YAxis.Title.Text

            'Draw Chart Label:
            'Check if "Label1" is already in the list of titles:
            If Chart1.Titles.IndexOf("Label1") = -1 Then 'Label "Label1" doesnt exist
                Chart1.Titles.Add("Label1").Name = "Label1" 'The name needs to be explicitly declared!
            End If

            Chart1.Titles("Label1").Text = PointChart.ChartLabel.Text

            Dim myFontStyle2 As FontStyle = FontStyle.Regular
            If PointChart.ChartLabel.Bold Then
                myFontStyle2 = myFontStyle2 Or FontStyle.Bold
            End If
            If PointChart.ChartLabel.Italic Then
                myFontStyle2 = myFontStyle2 Or FontStyle.Italic
            End If
            If PointChart.ChartLabel.Strikeout Then
                myFontStyle2 = myFontStyle2 Or FontStyle.Strikeout
            End If
            If PointChart.ChartLabel.Underline Then
                myFontStyle2 = myFontStyle2 Or FontStyle.Underline
            End If

            Chart1.Titles("Label1").Font = New Font("Arial", PointChart.ChartLabel.Size, myFontStyle2)
            Chart1.Titles("Label1").Alignment = PointChart.ChartLabel.Alignment

            'Chart1.




            'Display selected chart information:
            Message.Add(vbCrLf & "Main.Chart1.ChartAreas.Count: " & Chart1.ChartAreas.Count & vbCrLf) '1
            Message.Add("Main.Chart1.ChartAreas(0).Name: " & Chart1.ChartAreas(0).Name & vbCrLf) 'ChartArea1
            Message.Add("Main.Chart1.ChartAreas(0).AxisX.Minimum: " & Chart1.ChartAreas(0).AxisX.Minimum & vbCrLf) '0
            Message.Add("Main.Chart1.ChartAreas(0).AxisX.Maximum: " & Chart1.ChartAreas(0).AxisX.Maximum & vbCrLf) '1
            Message.Add("Main.Chart1.ChartAreas(0).AxisY.Minimum: " & Chart1.ChartAreas(0).AxisY.Minimum & vbCrLf) 'NaN
            Message.Add("Main.Chart1.ChartAreas(0).AxisY.Maximum: " & Chart1.ChartAreas(0).AxisY.Maximum & vbCrLf) 'NaN
            Message.Add("Main.Chart1.Series(0).Name: " & Chart1.Series(0).Name & vbCrLf) 'Series1
            Message.Add("Main.Chart1.Series(0).Legend: " & Chart1.Series(0).Legend & vbCrLf) 'Legend1
            Message.Add("Main.Chart1.Series(0).YValueType: " & Chart1.Series(0).YValueType & vbCrLf) '2
            'Main.MessageAdd( "Main.Chart1.Series(0).AxisLabel(0): " & Main.Chart1.Series(0).AxisLabel(0) & vbCrLf) 'Index out of range
            'Main.MessageAdd( "Main.Chart1.Series(0).AxisLabel(1): " & Main.Chart1.Series(0).AxisLabel(1) & vbCrLf) 'Index out of range
            Message.Add("Main.Chart1.ChartAreas(0).AxisY2.Minimum: " & Chart1.ChartAreas(0).AxisY2.Minimum & vbCrLf) 'NaN unless specified prior
            Message.Add("Main.Chart1.ChartAreas(0).AxisY2.Maximum: " & Chart1.ChartAreas(0).AxisY2.Maximum & vbCrLf) 'NaN unless specified prior
            Message.Add("Main.Chart1.ChartAreas(0).AxisY.AxisName: " & Chart1.ChartAreas(0).AxisY.AxisName & vbCrLf) '1
            Message.Add("Main.Chart1.ChartAreas(0).AxisX.MajorGrid.Interval: " & Chart1.ChartAreas(0).AxisX.MajorGrid.Interval & vbCrLf) '1
            Message.Add("Main.Chart1.ChartAreas(0).AxisY.MajorGrid.Interval: " & Chart1.ChartAreas(0).AxisY.MajorGrid.Interval & vbCrLf) 'NaN unless specified prior


        Catch ex As Exception
            Message.AddWarning("Error drawing point chart: " & ex.Message & vbCrLf)
        End Try

    End Sub

    'Set up the settings forms for a Point Chart
    Private Sub SetUpPointChartForm()
        'Set up Point chart:
        ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), "Point")
        txtChartDescr.Text = "Point Chart" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "The Point chart type uses value points to represent its data." & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "Custom Attributes:" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "EmptyPointValue - Specifies the value to be used for empty points. This property determines how an empty point is treated when the chart is drawn. (Average, Zero)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "LabelStyle - Specifies the label position of the data point. (Auto, Top, Bottom, Right, Left, TopLeft, TopRight, BottomLeft, BottomRight, Center)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PixelPointDepth - Specifies the 3D series depth in pixels. (Any integer > 0)" & vbCrLf
        txtChartDescr.Text = txtChartDescr.Text & "PixelPointGapDepth - Specifies the 3D gap depth in pixels. (Any integer > 0)" & vbCrLf

        'Y Values:
        DataGridView1.Rows.Clear()
        DataGridView1.Rows.Add(1)
        DataGridView1.Rows(0).Cells(0).Value = "Yvalue" 'Y Value Parameter Name

        'Custom Attributes:
        DataGridView2.Rows.Clear()
        DataGridView2.Rows.Add(4)
        DataGridView2.Rows(0).Cells(0).Value = "EmptyPointValue"
        DataGridView2.Rows(0).Cells(1).Value = "Average, Zero"
        Dim cbc0 As New DataGridViewComboBoxCell
        cbc0.Items.Add(" ")
        cbc0.Items.Add("Average")
        cbc0.Items.Add("Zero")
        DataGridView2.Rows(0).Cells(2) = cbc0
        DataGridView2.Rows(1).Cells(0).Value = "LabelStyle"
        DataGridView2.Rows(1).Cells(1).Value = "Auto, Top, Bottom, Right, Left, TopLeft, TopRight, BottomLeft, BottomRight, Center"
        Dim cbc1 As New DataGridViewComboBoxCell
        cbc1.Items.Add(" ")
        cbc1.Items.Add("Auto")
        cbc1.Items.Add("Top")
        cbc1.Items.Add("Bottom")
        cbc1.Items.Add("Right")
        cbc1.Items.Add("Left")
        cbc1.Items.Add("TopLeft")
        cbc1.Items.Add("TopRight")
        cbc1.Items.Add("BottomLeft")
        cbc1.Items.Add("BottomRight")
        cbc1.Items.Add("Center")
        DataGridView2.Rows(1).Cells(2) = cbc1
        DataGridView2.Rows(2).Cells(0).Value = "PixelPointDepth"
        DataGridView2.Rows(2).Cells(1).Value = "Any integer > 0"
        DataGridView2.Rows(3).Cells(0).Value = "PixelPointGapDepth"
        DataGridView2.Rows(3).Cells(1).Value = "Any integer > 0"

        'UpdateCurrentSettings()
        UpdateChartForm()
    End Sub

    'Update the Chart Type, Titles, X Axis and Y Axis tabs with the settings stored in PointChart.
    Private Sub UpdatePointChartForm()

        'Apply Input Data settings:
        InputDataType = PointChart.InputDataType
        InputDatabasePath = PointChart.InputDatabasePath
        InputQuery = PointChart.InputQuery
        InputDataDescr = PointChart.InputDataDescr

        txtSeriesName.Text = PointChart.SeriesName

        'Apply X Values Field Name:
        cmbXValues.SelectedIndex = cmbXValues.FindStringExact(PointChart.XValuesFieldName)

        'Apply Y Values selections:
        For I = 1 To cboFieldSelections.Items.Count
            If PointChart.YValuesFieldName = cboFieldSelections.Items(I - 1) Then
                DataGridView1.Rows(0).Cells(1).Value = cboFieldSelections.Items(I - 1)
            End If
        Next

        'Apply Custom Attributes selections:
        'LabelValueType (High, Low, Open, Close)
        If PointChart.EmptyPointValue <> "" Then
            DataGridView2.Rows(0).Cells(2).Value = PointChart.EmptyPointValue
        End If
        If PointChart.LabelStyle <> "" Then
            DataGridView2.Rows(1).Cells(2).Value = PointChart.LabelStyle
        End If
        'PixelPointDepth (Any integer > 0)
        DataGridView2.Rows(2).Cells(2).Value = PointChart.PixelPointDepth
        'PixelPointGapDepth (Any integer > 0)
        DataGridView2.Rows(3).Cells(2).Value = PointChart.PixelPointGapDepth

        'Update the ChartLabel settings: -------------------------------------------------------------------------
        txtChartTitle.Text = PointChart.ChartLabel.Text
        txtChartTitle.ForeColor = Color.FromName(PointChart.ChartLabel.Color)

        txtChartTitle.Text = PointChart.ChartLabel.Text
        txtChartTitle.ForeColor = Color.FromName(PointChart.ChartLabel.Color)
        Dim myFontStyle As FontStyle = FontStyle.Regular
        If PointChart.ChartLabel.Bold Then
            myFontStyle = myFontStyle Or FontStyle.Bold
        End If
        If PointChart.ChartLabel.Italic Then
            myFontStyle = myFontStyle Or FontStyle.Italic
        End If
        If PointChart.ChartLabel.Strikeout Then
            myFontStyle = myFontStyle Or FontStyle.Strikeout
        End If
        If PointChart.ChartLabel.Underline Then
            myFontStyle = myFontStyle Or FontStyle.Underline
        End If

        txtChartTitle.Font = New Font("Arial", PointChart.ChartLabel.Size, myFontStyle)

        'Update the XAxis settings: -------------------------------------------------------------------------
        txtXAxisTitle.Text = PointChart.XAxis.Title.Text
        txtXAxisTitle.ForeColor = Color.FromName(PointChart.XAxis.Title.Color)

        myFontStyle = FontStyle.Regular
        If PointChart.XAxis.Title.Bold Then
            myFontStyle = myFontStyle Or FontStyle.Bold
        End If
        If PointChart.XAxis.Title.Italic Then
            myFontStyle = myFontStyle Or FontStyle.Italic
        End If
        If PointChart.XAxis.Title.Strikeout Then
            myFontStyle = myFontStyle Or FontStyle.Strikeout
        End If
        If PointChart.XAxis.Title.Underline Then
            myFontStyle = myFontStyle Or FontStyle.Underline
        End If

        txtXAxisTitle.Font = New Font(PointChart.XAxis.Title.FontName, PointChart.XAxis.Title.Size, myFontStyle)

        chkXAxisAutoMin.Checked = PointChart.XAxis.AutoMinimum
        chkXAxisAutoMax.Checked = PointChart.XAxis.AutoMaximum

        txtXAxisMin.Text = PointChart.XAxis.Minimum
        txtXAxisMax.Text = PointChart.XAxis.Maximum

        chkXAxisAutoAnnotInt.Checked = PointChart.XAxis.AutoInterval
        chkXAxisAutoMajGridInt.Checked = PointChart.XAxis.AutoMajorGridInterval

        txtXAxisAnnotInt.Text = PointChart.XAxis.Interval
        txtXAxisMajGridInt.Text = PointChart.XAxis.MajorGridInterval



        'Update the YAxis settings: -----------------------------------------------------------------------------
        txtYAxisTitle.Text = PointChart.YAxis.Title.Text
        txtYAxisTitle.ForeColor = Color.FromName(PointChart.YAxis.Title.Color)

        myFontStyle = FontStyle.Regular
        If PointChart.YAxis.Title.Bold Then
            myFontStyle = myFontStyle Or FontStyle.Bold
        End If
        If PointChart.YAxis.Title.Italic Then
            myFontStyle = myFontStyle Or FontStyle.Italic
        End If
        If PointChart.YAxis.Title.Strikeout Then
            myFontStyle = myFontStyle Or FontStyle.Strikeout
        End If
        If PointChart.YAxis.Title.Underline Then
            myFontStyle = myFontStyle Or FontStyle.Underline
        End If

        txtYAxisTitle.Font = New Font(PointChart.YAxis.Title.FontName, PointChart.YAxis.Title.Size, myFontStyle)

        chkYAxisAutoMin.Checked = PointChart.YAxis.AutoMinimum
        chkYAxisAutoMax.Checked = PointChart.YAxis.AutoMaximum

        txtYAxisMin.Text = PointChart.YAxis.Minimum
        txtYAxisMax.Text = PointChart.YAxis.Maximum

        chkYAxisAutoAnnotInt.Checked = PointChart.YAxis.AutoInterval
        chkYAxisAutoMajGridInt.Checked = PointChart.YAxis.AutoMajorGridInterval

        txtYAxisAnnotInt.Text = PointChart.YAxis.Interval
        txtYAxisMajGridInt.Text = PointChart.YAxis.MajorGridInterval

        'Update chart File Name:
        txtChartFileName.Text = PointChart.FileName



    End Sub

    'Update PointChart with the settings selected on the Chart Type, Titles, X Axis and Y Axis tabs.
    Private Sub UpdatePointChartSettings()

        'Update the Input Data settings:
        PointChart.InputDataType = InputDataType
        PointChart.InputDatabasePath = InputDatabasePath
        PointChart.InputDataDescr = InputDataDescr
        PointChart.InputQuery = InputQuery

        'Update chart properties:
        If txtSeriesName.Text <> "" Then
            PointChart.SeriesName = Trim(txtSeriesName.Text)
        End If

        If IsNothing(cmbXValues.SelectedItem) Then
            Message.AddWarning("The Field containing the XValues for the chart has not been selected." & vbCrLf)
        Else
            PointChart.XValuesFieldName = cmbXValues.SelectedItem.ToString
        End If

        If Trim(DataGridView1.Rows(0).Cells(1).Value) = "" Then
            Message.AddWarning("The Field containing the YValues for the chart has not been selected." & vbCrLf)
        Else
            PointChart.YValuesFieldName = DataGridView1.Rows(0).Cells(1).Value
        End If

        If Trim(DataGridView2.Rows(0).Cells(2).Value) = "" Then 'EmptyPointValue not specified
        Else
            PointChart.EmptyPointValue = DataGridView2.Rows(0).Cells(2).Value
        End If

        If Trim(DataGridView2.Rows(1).Cells(2).Value) = "" Then 'LabelStyle not specified
        Else
            PointChart.LabelStyle = DataGridView2.Rows(1).Cells(2).Value
        End If

        If Trim(DataGridView2.Rows(2).Cells(2).Value) = "" Then 'PixelPointDepth not specified
        Else
            PointChart.PixelPointDepth = DataGridView2.Rows(2).Cells(2).Value
        End If

        If Trim(DataGridView2.Rows(3).Cells(2).Value) = "" Then 'PixelPointGapDepth not specified
        Else
            PointChart.PixelPointGapDepth = DataGridView2.Rows(3).Cells(2).Value
        End If

        'Update Point Chart label settings:
        'PointChart.ChartLabel.FontName = FontDialog1.Font.Name
        'PointChart.ChartLabel.Size = FontDialog1.Font.Size
        'PointChart.ChartLabel.Bold = FontDialog1.Font.Bold
        'PointChart.ChartLabel.Italic = FontDialog1.Font.Italic
        'PointChart.ChartLabel.Strikeout = FontDialog1.Font.Strikeout
        'PointChart.ChartLabel.Underline = FontDialog1.Font.Underline
        'PointChart.ChartLabel.Text = txtChartTitle.Text
        PointChart.ChartLabel.FontName = txtChartTitle.Font.Name
        PointChart.ChartLabel.Size = txtChartTitle.Font.Size
        PointChart.ChartLabel.Bold = txtChartTitle.Font.Bold
        PointChart.ChartLabel.Italic = txtChartTitle.Font.Italic
        PointChart.ChartLabel.Strikeout = txtChartTitle.Font.Strikeout
        PointChart.ChartLabel.Underline = txtChartTitle.Font.Underline
        PointChart.ChartLabel.Text = txtChartTitle.Text
        If IsNothing(cmbAlignment.SelectedItem) Then
        Else
            PointChart.ChartLabel.Alignment = [Enum].Parse(GetType(ContentAlignment), cmbAlignment.SelectedItem.ToString)
        End If


        'Update X Axis settings:
        PointChart.XAxis.Title.FontName = txtXAxisTitle.Font.Name
        PointChart.XAxis.Title.Size = txtXAxisTitle.Font.Size
        PointChart.XAxis.Title.Bold = txtXAxisTitle.Font.Bold
        PointChart.XAxis.Title.Italic = txtXAxisTitle.Font.Italic
        PointChart.XAxis.Title.Strikeout = txtXAxisTitle.Font.Strikeout
        PointChart.XAxis.Title.Underline = txtXAxisTitle.Font.Underline
        PointChart.XAxis.Title.Text = txtXAxisTitle.Text

        If chkXAxisAutoMin.Checked = True Then
            PointChart.XAxis.AutoMinimum = True
        Else
            PointChart.XAxis.AutoMinimum = False
        End If

        If chkXAxisAutoMax.Checked = True Then
            PointChart.XAxis.AutoMaximum = True
        Else
            PointChart.XAxis.AutoMaximum = False
        End If

        PointChart.XAxis.Minimum = Val(txtXAxisMin.Text)
        PointChart.XAxis.Maximum = Val(txtXAxisMax.Text)

        If chkXAxisAutoAnnotInt.Checked = True Then
            PointChart.XAxis.Interval = 0 '0 indicates auto annotation.
            PointChart.XAxis.AutoInterval = True
        Else
            PointChart.XAxis.Interval = Val(txtXAxisAnnotInt.Text)
            PointChart.XAxis.AutoInterval = False
        End If

        If chkXAxisAutoMajGridInt.Checked = True Then
            PointChart.XAxis.MajorGridInterval = 0
            PointChart.XAxis.AutoMajorGridInterval = True
            'Message.Add("X Axis major grid interval set to auto." & vbCrLf)
        Else
            PointChart.XAxis.MajorGridInterval = Val(txtXAxisMajGridInt.Text)
            PointChart.XAxis.AutoMajorGridInterval = False
            'Message.Add("X Axis major grid interval set to: " & txtXAxisMajGridInt.Text & vbCrLf)
        End If

        'Update Y Axis settings:
        PointChart.YAxis.Title.FontName = txtYAxisTitle.Font.Name
        PointChart.YAxis.Title.Size = txtYAxisTitle.Font.Size
        PointChart.YAxis.Title.Bold = txtYAxisTitle.Font.Bold
        PointChart.YAxis.Title.Italic = txtYAxisTitle.Font.Italic
        PointChart.YAxis.Title.Strikeout = txtYAxisTitle.Font.Strikeout
        PointChart.YAxis.Title.Underline = txtYAxisTitle.Font.Underline

        PointChart.YAxis.Title.Text = txtYAxisTitle.Text

        If chkYAxisAutoMin.Checked = True Then
            PointChart.YAxis.AutoMinimum = True
        Else
            PointChart.YAxis.AutoMinimum = False
        End If

        If chkYAxisAutoMax.Checked = True Then
            PointChart.YAxis.AutoMaximum = True
        Else
            PointChart.YAxis.AutoMaximum = False
        End If

        PointChart.YAxis.Minimum = Val(txtYAxisMin.Text)
        PointChart.YAxis.Maximum = Val(txtYAxisMax.Text)

        If chkYAxisAutoAnnotInt.Checked = True Then
            PointChart.YAxis.Interval = 0 '0 indicates auto annotation.
            PointChart.YAxis.AutoInterval = True
        Else
            PointChart.YAxis.Interval = Val(txtYAxisAnnotInt.Text)
            PointChart.YAxis.AutoInterval = False
        End If

        If chkYAxisAutoMajGridInt.Checked = True Then
            PointChart.YAxis.MajorGridInterval = 0
            PointChart.YAxis.AutoMajorGridInterval = True
            'Message.Add("Y Axis major grid interval set to auto." & vbCrLf)
        Else
            PointChart.YAxis.MajorGridInterval = Val(txtYAxisMajGridInt.Text)
            PointChart.YAxis.AutoMajorGridInterval = False
            'Message.Add("Y Axis major grid interval set to:" & txtYAxisMajGridInt.Text & vbCrLf)
        End If

    End Sub

    Private Sub SavePointChart(ByVal FileName As String)
        'Save the point chart with the name FileName

        If Trim(FileName) = "" Then
            Message.AddWarning("No file name specified." & vbCrLf)
            Exit Sub
        End If

        Dim myFileName As String = Trim(FileName)

        If myFileName.EndsWith(".PointChart") Then
            PointChart.SaveFile(myFileName)
        Else
            If myFileName.Contains(".") Then
                Message.AddWarning("File does not have the extension '.PointChart" & vbCrLf)
                Exit Sub
            Else
                myFileName = myFileName & ".PointChart"
                PointChart.SaveFile(myFileName)
                txtChartFileName.Text = myFileName
            End If
        End If

    End Sub

    Private Sub OpenPointChart()
        'Find and open a Point chart file.
        Select Case Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a Stock Chart file from the project directory:
                OpenFileDialog1.InitialDirectory = Project.DataLocn.Path
                OpenFileDialog1.Filter = "Point Chart | *.PointChart"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    txtChartFileName.Text = FileName
                    PointChart.LoadFile(FileName)
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                'Select a Point Chart file from the project archive:
                'Show the zip archive file selection form:
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Project.DataLocn.Path
                Zip.SelectFile() 'Show the Select File form
                Zip.SelectFileForm.ApplicationName = Project.ApplicationName
                Zip.SelectFileForm.SettingsLocn = Project.SettingsLocn
                Zip.SelectFileForm.Show()
                Zip.SelectFileForm.RestoreFormSettings()
                Zip.SelectFileForm.FileExtension = ".PointChart"
                Zip.SelectFileForm.GetFileList()
                If Zip.SelectedFile <> "" Then
                    'A file has been selected
                    txtChartFileName.Text = Zip.SelectedFile
                    PointChart.LoadFile(Zip.SelectedFile)
                End If
        End Select
    End Sub

#End Region 'Point Chart Settings -----------------------------------------------------------------------------------------------------------------------------------------------------

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        'This code makes the combo box drop down as soon as the cell is clicked:

        If e.ColumnIndex = 2 Then
            DataGridView2.BeginEdit(True)
            If TypeOf (DataGridView2.EditingControl) Is System.Windows.Forms.DataGridViewComboBoxEditingControl Then
                DirectCast(DataGridView2.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'This code makes the combo box drop down as soon as the cell is clicked:

        If e.ColumnIndex = 1 Then
            DataGridView1.BeginEdit(True)
            If TypeOf (DataGridView1.EditingControl) Is System.Windows.Forms.DataGridViewComboBoxEditingControl Then
                DirectCast(DataGridView1.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
            End If
        End If
    End Sub

    Public Sub DrawChart()
        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                DrawPointChart()
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                DrawStockChart()
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select
    End Sub

    Private Sub SaveLastUsedChart()
        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                'DrawPointChart()
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                'DrawStockChart()
                'StockChart.FileLocation.SaveXmlData()
                StockChart.SaveFile("LastUsed.StockChart")
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select
    End Sub

    Private Sub RestoreLastUsedChart()
        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                'DrawPointChart()
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                'DrawStockChart()
                'StockChart.FileLocation.SaveXmlData()
                'StockChart.SaveFile("LastUsed.StockChart")
                StockChart.LoadFile("LastUsed.StockChart")
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select
    End Sub



    'Update the form with the current chart settings.
    Public Sub UpdateChartForm()

        'First check that data to chart has been loaded into the dataset:
        If ds.Tables.Count = 0 Then
            Message.AddWarning("No data has been selected for charting." & vbCrLf)
            'Exit Sub
        Else
            'Get the list of available fields from the dataset:
            GetFieldListFromDataset()
        End If

        'Show the selected XValues field: --------------------------------------------------------------------------------
        Dim I As Integer 'Loop index
        For I = 1 To cmbXValues.Items.Count
            If cmbXValues.Items(I - 1) = StockChart.XValuesFieldName Then
                cmbXValues.SelectedIndex = I - 1
            End If
        Next

        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                UpdatePointChartForm()
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                UpdateStockChartForm()
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select

    End Sub

    'Update the chart settings with the form selections.
    Public Sub UpdateChartSettings()

        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                UpdatePointChartSettings()
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                UpdateStockChartSettings()
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select

    End Sub

    'Update the available list of fields for plotting on the Y axis.
    Private Sub GetFieldListFromDataset()

        cboFieldSelections.Items.Clear()
        cmbXValues.Items.Clear()

        If ds.Tables(0).Columns.Count > 0 Then
            Dim I As Integer 'Loop index
            For I = 1 To ds.Tables(0).Columns.Count
                cboFieldSelections.Items.Add(ds.Tables(0).Columns(I - 1).ColumnName) 'ComboBox column used in DataGridView1 (Y Values to chart)
                cmbXValues.Items.Add(ds.Tables(0).Columns(I - 1).ColumnName) 'ComboBox used to select the Field to use along the X Axis
            Next
        End If
    End Sub

    Private Sub cmbChartType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChartType.SelectedIndexChanged
        'Chart type selected

        'List of chart types:
        'https://msdn.microsoft.com/en-us/library/dd489233(v=vs.110).aspx

        'Set the new ChartType property
        If cmbChartType.SelectedItem.ToString = "Area" Then
            ChartType = Charting.SeriesChartType.Area
        ElseIf cmbChartType.SelectedItem.ToString = "Bar" Then
            ChartType = Charting.SeriesChartType.Bar
        ElseIf cmbChartType.SelectedItem.ToString = "BoxPlot" Then
            Message.AddWarning("BoxPlot chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Bubble" Then
            Message.AddWarning("Bubble chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Candlestick" Then
            Message.AddWarning("Candlestick chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Column" Then
            Message.AddWarning("Column chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Doughnut" Then
            Message.AddWarning("Doughnut chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "ErrorBar" Then
            Message.AddWarning("ErrorBar chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "FastLine" Then
            Message.AddWarning("FastLine chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "FastPoint" Then
            Message.AddWarning("FastPoint chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Funnel" Then
            Message.AddWarning("Funnel chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Kagi" Then
            Message.AddWarning("Kagi chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Line" Then
            Message.AddWarning("Line chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Point" Then
            ChartType = Charting.SeriesChartType.Point
        ElseIf cmbChartType.SelectedItem.ToString = "PointAndFigure" Then
            Message.AddWarning("PointAndFigure chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Polar" Then
            Message.AddWarning("Polar chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Pyramid" Then
            Message.AddWarning("Pyramid chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Radar" Then
            Message.AddWarning("Radar chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "RangeBar" Then
            Message.AddWarning("RangeBar chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "RangeColumn" Then
            Message.AddWarning("RangeColumn chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Renko" Then
            Message.AddWarning("Renko chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Spline" Then
            Message.AddWarning("Spline chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "SplineArea" Then
            Message.AddWarning("SplineArea chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "SplineRange" Then
            Message.AddWarning("SplineRange chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "StackedArea" Then
            Message.AddWarning("StackedArea chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "StackedArea100" Then
            Message.AddWarning("StackedArea100 chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "StackedBar" Then
            Message.AddWarning("StackedBar chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "StackedBar100" Then
            Message.AddWarning("StackedBar100 chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "StackedColumn" Then
            Message.AddWarning("StackedColumn chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "StackedColumn100" Then
            Message.AddWarning("StackedColumn100 chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "StepLine" Then
            Message.AddWarning("StepLine chart not yet implemented")
        ElseIf cmbChartType.SelectedItem.ToString = "Stock" Then
            ChartType = Charting.SeriesChartType.Stock
        ElseIf cmbChartType.SelectedItem.ToString = "ThreeLineBreak" Then
            Message.AddWarning("ThreeLineBreak chart not yet implemented")
        Else
            Message.AddWarning("Unknown chart type: " & cmbChartType.SelectedItem.ToString)
        End If

        SetUpChartForm()
        UpdateChartForm()

    End Sub

    Private Sub btnChartTitleFont_Click(sender As Object, e As EventArgs) Handles btnChartTitleFont.Click
        'Edit chart title font
        FontDialog1.Font = txtChartTitle.Font
        FontDialog1.ShowDialog()
        txtChartTitle.Font = FontDialog1.Font
    End Sub

    Private Sub btnXAxisTitleFont_Click(sender As Object, e As EventArgs) Handles btnXAxisTitleFont.Click
        FontDialog1.Font = txtXAxisTitle.Font
        FontDialog1.ShowDialog()
        txtXAxisTitle.Font = FontDialog1.Font
    End Sub

    Private Sub chkXAxisAutoMin_CheckedChanged(sender As Object, e As EventArgs) Handles chkXAxisAutoMin.CheckedChanged
        If chkXAxisAutoMin.Checked = True Then
            txtXAxisMin.Enabled = False
        Else
            txtXAxisMin.Enabled = True
        End If
    End Sub

    Private Sub chkXAxisAutoMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkXAxisAutoMax.CheckedChanged
        If chkXAxisAutoMax.Checked = True Then
            txtXAxisMax.Enabled = False
        Else
            txtXAxisMax.Enabled = True
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        'The X Axis minimum value has been picked as a date.
        txtXAxisMin.Text = Str(DateValue(DateTimePicker2.Value).ToOADate)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        'The X Axis maximum value has been picked as a date.
        txtXAxisMax.Text = Str(DateValue(DateTimePicker1.Value).ToOADate)
    End Sub

    Private Sub chkXAxisAutoAnnotInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkXAxisAutoAnnotInt.CheckedChanged
        If chkXAxisAutoAnnotInt.Checked = True Then
            txtXAxisAnnotInt.Enabled = False
        Else
            txtXAxisAnnotInt.Enabled = True
        End If
    End Sub

    Private Sub chkXAxisAutoMajGridInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkXAxisAutoMajGridInt.CheckedChanged
        If chkXAxisAutoMajGridInt.Checked = True Then
            txtXAxisMajGridInt.Enabled = False
        Else
            txtXAxisMajGridInt.Enabled = True
        End If
    End Sub

    Private Sub btnYAxisTitleFont_Click(sender As Object, e As EventArgs) Handles btnYAxisTitleFont.Click
        FontDialog1.Font = txtYAxisTitle.Font
        FontDialog1.ShowDialog()
        txtYAxisTitle.Font = FontDialog1.Font
    End Sub

    Private Sub chkYAxisAutoMin_CheckedChanged(sender As Object, e As EventArgs) Handles chkYAxisAutoMin.CheckedChanged
        If chkYAxisAutoMin.Checked = True Then
            txtYAxisMin.Enabled = False
        Else
            txtYAxisMin.Enabled = True
        End If
    End Sub

    Private Sub chkYAxisAutoMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkYAxisAutoMax.CheckedChanged
        If chkYAxisAutoMax.Checked = True Then
            txtYAxisMax.Enabled = False
        Else
            txtYAxisMax.Enabled = True
        End If
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged
        'The Y Axis minimum value has been picked as a date.
        txtYAxisMin.Text = Str(DateValue(DateTimePicker3.Value).ToOADate)
    End Sub

    Private Sub DateTimePicker4_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker4.ValueChanged
        'The Y Axis maximum value has been picked as a date.
        txtYAxisMax.Text = Str(DateValue(DateTimePicker4.Value).ToOADate)
    End Sub

    Private Sub chkYAxisAutoAnnotInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkYAxisAutoAnnotInt.CheckedChanged
        If chkYAxisAutoAnnotInt.Checked = True Then
            txtYAxisAnnotInt.Enabled = False
        Else
            txtYAxisAnnotInt.Enabled = True
        End If
    End Sub

    Private Sub chkYAxisAutoMajGridInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkYAxisAutoMajGridInt.CheckedChanged
        If chkYAxisAutoMajGridInt.Checked = True Then
            txtYAxisMajGridInt.Enabled = False
        Else
            txtYAxisMajGridInt.Enabled = True
        End If
    End Sub

    Private Sub TabPage4_Leave(sender As Object, e As EventArgs) Handles TabPage4.Leave
        'Apply chart settings (leaving Chart Settings Tab)
        UpdateChartSettings()
        Message.Add("Leaving the Settings tab. " & vbCrLf)
    End Sub

#End Region 'Chart Settings Tab -------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Chart Tab" '-----------------------------------------------------------------------------------------------------------------------------------------------------------------

    'Open a chart.
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        'Open a chart.

        'Find and open a chart file.
        Select Case Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a Stock Chart file from the project directory:
                OpenFileDialog1.InitialDirectory = Project.DataLocn.Path
                'OpenFileDialog1.Filter = "Point Chart | *.PointChart"
                OpenFileDialog1.Filter = "Chart files | *.PointChart; *.StockChart"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    txtChartFileName.Text = FileName
                    'PointChart.LoadFile(FileName)
                    OpenChart(FileName)
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                'Select a Point Chart file from the project archive:
                'Show the zip archive file selection form:
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Project.DataLocn.Path
                Zip.SelectFile() 'Show the Select File form
                Zip.SelectFileForm.ApplicationName = Project.ApplicationName
                Zip.SelectFileForm.SettingsLocn = Project.SettingsLocn
                Zip.SelectFileForm.Show()
                Zip.SelectFileForm.RestoreFormSettings()
                'Zip.SelectFileForm.FileExtension = ".PointChart"
                Zip.SelectFileForm.FileExtensions = {".PointChart", ".StockChart"}
                Zip.SelectFileForm.GetFileList()
                If Zip.SelectedFile <> "" Then
                    'A file has been selected
                    txtChartFileName.Text = Zip.SelectedFile
                    'PointChart.LoadFile(Zip.SelectedFile)
                    OpenChart(Zip.SelectedFile)
                End If
        End Select
    End Sub

    Private Sub OpenChart(ByVal ChartFileName As String)
        If ChartFileName.EndsWith(".PointChart") Then
            ChartType = Charting.SeriesChartType.Point
            'SetUpChartForm()
            SetUpPointChartForm()
            'UpdateChartForm()
            'UpdatePointChartForm()
            PointChart.LoadFile(ChartFileName)
            PointChart.FileName = ChartFileName
            UpdatePointChartForm()
            UpdateLastUsedChart("Point", PointChart.FileName)
        ElseIf ChartFileName.EndsWith(".StockChart") Then
            ChartType = Charting.SeriesChartType.Stock
            'SetUpChartForm()
            SetUpStockChartForm()
            'UpdateChartForm()
            'UpdateStockChartForm()
            StockChart.LoadFile(ChartFileName)
            StockChart.FileName = ChartFileName
            UpdateStockChartForm()
            UpdateLastUsedChart("Stock", StockChart.FileName)
        End If

        If chkAutoDraw.Checked Then
            DrawChart()
        End If

    End Sub

    Private Sub Zip_FileSelected(FileName As String) Handles Zip.FileSelected
        txtChartFileName.Text = FileName
        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point

            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                StockChart.LoadFile(FileName)
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select

    End Sub

    'Save the current chart.
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim ChartFileName As String = Trim(txtChartFileName.Text)

        If ChartFileName = "" Then
            Message.AddWarning("No chart file name specified" & vbCrLf)
            Exit Sub
        End If

        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                'DrawPointChart()
                SavePointChart(ChartFileName)
                UpdateLastUsedChart("Point", PointChart.FileName)
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                SaveStockChart(ChartFileName)
                UpdateLastUsedChart("Stock", StockChart.FileName)
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select

    End Sub

    'Open the last used chart corresponding the the current ChartType
    Private Sub OpenLastUsedChart()
        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                If LastUsedCharts.ContainsKey(ChartType.ToString) Then
                    Message.Add("Last used Point Chart has file name: " & LastUsedCharts("Point"))
                    PointChart.LoadFile(LastUsedCharts("Point"))
                    UpdatePointChartForm()
                Else
                    Message.AddWarning("There are no recent charts of type: " & ChartType.ToString & vbCrLf)
                End If
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                If LastUsedCharts.ContainsKey(ChartType.ToString) Then
                    Message.Add("Last used Stock Chart has file name: " & LastUsedCharts("Stock"))
                    StockChart.LoadFile(LastUsedCharts("Stock"))
                    UpdateStockChartForm()
                Else
                    Message.AddWarning("There are no recent charts of type: " & ChartType.ToString & vbCrLf)
                End If
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select
    End Sub

    Private Sub btnDrawChart_Click(sender As Object, e As EventArgs) Handles btnDrawChart.Click
        'Apply the chart settings - this updates the chart display.
        DrawChart()
    End Sub

    Private Sub UpdateChartQuery()
        'Update the Chart Query with the current InputQuery 

        Select Case ChartType
            Case Charting.SeriesChartType.Area

            Case Charting.SeriesChartType.Bar

            Case Charting.SeriesChartType.BoxPlot

            Case Charting.SeriesChartType.Bubble

            Case Charting.SeriesChartType.Candlestick

            Case Charting.SeriesChartType.Column

            Case Charting.SeriesChartType.Doughnut

            Case Charting.SeriesChartType.ErrorBar

            Case Charting.SeriesChartType.FastLine

            Case Charting.SeriesChartType.FastPoint

            Case Charting.SeriesChartType.Funnel

            Case Charting.SeriesChartType.Kagi

            Case Charting.SeriesChartType.Line

            Case Charting.SeriesChartType.Pie

            Case Charting.SeriesChartType.Point
                PointChart.InputQuery = InputQuery
            Case Charting.SeriesChartType.PointAndFigure

            Case Charting.SeriesChartType.Polar

            Case Charting.SeriesChartType.Pyramid

            Case Charting.SeriesChartType.Radar

            Case Charting.SeriesChartType.Range

            Case Charting.SeriesChartType.RangeBar

            Case Charting.SeriesChartType.RangeColumn

            Case Charting.SeriesChartType.Renko

            Case Charting.SeriesChartType.Spline

            Case Charting.SeriesChartType.SplineArea

            Case Charting.SeriesChartType.SplineRange

            Case Charting.SeriesChartType.StackedArea

            Case Charting.SeriesChartType.StackedArea100

            Case Charting.SeriesChartType.StackedBar

            Case Charting.SeriesChartType.StackedBar100

            Case Charting.SeriesChartType.StackedColumn

            Case Charting.SeriesChartType.StackedColumn100

            Case Charting.SeriesChartType.StepLine

            Case Charting.SeriesChartType.Stock
                StockChart.InputQuery = InputQuery
            Case Charting.SeriesChartType.ThreeLineBreak

        End Select
    End Sub

    'Update Last Used Chart
    Private Sub UpdateLastUsedChart(ByVal ChartType As String, ByVal ChartFileName As String)
        If LastUsedCharts.ContainsKey(ChartType) Then
            LastUsedCharts(ChartType) = ChartFileName 'Update the Chart File Name corresponding to the Chart Type
            Message.Add("Updated LastUsedCharts dictionary entry for Chart Type: " & ChartType & " to the Chart File Name: " & ChartFileName & vbCrLf)
        Else
            LastUsedCharts.Add(ChartType, ChartFileName) 'Add a new dictionary entry
            Message.Add("Added a new LastUsedCharts dictionary entry for Chart Type: " & ChartType & " with the Chart File Name: " & ChartFileName & vbCrLf)
        End If
    End Sub

    Private Sub btnNewChartWindow_Click(sender As Object, e As EventArgs) Handles btnNewChartWindow.Click
        'Open a form to view a chart.
        OpenNewCharForm()
    End Sub

    Private Sub OpenNewCharForm()
        'Code to show multiple instances if the Chart form:
        Chart = New frmChart

        If ChartList.Count = 0 Then
            ChartList.Add(Chart)
            ChartList(0).FormNo = 0
            ChartList(0).Show
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To ChartList.Count - 1 'Check if there are closed forms in ChartList. They can be re-used.
                If IsNothing(ChartList(I)) Then
                    ChartList(I) = Chart
                    ChartList(I).FormNo = I
                    ChartList(I).Show
                    FormAdded = True
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to ChartList.
                Dim FormNo As Integer
                ChartList.Add(Chart)
                FormNo = ChartList.Count - 1
                ChartList(FormNo).FormNo = FormNo
                ChartList(FormNo).Show
            End If
        End If
    End Sub

    Public Sub ChartFormClosed()
        'This subroutine is called when the Chart form has been closed.
        'The subroutine is usually called from the FormClosed event of the Chart form.
        'The Chart form may have multiple instances.
        'The ClosedFormNo property should contains the number of the instance of the Chart form.
        'This property should be updated by the Chart form when it is being closed.
        'The ClosedFormNo property value is used to determine which element in ChartList should be set to Nothing.

        If IsNothing(ChartList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            ChartList(ClosedFormNo) = Nothing
        End If
    End Sub


#End Region 'Chart Tab ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Project Events Code"

    Private Sub Project_Message(Msg As String) Handles Project.Message
        'Display the Project message:
        Message.Add(Msg & vbCrLf)
    End Sub

    Private Sub Project_ErrorMessage(Msg As String) Handles Project.ErrorMessage
        'Display the Project error message:
        Message.AddWarning(Msg & vbCrLf)
    End Sub

    Private Sub Project_Closing() Handles Project.Closing
        'The current project is closing.

        SaveFormSettings() 'Save the form settings - they are saved in the Project before is closes.
        SaveProjectSettings() 'Update this subroutine if project settings need to be saved.

        'Save the current project usage information:
        Project.Usage.SaveUsageInfo()
    End Sub

    Private Sub Project_Selected() Handles Project.Selected
        'A new project has been selected.

        RestoreFormSettings()
        Project.ReadProjectInfoFile()

        'ADDED 2Feb19:
        Project.ReadParameters()
        Project.ReadParentParameters()
        If Project.ParentParameterExists("AppNetName") Then
            'Project.Parameter("AppNetName") = Project.ParentParameter("AppNetName")
            Project.AddParameter("AppNetName", Project.ParentParameter("AppNetName").Value, Project.ParentParameter("AppNetName").Description) 'AddParameter will update the parameter if it already exists.
            AppNetName = Project.Parameter("AppNetName").Value
        Else
            'AppNetName = ""
            AppNetName = Project.GetParameter("AppNetName")
        End If

        Project.LockProject() 'Lock the project while it is open in this application.

        Project.Usage.StartTime = Now

        ApplicationInfo.SettingsLocn = Project.SettingsLocn
        Message.SettingsLocn = Project.SettingsLocn

        'Restore the new project settings:
        RestoreProjectSettings() 'Update this subroutine if project settings need to be restored.

        'Show the project information:
        txtProjectName.Text = Project.Name
        txtProjectDescription.Text = Project.Description
        Select Case Project.Type
            Case ADVL_Utilities_Library_1.Project.Types.Directory
                txtProjectType.Text = "Directory"
            Case ADVL_Utilities_Library_1.Project.Types.Archive
                txtProjectType.Text = "Archive"
            Case ADVL_Utilities_Library_1.Project.Types.Hybrid
                txtProjectType.Text = "Hybrid"
            Case ADVL_Utilities_Library_1.Project.Types.None
                txtProjectType.Text = "None"
        End Select

        txtCreationDate.Text = Format(Project.CreationDate, "d-MMM-yyyy H:mm:ss")
        txtLastUsed.Text = Format(Project.Usage.LastUsed, "d-MMM-yyyy H:mm:ss")
        Select Case Project.SettingsLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSettingsLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSettingsLocationType.Text = "Archive"
        End Select
        txtSettingsLocationPath.Text = Project.SettingsLocn.Path
        Select Case Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtDataLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtDataLocationType.Text = "Archive"
        End Select
        txtDataLocationPath.Text = Project.DataLocn.Path

    End Sub

#End Region 'Project Events Code

#Region " Online/Offline Code"

    Private Sub btnOnline_Click(sender As Object, e As EventArgs) Handles btnOnline.Click
        'Connect to or disconnect from the Message System (ComNet).
        If ConnectedToComNet = False Then
            ConnectToComNet()
        Else
            DisconnectFromComNet()
        End If
    End Sub

    Private Sub ConnectToComNet()
        'Connect to the Message System. (ComNet)

        'Dim Result As Boolean

        If IsNothing(client) Then
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        If ComNetRunning() Then
            'The Message Service is Running.
        Else 'The Message Service is NOT running'
            'Start the Message Service:
            If System.IO.File.Exists(MsgServiceExePath) Then 'OK to start the Message Service application:
                Shell(Chr(34) & MsgServiceExePath & Chr(34), AppWinStyle.NormalFocus) 'Start Message Service application with no argument
            Else
                'Incorrect Message Service Executable path.
            End If
        End If



        If client.State = ServiceModel.CommunicationState.Faulted Then
            Message.AddWarning("Client state is faulted. Connection not made!" & vbCrLf)
        Else
            Try
                'client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 8) 'Temporarily set the send timeaout to 8 seconds
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds

                'Result = client.Connect(ApplicationInfo.Name, ServiceReference1.clsConnectionAppTypes.Application, False, False) 'Application Name is "Application_Template"
                'appName, appType, getAllWarnings, getAllMessages
                ConnectionName = ApplicationInfo.Name 'This name will be modified if it is already used in an existing connection.
                'ConnectionName = client.Connect(ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.SettingsLocn.Type, Project.SettingsLocn.Path, ServiceReference1.clsConnectionAppTypes.Application, False, False)
                ConnectionName = client.Connect(AppNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False) 'UPDATED 2Feb19


                If ConnectionName <> "" Then
                    'Message.Add("Connected to the Application Network as " & ApplicationInfo.Name & vbCrLf)
                    'Message.Add("Connected to the Application Network as " & ConnectionName & vbCrLf)
                    Message.Add("Connected to the Communication Network as " & ConnectionName & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
                    btnOnline.Text = "Online"
                    btnOnline.ForeColor = Color.ForestGreen
                    'ConnectedToAppnet = True
                    ConnectedToComNet = True
                    SendApplicationInfo()
                    client.GetMessageServiceAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).
                Else
                    'Message.Add("Connection to the Application Network failed!" & vbCrLf)
                    Message.Add("Connection to the Communication Network failed!" & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
                End If
            Catch ex As System.TimeoutException
                'Message.Add("Timeout error. Check if the Application Network is running." & vbCrLf)
                Message.Add("Timeout error. Check if the Communication Network (Message Service) is running." & vbCrLf)
            Catch ex As Exception
                Message.Add("Error message: " & ex.Message & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
            End Try
        End If

    End Sub

    Private Sub ConnectToComNet(ByVal ConnName As String)
        'Connect to the Communication Network with the connection name ConnName.

        If ConnectedToComNet = False Then
            Dim Result As Boolean

            If IsNothing(client) Then
                client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
            End If

            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.AddWarning("client state is faulted. Connection not made!" & vbCrLf)
            Else
                Try
                    'client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 8) 'Temporarily set the send timeaout to 8 seconds
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds
                    'ConnectionName = ApplicationInfo.Name 'This name will be modified if it is already used in an existing connection.
                    ConnectionName = ConnName 'This name will be modified if it is already used in an existing connection.
                    'ConnectionName = client.Connect(ApplicationInfo.Name, ConnectionName, Project.Name, Project.DataLocn.Path, ServiceReference1.clsConnectionAppTypes.Application, False, False)
                    'ConnectionName = client.Connect(ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.SettingsLocn.Type, Project.SettingsLocn.Path, ServiceReference1.clsConnectionAppTypes.Application, False, False)
                    ConnectionName = client.Connect(AppNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False) 'UPDATED 2Feb19


                    If ConnectionName <> "" Then
                        'Message.Add("Connected to the Application Network as " & ConnectionName & vbCrLf)
                        Message.Add("Connected to the Communication Network as " & ConnectionName & vbCrLf)
                        client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
                        btnOnline.Text = "Online"
                        btnOnline.ForeColor = Color.ForestGreen
                        'ConnectedToAppnet = True
                        ConnectedToComNet = True
                        SendApplicationInfo()
                        client.GetMessageServiceAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).
                    Else
                        'Message.Add("Connection to the Application Network failed!" & vbCrLf)
                        Message.Add("Connection to the Communication Network failed!" & vbCrLf)
                        client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
                    End If
                Catch ex As System.TimeoutException
                    'Message.Add("Timeout error. Check if the Application Network is running." & vbCrLf)
                    Message.Add("Timeout error. Check if the Communication Network is running." & vbCrLf)
                Catch ex As Exception
                    Message.Add("Error message: " & ex.Message & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
                End Try
            End If
        Else
            'Message.AddWarning("Already connected to the Application Network." & vbCrLf)
            Message.AddWarning("Already connected to the Communication Network." & vbCrLf)
        End If

    End Sub

    'Private Sub DisconnectFromAppNet()
    Private Sub DisconnectFromComNet()
        'Disconnect from the Communication Network.

        'Dim Result As Boolean
        If ConnectedToComNet = True Then
            If ConnectedToComNet = True Then
                If IsNothing(client) Then
                    'Message.Add("Already disconnected from the Application Network." & vbCrLf)
                    Message.Add("Already disconnected from the Communication Network." & vbCrLf)
                    btnOnline.Text = "Offline"
                    btnOnline.ForeColor = Color.Red
                    'ConnectedToAppnet = False
                    ConnectedToComNet = False
                    ConnectionName = ""
                Else
                    If client.State = ServiceModel.CommunicationState.Faulted Then
                        Message.Add("client state is faulted." & vbCrLf)
                        ConnectionName = ""
                    Else
                        Try
                            'Message.Add("Running client.Disconnect(ApplicationName)   ApplicationName = " & ApplicationInfo.Name & vbCrLf)
                            'client.Disconnect(ApplicationInfo.Name) 'NOTE: If Application Network has closed, this application freezes at this line! Try Catch EndTry added to fix this.
                            'client.Disconnect(ApplicationInfo.Name)
                            client.Disconnect(AppNetName, ConnectionName) 'UPDATED 2Feb19
                            btnOnline.Text = "Offline"
                            btnOnline.ForeColor = Color.Red
                            'ConnectedToAppnet = False
                            ConnectedToComNet = False
                            ConnectionName = ""
                            'Message.Add("Disconnected from the Application Network." & vbCrLf)
                            Message.Add("Disconnected from the Communication Network." & vbCrLf)
                        Catch ex As Exception
                            'Message.SetWarningStyle()
                            'Message.Add("Error disconnecting from Application Network: " & ex.Message & vbCrLf)
                            Message.AddWarning("Error disconnecting from Communication Network: " & ex.Message & vbCrLf)
                        End Try
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub SendApplicationInfo()
        'Send the application information to the Administrator connections.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to send application information.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                Dim applicationInfo As New XElement("ApplicationInfo")
                Dim name As New XElement("Name", Me.ApplicationInfo.Name)
                applicationInfo.Add(name)

                Dim text As New XElement("Text", "Charts")
                applicationInfo.Add(text)

                Dim exePath As New XElement("ExecutablePath", Me.ApplicationInfo.ExecutablePath)
                applicationInfo.Add(exePath)

                Dim directory As New XElement("Directory", Me.ApplicationInfo.ApplicationDir)
                applicationInfo.Add(directory)
                Dim description As New XElement("Description", Me.ApplicationInfo.Description)
                applicationInfo.Add(description)
                xmessage.Add(applicationInfo)
                doc.Add(xmessage)

                'Show the message sent to AppNet:
                'Message.XAddText("Message sent to " & "ApplicationNetwork" & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddText("Message sent to " & "MessageService" & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line

                'client.SendMessage("ApplicationNetwork", doc.ToString)
                'client.SendMessage("MessageService", doc.ToString)
                client.SendMessage("", "MessageService", doc.ToString) 'UPDATED 2Feb19
            End If
        End If

    End Sub

    Private Function ComNetRunning() As Boolean
        'Return True if ComNet (Message Service) is running.
        If System.IO.File.Exists(MsgServiceAppPath & "\Application.Lock") Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region 'Online/Offline code

#Region " Process XMessages"

    Private Sub XMsg_Instruction(Info As String, Locn As String) Handles XMsg.Instruction
        'Process an XMessage instruction.
        'An XMessage is a simplified XSequence. It is used to exchange information between Andorville™ applications.
        '
        'An XSequence file is an AL-H7™ Information Vector Sequence stored in an XML format.
        'AL-H7™ is the name of a programming system that uses sequences of information and location value pairs to store data items or processing steps.
        'A single information and location value pair is called a knowledge element (or noxel).
        'Any program, mathematical expression or data set can be expressed as an Information Vector Sequence.

        'Add code here to process the XMessage instructions.
        'See other Andorville™ applciations for examples.

        If IsDBNull(Info) Then
            Info = ""
        End If

        Select Case Locn

            Case "ClientAppNetName"
                ClientAppNetName = Info 'The name of the Client Application Network requesting service. ADDED 2Feb19.

            Case "ClientName"
                ClientAppName = Info 'The name of the Client requesting service.

            Case "ClientConnectionName"
                ClientConnName = Info 'The name of the client requesting service.

            Case "ClientLocn" 'The Location within the Client requesting service.
                'TEST: Add Status OK element when the Client Location is changed:
                Dim statusOK As New XElement("Status", "OK")
                xlocns(xlocns.Count - 1).Add(statusOK)

                xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the instructions for the last location to the reply xmessage
                xlocns.Add(New XElement(Info)) 'Start the new location instructions

            Case "Main"
                 'Blank message - do nothing.

            Case "Main:Status"
                Select Case Info
                    Case "OK"
                        'Main instructions completed OK
                End Select

            Case "Command"
                Select Case Info
                    Case "GetStockChartSettings"
                        GetStockChartSettingsForClient()
                    Case "GetPointChartSettings"
                        GetPointChartSettingsForClient()
                    'Case "ConnectToAppNet" 'Startup Command
                    Case "ConnectToComNet" 'Startup Command
                        'If ConnectedToAppnet = False Then
                        If ConnectedToComNet = False Then
                            'ConnectToAppNet()
                            ConnectToComNet()
                        End If
                End Select

         'Stock Chart instructions: --------------------------------------------------------------------------------------------------------------------------------

            Case "StockChartSettings:ChartType"
                Select Case Info
                    Case "Stock"
                        ChartType = Charting.SeriesChartType.Stock
                        SetUpStockChartForm()
                End Select

            Case "StockChartSettings:InputData:Type"
                Select Case Info
                    Case "Database"
                        rbDatabase.Checked = True
                        StockChart.InputDataType = "Database"
                End Select

            Case "StockChartSettings:InputData:DatabasePath"
                InputDatabasePath = Info
                StockChart.InputDatabasePath = Info

            Case "StockChartSettings:InputData:DataDescription"
                InputDataDescr = Info
                StockChart.InputDataDescr = Info

            Case "StockChartSettings:InputData:DatabaseQuery"
                InputQuery = Info
                StockChart.InputQuery = Info

            Case "StockChartSettings:ChartProperties:XValuesFieldName"
                StockChart.XValuesFieldName = Info

            Case "StockChartSettings:ChartProperties:SeriesName"
                StockChart.SeriesName = Info

            Case "StockChartSettings:ChartProperties:YValuesHighFieldName"
                StockChart.YValuesHighFieldName = Info

            Case "StockChartSettings:ChartProperties:YValuesLowFieldName"
                StockChart.YValuesLowFieldName = Info

            Case "StockChartSettings:ChartProperties:YValuesOpenFieldName"
                StockChart.YValuesOpenFieldName = Info

            Case "StockChartSettings:ChartProperties:YValuesCloseFieldName"
                StockChart.YValuesCloseFieldName = Info

            Case "StockChartSettings:ChartTitle:LabelName"
                StockChart.ChartLabel.Name = Info

            Case "StockChartSettings:ChartTitle:Text"
                StockChart.ChartLabel.Text = Info

            Case "StockChartSettings:ChartTitle:FontName"
                StockChart.ChartLabel.FontName = Info

            Case "StockChartSettings:ChartTitle:Color"
                StockChart.ChartLabel.Color = Info

            Case "StockChartSettings:ChartTitle:Size"
                StockChart.ChartLabel.Size = Info

            Case "StockChartSettings:ChartTitle:Bold"
                StockChart.ChartLabel.Bold = Info

            Case "StockChartSettings:ChartTitle:Italic"
                StockChart.ChartLabel.Italic = Info

            Case "StockChartSettings:ChartTitle:Underline"
                StockChart.ChartLabel.Underline = Info

            Case "StockChartSettings:ChartTitle:Strikeout"
                StockChart.ChartLabel.Strikeout = Info

            Case "StockChartSettings:ChartTitle:Alignment"
                Select Case Info
                    Case "BottomCenter"
                        StockChart.ChartLabel.Alignment = ContentAlignment.BottomCenter
                    Case "BottomLeft"
                        StockChart.ChartLabel.Alignment = ContentAlignment.BottomLeft
                    Case "BottomRight"
                        StockChart.ChartLabel.Alignment = ContentAlignment.BottomRight
                    Case "MiddleCenter"
                        StockChart.ChartLabel.Alignment = ContentAlignment.MiddleCenter
                    Case "MiddleLeft"
                        StockChart.ChartLabel.Alignment = ContentAlignment.MiddleLeft
                    Case "MiddleRight"
                        StockChart.ChartLabel.Alignment = ContentAlignment.MiddleRight
                    Case "TopCenter"
                        StockChart.ChartLabel.Alignment = ContentAlignment.TopCenter
                    Case "TopLeft"
                        StockChart.ChartLabel.Alignment = ContentAlignment.TopLeft
                    Case "TopRight"
                        StockChart.ChartLabel.Alignment = ContentAlignment.TopRight
                    Case Else
                        Message.AddWarning("Unknown Chart Title Alignment: " & Info & vbCrLf)
                        StockChart.ChartLabel.Alignment = ContentAlignment.TopCenter
                End Select

            Case "StockChartSettings:XAxis:TitleText"
                StockChart.XAxis.Title.Text = Info

            Case "StockChartSettings:XAxis:TitleFontName"
                StockChart.XAxis.Title.FontName = Info

            Case "StockChartSettings:XAxis:TitleColor"
                StockChart.XAxis.Title.Color = Info

            Case "StockChartSettings:XAxis:TitleSize"
                StockChart.XAxis.Title.Size = Info

            Case "StockChartSettings:XAxis:TitleBold"
                StockChart.XAxis.Title.Bold = Info

            Case "StockChartSettings:XAxis:TitleItalic"
                StockChart.XAxis.Title.Italic = Info

            Case "StockChartSettings:XAxis:TitleUnderline"
                StockChart.XAxis.Title.Underline = Info

            Case "StockChartSettings:XAxis:TitleStrikeout"
                StockChart.XAxis.Title.Strikeout = Info

            Case "StockChartSettings:XAxis:TitleAlignment"
                Select Case Info
                    Case "Center"
                        StockChart.XAxis.TitleAlignment = StringAlignment.Center
                    Case "Far"
                        StockChart.XAxis.TitleAlignment = StringAlignment.Far
                    Case "Near"
                        StockChart.XAxis.TitleAlignment = StringAlignment.Near
                    Case Else
                        Message.AddWarning("Unknown Chart X Axis Title Alignment: " & Info & vbCrLf)
                        StockChart.XAxis.TitleAlignment = StringAlignment.Center
                End Select

            Case "StockChartSettings:XAxis:AutoMinimum"
                StockChart.XAxis.AutoMinimum = Info

            Case "StockChartSettings:XAxis:Minimum"
                StockChart.XAxis.Minimum = Info

            Case "StockChartSettings:XAxis:AutoMaximum"
                StockChart.XAxis.AutoMaximum = Info

            Case "StockChartSettings:XAxis:Maximum"
                StockChart.XAxis.Maximum = Info

            Case "StockChartSettings:XAxis:AutoInterval"
                StockChart.XAxis.AutoInterval = Info

            Case "StockChartSettings:XAxis:MajorGridInterval"
                StockChart.XAxis.MajorGridInterval = Info

            Case "StockChartSettings:XAxis:AutoMajorGridInterval"
                StockChart.XAxis.AutoMajorGridInterval = Info

            Case "StockChartSettings:YAxis:TitleText"
                StockChart.YAxis.Title.Text = Info

            Case "StockChartSettings:YAxis:TitleFontName"
                StockChart.YAxis.Title.FontName = Info

            Case "StockChartSettings:YAxis:TitleColor"
                StockChart.YAxis.Title.Color = Info

            Case "StockChartSettings:YAxis:TitleSize"
                StockChart.YAxis.Title.Size = Info

            Case "StockChartSettings:YAxis:TitleBold"
                StockChart.YAxis.Title.Bold = Info

            Case "StockChartSettings:YAxis:TitleItalic"
                StockChart.YAxis.Title.Italic = Info

            Case "StockChartSettings:YAxis:TitleUnderline"
                StockChart.YAxis.Title.Underline = Info

            Case "StockChartSettings:YAxis:TitleStrikeout"
                StockChart.YAxis.Title.Strikeout = Info

            Case "StockChartSettings:YAxis:TitleAlignment"
                'StockChart.YAxis.TitleAlignment = Info
                Select Case Info
                    Case "Center"
                        StockChart.YAxis.TitleAlignment = StringAlignment.Center
                    Case "Far"
                        StockChart.YAxis.TitleAlignment = StringAlignment.Far
                    Case "Near"
                        StockChart.YAxis.TitleAlignment = StringAlignment.Near
                    Case Else
                        Message.AddWarning("Unknown Chart Y Axis Title Alignment: " & Info & vbCrLf)
                        StockChart.YAxis.TitleAlignment = StringAlignment.Center
                End Select

            Case "StockChartSettings:YAxis:AutoMinimum"
                StockChart.YAxis.AutoMinimum = Info

            Case "StockChartSettings:YAxis:Minimum"
                StockChart.YAxis.Minimum = Info

            Case "StockChartSettings:YAxis:AutoMaximum"
                StockChart.YAxis.AutoMaximum = Info

            Case "StockChartSettings:YAxis:Maximum"
                StockChart.YAxis.Maximum = Info

            Case "StockChartSettings:YAxis:AutoInterval"
                StockChart.YAxis.AutoInterval = Info

            Case "StockChartSettings:YAxis:MajorGridInterval"
                StockChart.YAxis.MajorGridInterval = Info

            Case "StockChartSettings:YAxis:AutoMajorGridInterval"
                StockChart.YAxis.AutoMajorGridInterval = Info

            Case "StockChartSettings:Command"
                Select Case Info
                    Case "DrawChart"
                        UpdateStockChartForm()
                        DrawChart()
                    Case "ClearChart"
                        ClearChart()
                End Select


           'Point Chart instructions: --------------------------------------------------------------------------------------------------------------------------------

            Case "PointChartSettings:ChartType"
                Select Case Info
                    Case "Point"
                        ChartType = Charting.SeriesChartType.Point
                        SetUpPointChartForm()
                End Select

            Case "PointChartSettings:InputData:Type"
                Select Case Info
                    Case "Database"
                        rbDatabase.Checked = True
                        PointChart.InputDataType = "Database"
                End Select

            Case "PointChartSettings:InputData:DatabasePath"
                InputDatabasePath = Info
                PointChart.InputDatabasePath = Info

            Case "PointChartSettings:InputData:DataDescription"
                InputDataDescr = Info
                PointChart.InputDataDescr = Info

            Case "PointChartSettings:InputData:DatabaseQuery"
                InputQuery = Info
                PointChart.InputQuery = Info

            Case "PointChartSettings:ChartProperties:XValuesFieldName"
                PointChart.XValuesFieldName = Info

            Case "PointChartSettings:ChartProperties:YValuesFieldName"
                PointChart.YValuesFieldName = Info

            Case "PointChartSettings:ChartProperties:SeriesName"
                PointChart.SeriesName = Info

            Case "PointChartSettings:ChartTitle:LabelName"
                PointChart.ChartLabel.Name = Info

            Case "PointChartSettings:ChartTitle:Text"
                PointChart.ChartLabel.Text = Info

            Case "PointChartSettings:ChartTitle:FontName"
                PointChart.ChartLabel.FontName = Info

            Case "PointChartSettings:ChartTitle:Color"
                PointChart.ChartLabel.Color = Info

            Case "PointChartSettings:ChartTitle:Size"
                PointChart.ChartLabel.Size = Info

            Case "PointChartSettings:ChartTitle:Bold"
                PointChart.ChartLabel.Bold = Info

            Case "PointChartSettings:ChartTitle:Italic"
                PointChart.ChartLabel.Italic = Info

            Case "PointChartSettings:ChartTitle:Underline"
                PointChart.ChartLabel.Underline = Info

            Case "PointChartSettings:ChartTitle:Strikeout"
                PointChart.ChartLabel.Strikeout = Info

            Case "PointChartSettings:ChartTitle:Alignment"
                Select Case Info
                    Case "BottomCenter"
                        PointChart.ChartLabel.Alignment = ContentAlignment.BottomCenter
                    Case "BottomLeft"
                        PointChart.ChartLabel.Alignment = ContentAlignment.BottomLeft
                    Case "BottomRight"
                        PointChart.ChartLabel.Alignment = ContentAlignment.BottomRight
                    Case "MiddleCenter"
                        PointChart.ChartLabel.Alignment = ContentAlignment.MiddleCenter
                    Case "MiddleLeft"
                        PointChart.ChartLabel.Alignment = ContentAlignment.MiddleLeft
                    Case "MiddleRight"
                        PointChart.ChartLabel.Alignment = ContentAlignment.MiddleRight
                    Case "TopCenter"
                        PointChart.ChartLabel.Alignment = ContentAlignment.TopCenter
                    Case "TopLeft"
                        PointChart.ChartLabel.Alignment = ContentAlignment.TopLeft
                    Case "TopRight"
                        PointChart.ChartLabel.Alignment = ContentAlignment.TopRight
                    Case Else
                        Message.AddWarning("Unknown Chart Title Alignment: " & Info & vbCrLf)
                        PointChart.ChartLabel.Alignment = ContentAlignment.TopCenter
                End Select




            Case "PointChartSettings:XAxis:TitleText"
                PointChart.XAxis.Title.Text = Info

            Case "PointChartSettings:XAxis:TitleFontName"
                PointChart.XAxis.Title.FontName = Info

            Case "PointChartSettings:XAxis:TitleColor"
                PointChart.XAxis.Title.Color = Info

            Case "PointChartSettings:XAxis:TitleSize"
                PointChart.XAxis.Title.Size = Info

            Case "PointChartSettings:XAxis:TitleBold"
                PointChart.XAxis.Title.Bold = Info

            Case "PointChartSettings:XAxis:TitleItalic"
                PointChart.XAxis.Title.Italic = Info

            Case "PointChartSettings:XAxis:TitleUnderline"
                PointChart.XAxis.Title.Underline = Info

            Case "PointChartSettings:XAxis:TitleStrikeout"
                PointChart.XAxis.Title.Strikeout = Info

            Case "PointChartSettings:XAxis:TitleAlignment"
                Select Case Info
                    Case "Center"
                        PointChart.XAxis.TitleAlignment = StringAlignment.Center
                    Case "Far"
                        PointChart.XAxis.TitleAlignment = StringAlignment.Far
                    Case "Near"
                        PointChart.XAxis.TitleAlignment = StringAlignment.Near
                    Case Else
                        Message.AddWarning("Unknown Chart X Axis Title Alignment: " & Info & vbCrLf)
                        PointChart.XAxis.TitleAlignment = StringAlignment.Center
                End Select

            Case "PointChartSettings:XAxis:AutoMinimum"
                PointChart.XAxis.AutoMinimum = Info

            Case "PointChartSettings:XAxis:Minimum"
                PointChart.XAxis.Minimum = Info

            Case "PointChartSettings:XAxis:AutoMaximum"
                PointChart.XAxis.AutoMaximum = Info

            Case "PointChartSettings:XAxis:Maximum"
                PointChart.XAxis.Maximum = Info

            Case "PointChartSettings:XAxis:AutoInterval"
                PointChart.XAxis.AutoInterval = Info

            Case "PointChartSettings:XAxis:MajorGridInterval"
                PointChart.XAxis.MajorGridInterval = Info

            Case "PointChartSettings:XAxis:AutoMajorGridInterval"
                PointChart.XAxis.AutoMajorGridInterval = Info

            Case "PointChartSettings:YAxis:TitleText"
                PointChart.YAxis.Title.Text = Info

            Case "PointChartSettings:YAxis:TitleFontName"
                PointChart.YAxis.Title.FontName = Info

            Case "PointChartSettings:YAxis:TitleColor"
                PointChart.YAxis.Title.Color = Info

            Case "PointChartSettings:YAxis:TitleSize"
                PointChart.YAxis.Title.Size = Info

            Case "PointChartSettings:YAxis:TitleBold"
                PointChart.YAxis.Title.Bold = Info

            Case "PointChartSettings:YAxis:TitleItalic"
                PointChart.YAxis.Title.Italic = Info

            Case "PointChartSettings:YAxis:TitleUnderline"
                PointChart.YAxis.Title.Underline = Info

            Case "PointChartSettings:YAxis:TitleStrikeout"
                PointChart.YAxis.Title.Strikeout = Info

            Case "PointChartSettings:YAxis:TitleAlignment"
                'StockChart.YAxis.TitleAlignment = Info
                Select Case Info
                    Case "Center"
                        PointChart.YAxis.TitleAlignment = StringAlignment.Center
                    Case "Far"
                        PointChart.YAxis.TitleAlignment = StringAlignment.Far
                    Case "Near"
                        PointChart.YAxis.TitleAlignment = StringAlignment.Near
                    Case Else
                        Message.AddWarning("Unknown Chart Y Axis Title Alignment: " & Info & vbCrLf)
                        PointChart.YAxis.TitleAlignment = StringAlignment.Center
                End Select

            Case "PointChartSettings:YAxis:AutoMinimum"
                PointChart.YAxis.AutoMinimum = Info

            Case "PointChartSettings:YAxis:Minimum"
                PointChart.YAxis.Minimum = Info

            Case "PointChartSettings:YAxis:AutoMaximum"
                PointChart.YAxis.AutoMaximum = Info

            Case "PointChartSettings:YAxis:Maximum"
                PointChart.YAxis.Maximum = Info

            Case "PointChartSettings:YAxis:AutoInterval"
                PointChart.YAxis.AutoInterval = Info

            Case "PointChartSettings:YAxis:MajorGridInterval"
                PointChart.YAxis.MajorGridInterval = Info

            Case "PointChartSettings:YAxis:AutoMajorGridInterval"
                PointChart.YAxis.AutoMajorGridInterval = Info





            Case "PointChartSettings:Command"
                Select Case Info
                    Case "DrawChart"
                        UpdatePointChartForm()
                        DrawChart()
                    Case "ClearChart"
                        ClearChart()
                End Select



            'Startup Command Arguments ================================================
            Case "ProjectName"
                If Project.OpenProject(Info) = True Then
                    ProjectSelected = True 'Project has been opened OK.
                Else
                    ProjectSelected = False 'Project could not be opened.
                End If

            Case "ProjectID"
                Message.AddWarning("Add code to handle ProjectID parameter at StartUp!" & vbCrLf)
                'Note the AppNet will usually select a project using ProjectPath.

            Case "ProjectPath"
                If Project.OpenProjectPath(Info) = True Then
                    ProjectSelected = True 'Project has been opened OK.
                Else
                    ProjectSelected = False 'Project could not be opened.
                End If

            Case "ConnectionName"
                StartupConnectionName = Info
            '--------------------------------------------------------------------------

            'Application Information  =================================================
            'returned by client.GetMessageServiceAppInfoAsync()
            Case "MessageServiceAppInfo:Name"
                'The name of the Message Service Application. (Not used.)

            Case "MessageServiceAppInfo:ExePath"
                'The executable file path of the Message Service Application.
                MsgServiceExePath = Info

            Case "MessageServiceAppInfo:Path"
                'The path of the Message Service Application (ComNet). (This is where an Application.Lock file will be found while ComNet is running.)
                MsgServiceAppPath = Info
           '---------------------------------------------------------------------------

            Case "EndOfSequence"
                'End of Information Vector Sequence reached.
                'TEST: Add Status OK element at the end of the sequence:
                Dim statusOK As New XElement("Status", "OK")
                xlocns(xlocns.Count - 1).Add(statusOK)


            Case Else
                'Message.SetWarningStyle()
                'Message.Add("Unknown location: " & Locn & vbCrLf)
                'Message.SetNormalStyle()
                Message.AddWarning("Unknown location: " & Locn & vbCrLf)
                Message.AddWarning("            info: " & Info & vbCrLf)

        End Select

    End Sub

    Private Sub GetStockChartSettingsForClient()
        'Get the Stock Chart settings and send it to the Client.

        Dim chartSettings As New XElement("Settings")

        Dim commandClearChart As New XElement("Command", "ClearChart")
        chartSettings.Add(commandClearChart)

        Dim inputData As New XElement("InputData")

        Dim dataType As New XElement("Type", StockChart.InputDataType)
        inputData.Add(dataType)

        If Trim(StockChart.InputDatabasePath) = "" Then
            Dim databasePath As New XElement("DatabasePath", "-")
            inputData.Add(databasePath)
        Else
            Dim databasePath As New XElement("DatabasePath", StockChart.InputDatabasePath)
            inputData.Add(databasePath)
        End If

        If Trim(StockChart.InputDataDescr) = "" Then
            Dim dataDescription As New XElement("DataDescription", "-")
            inputData.Add(dataDescription)
        Else
            Dim dataDescription As New XElement("DataDescription", StockChart.InputDataDescr)
            inputData.Add(dataDescription)
        End If

        If Trim(StockChart.InputQuery) = "" Then
            Dim databaseQuery As New XElement("DatabaseQuery", "-")
            inputData.Add(databaseQuery)
        Else
            Dim databaseQuery As New XElement("DatabaseQuery", StockChart.InputQuery)
            inputData.Add(databaseQuery)
        End If

        chartSettings.Add(inputData)

        Dim chartProperties As New XElement("ChartProperties")

        If Trim(StockChart.SeriesName) = "" Then
            Dim seriesName As New XElement("SeriesName", "-")
            chartProperties.Add(seriesName)
        Else
            Dim seriesName As New XElement("SeriesName", StockChart.SeriesName)
            chartProperties.Add(seriesName)
        End If

        If Trim(StockChart.XValuesFieldName) = "" Then
            Dim xValuesFieldName As New XElement("XValuesFieldName", "-")
            chartProperties.Add(xValuesFieldName)
        Else
            Dim xValuesFieldName As New XElement("XValuesFieldName", StockChart.XValuesFieldName)
            chartProperties.Add(xValuesFieldName)
        End If

        If Trim(StockChart.YValuesHighFieldName) = "" Then
            Dim yValuesHighFieldName As New XElement("YValuesHighFieldName", "-")
            chartProperties.Add(yValuesHighFieldName)
        Else
            Dim yValuesHighFieldName As New XElement("YValuesHighFieldName", StockChart.YValuesHighFieldName)
            chartProperties.Add(yValuesHighFieldName)
        End If

        If Trim(StockChart.YValuesLowFieldName) = "" Then
            Dim yValuesLowFieldName As New XElement("YValuesLowFieldName", "-")
            chartProperties.Add(yValuesLowFieldName)
        Else
            Dim yValuesLowFieldName As New XElement("YValuesLowFieldName", StockChart.YValuesLowFieldName)
            chartProperties.Add(yValuesLowFieldName)
        End If

        If Trim(StockChart.YValuesOpenFieldName) = "" Then
            Dim yValuesOpenFieldName As New XElement("YValuesOpenFieldName", "-")
            chartProperties.Add(yValuesOpenFieldName)
        Else
            Dim yValuesOpenFieldName As New XElement("YValuesOpenFieldName", StockChart.YValuesOpenFieldName)
            chartProperties.Add(yValuesOpenFieldName)
        End If

        Dim yValuesCloseFieldName As New XElement("YValuesCloseFieldName", StockChart.YValuesCloseFieldName)
        chartProperties.Add(yValuesCloseFieldName)
        chartSettings.Add(chartProperties)

        Dim chartTitle As New XElement("ChartTitle")
        If Trim(StockChart.ChartLabel.Name) = "" Then
            Dim chartTitleLabelName As New XElement("LabelName", "Label1")
            chartTitle.Add(chartTitleLabelName)
        Else
            Dim chartTitleLabelName As New XElement("LabelName", StockChart.ChartLabel.Name)
            chartTitle.Add(chartTitleLabelName)
        End If

        If Trim(StockChart.ChartLabel.Text) = "" Then
            Dim chartTitleText As New XElement("Text", "-")
            chartTitle.Add(chartTitleText)
        Else
            Dim chartTitleText As New XElement("Text", StockChart.ChartLabel.Text)
            chartTitle.Add(chartTitleText)
        End If

        Dim chartTitleFontName As New XElement("FontName", StockChart.ChartLabel.FontName)
        chartTitle.Add(chartTitleFontName)
        Dim chartTitleColor As New XElement("Color", StockChart.ChartLabel.Color)
        chartTitle.Add(chartTitleColor)
        Dim chartTitleSize As New XElement("Size", StockChart.ChartLabel.Size)
        chartTitle.Add(chartTitleSize)
        Dim chartTitleBold As New XElement("Bold", StockChart.ChartLabel.Bold)
        chartTitle.Add(chartTitleBold)
        Dim chartTitleItalic As New XElement("Italic", StockChart.ChartLabel.Italic)
        chartTitle.Add(chartTitleItalic)
        Dim chartTitleUnderline As New XElement("Underline", StockChart.ChartLabel.Underline)
        chartTitle.Add(chartTitleUnderline)
        Dim chartTitleStrikeout As New XElement("Strikeout", StockChart.ChartLabel.Strikeout)
        chartTitle.Add(chartTitleStrikeout)
        Dim chartTitleAlignment As New XElement("Alignment", StockChart.ChartLabel.Alignment)
        chartTitle.Add(chartTitleAlignment)

        chartSettings.Add(chartTitle)

        Dim xAxis As New XElement("XAxis")
        If Trim(StockChart.XAxis.Title.Text) = "" Then
            Dim titleText As New XElement("TitleText", "-")
            xAxis.Add(titleText)
        Else
            Dim titleText As New XElement("TitleText", StockChart.XAxis.Title.Text)
            xAxis.Add(titleText)
        End If

        Dim titleFontName As New XElement("TitleFontName", StockChart.XAxis.Title.FontName)
        xAxis.Add(titleFontName)
        Dim titleFontColor As New XElement("TitleColor", StockChart.XAxis.Title.Color)
        xAxis.Add(titleFontColor)
        Dim titleSize As New XElement("TitleSize", StockChart.XAxis.Title.Size)
        xAxis.Add(titleSize)
        Dim titleBold As New XElement("TitleBold", StockChart.XAxis.Title.Bold)
        xAxis.Add(titleBold)
        Dim titleItalic As New XElement("TitleItalic", StockChart.XAxis.Title.Italic)
        xAxis.Add(titleItalic)
        Dim titleUnderline As New XElement("TitleUnderline", StockChart.XAxis.Title.Underline)
        xAxis.Add(titleUnderline)
        Dim titleStrikeout As New XElement("TitleStrikeout", StockChart.XAxis.Title.Strikeout)
        xAxis.Add(titleStrikeout)
        Dim titleAlignment As New XElement("TitleAlignment", StockChart.XAxis.TitleAlignment)
        xAxis.Add(titleAlignment)
        Dim autoMinimum As New XElement("AutoMinimum", StockChart.XAxis.AutoMinimum)
        xAxis.Add(autoMinimum)
        Dim minimum As New XElement("Minimum", StockChart.XAxis.Minimum)
        xAxis.Add(minimum)
        Dim autoMaximum As New XElement("AutoMaximum", StockChart.XAxis.AutoMaximum)
        xAxis.Add(autoMaximum)
        Dim maximum As New XElement("Maximum", StockChart.XAxis.Maximum)
        xAxis.Add(maximum)
        Dim autoInterval As New XElement("AutoInterval", StockChart.XAxis.AutoInterval)
        xAxis.Add(autoInterval)
        Dim interval As New XElement("Interval", StockChart.XAxis.Interval)
        xAxis.Add(interval)
        Dim autoMajorGridInterval As New XElement("AutoMajorGridInterval", StockChart.XAxis.AutoMajorGridInterval)
        xAxis.Add(autoMajorGridInterval)
        Dim majorGridInterval As New XElement("MajorGridInterval", StockChart.XAxis.MajorGridInterval)
        xAxis.Add(majorGridInterval)
        chartSettings.Add(xAxis)

        Dim yAxis As New XElement("YAxis")
        If Trim(StockChart.YAxis.Title.Text) = "" Then
            Dim titleText2 As New XElement("TitleText", "-")
            yAxis.Add(titleText2)
        Else
            Dim titleText2 As New XElement("TitleText", StockChart.YAxis.Title.Text)
            yAxis.Add(titleText2)
        End If

        Dim titleFontName2 As New XElement("TitleFontName", StockChart.YAxis.Title.FontName)
        yAxis.Add(titleFontName2)
        Dim titleFontColor2 As New XElement("TitleColor", StockChart.YAxis.Title.Color)
        yAxis.Add(titleFontColor2)
        Dim titleSize2 As New XElement("TitleSize", StockChart.YAxis.Title.Size)
        yAxis.Add(titleSize2)
        Dim titleBold2 As New XElement("TitleBold", StockChart.YAxis.Title.Bold)
        yAxis.Add(titleBold2)
        Dim titleItalic2 As New XElement("TitleItalic", StockChart.YAxis.Title.Italic)
        yAxis.Add(titleItalic2)
        Dim titleUnderline2 As New XElement("TitleUnderline", StockChart.YAxis.Title.Underline)
        yAxis.Add(titleUnderline2)
        Dim titleStrikeout2 As New XElement("TitleStrikeout", StockChart.YAxis.Title.Strikeout)
        yAxis.Add(titleStrikeout2)
        Dim titleAlignment2 As New XElement("TitleAlignment", StockChart.YAxis.TitleAlignment)
        yAxis.Add(titleAlignment2)
        Dim autoMinimum2 As New XElement("AutoMinimum", StockChart.YAxis.AutoMinimum)
        yAxis.Add(autoMinimum2)
        Dim minimum2 As New XElement("Minimum", StockChart.YAxis.Minimum)
        yAxis.Add(minimum2)
        Dim autoMaximum2 As New XElement("AutoMaximum", StockChart.YAxis.AutoMaximum)
        yAxis.Add(autoMaximum2)
        Dim maximum2 As New XElement("Maximum", StockChart.YAxis.Maximum)
        yAxis.Add(maximum2)
        Dim autoInterval2 As New XElement("AutoInterval", StockChart.YAxis.AutoInterval)
        yAxis.Add(autoInterval2)
        Dim interval2 As New XElement("Interval", StockChart.YAxis.Interval)
        yAxis.Add(interval2)
        Dim autoMajorGridInterval2 As New XElement("AutoMajorGridInterval", StockChart.YAxis.AutoMajorGridInterval)
        yAxis.Add(autoMajorGridInterval2)
        Dim majorGridInterval2 As New XElement("MajorGridInterval", StockChart.YAxis.MajorGridInterval)
        yAxis.Add(majorGridInterval2)
        chartSettings.Add(yAxis)

        Dim commandOK As New XElement("Command", "OK")
        chartSettings.Add(commandOK)

        xlocns(xlocns.Count - 1).Add(chartSettings) 'The settings are aded to the last location in the XLocations list.

    End Sub

    Private Sub GetPointChartSettingsForClient()
        'Get the Point Chart settings and send it to the Client.

        Dim chartSettings As New XElement("Settings")

        Dim commandClearChart As New XElement("Command", "ClearChart")
        chartSettings.Add(commandClearChart)

        Dim inputData As New XElement("InputData")

        Dim dataType As New XElement("Type", PointChart.InputDataType)
        inputData.Add(dataType)

        If Trim(PointChart.InputDatabasePath) = "" Then
            Dim databasePath As New XElement("DatabasePath", "-")
            inputData.Add(databasePath)
        Else
            Dim databasePath As New XElement("DatabasePath", PointChart.InputDatabasePath)
            inputData.Add(databasePath)
        End If

        If Trim(PointChart.InputDataDescr) = "" Then
            Dim dataDescription As New XElement("DataDescription", "-")
            inputData.Add(dataDescription)
        Else
            Dim dataDescription As New XElement("DataDescription", PointChart.InputDataDescr)
            inputData.Add(dataDescription)
        End If

        If Trim(PointChart.InputQuery) = "" Then
            Dim databaseQuery As New XElement("DatabaseQuery", "-")
            inputData.Add(databaseQuery)
        Else
            Dim databaseQuery As New XElement("DatabaseQuery", PointChart.InputQuery)
            inputData.Add(databaseQuery)
        End If

        chartSettings.Add(inputData)

        Dim chartProperties As New XElement("ChartProperties")

        If Trim(PointChart.SeriesName) = "" Then
            Dim seriesName As New XElement("SeriesName", "-")
            chartProperties.Add(seriesName)
        Else
            Dim seriesName As New XElement("SeriesName", PointChart.SeriesName)
            chartProperties.Add(seriesName)
        End If

        If Trim(PointChart.XValuesFieldName) = "" Then
            Dim xValuesFieldName As New XElement("XValuesFieldName", "-")
            chartProperties.Add(xValuesFieldName)
        Else
            Dim xValuesFieldName As New XElement("XValuesFieldName", PointChart.XValuesFieldName)
            chartProperties.Add(xValuesFieldName)
        End If

        If Trim(PointChart.YValuesFieldName) = "" Then
            Dim yValuesFieldName As New XElement("YValuesFieldName", "-")
            chartProperties.Add(yValuesFieldName)
        Else
            Dim yValuesFieldName As New XElement("YValuesFieldName", PointChart.YValuesFieldName)
            chartProperties.Add(yValuesFieldName)
        End If

        chartSettings.Add(chartProperties)

        Dim chartTitle As New XElement("ChartTitle")
        If Trim(PointChart.ChartLabel.Name) = "" Then
            Dim chartTitleLabelName As New XElement("LabelName", "Label1")
            chartTitle.Add(chartTitleLabelName)
        Else
            Dim chartTitleLabelName As New XElement("LabelName", PointChart.ChartLabel.Name)
            chartTitle.Add(chartTitleLabelName)
        End If

        If Trim(PointChart.ChartLabel.Text) = "" Then
            Dim chartTitleText As New XElement("Text", "-")
            chartTitle.Add(chartTitleText)
        Else
            Dim chartTitleText As New XElement("Text", PointChart.ChartLabel.Text)
            chartTitle.Add(chartTitleText)
        End If

        Dim chartTitleFontName As New XElement("FontName", PointChart.ChartLabel.FontName)
        chartTitle.Add(chartTitleFontName)
        Dim chartTitleColor As New XElement("Color", PointChart.ChartLabel.Color)
        chartTitle.Add(chartTitleColor)
        Dim chartTitleSize As New XElement("Size", PointChart.ChartLabel.Size)
        chartTitle.Add(chartTitleSize)
        Dim chartTitleBold As New XElement("Bold", PointChart.ChartLabel.Bold)
        chartTitle.Add(chartTitleBold)
        Dim chartTitleItalic As New XElement("Italic", PointChart.ChartLabel.Italic)
        chartTitle.Add(chartTitleItalic)
        Dim chartTitleUnderline As New XElement("Underline", PointChart.ChartLabel.Underline)
        chartTitle.Add(chartTitleUnderline)
        Dim chartTitleStrikeout As New XElement("Strikeout", PointChart.ChartLabel.Strikeout)
        chartTitle.Add(chartTitleStrikeout)
        Dim chartTitleAlignment As New XElement("Alignment", PointChart.ChartLabel.Alignment)
        chartTitle.Add(chartTitleAlignment)

        chartSettings.Add(chartTitle)

        Dim xAxis As New XElement("XAxis")
        If Trim(PointChart.XAxis.Title.Text) = "" Then
            Dim titleText As New XElement("TitleText", "-")
            xAxis.Add(titleText)
        Else
            Dim titleText As New XElement("TitleText", PointChart.XAxis.Title.Text)
            xAxis.Add(titleText)
        End If

        Dim titleFontName As New XElement("TitleFontName", PointChart.XAxis.Title.FontName)
        xAxis.Add(titleFontName)
        Dim titleFontColor As New XElement("TitleColor", PointChart.XAxis.Title.Color)
        xAxis.Add(titleFontColor)
        Dim titleSize As New XElement("TitleSize", PointChart.XAxis.Title.Size)
        xAxis.Add(titleSize)
        Dim titleBold As New XElement("TitleBold", PointChart.XAxis.Title.Bold)
        xAxis.Add(titleBold)
        Dim titleItalic As New XElement("TitleItalic", PointChart.XAxis.Title.Italic)
        xAxis.Add(titleItalic)
        Dim titleUnderline As New XElement("TitleUnderline", PointChart.XAxis.Title.Underline)
        xAxis.Add(titleUnderline)
        Dim titleStrikeout As New XElement("TitleStrikeout", PointChart.XAxis.Title.Strikeout)
        xAxis.Add(titleStrikeout)
        Dim titleAlignment As New XElement("TitleAlignment", PointChart.XAxis.TitleAlignment)
        xAxis.Add(titleAlignment)
        Dim autoMinimum As New XElement("AutoMinimum", PointChart.XAxis.AutoMinimum)
        xAxis.Add(autoMinimum)
        Dim minimum As New XElement("Minimum", PointChart.XAxis.Minimum)
        xAxis.Add(minimum)
        Dim autoMaximum As New XElement("AutoMaximum", PointChart.XAxis.AutoMaximum)
        xAxis.Add(autoMaximum)
        Dim maximum As New XElement("Maximum", PointChart.XAxis.Maximum)
        xAxis.Add(maximum)
        Dim autoInterval As New XElement("AutoInterval", PointChart.XAxis.AutoInterval)
        xAxis.Add(autoInterval)
        Dim interval As New XElement("Interval", PointChart.XAxis.Interval)
        xAxis.Add(interval)
        Dim autoMajorGridInterval As New XElement("AutoMajorGridInterval", PointChart.XAxis.AutoMajorGridInterval)
        xAxis.Add(autoMajorGridInterval)
        Dim majorGridInterval As New XElement("MajorGridInterval", PointChart.XAxis.MajorGridInterval)
        xAxis.Add(majorGridInterval)
        chartSettings.Add(xAxis)

        Dim yAxis As New XElement("YAxis")
        If Trim(PointChart.YAxis.Title.Text) = "" Then
            Dim titleText2 As New XElement("TitleText", "-")
            yAxis.Add(titleText2)
        Else
            Dim titleText2 As New XElement("TitleText", PointChart.YAxis.Title.Text)
            yAxis.Add(titleText2)
        End If

        Dim titleFontName2 As New XElement("TitleFontName", PointChart.YAxis.Title.FontName)
        yAxis.Add(titleFontName2)
        Dim titleFontColor2 As New XElement("TitleColor", PointChart.YAxis.Title.Color)
        yAxis.Add(titleFontColor2)
        Dim titleSize2 As New XElement("TitleSize", PointChart.YAxis.Title.Size)
        yAxis.Add(titleSize2)
        Dim titleBold2 As New XElement("TitleBold", PointChart.YAxis.Title.Bold)
        yAxis.Add(titleBold2)
        Dim titleItalic2 As New XElement("TitleItalic", PointChart.YAxis.Title.Italic)
        yAxis.Add(titleItalic2)
        Dim titleUnderline2 As New XElement("TitleUnderline", PointChart.YAxis.Title.Underline)
        yAxis.Add(titleUnderline2)
        Dim titleStrikeout2 As New XElement("TitleStrikeout", PointChart.YAxis.Title.Strikeout)
        yAxis.Add(titleStrikeout2)
        Dim titleAlignment2 As New XElement("TitleAlignment", PointChart.YAxis.TitleAlignment)
        yAxis.Add(titleAlignment2)
        Dim autoMinimum2 As New XElement("AutoMinimum", PointChart.YAxis.AutoMinimum)
        yAxis.Add(autoMinimum2)
        Dim minimum2 As New XElement("Minimum", PointChart.YAxis.Minimum)
        yAxis.Add(minimum2)
        Dim autoMaximum2 As New XElement("AutoMaximum", PointChart.YAxis.AutoMaximum)
        yAxis.Add(autoMaximum2)
        Dim maximum2 As New XElement("Maximum", PointChart.YAxis.Maximum)
        yAxis.Add(maximum2)
        Dim autoInterval2 As New XElement("AutoInterval", PointChart.YAxis.AutoInterval)
        yAxis.Add(autoInterval2)
        Dim interval2 As New XElement("Interval", PointChart.YAxis.Interval)
        yAxis.Add(interval2)
        Dim autoMajorGridInterval2 As New XElement("AutoMajorGridInterval", PointChart.YAxis.AutoMajorGridInterval)
        yAxis.Add(autoMajorGridInterval2)
        Dim majorGridInterval2 As New XElement("MajorGridInterval", PointChart.YAxis.MajorGridInterval)
        yAxis.Add(majorGridInterval2)
        chartSettings.Add(yAxis)

        Dim commandOK As New XElement("Command", "OK")
        chartSettings.Add(commandOK)

        xlocns(xlocns.Count - 1).Add(chartSettings) 'The settings are added to the last location in the XLocations list.




    End Sub

    Private Sub SendMessage()
        'Code used to send a message after a timer delay.
        'The message destination is stored in MessageDest
        'The message text is stored in MessageText
        Timer1.Interval = 100 '100ms delay
        Timer1.Enabled = True 'Start the timer.
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If IsNothing(client) Then
            Message.AddWarning("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.AddWarning("client state is faulted. Message not sent!" & vbCrLf)
            Else
                Try
                    'Message.Add("Sending a message. Number of characters: " & MessageText.Length & vbCrLf)
                    'client.SendMessage(ClientAppName, MessageText)
                    client.SendMessage(ClientAppNetName, ClientConnName, MessageText) 'Added 2Feb19
                    MessageText = "" 'Clear the message after it has been sent.
                    ClientAppName = "" 'Clear the Client Application Name after the message has been sent.
                    'ClientAppLocn = "" 'Clear the Client Application Location after the message has been sent.
                    ClientConnName = "" 'Clear the Client Application Name after the message has been sent.
                    xlocns.Clear()
                Catch ex As Exception
                    Message.AddWarning("Error sending message: " & ex.Message & vbCrLf)
                End Try
            End If
        End If

        'Stop timer:
        Timer1.Enabled = False
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    'Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click
    '    Try
    '        'Dim pointIndex As Integer
    '        Dim result As Charting.HitTestResult
    '        'result = Chart1.HitTest(Cursor.Position.X, Cursor.Position.Y, True)
    '        result = Chart1.HitTest(Cursor.Position.X, Cursor.Position.Y, True)

    '        Message.Add("X : " & Cursor.Position.X & " Y: " & Cursor.Position.Y & vbCrLf)

    '        If result.ChartElementType = Charting.ChartElementType.DataPoint Then
    '            Message.Add("Point index: " & result.PointIndex & vbCrLf)
    '        Else
    '            Message.Add("result.ChartElementType.ToString: " & result.ChartElementType.ToString & vbCrLf)
    '        End If



    '    Catch ex As Exception
    '        Message.AddWarning(ex.Message & vbCrLf)
    '    End Try
    'End Sub

    Private Sub Chart1_MouseClick(sender As Object, e As MouseEventArgs) Handles Chart1.MouseClick
        Try
            'Dim pointIndex As Integer
            Dim result As Charting.HitTestResult
            'result = Chart1.HitTest(Cursor.Position.X, Cursor.Position.Y, True)
            result = Chart1.HitTest(e.X, e.Y, True)

            'Message.Add("X : " & Cursor.Position.X & " Y: " & Cursor.Position.Y & vbCrLf)
            'Message.Add("X : " & e.X & " Y: " & e.Y & vbCrLf)

            If result.ChartElementType = Charting.ChartElementType.DataPoint Then
                'Message.Add("Point index: " & result.PointIndex & "  ")
                Message.Add("Return on Capital percent: " & ds.Tables(0).Rows(result.PointIndex).Item("Gr_Return_on_Capital_pct") & "  ")
                Message.Add("Earnings Yield percent: " & ds.Tables(0).Rows(result.PointIndex).Item("Gr_Earnings_Yield_pct") & "  ")
                Message.Add("Total_Profit_pct: " & ds.Tables(0).Rows(result.PointIndex).Item("Total_Profit_pct") & vbCrLf)

                If IsNothing(ViewDatabaseData) Then
                    'ViewDatabaseData = New frmViewDatabaseData
                    'ViewDatabaseData.Show()
                    'ViewDatabaseData.Update()
                Else
                    'ViewDatabaseData.Show()
                    'ViewDatabaseData.Update()
                    ViewDatabaseData.DataGridView1.MultiSelect = False
                    ViewDatabaseData.DataGridView1.Rows(result.PointIndex).Selected = True
                    ViewDatabaseData.DataGridView1.FirstDisplayedScrollingRowIndex = result.PointIndex
                End If

            Else
                Message.Add("result.ChartElementType.ToString: " & result.ChartElementType.ToString & vbCrLf)
            End If

        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub ApplicationInfo_UpdateExePath() Handles ApplicationInfo.UpdateExePath
        'Update the Executable Path.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath
    End Sub

    Private Sub TabPage1_Enter(sender As Object, e As EventArgs) Handles TabPage1.Enter
        'txtCurrentDuration.Text = Format(Project.Usage.CurrentDuration.TotalHours, "0.000")
        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                                 Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                                 Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                                 Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        Timer2.Interval = 5000 '5 seconds
        Timer2.Enabled = True
        Timer2.Start()
    End Sub

    Private Sub TabPage1_Leave(sender As Object, e As EventArgs) Handles TabPage1.Leave
        Timer2.Enabled = False
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'Update the current duration:
        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                        Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                        Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                        Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Add the current project to the Message Service list.

        If Project.ParentProjectName <> "" Then
            Message.AddWarning("This project has a parent: " & Project.ParentProjectName & vbCrLf)
            Message.AddWarning("Child projects can not be added to the list." & vbCrLf)
            Exit Sub
        End If

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    'Dim Path As New XElement("Path", Me.Project.Path)
                    Dim Path As New XElement("Path", Project.Path)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to AppNet:
                    Message.XAddText("Message sent to " & "MessageService" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    'client.SendMessage("MessageService", doc.ToString)
                    client.SendMessage("", "MessageService", doc.ToString) 'UPDATED 2Feb19
                End If
            End If
        End If
    End Sub






#End Region 'Process XMessages

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class
