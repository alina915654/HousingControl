using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HousingControl.Forms.Add
{
    public partial class UserDetailForm : Form
    {
        private string connectionString;
        public int? UserId
        {
            get; private set;
        }
        public UserDetailForm ( string connString )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.UserId = null;
            this.Text = "Добавить пользователя";
            txtUserId.Text = "Новый";
            txtUserId.Enabled = false;
            txtUserId.ReadOnly = true;
        }
        public UserDetailForm ( string connString, int userId )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.UserId = userId;
            this.Text = "Редактировать пользователя";
            txtUserId.Text = userId.ToString ();
            txtUserId.Enabled = false;
            txtUserId.ReadOnly = true;
        }
        private void UserDetailForm_Load ( object sender, EventArgs e )
        {
            this.usersTableAdapter.Fill ( this.housingControlDataSet.Users );
            PopulateRoleComboBox (); 

            if ( UserId.HasValue )
            {
                LoadUserData (); 
            }
            else
            {
                if ( cmbRole.Items.Count > 0 )
                {
                    cmbRole.SelectedIndex = 0; 
                }
            }
        }
        private void PopulateRoleComboBox ( )
        {
            cmbRole.Items.Clear ();
            cmbRole.Items.Add ( "Администратор" );
            cmbRole.Items.Add ( "Инспектор" );
            if ( cmbRole.Items.Count > 0 )
            {
                cmbRole.SelectedIndex = 0;
            }
        }

        private void LoadUserData ( )
        {
            if ( !UserId.HasValue ) return;

            string query = "SELECT Username, Password, FullName, Role FROM Users WHERE UserId = @UserId";
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@UserId", UserId.Value );
                        using ( SqlDataReader reader = cmd.ExecuteReader () )
                        {
                            if ( reader.Read () )
                            {
                                txtUsername.Text = reader.GetString ( reader.GetOrdinal ( "Username" ) );
                                txtPassword.Text = reader.GetString(reader.GetOrdinal("Password")); 
                                txtFullName.Text = reader.GetString ( reader.GetOrdinal ( "FullName" ) );
                                string role = reader.GetString ( reader.GetOrdinal ( "Role" ) );
                                if ( cmbRole.Items.Contains ( role ) )
                                {
                                    cmbRole.SelectedItem = role;
                                }
                            }
                            else
                            {
                                MessageBox.Show ( "Пользователь не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                                this.Close ();
                            }
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки данных пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Close ();
            }
        }

        private bool ValidateInputs ( )
        {
            if ( string.IsNullOrWhiteSpace ( txtUsername.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, введите логин.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtUsername.Focus ();
                return false;
            }
            if ( string.IsNullOrWhiteSpace ( txtFullName.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, введите ФИО.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtFullName.Focus ();
                return false;
            }
            if ( cmbRole.SelectedItem == null )
            {
                MessageBox.Show ( "Пожалуйста, выберите роль.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                cmbRole.Focus ();
                return false;
            }

            if ( !UserId.HasValue && string.IsNullOrWhiteSpace ( txtPassword.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, введите пароль для нового пользователя.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtPassword.Focus ();
                return false;
            }
            if ( IsUsernameTaken ( txtUsername.Text.Trim (), UserId ) )
            {
                MessageBox.Show ( "Этот логин уже занят. Пожалуйста, выберите другой.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtUsername.Focus ();
                return false;
            }

            return true;
        }

        private bool IsUsernameTaken ( string username, int? currentUserId )
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            if ( currentUserId.HasValue )
            {
                query += " AND UserId <> @UserId";
            }

            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@Username", username );
                        if ( currentUserId.HasValue )
                        {
                            cmd.Parameters.AddWithValue ( "@UserId", currentUserId.Value );
                        }
                        return ( int ) cmd.ExecuteScalar () > 0;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка проверки уникальности логина: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return true;
            }
        }

        private void btnSave_Click ( object sender, EventArgs e )
        {
            if ( !ValidateInputs () )
            {
                return;
            }

            string query;
            string passwordToSave = txtPassword.Text.Trim ();

            if ( UserId.HasValue )
            {
                if ( string.IsNullOrWhiteSpace ( passwordToSave ) )
                {
                    query = @"UPDATE Users SET 
                                Username = @Username, 
                                FullName = @FullName, 
                                Role = @Role 
                              WHERE UserId = @UserId";
                }
                else
                {
                    query = @"UPDATE Users SET 
                                Username = @Username, 
                                Password = @Password, 
                                FullName = @FullName, 
                                Role = @Role 
                              WHERE UserId = @UserId";
                }
            }
            else
            {
                query = @"INSERT INTO Users (Username, Password, FullName, Role) 
                          VALUES (@Username, @Password, @FullName, @Role)";
            }

            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@Username", txtUsername.Text.Trim () );
                        cmd.Parameters.AddWithValue ( "@FullName", txtFullName.Text.Trim () );
                        cmd.Parameters.AddWithValue ( "@Role", cmbRole.SelectedItem.ToString () );

                        if ( !string.IsNullOrWhiteSpace ( passwordToSave ) )
                        {
                            cmd.Parameters.AddWithValue ( "@Password", passwordToSave );
                        }
                        if ( UserId.HasValue )
                        {
                            cmd.Parameters.AddWithValue ( "@UserId", UserId.Value );
                        }

                        cmd.ExecuteNonQuery ();
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close ();
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка сохранения данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnCancel_Click ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close ();
        }
    }
}
