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
    public partial class AllComplaintsForm : Form
    {
        private string connectionString;
        private DataTable complaintsTable;
        private readonly int _userId;
        private readonly string _passedConnectionString;
        private Form _parentForm;
        public AllComplaintsForm ( )
        {
            InitializeComponent ();
            this._userId = 0;
            this.connectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=HousingControl;Integrated Security=True;TrustServerCertificate=True";
            this._passedConnectionString = this.connectionString;
            this._parentForm = null;
            complaintsTable = new DataTable ();
        }

        public AllComplaintsForm ( int userId, string connString, Form parentForm = null )
        {
            InitializeComponent ();
            this._userId = userId;
            this._passedConnectionString = connString;
            this.connectionString = connString;
            this._parentForm = parentForm;
            complaintsTable = new DataTable ();
        }

        private void AllComplaintsForm_Load ( object sender, EventArgs e )
        {
            SetupDataGridView ();
            PopulateSortComboBox (); 
            LoadComplaints (); 
        }

        private void SetupDataGridView ( )
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold );

            dataGridView.Columns.Clear ();

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "ComplaintId",
                HeaderText = "ID",
                Name = "ComplaintIdColumn",
                Visible = false
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "Address",
                HeaderText = "Адрес дома",
                Name = "AddressColumn",
                Width = 150
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "ResidentName",
                HeaderText = "ФИО жителя",
                Name = "ResidentNameColumn",
                Width = 150
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "AssignedToUserId",
                HeaderText = "Назначено ID",
                Name = "AssignedToUserIdColumn",
                Visible = false
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "ComplaintDate",
                HeaderText = "Дата жалобы",
                Name = "ComplaintDateColumn",
                Width = 100
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "Description",
                HeaderText = "Описание",
                Name = "DescriptionColumn",
                Width = 200
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "Status",
                HeaderText = "Статус",
                Name = "StatusColumn",
                Width = 100
            } );
        }

        private void PopulateSortComboBox ( )
        {
            cmbSort.Items.Clear ();
            cmbSort.Items.Add ( new SortOption ( "По дате (новые)", "ComplaintDate DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По дате (старые)", "ComplaintDate ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По адресу (А-Я)", "Address ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По адресу (Я-А)", "Address DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По статусу", "Status ASC" ) );
            cmbSort.DisplayMember = "DisplayName";
            cmbSort.ValueMember = "SqlOrderBy";
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 ) cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
        }

        private void LoadComplaints ( )
        {
            try
            {
                complaintsTable.Clear (); 

                using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                {
                    string query = @"SELECT c.ComplaintId, b.Address, c.ResidentName, 
                           c.ComplaintDate, c.Description, c.Status, c.AssignedToUserId
                           FROM Complaints c
                           JOIN Buildings b ON c.BuildingId = b.BuildingId";

                    SqlDataAdapter adapter = new SqlDataAdapter ( query, connection );
                    adapter.Fill ( complaintsTable ); 
                }

                ApplySortingAndFiltering ();
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при загрузке жалоб: " + ex.Message,
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void ApplySortingAndFiltering ( )
        {
            if ( complaintsTable == null || complaintsTable.Columns.Count == 0 )
            {
                dataGridView.DataSource = null;
                return;
            }

            DataView dv = complaintsTable.DefaultView;
            List<string> filterParts = new List<string> ();

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                string searchText = txtSearch.Text.Trim ().Replace ( "'", "''" );
                filterParts.Add ( $"(Address LIKE '%{searchText}%' OR ResidentName LIKE '%{searchText}%' OR Description LIKE '%{searchText}%' OR Status LIKE '%{searchText}%')" );
            }

            if ( filterParts.Any () )
            {
                dv.RowFilter = string.Join ( " AND ", filterParts );
            }
            else
            {
                dv.RowFilter = "";
            }

            string sortOrder = "ComplaintDate DESC";
            if ( cmbSort.SelectedItem != null && cmbSort.SelectedItem is SortOption selectedSort )
            {
                if ( !string.IsNullOrEmpty ( selectedSort.SqlOrderBy ) )
                {
                    sortOrder = selectedSort.SqlOrderBy;
                }
            }
            dv.Sort = sortOrder;

            dataGridView.DataSource = dv;
        }

        private void btnAdd_Click ( object sender, EventArgs e )
        {
            EditComplaintForm form = new EditComplaintForm ( connectionString, _userId );
            if ( form.ShowDialog ( this ) == DialogResult.OK ) 
            {
                LoadComplaints ();
            }
        }

        private void btnEdit_Click ( object sender, EventArgs e )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Выберите жалобу для редактирования",
                               "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            try
            {
                int complaintId = Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "ComplaintIdColumn" ].Value );
                int? assignedToUserId = dataGridView.SelectedRows [ 0 ].Cells [ "AssignedToUserIdColumn" ].Value != DBNull.Value ?
                                        ( int? ) Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "AssignedToUserIdColumn" ].Value ) : null;

                EditComplaintForm form = new EditComplaintForm ( connectionString, complaintId);
                if ( form.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadComplaints ();
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка при открытии жалобы: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnDelete_Click ( object sender, EventArgs e )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Выберите жалобу для удаления", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            if ( MessageBox.Show ( "Удалить выбранную жалобу?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
            {
                int complaintId = Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "ComplaintIdColumn" ].Value );

                try
                {
                    using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                    {
                        connection.Open ();
                        SqlCommand cmd = new SqlCommand ( "DELETE FROM Complaints WHERE ComplaintId = @Id", connection );
                        cmd.Parameters.AddWithValue ( "@Id", complaintId );
                        cmd.ExecuteNonQuery ();
                    }

                    LoadComplaints ();
                    MessageBox.Show ( "Жалоба успешно удалена.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( "Ошибка при удалении жалобы: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void txtSearch_TextChanged ( object sender, EventArgs e )
        {
            ApplySortingAndFiltering ();
        }

        private void cmbSort_SelectedIndexChanged ( object sender, EventArgs e )
        {
            ApplySortingAndFiltering ();
        }

        private void btnSbros_Click ( object sender, EventArgs e )
        {
            txtSearch.Text = "";

            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 ) cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;

            ApplySortingAndFiltering ();
        }

        private void btnExit_Click ( object sender, EventArgs e )
        {
            this.Close ();
            if ( _parentForm != null )
            {
                _parentForm.Show ();
            }
            else
            {
                AdminMainForm adminMainForm = new AdminMainForm ( _userId, _passedConnectionString );
                adminMainForm.Show ();
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