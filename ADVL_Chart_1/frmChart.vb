Public Class frmChart
    'Chart display form.

#Region " Variable Declarations - All the variables used in this form and this application." '-------------------------------------------------------------------------------------------------

    'Declare forms opened from this form:

    'Variables used to connect to a database and open a table:
    'Dim connString As String
    'Public myConnection As OleDb.OleDbConnection = New OleDb.OleDbConnection
    Public ds As DataSet = New DataSet
    'Dim da As OleDb.OleDbDataAdapter
    'Dim tables As DataTableCollection = ds.Tables

    'Dim UpdateNeeded As Boolean

    Dim WithEvents Zip As ADVL_Utilities_Library_1.ZipComp

    'Chart Objects
    Dim StockChart As StockChart
    Dim PointChart As PointChart

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Properties - All the properties used in this form and this application" '------------------------------------------------------------------------------------------------------------

    'The FormNo property stores the number of the instance of this form.
    'This form can have multipe instances, which are stored in the ChartList ArrayList in the ADVL_Chart_1 Main form.
    'When this form is closed, the FormNo is used to update the ClosedFormNo property of the Main form.
    'ClosedFormNo is then used by a method to set the corresponding form element in SharePricesList to Nothing.

    Private _formNo As Integer
    Public Property FormNo As Integer
        Get
            Return _formNo
        End Get
        Set(ByVal value As Integer)
            _formNo = value
        End Set
    End Property

    Private _chartType As String = "Stock"
    Property ChartType As String
        Get
            Return _chartType
        End Get
        Set(value As String)
            _chartType = value
        End Set
    End Property

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '-------------------------------------------------------------------------------------------------------------------------------------

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
                               <ChartType><%= ChartType %></ChartType>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        'Multiple form version of the SettingsFileName:
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & FormNo & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        'Multiple form version of the SettingsFileName:
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & FormNo & ".xml"

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
            If Settings.<FormSettings>.<ChartType>.Value <> Nothing Then
                ChartType = Settings.<FormSettings>.<ChartType>.Value
                cmbChartType.SelectedIndex = cmbChartType.FindStringExact(ChartType)
            End If
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

#Region " Form Display Methods - Code used to display this form." '----------------------------------------------------------------------------------------------------------------------------

    'Private Sub frmTemplate_Load(sender As Object, e As EventArgs) Handles Me.Load
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load

        cmbChartType.Items.Clear()
        cmbChartType.Items.Add("Stock")
        cmbChartType.Items.Add("Point")

        RestoreFormSettings()   'Restore the form settings

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form
        Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the SharePricesFormClosed method to select the correct form to set to nothing.
        Me.Close() 'Close the form
    End Sub

    'Private Sub frmTemplate_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if form is minimised.
        End If
    End Sub

    Private Sub frmSharePrices_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Main.ChartFormClosed()
    End Sub


#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Form Methods - The main actions performed by this form." '---------------------------------------------------------------------------------------------------------------------------

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Select Case ChartType
            Case "Area"

            Case "Bar"

            Case "BoxPlot"

            Case "Bubble"

            Case "Candlestick"

            Case "Column"

            Case "Doughnut"

            Case "ErrorBar"

            Case "FastLine"

            Case "FastPoint"

            Case "Funnel"

            Case "Kagi"

            Case "Line"

            Case "Pie"

            Case "Point"
                OpenPointChart
            Case "PointAndFigure"

            Case "Polar"

            Case "Pyramid"

            Case "Radar"

            Case "Range"

            Case "RangeBar"

            Case "RangeColumn"

            Case "Renko"

            Case "Spline"

            Case "SplineArea"

            Case "SplineRange"

            Case "StackedArea"

            Case "StackedArea100"

            Case "StackedBar"

            Case "StackedBar100"

            Case "StackedColumn"

            Case "StackedColumn100"

            Case "StepLine"

            Case "Stock"
                OpenStockChart()

            Case "ThreeLineBreak"

        End Select
    End Sub

    Private Sub ClearAllChartTypes()
        StockChart = Nothing
        PointChart = Nothing

    End Sub

    Private Sub OpenPointChart()
        'Find and open a Point Chart file.
        Select Case Main.Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a Stock Chart file from the project directory:
                OpenFileDialog1.InitialDirectory = Main.Project.DataLocn.Path
                OpenFileDialog1.Filter = "Point Chart | *.PointChart"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    ClearAllChartTypes()
                    PointChart = New PointChart
                    PointChart.DataLocation = Main.Project.DataLocn
                    txtChartFileName.Text = FileName
                    PointChart.LoadFile(FileName)
                    LoadPointChartData()
                    DrawPointChart()
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                'Select a Stock Chart file from the project archive:
                'Show the zip archive file selection form:
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Main.Project.DataLocn.Path
                Zip.SelectFile()
                'Zip.SelectFileForm.ApplicationName = Main.Project.ApplicationName
                Zip.SelectFileForm.ApplicationName = Main.Project.Application.Name
                Zip.SelectFileForm.SettingsLocn = Main.Project.SettingsLocn
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

    Private Sub OpenStockChart()
        'Find and open a Stock Chart file.
        Select Case Main.Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a Stock Chart file from the project directory:
                OpenFileDialog1.InitialDirectory = Main.Project.DataLocn.Path
                OpenFileDialog1.Filter = "Stock Chart | *.StockChart"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    ClearAllChartTypes()
                    StockChart = New StockChart
                    StockChart.DataLocation = Main.Project.DataLocn
                    txtChartFileName.Text = FileName
                    StockChart.LoadFile(FileName)
                    LoadStockChartData()
                    DrawStockChart()
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                'Select a Stock Chart file from the project archive:
                'Show the zip archive file selection form:
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Main.Project.DataLocn.Path
                Zip.SelectFile()
                'Zip.SelectFileForm.ApplicationName = Main.Project.ApplicationName
                Zip.SelectFileForm.ApplicationName = Main.Project.Application.Name
                Zip.SelectFileForm.SettingsLocn = Main.Project.SettingsLocn
                Zip.SelectFileForm.Show()
                Zip.SelectFileForm.RestoreFormSettings()
                Zip.SelectFileForm.FileExtension = ".StockChart"
                Zip.SelectFileForm.GetFileList()
        End Select
    End Sub

    Private Sub Zip_FileSelected(FileName As String) Handles Zip.FileSelected
        txtChartFileName.Text = FileName
        Select Case ChartType
            Case "Area"

            Case "Bar"

            Case "BoxPlot"

            Case "Bubble"

            Case "Candlestick"

            Case "Column"

            Case "Doughnut"

            Case "ErrorBar"

            Case "FastLine"

            Case "FastPoint"

            Case "Funnel"

            Case "Kagi"

            Case "Line"

            Case "Pie"

            Case "Point"

            Case "PointAndFigure"

            Case "Polar"

            Case "Pyramid"

            Case "Radar"

            Case "Range"

            Case "RangeBar"

            Case "RangeColumn"

            Case "Renko"

            Case "Spline"

            Case "SplineArea"

            Case "SplineRange"

            Case "StackedArea"

            Case "StackedArea100"

            Case "StackedBar"

            Case "StackedBar100"

            Case "StackedColumn"

            Case "StackedColumn100"

            Case "StepLine"

            Case "Stock"
                ClearAllChartTypes()
                StockChart = New StockChart
                StockChart.DataLocation = Main.Project.DataLocn
                txtChartFileName.Text = FileName
                StockChart.LoadFile(FileName)
                LoadStockChartData
                DrawStockChart()
            Case "ThreeLineBreak"

        End Select
    End Sub

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

            Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = 5 'Set the grid interval 
            Chart1.ChartAreas(0).AxisY.Interval = 5 'Set the annotation interval

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

            ''Display selected chart information:
            'Message.Add(vbCrLf & "Main.Chart1.ChartAreas.Count: " & Chart1.ChartAreas.Count & vbCrLf) '1
            'Message.Add("Main.Chart1.ChartAreas(0).Name: " & Chart1.ChartAreas(0).Name & vbCrLf) 'ChartArea1
            'Message.Add("Main.Chart1.ChartAreas(0).AxisX.Minimum: " & Chart1.ChartAreas(0).AxisX.Minimum & vbCrLf) '0
            'Message.Add("Main.Chart1.ChartAreas(0).AxisX.Maximum: " & Chart1.ChartAreas(0).AxisX.Maximum & vbCrLf) '1
            'Message.Add("Main.Chart1.ChartAreas(0).AxisY.Minimum: " & Chart1.ChartAreas(0).AxisY.Minimum & vbCrLf) 'NaN
            'Message.Add("Main.Chart1.ChartAreas(0).AxisY.Maximum: " & Chart1.ChartAreas(0).AxisY.Maximum & vbCrLf) 'NaN
            'Message.Add("Main.Chart1.Series(0).Name: " & Chart1.Series(0).Name & vbCrLf) 'Series1
            'Message.Add("Main.Chart1.Series(0).Legend: " & Chart1.Series(0).Legend & vbCrLf) 'Legend1
            'Message.Add("Main.Chart1.Series(0).YValueType: " & Chart1.Series(0).YValueType & vbCrLf) '2
            ''Main.MessageAdd( "Main.Chart1.Series(0).AxisLabel(0): " & Main.Chart1.Series(0).AxisLabel(0) & vbCrLf) 'Index out of range
            ''Main.MessageAdd( "Main.Chart1.Series(0).AxisLabel(1): " & Main.Chart1.Series(0).AxisLabel(1) & vbCrLf) 'Index out of range
            'Message.Add("Main.Chart1.ChartAreas(0).AxisY2.Minimum: " & Chart1.ChartAreas(0).AxisY2.Minimum & vbCrLf) 'NaN unless specified prior
            'Message.Add("Main.Chart1.ChartAreas(0).AxisY2.Maximum: " & Chart1.ChartAreas(0).AxisY2.Maximum & vbCrLf) 'NaN unless specified prior
            'Message.Add("Main.Chart1.ChartAreas(0).AxisY.AxisName: " & Chart1.ChartAreas(0).AxisY.AxisName & vbCrLf) '1
            'Message.Add("Main.Chart1.ChartAreas(0).AxisX.MajorGrid.Interval: " & Chart1.ChartAreas(0).AxisX.MajorGrid.Interval & vbCrLf) '1
            'Message.Add("Main.Chart1.ChartAreas(0).AxisY.MajorGrid.Interval: " & Chart1.ChartAreas(0).AxisY.MajorGrid.Interval & vbCrLf) 'NaN unless specified prior

        Catch ex As Exception
            Main.Message.AddWarning("Error drawing stock chart: " & ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub LoadStockChartData()
        If StockChart.InputDataType = "Database" Then
            If StockChart.InputDatabasePath = "" Then
                Main.Message.AddWarning("Input database path not spedcified." & vbCrLf)
            Else
                'Database access for MS Access:
                Dim connectionString As String 'Declare a connection string for MS Access - defines the database or server to be used.
                Dim conn As System.Data.OleDb.OleDbConnection 'Declare a connection for MS Access - used by the Data Adapter to connect to and disconnect from the database.
                Dim commandString As String 'Declare a command string - contains the query to be passed to the database.

                'Specify the connection string (Access 2007):
                connectionString = "provider=Microsoft.ACE.OLEDB.12.0;" +
                "data source = " + StockChart.InputDatabasePath

                'Connect to the Access database:
                conn = New System.Data.OleDb.OleDbConnection(connectionString)
                conn.Open()

                'Specify the commandString to query the database:
                commandString = StockChart.InputQuery
                Dim dataAdapter As New System.Data.OleDb.OleDbDataAdapter(commandString, conn)

                ds.Clear()
                ds.Reset()

                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

                Try
                    dataAdapter.Fill(ds, "SelTable")
                Catch ex As Exception
                    Main.Message.AddWarning("Error applying query." & vbCrLf)
                    Main.Message.AddWarning(ex.Message & vbCrLf)
                End Try

                conn.Close()

            End If
        ElseIf StockChart.InputDataType = "DataFile" Then
            Main.Message.AddWarning("DataFile input data type not yet supported." & vbCrLf)
        Else
            Main.Message.AddWarning("Unknown input data type: " & StockChart.InputDataType & vbCrLf)
        End If
    End Sub

    Private Sub LoadPointChartData()
        If PointChart.InputDataType = "Database" Then
            If PointChart.InputDatabasePath = "" Then
                Main.Message.AddWarning("Input database path not spedcified." & vbCrLf)
            Else
                'Database access for MS Access:
                Dim connectionString As String 'Declare a connection string for MS Access - defines the database or server to be used.
                Dim conn As System.Data.OleDb.OleDbConnection 'Declare a connection for MS Access - used by the Data Adapter to connect to and disconnect from the database.
                Dim commandString As String 'Declare a command string - contains the query to be passed to the database.

                'Specify the connection string (Access 2007):
                connectionString = "provider=Microsoft.ACE.OLEDB.12.0;" +
                "data source = " + PointChart.InputDatabasePath

                'Connect to the Access database:
                conn = New System.Data.OleDb.OleDbConnection(connectionString)
                conn.Open()

                'Specify the commandString to query the database:
                commandString = PointChart.InputQuery
                Dim dataAdapter As New System.Data.OleDb.OleDbDataAdapter(commandString, conn)

                ds.Clear()
                ds.Reset()

                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

                Try
                    dataAdapter.Fill(ds, "SelTable")
                Catch ex As Exception
                    Main.Message.AddWarning("Error applying query." & vbCrLf)
                    Main.Message.AddWarning(ex.Message & vbCrLf)
                End Try

                conn.Close()

            End If
        ElseIf PointChart.InputDataType = "DataFile" Then
            Main.Message.AddWarning("DataFile input data type not yet supported." & vbCrLf)
        Else
            Main.Message.AddWarning("Unknown input data type: " & PointChart.InputDataType & vbCrLf)
        End If
    End Sub

    Private Sub DrawPointChart()

        Try
            Chart1.Series.Clear()
            Chart1.Series.Add(PointChart.SeriesName)
            Chart1.Series(PointChart.SeriesName).YValuesPerPoint = 1
            Chart1.Series(PointChart.SeriesName).Points.DataBindXY(ds.Tables(0).DefaultView, PointChart.XValuesFieldName, ds.Tables(0).DefaultView, PointChart.YValuesFieldName)
            Chart1.Series(PointChart.SeriesName).ChartType = DataVisualization.Charting.SeriesChartType.Point

            'Dim NRows As Integer = ds.Tables(0).Rows.Count
            'Dim I As Integer

            'For I = 0 To NRows - 1
            '    If IsDBNull(ds.Tables(0).Rows(I).Item("Total_Profit_pct")) Then

            '    Else
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

            Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = 5 'Set the grid interval 
            Chart1.ChartAreas(0).AxisY.Interval = 5 'Set the annotation interval

            'Specify Y Axis annotation and major grid intervals: -----------------------------------------------------
            If PointChart.YAxis.AutoInterval = True Then
                Chart1.ChartAreas(0).AxisY.Interval = 0
            Else
                Chart1.ChartAreas(0).AxisY.Interval = PointChart.YAxis.Interval
            End If

            If PointChart.YAxis.AutoMajorGridInterval = True Then
                Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = 0
                'Message.Add("Y Axis major grid interval is automatic." & vbCrLf)
            Else
                Chart1.ChartAreas(0).AxisY.MajorGrid.Interval = PointChart.YAxis.MajorGridInterval
            End If


            'Specify X Axis range: ------------------------------------------------------------------------------
            Chart1.ChartAreas(0).AxisX.IntervalType = DataVisualization.Charting.DateTimeIntervalType.Auto

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
            If PointChart.XAxis.AutoInterval = True Then
                Chart1.ChartAreas(0).AxisX.Interval = 0
            Else
                Chart1.ChartAreas(0).AxisX.Interval = PointChart.XAxis.Interval
            End If

            If PointChart.XAxis.AutoMajorGridInterval = True Then
                Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = 0
                'Message.Add("X Axis major grid interval is automatic." & vbCrLf)
            Else
                Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = PointChart.XAxis.MajorGridInterval
            End If


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

        Catch ex As Exception
            Main.Message.AddWarning("Error drawing point chart: " & ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub cmbChartType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChartType.SelectedIndexChanged
        ChartType = cmbChartType.SelectedItem.ToString
    End Sub


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------




End Class