using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HousingControl.Forms.Add
{
    public partial class EditComplaintForm : Form
    {
        private string connectionString;
        private int? complaintId;
        private DataTable buildingsTable = new DataTable ();
        private DataTable usersTable = new DataTable (); 
        public EditComplaintForm ( string connectionString )
        {
            InitializeComponent ();
            this.connectionString = connectionString;
            this.complaintId = null;
            InitializeFormDefaults ( null ); 
        }

        public EditComplaintForm ( string connectionString, int complaintId )
        {
            InitializeComponent ();
            this.connectionString = connectionString;
            this.complaintId = complaintId;
            InitializeFormDefaults ( null );
        }

        public EditComplaintForm ( string connectionString, int? complaintId, int currentUserIdForPreselection )
        {
            InitializeComponent ();
            this.connectionString = connectionString;
            this.complaintId = complaintId;
            InitializeFormDefaults ( currentUserIdForPreselection );
        }

        private void InitializeFormDefaults ( int? userIdToPreselect )
        {
            InitializeStatusComboBox ();
            LoadBuildings ();
            LoadUsersForAssignment ();

            if ( complaintId.HasValue )
            {
                LoadComplaintData ();
                Text = "Редактирование жалобы";
            }
            else 
            {
                Text = "Добавление новой жалобы";
                dtpComplaintDate.Value = DateTime.Now;

                if ( userIdToPreselect.HasValue && cmbAssignedToUser.Items.Count > 0 )
                {
                    DataRow [ ] rows = usersTable.Select ( $"UserId = {userIdToPreselect.Value}" );
                    if ( rows.Length > 0 )
                    {
                        cmbAssignedToUser.SelectedValue = userIdToPreselect.Value;
                    }
                    else
                    {
                        cmbAssignedToUser.SelectedIndex = 0;
                    }
                }
                else
                {
                    cmbAssignedToUser.SelectedIndex = 0;
                }
            }
        }

        private void InitializeStatusComboBox ( )
        {
            cmbStatus.Items.AddRange ( new string [ ] {
            "Зарегистрирована",
            "В работе",
            "Закрыта"} );

            if ( cmbStatus.Items.Count > 0 )
            {
                cmbStatus.SelectedIndex = 0;
            }
        }

        private void LoadBuildings ( )
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                {
                    string query = "SELECT BuildingId, Address FROM Buildings ORDER BY Address";
                    SqlDataAdapter adapter = new SqlDataAdapter ( query, connection );
                    buildingsTable.Clear ();
                    adapter.Fill ( buildingsTable );

                    cmbBuilding.DataSource = buildingsTable;
                    cmbBuilding.DisplayMember = "Address";
                    cmbBuilding.ValueMember = "BuildingId";
                }
                if ( cmbBuilding.Items.Count > 0 ) cmbBuilding.SelectedIndex = -1;
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при загрузке домов: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void LoadUsersForAssignment ( )
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                {
                    string query = "SELECT UserId, FullName FROM Users WHERE Role IN ('Инспектор', 'Администратор') ORDER BY FullName";
                    SqlDataAdapter adapter = new SqlDataAdapter ( query, connection );
                    usersTable.Clear ();
                    adapter.Fill ( usersTable );
                    DataRow unassignedRow = usersTable.NewRow ();
                    unassignedRow [ "UserId" ] = DBNull.Value;
                    unassignedRow [ "FullName" ] = "Не назначено";
                    usersTable.Rows.InsertAt ( unassignedRow, 0 );
                    cmbAssignedToUser.DataSource = usersTable;
                    cmbAssignedToUser.DisplayMember = "FullName";
                    cmbAssignedToUser.ValueMember = "UserId";
                }
                if ( cmbAssignedToUser.Items.Count > 0 ) cmbAssignedToUser.SelectedIndex = 0;
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка при загрузке списка пользователей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }


        private void LoadComplaintData ( )
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                {
                    connection.Open ();
                    string query = "SELECT BuildingId, ComplaintDate, ResidentName, ContactPhone, Description, Status, AssignedToUserId FROM Complaints WHERE ComplaintId = @Id";
                    SqlCommand cmd = new SqlCommand ( query, connection );
                    cmd.Parameters.AddWithValue ( "@Id", complaintId.Value );

                    using ( SqlDataReader reader = cmd.ExecuteReader () )
                    {
                        if ( reader.Read () )
                        {
                            int buildingIdOrdinal = reader.GetOrdinal ( "BuildingId" );
                            if ( !reader.IsDBNull ( buildingIdOrdinal ) && cmbBuilding.Items.Count > 0 )
                            {
                                cmbBuilding.SelectedValue = reader.GetInt32 ( buildingIdOrdinal );
                            }

                            txtResidentName.Text = reader [ "ResidentName" ].ToString ();
                            txtContactPhone.Text = reader [ "ContactPhone" ].ToString ();
                            dtpComplaintDate.Value = Convert.ToDateTime ( reader [ "ComplaintDate" ] );
                            txtDescription.Text = reader [ "Description" ].ToString ();

                            string status = reader [ "Status" ].ToString ();
                            if ( cmbStatus.Items.Contains ( status ) )
                            {
                                cmbStatus.SelectedItem = status;
                            }
                            else
                            {
                                cmbStatus.SelectedIndex = 0;
                            }
                            int assignedToUserOrdinal = reader.GetOrdinal ( "AssignedToUserId" );
                            if ( !reader.IsDBNull ( assignedToUserOrdinal ) && cmbAssignedToUser.Items.Count > 0 )
                            {
                                cmbAssignedToUser.SelectedValue = reader.GetInt32 ( assignedToUserOrdinal );
                            }
                            else
                            {
                                cmbAssignedToUser.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            MessageBox.Show ( "Жалоба с указанным ID не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                            this.Close ();
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при загрузке данных жалобы: " + ex.Message,
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnSave_Click ( object sender, EventArgs e )
        {
            if ( ValidateForm () )
            {
                try
                {
                    using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                    {
                        connection.Open ();
                        SqlCommand cmd;

                        if ( complaintId.HasValue )
                        {
                            cmd = new SqlCommand (
                                "UPDATE Complaints SET BuildingId = @BuildingId, ResidentName = @ResidentName, " +
                                "ContactPhone = @ContactPhone, ComplaintDate = @ComplaintDate, " +
                                "Description = @Description, Status = @Status, AssignedToUserId = @AssignedToUserId " +
                                "WHERE ComplaintId = @ComplaintId", connection );
                            cmd.Parameters.AddWithValue ( "@ComplaintId", complaintId.Value );
                        }
                        else
                        {
                            cmd = new SqlCommand (
                                "INSERT INTO Complaints (BuildingId, ResidentName, ContactPhone, " +
                                "ComplaintDate, Description, Status, AssignedToUserId) " +
                                "VALUES (@BuildingId, @ResidentName, @ContactPhone, " +
                                "@ComplaintDate, @Description, @Status, @AssignedToUserId)", connection );
                        }

                        cmd.Parameters.AddWithValue ( "@BuildingId", cmbBuilding.SelectedValue );
                        cmd.Parameters.AddWithValue ( "@ResidentName", txtResidentName.Text );
                        cmd.Parameters.AddWithValue ( "@ContactPhone", string.IsNullOrWhiteSpace ( txtContactPhone.Text ) ? ( object ) DBNull.Value : txtContactPhone.Text );
                        cmd.Parameters.AddWithValue ( "@ComplaintDate", dtpComplaintDate.Value );
                        cmd.Parameters.AddWithValue ( "@Description", txtDescription.Text );
                        string selectedStatus = cmbStatus.SelectedItem?.ToString () ?? "Зарегистрирована";
                        cmd.Parameters.AddWithValue ( "@Status", selectedStatus );
                        if ( cmbAssignedToUser.SelectedValue is DBNull )
                        {
                            cmd.Parameters.AddWithValue ( "@AssignedToUserId", DBNull.Value );
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue ( "@AssignedToUserId", cmbAssignedToUser.SelectedValue );
                        }
                        cmd.ExecuteNonQuery ();
                        DialogResult = DialogResult.OK;
                        Close ();
                    }
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( "Ошибка при сохранении жалобы: " + ex.Message,
                                  "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }
        private bool ValidateForm ( )
        {
            if ( cmbBuilding.SelectedIndex < 0 )
            {
                MessageBox.Show ( "Выберите дом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return false;
            }

            if ( string.IsNullOrWhiteSpace ( txtResidentName.Text ) )
            {
                MessageBox.Show ( "Введите ФИО жителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return false;
            }

            if ( string.IsNullOrWhiteSpace ( txtDescription.Text ) )
            {
                MessageBox.Show ( "Введите описание жалобы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return false;
            }

            return true;
        }

        private void btnCancel_Click ( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close ();
        }

        private void EditComplaintForm_Load ( object sender, EventArgs e ){}
    }
}