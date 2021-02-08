<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnOnline = New System.Windows.Forms.Button()
        Me.btnMessages = New System.Windows.Forms.Button()
        Me.btnAppInfo = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.chkAutoDraw = New System.Windows.Forms.CheckBox()
        Me.btnNewChartWindow = New System.Windows.Forms.Button()
        Me.txtChartType = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtChartFileName = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.rbNewWindowChart = New System.Windows.Forms.RadioButton()
        Me.rbPreviewChart = New System.Windows.Forms.RadioButton()
        Me.btnDrawChart = New System.Windows.Forms.Button()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.btnApplyQuery = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtDataDescription = New System.Windows.Forms.TextBox()
        Me.btnViewData = New System.Windows.Forms.Button()
        Me.btnDesignQuery = New System.Windows.Forms.Button()
        Me.lstFields = New System.Windows.Forms.ListBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lstTables = New System.Windows.Forms.ListBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtInputQuery = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbDatabaseType = New System.Windows.Forms.ComboBox()
        Me.btnDatabase = New System.Windows.Forms.Button()
        Me.txtDatabasePath = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.rbDataset = New System.Windows.Forms.RadioButton()
        Me.rbDatabase = New System.Windows.Forms.RadioButton()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.btnNewChart = New System.Windows.Forms.Button()
        Me.txtSeriesName = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtChartDescr = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbChartType = New System.Windows.Forms.ComboBox()
        Me.cmbXValues = New System.Windows.Forms.ComboBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.cmbAlignment = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnChartTitleFont = New System.Windows.Forms.Button()
        Me.txtChartTitle = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TabPage10 = New System.Windows.Forms.TabPage()
        Me.chkXAxisAutoMajGridInt = New System.Windows.Forms.CheckBox()
        Me.txtXAxisMajGridInt = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.chkXAxisAutoAnnotInt = New System.Windows.Forms.CheckBox()
        Me.txtXAxisAnnotInt = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.chkXAxisAutoMax = New System.Windows.Forms.CheckBox()
        Me.chkXAxisAutoMin = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.txtXAxisMax = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtXAxisMin = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbXAxisTitleAlignment = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnXAxisTitleFont = New System.Windows.Forms.Button()
        Me.txtXAxisTitle = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TabPage11 = New System.Windows.Forms.TabPage()
        Me.chkYAxisAutoMajGridInt = New System.Windows.Forms.CheckBox()
        Me.txtYAxisMajGridInt = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.chkYAxisAutoAnnotInt = New System.Windows.Forms.CheckBox()
        Me.txtYAxisAnnotInt = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.chkYAxisAutoMax = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker4 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker3 = New System.Windows.Forms.DateTimePicker()
        Me.chkYAxisAutoMin = New System.Windows.Forms.CheckBox()
        Me.txtYAxisMax = New System.Windows.Forms.TextBox()
        Me.txtYAxisMin = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cmbYAxisTitleAlignment = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.btnYAxisTitleFont = New System.Windows.Forms.Button()
        Me.txtYAxisTitle = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkConnect = New System.Windows.Forms.CheckBox()
        Me.btnOpenProject = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtProjectPath = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtProNetName = New System.Windows.Forms.TextBox()
        Me.btnOpenAppDir = New System.Windows.Forms.Button()
        Me.btnOpenSystem = New System.Windows.Forms.Button()
        Me.btnOpenData = New System.Windows.Forms.Button()
        Me.btnOpenSettings = New System.Windows.Forms.Button()
        Me.btnParameters = New System.Windows.Forms.Button()
        Me.txtParentProject = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.txtSystemLocationType = New System.Windows.Forms.TextBox()
        Me.txtSystemLocationPath = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtCurrentDuration = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtTotalDuration = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtLastUsed = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCreationDate = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDataLocationPath = New System.Windows.Forms.TextBox()
        Me.txtDataLocationType = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSettingsLocationPath = New System.Windows.Forms.TextBox()
        Me.txtSettingsLocationType = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtProjectType = New System.Windows.Forms.TextBox()
        Me.txtProjectDescription = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtProjectName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnProject = New System.Windows.Forms.Button()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAndorville = New System.Windows.Forms.Button()
        Me.btnWebPages = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1_EditWorkflowTabPage = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1_ShowStartPageInWorkflowTab = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage9.SuspendLayout()
        Me.TabPage10.SuspendLayout()
        Me.TabPage11.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(719, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(64, 22)
        Me.btnExit.TabIndex = 51
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnOnline
        '
        Me.btnOnline.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOnline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOnline.ForeColor = System.Drawing.Color.Red
        Me.btnOnline.Location = New System.Drawing.Point(657, 12)
        Me.btnOnline.Name = "btnOnline"
        Me.btnOnline.Size = New System.Drawing.Size(56, 22)
        Me.btnOnline.TabIndex = 52
        Me.btnOnline.Text = "Offline"
        Me.btnOnline.UseVisualStyleBackColor = True
        '
        'btnMessages
        '
        Me.btnMessages.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMessages.Location = New System.Drawing.Point(579, 12)
        Me.btnMessages.Name = "btnMessages"
        Me.btnMessages.Size = New System.Drawing.Size(72, 22)
        Me.btnMessages.TabIndex = 53
        Me.btnMessages.Text = "Messages"
        Me.btnMessages.UseVisualStyleBackColor = True
        '
        'btnAppInfo
        '
        Me.btnAppInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAppInfo.Location = New System.Drawing.Point(478, 12)
        Me.btnAppInfo.Name = "btnAppInfo"
        Me.btnAppInfo.Size = New System.Drawing.Size(95, 22)
        Me.btnAppInfo.TabIndex = 54
        Me.btnAppInfo.Text = "Application Info"
        Me.btnAppInfo.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(12, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(771, 467)
        Me.TabControl1.TabIndex = 55
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.WebBrowser1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(763, 441)
        Me.TabPage2.TabIndex = 5
        Me.TabPage2.Text = "Start Page"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(793, 435)
        Me.WebBrowser1.TabIndex = 69
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.chkAutoDraw)
        Me.TabPage5.Controls.Add(Me.btnNewChartWindow)
        Me.TabPage5.Controls.Add(Me.txtChartType)
        Me.TabPage5.Controls.Add(Me.Label34)
        Me.TabPage5.Controls.Add(Me.txtChartFileName)
        Me.TabPage5.Controls.Add(Me.Label33)
        Me.TabPage5.Controls.Add(Me.btnSave)
        Me.TabPage5.Controls.Add(Me.btnNew)
        Me.TabPage5.Controls.Add(Me.btnOpen)
        Me.TabPage5.Controls.Add(Me.rbNewWindowChart)
        Me.TabPage5.Controls.Add(Me.rbPreviewChart)
        Me.TabPage5.Controls.Add(Me.btnDrawChart)
        Me.TabPage5.Controls.Add(Me.Chart1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(763, 441)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Chart"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'chkAutoDraw
        '
        Me.chkAutoDraw.AutoSize = True
        Me.chkAutoDraw.Location = New System.Drawing.Point(219, 12)
        Me.chkAutoDraw.Name = "chkAutoDraw"
        Me.chkAutoDraw.Size = New System.Drawing.Size(76, 17)
        Me.chkAutoDraw.TabIndex = 66
        Me.chkAutoDraw.Text = "Auto Draw"
        Me.chkAutoDraw.UseVisualStyleBackColor = True
        '
        'btnNewChartWindow
        '
        Me.btnNewChartWindow.Location = New System.Drawing.Point(501, 8)
        Me.btnNewChartWindow.Name = "btnNewChartWindow"
        Me.btnNewChartWindow.Size = New System.Drawing.Size(119, 22)
        Me.btnNewChartWindow.TabIndex = 56
        Me.btnNewChartWindow.Text = "New Chart Window"
        Me.btnNewChartWindow.UseVisualStyleBackColor = True
        '
        'txtChartType
        '
        Me.txtChartType.Location = New System.Drawing.Point(91, 62)
        Me.txtChartType.Name = "txtChartType"
        Me.txtChartType.Size = New System.Drawing.Size(219, 20)
        Me.txtChartType.TabIndex = 65
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(5, 65)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(58, 13)
        Me.Label34.TabIndex = 64
        Me.Label34.Text = "Chart type:"
        '
        'txtChartFileName
        '
        Me.txtChartFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtChartFileName.Location = New System.Drawing.Point(91, 36)
        Me.txtChartFileName.Name = "txtChartFileName"
        Me.txtChartFileName.Size = New System.Drawing.Size(633, 20)
        Me.txtChartFileName.TabIndex = 63
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(5, 39)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(80, 13)
        Me.Label33.TabIndex = 62
        Me.Label33.Text = "Chart file name:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(112, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(46, 22)
        Me.btnSave.TabIndex = 61
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(60, 8)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(46, 22)
        Me.btnNew.TabIndex = 60
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(8, 8)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(46, 22)
        Me.btnOpen.TabIndex = 59
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'rbNewWindowChart
        '
        Me.rbNewWindowChart.AutoSize = True
        Me.rbNewWindowChart.Location = New System.Drawing.Point(370, 11)
        Me.rbNewWindowChart.Name = "rbNewWindowChart"
        Me.rbNewWindowChart.Size = New System.Drawing.Size(125, 17)
        Me.rbNewWindowChart.TabIndex = 58
        Me.rbNewWindowChart.TabStop = True
        Me.rbNewWindowChart.Text = "Show in new window"
        Me.rbNewWindowChart.UseVisualStyleBackColor = True
        '
        'rbPreviewChart
        '
        Me.rbPreviewChart.AutoSize = True
        Me.rbPreviewChart.Location = New System.Drawing.Point(301, 11)
        Me.rbPreviewChart.Name = "rbPreviewChart"
        Me.rbPreviewChart.Size = New System.Drawing.Size(63, 17)
        Me.rbPreviewChart.TabIndex = 57
        Me.rbPreviewChart.TabStop = True
        Me.rbPreviewChart.Text = "Preview"
        Me.rbPreviewChart.UseVisualStyleBackColor = True
        '
        'btnDrawChart
        '
        Me.btnDrawChart.Location = New System.Drawing.Point(164, 8)
        Me.btnDrawChart.Name = "btnDrawChart"
        Me.btnDrawChart.Size = New System.Drawing.Size(49, 22)
        Me.btnDrawChart.TabIndex = 56
        Me.btnDrawChart.Text = "Draw"
        Me.btnDrawChart.UseVisualStyleBackColor = True
        '
        'Chart1
        '
        Me.Chart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Chart1.BorderlineColor = System.Drawing.Color.Black
        Me.Chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(3, 91)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(757, 330)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label31)
        Me.TabPage3.Controls.Add(Me.TabControl2)
        Me.TabPage3.Controls.Add(Me.rbDataset)
        Me.TabPage3.Controls.Add(Me.rbDatabase)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(763, 441)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Input Data"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(4, 14)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(81, 13)
        Me.Label31.TabIndex = 23
        Me.Label31.Text = "Input data type:"
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPage6)
        Me.TabControl2.Controls.Add(Me.TabPage7)
        Me.TabControl2.Location = New System.Drawing.Point(3, 37)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(757, 401)
        Me.TabControl2.TabIndex = 22
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.btnApplyQuery)
        Me.TabPage6.Controls.Add(Me.Label32)
        Me.TabPage6.Controls.Add(Me.txtDataDescription)
        Me.TabPage6.Controls.Add(Me.btnViewData)
        Me.TabPage6.Controls.Add(Me.btnDesignQuery)
        Me.TabPage6.Controls.Add(Me.lstFields)
        Me.TabPage6.Controls.Add(Me.Label15)
        Me.TabPage6.Controls.Add(Me.lstTables)
        Me.TabPage6.Controls.Add(Me.Label13)
        Me.TabPage6.Controls.Add(Me.txtInputQuery)
        Me.TabPage6.Controls.Add(Me.Label1)
        Me.TabPage6.Controls.Add(Me.Label12)
        Me.TabPage6.Controls.Add(Me.cmbDatabaseType)
        Me.TabPage6.Controls.Add(Me.btnDatabase)
        Me.TabPage6.Controls.Add(Me.txtDatabasePath)
        Me.TabPage6.Controls.Add(Me.Label14)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(749, 375)
        Me.TabPage6.TabIndex = 0
        Me.TabPage6.Text = "Database"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'btnApplyQuery
        '
        Me.btnApplyQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApplyQuery.Location = New System.Drawing.Point(6, 343)
        Me.btnApplyQuery.Name = "btnApplyQuery"
        Me.btnApplyQuery.Size = New System.Drawing.Size(56, 22)
        Me.btnApplyQuery.TabIndex = 59
        Me.btnApplyQuery.Text = "Apply"
        Me.btnApplyQuery.UseVisualStyleBackColor = True
        '
        'Label32
        '
        Me.Label32.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(6, 245)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(87, 13)
        Me.Label32.TabIndex = 58
        Me.Label32.Text = "Data description:"
        '
        'txtDataDescription
        '
        Me.txtDataDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDataDescription.Location = New System.Drawing.Point(102, 242)
        Me.txtDataDescription.Name = "txtDataDescription"
        Me.txtDataDescription.Size = New System.Drawing.Size(641, 20)
        Me.txtDataDescription.TabIndex = 57
        '
        'btnViewData
        '
        Me.btnViewData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnViewData.Location = New System.Drawing.Point(6, 315)
        Me.btnViewData.Name = "btnViewData"
        Me.btnViewData.Size = New System.Drawing.Size(56, 22)
        Me.btnViewData.TabIndex = 56
        Me.btnViewData.Text = "View"
        Me.btnViewData.UseVisualStyleBackColor = True
        '
        'btnDesignQuery
        '
        Me.btnDesignQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDesignQuery.Location = New System.Drawing.Point(6, 287)
        Me.btnDesignQuery.Name = "btnDesignQuery"
        Me.btnDesignQuery.Size = New System.Drawing.Size(56, 22)
        Me.btnDesignQuery.TabIndex = 56
        Me.btnDesignQuery.Text = "Design"
        Me.btnDesignQuery.UseVisualStyleBackColor = True
        '
        'lstFields
        '
        Me.lstFields.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstFields.FormattingEnabled = True
        Me.lstFields.Location = New System.Drawing.Point(288, 72)
        Me.lstFields.Name = "lstFields"
        Me.lstFields.Size = New System.Drawing.Size(455, 160)
        Me.lstFields.TabIndex = 21
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(285, 56)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Fields:"
        '
        'lstTables
        '
        Me.lstTables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstTables.FormattingEnabled = True
        Me.lstTables.Location = New System.Drawing.Point(9, 72)
        Me.lstTables.Name = "lstTables"
        Me.lstTables.Size = New System.Drawing.Size(273, 160)
        Me.lstTables.TabIndex = 19
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 56)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 13)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "Tables:"
        '
        'txtInputQuery
        '
        Me.txtInputQuery.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputQuery.Location = New System.Drawing.Point(68, 268)
        Me.txtInputQuery.Multiline = True
        Me.txtInputQuery.Name = "txtInputQuery"
        Me.txtInputQuery.Size = New System.Drawing.Size(675, 101)
        Me.txtInputQuery.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 271)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Query"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 35)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 13)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "Type:"
        '
        'cmbDatabaseType
        '
        Me.cmbDatabaseType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDatabaseType.FormattingEnabled = True
        Me.cmbDatabaseType.Location = New System.Drawing.Point(68, 32)
        Me.cmbDatabaseType.Name = "cmbDatabaseType"
        Me.cmbDatabaseType.Size = New System.Drawing.Size(675, 21)
        Me.cmbDatabaseType.TabIndex = 14
        '
        'btnDatabase
        '
        Me.btnDatabase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDatabase.Location = New System.Drawing.Point(679, 5)
        Me.btnDatabase.Name = "btnDatabase"
        Me.btnDatabase.Size = New System.Drawing.Size(64, 22)
        Me.btnDatabase.TabIndex = 13
        Me.btnDatabase.Text = "Find"
        Me.btnDatabase.UseVisualStyleBackColor = True
        '
        'txtDatabasePath
        '
        Me.txtDatabasePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDatabasePath.Location = New System.Drawing.Point(68, 6)
        Me.txtDatabasePath.Name = "txtDatabasePath"
        Me.txtDatabasePath.Size = New System.Drawing.Size(605, 20)
        Me.txtDatabasePath.TabIndex = 12
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 13)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "Database:"
        '
        'TabPage7
        '
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(749, 375)
        Me.TabPage7.TabIndex = 1
        Me.TabPage7.Text = "Dataset"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'rbDataset
        '
        Me.rbDataset.AutoSize = True
        Me.rbDataset.Location = New System.Drawing.Point(168, 12)
        Me.rbDataset.Name = "rbDataset"
        Me.rbDataset.Size = New System.Drawing.Size(62, 17)
        Me.rbDataset.TabIndex = 21
        Me.rbDataset.Text = "Dataset"
        Me.rbDataset.UseVisualStyleBackColor = True
        '
        'rbDatabase
        '
        Me.rbDatabase.AutoSize = True
        Me.rbDatabase.Checked = True
        Me.rbDatabase.Location = New System.Drawing.Point(91, 12)
        Me.rbDatabase.Name = "rbDatabase"
        Me.rbDatabase.Size = New System.Drawing.Size(71, 17)
        Me.rbDatabase.TabIndex = 20
        Me.rbDatabase.TabStop = True
        Me.rbDatabase.Text = "Database"
        Me.rbDatabase.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.TabControl3)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(763, 441)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Settings"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TabControl3
        '
        Me.TabControl3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl3.Controls.Add(Me.TabPage8)
        Me.TabControl3.Controls.Add(Me.TabPage9)
        Me.TabControl3.Controls.Add(Me.TabPage10)
        Me.TabControl3.Controls.Add(Me.TabPage11)
        Me.TabControl3.Location = New System.Drawing.Point(3, 3)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(757, 435)
        Me.TabControl3.TabIndex = 73
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.btnNewChart)
        Me.TabPage8.Controls.Add(Me.txtSeriesName)
        Me.TabPage8.Controls.Add(Me.Label35)
        Me.TabPage8.Controls.Add(Me.txtChartDescr)
        Me.TabPage8.Controls.Add(Me.Label2)
        Me.TabPage8.Controls.Add(Me.Label16)
        Me.TabPage8.Controls.Add(Me.cmbChartType)
        Me.TabPage8.Controls.Add(Me.cmbXValues)
        Me.TabPage8.Controls.Add(Me.SplitContainer1)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage8.Size = New System.Drawing.Size(749, 409)
        Me.TabPage8.TabIndex = 0
        Me.TabPage8.Text = "Chart Type"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'btnNewChart
        '
        Me.btnNewChart.Location = New System.Drawing.Point(6, 33)
        Me.btnNewChart.Name = "btnNewChart"
        Me.btnNewChart.Size = New System.Drawing.Size(72, 22)
        Me.btnNewChart.TabIndex = 74
        Me.btnNewChart.Text = "New Chart"
        Me.btnNewChart.UseVisualStyleBackColor = True
        '
        'txtSeriesName
        '
        Me.txtSeriesName.Location = New System.Drawing.Point(177, 33)
        Me.txtSeriesName.Name = "txtSeriesName"
        Me.txtSeriesName.Size = New System.Drawing.Size(161, 20)
        Me.txtSeriesName.TabIndex = 73
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(103, 36)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(68, 13)
        Me.Label35.TabIndex = 72
        Me.Label35.Text = "Series name:"
        '
        'txtChartDescr
        '
        Me.txtChartDescr.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtChartDescr.Location = New System.Drawing.Point(6, 320)
        Me.txtChartDescr.Multiline = True
        Me.txtChartDescr.Name = "txtChartDescr"
        Me.txtChartDescr.Size = New System.Drawing.Size(737, 83)
        Me.txtChartDescr.TabIndex = 65
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(249, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "X Values:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(9, 9)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(58, 13)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Chart type:"
        '
        'cmbChartType
        '
        Me.cmbChartType.FormattingEnabled = True
        Me.cmbChartType.Location = New System.Drawing.Point(73, 6)
        Me.cmbChartType.Name = "cmbChartType"
        Me.cmbChartType.Size = New System.Drawing.Size(170, 21)
        Me.cmbChartType.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbChartType, "Chart types in brackets have not yet been implemented.")
        '
        'cmbXValues
        '
        Me.cmbXValues.FormattingEnabled = True
        Me.cmbXValues.Location = New System.Drawing.Point(307, 6)
        Me.cmbXValues.Name = "cmbXValues"
        Me.cmbXValues.Size = New System.Drawing.Size(232, 21)
        Me.cmbXValues.TabIndex = 71
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(6, 61)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label17)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label18)
        Me.SplitContainer1.Size = New System.Drawing.Size(711, 253)
        Me.SplitContainer1.SplitterDistance = 329
        Me.SplitContainer1.TabIndex = 69
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(6, 23)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(320, 227)
        Me.DataGridView1.TabIndex = 63
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(52, 13)
        Me.Label17.TabIndex = 66
        Me.Label17.Text = "Y Values:"
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(3, 23)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(364, 227)
        Me.DataGridView2.TabIndex = 67
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(92, 13)
        Me.Label18.TabIndex = 68
        Me.Label18.Text = "Custom Attributes:"
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.cmbAlignment)
        Me.TabPage9.Controls.Add(Me.Label19)
        Me.TabPage9.Controls.Add(Me.btnChartTitleFont)
        Me.TabPage9.Controls.Add(Me.txtChartTitle)
        Me.TabPage9.Controls.Add(Me.Label20)
        Me.TabPage9.Location = New System.Drawing.Point(4, 22)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage9.Size = New System.Drawing.Size(749, 409)
        Me.TabPage9.TabIndex = 1
        Me.TabPage9.Text = "Titles"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'cmbAlignment
        '
        Me.cmbAlignment.FormattingEnabled = True
        Me.cmbAlignment.Location = New System.Drawing.Point(74, 52)
        Me.cmbAlignment.Name = "cmbAlignment"
        Me.cmbAlignment.Size = New System.Drawing.Size(255, 21)
        Me.cmbAlignment.TabIndex = 6
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 55)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(56, 13)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "Alignment:"
        '
        'btnChartTitleFont
        '
        Me.btnChartTitleFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChartTitleFont.Location = New System.Drawing.Point(689, 5)
        Me.btnChartTitleFont.Name = "btnChartTitleFont"
        Me.btnChartTitleFont.Size = New System.Drawing.Size(54, 22)
        Me.btnChartTitleFont.TabIndex = 4
        Me.btnChartTitleFont.Text = "Font"
        Me.btnChartTitleFont.UseVisualStyleBackColor = True
        '
        'txtChartTitle
        '
        Me.txtChartTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtChartTitle.Location = New System.Drawing.Point(73, 6)
        Me.txtChartTitle.Name = "txtChartTitle"
        Me.txtChartTitle.Size = New System.Drawing.Size(610, 20)
        Me.txtChartTitle.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 9)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(58, 13)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Chart Title:"
        '
        'TabPage10
        '
        Me.TabPage10.Controls.Add(Me.chkXAxisAutoMajGridInt)
        Me.TabPage10.Controls.Add(Me.txtXAxisMajGridInt)
        Me.TabPage10.Controls.Add(Me.Label36)
        Me.TabPage10.Controls.Add(Me.chkXAxisAutoAnnotInt)
        Me.TabPage10.Controls.Add(Me.txtXAxisAnnotInt)
        Me.TabPage10.Controls.Add(Me.Label27)
        Me.TabPage10.Controls.Add(Me.chkXAxisAutoMax)
        Me.TabPage10.Controls.Add(Me.chkXAxisAutoMin)
        Me.TabPage10.Controls.Add(Me.DateTimePicker2)
        Me.TabPage10.Controls.Add(Me.DateTimePicker1)
        Me.TabPage10.Controls.Add(Me.txtXAxisMax)
        Me.TabPage10.Controls.Add(Me.Label21)
        Me.TabPage10.Controls.Add(Me.txtXAxisMin)
        Me.TabPage10.Controls.Add(Me.Label22)
        Me.TabPage10.Controls.Add(Me.cmbXAxisTitleAlignment)
        Me.TabPage10.Controls.Add(Me.Label23)
        Me.TabPage10.Controls.Add(Me.btnXAxisTitleFont)
        Me.TabPage10.Controls.Add(Me.txtXAxisTitle)
        Me.TabPage10.Controls.Add(Me.Label24)
        Me.TabPage10.Location = New System.Drawing.Point(4, 22)
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage10.Size = New System.Drawing.Size(749, 409)
        Me.TabPage10.TabIndex = 2
        Me.TabPage10.Text = "X Axis"
        Me.TabPage10.UseVisualStyleBackColor = True
        '
        'chkXAxisAutoMajGridInt
        '
        Me.chkXAxisAutoMajGridInt.AutoSize = True
        Me.chkXAxisAutoMajGridInt.Location = New System.Drawing.Point(282, 170)
        Me.chkXAxisAutoMajGridInt.Name = "chkXAxisAutoMajGridInt"
        Me.chkXAxisAutoMajGridInt.Size = New System.Drawing.Size(48, 17)
        Me.chkXAxisAutoMajGridInt.TabIndex = 23
        Me.chkXAxisAutoMajGridInt.Text = "Auto"
        Me.chkXAxisAutoMajGridInt.UseVisualStyleBackColor = True
        '
        'txtXAxisMajGridInt
        '
        Me.txtXAxisMajGridInt.Location = New System.Drawing.Point(113, 168)
        Me.txtXAxisMajGridInt.Name = "txtXAxisMajGridInt"
        Me.txtXAxisMajGridInt.Size = New System.Drawing.Size(163, 20)
        Me.txtXAxisMajGridInt.TabIndex = 22
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(9, 171)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(93, 13)
        Me.Label36.TabIndex = 21
        Me.Label36.Text = "Major grid interval:"
        '
        'chkXAxisAutoAnnotInt
        '
        Me.chkXAxisAutoAnnotInt.AutoSize = True
        Me.chkXAxisAutoAnnotInt.Location = New System.Drawing.Point(282, 145)
        Me.chkXAxisAutoAnnotInt.Name = "chkXAxisAutoAnnotInt"
        Me.chkXAxisAutoAnnotInt.Size = New System.Drawing.Size(48, 17)
        Me.chkXAxisAutoAnnotInt.TabIndex = 20
        Me.chkXAxisAutoAnnotInt.Text = "Auto"
        Me.chkXAxisAutoAnnotInt.UseVisualStyleBackColor = True
        '
        'txtXAxisAnnotInt
        '
        Me.txtXAxisAnnotInt.Location = New System.Drawing.Point(113, 143)
        Me.txtXAxisAnnotInt.Name = "txtXAxisAnnotInt"
        Me.txtXAxisAnnotInt.Size = New System.Drawing.Size(163, 20)
        Me.txtXAxisAnnotInt.TabIndex = 19
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(9, 146)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(98, 13)
        Me.Label27.TabIndex = 18
        Me.Label27.Text = "Annotation interval:"
        '
        'chkXAxisAutoMax
        '
        Me.chkXAxisAutoMax.AutoSize = True
        Me.chkXAxisAutoMax.Location = New System.Drawing.Point(605, 83)
        Me.chkXAxisAutoMax.Name = "chkXAxisAutoMax"
        Me.chkXAxisAutoMax.Size = New System.Drawing.Size(48, 17)
        Me.chkXAxisAutoMax.TabIndex = 17
        Me.chkXAxisAutoMax.Text = "Auto"
        Me.chkXAxisAutoMax.UseVisualStyleBackColor = True
        '
        'chkXAxisAutoMin
        '
        Me.chkXAxisAutoMin.AutoSize = True
        Me.chkXAxisAutoMin.Location = New System.Drawing.Point(282, 83)
        Me.chkXAxisAutoMin.Name = "chkXAxisAutoMin"
        Me.chkXAxisAutoMin.Size = New System.Drawing.Size(48, 17)
        Me.chkXAxisAutoMin.TabIndex = 16
        Me.chkXAxisAutoMin.Text = "Auto"
        Me.chkXAxisAutoMin.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(74, 107)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(202, 20)
        Me.DateTimePicker2.TabIndex = 15
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(397, 107)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(202, 20)
        Me.DateTimePicker1.TabIndex = 14
        '
        'txtXAxisMax
        '
        Me.txtXAxisMax.Location = New System.Drawing.Point(397, 81)
        Me.txtXAxisMax.Name = "txtXAxisMax"
        Me.txtXAxisMax.Size = New System.Drawing.Size(202, 20)
        Me.txtXAxisMax.TabIndex = 13
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(361, 84)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(30, 13)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "Max:"
        '
        'txtXAxisMin
        '
        Me.txtXAxisMin.Location = New System.Drawing.Point(74, 81)
        Me.txtXAxisMin.Name = "txtXAxisMin"
        Me.txtXAxisMin.Size = New System.Drawing.Size(202, 20)
        Me.txtXAxisMin.TabIndex = 11
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(41, 84)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(27, 13)
        Me.Label22.TabIndex = 10
        Me.Label22.Text = "Min:"
        '
        'cmbXAxisTitleAlignment
        '
        Me.cmbXAxisTitleAlignment.FormattingEnabled = True
        Me.cmbXAxisTitleAlignment.Location = New System.Drawing.Point(74, 52)
        Me.cmbXAxisTitleAlignment.Name = "cmbXAxisTitleAlignment"
        Me.cmbXAxisTitleAlignment.Size = New System.Drawing.Size(255, 21)
        Me.cmbXAxisTitleAlignment.TabIndex = 9
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 55)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(56, 13)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "Alignment:"
        '
        'btnXAxisTitleFont
        '
        Me.btnXAxisTitleFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnXAxisTitleFont.Location = New System.Drawing.Point(689, 5)
        Me.btnXAxisTitleFont.Name = "btnXAxisTitleFont"
        Me.btnXAxisTitleFont.Size = New System.Drawing.Size(54, 22)
        Me.btnXAxisTitleFont.TabIndex = 2
        Me.btnXAxisTitleFont.Text = "Font"
        Me.btnXAxisTitleFont.UseVisualStyleBackColor = True
        '
        'txtXAxisTitle
        '
        Me.txtXAxisTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtXAxisTitle.Location = New System.Drawing.Point(74, 6)
        Me.txtXAxisTitle.Name = "txtXAxisTitle"
        Me.txtXAxisTitle.Size = New System.Drawing.Size(609, 20)
        Me.txtXAxisTitle.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 9)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(62, 13)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "X Axis Title:"
        '
        'TabPage11
        '
        Me.TabPage11.Controls.Add(Me.chkYAxisAutoMajGridInt)
        Me.TabPage11.Controls.Add(Me.txtYAxisMajGridInt)
        Me.TabPage11.Controls.Add(Me.Label26)
        Me.TabPage11.Controls.Add(Me.chkYAxisAutoAnnotInt)
        Me.TabPage11.Controls.Add(Me.txtYAxisAnnotInt)
        Me.TabPage11.Controls.Add(Me.Label37)
        Me.TabPage11.Controls.Add(Me.chkYAxisAutoMax)
        Me.TabPage11.Controls.Add(Me.DateTimePicker4)
        Me.TabPage11.Controls.Add(Me.DateTimePicker3)
        Me.TabPage11.Controls.Add(Me.chkYAxisAutoMin)
        Me.TabPage11.Controls.Add(Me.txtYAxisMax)
        Me.TabPage11.Controls.Add(Me.txtYAxisMin)
        Me.TabPage11.Controls.Add(Me.Label25)
        Me.TabPage11.Controls.Add(Me.Label28)
        Me.TabPage11.Controls.Add(Me.cmbYAxisTitleAlignment)
        Me.TabPage11.Controls.Add(Me.Label29)
        Me.TabPage11.Controls.Add(Me.btnYAxisTitleFont)
        Me.TabPage11.Controls.Add(Me.txtYAxisTitle)
        Me.TabPage11.Controls.Add(Me.Label30)
        Me.TabPage11.Location = New System.Drawing.Point(4, 22)
        Me.TabPage11.Name = "TabPage11"
        Me.TabPage11.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage11.Size = New System.Drawing.Size(749, 409)
        Me.TabPage11.TabIndex = 3
        Me.TabPage11.Text = "Y Axis"
        Me.TabPage11.UseVisualStyleBackColor = True
        '
        'chkYAxisAutoMajGridInt
        '
        Me.chkYAxisAutoMajGridInt.AutoSize = True
        Me.chkYAxisAutoMajGridInt.Location = New System.Drawing.Point(282, 170)
        Me.chkYAxisAutoMajGridInt.Name = "chkYAxisAutoMajGridInt"
        Me.chkYAxisAutoMajGridInt.Size = New System.Drawing.Size(48, 17)
        Me.chkYAxisAutoMajGridInt.TabIndex = 29
        Me.chkYAxisAutoMajGridInt.Text = "Auto"
        Me.chkYAxisAutoMajGridInt.UseVisualStyleBackColor = True
        '
        'txtYAxisMajGridInt
        '
        Me.txtYAxisMajGridInt.Location = New System.Drawing.Point(113, 168)
        Me.txtYAxisMajGridInt.Name = "txtYAxisMajGridInt"
        Me.txtYAxisMajGridInt.Size = New System.Drawing.Size(163, 20)
        Me.txtYAxisMajGridInt.TabIndex = 28
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(9, 171)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(93, 13)
        Me.Label26.TabIndex = 27
        Me.Label26.Text = "Major grid interval:"
        '
        'chkYAxisAutoAnnotInt
        '
        Me.chkYAxisAutoAnnotInt.AutoSize = True
        Me.chkYAxisAutoAnnotInt.Location = New System.Drawing.Point(282, 145)
        Me.chkYAxisAutoAnnotInt.Name = "chkYAxisAutoAnnotInt"
        Me.chkYAxisAutoAnnotInt.Size = New System.Drawing.Size(48, 17)
        Me.chkYAxisAutoAnnotInt.TabIndex = 26
        Me.chkYAxisAutoAnnotInt.Text = "Auto"
        Me.chkYAxisAutoAnnotInt.UseVisualStyleBackColor = True
        '
        'txtYAxisAnnotInt
        '
        Me.txtYAxisAnnotInt.Location = New System.Drawing.Point(113, 143)
        Me.txtYAxisAnnotInt.Name = "txtYAxisAnnotInt"
        Me.txtYAxisAnnotInt.Size = New System.Drawing.Size(163, 20)
        Me.txtYAxisAnnotInt.TabIndex = 25
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(9, 146)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(98, 13)
        Me.Label37.TabIndex = 24
        Me.Label37.Text = "Annotation interval:"
        '
        'chkYAxisAutoMax
        '
        Me.chkYAxisAutoMax.AutoSize = True
        Me.chkYAxisAutoMax.Location = New System.Drawing.Point(605, 83)
        Me.chkYAxisAutoMax.Name = "chkYAxisAutoMax"
        Me.chkYAxisAutoMax.Size = New System.Drawing.Size(48, 17)
        Me.chkYAxisAutoMax.TabIndex = 12
        Me.chkYAxisAutoMax.Text = "Auto"
        Me.chkYAxisAutoMax.UseVisualStyleBackColor = True
        '
        'DateTimePicker4
        '
        Me.DateTimePicker4.Location = New System.Drawing.Point(397, 107)
        Me.DateTimePicker4.Name = "DateTimePicker4"
        Me.DateTimePicker4.Size = New System.Drawing.Size(202, 20)
        Me.DateTimePicker4.TabIndex = 11
        '
        'DateTimePicker3
        '
        Me.DateTimePicker3.Location = New System.Drawing.Point(74, 107)
        Me.DateTimePicker3.Name = "DateTimePicker3"
        Me.DateTimePicker3.Size = New System.Drawing.Size(202, 20)
        Me.DateTimePicker3.TabIndex = 10
        '
        'chkYAxisAutoMin
        '
        Me.chkYAxisAutoMin.AutoSize = True
        Me.chkYAxisAutoMin.Location = New System.Drawing.Point(282, 83)
        Me.chkYAxisAutoMin.Name = "chkYAxisAutoMin"
        Me.chkYAxisAutoMin.Size = New System.Drawing.Size(48, 17)
        Me.chkYAxisAutoMin.TabIndex = 9
        Me.chkYAxisAutoMin.Text = "Auto"
        Me.chkYAxisAutoMin.UseVisualStyleBackColor = True
        '
        'txtYAxisMax
        '
        Me.txtYAxisMax.Location = New System.Drawing.Point(397, 81)
        Me.txtYAxisMax.Name = "txtYAxisMax"
        Me.txtYAxisMax.Size = New System.Drawing.Size(202, 20)
        Me.txtYAxisMax.TabIndex = 8
        '
        'txtYAxisMin
        '
        Me.txtYAxisMin.Location = New System.Drawing.Point(74, 81)
        Me.txtYAxisMin.Name = "txtYAxisMin"
        Me.txtYAxisMin.Size = New System.Drawing.Size(202, 20)
        Me.txtYAxisMin.TabIndex = 7
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(361, 84)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(30, 13)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "Max:"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(41, 84)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(27, 13)
        Me.Label28.TabIndex = 5
        Me.Label28.Text = "Min:"
        '
        'cmbYAxisTitleAlignment
        '
        Me.cmbYAxisTitleAlignment.FormattingEnabled = True
        Me.cmbYAxisTitleAlignment.Location = New System.Drawing.Point(74, 52)
        Me.cmbYAxisTitleAlignment.Name = "cmbYAxisTitleAlignment"
        Me.cmbYAxisTitleAlignment.Size = New System.Drawing.Size(255, 21)
        Me.cmbYAxisTitleAlignment.TabIndex = 4
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 55)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(56, 13)
        Me.Label29.TabIndex = 3
        Me.Label29.Text = "Alignment:"
        '
        'btnYAxisTitleFont
        '
        Me.btnYAxisTitleFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnYAxisTitleFont.Location = New System.Drawing.Point(689, 5)
        Me.btnYAxisTitleFont.Name = "btnYAxisTitleFont"
        Me.btnYAxisTitleFont.Size = New System.Drawing.Size(54, 22)
        Me.btnYAxisTitleFont.TabIndex = 2
        Me.btnYAxisTitleFont.Text = "Font"
        Me.btnYAxisTitleFont.UseVisualStyleBackColor = True
        '
        'txtYAxisTitle
        '
        Me.txtYAxisTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtYAxisTitle.Location = New System.Drawing.Point(74, 6)
        Me.txtYAxisTitle.Name = "txtYAxisTitle"
        Me.txtYAxisTitle.Size = New System.Drawing.Size(609, 20)
        Me.txtYAxisTitle.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(6, 9)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(62, 13)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "Y Axis Title:"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.chkConnect)
        Me.TabPage1.Controls.Add(Me.btnOpenProject)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtProjectPath)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.txtProNetName)
        Me.TabPage1.Controls.Add(Me.btnOpenAppDir)
        Me.TabPage1.Controls.Add(Me.btnOpenSystem)
        Me.TabPage1.Controls.Add(Me.btnOpenData)
        Me.TabPage1.Controls.Add(Me.btnOpenSettings)
        Me.TabPage1.Controls.Add(Me.btnParameters)
        Me.TabPage1.Controls.Add(Me.txtParentProject)
        Me.TabPage1.Controls.Add(Me.Label45)
        Me.TabPage1.Controls.Add(Me.btnAdd)
        Me.TabPage1.Controls.Add(Me.Label80)
        Me.TabPage1.Controls.Add(Me.txtSystemLocationType)
        Me.TabPage1.Controls.Add(Me.txtSystemLocationPath)
        Me.TabPage1.Controls.Add(Me.Label38)
        Me.TabPage1.Controls.Add(Me.txtCurrentDuration)
        Me.TabPage1.Controls.Add(Me.Label39)
        Me.TabPage1.Controls.Add(Me.Label40)
        Me.TabPage1.Controls.Add(Me.txtTotalDuration)
        Me.TabPage1.Controls.Add(Me.Label41)
        Me.TabPage1.Controls.Add(Me.Label42)
        Me.TabPage1.Controls.Add(Me.txtLastUsed)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.txtCreationDate)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.txtDataLocationPath)
        Me.TabPage1.Controls.Add(Me.txtDataLocationType)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.txtSettingsLocationPath)
        Me.TabPage1.Controls.Add(Me.txtSettingsLocationType)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.txtProjectType)
        Me.TabPage1.Controls.Add(Me.txtProjectDescription)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.txtProjectName)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.btnProject)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(763, 441)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Project Information"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkConnect
        '
        Me.chkConnect.AutoSize = True
        Me.chkConnect.Location = New System.Drawing.Point(410, 139)
        Me.chkConnect.Name = "chkConnect"
        Me.chkConnect.Size = New System.Drawing.Size(112, 17)
        Me.chkConnect.TabIndex = 305
        Me.chkConnect.Text = "Connect On Open"
        Me.chkConnect.UseVisualStyleBackColor = True
        '
        'btnOpenProject
        '
        Me.btnOpenProject.Location = New System.Drawing.Point(84, 181)
        Me.btnOpenProject.Name = "btnOpenProject"
        Me.btnOpenProject.Size = New System.Drawing.Size(48, 22)
        Me.btnOpenProject.TabIndex = 304
        Me.btnOpenProject.Text = "Open"
        Me.btnOpenProject.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 303
        Me.Label5.Text = "Project path:"
        '
        'txtProjectPath
        '
        Me.txtProjectPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProjectPath.Location = New System.Drawing.Point(138, 162)
        Me.txtProjectPath.Multiline = True
        Me.txtProjectPath.Name = "txtProjectPath"
        Me.txtProjectPath.Size = New System.Drawing.Size(617, 46)
        Me.txtProjectPath.TabIndex = 302
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(162, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 13)
        Me.Label9.TabIndex = 301
        Me.Label9.Text = "Project network:"
        '
        'txtProNetName
        '
        Me.txtProNetName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProNetName.Location = New System.Drawing.Point(271, 36)
        Me.txtProNetName.Name = "txtProNetName"
        Me.txtProNetName.Size = New System.Drawing.Size(486, 20)
        Me.txtProNetName.TabIndex = 300
        Me.ToolTip1.SetToolTip(Me.txtProNetName, "The name of the Application Network containing this project")
        '
        'btnOpenAppDir
        '
        Me.btnOpenAppDir.Location = New System.Drawing.Point(6, 370)
        Me.btnOpenAppDir.Name = "btnOpenAppDir"
        Me.btnOpenAppDir.Size = New System.Drawing.Size(150, 22)
        Me.btnOpenAppDir.TabIndex = 298
        Me.btnOpenAppDir.Text = "Open Application Directory"
        Me.btnOpenAppDir.UseVisualStyleBackColor = True
        '
        'btnOpenSystem
        '
        Me.btnOpenSystem.Location = New System.Drawing.Point(84, 337)
        Me.btnOpenSystem.Name = "btnOpenSystem"
        Me.btnOpenSystem.Size = New System.Drawing.Size(48, 22)
        Me.btnOpenSystem.TabIndex = 295
        Me.btnOpenSystem.Text = "Open"
        Me.btnOpenSystem.UseVisualStyleBackColor = True
        '
        'btnOpenData
        '
        Me.btnOpenData.Location = New System.Drawing.Point(84, 285)
        Me.btnOpenData.Name = "btnOpenData"
        Me.btnOpenData.Size = New System.Drawing.Size(48, 22)
        Me.btnOpenData.TabIndex = 294
        Me.btnOpenData.Text = "Open"
        Me.btnOpenData.UseVisualStyleBackColor = True
        '
        'btnOpenSettings
        '
        Me.btnOpenSettings.Location = New System.Drawing.Point(84, 233)
        Me.btnOpenSettings.Name = "btnOpenSettings"
        Me.btnOpenSettings.Size = New System.Drawing.Size(48, 22)
        Me.btnOpenSettings.TabIndex = 293
        Me.btnOpenSettings.Text = "Open"
        Me.btnOpenSettings.UseVisualStyleBackColor = True
        '
        'btnParameters
        '
        Me.btnParameters.Location = New System.Drawing.Point(84, 6)
        Me.btnParameters.Name = "btnParameters"
        Me.btnParameters.Size = New System.Drawing.Size(72, 22)
        Me.btnParameters.TabIndex = 285
        Me.btnParameters.Text = "Parameters"
        Me.btnParameters.UseVisualStyleBackColor = True
        '
        'txtParentProject
        '
        Me.txtParentProject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtParentProject.Location = New System.Drawing.Point(271, 10)
        Me.txtParentProject.Name = "txtParentProject"
        Me.txtParentProject.Size = New System.Drawing.Size(486, 20)
        Me.txtParentProject.TabIndex = 97
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(166, 11)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(76, 13)
        Me.Label45.TabIndex = 96
        Me.Label45.Text = "Parent project:"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(6, 34)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(150, 22)
        Me.btnAdd.TabIndex = 95
        Me.btnAdd.Text = "Add to Message Service"
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add selected project to the Message Service list")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Location = New System.Drawing.Point(4, 321)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(68, 13)
        Me.Label80.TabIndex = 91
        Me.Label80.Text = "System path:"
        '
        'txtSystemLocationType
        '
        Me.txtSystemLocationType.Location = New System.Drawing.Point(6, 337)
        Me.txtSystemLocationType.Name = "txtSystemLocationType"
        Me.txtSystemLocationType.Size = New System.Drawing.Size(72, 20)
        Me.txtSystemLocationType.TabIndex = 89
        '
        'txtSystemLocationPath
        '
        Me.txtSystemLocationPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSystemLocationPath.Location = New System.Drawing.Point(138, 318)
        Me.txtSystemLocationPath.Multiline = True
        Me.txtSystemLocationPath.Name = "txtSystemLocationPath"
        Me.txtSystemLocationPath.Size = New System.Drawing.Size(617, 46)
        Me.txtSystemLocationPath.TabIndex = 88
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(562, 373)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(41, 13)
        Me.Label38.TabIndex = 79
        Me.Label38.Text = "d:h:m:s"
        '
        'txtCurrentDuration
        '
        Me.txtCurrentDuration.Location = New System.Drawing.Point(471, 370)
        Me.txtCurrentDuration.Name = "txtCurrentDuration"
        Me.txtCurrentDuration.Size = New System.Drawing.Size(85, 20)
        Me.txtCurrentDuration.TabIndex = 78
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(374, 373)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(41, 13)
        Me.Label39.TabIndex = 77
        Me.Label39.Text = "d:h:m:s"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(421, 373)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(44, 13)
        Me.Label40.TabIndex = 76
        Me.Label40.Text = "Current:"
        '
        'txtTotalDuration
        '
        Me.txtTotalDuration.Location = New System.Drawing.Point(283, 370)
        Me.txtTotalDuration.Name = "txtTotalDuration"
        Me.txtTotalDuration.Size = New System.Drawing.Size(85, 20)
        Me.txtTotalDuration.TabIndex = 75
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(243, 375)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(34, 13)
        Me.Label41.TabIndex = 74
        Me.Label41.Text = "Total:"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(162, 375)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(75, 13)
        Me.Label42.TabIndex = 73
        Me.Label42.Text = "Project usage:"
        '
        'txtLastUsed
        '
        Me.txtLastUsed.Location = New System.Drawing.Point(274, 136)
        Me.txtLastUsed.Name = "txtLastUsed"
        Me.txtLastUsed.Size = New System.Drawing.Size(120, 20)
        Me.txtLastUsed.TabIndex = 65
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(212, 139)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Last used:"
        '
        'txtCreationDate
        '
        Me.txtCreationDate.Location = New System.Drawing.Point(86, 136)
        Me.txtCreationDate.Name = "txtCreationDate"
        Me.txtCreationDate.Size = New System.Drawing.Size(120, 20)
        Me.txtCreationDate.TabIndex = 63
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 140)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "Creation date:"
        '
        'txtDataLocationPath
        '
        Me.txtDataLocationPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDataLocationPath.Location = New System.Drawing.Point(138, 266)
        Me.txtDataLocationPath.Multiline = True
        Me.txtDataLocationPath.Name = "txtDataLocationPath"
        Me.txtDataLocationPath.Size = New System.Drawing.Size(617, 46)
        Me.txtDataLocationPath.TabIndex = 61
        '
        'txtDataLocationType
        '
        Me.txtDataLocationType.Location = New System.Drawing.Point(6, 285)
        Me.txtDataLocationType.Name = "txtDataLocationType"
        Me.txtDataLocationType.Size = New System.Drawing.Size(72, 20)
        Me.txtDataLocationType.TabIndex = 60
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 269)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 58
        Me.Label8.Text = "Data path:"
        '
        'txtSettingsLocationPath
        '
        Me.txtSettingsLocationPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSettingsLocationPath.Location = New System.Drawing.Point(138, 214)
        Me.txtSettingsLocationPath.Multiline = True
        Me.txtSettingsLocationPath.Name = "txtSettingsLocationPath"
        Me.txtSettingsLocationPath.Size = New System.Drawing.Size(617, 46)
        Me.txtSettingsLocationPath.TabIndex = 57
        '
        'txtSettingsLocationType
        '
        Me.txtSettingsLocationType.Location = New System.Drawing.Point(6, 233)
        Me.txtSettingsLocationType.Name = "txtSettingsLocationType"
        Me.txtSettingsLocationType.Size = New System.Drawing.Size(72, 20)
        Me.txtSettingsLocationType.TabIndex = 55
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 217)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 54
        Me.Label6.Text = "Settings path:"
        '
        'txtProjectType
        '
        Me.txtProjectType.Location = New System.Drawing.Point(6, 181)
        Me.txtProjectType.Name = "txtProjectType"
        Me.txtProjectType.Size = New System.Drawing.Size(72, 20)
        Me.txtProjectType.TabIndex = 53
        '
        'txtProjectDescription
        '
        Me.txtProjectDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProjectDescription.Location = New System.Drawing.Point(123, 90)
        Me.txtProjectDescription.Multiline = True
        Me.txtProjectDescription.Name = "txtProjectDescription"
        Me.txtProjectDescription.Size = New System.Drawing.Size(634, 40)
        Me.txtProjectDescription.TabIndex = 51
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Description:"
        '
        'txtProjectName
        '
        Me.txtProjectName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProjectName.Location = New System.Drawing.Point(123, 64)
        Me.txtProjectName.Name = "txtProjectName"
        Me.txtProjectName.Size = New System.Drawing.Size(634, 20)
        Me.txtProjectName.TabIndex = 49
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Name:"
        '
        'btnProject
        '
        Me.btnProject.Location = New System.Drawing.Point(6, 6)
        Me.btnProject.Name = "btnProject"
        Me.btnProject.Size = New System.Drawing.Size(72, 22)
        Me.btnProject.TabIndex = 47
        Me.btnProject.Text = "Project List"
        Me.btnProject.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnAndorville
        '
        Me.btnAndorville.BackgroundImage = Global.ADVL_Chart_1.My.Resources.Resources.Andorville_16May16_TM_Crop_Grey
        Me.btnAndorville.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAndorville.Font = New System.Drawing.Font("Harlow Solid Italic", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAndorville.Location = New System.Drawing.Point(5, 5)
        Me.btnAndorville.Name = "btnAndorville"
        Me.btnAndorville.Size = New System.Drawing.Size(118, 29)
        Me.btnAndorville.TabIndex = 50
        Me.btnAndorville.UseVisualStyleBackColor = True
        '
        'btnWebPages
        '
        Me.btnWebPages.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWebPages.ContextMenuStrip = Me.ContextMenuStrip1
        Me.btnWebPages.Location = New System.Drawing.Point(396, 12)
        Me.btnWebPages.Name = "btnWebPages"
        Me.btnWebPages.Size = New System.Drawing.Size(76, 22)
        Me.btnWebPages.TabIndex = 280
        Me.btnWebPages.Text = "Workflows"
        Me.btnWebPages.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1_EditWorkflowTabPage, Me.ToolStripMenuItem1_ShowStartPageInWorkflowTab})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(248, 48)
        '
        'ToolStripMenuItem1_EditWorkflowTabPage
        '
        Me.ToolStripMenuItem1_EditWorkflowTabPage.Name = "ToolStripMenuItem1_EditWorkflowTabPage"
        Me.ToolStripMenuItem1_EditWorkflowTabPage.Size = New System.Drawing.Size(247, 22)
        Me.ToolStripMenuItem1_EditWorkflowTabPage.Text = "Edit Workflow Tab Page"
        '
        'ToolStripMenuItem1_ShowStartPageInWorkflowTab
        '
        Me.ToolStripMenuItem1_ShowStartPageInWorkflowTab.Name = "ToolStripMenuItem1_ShowStartPageInWorkflowTab"
        Me.ToolStripMenuItem1_ShowStartPageInWorkflowTab.Size = New System.Drawing.Size(247, 22)
        Me.ToolStripMenuItem1_ShowStartPageInWorkflowTab.Text = "Show Start Page In Workflow Tab"
        '
        'Timer2
        '
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 519)
        Me.Controls.Add(Me.btnWebPages)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnAppInfo)
        Me.Controls.Add(Me.btnMessages)
        Me.Controls.Add(Me.btnOnline)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnAndorville)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Text = "Chart V1-0"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabControl3.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.TabPage8.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage9.ResumeLayout(False)
        Me.TabPage9.PerformLayout()
        Me.TabPage10.ResumeLayout(False)
        Me.TabPage10.PerformLayout()
        Me.TabPage11.ResumeLayout(False)
        Me.TabPage11.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnAndorville As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents btnOnline As Button
    Friend WithEvents btnMessages As Button
    Friend WithEvents btnAppInfo As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents txtLastUsed As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtCreationDate As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtDataLocationPath As TextBox
    Friend WithEvents txtDataLocationType As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtSettingsLocationPath As TextBox
    Friend WithEvents txtSettingsLocationType As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtProjectType As TextBox
    Friend WithEvents txtProjectDescription As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtProjectName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnProject As Button
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents lstFields As ListBox
    Friend WithEvents Label15 As Label
    Friend WithEvents lstTables As ListBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtInputQuery As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbDatabaseType As ComboBox
    Friend WithEvents btnDatabase As Button
    Friend WithEvents txtDatabasePath As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents rbDataset As RadioButton
    Friend WithEvents rbDatabase As RadioButton
    Friend WithEvents TabControl3 As TabControl
    Friend WithEvents TabPage8 As TabPage
    Friend WithEvents txtChartDescr As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents cmbChartType As ComboBox
    Friend WithEvents cmbXValues As ComboBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label17 As Label
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents Label18 As Label
    Friend WithEvents TabPage9 As TabPage
    Friend WithEvents cmbAlignment As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents btnChartTitleFont As Button
    Friend WithEvents txtChartTitle As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents TabPage10 As TabPage
    Friend WithEvents chkXAxisAutoMax As CheckBox
    Friend WithEvents chkXAxisAutoMin As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents txtXAxisMax As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents txtXAxisMin As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents cmbXAxisTitleAlignment As ComboBox
    Friend WithEvents Label23 As Label
    Friend WithEvents btnXAxisTitleFont As Button
    Friend WithEvents txtXAxisTitle As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents TabPage11 As TabPage
    Friend WithEvents chkYAxisAutoMax As CheckBox
    Friend WithEvents DateTimePicker4 As DateTimePicker
    Friend WithEvents DateTimePicker3 As DateTimePicker
    Friend WithEvents chkYAxisAutoMin As CheckBox
    Friend WithEvents txtYAxisMax As TextBox
    Friend WithEvents txtYAxisMin As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents cmbYAxisTitleAlignment As ComboBox
    Friend WithEvents Label29 As Label
    Friend WithEvents btnYAxisTitleFont As Button
    Friend WithEvents txtYAxisTitle As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents btnViewData As Button
    Friend WithEvents btnDesignQuery As Button
    Friend WithEvents rbNewWindowChart As RadioButton
    Friend WithEvents rbPreviewChart As RadioButton
    Friend WithEvents btnDrawChart As Button
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label32 As Label
    Friend WithEvents txtDataDescription As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnOpen As Button
    Friend WithEvents txtChartFileName As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents txtChartType As TextBox
    Friend WithEvents Label34 As Label
    Friend WithEvents txtSeriesName As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents btnNewChartWindow As Button
    Friend WithEvents btnApplyQuery As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents chkXAxisAutoMajGridInt As CheckBox
    Friend WithEvents txtXAxisMajGridInt As TextBox
    Friend WithEvents Label36 As Label
    Friend WithEvents chkXAxisAutoAnnotInt As CheckBox
    Friend WithEvents txtXAxisAnnotInt As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents chkYAxisAutoMajGridInt As CheckBox
    Friend WithEvents txtYAxisMajGridInt As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents chkYAxisAutoAnnotInt As CheckBox
    Friend WithEvents txtYAxisAnnotInt As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents chkAutoDraw As CheckBox
    Friend WithEvents btnNewChart As Button
    Friend WithEvents Label38 As Label
    Friend WithEvents txtCurrentDuration As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents txtTotalDuration As TextBox
    Friend WithEvents Label41 As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents btnWebPages As Button
    Friend WithEvents Label80 As Label
    Friend WithEvents txtSystemLocationType As TextBox
    Friend WithEvents txtSystemLocationPath As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents txtParentProject As TextBox
    Friend WithEvents Label45 As Label
    Friend WithEvents btnOpenAppDir As Button
    Friend WithEvents btnOpenSystem As Button
    Friend WithEvents btnOpenData As Button
    Friend WithEvents btnOpenSettings As Button
    Friend WithEvents btnParameters As Button
    Friend WithEvents Timer2 As Timer
    Friend WithEvents btnOpenProject As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtProjectPath As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtProNetName As TextBox
    Friend WithEvents chkConnect As CheckBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1_EditWorkflowTabPage As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1_ShowStartPageInWorkflowTab As ToolStripMenuItem
End Class
