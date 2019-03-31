'create a new vb.net Consol Application project and paste into Module1.vb
'created with MS VS Express 2013

Imports System.Text
Imports System.IO
Imports System.Reflection
Imports System.Data
Imports System.Linq

Module Module1
    Dim TextFileTable As DataTable = Nothing
    Dim programPath As String = IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\"

    Sub Main()
        Console.WriteLine(programPath)
        Dim Directory As New IO.DirectoryInfo(programPath)
        'Dim Directory As New IO.DirectoryInfo(programPath)
        'Console.WriteLine(programPath)
        Console.WriteLine("SuperSID File Converter v1.0 (c) James Hilton")
        Console.WriteLine("This program adds a timestamp column to a supersid format CSV.")
        Console.WriteLine("It supports up to 10 stations")
        Console.WriteLine("")
        Console.WriteLine("Processing files.....")
        Dim allFiles As IO.FileInfo() = Directory.GetFiles("*.csv")
        Dim singleFile As IO.FileInfo


        For Each singleFile In allFiles
            'Console.WriteLine(singleFile.FullName)
            Console.WriteLine(singleFile.Name)
            'Console.WriteLine(singleFile.DirectoryName)


            Try
                Dim TextFileReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(singleFile.FullName)
                TextFileReader.TextFieldType = FileIO.FieldType.Delimited
                TextFileReader.SetDelimiters(",")
                Dim Column As DataColumn
                Dim Row As DataRow
                Dim UpperBound As Int32
                Dim ColumnCount As Int32
                Dim CurrentRow As String()
                While Not TextFileReader.EndOfData
                    Try
                        CurrentRow = TextFileReader.ReadFields()
                        If Not CurrentRow Is Nothing Then
                            ''# Check if DataTable has been created
                            If TextFileTable Is Nothing Then
                                TextFileTable = New DataTable("TextFileTable")
                                ''# Get number of columns
                                UpperBound = CurrentRow.GetUpperBound(0)
                                ''# Create new DataTable
                                For ColumnCount = 0 To 10
                                    Column = New DataColumn()
                                    Column.DataType = System.Type.GetType("System.String")
                                    Column.ColumnName = "Column" & ColumnCount
                                    Column.Caption = "Column" & ColumnCount
                                    Column.ReadOnly = False
                                    Column.Unique = False
                                    TextFileTable.Columns.Add(Column)
                                Next
                            End If
                            Row = TextFileTable.NewRow
                            UpperBound = CurrentRow.GetUpperBound(0)
                            For ColumnCount = 0 To UpperBound
                                Row("Column" & ColumnCount) = CurrentRow(ColumnCount).ToString
                            Next
                            TextFileTable.Rows.Add(Row)
                        End If
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    End Try
                End While
                TextFileReader.Dispose()
                TextFileTable.Columns.Add("DateTime").SetOrdinal(0)
                'DataGridView1.DataSource = TextFileTable
                'edit datatable for date etc
                Dim logint As Integer = TextFileTable.Rows(10)("Column0").Remove(0, 16)
                Dim utc_start_time As String = TextFileTable.Rows(9)("Column0").Remove(0, 18)
                Dim oDate As DateTime = DateTime.ParseExact(utc_start_time, "yyyy-MM-dd HH:mm:ss", Nothing)
                oDate = oDate.AddSeconds((-15 * logint))
                Dim format As String = "yyyy-MM-dd HH:mm:ss"
                For Each row1 As DataRow In TextFileTable.Rows
                    row1.Item("DateTime") = oDate.ToString(format)
                    oDate = oDate.AddSeconds(10)
                Next row1
                Dim station As String = TextFileTable.Rows(13)("Column0").Remove(0, 13)
                Dim freq As String = TextFileTable.Rows(14)("Column0").Remove(0, 16)
                TextFileTable.Rows(13)("Column0") = station
                TextFileTable.Rows(14)("Column0") = freq
                TextFileTable.Rows(13)("DateTime") = "Stations"
                TextFileTable.Rows(14)("DateTime") = "Frequencies"
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                TextFileTable.Rows.Remove(TextFileTable.Rows(0))
                Dim col_count As Integer = 0
                If TextFileTable.Rows(1)("Column10").ToString = "" Then
                    TextFileTable.Columns.Remove("Column10")
                End If
                If TextFileTable.Rows(1)("Column9").ToString = "" Then
                    TextFileTable.Columns.Remove("Column9")
                End If
                If TextFileTable.Rows(1)("Column8").ToString = "" Then
                    TextFileTable.Columns.Remove("Column8")
                End If
                If TextFileTable.Rows(1)("Column7").ToString = "" Then
                    TextFileTable.Columns.Remove("Column7")
                End If
                If TextFileTable.Rows(1)("Column6").ToString = "" Then
                    TextFileTable.Columns.Remove("Column6")
                End If
                If TextFileTable.Rows(1)("Column5").ToString = "" Then
                    TextFileTable.Columns.Remove("Column5")
                End If
                If TextFileTable.Rows(1)("Column4").ToString = "" Then
                    TextFileTable.Columns.Remove("Column4")
                End If
                If TextFileTable.Rows(1)("Column3").ToString = "" Then
                    TextFileTable.Columns.Remove("Column3")
                End If
                If TextFileTable.Rows(1)("Column2").ToString = "" Then
                    TextFileTable.Columns.Remove("Column2")
                End If
                If TextFileTable.Rows(1)("Column1").ToString = "" Then
                    TextFileTable.Columns.Remove("Column1")
                End If
                Dim path_check As String = programPath + "output"
                'If Not System.IO.Directory.Exists(programPath) Then
                System.IO.Directory.CreateDirectory(path_check)
                'End If
                Dim new_filename As String = singleFile.Name.Remove(singleFile.Name.Length - 4)
                Dim new_filenameandpath As String = path_check + "\" + new_filename + "_clean.csv"
                Using writer As StreamWriter = New StreamWriter(new_filenameandpath)
                    WriteDataTable(TextFileTable, writer, False)
                End Using
                'Console.WriteLine("got here")
                'TextFileTable.Reset()
                TextFileTable = Nothing
                'Console.WriteLine("got here2")
                'TextFileTable.Clear(
            Catch ex As Exception
                TextFileTable = Nothing
                Console.WriteLine("Error while processing" + singleFile.Name.ToString)
            End Try
        Next
        Console.WriteLine("")
        Console.WriteLine("Processing files 100% complete. Press any key to exit")
        Console.ReadKey()

    End Sub

    Sub WriteDataTable(ByVal sourceTable As DataTable, ByVal writer As TextWriter, ByVal includeHeaders As Boolean)
        'source is https://www.codeproject.com/Tips/665519/Writing-a-DataTable-to-a-CSV-File
        '(c) Thomas Corey 8 Sep 2017
        'source code for this RFC 4180-compliant CSV writer is licensed under The Code Project Open License (CPOL)
        'converted from c# to vb.net using http://converter.telerik.com/

        If includeHeaders Then
            Dim headerValues As IEnumerable(Of String) = sourceTable.Columns.OfType(Of DataColumn)().[Select](Function(column) QuoteValue(column.ColumnName))
            writer.WriteLine(String.Join(",", headerValues))
        End If

        Dim items As IEnumerable(Of String) = Nothing

        For Each row As DataRow In sourceTable.Rows
            items = row.ItemArray.[Select](Function(o) QuoteValue(If(o.ToString(), String.Empty)))
            writer.WriteLine(String.Join(",", items))
        Next

        writer.Flush()
    End Sub

    Private Function QuoteValue(ByVal value As String) As String
        'source is https://www.codeproject.com/Tips/665519/Writing-a-DataTable-to-a-CSV-File
        '(c) Thomas Corey 8 Sep 2017
        'source code for this RFC 4180-compliant CSV writer is licensed under The Code Project Open License (CPOL)
        'converted from c# to vb.net using http://converter.telerik.com/
        Return String.Concat("""", value.Replace("""", """"""), """")
    End Function


End Module
