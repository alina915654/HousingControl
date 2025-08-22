using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Reflection; // Используется для Missing.Value, если нужно
using System.Drawing.Printing;

namespace HousingControl.Forms.Admin
{
    public partial class ViolationsReportForm : Form
    {
        private string connectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=HousingControl;Integrated Security=True;TrustServerCertificate=True";
        private DataTable reportData;
        private DataTable inspectorsTable = new DataTable ();
        private readonly int _currentUserId;
        private readonly string _passedConnectionString;

        // Удалите локальные объявления printDocument1 и printPreviewDialog1,
        // так как они теперь будут объявлены в Designer.cs
        // private System.Drawing.Printing.PrintDocument printDocument1;
        // private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;

        public ViolationsReportForm ( int userId, string connString )
        {
            InitializeComponent ();
            _currentUserId = userId;
            _passedConnectionString = connString;
            // Убедитесь, что PrintDocument и PrintPreviewDialog инициализированы в Designer.cs.
            // Если они не инициализированы там, вы можете сделать это здесь:
            // this.printDocument1 = new System.Drawing.Printing.PrintDocument ();
            // this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog ();
            // this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler ( this.printDocument1_PrintPage );
        }

        private void ViolationsReportForm_Load ( object sender, EventArgs e )
        {
            SetupDataGridView ();
            LoadInspectorsForCombo ();

            // Отсоединяем/присоединяем обработчики, чтобы гарантировать, что LoadReportData()
            // не вызывается преждевременно или несколько раз при инициализации фильтров.
            dtpDateFrom.ValueChanged -= dtpDateFrom_ValueChanged_Handler;
            dtpDateTo.ValueChanged -= dtpDateTo_ValueChanged_Handler;
            cmbInspector.SelectedIndexChanged -= cmbInspector_SelectedIndexChanged_Handler;
            chkOnlyUnfixed.CheckedChanged -= chkOnlyUnfixed_CheckedChanged_Handler;

            ResetFilters (); // Сбрасываем фильтры к значениям по умолчанию

            dtpDateFrom.ValueChanged += dtpDateFrom_ValueChanged_Handler;
            dtpDateTo.ValueChanged += dtpDateTo_ValueChanged_Handler;
            cmbInspector.SelectedIndexChanged += cmbInspector_SelectedIndexChanged_Handler;
            chkOnlyUnfixed.CheckedChanged += chkOnlyUnfixed_CheckedChanged_Handler;

            // Если эти обработчики уже привязаны в Designer.cs, удалите их здесь.
            // this.btnExportToExcel.Click += new System.EventHandler ( this.btnExportToExcel_Click );
            // this.btnPrintPreview.Click += new System.EventHandler ( this.btnPrintPreview_Click );
            // this.btnResetFilters.Click += new System.EventHandler ( this.btnResetFilters_Click );
            // this.btnExit.Click += new System.EventHandler ( this.btnExit_Click );

            LoadReportData (); // Первая загрузка данных после всех настроек
        }

        // Именованные обработчики для фильтров
        private void dtpDateFrom_ValueChanged_Handler ( object sender, EventArgs e )
        {
            LoadReportData ();
        }
        private void dtpDateTo_ValueChanged_Handler ( object sender, EventArgs e )
        {
            LoadReportData ();
        }
        private void cmbInspector_SelectedIndexChanged_Handler ( object sender, EventArgs e )
        {
            LoadReportData ();
        }
        private void chkOnlyUnfixed_CheckedChanged_Handler ( object sender, EventArgs e )
        {
            LoadReportData ();
        }

        private void SetupDataGridView ( )
        {
            dgvReport.AutoGenerateColumns = false;
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReport.MultiSelect = false;
            dgvReport.ReadOnly = true;
            dgvReport.AllowUserToAddRows = false;
            dgvReport.AllowUserToDeleteRows = false;
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold );
            dgvReport.Columns.Clear ();
            dgvReport.Columns.Add ( new DataGridViewTextBoxColumn { DataPropertyName = "ViolationId", HeaderText = "ID Нарушения", Visible = false } );
            dgvReport.Columns.Add ( new DataGridViewTextBoxColumn { DataPropertyName = "InspectionId", HeaderText = "ID Проверки", Visible = false } );
            dgvReport.Columns.Add ( new DataGridViewTextBoxColumn { DataPropertyName = "Address", HeaderText = "Адрес дома", MinimumWidth = 150 } );
            dgvReport.Columns.Add ( new DataGridViewTextBoxColumn { DataPropertyName = "InspectionDate", HeaderText = "Дата проверки", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } } );
            dgvReport.Columns.Add ( new DataGridViewTextBoxColumn { DataPropertyName = "InspectorFullName", HeaderText = "Инспектор", MinimumWidth = 120 } );
            dgvReport.Columns.Add ( new DataGridViewTextBoxColumn { DataPropertyName = "ViolationType", HeaderText = "Тип нарушения", MinimumWidth = 120 } );
            dgvReport.Columns.Add ( new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Описание", AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, MinimumWidth = 200 } );
            dgvReport.Columns.Add ( new DataGridViewTextBoxColumn { DataPropertyName = "Deadline", HeaderText = "Крайний срок", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } } );
            dgvReport.Columns.Add ( new DataGridViewCheckBoxColumn { DataPropertyName = "IsFixed", HeaderText = "Исправлено" } );
        }

        private void LoadInspectorsForCombo ( )
        {
            try
            {
                using ( SqlConnection conn = new SqlConnection ( this.connectionString ) )
                {
                    string query = "SELECT UserId, FullName FROM Users WHERE Role = N'Инспектор' ORDER BY FullName";
                    SqlDataAdapter adapter = new SqlDataAdapter ( query, conn );
                    inspectorsTable.Clear ();
                    adapter.Fill ( inspectorsTable );

                    DataRow allInspectorsRow = inspectorsTable.NewRow ();
                    allInspectorsRow [ "UserId" ] = 0;
                    allInspectorsRow [ "FullName" ] = "Все инспекторы";
                    inspectorsTable.Rows.InsertAt ( allInspectorsRow, 0 );

                    cmbInspector.DataSource = inspectorsTable;
                    cmbInspector.DisplayMember = "FullName";
                    cmbInspector.ValueMember = "UserId";
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки списка инспекторов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void ResetFilters ( )
        {
            // Отсоединяем обработчики, чтобы установка значений не вызывала LoadReportData() сразу
            dtpDateFrom.ValueChanged -= dtpDateFrom_ValueChanged_Handler;
            dtpDateTo.ValueChanged -= dtpDateTo_ValueChanged_Handler;
            cmbInspector.SelectedIndexChanged -= cmbInspector_SelectedIndexChanged_Handler;
            chkOnlyUnfixed.CheckedChanged -= chkOnlyUnfixed_CheckedChanged_Handler;

            dtpDateFrom.Value = DateTime.Today.AddYears ( -1 );
            dtpDateTo.Value = DateTime.Today;
            if ( cmbInspector.Items.Count > 0 )
            {
                cmbInspector.SelectedValue = 0;
            }
            chkOnlyUnfixed.Checked = false;

            // Присоединяем обработчики обратно
            dtpDateFrom.ValueChanged += dtpDateFrom_ValueChanged_Handler;
            dtpDateTo.ValueChanged += dtpDateTo_ValueChanged_Handler;
            cmbInspector.SelectedIndexChanged += cmbInspector_SelectedIndexChanged_Handler;
            chkOnlyUnfixed.CheckedChanged += chkOnlyUnfixed_CheckedChanged_Handler;
        }

        private void LoadReportData ( )
        {
            DateTime dateFrom = dtpDateFrom.Value.Date;
            DateTime dateTo = dtpDateTo.Value.Date;
            int? selectedInspectorId = cmbInspector.SelectedValue as int?;
            bool onlyUnfixed = chkOnlyUnfixed.Checked;

            string baseQuery = @"SELECT 
                                    v.ViolationId, 
                                    v.InspectionId, 
                                    b.Address, 
                                    i.InspectionDate, 
                                    u.FullName AS InspectorFullName, 
                                    v.ViolationType, 
                                    v.Description, 
                                    v.Deadline, 
                                    v.IsFixed
                                FROM Violations v
                                JOIN Inspections i ON v.InspectionId = i.InspectionId
                                JOIN Buildings b ON i.BuildingId = b.BuildingId
                                JOIN Users u ON i.UserId = u.UserId";

            List<string> conditions = new List<string> ();
            List<SqlParameter> parameters = new List<SqlParameter> ();

            conditions.Add ( "i.InspectionDate >= @DateFrom AND i.InspectionDate <= @DateTo" );
            parameters.Add ( new SqlParameter ( "@DateFrom", dateFrom ) );
            parameters.Add ( new SqlParameter ( "@DateTo", dateTo.AddDays ( 1 ).AddMilliseconds ( -1 ) ) );

            if ( selectedInspectorId.HasValue && selectedInspectorId.Value > 0 )
            {
                conditions.Add ( "i.UserId = @InspectorId" );
                parameters.Add ( new SqlParameter ( "@InspectorId", selectedInspectorId.Value ) );
            }

            if ( onlyUnfixed )
            {
                conditions.Add ( "v.IsFixed = 0" );
            }

            string finalQuery = baseQuery;
            if ( conditions.Any () )
            {
                finalQuery += " WHERE " + string.Join ( " AND ", conditions );
            }
            finalQuery += " ORDER BY i.InspectionDate DESC, b.Address ASC";

            try
            {
                using ( SqlConnection conn = new SqlConnection ( this.connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( finalQuery, conn ) )
                    {
                        cmd.Parameters.AddRange ( parameters.ToArray () );

                        SqlDataAdapter adapter = new SqlDataAdapter ( cmd );
                        reportData = new DataTable ();
                        adapter.Fill ( reportData );
                        dgvReport.DataSource = reportData;

                        if ( reportData.Rows.Count == 0 )
                        {
                            MessageBox.Show ( "По текущим критериям отчет не содержит данных.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка формирования отчета: {ex.Message}\n\nSQL Запрос:\n{finalQuery}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnExportToExcel_Click ( object sender, EventArgs e )
        {
            if ( dgvReport.Rows.Count == 0 )
            {
                MessageBox.Show ( "Нет данных для экспорта.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application ();
                excelApp.Visible = true;
                workbook = excelApp.Workbooks.Add ( Missing.Value );
                worksheet = ( Microsoft.Office.Interop.Excel.Worksheet ) workbook.ActiveSheet;

                int excelColumnCounter = 1;
                for ( int i = 0 ; i < dgvReport.ColumnCount ; i++ )
                {
                    if ( dgvReport.Columns [ i ].Visible )
                    {
                        worksheet.Cells [ 1, excelColumnCounter ] = dgvReport.Columns [ i ].HeaderText;
                        excelColumnCounter++;
                    }
                }

                for ( int i = 0 ; i < dgvReport.Rows.Count ; i++ )
                {
                    excelColumnCounter = 1;
                    for ( int j = 0 ; j < dgvReport.Columns.Count ; j++ )
                    {
                        if ( dgvReport.Columns [ j ].Visible )
                        {
                            worksheet.Cells [ i + 2, excelColumnCounter ] = dgvReport.Rows [ i ].Cells [ j ].Value?.ToString ();
                            excelColumnCounter++;
                        }
                    }
                }

                worksheet.Columns.AutoFit ();

                MessageBox.Show ( "Данные успешно экспортированы в Excel.", "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при экспорте в Excel: " + ex.Message, "Ошибка экспорта", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            finally
            {
                ReleaseObject ( worksheet );
                ReleaseObject ( workbook );
                ReleaseObject ( excelApp );
            }
        }

        private void ReleaseObject ( object obj )
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject ( obj );
                obj = null;
            }
            catch ( Exception )
            {
                obj = null;
            }
            finally
            {
                GC.Collect ();
            }
        }

        private int currentPage = 0;
        private int currentRow = 0;
        // private System.Drawing.Printing.PrintDocument printDocument1; // Удалены локальные объявления
        // private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1; // Удалены локальные объявления

        private void btnPrintPreview_Click ( object sender, EventArgs e )
        {
            if ( dgvReport.Rows.Count == 0 )
            {
                MessageBox.Show ( "Нет данных для печати.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            printDocument1.DefaultPageSettings.Landscape = true; // Альбомная ориентация
            printDocument1.DefaultPageSettings.Margins = new Margins ( 50, 50, 50, 50 ); // Поля

            currentPage = 0;
            currentRow = 0;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog ();
        }

        private void btnResetFilters_Click ( object sender, EventArgs e )
        {
            ResetFilters ();
            LoadReportData ();
        }

        private void btnExit_Click ( object sender, EventArgs e )
        {
            this.Close ();
            AdminMainForm adminMainFormInstance = Application.OpenForms.OfType<AdminMainForm> ().FirstOrDefault ();
            if ( adminMainFormInstance != null )
            {
                adminMainFormInstance.Show ();
            }
            else
            {
                AdminMainForm adminMainForm = new AdminMainForm ( _currentUserId, _passedConnectionString );
                adminMainForm.Show ();
            }
        }

        private void printDocument1_PrintPage_1 ( object sender, PrintPageEventArgs e )
        {
            Graphics graphics = e.Graphics;
            Font headerFont = new Font ( "Arial", 10, FontStyle.Bold );
            Font cellFont = new Font ( "Arial", 9 );
            float headerHeight = headerFont.GetHeight ( graphics ) + 5;
            float cellLineHeight = cellFont.GetHeight ( graphics ) + 3; // Отступ между строками

            int startX = e.MarginBounds.Left;
            int startY = e.MarginBounds.Top;
            int offset = 0; // Для отступа от верха страницы/полей

            // Заголовок отчета
            string reportTitle = "Отчет по нарушениям";
            Font titleFont = new Font ( "Arial", 16, FontStyle.Bold );
            SizeF titleSize = graphics.MeasureString ( reportTitle, titleFont );
            graphics.DrawString ( reportTitle, titleFont, Brushes.Black, startX + ( e.MarginBounds.Width - titleSize.Width ) / 2, startY + offset );
            offset += ( int ) titleSize.Height + 20;

            // Расчет ширины колонок для печати
            List<DataGridViewColumn> visibleColumns = dgvReport.Columns.Cast<DataGridViewColumn> ().Where ( c => c.Visible ).ToList ();
            float [ ] columnPrintWidths = new float [ visibleColumns.Count ];

            // Определяем веса для колонок, чтобы они выглядели хорошо при печати
            // Сумма этих весов должна быть 1.0 (или просто пропорциональной, если пересчитываем)
            // Эти веса будут использоваться для распределения доступной ширины страницы.
            Dictionary<string, float> columnWeights = new Dictionary<string, float>
            {
                { "Address", 0.18f }, // 18% ширины
                { "InspectionDate", 0.10f }, // 10%
                { "InspectorFullName", 0.15f }, // 15%
                { "ViolationType", 0.15f }, // 15%
                { "Description", 0.30f }, // 30% - самое широкое описание
                { "Deadline", 0.07f }, // 7%
                { "IsFixed", 0.05f } // 5%
            };

            float totalPageWidth = e.MarginBounds.Width; // Доступная ширина страницы
            for ( int i = 0 ; i < visibleColumns.Count ; i++ )
            {
                string dataPropName = visibleColumns [ i ].DataPropertyName;
                if ( columnWeights.ContainsKey ( dataPropName ) )
                {
                    columnPrintWidths [ i ] = totalPageWidth * columnWeights [ dataPropName ];
                }
                else
                {
                    columnPrintWidths [ i ] = totalPageWidth * ( 1.0f / visibleColumns.Count ); // Равномерно, если нет веса
                }
            }


            // Рисуем заголовки таблицы
            int currentX = startX;
            int headerMaxHeight = ( int ) headerHeight; // Для многострочных заголовков, если понадобится

            for ( int i = 0 ; i < visibleColumns.Count ; i++ )
            {
                DataGridViewColumn col = visibleColumns [ i ];
                Rectangle headerRect = new Rectangle ( ( int ) currentX, startY + offset, ( int ) columnPrintWidths [ i ], headerMaxHeight );
                graphics.FillRectangle ( Brushes.LightGray, headerRect );
                graphics.DrawRectangle ( Pens.Black, headerRect );

                StringFormat headerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.LineLimit };
                graphics.DrawString ( col.HeaderText, headerFont, Brushes.Black, headerRect, headerFormat );
                currentX += ( int ) columnPrintWidths [ i ];
            }
            offset += headerMaxHeight;

            // Рисуем данные таблицы
            while ( currentRow < reportData.Rows.Count )
            {
                currentX = startX;
                int y = startY + offset; // Объявляем y внутри цикла, чтобы он был обновлен для каждой новой строки

                // Измеряем высоту текущей строки с учетом переноса текста
                float rowMaxHeight = cellLineHeight; // Минимальная высота строки

                for ( int i = 0 ; i < visibleColumns.Count ; i++ )
                {
                    DataGridViewColumn col = visibleColumns [ i ];
                    object cellValue = reportData.Rows [ currentRow ] [ col.DataPropertyName ];
                    string textToDraw = cellValue?.ToString () ?? string.Empty;

                    // Специальная обработка для IsFixed (CheckBoxColumn)
                    if ( col.DataPropertyName == "IsFixed" )
                    {
                        textToDraw = ( bool ) cellValue ? "Да" : "Нет";
                    }
                    // Специальная обработка для дат
                    else if ( col.DataPropertyName.Contains ( "Date" ) || col.DataPropertyName.Contains ( "Deadline" ) )
                    {
                        if ( cellValue != DBNull.Value && cellValue is DateTime dateValue )
                        {
                            textToDraw = dateValue.ToString ( "dd.MM.yyyy" );
                        }
                        else
                        {
                            textToDraw = string.Empty;
                        }
                    }

                    // Измеряем размер текста, который может быть многострочным
                    StringFormat cellFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.LineLimit };
                    SizeF textSize = graphics.MeasureString ( textToDraw, cellFont, ( int ) columnPrintWidths [ i ], cellFormat );
                    rowMaxHeight = Math.Max ( rowMaxHeight, textSize.Height + 5 ); // 5 - небольшой отступ
                }

                // Проверка, помещается ли строка на текущей странице
                if ( y + rowMaxHeight > e.MarginBounds.Bottom )
                {
                    e.HasMorePages = true; // Есть еще страницы
                    return; // Завершаем текущую страницу
                }

                // Рисуем ячейки текущей строки
                for ( int i = 0 ; i < visibleColumns.Count ; i++ )
                {
                    DataGridViewColumn col = visibleColumns [ i ];
                    Rectangle cellRect = new Rectangle ( ( int ) currentX, ( int ) y, ( int ) columnPrintWidths [ i ], ( int ) rowMaxHeight );
                    graphics.DrawRectangle ( Pens.Black, cellRect ); // Рисуем рамку ячейки

                    object cellValue = reportData.Rows [ currentRow ] [ col.DataPropertyName ];
                    string textToDraw = cellValue?.ToString () ?? string.Empty;

                    // Специальная обработка для CheckBox и дат (повторяем, чтобы избежать проблем)
                    if ( col.DataPropertyName == "IsFixed" )
                    {
                        textToDraw = ( bool ) cellValue ? "Да" : "Нет";
                    }
                    else if ( col.DataPropertyName.Contains ( "Date" ) || col.DataPropertyName.Contains ( "Deadline" ) )
                    {
                        if ( cellValue != DBNull.Value && cellValue is DateTime dateValue )
                        {
                            textToDraw = dateValue.ToString ( "dd.MM.yyyy" );
                        }
                        else
                        {
                            textToDraw = string.Empty;
                        }
                    }

                    // Рисуем текст в ячейке с переносом
                    StringFormat cellFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.LineLimit };
                    graphics.DrawString ( textToDraw, cellFont, Brushes.Black, cellRect, cellFormat );

                    currentX += ( int ) columnPrintWidths [ i ];
                }
                offset += ( int ) rowMaxHeight; // Переходим к следующей строке
                currentRow++;
            }
            e.HasMorePages = false; // Нет больше страниц
        }
    }
}