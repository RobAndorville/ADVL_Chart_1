Public Class frmViewDatabaseData
    'Form used to view databse data selected for charting

#Region " Variable Declarations - All the variables used in this form and this application." '-------------------------------------------------------------------------------------------------

    'Declare forms opened from this form:
    'Public WithEvents DesignQuery As frmDesignQuery

    ''Variables used to connect to a database and open a table:
    'Dim connString As String
    'Public myConnection As OleDb.OleDbConnection = New OleDb.OleDbConnection
    'Public ds As DataSet = New DataSet
    'Dim da As OleDb.OleDbDataAdapter
    'Dim tables As DataTableCollection = ds.Tables

    'Public DatabasePath As String = "" 'The path to the selected database
    'Public TableName As String = ""    'The name of the table selected for viewing
    'Public Query As String = ""        'The text of the query used to display table values

    'Dim UpdateNeeded As Boolean

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Properties - All the properties used in this form and this application" '------------------------------------------------------------------------------------------------------------

    'Private _query As String = "" 'The Query property stores the text of the SQL query used to display table values in DataGridView1
    ''Public Property Query() As String
    'Public Property Query As String
    '    Get
    '        Return _query
    '    End Get
    '    Set(ByVal value As String)
    '        _query = value
    '        'If _query = "" Then
    '        '    _query = "SELECT TOP 500 * FROM " & TableName
    '        'End If
    '        txtQuery.Text = _query
    '    End Set
    'End Property

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
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"

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

        RestoreFormSettings()   'Restore the form settings

        'DataGridView1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect
        'DataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText

        ' DataGridView1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form
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

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Form Methods - The main actions performed by this form." '---------------------------------------------------------------------------------------------------------------------------

    Public Sub Update()
        txtDataDescr.Text = Main.InputDataDescr
        txtQuery.Text = Main.InputQuery
        'ApplyQuery()

        FillDataGridView()

    End Sub
    'Public Sub ApplyQuery()
    '    'Apply the specified databse query and display the data in DataGridView1

    '    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" & Main.InputDatabasePath 'DatabasePath 
    '    myConnection.ConnectionString = connString
    '    myConnection.Open()

    '    'da = New OleDb.OleDbDataAdapter(Query, myConnection)
    '    da = New OleDb.OleDbDataAdapter(Main.InputQuery, myConnection)

    '    da.MissingSchemaAction = MissingSchemaAction.AddWithKey

    '    ds.Clear()
    '    ds.Reset()
    '    Try
    '        'da.Fill(ds, TableName)
    '        da.Fill(ds, "myData")

    '        DataGridView1.AutoGenerateColumns = True

    '        DataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke

    '        DataGridView1.DataSource = ds.Tables(0)
    '        DataGridView1.AutoResizeColumns()

    '        DataGridView1.Update()
    '        DataGridView1.Refresh()
    '    Catch ex As Exception
    '        Main.Message.Add("Error applying query." & vbCrLf)
    '        Main.Message.Add(ex.Message & vbCrLf & vbCrLf)
    '    End Try

    '    myConnection.Close()
    'End Sub

    Private Sub FillDataGridView()
        'Fill DataGridView1 with the table in Main.ds

        Try
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke
            DataGridView1.DataSource = Main.ds.Tables("SelTable")
            DataGridView1.AutoResizeColumns()
            DataGridView1.Update()
            DataGridView1.Refresh()
        Catch ex As Exception
            Main.Message.Add("Error showing data." & vbCrLf)
            Main.Message.Add(ex.Message & vbCrLf & vbCrLf)
        End Try

        If Main.ds.Tables.Count > 0 Then
            txtNRecords.Text = Main.ds.Tables(0).Rows.Count
            If DataGridView1.SelectedCells.Count > 0 Then
                txtSelectedRecord.Text = DataGridView1.SelectedCells.Item(0).RowIndex
            Else
                txtSelectedRecord.Text = ""
            End If
        End If


    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.SelectedCells.Count > 0 Then
            txtSelectedRecord.Text = DataGridView1.SelectedCells.Item(0).RowIndex
        Else
            txtSelectedRecord.Text = ""
        End If
    End Sub

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------






End Class