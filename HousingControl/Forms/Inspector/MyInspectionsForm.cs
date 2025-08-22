using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HousingControl.Forms.Add;
using HousingControl.Forms.Admin;

namespace HousingControl.Forms.Inspector
{
    public partial class MyInspectionsForm : Form
    {
        private string connectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=HousingControl;Integrated Security=True;TrustServerCertificate=True";
        private DataTable myInspectionsTable;
        private readonly int _userId;
        private readonly string _passedConnectionString;
        private string _inspectorName;
        private readonly string _connectionString;
        public MyInspectionsForm ( int userId, string connectionString )
        {
            InitializeComponent ();
            _userId = userId;
            _connectionString = connectionString;
            this._userId = userId;
            this._passedConnectionString = connectionString;
            this.connectionString = connectionString;
            this.Text = "Мои проверки";
        }

        private void MyInspectionsForm_Load ( object sender, EventArgs e )
        {
            _inspectorName = GetInspectorNameByUserId ( _userId );
            if ( string.IsNullOrEmpty ( _inspectorName ) )
            {
                MessageBox.Show ( "Не удалось определить имя инспектора для текущего пользователя. Проверки могут быть показаны некорректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }

            SetupDataGridView ();
            PopulateSortComboBox ();
            LoadMyInspections (); 

            this.txtSearch.TextChanged += new System.EventHandler ( this.txtSearch_TextChanged );
            this.cmbSort.SelectedIndexChanged += new System.EventHandler ( this.cmbSort_SelectedIndexChanged );
            this.btnSbros.Click += new System.EventHandler ( this.btnSbros_Click );
            this.btnAdd.Click += new System.EventHandler ( this.btnAdd_Click );
            this.btnEdit.Click += new System.EventHandler ( this.btnEdit_Click );
            this.btnDelete.Click += new System.EventHandler ( this.btnDelete_Click );
            this.btnExit.Click += new System.EventHandler ( this.btnExit_Click );
            this.btnViewViolations.Click += new System.EventHandler ( this.btnViewViolations_Click );
            this.btnBrowse.Click += new System.EventHandler ( this.btnBrowse_Click );
            this.dgvMyInspections.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler ( this.dgvMyInspections_CellDoubleClick );
        }
        private void SetupDataGridView ( )
        {
            dgvMyInspections.AutoGenerateColumns = false;
            dgvMyInspections.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyInspections.MultiSelect = false;
            dgvMyInspections.ReadOnly = true;
            dgvMyInspections.AllowUserToAddRows = false;
            dgvMyInspections.AllowUserToDeleteRows = false;
            dgvMyInspections.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMyInspections.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold );

            dgvMyInspections.Columns.Clear ();
            dgvMyInspections.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionId",
                HeaderText = "ID",
                Name = "InspectionIdColumn",
                Visible = false
            } );
            dgvMyInspections.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Address",
                HeaderText = "Адрес дома",
                Name = "AddressColumn",
                MinimumWidth = 180,
                FillWeight = 25
            } );
            dgvMyInspections.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionDate",
                HeaderText = "Дата проверки",
                Name = "InspectionDateColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" },
                Width = 120,
                FillWeight = 15
            } );
            dgvMyInspections.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectorName",
                HeaderText = "Инспектор",
                Name = "InspectorNameColumn",
                MinimumWidth = 150,
                FillWeight = 20
            } );
            dgvMyInspections.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InspectionType",
                HeaderText = "Тип проверки",
                Name = "InspectionTypeColumn",
                Width = 110,
                FillWeight = 15
            } );
            dgvMyInspections.Columns.Add ( new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "ViolationsFound",
                HeaderText = "Нарушения",
                Name = "ViolationsFoundColumn",
                Width = 90,
                FillWeight = 10
            } );
            dgvMyInspections.Columns.Add ( new DataGridViewTextBoxColumn
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
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
        }

        private void LoadMyInspections ( )
        {
            if ( string.IsNullOrEmpty ( _inspectorName ) )
            {
                dgvMyInspections.DataSource = null;
                return;
            }

            string baseQuery = @"SELECT 
                                    i.InspectionId, 
                                    b.Address, 
                                    i.InspectionDate, 
                                    i.InspectorName, 
                                    i.InspectionType, 
                                    i.Result, 
                                    i.ViolationsFound
                                FROM Inspections i
                                JOIN Buildings b ON i.BuildingId = b.BuildingId
                                WHERE i.InspectorName = @InspectorName"; 

            List<string> conditions = new List<string> ();
            List<SqlParameter> parameters = new List<SqlParameter> ();
            parameters.Add ( new SqlParameter ( "@InspectorName", _inspectorName ) ); 

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                string searchTerm = $"%{txtSearch.Text.Trim ()}%";
                conditions.Add ( "(b.Address LIKE @searchTerm OR i.InspectionType LIKE @searchTerm OR i.Result LIKE @searchTerm)" );
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
                    finalQuery += " ORDER BY i.InspectionDate DESC, i.InspectionId DESC"; 
                }
            }
            else
            {
                finalQuery += " ORDER BY i.InspectionDate DESC, i.InspectionId DESC";
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
                        myInspectionsTable = new DataTable ();
                        adapter.Fill ( myInspectionsTable );
                        dgvMyInspections.DataSource = myInspectionsTable;

                        if ( myInspectionsTable.Rows.Count == 0 && !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
                        {
                            MessageBox.Show ( "Проверки не найдены по текущим критериям поиска.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки моих проверок: {ex.Message}\n\nSQL Запрос:\n{finalQuery}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private string GetInspectorNameByUserId ( int userId )
        {
            string inspectorName = string.Empty;
            string query = "SELECT FullName FROM Users WHERE UserId = @UserId AND Role = 'Инспектор'";
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@UserId", userId );
                        object result = cmd.ExecuteScalar ();
                        if ( result != null )
                        {
                            inspectorName = result.ToString ();
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка получения имени инспектора: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            return inspectorName;
        }

        private void txtSearch_TextChanged ( object sender, EventArgs e )
        {
            LoadMyInspections ();
        }

        private void btnSbros_Click ( object sender, EventArgs e )
        {
            txtSearch.Clear ();
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;

            LoadMyInspections ();
        }

        private void cmbSort_SelectedIndexChanged ( object sender, EventArgs e )
        {
            LoadMyInspections ();
        }

        private void btnAdd_Click ( object sender, EventArgs e )
        {
            using ( InspectionDetailForm detailForm = new InspectionDetailForm ( connectionString ) )
            {
                if ( detailForm.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadMyInspections ();
                }
            }
        }

        private void btnEdit_Click ( object sender, EventArgs e )
        {
            if ( dgvMyInspections.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedInspectionId = Convert.ToInt32 ( dgvMyInspections.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );

            using ( InspectionDetailForm detailForm = new InspectionDetailForm ( connectionString, selectedInspectionId ) )
            {
                if ( detailForm.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadMyInspections (); 
                }
            }
        }

        private void btnDelete_Click ( object sender, EventArgs e )
        {
            if ( dgvMyInspections.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedInspectionId = Convert.ToInt32 ( dgvMyInspections.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );
            string inspectionInfo = $"проверку для дома '{dgvMyInspections.SelectedRows [ 0 ].Cells [ "AddressColumn" ].Value}' от {( ( DateTime ) dgvMyInspections.SelectedRows [ 0 ].Cells [ "InspectionDateColumn" ].Value ):dd.MM.yyyy}";

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
                        hasViolations = ( int ) cmd.ExecuteScalar () > 0;
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
                    LoadMyInspections (); 
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Ошибка удаления проверки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void btnExit_Click ( object sender, EventArgs e )
        {
            InspectorMainForm inspectorMainForm = new InspectorMainForm ( _userId, _passedConnectionString );
            inspectorMainForm.Show ();
            this.Hide ();
        }

        private void btnViewViolations_Click ( object sender, EventArgs e )
        {
            if ( dgvMyInspections.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку, чтобы просмотреть ее нарушения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int inspectionId = Convert.ToInt32 ( dgvMyInspections.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );

            using ( var form = new AllViolationsForm ( inspectionId, connectionString, _userId ) )
            {
                form.ShowDialog ( this );
            }
        }

        private void btnBrowse_Click ( object sender, EventArgs e )
        {
            if ( dgvMyInspections.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку для просмотра.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int inspectionId = Convert.ToInt32 ( dgvMyInspections.SelectedRows [ 0 ].Cells [ "InspectionIdColumn" ].Value );            // Открываем форму деталей, передавая ID и строку подключения
            using ( var form = new InspectionDetailForm ( connectionString, inspectionId ) )
            {
                form.ShowDialog ( this );
            }
        }

        private void dgvMyInspections_CellDoubleClick ( object sender, DataGridViewCellEventArgs e )
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
