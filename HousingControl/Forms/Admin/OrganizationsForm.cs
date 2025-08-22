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
    public partial class OrganizationsForm : Form
    {
        private readonly int _userId;
        private readonly string _connectionString;
        private DataTable _organizationsTable = new DataTable ();
        private Form _parentForm;

        public OrganizationsForm ( int userId, string connectionString, Form parentForm = null )
        {
            InitializeComponent ();
            _userId = userId;
            _connectionString = connectionString;
            _parentForm = parentForm;
        }

        private void OrganizationsForm_Load ( object sender, EventArgs e )
        {
            InitializeFormControlsAndGrid ();
            LoadOrganizations ();
        }

        private void InitializeFormControlsAndGrid ( )
        {
            dataGridViewOrgs.AutoGenerateColumns = false;
            dataGridViewOrgs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOrgs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrgs.MultiSelect = false;
            dataGridViewOrgs.AllowUserToAddRows = false;
            dataGridViewOrgs.ReadOnly = true;
            dataGridViewOrgs.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold );

            dataGridViewOrgs.Columns.Clear ();

            dataGridViewOrgs.Columns.Add ( new DataGridViewTextBoxColumn
            {
                Name = "OrgId",
                DataPropertyName = "OrgId",
                HeaderText = "ID",
                Visible = false,
                FillWeight = 5
            } );
            dataGridViewOrgs.Columns.Add ( new DataGridViewTextBoxColumn
            {
                Name = "NameCol",
                DataPropertyName = "Name",
                HeaderText = "Название",
                FillWeight = 25
            } );
            dataGridViewOrgs.Columns.Add ( new DataGridViewTextBoxColumn
            {
                Name = "OrgTypeCol",
                DataPropertyName = "OrgType",
                HeaderText = "Тип",
                FillWeight = 10
            } );

            dataGridViewOrgs.Columns.Add ( new DataGridViewTextBoxColumn
            {
                Name = "LicenseNumberCol",
                DataPropertyName = "LicenseNumber",
                HeaderText = "Лицензия",
                FillWeight = 15
            } );
            dataGridViewOrgs.Columns.Add ( new DataGridViewTextBoxColumn
            {
                Name = "DirectorNameCol",
                DataPropertyName = "DirectorName",
                HeaderText = "Директор",
                FillWeight = 20
            } );
            dataGridViewOrgs.Columns.Add ( new DataGridViewTextBoxColumn
            {
                Name = "PhoneCol",
                DataPropertyName = "Phone",
                HeaderText = "Телефон",
                FillWeight = 15
            } );
            dataGridViewOrgs.Columns.Add ( new DataGridViewTextBoxColumn
            {
                Name = "EmailCol",
                DataPropertyName = "Email",
                HeaderText = "E-mail",
                FillWeight = 20
            } );

            cmbSort.Items.Clear ();
            cmbSort.Items.Add ( new SortOption ( "По названию (А-Я)", "Name ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По названию (Я-А)", "Name DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По типу (А-Я)", "OrgType ASC, Name ASC" ) );
            cmbSort.DisplayMember = "DisplayName";
            cmbSort.ValueMember = "SqlOrderBy";
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 ) cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
            cmbFilterOrgType.Items.Clear ();
            cmbFilterOrgType.Items.Add ( "Все типы" );
            cmbFilterOrgType.Items.AddRange ( new string [ ] { "УК", "ТСЖ", "ЖСК" } );
            cmbFilterOrgType.SelectedIndexChanged -= cmbFilterOrgType_SelectedIndexChanged;
            if ( cmbFilterOrgType.Items.Count > 0 ) cmbFilterOrgType.SelectedIndex = 0;
            cmbFilterOrgType.SelectedIndexChanged += cmbFilterOrgType_SelectedIndexChanged;
        }

        private void LoadOrganizations ( )
        {
            try
            {
                _organizationsTable.Clear ();
                using ( SqlConnection connection = new SqlConnection ( _connectionString ) )
                {
                    string query = "SELECT OrgId, Name, OrgType, LicenseNumber, DirectorName, Phone, Email FROM ManagementOrganizations";
                    SqlDataAdapter adapter = new SqlDataAdapter ( query, connection );
                    adapter.Fill ( _organizationsTable );
                }
                ApplySortingAndFiltering ();
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при загрузке списка организаций: " + ex.Message, "Ошибка загрузки данных", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void ApplySortingAndFiltering ( )
        {
            if ( _organizationsTable == null || _organizationsTable.Columns.Count == 0 )
            {
                dataGridViewOrgs.DataSource = null;
                return;
            }

            DataView dv = _organizationsTable.DefaultView;

            List<string> filterParts = new List<string> ();

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                string searchText = txtSearch.Text.Trim ().Replace ( "'", "''" );
                filterParts.Add ( $"(Name LIKE '%{searchText}%' OR DirectorName LIKE '%{searchText}%' OR Phone LIKE '%{searchText}%' OR Email LIKE '%{searchText}%')" );
            }

            if ( cmbFilterOrgType.SelectedIndex > 0 )
            {
                string selectedOrgType = cmbFilterOrgType.SelectedItem.ToString ();
                filterParts.Add ( $"OrgType = '{selectedOrgType}'" );
            }

            if ( filterParts.Any () )
            {
                dv.RowFilter = string.Join ( " AND ", filterParts );
            }
            else
            {
                dv.RowFilter = "";
            }

            string sortOrder = "Name ASC";
            if ( cmbSort.SelectedItem != null && cmbSort.SelectedItem is SortOption selectedSort )
            {
                if ( !string.IsNullOrEmpty ( selectedSort.SqlOrderBy ) )
                {
                    sortOrder = selectedSort.SqlOrderBy;
                }
            }
            dv.Sort = sortOrder;

            dataGridViewOrgs.DataSource = dv;
        }

        private void btnAdd_Click ( object sender, EventArgs e )
        {
            EditOrganizationsForm frmAddOrg = new EditOrganizationsForm ( _connectionString );
            if ( frmAddOrg.ShowDialog ( this ) == DialogResult.OK )
            {
                LoadOrganizations ();
            }
        }

        private void btnEdit_Click ( object sender, EventArgs e )
        {
            if ( dataGridViewOrgs.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Выберите организацию для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            try
            {
                int orgId = Convert.ToInt32 ( dataGridViewOrgs.SelectedRows [ 0 ].Cells [ "OrgId" ].Value );
                EditOrganizationsForm frmEditOrg = new EditOrganizationsForm ( _connectionString, orgId );
                if ( frmEditOrg.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadOrganizations ();
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка при открытии формы редактирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnDelete_Click ( object sender, EventArgs e )
        {
            if ( dataGridViewOrgs.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Выберите организацию для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            if ( MessageBox.Show ( "Вы уверены, что хотите удалить выбранную организацию?\nСвязанные с ней дома также будут откреплены (их OrgId станет NULL).",
                                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.Yes )
            {
                try
                {
                    int orgId = Convert.ToInt32 ( dataGridViewOrgs.SelectedRows [ 0 ].Cells [ "OrgId" ].Value );
                    string orgName = dataGridViewOrgs.SelectedRows [ 0 ].Cells [ "NameCol" ].Value.ToString ();

                    using ( SqlConnection connection = new SqlConnection ( _connectionString ) )
                    {
                        connection.Open ();

                        SqlCommand checkCmd = new SqlCommand ( "SELECT COUNT(*) FROM Buildings WHERE OrgId = @OrgId", connection );
                        checkCmd.Parameters.AddWithValue ( "@OrgId", orgId );
                        int dependentBuildingsCount = ( int ) checkCmd.ExecuteScalar ();

                        if ( dependentBuildingsCount > 0 )
                        {
                            if ( MessageBox.Show ( $"Организация '{orgName}' управляет {dependentBuildingsCount} домом(ами). " +
                                                "При удалении организации, эти дома потеряют связь с УК (их OrgId станет NULL). " +
                                                "Вы уверены, что хотите продолжить?",
                                                "Предупреждение о зависимостях", MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.No )
                            {
                                return;
                            }
                        }

                        SqlCommand updateBuildingsCmd = new SqlCommand ( "UPDATE Buildings SET OrgId = NULL WHERE OrgId = @OrgId", connection );
                        updateBuildingsCmd.Parameters.AddWithValue ( "@OrgId", orgId );
                        updateBuildingsCmd.ExecuteNonQuery ();

                        SqlCommand deleteCmd = new SqlCommand ( "DELETE FROM ManagementOrganizations WHERE OrgId = @OrgId", connection );
                        deleteCmd.Parameters.AddWithValue ( "@OrgId", orgId );
                        int rowsAffected = deleteCmd.ExecuteNonQuery ();

                        if ( rowsAffected > 0 )
                        {
                            LoadOrganizations ();
                            MessageBox.Show ( "Организация успешно удалена.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        }
                        else
                        {
                            MessageBox.Show ( "Организация не найдена.", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error );
                        }
                    }
                }
                catch ( SqlException sqlEx )
                {
                    MessageBox.Show ( $"Ошибка SQL при удалении организации: {sqlEx.Message}", "Ошибка SQL", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Произошла ошибка при удалении организации: {ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void cmbFilterOrgType_SelectedIndexChanged ( object sender, EventArgs e )
        {
            ApplySortingAndFiltering ();
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

            cmbFilterOrgType.SelectedIndexChanged -= cmbFilterOrgType_SelectedIndexChanged;
            if ( cmbFilterOrgType.Items.Count > 0 ) cmbFilterOrgType.SelectedIndex = 0;
            cmbFilterOrgType.SelectedIndexChanged += cmbFilterOrgType_SelectedIndexChanged;

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
                AdminMainForm adminMainFormInstance = Application.OpenForms.OfType<AdminMainForm> ().FirstOrDefault ();

                if ( adminMainFormInstance != null )
                {
                    adminMainFormInstance.Show ();
                }
                else
                {
                    AdminMainForm adminMainForm = new AdminMainForm ( _userId, _connectionString);
                    adminMainForm.Show ();
                }
            }
        }

        private void dataGridViewOrgs_CellDoubleClick ( object sender, DataGridViewCellEventArgs e )
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

        private void cmbSort_SelectedIndexChanged_1 ( object sender, EventArgs e )
        {
            ApplySortingAndFiltering ();
        }

        private void txtSearch_TextChanged_1 ( object sender, EventArgs e )
        {
            ApplySortingAndFiltering ();
        }
    }
}