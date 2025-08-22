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
    public partial class UsersForm : Form
    {
        private string connectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=HousingControl;Integrated Security=True;TrustServerCertificate=True";
        private DataTable usersTable = new DataTable ();
        private readonly int _loggedInUserId;
        private readonly string _passedConnectionString;

        public UsersForm ( )
        {
            InitializeComponent ();
            this._loggedInUserId = 0;
            this._passedConnectionString = this.connectionString;
        }

        public UsersForm ( int loggedInUserId, string connString )
        {
            InitializeComponent ();
            this._loggedInUserId = loggedInUserId;
            this._passedConnectionString = connString;
            this.connectionString = connString;
            this.Text = "Управление пользователями";
        }

        private void UsersForm_Load ( object sender, EventArgs e )
        {
            SetupDataGridView ();
            PopulateRoleFilterComboBox ();
            PopulateSortComboBox ();
            LoadUsers (); 
        }

        private void SetupDataGridView ( )
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold );

            dgvUsers.Columns.Clear ();
            dgvUsers.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserId",
                HeaderText = "ID",
                Name = "UserIdColumn",
                Visible = false
            } );
            dgvUsers.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Логин",
                Name = "UsernameColumn",
                MinimumWidth = 120,
                FillWeight = 30
            } );
            dgvUsers.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "ФИО",
                Name = "FullNameColumn",
                MinimumWidth = 200,
                FillWeight = 40
            } );
            dgvUsers.Columns.Add ( new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Роль",
                Name = "RoleColumn",
                MinimumWidth = 100,
                FillWeight = 20
            } );
        }

        private void PopulateRoleFilterComboBox ( )
        {
            cmbRoleFilter.Items.Clear ();
            cmbRoleFilter.Items.Add ( "Все" );
            cmbRoleFilter.Items.Add ( "Администратор" );
            cmbRoleFilter.Items.Add ( "Инспектор" );

            cmbRoleFilter.SelectedIndexChanged -= cmbRoleFilter_SelectedIndexChanged;
            if ( cmbRoleFilter.Items.Count > 0 )
                cmbRoleFilter.SelectedIndex = 0;
            cmbRoleFilter.SelectedIndexChanged += cmbRoleFilter_SelectedIndexChanged;
        }

        private void PopulateSortComboBox ( )
        {
            cmbSort.Items.Clear ();
            cmbSort.Items.Add ( new SortOption ( "Без сортировки", "" ) );
            cmbSort.Items.Add ( new SortOption ( "По логину (А-Я)", "Username ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По логину (Я-А)", "Username DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По ФИО (А-Я)", "FullName ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По ФИО (Я-А)", "FullName DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По роли (А-Я)", "Role ASC, FullName ASC" ) );
            cmbSort.DisplayMember = "DisplayName";
            cmbSort.ValueMember = "SqlOrderBy";

            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
        }

        private void LoadUsers ( )
        {
            string baseQuery = "SELECT UserId, Username, FullName, Role FROM Users";
            List<string> conditions = new List<string> ();
            List<SqlParameter> parameters = new List<SqlParameter> ();

            if ( cmbRoleFilter.SelectedItem != null && cmbRoleFilter.SelectedItem.ToString () != "Все" )
            {
                conditions.Add ( "Role = @Role" );
                parameters.Add ( new SqlParameter ( "@Role", cmbRoleFilter.SelectedItem.ToString () ) );
            }

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                string searchTerm = $"%{txtSearch.Text.Trim ()}%";
                conditions.Add ( "(Username LIKE @searchTerm OR FullName LIKE @searchTerm)" );
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
                    finalQuery += " ORDER BY FullName ASC"; 
                }
            }
            else
            {
                finalQuery += " ORDER BY FullName ASC";
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
                        usersTable.Clear ();
                        adapter.Fill ( usersTable );
                        dgvUsers.DataSource = usersTable;

                        if ( usersTable.Rows.Count == 0 && ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) || ( cmbRoleFilter.SelectedItem != null && cmbRoleFilter.SelectedItem.ToString () != "Все" ) ) )
                        {
                            MessageBox.Show ( "Пользователи не найдены по текущим критериям поиска/фильтрации.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки пользователей: {ex.Message}\n\nSQL Запрос:\n{finalQuery}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void txtSearch_TextChanged ( object sender, EventArgs e )
        {
            LoadUsers ();
        }

        private void cmbRoleFilter_SelectedIndexChanged ( object sender, EventArgs e )
        {
            LoadUsers ();
        }

        private void cmbSort_SelectedIndexChanged ( object sender, EventArgs e )
        {
            LoadUsers (); 
        }

        private void btnSbros_Click ( object sender, EventArgs e )
        {
            txtSearch.Clear ();

            cmbRoleFilter.SelectedIndexChanged -= cmbRoleFilter_SelectedIndexChanged;
            if ( cmbRoleFilter.Items.Count > 0 )
                cmbRoleFilter.SelectedIndex = 0;
            cmbRoleFilter.SelectedIndexChanged += cmbRoleFilter_SelectedIndexChanged;

            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;

            LoadUsers ();
        }

        private void btnAdd_Click ( object sender, EventArgs e )
        {
            using ( UserDetailForm detailForm = new UserDetailForm ( connectionString ) )
            {
                if ( detailForm.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadUsers ();
                }
            }
        }

        private void btnEdit_Click ( object sender, EventArgs e )
        {
            if ( dgvUsers.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите пользователя для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedUserId = Convert.ToInt32 ( dgvUsers.SelectedRows [ 0 ].Cells [ "UserIdColumn" ].Value );

            using ( UserDetailForm detailForm = new UserDetailForm ( connectionString, selectedUserId ) )
            {
                if ( detailForm.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadUsers ();
                }
            }
        }

        private void btnDelete_Click ( object sender, EventArgs e )
        {
            if ( dgvUsers.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Пожалуйста, выберите пользователя для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            int selectedUserId = Convert.ToInt32 ( dgvUsers.SelectedRows [ 0 ].Cells [ "UserIdColumn" ].Value );
            string username = dgvUsers.SelectedRows [ 0 ].Cells [ "UsernameColumn" ].Value.ToString ();
            string fullName = dgvUsers.SelectedRows [ 0 ].Cells [ "FullNameColumn" ].Value.ToString ();

            if ( selectedUserId == _loggedInUserId )
            {
                MessageBox.Show ( "Вы не можете удалить свою учетную запись, находясь в ней.", "Удаление невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            bool hasRelatedInspections = CheckForRelatedRecords ( "Inspections", "UserId", selectedUserId );
            bool hasRelatedComplaints = CheckForRelatedRecords ( "Complaints", "AssignedToUserId", selectedUserId );

            if ( hasRelatedInspections || hasRelatedComplaints )
            {
                string message = $"Невозможно удалить пользователя '{fullName}', так как он связан со следующими записями:\n";
                if ( hasRelatedInspections ) message += "- Проверки\n";
                if ( hasRelatedComplaints ) message += "- Жалобы\n";
                message += "Сначала удалите или переназначьте все связанные записи.";
                MessageBox.Show ( message, "Удаление невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            if ( MessageBox.Show ( $"Вы уверены, что хотите удалить пользователя '{fullName}' (логин: {username})?",
                                "Подтверждение удаления",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question ) == DialogResult.Yes )
            {
                string deleteQuery = "DELETE FROM Users WHERE UserId = @UserId";
                try
                {
                    using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                    {
                        conn.Open ();
                        using ( SqlCommand cmd = new SqlCommand ( deleteQuery, conn ) )
                        {
                            cmd.Parameters.AddWithValue ( "@UserId", selectedUserId );
                            cmd.ExecuteNonQuery ();
                        }
                    }
                    LoadUsers ();
                    MessageBox.Show ( "Пользователь успешно удален.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Ошибка удаления пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void btnExit_Click ( object sender, EventArgs e )
        {
            this.Hide (); 
            AdminMainForm adminMainFormInstance = Application.OpenForms.OfType<AdminMainForm> ().FirstOrDefault ();
            if ( adminMainFormInstance != null )
            {
                adminMainFormInstance.Show ();
            }
            else
            {
                AdminMainForm adminMainForm = new AdminMainForm ( _loggedInUserId, _passedConnectionString );
                adminMainForm.Show ();
            }
        }

        private void dgvUsers_CellDoubleClick ( object sender, DataGridViewCellEventArgs e )
        {
            if ( e.RowIndex >= 0 )
            {
                btnEdit_Click ( sender, e );
            }
        }

        private bool CheckForRelatedRecords ( string tableName, string fkColumnName, int fkValue )
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {fkColumnName} = @FkValue";
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@FkValue", fkValue );
                        return ( int ) cmd.ExecuteScalar () > 0;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка при проверке связанных записей в таблице {tableName}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return true;
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