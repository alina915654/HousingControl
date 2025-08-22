using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HousingControl.Forms.Add
{
    public partial class InspectionDetailForm : Form
    {
        private string connectionString;
        public int? InspectionId
        {
            get; private set;
        }
        private DataTable buildingsTable = new DataTable ();
        private DataTable inspectorsTable = new DataTable ();
        private bool isViewMode;
        public InspectionDetailForm ( string connString )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.InspectionId = null;
            this.isViewMode = false;
            this.Text = "Добавить проверку";
            txtInspectionId.Text = "Новая";
            txtInspectionId.Enabled = false;
            txtInspectionId.ReadOnly = true;
        }

        public InspectionDetailForm ( string connString, int inspectionId )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.InspectionId = inspectionId;
            this.isViewMode = false; 
            this.Text = "Редактировать проверку";
            txtInspectionId.Text = inspectionId.ToString ();
            txtInspectionId.Enabled = false;
            txtInspectionId.ReadOnly = true;
        }

        public InspectionDetailForm ( string connString, int inspectionId, bool viewMode )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.InspectionId = inspectionId;
            this.isViewMode = viewMode;
            this.Text = "Просмотр проверки";
            txtInspectionId.Text = inspectionId.ToString ();
            txtInspectionId.Enabled = false;
            txtInspectionId.ReadOnly = true;
        }

        private void InspectionDetailForm_Load ( object sender, EventArgs e )
        {
            LoadInspectors ();
            LoadBuildings ();
            LoadInspectionTypes ();

            if ( InspectionId.HasValue )
            {
                LoadInspectionData ();
            }
            else
            {
                dtpInspectionDate.Value = DateTime.Today;
                if ( cmbInspectionType.Items.Count > 0 )
                    cmbInspectionType.SelectedIndex = 0;
                chkViolationsFound.Checked = false;
                if ( cmbInspector.Items.Count > 0 )
                {
                    cmbInspector.SelectedIndex = 0;
                }
            }

            if ( isViewMode )
            {
                btnSave.Visible = false;
                btnCancel.Text = "Закрыть";

                cmbInspector.Enabled = false;
                cmbBuilding.Enabled = false;
                dtpInspectionDate.Enabled = false;
                cmbInspectionType.Enabled = false;
                txtResult.ReadOnly = true;
                chkViolationsFound.Enabled = false;
            }
        }

        private void LoadInspectors ( )
        {
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    string query = "SELECT UserId, FullName FROM Users WHERE Role = 'Инспектор' ORDER BY FullName";
                    SqlDataAdapter adapter = new SqlDataAdapter ( query, conn );
                    inspectorsTable.Clear ();
                    adapter.Fill ( inspectorsTable );

                    cmbInspector.DataSource = inspectorsTable;
                    cmbInspector.DisplayMember = "FullName";
                    cmbInspector.ValueMember = "UserId";
                }
                if ( cmbInspector.Items.Count > 0 && !isViewMode )
                {
                    cmbInspector.SelectedIndex = -1;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки списка инспекторов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void LoadBuildings ( )
        {
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    string query = "SELECT BuildingId, Address FROM Buildings ORDER BY Address";
                    SqlDataAdapter adapter = new SqlDataAdapter ( query, conn );
                    buildingsTable.Clear ();
                    adapter.Fill ( buildingsTable );

                    cmbBuilding.DataSource = buildingsTable;
                    cmbBuilding.DisplayMember = "Address";
                    cmbBuilding.ValueMember = "BuildingId";
                }
                if ( cmbBuilding.Items.Count > 0 && !isViewMode )
                    cmbBuilding.SelectedIndex = -1;
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки списка домов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void LoadInspectionTypes ( )
        {
            cmbInspectionType.Items.Clear ();
            cmbInspectionType.Items.Add ( "Плановая" );
            cmbInspectionType.Items.Add ( "Внеплановая" );
            if ( cmbInspectionType.Items.Count > 0 && !isViewMode )
                cmbInspectionType.SelectedIndex = 0;
        }

        private void LoadInspectionData ( )
        {
            if ( !InspectionId.HasValue ) return;
            string query = @"SELECT 
                                i.BuildingId, 
                                i.InspectionDate, 
                                i.InspectionType, 
                                i.Result, 
                                i.ViolationsFound,
                                i.InspectorName, -- Это старое строковое поле
                                i.UserId -- НОВОЕ: Имя столбца в БД - UserId
                             FROM Inspections i
                             WHERE i.InspectionId = @InspectionId";

            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@InspectionId", InspectionId.Value );
                        using ( SqlDataReader reader = cmd.ExecuteReader () )
                        {
                            if ( reader.Read () )
                            {
                                int buildingIdOrdinal = reader.GetOrdinal ( "BuildingId" );
                                if ( !reader.IsDBNull ( buildingIdOrdinal ) && cmbBuilding.Items.Count > 0 )
                                {
                                    cmbBuilding.SelectedValue = reader.GetInt32 ( buildingIdOrdinal );
                                }
                                try
                                {
                                    int userIdOrdinal = reader.GetOrdinal ( "UserId" );
                                    if ( !reader.IsDBNull ( userIdOrdinal ) && cmbInspector.Items.Count > 0 )
                                    {
                                        cmbInspector.SelectedValue = reader.GetInt32 ( userIdOrdinal );
                                    }
                                    else
                                    {
                                        int inspectorNameOrdinal = reader.GetOrdinal ( "InspectorName" );
                                        if ( !reader.IsDBNull ( inspectorNameOrdinal ) )
                                        {
                                            string inspectorName = reader.GetString ( inspectorNameOrdinal );
                                            cmbInspector.SelectedIndex = cmbInspector.FindStringExact ( inspectorName );
                                        }
                                    }
                                }
                                catch ( IndexOutOfRangeException )
                                {
                                    int inspectorNameOrdinal = reader.GetOrdinal ( "InspectorName" );
                                    if ( !reader.IsDBNull ( inspectorNameOrdinal ) )
                                    {
                                        string inspectorName = reader.GetString ( inspectorNameOrdinal );
                                        cmbInspector.SelectedIndex = cmbInspector.FindStringExact ( inspectorName );
                                    }
                                }

                                dtpInspectionDate.Value = reader.GetDateTime ( reader.GetOrdinal ( "InspectionDate" ) );
                                cmbInspectionType.SelectedItem = reader.GetString ( reader.GetOrdinal ( "InspectionType" ) );
                                txtResult.Text = reader.IsDBNull ( reader.GetOrdinal ( "Result" ) ) ? string.Empty : reader.GetString ( reader.GetOrdinal ( "Result" ) );
                                chkViolationsFound.Checked = reader.GetBoolean ( reader.GetOrdinal ( "ViolationsFound" ) );
                            }
                            else
                            {
                                MessageBox.Show ( "Проверка с указанным ID не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                                this.Close ();
                            }
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки данных проверки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Close ();
            }
        }

        private bool ValidateInputs ( )
        {
            if ( isViewMode ) return true;

            if ( cmbInspector.SelectedValue == null )
            {
                MessageBox.Show ( "Пожалуйста, выберите инспектора.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                cmbInspector.Focus ();
                return false;
            }

            if ( cmbBuilding.SelectedValue == null )
            {
                MessageBox.Show ( "Пожалуйста, выберите дом.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                cmbBuilding.Focus ();
                return false;
            }
            if ( cmbInspectionType.SelectedItem == null )
            {
                MessageBox.Show ( "Пожалуйста, выберите тип проверки.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                cmbInspectionType.Focus ();
                return false;
            }
            return true;
        }

        private void btnSave_Click ( object sender, EventArgs e )
        {
            if ( isViewMode ) return;

            if ( !ValidateInputs () )
            {
                return;
            }

            string query;
            if ( InspectionId.HasValue )
            {
                query = @"UPDATE Inspections SET 
                            BuildingId = @BuildingId, 
                            InspectionDate = @InspectionDate, 
                            UserId = @UserId, -- Имя столбца в БД
                            InspectorName = @InspectorName, 
                            InspectionType = @InspectionType, 
                            Result = @Result, 
                            ViolationsFound = @ViolationsFound 
                          WHERE InspectionId = @InspectionId";
            }
            else
            {
                query = @"INSERT INTO Inspections (BuildingId, InspectionDate, UserId, InspectorName, InspectionType, Result, ViolationsFound) 
                          VALUES (@BuildingId, @InspectionDate, @UserId, @InspectorName, @InspectionType, @Result, @ViolationsFound)";
            }

            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@BuildingId", cmbBuilding.SelectedValue );
                        cmd.Parameters.AddWithValue ( "@InspectionDate", dtpInspectionDate.Value.Date );
                        cmd.Parameters.AddWithValue ( "@UserId", cmbInspector.SelectedValue );
                        cmd.Parameters.AddWithValue ( "@InspectorName", cmbInspector.Text.Trim () );
                        cmd.Parameters.AddWithValue ( "@InspectionType", cmbInspectionType.SelectedItem.ToString () );
                        cmd.Parameters.AddWithValue ( "@Result", string.IsNullOrWhiteSpace ( txtResult.Text ) ? ( object ) DBNull.Value : txtResult.Text.Trim () );
                        cmd.Parameters.AddWithValue ( "@ViolationsFound", chkViolationsFound.Checked );
                        if ( InspectionId.HasValue )
                        {
                            cmd.Parameters.AddWithValue ( "@InspectionId", InspectionId.Value );
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