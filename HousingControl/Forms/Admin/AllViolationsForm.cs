using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HousingControl.Forms.Add;


namespace HousingControl.Forms.Admin
{
    public partial class AllViolationsForm : Form
    {
        private string connectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=HousingControl;Integrated Security=True;TrustServerCertificate=True";
        private DataTable violationsTable;
        private int? _filterInspectionId;
        private readonly int _userId;
        private readonly string _passedConnectionString;
        private Form _parentForm;
        public AllViolationsForm ( )
        {
            InitializeComponent ();
            this._filterInspectionId = null;
            this._userId = 0;
            this._passedConnectionString = this.connectionString;
            this._parentForm = null;
        }
        public AllViolationsForm ( int inspectionId, string connString, Form parentForm = null )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this._filterInspectionId = inspectionId;
            this.Text = $"Нарушения для проверки ID: {inspectionId}";
            this._userId = 0; 
            this._passedConnectionString = connString;
            this._parentForm = parentForm;
        }

        public AllViolationsForm ( int userId, string connString, int? filterInspectionId = null, Form parentForm = null )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this._userId = userId;
            this._passedConnectionString = connString;
            this._filterInspectionId = filterInspectionId;
            this._parentForm = parentForm;

            if ( _filterInspectionId.HasValue )
            {
                this.Text = $"Нарушения для проверки ID: {_filterInspectionId.Value}";
            }
            else
            {
                this.Text = "Все нарушения";
            }
        }
        private void AllViolationsForm_Load ( object sender, EventArgs e )
        {
            SetupDataGridView ();
            PopulateSortComboBox ();
            LoadViolations ();
        }

        private void SetupDataGridView ( )
        {
            dgvViolations.AutoGenerateColumns = false;
            dgvViolations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvViolations.MultiSelect = false;
            dgvViolations.ReadOnly = true;
            dgvViolations.AllowUserToAddRows = false;
            dgvViolations.AllowUserToDeleteRows = false;
            dgvViolations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvViolations.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold );

            dgvViolations.Columns.Clear ();
            dgvViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ViolationId",
                HeaderText = "ID Нарушения",
                Name = "ViolationIdColumn",
                Visible = false
            } );
            dgvViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionId",
                HeaderText = "ID Проверки",
                Name = "InspectionIdColumn",
                Visible = false
            } );
            dgvViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Address",
                HeaderText = "Адрес дома",
                Name = "AddressColumn",
                MinimumWidth = 150,
                FillWeight = 20
            } );
            dgvViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionDate",
                HeaderText = "Дата проверки",
                Name = "InspectionDateColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" },
                Width = 120,
                FillWeight = 15
            } );
            dgvViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectorName",
                HeaderText = "Инспектор",
                Name = "InspectorNameColumn",
                Width = 150,
                FillWeight = 15
            } );
            dgvViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ViolationType",
                HeaderText = "Тип нарушения",
                Name = "ViolationTypeColumn",
                Width = 150,
                FillWeight = 20
            } );
            dgvViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Описание",
                Name = "DescriptionColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet,
                MinimumWidth = 200,
                FillWeight = 25
            } );
            dgvViolations.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Deadline",
                HeaderText = "Крайний срок",
                Name = "DeadlineColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" },
                Width = 120,
                FillWeight = 15
            } );
            dgvViolations.Columns.Add ( new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsFixed",
                HeaderText = "Исправлено",
                Name = "IsFixedColumn",
                Width = 80,
                FillWeight = 10
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
            cmbSort.Items.Add ( new SortOption ( "Статус (Исправлено/Нет)", "v.IsFixed ASC" ) );
            cmbSort.DisplayMember = "DisplayName";
            cmbSort.ValueMember = "SqlOrderBy";

            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
        }

        private void LoadViolations ( )
        {
            string baseQuery = @"SELECT 
                                    v.ViolationId, 
                                    v.InspectionId, 
                                    b.Address, 
                                    i.InspectionDate, 
                                    i.InspectorName, 
                                    v.ViolationType, 
                                    v.Description, 
                                    v.Deadline, 
                                    v.IsFixed
                                FROM Violations v
                                JOIN Inspections i ON v.InspectionId = i.InspectionId
                                JOIN Buildings b ON i.BuildingId = b.BuildingId";

            List<string> conditions = new List<string> ();
            List<SqlParameter> parameters = new List<SqlParameter> ();

            if ( _filterInspectionId.HasValue )
            {
                conditions.Add ( "v.InspectionId = @InspectionId" );
                parameters.Add ( new SqlParameter ( "@InspectionId", _filterInspectionId.Value ) );
            }

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                string searchTerm = $"%{txtSearch.Text.Trim ()}%";
                conditions.Add ( "(b.Address LIKE @searchTerm OR i.InspectorName LIKE @searchTerm OR v.ViolationType LIKE @searchTerm OR v.Description LIKE @searchTerm)" );
                parameters.Add ( new SqlParameter ( "@searchTerm", searchTerm ) );
            }

            string finalQuery = baseQuery;
            if ( conditions.Any () )
            {
                finalQuery += " WHERE " + string.Join ( " AND ", conditions );
            }

            if ( cmbSort.SelectedItem != null && cmbSort.SelectedItem is SortOption selectedSort )
            {
                if ( !string.IsNullOrEmpty ( selectedSort.SqlOrderBy ) )
                {
                    finalQuery += " ORDER BY " + selectedSort.SqlOrderBy;
                }
                else
                {
                    finalQuery += " ORDER BY v.ViolationId DESC";
                }
            }
            else
            {
                finalQuery += " ORDER BY v.ViolationId DESC";
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
                        violationsTable = new DataTable ();
                        adapter.Fill ( violationsTable );
                        dgvViolations.DataSource = violationsTable;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки нарушений: {ex.Message}\n\nSQL Запрос:\n{finalQuery}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void txtSearch_TextChanged ( object sender, EventArgs e )
        {
            LoadViolations ();
        }

        private void cmbSort_SelectedIndexChanged ( object sender, EventArgs e )
        {
            LoadViolations ();
        }

        private void btnSbros_Click ( object sender, EventArgs e )
        {
            txtSearch.Clear ();
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;

            LoadViolations ();
        }

        private void btnAdd_Click ( object sender, EventArgs e )
        {
            using ( ViolationDetailForm detailForm = new ViolationDetailForm ( connectionString, _filterInspectionId ) )
            {
                if ( detailForm.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadViolations ();
                }
            }
        }

        private void btnEdit_Click ( object sender, EventArgs e )
        {
            if ( dgvViolations.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите нарушение для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedViolationId = Convert.ToInt32 ( dgvViolations.SelectedRows [ 0 ].Cells [ "ViolationIdColumn" ].Value );

            using ( ViolationDetailForm detailForm = new ViolationDetailForm ( connectionString, selectedViolationId ) )
            {
                if ( detailForm.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadViolations ();
                }
            }
        }

        private void btnDelete_Click ( object sender, EventArgs e )
        {
            if ( dgvViolations.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите нарушение для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedViolationId = Convert.ToInt32 ( dgvViolations.SelectedRows [ 0 ].Cells [ "ViolationIdColumn" ].Value );
            string violationInfo = $"нарушение типа '{dgvViolations.SelectedRows [ 0 ].Cells [ "ViolationTypeColumn" ].Value}' для дома '{dgvViolations.SelectedRows [ 0 ].Cells [ "AddressColumn" ].Value}'";

            if ( MessageBox.Show ( $"Вы уверены, что хотите удалить {violationInfo}?",
                                "Подтверждение удаления",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question ) == DialogResult.Yes )
            {
                string deleteQuery = "DELETE FROM Violations WHERE ViolationId = @ViolationId";
                try
                {
                    using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                    {
                        conn.Open ();
                        using ( SqlCommand cmd = new SqlCommand ( deleteQuery, conn ) )
                        {
                            cmd.Parameters.AddWithValue ( "@ViolationId", selectedViolationId );
                            cmd.ExecuteNonQuery ();
                        }
                    }
                    LoadViolations ();
                    int inspectionIdBeforeDelete = Convert.ToInt32 ( dgvViolations.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );
                    UpdateInspectionViolationsFoundStatus ( inspectionIdBeforeDelete );

                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Ошибка удаления нарушения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void btnMarkFixed_Click ( object sender, EventArgs e )
        {
            if ( dgvViolations.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите нарушение, чтобы отметить его как исправленное.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedViolationId = Convert.ToInt32 ( dgvViolations.SelectedRows [ 0 ].Cells [ "ViolationIdColumn" ].Value );
            bool isFixed = Convert.ToBoolean ( dgvViolations.SelectedRows [ 0 ].Cells [ "IsFixedColumn" ].Value );
            int inspectionIdForUpdate = Convert.ToInt32 ( dgvViolations.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );


            if ( isFixed )
            {
                MessageBox.Show ( "Выбранное нарушение уже отмечено как исправленное.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

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
                    LoadViolations ();
                    UpdateInspectionViolationsFoundStatus ( inspectionIdForUpdate );
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


        private void btnExit_Click ( object sender, EventArgs e )
        {
            if ( _parentForm != null )
            {
                _parentForm.Show ();
                this.Hide (); 
            }
            else
            {
                Application.Exit (); 
            }
        }

        private void btnInspDet_Click ( object sender, EventArgs e )
        {
            if ( dgvViolations.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите нарушение, чтобы посмотреть детали связанной проверки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int inspectionIdForDetails = Convert.ToInt32 ( dgvViolations.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );

            using ( InspectionDetailForm inspectionDetailForm = new InspectionDetailForm ( connectionString, inspectionIdForDetails, true ) )
            {
                inspectionDetailForm.ShowDialog ( this );
            }
        }

        private void dgvViolations_CellDoubleClick ( object sender, DataGridViewCellEventArgs e )
        {
            if ( e.RowIndex >= 0 )
            {
                btnEdit_Click ( sender, e );
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

        private void btnInspDet_Click_1 ( object sender, EventArgs e )
        {
            btnInspDet_Click ( sender, e );
        }
    }
}