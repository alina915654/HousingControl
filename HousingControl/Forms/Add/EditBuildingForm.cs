using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace HousingControl.Forms.Add
{
    public partial class EditBuildingForm : Form
    {
        private string connectionString;
        private int? buildingId;
        private DataTable managementOrgsTable = new DataTable ();
        private string _currentImageRelativePath = null;
        private string _selectedFullSourceImagePath = null;

        public EditBuildingForm ( string connString )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.buildingId = null;
            this.Text = "Добавить новый дом";
            txtBuildingId.Text = "Новый";
            txtBuildingId.Enabled = false;
            txtBuildingId.ReadOnly = true;
            DisplayImage ( Properties.Resources.NoImage );
            txtImageFilePath.Text = "Фото не выбрано";
        }

        public EditBuildingForm ( string connString, int buildingId )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.buildingId = buildingId;
            this.Text = "Редактировать дом";
            txtBuildingId.Text = buildingId.ToString ();
            txtBuildingId.Enabled = false;
            txtBuildingId.ReadOnly = true;
        }

        private void EditBuildingForm_Load ( object sender, EventArgs e )
        {
            LoadManagementOrganizations (); 

            if ( buildingId.HasValue )
            {
                LoadBuildingData ();
            }
            else
            {
                if ( cmbManagementOrg.Items.Count > 0 ) cmbManagementOrg.SelectedIndex = -1;
                chkIsEmergency.Checked = false;
                DisplayImage ( Properties.Resources.NoImage );
                txtImageFilePath.Text = "Фото не выбрано";
            }
        }

        private void LoadManagementOrganizations ( )
        {
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    string query = "SELECT OrgId, Name FROM ManagementOrganizations ORDER BY Name";
                    SqlDataAdapter adapter = new SqlDataAdapter ( query, conn );
                    managementOrgsTable.Clear ();
                    adapter.Fill ( managementOrgsTable );
                    cmbManagementOrg.DataSource = managementOrgsTable;
                    cmbManagementOrg.DisplayMember = "Name";
                    cmbManagementOrg.ValueMember = "OrgId";
                }
                if ( cmbManagementOrg.Items.Count > 0 ) cmbManagementOrg.SelectedIndex = -1;
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки списка управляющих организаций: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void LoadBuildingData ( )
        {
            if ( !buildingId.HasValue ) return; 

            string query = "SELECT Address, FloorsCount, ApartmentsCount, YearBuilt, OrgId, IsEmergency, ImagePath FROM Buildings WHERE BuildingId = @BuildingId";
            
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@BuildingId", buildingId.Value );
                        using ( SqlDataReader reader = cmd.ExecuteReader () )
                        {
                            if ( reader.Read () )
                            {
                                txtAddress.Text = reader.GetString ( reader.GetOrdinal ( "Address" ) );
                                txtFloorsCount.Text = reader.IsDBNull ( reader.GetOrdinal ( "FloorsCount" ) ) ? "" : reader.GetInt32 ( reader.GetOrdinal ( "FloorsCount" ) ).ToString ();
                                txtApartmentsCount.Text = reader.IsDBNull ( reader.GetOrdinal ( "ApartmentsCount" ) ) ? "" : reader.GetInt32 ( reader.GetOrdinal ( "ApartmentsCount" ) ).ToString ();
                                txtYearBuilt.Text = reader.IsDBNull ( reader.GetOrdinal ( "YearBuilt" ) ) ? "" : reader.GetInt32 ( reader.GetOrdinal ( "YearBuilt" ) ).ToString ();

                                int orgIdOrdinal = reader.GetOrdinal ( "OrgId" );
                                if ( !reader.IsDBNull ( orgIdOrdinal ) )
                                {
                                    cmbManagementOrg.SelectedValue = reader.GetInt32 ( orgIdOrdinal );
                                }
                                else
                                {
                                    cmbManagementOrg.SelectedIndex = -1;
                                }

                                chkIsEmergency.Checked = reader.GetBoolean ( reader.GetOrdinal ( "IsEmergency" ) );
                                int imagePathOrdinal = reader.GetOrdinal ( "ImagePath" );

                                if ( !reader.IsDBNull ( imagePathOrdinal ) )
                                {
                                    _currentImageRelativePath = reader.GetString ( imagePathOrdinal );
                                    string fullPath = Path.Combine ( Application.StartupPath, "Imagee", _currentImageRelativePath ); // ИСПРАВЛЕНО: "Imagee"

                                    if ( File.Exists ( fullPath ) )
                                    {
                                        try
                                        {
                                            using ( var fs = new FileStream ( fullPath, FileMode.Open, FileAccess.Read ) )
                                            using ( var ms = new MemoryStream () )
                                            {
                                                fs.CopyTo ( ms );
                                                ms.Position = 0;
                                                DisplayImage ( Image.FromStream ( ms ) );
                                                txtImageFilePath.Text = Path.GetFileName ( fullPath );
                                            }
                                        }
                                        catch ( Exception )
                                        {
                                            DisplayImage ( Properties.Resources.NoImage );
                                            txtImageFilePath.Text = "Ошибка загрузки фото";
                                            _currentImageRelativePath = null;
                                        }
                                    }
                                    else
                                    {
                                        DisplayImage ( Properties.Resources.NoImage );
                                        txtImageFilePath.Text = "Фото не найдено";
                                        _currentImageRelativePath = null;
                                    }
                                }
                                else
                                {
                                    DisplayImage ( Properties.Resources.NoImage );
                                    txtImageFilePath.Text = "Фото не выбрано";
                                    _currentImageRelativePath = null;
                                }
                            }
                            else
                            {
                                MessageBox.Show ( "Дом с указанным ID не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                                this.Close ();
                            }
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки данных дома: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Close ();
            }
        }
        
        private void DisplayImage ( Image img )
        {
            if ( pbSelectedImage.Image != null )
            {
                pbSelectedImage.Image.Dispose ();
            }
            pbSelectedImage.Image = img;
        }

        private bool ValidateInputs ( )
        {
            if ( string.IsNullOrWhiteSpace ( txtAddress.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, введите адрес.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtAddress.Focus ();
                return false;
            }

            int tempInt;
            if ( !string.IsNullOrWhiteSpace ( txtFloorsCount.Text ) && !int.TryParse ( txtFloorsCount.Text, out tempInt ) )
            {
                MessageBox.Show ( "Количество этажей должно быть числом.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtFloorsCount.Focus ();
                return false;
            }
            if ( !string.IsNullOrWhiteSpace ( txtApartmentsCount.Text ) && !int.TryParse ( txtApartmentsCount.Text, out tempInt ) )
            {
                MessageBox.Show ( "Количество квартир должно быть числом.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtApartmentsCount.Focus ();
                return false;
            }
            if ( !string.IsNullOrWhiteSpace ( txtYearBuilt.Text ) && !int.TryParse ( txtYearBuilt.Text, out tempInt ) )
            {
                MessageBox.Show ( "Год постройки должен быть числом.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtYearBuilt.Focus ();
                return false;
            }
            return true;
        }

        private void btnSave_Click ( object sender, EventArgs e )
        {
            if ( !ValidateInputs () )
            {
                return;
            }

            string imagePathToSaveToDb = _currentImageRelativePath;
            if ( !string.IsNullOrEmpty ( _selectedFullSourceImagePath ) )
            {
                string destinationFolderName = "Buildings";
                string destinationFullDirectory = Path.Combine ( Application.StartupPath, "Imagee", destinationFolderName );
                if ( !Directory.Exists ( destinationFullDirectory ) )
                {
                    Directory.CreateDirectory ( destinationFullDirectory );
                }

                string sourceFileName = Path.GetFileName ( _selectedFullSourceImagePath );
                string newImageFileName;
                newImageFileName = Guid.NewGuid ().ToString () + "_" + sourceFileName;
                string destinationFullPath = Path.Combine ( destinationFullDirectory, newImageFileName );

                try
                {
                    File.Copy ( _selectedFullSourceImagePath, destinationFullPath, true ); 
                    imagePathToSaveToDb = Path.Combine ( destinationFolderName, newImageFileName ); 
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Ошибка при копировании файла изображения: {ex.Message}\nСохранение будет выполнено без нового фото.", "Ошибка сохранения фото", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    imagePathToSaveToDb = _currentImageRelativePath; 
                }
            }

            string query;
            int? returnedBuildingId = null; 
            using ( SqlConnection conn = new SqlConnection ( connectionString ) )
            {
                conn.Open ();
                SqlTransaction transaction = conn.BeginTransaction (); 

                try
                {
                    SqlCommand cmd = conn.CreateCommand ();
                    cmd.Transaction = transaction; 

                    if ( buildingId.HasValue ) 
                    {
                        query = @"UPDATE Buildings SET 
                                    Address = @Address, 
                                    FloorsCount = @FloorsCount, 
                                    ApartmentsCount = @ApartmentsCount, 
                                    YearBuilt = @YearBuilt, 
                                    OrgId = @OrgId, 
                                    IsEmergency = @IsEmergency,
                                    ImagePath = @ImagePath 
                                  WHERE BuildingId = @BuildingId";

                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue ( "@BuildingId", buildingId.Value );
                        cmd.Parameters.AddWithValue ( "@Address", txtAddress.Text.Trim () );
                        cmd.Parameters.AddWithValue ( "@FloorsCount", string.IsNullOrWhiteSpace ( txtFloorsCount.Text ) ? ( object ) DBNull.Value : int.Parse ( txtFloorsCount.Text ) );
                        cmd.Parameters.AddWithValue ( "@ApartmentsCount", string.IsNullOrWhiteSpace ( txtApartmentsCount.Text ) ? ( object ) DBNull.Value : int.Parse ( txtApartmentsCount.Text ) );
                        cmd.Parameters.AddWithValue ( "@YearBuilt", string.IsNullOrWhiteSpace ( txtYearBuilt.Text ) ? ( object ) DBNull.Value : int.Parse ( txtYearBuilt.Text ) );
                        cmd.Parameters.AddWithValue ( "@OrgId", cmbManagementOrg.SelectedValue == null ? ( object ) DBNull.Value : cmbManagementOrg.SelectedValue );
                        cmd.Parameters.AddWithValue ( "@IsEmergency", chkIsEmergency.Checked );
                        cmd.Parameters.AddWithValue ( "@ImagePath", string.IsNullOrWhiteSpace ( imagePathToSaveToDb ) ? ( object ) DBNull.Value : imagePathToSaveToDb );
                        cmd.ExecuteNonQuery ();
                    }
                    else
                    {
                        query = @"INSERT INTO Buildings (Address, FloorsCount, ApartmentsCount, YearBuilt, OrgId, IsEmergency, ImagePath) 
                                  VALUES (@Address, @FloorsCount, @ApartmentsCount, @YearBuilt, @OrgId, @IsEmergency, @ImagePath);
                                  SELECT SCOPE_IDENTITY();"; 
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue ( "@Address", txtAddress.Text.Trim () );
                        cmd.Parameters.AddWithValue ( "@FloorsCount", string.IsNullOrWhiteSpace ( txtFloorsCount.Text ) ? ( object ) DBNull.Value : int.Parse ( txtFloorsCount.Text ) );
                        cmd.Parameters.AddWithValue ( "@ApartmentsCount", string.IsNullOrWhiteSpace ( txtApartmentsCount.Text ) ? ( object ) DBNull.Value : int.Parse ( txtApartmentsCount.Text ) );
                        cmd.Parameters.AddWithValue ( "@YearBuilt", string.IsNullOrWhiteSpace ( txtYearBuilt.Text ) ? ( object ) DBNull.Value : int.Parse ( txtYearBuilt.Text ) );
                        cmd.Parameters.AddWithValue ( "@OrgId", cmbManagementOrg.SelectedValue == null ? ( object ) DBNull.Value : cmbManagementOrg.SelectedValue );
                        cmd.Parameters.AddWithValue ( "@IsEmergency", chkIsEmergency.Checked );
                        cmd.Parameters.AddWithValue ( "@ImagePath", string.IsNullOrWhiteSpace ( imagePathToSaveToDb ) ? ( object ) DBNull.Value : imagePathToSaveToDb );
                        returnedBuildingId = Convert.ToInt32 ( cmd.ExecuteScalar () );
                        buildingId = returnedBuildingId;
                    }

                    transaction.Commit ();
                    this.DialogResult = DialogResult.OK;
                    this.Close ();
                }
                catch ( Exception ex )
                {
                    transaction.Rollback ();
                    MessageBox.Show ( $"Ошибка сохранения данных дома: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        private void btnCancel_Click ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close ();
        }

        private void btnSelectImage_Click_1 ( object sender, EventArgs e )
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
            openFileDialog1.Title = "Выберите файл изображения";

            if ( openFileDialog1.ShowDialog () == DialogResult.OK )
            {
                _selectedFullSourceImagePath = openFileDialog1.FileName; 
                txtImageFilePath.Text = Path.GetFileName ( _selectedFullSourceImagePath ); 
                try
                {
                    DisplayImage ( Image.FromFile ( _selectedFullSourceImagePath ) );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Ошибка загрузки выбранного изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    DisplayImage ( Properties.Resources.NoImage ); 
                    txtImageFilePath.Text = "Ошибка";
                    _selectedFullSourceImagePath = null; 
                }
            }
        }
    }
}