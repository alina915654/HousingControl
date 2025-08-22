using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HousingControl.Forms.Add;


namespace HousingControl.Forms.Inspector
{
    public partial class ActiveViolationsForm : Form
    {
        private string connectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=HousingControl;Integrated Security=True;TrustServerCertificate=True";
        private DataTable activeViolationsTable;
        private readonly int _userId; 
        private readonly string _passedConnectionString; 
        public ActiveViolationsForm ( )
        {
            InitializeComponent ();
            this._userId = 0; 
            this._passedConnectionString = this.connectionString; 
            this.Text = "Активные нарушения (Все)";
        }

        public ActiveViolationsForm ( int userId, string connString )
        {
            InitializeComponent ();
            this._userId = userId;
            this._passedConnectionString = connString;
            this.connectionString = connString; 
            this.Text = "Мои активные нарушения";
        }

        private void ActiveViolationsForm_Load ( object sender, EventArgs e )
        {
            SetupDataGridView ();
            PopulateSortComboBox ();
            LoadActiveViolations ();
        }

        private void SetupDataGridView ( )
        {
            dgvActiveViolations.AutoGenerateColumns = false;
            dgvActiveViolations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActiveViolations.MultiSelect = false;
            dgvActiveViolations.ReadOnly = true;
            dgvActiveViolations.AllowUserToAddRows = false;
            dgvActiveViolations.AllowUserToDeleteRows = false;
            dgvActiveViolations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActiveViolations.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold ); // Используем YaHei для консистентности

            dgvActiveViolations.Columns.Clear ();
            dgvActiveViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ViolationId",
                HeaderText = "ID Нарушения",
                Name = "ViolationIdColumn",
                Visible = false
            } );
            dgvActiveViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionId",
                HeaderText = "ID Проверки",
                Name = "InspectionIdColumn",
                Visible = false
            } );
            dgvActiveViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Address",
                HeaderText = "Адрес дома",
                Name = "AddressColumn",
                MinimumWidth = 150,
                FillWeight = 20
            } );
            dgvActiveViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionDate",
                HeaderText = "Дата проверки",
                Name = "InspectionDateColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" },
                Width = 120,
                FillWeight = 15
            } );
            dgvActiveViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectorName",
                HeaderText = "Инспектор",
                Name = "InspectorNameColumn",
                Width = 150,
                FillWeight = 15
            } );
            dgvActiveViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ViolationType",
                HeaderText = "Тип нарушения",
                Name = "ViolationTypeColumn",
                Width = 150,
                FillWeight = 20
            } );
            dgvActiveViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Описание",
                Name = "DescriptionColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet,
                MinimumWidth = 200,
                FillWeight = 25
            } );
            dgvActiveViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Deadline",
                HeaderText = "Крайний срок",
                Name = "DeadlineColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" },
                Width = 120,
                FillWeight = 15
            } );
            dgvActiveViolations.Columns.Add ( new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsFixed",
                HeaderText = "Исправлено",
                Name = "IsFixedColumn",
                Visible = false 
            } );
        }

        private void PopulateSortComboBox ( )
        {
            cmbSort.Items.Clear ();
            cmbSort.Items.Add ( new SortOption ( "Без сортировки", "" ) );
            cmbSort.Items.Add ( new SortOption ( "Крайний срок (ближайшие)", "v.Deadline ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "Крайний срок (дальние)", "v.Deadline DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "Адрес дома (А-Я)", "b.Address ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "Тип нарушения (А-Я)", "v.ViolationType ASC" ) );

            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
        }

        private void LoadActiveViolations ( )
        {
            string baseQuery = @"SELECT 
                                    v.ViolationId, 
                                    v.InspectionId, 
                                    b.Address, 
                                    i.InspectionDate, 
                                    u.FullName AS InspectorName, -- НОВОЕ: Получаем имя инспектора из таблицы Users
                                    v.ViolationType, 
                                    v.Description, 
                                    v.Deadline, 
                                    v.IsFixed
                                FROM Violations v
                                JOIN Inspections i ON v.InspectionId = i.InspectionId
                                JOIN Buildings b ON i.BuildingId = b.BuildingId
                                LEFT JOIN Users u ON i.UserId = u.UserId -- НОВОЕ: Соединяем с Users для получения FullName
                                WHERE v.IsFixed = 0"; 

            List<string> conditions = new List<string> ();
            List<SqlParameter> parameters = new List<SqlParameter> ();

            if ( _userId != 0 )
            {
                conditions.Add ( "i.UserId = @UserId" ); 
                parameters.Add ( new SqlParameter ( "@UserId", _userId ) );
            }

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                string searchTerm = $"%{txtSearch.Text.Trim ()}%";
                conditions.Add ( "(b.Address LIKE @searchTerm OR u.FullName LIKE @searchTerm OR v.ViolationType LIKE @searchTerm OR v.Description LIKE @searchTerm)" );
                parameters.Add ( new SqlParameter ( "@searchTerm", searchTerm ) );
            }

            string finalQuery = baseQuery;
            if ( conditions.Any () )
            {
                finalQuery += " AND " + string.Join ( " AND ", conditions );
            }

            if ( cmbSort.SelectedItem != null && cmbSort.SelectedItem is SortOption selectedSort )
            {
                if ( !string.IsNullOrEmpty ( selectedSort.SqlOrderBy ) )
                {
                    finalQuery += " ORDER BY " + selectedSort.SqlOrderBy;
                }
                else
                {
                    finalQuery += " ORDER BY v.Deadline ASC, v.ViolationId DESC"; 
                }
            }
            else
            {
                finalQuery += " ORDER BY v.Deadline ASC, v.ViolationId DESC"; 
            }

            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( finalQuery, conn ) )
                    {
                        if ( parameters.Any () )
                        {
                            cmd.Parameters.AddRange ( parameters.ToArray () );
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter ( cmd );
                        activeViolationsTable = new DataTable ();
                        adapter.Fill ( activeViolationsTable );
                        dgvActiveViolations.DataSource = activeViolationsTable;

                        if ( activeViolationsTable.Rows.Count == 0 && !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
                        {
                            MessageBox.Show ( "Активные нарушения не найдены по текущим критериям поиска.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки активных нарушений: {ex.Message}\n\nSQL Запрос:\n{finalQuery}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void txtSearch_TextChanged ( object sender, EventArgs e )
        {
            LoadActiveViolations ();
        }

        private void cmbSort_SelectedIndexChanged ( object sender, EventArgs e )
        {
            LoadActiveViolations ();
        }

        private void btnSbros_Click ( object sender, EventArgs e )
        {
            txtSearch.Clear ();
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;

            LoadActiveViolations ();
        }

        private void btnMarkFixed_Click ( object sender, EventArgs e )
        {
            if ( dgvActiveViolations.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите нарушение, чтобы отметить его как исправленное.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedViolationId = Convert.ToInt32 ( dgvActiveViolations.SelectedRows [ 0 ].Cells [ "ViolationIdColumn" ].Value );
            int inspectionIdToUpdateStatus = Convert.ToInt32 ( dgvActiveViolations.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );

            bool isFixed = Convert.ToBoolean ( dgvActiveViolations.SelectedRows [ 0 ].Cells [ "IsFixedColumn" ].Value );
            if ( isFixed ) { MessageBox.Show ( "Выбранное нарушение уже отмечено как исправленное." ); return; }

            if ( MessageBox.Show ( "Отметить выбранное нарушение как исправленное?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
            {
                string updateQuery = "UPDATE Violations SET IsFixed = 1 WHERE ViolationId = @ViolationId";
                try
                {
                    using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                    {
                        conn.Open ();
                        using ( SqlCommand cmd = new SqlCommand ( updateQuery, conn ) )
                        {
                            cmd.Parameters.AddWithValue ( "@ViolationId", selectedViolationId );
                            cmd.ExecuteNonQuery ();
                        }
                    }
                    LoadActiveViolations (); 
                    UpdateInspectionViolationsFoundStatus ( inspectionIdToUpdateStatus ); 
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Ошибка при обновлении статуса нарушения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void UpdateInspectionViolationsFoundStatus ( int inspectionId )
        {
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    SqlCommand checkCmd = new SqlCommand ( "SELECT COUNT(*) FROM Violations WHERE InspectionId = @InspectionId AND IsFixed = 0", conn );
                    checkCmd.Parameters.AddWithValue ( "@InspectionId", inspectionId );
                    bool violationsStillFound = ( int ) checkCmd.ExecuteScalar () > 0;

                    SqlCommand updateCmd = new SqlCommand ( "UPDATE Inspections SET ViolationsFound = @ViolationsFound WHERE InspectionId = @InspectionId", conn );
                    updateCmd.Parameters.AddWithValue ( "@ViolationsFound", violationsStillFound );
                    updateCmd.Parameters.AddWithValue ( "@InspectionId", inspectionId );
                    updateCmd.ExecuteNonQuery ();
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка обновления статуса нарушений в проверке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnInspDet_Click ( object sender, EventArgs e )
        {
            if ( dgvActiveViolations.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите нарушение, чтобы посмотреть детали связанной проверки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int inspectionIdForDetails = Convert.ToInt32 ( dgvActiveViolations.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );

            using ( InspectionDetailForm inspectionDetailForm = new InspectionDetailForm ( connectionString, inspectionIdForDetails, true ) )
            {
                inspectionDetailForm.ShowDialog ( this );
            }
        }

        private void btnExit_Click ( object sender, EventArgs e )
        {
            InspectorMainForm inspectorMainForm = new InspectorMainForm ( _userId, _passedConnectionString );
            inspectorMainForm.Show ();
            this.Hide ();
        }

        private void dgvActiveViolations_CellDoubleClick ( object sender, DataGridViewCellEventArgs e )
        {
            if ( e.RowIndex >= 0 )
            {
                btnInspDet_Click ( sender, e );
            }
        }

        private class SortOption
        {
            public string DisplayName
            {
                get; set;
            }
            public string SqlOrderBy
            {
                get; set;
            }

            public SortOption ( string displayName, string sqlOrderBy )
            {
                DisplayName = displayName;
                SqlOrderBy = sqlOrderBy;
            }

            public override string ToString ( )
            {
                return DisplayName;
            }
        }
    }
}