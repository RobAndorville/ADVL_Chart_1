'Public Class ChartInfo

'End Class

Public Class AxisProperties
    'Axis Properties

    Public Title As LabelProperties = New LabelProperties 'Title contains Text, FontName, Color, Size, Bold, Italic, Underline and Strikeout properties


    Private _titleAlignment As System.Drawing.StringAlignment = StringAlignment.Center 'Near (0) Center (1) Far (2)
    Property TitleAlignment As System.Drawing.StringAlignment
        Get
            Return _titleAlignment
        End Get
        Set(value As System.Drawing.StringAlignment)
            _titleAlignment = value
        End Set
    End Property

    'If True, the Axis minimum value is determined automatically.
    'If False, the Minimum property is used.
    Private _autoMinimum As Boolean = True
    Property AutoMinimum As Boolean
        Get
            Return _autoMinimum
        End Get
        Set(value As Boolean)
            _autoMinimum = value
        End Set
    End Property

    'The minimum value displayed along the axis.
    Private _minimum As Single
    Property Minimum As Single
        Get
            Return _minimum
        End Get
        Set(value As Single)
            _minimum = value
        End Set
    End Property

    'If True, the Axis maximum value is determined automatically.
    'If False, the Maximum property is used.
    Private _autoMaximum As Boolean = True
    Property AutoMaximum As Boolean
        Get
            Return _autoMaximum
        End Get
        Set(value As Boolean)
            _autoMaximum = value
        End Set
    End Property

    'The maximum value displayed along the axis.
    Private _maximum As Single
    Property Maximum As Single
        Get
            Return _maximum
        End Get
        Set(value As Single)
            _maximum = value
        End Set
    End Property
End Class

Public Class ChartLabelProperties
    'Chart Label Properties

    'The name of the label (used by the chart control to reference to label).
    Private _name As String = "Label1"
    Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    'The text displayed by the Chart Label.
    Private _text = ""
    Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
        End Set
    End Property

    'The label alignment relative to the chart.
    'Private _alignment As LabelAlignment = LabelAlignment.TopCenter
    Private _alignment As System.Drawing.ContentAlignment = ContentAlignment.TopCenter
    'BottomCenter (512) BottomLeft (256) BottomRight (1024) MiddleCenter (32) MiddleLeft (16) MiddleRight (64) TopCenter (2) TopLeft (1) TopRight (4)
    Property Alignment As ContentAlignment
        Get
            Return _alignment
        End Get
        Set(value As ContentAlignment)
            _alignment = value
        End Set
    End Property

    'The name of the font used to display the label.
    Private _fontName As String = "Arial"
    Property FontName As String
        Get
            Return _fontName
        End Get
        Set(value As String)
            _fontName = value
        End Set
    End Property

    'The colour of the label text.
    Private _color As String = "Black" 'Selected from System.Drawing.Color
    Property Color As String
        Get
            Return _color
        End Get
        Set(value As String)
            _color = value
        End Set
    End Property

    'The size of the label text.
    Private _size As Single = 14
    Property Size As Single
        Get
            Return _size
        End Get
        Set(value As Single)
            _size = value
        End Set
    End Property

    'Indicates if the label text is bold.
    Private _bold As Boolean = True
    Property Bold As Boolean
        Get
            Return _bold
        End Get
        Set(value As Boolean)
            _bold = value
        End Set
    End Property

    'Indicates if the label text is italic.
    Private _italic As Boolean = False
    Property Italic As Boolean
        Get
            Return _italic
        End Get
        Set(value As Boolean)
            _italic = value
        End Set
    End Property

    'Indicates if the label text is underlined.
    Private _underline As Boolean = False
    Property Underline As Boolean
        Get
            Return _underline
        End Get
        Set(value As Boolean)
            _underline = value
        End Set
    End Property

    'Indicates if the label text is strikeout.
    Private _strikeout As Boolean = False
    Property Strikeout As Boolean
        Get
            Return _strikeout
        End Get
        Set(value As Boolean)
            _strikeout = value
        End Set
    End Property

End Class

Public Class LabelProperties
    'Label properties.

    'The text displayed by the Label.
    Private _text = ""
    Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
        End Set
    End Property

    'The name of the font used to display the label.
    Private _fontName As String = "Arial"
    Property FontName As String
        Get
            Return _fontName
        End Get
        Set(value As String)
            _fontName = value
        End Set
    End Property

    'The colour of the label text.
    Private _color As String = "Black" 'Selected from System.Drawing.Color
    Property Color As String
        Get
            Return _color
        End Get
        Set(value As String)
            _color = value
        End Set
    End Property

    'The size of the label text.
    Private _size As Single = 14
    Property Size As Single
        Get
            Return _size
        End Get
        Set(value As Single)
            _size = value
        End Set
    End Property

    'Indicates if the label text is bold.
    Private _bold As Boolean = True
    Property Bold As Boolean
        Get
            Return _bold
        End Get
        Set(value As Boolean)
            _bold = value
        End Set
    End Property

    'Indicates if the label text is italic.
    Private _italic As Boolean = False
    Property Italic As Boolean
        Get
            Return _italic
        End Get
        Set(value As Boolean)
            _italic = value
        End Set
    End Property

    'Indicates if the label text is underlined.
    Private _underline As Boolean = False
    Property Underline As Boolean
        Get
            Return _underline
        End Get
        Set(value As Boolean)
            _underline = value
        End Set
    End Property

    'Indicates if the label text is strikeout.
    Private _strikeout As Boolean = False
    Property Strikeout As Boolean
        Get
            Return _strikeout
        End Get
        Set(value As Boolean)
            _strikeout = value
        End Set
    End Property
End Class

Public Class PointChart
    'Point Chart Properties

#Region " Variables" '----------------------------------------------------------------------------------------------------
    Public ChartLabel As New ChartLabelProperties
    Public XAxis As New AxisProperties
    Public YAxis As New AxisProperties
    Public DataLocation As New ADVL_Utilities_Library_1.FileLocation 'Stores information about the data location in the Project - used to read the chart settings files.
#End Region 'Variables ---------------------------------------------------------------------------------------------------

#Region " Properties" '---------------------------------------------------------------------------------------------------

    Private _fileName As String = "" 'The file name (with extension) of the chart settings. This file is stored in the Project.
    Property FileName As String
        Get
            Return _fileName
        End Get
        Set(value As String)
            _fileName = value
        End Set
    End Property

    Private _inputDataType As String = "Database" 'Database or Dataset
    Property InputDataType As String
        Get
            Return _inputDataType
        End Get
        Set(value As String)
            _inputDataType = value
        End Set
    End Property

    Private _inputDatabasePath As String = ""
    Property InputDatabasePath As String
        Get
            Return _inputDatabasePath
        End Get
        Set(value As String)
            _inputDatabasePath = value
        End Set
    End Property

    Private _inputQuery As String = ""
    Property InputQuery As String
        Get
            Return _inputQuery
        End Get
        Set(value As String)
            _inputQuery = value
        End Set
    End Property

    Private _inputDataDescr As String = "" 'A description of the data selected for charting.
    Property InputDataDescr As String
        Get
            Return _inputDataDescr
        End Get
        Set(value As String)
            _inputDataDescr = value
        End Set
    End Property

    Private _seriesName As String = "Series1" 'The name of the data series being plotted.
    Property SeriesName As String
        Get
            Return _seriesName
        End Get
        Set(value As String)
            _seriesName = value
        End Set
    End Property

    'The name of the Field containing the X values for the Point Chart.
    Private _xValuesFieldName As String = ""
    Property XValuesFieldName As String
        Get
            Return _xValuesFieldName
        End Get
        Set(value As String)
            _xValuesFieldName = value
        End Set
    End Property

    'The name of the Field containing the Y values for the Point Chart.
    Private _yValuesFieldName As String = ""
    Property YValuesFieldName As String
        Get
            Return _yValuesFieldName
        End Get
        Set(value As String)
            _yValuesFieldName = value
        End Set
    End Property

    'Specifies the value to be used for empty points. This property determines how an empty point is treated when the chart is drawn. (Average, Zero)
    Private _emptyPointValue As String = "Average"
    Property EmptyPointValue As String
        Get
            Return _emptyPointValue
        End Get
        Set(value As String)
            _emptyPointValue = value
        End Set
    End Property

    'Specifies the label position of the data point. (Auto, Top, Bottom, Right, Left, TopLeft, TopRight, BottomLeft, BottomRight, Center)
    Private _labelStyle As String = "Auto"
    Property LabelStyle As String
        Get
            Return _labelStyle
        End Get
        Set(value As String)
            _labelStyle = value
        End Set
    End Property

    'The Custom Property PixelPointDepth. Value range: Any integer > 0.
    Private _pixelPointDepth As Integer = 0 'Default value
    Property PixelPointDepth As Integer
        Get
            Return _pixelPointDepth
        End Get
        Set(value As Integer)
            _pixelPointDepth = value
        End Set
    End Property

    'The Custom Property PixelPointGapDepth. Value range: Any integer > 0.
    Private _pixelPointGapDepth As Integer = 0 'Default value
    Property PixelPointGapDepth As Integer
        Get
            Return _pixelPointGapDepth
        End Get
        Set(value As Integer)
            _pixelPointGapDepth = value
        End Set
    End Property

#End Region 'Properties --------------------------------------------------------------------------------------------------

#Region "Methods" '-------------------------------------------------------------------------------------------------------

    'Load the Stock Chart settings from the selected file.
    Public Sub LoadFile(ByRef myFileName As String)

        If myFileName = "" Then 'No stock point settings file has been selected.
            Exit Sub
        End If

        Dim XDoc As System.Xml.Linq.XDocument
        DataLocation.ReadXmlData(myFileName, XDoc)

        If XDoc Is Nothing Then
            RaiseEvent ErrorMessage("Xml list file is blank." & vbCrLf)
            Exit Sub
        End If

        FileName = myFileName

        'If XDoc.<ChartSettings>.<ChartType>.Value <> Nothing Then ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), XDoc.<ChartSettings>.<ChartType>.Value)

        'Input Data:
        If XDoc.<ChartSettings>.<InputDataType>.Value <> Nothing Then InputDataType = XDoc.<ChartSettings>.<InputDataType>.Value 'Database or Dataset
        If XDoc.<ChartSettings>.<InputDatabasePath>.Value <> Nothing Then InputDatabasePath = XDoc.<ChartSettings>.<InputDatabasePath>.Value
        If XDoc.<ChartSettings>.<InputQuery>.Value <> Nothing Then InputQuery = XDoc.<ChartSettings>.<InputQuery>.Value
        If XDoc.<ChartSettings>.<InputDataDescr>.Value <> Nothing Then InputDataDescr = XDoc.<ChartSettings>.<InputDataDescr>.Value

        'Chart Properties:
        If XDoc.<ChartSettings>.<SeriesName>.Value <> Nothing Then SeriesName = XDoc.<ChartSettings>.<SeriesName>.Value
        If XDoc.<ChartSettings>.<XValuesFieldName>.Value <> Nothing Then XValuesFieldName = XDoc.<ChartSettings>.<XValuesFieldName>.Value
        If XDoc.<ChartSettings>.<YValuesFieldName>.Value <> Nothing Then YValuesFieldName = XDoc.<ChartSettings>.<YValuesFieldName>.Value
        If XDoc.<ChartSettings>.<EmptyPointValue>.Value <> Nothing Then EmptyPointValue = XDoc.<ChartSettings>.<EmptyPointValue>.Value
        If XDoc.<ChartSettings>.<LabelStyle>.Value <> Nothing Then LabelStyle = XDoc.<ChartSettings>.<LabelStyle>.Value
        If XDoc.<ChartSettings>.<PixelPointGapDepth>.Value <> Nothing Then PixelPointGapDepth = XDoc.<ChartSettings>.<PixelPointGapDepth>.Value

        'Chart Label:
        If XDoc.<ChartSettings>.<ChartLabel>.<Name>.Value <> Nothing Then ChartLabel.Name = XDoc.<ChartSettings>.<ChartLabel>.<Name>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Text>.Value <> Nothing Then ChartLabel.Text = XDoc.<ChartSettings>.<ChartLabel>.<Text>.Value
        'If XDoc.<ChartSettings>.<ChartLabel>.<Alignment>.Value <> Nothing Then ChartLabel.Alignment = XDoc.<ChartSettings>.<ChartLabel>.<Alignment>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Alignment>.Value <> Nothing Then ChartLabel.Alignment = [Enum].Parse(GetType(ContentAlignment), XDoc.<ChartSettings>.<ChartLabel>.<Alignment>.Value)
        If XDoc.<ChartSettings>.<ChartLabel>.<FontName>.Value <> Nothing Then ChartLabel.FontName = XDoc.<ChartSettings>.<ChartLabel>.<FontName>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Color>.Value <> Nothing Then ChartLabel.Color = XDoc.<ChartSettings>.<ChartLabel>.<Color>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Size>.Value <> Nothing Then ChartLabel.Size = XDoc.<ChartSettings>.<ChartLabel>.<Size>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Bold>.Value <> Nothing Then ChartLabel.Bold = XDoc.<ChartSettings>.<ChartLabel>.<Bold>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Italic>.Value <> Nothing Then ChartLabel.Italic = XDoc.<ChartSettings>.<ChartLabel>.<Italic>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Underline>.Value <> Nothing Then ChartLabel.Underline = XDoc.<ChartSettings>.<ChartLabel>.<Underline>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Strikeout>.Value <> Nothing Then ChartLabel.Strikeout = XDoc.<ChartSettings>.<ChartLabel>.<Strikeout>.Value

        'X Axis:
        If XDoc.<ChartSettings>.<XAxis>.<TitleText>.Value <> Nothing Then XAxis.Title.Text = XDoc.<ChartSettings>.<XAxis>.<TitleText>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleFontName>.Value <> Nothing Then XAxis.Title.FontName = XDoc.<ChartSettings>.<XAxis>.<TitleFontName>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleFontColor>.Value <> Nothing Then XAxis.Title.Color = XDoc.<ChartSettings>.<XAxis>.<TitleFontColor>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleSize>.Value <> Nothing Then XAxis.Title.Size = XDoc.<ChartSettings>.<XAxis>.<TitleSize>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleBold>.Value <> Nothing Then XAxis.Title.Bold = XDoc.<ChartSettings>.<XAxis>.<TitleBold>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleItalic>.Value <> Nothing Then XAxis.Title.Italic = XDoc.<ChartSettings>.<XAxis>.<TitleItalic>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleUnderline>.Value <> Nothing Then XAxis.Title.Underline = XDoc.<ChartSettings>.<XAxis>.<TitleUnderline>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleStrikeout>.Value <> Nothing Then XAxis.Title.Strikeout = XDoc.<ChartSettings>.<XAxis>.<TitleStrikeout>.Value
        'If XDoc.<ChartSettings>.<XAxis>.<TitleAlignment>.Value <> Nothing Then XAxis.TitleAlignment = XDoc.<ChartSettings>.<XAxis>.<TitleAlignment>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleAlignment>.Value <> Nothing Then XAxis.TitleAlignment = [Enum].Parse(GetType(StringAlignment), XDoc.<ChartSettings>.<XAxis>.<TitleAlignment>.Value)
        If XDoc.<ChartSettings>.<XAxis>.<AutoMinimum>.Value <> Nothing Then XAxis.AutoMinimum = XDoc.<ChartSettings>.<XAxis>.<AutoMinimum>.Value
        If XDoc.<ChartSettings>.<XAxis>.<Minimum>.Value <> Nothing Then XAxis.Minimum = XDoc.<ChartSettings>.<XAxis>.<Minimum>.Value
        If XDoc.<ChartSettings>.<XAxis>.<AutoMaximum>.Value <> Nothing Then XAxis.AutoMaximum = XDoc.<ChartSettings>.<XAxis>.<AutoMaximum>.Value
        If XDoc.<ChartSettings>.<XAxis>.<Maximum>.Value <> Nothing Then XAxis.Maximum = XDoc.<ChartSettings>.<XAxis>.<Maximum>.Value

        'X Axis:
        If XDoc.<ChartSettings>.<YAxis>.<TitleText>.Value <> Nothing Then YAxis.Title.Text = XDoc.<ChartSettings>.<YAxis>.<TitleText>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleFontName>.Value <> Nothing Then YAxis.Title.FontName = XDoc.<ChartSettings>.<YAxis>.<TitleFontName>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleFontColor>.Value <> Nothing Then YAxis.Title.Color = XDoc.<ChartSettings>.<YAxis>.<TitleFontColor>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleSize>.Value <> Nothing Then YAxis.Title.Size = XDoc.<ChartSettings>.<YAxis>.<TitleSize>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleBold>.Value <> Nothing Then YAxis.Title.Bold = XDoc.<ChartSettings>.<YAxis>.<TitleBold>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleItalic>.Value <> Nothing Then YAxis.Title.Italic = XDoc.<ChartSettings>.<YAxis>.<TitleItalic>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleUnderline>.Value <> Nothing Then YAxis.Title.Underline = XDoc.<ChartSettings>.<YAxis>.<TitleUnderline>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleStrikeout>.Value <> Nothing Then YAxis.Title.Strikeout = XDoc.<ChartSettings>.<YAxis>.<TitleStrikeout>.Value
        'If XDoc.<ChartSettings>.<YAxis>.<TitleAlignment>.Value <> Nothing Then YAxis.TitleAlignment = XDoc.<ChartSettings>.<YAxis>.<TitleAlignment>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleAlignment>.Value <> Nothing Then YAxis.TitleAlignment = [Enum].Parse(GetType(StringAlignment), XDoc.<ChartSettings>.<YAxis>.<TitleAlignment>.Value)
        If XDoc.<ChartSettings>.<YAxis>.<AutoMinimum>.Value <> Nothing Then YAxis.AutoMinimum = XDoc.<ChartSettings>.<YAxis>.<AutoMinimum>.Value
        If XDoc.<ChartSettings>.<YAxis>.<Minimum>.Value <> Nothing Then YAxis.Minimum = XDoc.<ChartSettings>.<YAxis>.<Minimum>.Value
        If XDoc.<ChartSettings>.<YAxis>.<AutoMaximum>.Value <> Nothing Then YAxis.AutoMaximum = XDoc.<ChartSettings>.<YAxis>.<AutoMaximum>.Value
        If XDoc.<ChartSettings>.<YAxis>.<Maximum>.Value <> Nothing Then YAxis.Maximum = XDoc.<ChartSettings>.<YAxis>.<Maximum>.Value

    End Sub

    'Function to return the Point Chart settings in an XDocument.
    Public Function ToXDoc() As System.Xml.Linq.XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <!---->
                   <!--Point Chart Settings File-->
                   <ChartSettings>
                       <!--Input Data:-->
                       <InputDataType><%= InputDataType %></InputDataType>
                       <InputDatabasePath><%= InputDatabasePath %></InputDatabasePath>
                       <InputQuery><%= InputQuery %></InputQuery>
                       <InputDataDescr><%= InputDataDescr %></InputDataDescr>
                       <!--Chart Properties:-->
                       <SeriesName><%= SeriesName %></SeriesName>
                       <XValuesFieldName><%= XValuesFieldName %></XValuesFieldName>
                       <YValuesFieldName><%= YValuesFieldName %></YValuesFieldName>
                       <EmptyPointValue><%= EmptyPointValue %></EmptyPointValue>
                       <LabelStyle><%= LabelStyle %></LabelStyle>
                       <PixelPointDepth><%= PixelPointDepth %></PixelPointDepth>
                       <PixelPointGapDepth><%= PixelPointGapDepth %></PixelPointGapDepth>
                       <ChartLabel>
                           <Name><%= ChartLabel.Name %></Name>
                           <Text><%= ChartLabel.Text %></Text>
                           <Alignment><%= ChartLabel.Alignment %></Alignment>
                           <FontName><%= ChartLabel.FontName %></FontName>
                           <Color><%= ChartLabel.Color %></Color>
                           <Size><%= ChartLabel.Size %></Size>
                           <Bold><%= ChartLabel.Bold %></Bold>
                           <Italic><%= ChartLabel.Italic %></Italic>
                           <Underline><%= ChartLabel.Underline %></Underline>
                           <Strikeout><%= ChartLabel.Strikeout %></Strikeout>
                       </ChartLabel>
                       <XAxis>
                           <TitleText><%= XAxis.Title.Text %></TitleText>
                           <TitleFontName><%= XAxis.Title.FontName %></TitleFontName>
                           <TitleFontColor><%= XAxis.Title.Color %></TitleFontColor>
                           <TitleSize><%= XAxis.Title.Size %></TitleSize>
                           <TitleBold><%= XAxis.Title.Bold %></TitleBold>
                           <TitleItalic><%= XAxis.Title.Italic %></TitleItalic>
                           <TitleUnderline><%= XAxis.Title.Underline %></TitleUnderline>
                           <TitleStrikeout><%= XAxis.Title.Strikeout %></TitleStrikeout>
                           <TitleAlignment><%= XAxis.TitleAlignment %></TitleAlignment>
                           <AutoMinimum><%= XAxis.AutoMinimum %></AutoMinimum>
                           <Minimum><%= XAxis.Minimum %></Minimum>
                           <AutoMaximum><%= XAxis.AutoMaximum %></AutoMaximum>
                           <Maximum><%= XAxis.Maximum %></Maximum>
                       </XAxis>
                       <YAxis>
                           <TitleText><%= YAxis.Title.Text %></TitleText>
                           <TitleFontName><%= YAxis.Title.FontName %></TitleFontName>
                           <TitleFontColor><%= YAxis.Title.Color %></TitleFontColor>
                           <TitleSize><%= YAxis.Title.Size %></TitleSize>
                           <TitleBold><%= YAxis.Title.Bold %></TitleBold>
                           <TitleItalic><%= YAxis.Title.Italic %></TitleItalic>
                           <TitleUnderline><%= YAxis.Title.Underline %></TitleUnderline>
                           <TitleStrikeout><%= YAxis.Title.Strikeout %></TitleStrikeout>
                           <TitleAlignment><%= YAxis.TitleAlignment %></TitleAlignment>
                           <AutoMinimum><%= YAxis.AutoMinimum %></AutoMinimum>
                           <Minimum><%= YAxis.Minimum %></Minimum>
                           <AutoMaximum><%= YAxis.AutoMaximum %></AutoMaximum>
                           <Maximum><%= YAxis.Maximum %></Maximum>
                       </YAxis>
                   </ChartSettings>

        Return XDoc
    End Function

    'Save the Point Chart settings in a file named FileName.
    Public Sub SaveFile(ByVal myFileName As String)

        If myFileName = "" Then 'No stock chart settings file has been selected.
            Exit Sub
        End If

        DataLocation.SaveXmlData(myFileName, ToXDoc)

    End Sub




#End Region 'Methods -----------------------------------------------------------------------------------------------------

#Region "Events" '--------------------------------------------------------------------------------------------------------

    Event ErrorMessage(ByVal Message As String) 'Send an error message.
    Event Message(ByVal Message As String) 'Send a normal message.

#End Region 'Events ------------------------------------------------------------------------------------------------------

End Class

Public Class StockChart
    'Stock Chart Properties

#Region " Variables" '----------------------------------------------------------------------------------------------------
    Public ChartLabel As New ChartLabelProperties
    Public XAxis As New AxisProperties
    Public YAxis As New AxisProperties
    Public DataLocation As New ADVL_Utilities_Library_1.FileLocation 'Stores information about the data location in the Project - used to read the chart settings files.
#End Region 'Variables ---------------------------------------------------------------------------------------------------


#Region " Properties" '---------------------------------------------------------------------------------------------------

    Private _fileName As String = "" 'The file name (with extension) of the chart settings. This file is stored in the Project.
    Property FileName As String
        Get
            Return _fileName
        End Get
        Set(value As String)
            _fileName = value
        End Set
    End Property

    'Private _lastUsedFileName As String = "" 'The File Name of the last used stock chart settings file.
    'Property LastUsedFileName As String
    '    Get
    '        Return _lastUsedFileName
    '    End Get
    '    Set(value As String)
    '        _lastUsedFileName = value
    '    End Set
    'End Property


    Private _inputDataType As String = "Database" 'Database or Dataset
    Property InputDataType As String
        Get
            Return _inputDataType
        End Get
        Set(value As String)
            _inputDataType = value
        End Set
    End Property

    Private _inputDatabasePath As String = ""
    Property InputDatabasePath As String
        Get
            Return _inputDatabasePath
        End Get
        Set(value As String)
            _inputDatabasePath = value
        End Set
    End Property

    Private _inputQuery As String = ""
    Property InputQuery As String
        Get
            Return _inputQuery
        End Get
        Set(value As String)
            _inputQuery = value
        End Set
    End Property

    Private _inputDataDescr As String = "" 'A description of the data selected for charting.
    Property InputDataDescr As String
        Get
            Return _inputDataDescr
        End Get
        Set(value As String)
            _inputDataDescr = value
        End Set
    End Property

    'NOTE: ChartType is redundant - A stock chart is always of Stock chart type!
    Private _chartType As DataVisualization.Charting.SeriesChartType = DataVisualization.Charting.SeriesChartType.Stock
    'Area (13) Bar (7) BoxPlot (28) Bubble (2) Candlestick (20) Column (10) Doughnut (18) ErrorBar (27) FastLine (6) FastPoint (1) Funnel (33) Kagi (31) Line (3) Pie (17) 
    'Point (0) PointAndFigure (32) Polar (26) Pyramid (34) Radar (25) Range (21) RangeBar (23) RangeColumn (24) Renko (29) Spline (4) SplineArea (14) SplineRange (22) 
    'StackedArea (15) StackedArea100 (16) StackedBar (8) StackedBar100 (9) StackedColumn (11) StackedColumn100 (12) StepLine (5) Stock (19) ThreeLineBreak (30)

    Property ChartType As DataVisualization.Charting.SeriesChartType
        Get
            Return _chartType
        End Get
        Set(value As DataVisualization.Charting.SeriesChartType)
            _chartType = value
        End Set
    End Property

    Private _seriesName As String = "Series1" 'The name of the data series being plotted.
    Property SeriesName As String
        Get
            Return _seriesName
        End Get
        Set(value As String)
            _seriesName = value
        End Set
    End Property

    'The name of the Field containing the X values for the Stock Chart.
    Private _xValuesFieldName As String = ""
    Property XValuesFieldName As String
        Get
            Return _xValuesFieldName
        End Get
        Set(value As String)
            _xValuesFieldName = value
        End Set
    End Property

    'The name of the Field containing the High values for the Stock Chart.
    Private _yValuesHighFieldName As String = ""
    Property YValuesHighFieldName As String
        Get
            Return _yValuesHighFieldName
        End Get
        Set(value As String)
            _yValuesHighFieldName = value
        End Set
    End Property

    'The name of the Field containing the Low values for the Stock Chart.
    Private _yValuesLowFieldName As String = ""
    Property YValuesLowFieldName As String
        Get
            Return _yValuesLowFieldName
        End Get
        Set(value As String)
            _yValuesLowFieldName = value
        End Set
    End Property

    'The name of the Field containing the Open values for the Stock Chart.
    Private _yValuesOpenFieldName As String = ""
    Property YValuesOpenFieldName As String
        Get
            Return _yValuesOpenFieldName
        End Get
        Set(value As String)
            _yValuesOpenFieldName = value
        End Set
    End Property

    'The name of the Field containing the Close values for the Stock Chart.
    Private _yValuesCloseFieldName As String = ""
    Property YValuesCloseFieldName As String
        Get
            Return _yValuesCloseFieldName
        End Get
        Set(value As String)
            _yValuesCloseFieldName = value
        End Set
    End Property

    'The Custom Property LabelValueType. Value range: High, Low, Open, Close.
    Private _labelValueType As String = "Close" 'Default value
    Property LabelValueType As String
        Get
            Return _labelValueType
        End Get
        Set(value As String)
            _labelValueType = value
        End Set
    End Property

    'The Custom Property MaxPixelPointWidth. Value range: Any integer > 0.
    Private _maxPixelPointWidth As Integer = 1 'Default value
    Property MaxPixelPointWidth As Integer
        Get
            Return _maxPixelPointWidth
        End Get
        Set(value As Integer)
            _maxPixelPointWidth = value
            If _maxPixelPointWidth <= 0 Then 'Value must be an integer > 0
                _maxPixelPointWidth = 1
            End If
        End Set
    End Property

    'The Custom Property MinPixelPointWidth. Value range: Any integer > 0.
    Private _minPixelPointWidth As Integer = 1 'Default value 
    Property MinPixelPointWidth As Integer
        Get
            Return _minPixelPointWidth
        End Get
        Set(value As Integer)
            _minPixelPointWidth = value
            If _minPixelPointWidth <= 0 Then 'Value must be an integer > 0
                _minPixelPointWidth = 1
            End If
        End Set
    End Property

    'The Custom Property OpenCloseStyle. Value range: Triangle, Line, Candlestick.
    Private _openCloseStyle As String = "Line" 'Default value
    Property OpenCloseStyle As String
        Get
            Return _openCloseStyle
        End Get
        Set(value As String)
            _openCloseStyle = value
        End Set
    End Property

    'The Custom Property PixelPointDepth. Value range: Any integer > 0.
    Private _pixelPointDepth As Integer = 1 'Default value
    Property PixelPointDepth As Integer
        Get
            Return _pixelPointDepth
        End Get
        Set(value As Integer)
            _pixelPointDepth = value
        End Set
    End Property

    'The Custom Property PixelPointGapDepth. Value range: Any integer > 0.
    Private _pixelPointGapDepth As Integer = 1 'Default value
    Property PixelPointGapDepth As Integer
        Get
            Return _pixelPointGapDepth
        End Get
        Set(value As Integer)
            _pixelPointGapDepth = value
        End Set
    End Property

    'The Custom Property PixelPointWidth. Value range: Any integer > 0.
    Private _pixelPointWidth As Integer = 1 'Default value
    Property PixelPointWidth As Integer
        Get
            Return _pixelPointWidth
        End Get
        Set(value As Integer)
            _pixelPointWidth = value
        End Set
    End Property

    'The Custom Property PointWidth. Value range: 0 to 2.
    Private _pointWidth As Single = 0.8 'Default value
    Property PointWidth As Single
        Get
            Return _pointWidth
        End Get
        Set(value As Single)
            _pointWidth = value
        End Set
    End Property

    'The Custom Property ShowOpenClose. Value range: Both, Open, Close.
    Private _showOpenClose As String = "Both" 'Default value 
    Property ShowOpenClose As String
        Get
            Return _showOpenClose
        End Get
        Set(value As String)
            _showOpenClose = value
        End Set
    End Property

#End Region 'Properties --------------------------------------------------------------------------------------------------


#Region "Methods" '-------------------------------------------------------------------------------------------------------

    'Load the Stock Chart settings from the selected settings file.
    'Public Sub LoadFile()
    Public Sub LoadFile(ByRef myFileName As String)

        If myFileName = "" Then 'No stock chart settings file has been selected.
            Exit Sub
        End If

        Dim XDoc As System.Xml.Linq.XDocument
        DataLocation.ReadXmlData(myFileName, XDoc)

        If XDoc Is Nothing Then
            RaiseEvent ErrorMessage("Xml list file is blank." & vbCrLf)
            Exit Sub
        End If

        FileName = myFileName

        If XDoc.<ChartSettings>.<ChartType>.Value <> Nothing Then ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), XDoc.<ChartSettings>.<ChartType>.Value)
        If XDoc.<ChartSettings>.<InputDataType>.Value <> Nothing Then InputDataType = XDoc.<ChartSettings>.<InputDataType>.Value
        If XDoc.<ChartSettings>.<InputDatabasePath>.Value <> Nothing Then InputDatabasePath = XDoc.<ChartSettings>.<InputDatabasePath>.Value
        If XDoc.<ChartSettings>.<InputQuery>.Value <> Nothing Then InputQuery = XDoc.<ChartSettings>.<InputQuery>.Value
        If XDoc.<ChartSettings>.<InputDataDescr>.Value <> Nothing Then InputDataDescr = XDoc.<ChartSettings>.<InputDataDescr>.Value
        If XDoc.<ChartSettings>.<SeriesName>.Value <> Nothing Then SeriesName = XDoc.<ChartSettings>.<SeriesName>.Value
        If XDoc.<ChartSettings>.<XValuesFieldName>.Value <> Nothing Then XValuesFieldName = XDoc.<ChartSettings>.<XValuesFieldName>.Value
        If XDoc.<ChartSettings>.<YValuesHighFieldName>.Value <> Nothing Then YValuesHighFieldName = XDoc.<ChartSettings>.<YValuesHighFieldName>.Value
        If XDoc.<ChartSettings>.<YValuesLowFieldName>.Value <> Nothing Then YValuesLowFieldName = XDoc.<ChartSettings>.<YValuesLowFieldName>.Value
        If XDoc.<ChartSettings>.<YValuesOpenFieldName>.Value <> Nothing Then YValuesOpenFieldName = XDoc.<ChartSettings>.<YValuesOpenFieldName>.Value
        If XDoc.<ChartSettings>.<YValuesCloseFieldName>.Value <> Nothing Then YValuesCloseFieldName = XDoc.<ChartSettings>.<YValuesCloseFieldName>.Value
        If XDoc.<ChartSettings>.<LabelValueType>.Value <> Nothing Then LabelValueType = XDoc.<ChartSettings>.<LabelValueType>.Value
        If XDoc.<ChartSettings>.<MaxPixelPointWidth>.Value <> Nothing Then MaxPixelPointWidth = XDoc.<ChartSettings>.<MaxPixelPointWidth>.Value
        If XDoc.<ChartSettings>.<MinPixelPointWidth>.Value <> Nothing Then MinPixelPointWidth = XDoc.<ChartSettings>.<MinPixelPointWidth>.Value
        If XDoc.<ChartSettings>.<OpenCloseStyle>.Value <> Nothing Then OpenCloseStyle = XDoc.<ChartSettings>.<OpenCloseStyle>.Value
        If XDoc.<ChartSettings>.<PixelPointDepth>.Value <> Nothing Then PixelPointDepth = XDoc.<ChartSettings>.<PixelPointDepth>.Value
        If XDoc.<ChartSettings>.<PixelPointGapDepth>.Value <> Nothing Then PixelPointGapDepth = XDoc.<ChartSettings>.<PixelPointGapDepth>.Value
        If XDoc.<ChartSettings>.<PixelPointWidth>.Value <> Nothing Then PixelPointWidth = XDoc.<ChartSettings>.<PixelPointWidth>.Value
        If XDoc.<ChartSettings>.<PointWidth>.Value <> Nothing Then PointWidth = XDoc.<ChartSettings>.<PointWidth>.Value
        If XDoc.<ChartSettings>.<ShowOpenClose>.Value <> Nothing Then ShowOpenClose = XDoc.<ChartSettings>.<ShowOpenClose>.Value

        If XDoc.<ChartSettings>.<ChartLabel>.<Name>.Value <> Nothing Then ChartLabel.Name = XDoc.<ChartSettings>.<ChartLabel>.<Name>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Text>.Value <> Nothing Then ChartLabel.Text = XDoc.<ChartSettings>.<ChartLabel>.<Text>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Alignment>.Value <> Nothing Then ChartLabel.Alignment = [Enum].Parse(GetType(ContentAlignment), XDoc.<ChartSettings>.<ChartLabel>.<Alignment>.Value)
        If XDoc.<ChartSettings>.<ChartLabel>.<FontName>.Value <> Nothing Then ChartLabel.FontName = XDoc.<ChartSettings>.<ChartLabel>.<FontName>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Color>.Value <> Nothing Then ChartLabel.Color = XDoc.<ChartSettings>.<ChartLabel>.<Color>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Size>.Value <> Nothing Then ChartLabel.Size = XDoc.<ChartSettings>.<ChartLabel>.<Size>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Bold>.Value <> Nothing Then ChartLabel.Bold = XDoc.<ChartSettings>.<ChartLabel>.<Bold>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Italic>.Value <> Nothing Then ChartLabel.Italic = XDoc.<ChartSettings>.<ChartLabel>.<Italic>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Underline>.Value <> Nothing Then ChartLabel.Underline = XDoc.<ChartSettings>.<ChartLabel>.<Underline>.Value
        If XDoc.<ChartSettings>.<ChartLabel>.<Strikeout>.Value <> Nothing Then ChartLabel.Strikeout = XDoc.<ChartSettings>.<ChartLabel>.<Strikeout>.Value

        If XDoc.<ChartSettings>.<XAxis>.<TitleText>.Value <> Nothing Then XAxis.Title.Text = XDoc.<ChartSettings>.<XAxis>.<TitleText>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleFontName>.Value <> Nothing Then XAxis.Title.FontName = XDoc.<ChartSettings>.<XAxis>.<TitleFontName>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleFontColor>.Value <> Nothing Then XAxis.Title.Color = XDoc.<ChartSettings>.<XAxis>.<TitleFontColor>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleSize>.Value <> Nothing Then XAxis.Title.Size = XDoc.<ChartSettings>.<XAxis>.<TitleSize>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleBold>.Value <> Nothing Then XAxis.Title.Bold = XDoc.<ChartSettings>.<XAxis>.<TitleBold>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleItalic>.Value <> Nothing Then XAxis.Title.Italic = XDoc.<ChartSettings>.<XAxis>.<TitleItalic>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleUnderline>.Value <> Nothing Then XAxis.Title.Underline = XDoc.<ChartSettings>.<XAxis>.<TitleUnderline>.Value
        If XDoc.<ChartSettings>.<XAxis>.<TitleStrikeout>.Value <> Nothing Then XAxis.Title.Strikeout = XDoc.<ChartSettings>.<XAxis>.<TitleStrikeout>.Value
        'If XDoc.<ChartSettings>.<XAxis>.<TitleAlignment>.Value <> Nothing Then XAxis.TitleAlignment = [Enum].Parse(GetType(ContentAlignment), XDoc.<ChartSettings>.<XAxis>.<TitleAlignment>.Value)
        If XDoc.<ChartSettings>.<XAxis>.<TitleAlignment>.Value <> Nothing Then XAxis.TitleAlignment = [Enum].Parse(GetType(StringAlignment), XDoc.<ChartSettings>.<XAxis>.<TitleAlignment>.Value)
        If XDoc.<ChartSettings>.<XAxis>.<AutoMinimum>.Value <> Nothing Then XAxis.AutoMinimum = XDoc.<ChartSettings>.<XAxis>.<AutoMinimum>.Value
        If XDoc.<ChartSettings>.<XAxis>.<Minimum>.Value <> Nothing Then XAxis.Minimum = XDoc.<ChartSettings>.<XAxis>.<Minimum>.Value
        If XDoc.<ChartSettings>.<XAxis>.<AutoMaximum>.Value <> Nothing Then XAxis.AutoMaximum = XDoc.<ChartSettings>.<XAxis>.<AutoMaximum>.Value
        If XDoc.<ChartSettings>.<XAxis>.<Maximum>.Value <> Nothing Then XAxis.Maximum = XDoc.<ChartSettings>.<XAxis>.<Maximum>.Value

        If XDoc.<ChartSettings>.<YAxis>.<TitleText>.Value <> Nothing Then YAxis.Title.Text = XDoc.<ChartSettings>.<YAxis>.<TitleText>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleFontName>.Value <> Nothing Then YAxis.Title.FontName = XDoc.<ChartSettings>.<YAxis>.<TitleFontName>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleFontColor>.Value <> Nothing Then YAxis.Title.Color = XDoc.<ChartSettings>.<YAxis>.<TitleFontColor>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleSize>.Value <> Nothing Then YAxis.Title.Size = XDoc.<ChartSettings>.<YAxis>.<TitleSize>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleBold>.Value <> Nothing Then YAxis.Title.Bold = XDoc.<ChartSettings>.<YAxis>.<TitleBold>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleItalic>.Value <> Nothing Then YAxis.Title.Italic = XDoc.<ChartSettings>.<YAxis>.<TitleItalic>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleUnderline>.Value <> Nothing Then YAxis.Title.Underline = XDoc.<ChartSettings>.<YAxis>.<TitleUnderline>.Value
        If XDoc.<ChartSettings>.<YAxis>.<TitleStrikeout>.Value <> Nothing Then YAxis.Title.Strikeout = XDoc.<ChartSettings>.<YAxis>.<TitleStrikeout>.Value
        'If XDoc.<ChartSettings>.<YAxis>.<TitleAlignment>.Value <> Nothing Then YAxis.TitleAlignment = [Enum].Parse(GetType(ContentAlignment), XDoc.<ChartSettings>.<YAxis>.<TitleAlignment>.Value)
        If XDoc.<ChartSettings>.<YAxis>.<TitleAlignment>.Value <> Nothing Then YAxis.TitleAlignment = [Enum].Parse(GetType(StringAlignment), XDoc.<ChartSettings>.<YAxis>.<TitleAlignment>.Value)
        If XDoc.<ChartSettings>.<YAxis>.<AutoMinimum>.Value <> Nothing Then YAxis.AutoMinimum = XDoc.<ChartSettings>.<YAxis>.<AutoMinimum>.Value
        If XDoc.<ChartSettings>.<YAxis>.<Minimum>.Value <> Nothing Then YAxis.Minimum = XDoc.<ChartSettings>.<YAxis>.<Minimum>.Value
        If XDoc.<ChartSettings>.<YAxis>.<AutoMaximum>.Value <> Nothing Then YAxis.AutoMaximum = XDoc.<ChartSettings>.<YAxis>.<AutoMaximum>.Value
        If XDoc.<ChartSettings>.<YAxis>.<Maximum>.Value <> Nothing Then YAxis.Maximum = XDoc.<ChartSettings>.<YAxis>.<Maximum>.Value

    End Sub

    'Function to return the Stock Chart settings in an XDocument.
    Public Function ToXDoc() As System.Xml.Linq.XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <!---->
                   <!--Stock Chart Settings File-->
                   <ChartSettings>
                       <!---->
                       <ChartType><%= ChartType %></ChartType>
                       <!--Input Data:-->
                       <InputDataType><%= InputDataType %></InputDataType>
                       <InputDatabasePath><%= InputDatabasePath %></InputDatabasePath>
                       <InputQuery><%= InputQuery %></InputQuery>
                       <InputDataDescr><%= InputDataDescr %></InputDataDescr>
                       <!--Chart Properties:-->
                       <SeriesName><%= SeriesName %></SeriesName>
                       <XValuesFieldName><%= XValuesFieldName %></XValuesFieldName>
                       <YValuesHighFieldName><%= YValuesHighFieldName %></YValuesHighFieldName>
                       <YValuesLowFieldName><%= YValuesLowFieldName %></YValuesLowFieldName>
                       <YValuesOpenFieldName><%= YValuesOpenFieldName %></YValuesOpenFieldName>
                       <YValuesCloseFieldName><%= YValuesCloseFieldName %></YValuesCloseFieldName>
                       <LabelValueType><%= LabelValueType %></LabelValueType>
                       <MaxPixelPointWidth><%= MaxPixelPointWidth %></MaxPixelPointWidth>
                       <MinPixelPointWidth><%= MinPixelPointWidth %></MinPixelPointWidth>
                       <OpenCloseStyle><%= OpenCloseStyle %></OpenCloseStyle>
                       <PixelPointDepth><%= PixelPointDepth %></PixelPointDepth>
                       <PixelPointGapDepth><%= PixelPointGapDepth %></PixelPointGapDepth>
                       <PixelPointWidth><%= PixelPointWidth %></PixelPointWidth>
                       <PointWidth><%= PointWidth %></PointWidth>
                       <ShowOpenClose><%= ShowOpenClose %></ShowOpenClose>
                       <ChartLabel>
                           <Name><%= ChartLabel.Name %></Name>
                           <Text><%= ChartLabel.Text %></Text>
                           <Alignment><%= ChartLabel.Alignment %></Alignment>
                           <FontName><%= ChartLabel.FontName %></FontName>
                           <Color><%= ChartLabel.Color %></Color>
                           <Size><%= ChartLabel.Size %></Size>
                           <Bold><%= ChartLabel.Bold %></Bold>
                           <Italic><%= ChartLabel.Italic %></Italic>
                           <Underline><%= ChartLabel.Underline %></Underline>
                           <Strikeout><%= ChartLabel.Strikeout %></Strikeout>
                       </ChartLabel>
                       <XAxis>
                           <TitleText><%= XAxis.Title.Text %></TitleText>
                           <TitleFontName><%= XAxis.Title.FontName %></TitleFontName>
                           <TitleFontColor><%= XAxis.Title.Color %></TitleFontColor>
                           <TitleSize><%= XAxis.Title.Size %></TitleSize>
                           <TitleBold><%= XAxis.Title.Bold %></TitleBold>
                           <TitleItalic><%= XAxis.Title.Italic %></TitleItalic>
                           <TitleUnderline><%= XAxis.Title.Underline %></TitleUnderline>
                           <TitleStrikeout><%= XAxis.Title.Strikeout %></TitleStrikeout>
                           <TitleAlignment><%= XAxis.TitleAlignment %></TitleAlignment>
                           <AutoMinimum><%= XAxis.AutoMinimum %></AutoMinimum>
                           <Minimum><%= XAxis.Minimum %></Minimum>
                           <AutoMaximum><%= XAxis.AutoMaximum %></AutoMaximum>
                           <Maximum><%= XAxis.Maximum %></Maximum>
                       </XAxis>
                       <YAxis>
                           <TitleText><%= YAxis.Title.Text %></TitleText>
                           <TitleFontName><%= YAxis.Title.FontName %></TitleFontName>
                           <TitleFontColor><%= YAxis.Title.Color %></TitleFontColor>
                           <TitleSize><%= YAxis.Title.Size %></TitleSize>
                           <TitleBold><%= YAxis.Title.Bold %></TitleBold>
                           <TitleItalic><%= YAxis.Title.Italic %></TitleItalic>
                           <TitleUnderline><%= YAxis.Title.Underline %></TitleUnderline>
                           <TitleStrikeout><%= YAxis.Title.Strikeout %></TitleStrikeout>
                           <TitleAlignment><%= YAxis.TitleAlignment %></TitleAlignment>
                           <AutoMinimum><%= YAxis.AutoMinimum %></AutoMinimum>
                           <Minimum><%= YAxis.Minimum %></Minimum>
                           <AutoMaximum><%= YAxis.AutoMaximum %></AutoMaximum>
                           <Maximum><%= YAxis.Maximum %></Maximum>
                       </YAxis>
                   </ChartSettings>

        Return XDoc
    End Function

    'Save the Stock Chart settings in a file named FileName.
    Public Sub SaveFile(ByVal myFileName As String)

        If myFileName = "" Then 'No stock chart settings file has been selected.
            Exit Sub
        End If

        DataLocation.SaveXmlData(myFileName, ToXDoc)

    End Sub

#End Region 'Methods -----------------------------------------------------------------------------------------------------


#Region "Events" '--------------------------------------------------------------------------------------------------------

    Event ErrorMessage(ByVal Message As String) 'Send an error message.
    Event Message(ByVal Message As String) 'Send a normal message.

#End Region 'Events ------------------------------------------------------------------------------------------------------

End Class
