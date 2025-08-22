using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HousingControl.Forms.Add
{
    public partial class EditUserForm : Form
    {
        private readonly string _connectionString;
        private readonly int? _userId;
        private bool _isPasswordChanged = false;

        public EditUserForm ( string connectionString, int? userId = null )
        {
            InitializeComponent ();
            _connectionString = connectionString;
            _userId = userId;
        }

        private void usersBindingNavigatorSaveItem_Click ( object sender, EventArgs e )
        {
            this.Validate ();
            this.usersBindingSource.EndEdit ();
            this.tableAdapterManager.UpdateAll ( this.housingControlDataSet );

        }
        private void InitializeRoleComboBox ( )
        {
            cmbRole.Items.Clear ();
            cmbRole.Items.AddRange ( new string [ ] { "Инспектор", "Администратор" } );
        }

        private void LoadUserData ( )
        {
            if ( !_userId.HasValue ) return;

            try
            {
                using ( SqlConnection connection = new SqlConnection ( _connectionString ) )
                {
                    connection.Open ();
                    string query = "SELECT UserId, Username, FullName, Role FROM Users WHERE UserId = @UserId";
                    SqlCommand cmd = new SqlCommand ( query, connection );
                    cmd.Parameters.AddWithValue ( "@UserId", _userId.Value );

                    using ( SqlDataReader reader = cmd.ExecuteReader () )
                    {
                        if ( reader.Read () )
                        {
                            txtUserId.Text = reader [ "UserId" ].ToString ();
                            txtUsername.Text = reader [ "Username" ].ToString ();
                            txtFullName.Text = reader [ "FullName" ].ToString ();
                            cmbRole.SelectedItem = reader [ "Role" ].ToString ();
                        }
                        else
                        {
                            MessageBox.Show ( "Пользователь с указанным ID не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                            this.Close ();
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при загрузке данных пользователя: " + ex.Message, "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Close ();
            }
        }

        private bool ValidateInput ( )
        {
            if ( string.IsNullOrWhiteSpace ( txtUsername.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, введите имя для входа.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtUsername.Focus ();
                return false;
            }

            if ( !_userId.HasValue || ( _userId.HasValue && _isPasswordChanged ) )
            {
                if ( string.IsNullOrWhiteSpace ( txtPassword.Text ) )
                {
                    MessageBox.Show ( "Пожалуйста, введите пароль.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    txtPassword.Focus ();
                    return false;
                }
                if ( txtPassword.Text.Length < 6 ) 
                {
                    MessageBox.Show ( "Пароль должен содержать не менее 6 символов.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    txtPassword.Focus ();
                    return false;
                }
                if ( txtPassword.Text != txtConfirmPassword.Text )
                {
                    MessageBox.Show ( "Пароли не совпадают.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    txtConfirmPassword.Focus ();
                    return false;
                }
            }


            if ( string.IsNullOrWhiteSpace ( txtFullName.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, введите ФИО пользователя.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtFullName.Focus ();
                return false;
            }

            if ( cmbRole.SelectedItem == null )
            {
                MessageBox.Show ( "Пожалуйста, выберите роль пользователя.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                cmbRole.Focus ();
                return false;
            }

            try
            {
                using ( SqlConnection connection = new SqlConnection ( _connectionString ) )
                {
                    connection.Open ();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND (@UserId IS NULL OR UserId != @UserId)";
                    SqlCommand cmd = new SqlCommand ( query, connection );
                    cmd.Parameters.AddWithValue ( "@Username", txtUsername.Text.Trim () );
                    cmd.Parameters.AddWithValue ( "@UserId", _userId.HasValue ? ( object ) _userId.Value : DBNull.Value );

                    int count = ( int ) cmd.ExecuteScalar ();
                    if ( count > 0 )
                    {
                        MessageBox.Show ( "Пользователь с таким именем для входа уже существует.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                        txtUsername.Focus ();
                        return false;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при проверке имени пользователя: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false; 
            }


            return true;
        }
        private void EditUserForm_Load ( object sender, EventArgs e )
        {
            this.usersTableAdapter.Fill ( this.housingControlDataSet.Users );
            InitializeRoleComboBox ();

            if ( _userId.HasValue )
            {
                this.Text = "Редактирование пользователя";
                LoadUserData ();
            }
            else
            {
                this.Text = "Добавление нового пользователя";
                if ( cmbRole.Items.Count > 0 )
                    cmbRole.SelectedIndex = 0;
            }
            txtPassword.TextChanged += ( s, ev ) => _isPasswordChanged = true;
        }

        private void btnSave_Click ( object sender, EventArgs e )
        {
            if ( !ValidateInput () )
            {
                return;
            }

            try
            {
                using ( SqlConnection connection = new SqlConnection ( _connectionString ) )
                {
                    connection.Open ();
                    SqlCommand cmd;
                    string sql;
                    string hashedPassword = "";

                    if ( _isPasswordChanged || !_userId.HasValue )
                    {
                        hashedPassword = txtPassword.Text;
                    }


                    if ( _userId.HasValue )
                    {
                        if ( _isPasswordChanged )
                        {
                            sql = @"UPDATE Users 
                                    SET Username = @Username, Password = @Password, FullName = @FullName, Role = @Role
                                    WHERE UserId = @UserId";
                        }
                        else 
                        {
                            sql = @"UPDATE Users 
                                    SET Username = @Username, FullName = @FullName, Role = @Role
                                    WHERE UserId = @UserId";
                        }
                        cmd = new SqlCommand ( sql, connection );
                        cmd.Parameters.AddWithValue ( "@UserId", _userId.Value );
                    }
                    else
                    {
                        sql = @"INSERT INTO Users (Username, Password, FullName, Role) 
                                VALUES (@Username, @Password, @FullName, @Role)";
                        cmd = new SqlCommand ( sql, connection );
                    }

                    cmd.Parameters.AddWithValue ( "@Username", txtUsername.Text.Trim () );
                    cmd.Parameters.AddWithValue ( "@FullName", txtFullName.Text.Trim () );
                    cmd.Parameters.AddWithValue ( "@Role", cmbRole.SelectedItem.ToString () );

                    if ( _isPasswordChanged || !_userId.HasValue )
                    {
                        cmd.Parameters.AddWithValue ( "@Password", hashedPassword );
                    }

                    cmd.ExecuteNonQuery ();

                    DialogResult = DialogResult.OK;
                    Close ();
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при сохранении данных пользователя: " + ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnCancel_Click ( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close ();
        }

        private void chkShowPassword_CheckedChanged ( object sender, EventArgs e )
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            txtConfirmPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
