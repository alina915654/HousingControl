using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using HousingControl.Forms.Admin;
using HousingControl.Forms.Inspector;

namespace HousingControl.Forms.Auth
{
    public partial class LoginForm : Form
    {
        private const string ConnectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=HousingControl;Integrated Security=True;TrustServerCertificate=True";
        public LoginForm ( )
        {
            InitializeComponent ();
        }

        private void LoginForm_Load ( object sender, EventArgs e )
        {

        }

        private void btnLogin_Click ( object sender, EventArgs e )
        {
            string username = txtLogin.Text;
            string password = txtPassword.Text;

            using ( SqlConnection connection = new SqlConnection ( ConnectionString ) )
            {
                string query = "SELECT UserId, Role FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand ( query, connection );
                cmd.Parameters.AddWithValue ( "@Username", username );
                cmd.Parameters.AddWithValue ( "@Password", password );

                try
                {
                    connection.Open ();
                    SqlDataReader reader = cmd.ExecuteReader ();

                    if ( reader.Read () )
                    {
                        int userId = reader.GetInt32 ( 0 );
                        string role = reader.GetString ( 1 );

                        this.Hide ();

                        if ( role == "Администратор" )
                        {
                            AdminMainForm adminForm = new AdminMainForm ( userId, ConnectionString );
                            adminForm.Show ();
                        }
                        else
                        {
                            InspectorMainForm inspectorForm = new InspectorMainForm ( userId, ConnectionString );
                            inspectorForm.Show ();
                        }
                    }
                    else
                    {
                        MessageBox.Show ( "Неверное имя пользователя или пароль" );
                    }
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( "Ошибка подключения: " + ex.Message );
                }
            }
        }
    }
}

