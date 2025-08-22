using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HousingControl.Forms.Add
{
    public partial class EditOrganizationsForm : Form
    {
        private string connectionString;
        private int? orgId;
        private Form _parentForm;
        public EditOrganizationsForm ( string connString, Form parentForm = null )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.orgId = null;
            this.Text = "Добавить организацию";
            this._parentForm = parentForm;
        }
        public EditOrganizationsForm ( string connString, int orgId, Form parentForm = null )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.orgId = orgId;
            this.Text = "Редактировать организацию";
            this._parentForm = parentForm;
        }
        private void EditOrganizationsForm_Load ( object sender, EventArgs e )
        {
            this.managementOrganizationsTableAdapter.Fill ( this.housingControlDataSet.ManagementOrganizations );
            InitializeOrgTypeComboBox ();

            if ( orgId.HasValue )
            {
                this.Text = "Редактирование организации";
                LoadOrganizationData ();
            }
            else
            {
                this.Text = "Добавление новой организации";
                if ( cmbOrgType.Items.Count > 0 )
                    cmbOrgType.SelectedIndex = 0;
            }
        }
        private void InitializeOrgTypeComboBox ( )
        {
            cmbOrgType.Items.Clear ();
            cmbOrgType.Items.AddRange ( new string [ ] { "УК", "ТСЖ", "ЖСК" } );
        }

        private void LoadOrganizationData ( )
        {
            if ( !orgId.HasValue ) return;

            try
            {
                using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                {
                    connection.Open ();
                    string query = "SELECT * FROM ManagementOrganizations WHERE OrgId = @OrgId";
                    SqlCommand cmd = new SqlCommand ( query, connection );
                    cmd.Parameters.AddWithValue ( "@OrgId", orgId.Value );

                    using ( SqlDataReader reader = cmd.ExecuteReader () )
                    {
                        if ( reader.Read () )
                        {
                            txtOrgId.Text = reader [ "OrgId" ].ToString ();
                            txtName.Text = reader [ "Name" ].ToString ();
                            cmbOrgType.SelectedItem = reader [ "OrgType" ].ToString ();
                            txtLicenseNumber.Text = reader [ "LicenseNumber" ] != DBNull.Value ? reader [ "LicenseNumber" ].ToString () : string.Empty;
                            txtDirectorName.Text = reader [ "DirectorName" ] != DBNull.Value ? reader [ "DirectorName" ].ToString () : string.Empty;
                            txtPhone.Text = reader [ "Phone" ] != DBNull.Value ? reader [ "Phone" ].ToString () : string.Empty;
                            txtEmail.Text = reader [ "Email" ] != DBNull.Value ? reader [ "Email" ].ToString () : string.Empty;
                        }
                        else
                        {
                            MessageBox.Show ( "Организация с указанным ID не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                            this.Close ();
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при загрузке данных организации: " + ex.Message, "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Close ();
            }
        }

        private bool ValidateInput ( )
        {
            if ( string.IsNullOrWhiteSpace ( txtName.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, введите название организации.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtName.Focus ();
                return false;
            }

            if ( cmbOrgType.SelectedItem == null )
            {
                MessageBox.Show ( "Пожалуйста, выберите тип организации.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                cmbOrgType.Focus ();
                return false;
            }

            if ( !string.IsNullOrWhiteSpace ( txtEmail.Text ) )
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if ( !Regex.IsMatch ( txtEmail.Text, emailPattern ) )
                {
                    MessageBox.Show ( "Пожалуйста, введите корректный E-mail адрес.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    txtEmail.Focus ();
                    return false;
                }
            }

            return true;
        }

        private void btnSave_Click ( object sender, EventArgs e )
        {
            if ( !ValidateInput () )
            {
                return;
            }

            try
            {
                using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                {
                    connection.Open ();
                    SqlCommand cmd;
                    string sql;

                    if ( orgId.HasValue )
                    {
                        sql = @"UPDATE ManagementOrganizations 
                                SET Name = @Name, OrgType = @OrgType, LicenseNumber = @LicenseNumber, 
                                    DirectorName = @DirectorName, Phone = @Phone, Email = @Email
                                WHERE OrgId = @OrgId";
                        cmd = new SqlCommand ( sql, connection );
                        cmd.Parameters.AddWithValue ( "@OrgId", orgId.Value );
                    }
                    else 
                    {
                        sql = @"INSERT INTO ManagementOrganizations (Name, OrgType, LicenseNumber, DirectorName, Phone, Email) 
                                VALUES (@Name, @OrgType, @LicenseNumber, @DirectorName, @Phone, @Email)";
                        cmd = new SqlCommand ( sql, connection );
                    }

                    cmd.Parameters.AddWithValue ( "@Name", txtName.Text.Trim () );
                    cmd.Parameters.AddWithValue ( "@OrgType", cmbOrgType.SelectedItem.ToString () );
                    cmd.Parameters.AddWithValue ( "@LicenseNumber", string.IsNullOrWhiteSpace ( txtLicenseNumber.Text ) ? ( object ) DBNull.Value : txtLicenseNumber.Text.Trim () );
                    cmd.Parameters.AddWithValue ( "@DirectorName", string.IsNullOrWhiteSpace ( txtDirectorName.Text ) ? ( object ) DBNull.Value : txtDirectorName.Text.Trim () );
                    cmd.Parameters.AddWithValue ( "@Phone", string.IsNullOrWhiteSpace ( txtPhone.Text ) ? ( object ) DBNull.Value : txtPhone.Text.Trim () );
                    cmd.Parameters.AddWithValue ( "@Email", string.IsNullOrWhiteSpace ( txtEmail.Text ) ? ( object ) DBNull.Value : txtEmail.Text.Trim () );

                    cmd.ExecuteNonQuery ();

                    DialogResult = DialogResult.OK;
                    Close ();
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при сохранении данных организации: " + ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnCancel_Click ( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close ();
        }
    }
}
