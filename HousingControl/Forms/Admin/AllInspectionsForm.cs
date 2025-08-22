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
    public partial class AllInspectionsForm : Form
    {
        private string connectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=HousingControl;Integrated Security=True;TrustServerCertificate=True";
        private DataTable inspectionsTable;
        private readonly int _userId;
        private readonly string _passedConnectionString;
        private Form _parentForm;

        public AllInspectionsForm ( )
        {
            InitializeComponent ();
            this._userId = 0;
            this._passedConnectionString = this.connectionString;
            this._parentForm = null;
        }
        public AllInspectionsForm ( int userId, string connString, Form parentForm )
        {
            InitializeComponent ();
            this._userId = userId;
            this._passedConnectionString = connString;
            this.connectionString = connString;
            this._parentForm = parentForm;
        }

        private void AllInspectionsForm_Load ( object sender, EventArgs e )
        {
            SetupDataGridView ();
            PopulateSortComboBox ();
            LoadInspections ();
        }

        private void SetupDataGridView ( )
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold );

            dataGridView.Columns.Clear ();
            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionId",
                HeaderText = "ID",
                Name = "InspectionIdColumn",
                Visible = false
            } );
            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Address",
                HeaderText = "Адрес дома",
                Name = "AddressColumn",
                MinimumWidth = 180,
                FillWeight = 25
            } );
            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionDate",
                HeaderText = "Дата проверки",
                Name = "InspectionDateColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" },
                Width = 120,
                FillWeight = 15
            } );
            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectorName",
                HeaderText = "Инспектор",
                Name = "InspectorNameColumn",
                Width = 150,
                FillWeight = 20
            } );
            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionType",
                HeaderText = "Тип проверки",
                Name = "InspectionTypeColumn",
                Width = 110,
                FillWeight = 15
            } );
            dataGridView.Columns.Add ( new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "ViolationsFound",
                HeaderText = "Нарушения",
                Name = "ViolationsFoundColumn",
                Width = 90,
                FillWeight = 10
            } );
            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Result",
                HeaderText = "Результат",
                Name = "ResultColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet,
                MinimumWidth = 200,
                FillWeight = 25
            } );
        }

        private void PopulateSortComboBox ( )
        {
            cmbSort.Items.Clear ();
            cmbSort.Items.Add ( new SortOption ( "Без сортировки", "" ) );
            cmbSort.Items.Add ( new SortOption ( "Дата (новые сначала)", "i.InspectionDate DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "Дата (старые сначала)", "i.InspectionDate ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "Адрес дома (А-Я)", "b.Address ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "Инспектор (А-Я)", "i.InspectorName ASC" ) );
            cmbSort.DisplayMember = "DisplayName";
            cmbSort.ValueMember = "SqlOrderBy";
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
        }

        private void LoadInspections ( )
        {
            string baseQuery = @"SELECT 
                                    i.InspectionId, 
                                    b.Address, 
                                    i.InspectionDate, 
                                    i.InspectorName, 
                                    i.InspectionType, 
                                    i.Result, 
                                    i.ViolationsFound
                                FROM Inspections i
                                JOIN Buildings b ON i.BuildingId = b.BuildingId";

            List<string> conditions = new List<string> ();
            List<SqlParameter> parameters = new List<SqlParameter> ();

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                conditions.Add ( "(b.Address LIKE @searchTerm OR i.InspectorName LIKE @searchTerm)" );
                parameters.Add ( new SqlParameter ( "@searchTerm", $"%{txtSearch.Text.Trim ()}%" ) );
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
                    finalQuery += " ORDER BY i.InspectionId DESC";
                }
            }
            else
            {
                finalQuery += " ORDER BY i.InspectionId DESC";
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
                        inspectionsTable = new DataTable ();
                        adapter.Fill ( inspectionsTable );
                        dataGridView.DataSource = inspectionsTable;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки проверок: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void txtSearch_TextChanged ( object sender, EventArgs e )
        {
            LoadInspections ();
        }

        private void cmbSort_SelectedIndexChanged ( object sender, EventArgs e )
        {
            LoadInspections ();
        }

        private void btnSbros_Click ( object sender, EventArgs e )
        {
            txtSearch.Clear ();
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;

            LoadInspections ();
        }

        private void btnAdd_Click ( object sender, EventArgs e )
        {
            using ( InspectionDetailForm detailForm = new InspectionDetailForm ( connectionString ) )
            {
                if ( detailForm.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadInspections ();
                }
            }
        }

        private void btnEdit_Click ( object sender, EventArgs e )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedInspectionId = Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );

            using ( InspectionDetailForm detailForm = new InspectionDetailForm ( connectionString, selectedInspectionId ) )
            {
                if ( detailForm.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadInspections ();
                }
            }
        }

        private void btnDelete_Click ( object sender, EventArgs e )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedInspectionId = Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );
            string inspectionInfo = $"проверку для дома '{dataGridView.SelectedRows [ 0 ].Cells [ "AddressColumn" ].Value}' от {( ( DateTime ) dataGridView.SelectedRows [ 0 ].Cells [ "InspectionDateColumn" ].Value ):dd.MM.yyyy}";

            bool hasViolations = false;
            string checkViolationsQuery = "SELECT COUNT(*) FROM Violations WHERE InspectionId = @InspectionId";
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( checkViolationsQuery, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@InspectionId", selectedInspectionId );
                        int count = ( int ) cmd.ExecuteScalar ();
                        if ( count > 0 )
                        {
                            hasViolations = true;
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка проверки связанных нарушений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            if ( hasViolations )
            {
                MessageBox.Show ( $"Невозможно удалить {inspectionInfo}, так как для нее зарегистрированы нарушения. Сначала удалите все связанные нарушения или настройте каскадное удаление в БД.", "Удаление невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            if ( MessageBox.Show ( $"Вы уверены, что хотите удалить {inspectionInfo}?",
                                "Подтверждение удаления",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question ) == DialogResult.Yes )
            {
                string deleteQuery = "DELETE FROM Inspections WHERE InspectionId = @InspectionId";
                try
                {
                    using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                    {
                        conn.Open ();
                        using ( SqlCommand cmd = new SqlCommand ( deleteQuery, conn ) )
                        {
                            cmd.Parameters.AddWithValue ( "@InspectionId", selectedInspectionId );
                            cmd.ExecuteNonQuery ();
                        }
                    }
                    LoadInspections ();
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Ошибка удаления проверки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void btnExit_Click ( object sender, EventArgs e )
        {
            this.Close ();
            if ( _parentForm != null )
            {
                _parentForm.Show ();
            }
            else { Application.Exit (); }
        }

        private void btnBrowse_Click ( object sender, EventArgs e )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку для просмотра.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int inspectionId = Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );
            using ( var form = new InspectionDetailForm ( connectionString, inspectionId, true ) )
            {
                form.ShowDialog ( this );
            }
        }

        private void btnViewViolations_Click ( object sender, EventArgs e )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку, чтобы просмотреть ее нарушения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int inspectionId = Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );
            using ( var form = new AllViolationsForm ( inspectionId, connectionString, _parentForm ) )
            {
                this.Hide ();
                form.ShowDialog ();
            }
        }

        private void dataGridView_CellDoubleClick ( object sender, DataGridViewCellEventArgs e )
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
    }
}