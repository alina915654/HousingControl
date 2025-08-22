using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using HousingControl.Forms.Add; 
using HousingControl.UserControls; 

namespace HousingControl.Forms.Admin
{
    public partial class BuildingsForm : Form
    {
        private readonly string _connectionString;
        private readonly int _userId;
        private DataTable _buildingsData = new DataTable ();
        private Form _parentForm;
        private BuildingCardControl _selectedCard = null;

        public BuildingsForm ( int userId, string connectionString, Form parentForm = null )
        {
            InitializeComponent ();
            _userId = userId;
            _connectionString = connectionString;
            _parentForm = parentForm;
            InitializeFormControlsAndGrid ();
        }

        private void BuildingsForm_Load ( object sender, EventArgs e )
        {
            LoadBuildings (); 
        }

        private void InitializeFormControlsAndGrid ( )
        {
            cmbSort.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSort.Items.Clear ();
            cmbSort.Items.Add ( new SortOption ( "По адресу (А-Я)", "Address ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По адресу (Я-А)", "Address DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По году постройки (новые)", "YearBuilt DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По году постройки (старые)", "YearBuilt ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По управл. организации (А-Я)", "ManagementOrgName ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По аварийности (сначала аварийные)", "IsEmergency DESC, Address ASC" ) );
            cmbSort.DisplayMember = "DisplayName";
            cmbSort.ValueMember = "SqlOrderBy";

            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 ) cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
        }

        private void LoadBuildings ( )
        {
            try
            {
                _buildingsData.Clear ();

                using ( SqlConnection connection = new SqlConnection ( _connectionString ) )
                {
                    string query = @"SELECT b.BuildingId, b.Address, b.FloorsCount, b.ApartmentsCount, b.YearBuilt, 
                                           mo.Name AS ManagementOrgName, b.IsEmergency, b.ImagePath 
                                   FROM Buildings b
                                   LEFT JOIN ManagementOrganizations mo ON b.OrgId = mo.OrgId";

                    SqlDataAdapter adapter = new SqlDataAdapter ( query, connection );
                    adapter.Fill ( _buildingsData );
                }
                ApplySortingAndFiltering ();
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Ошибка при загрузке списка домов: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void ApplySortingAndFiltering ( )
        {
            if ( _buildingsData == null || _buildingsData.Columns.Count == 0 )
            {
                flowLayoutPanelBuildings.Controls.Clear ();
                return;
            }

            DataView dv = _buildingsData.DefaultView;
            List<string> filterParts = new List<string> ();

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                string searchText = txtSearch.Text.Trim ().Replace ( "'", "''" );
                filterParts.Add ( $"(Address LIKE '%{searchText}%' OR ManagementOrgName LIKE '%{searchText}%')" );
            }

            if ( filterParts.Any () )
            {
                dv.RowFilter = string.Join ( " AND ", filterParts );
            }
            else
            {
                dv.RowFilter = "";
            }

            string sortOrder = "Address ASC";
            if ( cmbSort.SelectedItem != null && cmbSort.SelectedItem is SortOption selectedSort )
            {
                if ( !string.IsNullOrEmpty ( selectedSort.SqlOrderBy ) )
                {
                    sortOrder = selectedSort.SqlOrderBy;
                }
            }

            try
            {
                dv.Sort = sortOrder;
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка при применении сортировки ('{sortOrder}'): {ex.Message}", "Ошибка сортировки", MessageBoxButtons.OK, MessageBoxIcon.Error );
                dv.Sort = "Address ASC";
            }

            flowLayoutPanelBuildings.Controls.Clear ();
            _selectedCard = null; 

            foreach ( DataRowView rowView in dv )
            {
                DataRow row = rowView.Row;
                int buildingId = row.Field<int> ( "BuildingId" );
                string address = row.Field<string> ( "Address" );
                string managementOrgName = row.Field<string> ( "ManagementOrgName" );
                int? yearBuilt = row.Field<int?> ( "YearBuilt" );
                int? floorsCount = row.Field<int?> ( "FloorsCount" );
                int? apartmentsCount = row.Field<int?> ( "ApartmentsCount" );
                bool isEmergency = row.Field<bool> ( "IsEmergency" );
                string imagePath = row.Field<string> ( "ImagePath" );
                string imageFileName = !string.IsNullOrEmpty ( imagePath ) ? imagePath : "";

                BuildingCardControl card = new BuildingCardControl ( _connectionString );
                card.SetBuildingData ( buildingId, address, managementOrgName, yearBuilt, floorsCount, apartmentsCount, isEmergency, imageFileName );

                card.EditClicked += Card_EditClicked;
                card.DeleteClicked += Card_DeleteClicked;
                card.Click += BuildingCard_Click;
                foreach ( Control control in card.Controls )
                {
                    control.Click += BuildingCard_Click;
                }

                flowLayoutPanelBuildings.Controls.Add ( card );
            }
        }

        private void Card_EditClicked ( object sender, BuildingEventArgs e )
        {
            EditBuildingForm frmEditBuilding = new EditBuildingForm ( _connectionString, e.BuildingId );
            if ( frmEditBuilding.ShowDialog ( this ) == DialogResult.OK )
            {
                LoadBuildings ();
            }
        }

        private void Card_DeleteClicked ( object sender, BuildingEventArgs e )
        {
            if ( MessageBox.Show ( $"Вы уверены, что хотите удалить дом с ID: {e.BuildingId}? Это действие нельзя будет отменить.",
                                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
            {
                DeleteBuilding ( e.BuildingId );
            }
        }

        private void DeleteBuilding ( int buildingId )
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection ( _connectionString ) )
                {
                    connection.Open ();

                    SqlCommand deleteViolationsCmd = new SqlCommand ( "DELETE FROM Violations WHERE InspectionId IN (SELECT InspectionId FROM Inspections WHERE BuildingId = @BuildingId)", connection );
                    deleteViolationsCmd.Parameters.AddWithValue ( "@BuildingId", buildingId );
                    deleteViolationsCmd.ExecuteNonQuery ();

                    SqlCommand deleteInspectionsCmd = new SqlCommand ( "DELETE FROM Inspections WHERE BuildingId = @BuildingId", connection );
                    deleteInspectionsCmd.Parameters.AddWithValue ( "@BuildingId", buildingId );
                    deleteInspectionsCmd.ExecuteNonQuery ();

                    SqlCommand deleteComplaintsCmd = new SqlCommand ( "DELETE FROM Complaints WHERE BuildingId = @BuildingId", connection );
                    deleteComplaintsCmd.Parameters.AddWithValue ( "@BuildingId", buildingId );
                    deleteComplaintsCmd.ExecuteNonQuery ();

                    string deleteBuildingQuery = "DELETE FROM Buildings WHERE BuildingId = @BuildingId";
                    SqlCommand deleteBuildingCmd = new SqlCommand ( deleteBuildingQuery, connection );
                    deleteBuildingCmd.Parameters.AddWithValue ( "@BuildingId", buildingId );

                    int rowsAffected = deleteBuildingCmd.ExecuteNonQuery ();
                    if ( rowsAffected > 0 )
                    {
                        LoadBuildings ();
                        MessageBox.Show ( "Дом успешно удален.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    }
                    else
                    {
                        MessageBox.Show ( "Дом не найден или уже был удален.", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    }
                }
            }
            catch ( SqlException sqlEx )
            {
                if ( sqlEx.Number == 547 )
                {
                    MessageBox.Show ( "Невозможно удалить дом, так как с ним связаны другие записи (проверки или жалобы). Сначала удалите связанные записи.", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
                else
                {
                    MessageBox.Show ( "Ошибка при удалении дома: " + sqlEx.Message, "Ошибка SQL", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( "Произошла ошибка при удалении дома: " + ex.Message, "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnAdd_Click ( object sender, EventArgs e )
        {
            EditBuildingForm frmAddBuilding = new EditBuildingForm ( _connectionString );
            if ( frmAddBuilding.ShowDialog ( this ) == DialogResult.OK )
            {
                LoadBuildings ();
            }
        }

        private void btnEdit_Click ( object sender, EventArgs e )
        {
            if ( _selectedCard != null )
            {
                Card_EditClicked ( this, new BuildingEventArgs ( _selectedCard.BuildingId ) );
            }
            else
            {
                MessageBox.Show ( "Пожалуйста, выберите дом для редактирования (кликните по карточке).", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        private void btnDelete_Click ( object sender, EventArgs e )
        {
            if ( _selectedCard != null )
            {
                Card_DeleteClicked ( this, new BuildingEventArgs ( _selectedCard.BuildingId ) );
            }
            else
            {
                MessageBox.Show ( "Пожалуйста, выберите дом для удаления (кликните по карточке).", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        private void BuildingCard_Click ( object sender, EventArgs e )
        {
            BuildingCardControl clickedCard = null;
            if ( sender is BuildingCardControl )
            {
                clickedCard = sender as BuildingCardControl;
            }
            else if ( sender is Control control && control.Parent is BuildingCardControl )
            {
                clickedCard = control.Parent as BuildingCardControl;
            }

            if ( clickedCard != null )
            {
                if ( _selectedCard != null && _selectedCard != clickedCard )
                {
                    _selectedCard.BorderStyle = BorderStyle.FixedSingle;
                }

                clickedCard.BorderStyle = BorderStyle.Fixed3D;
                _selectedCard = clickedCard;
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
            AdminMainForm adminMainFormInstance = Application.OpenForms.OfType<AdminMainForm> ().FirstOrDefault ();
            if ( adminMainFormInstance != null )
            {
                adminMainFormInstance.Show ();
            }
            else
            {
                AdminMainForm adminMainForm = new AdminMainForm ( _userId, _connectionString );
                adminMainForm.Show ();
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