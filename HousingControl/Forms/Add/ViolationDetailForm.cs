using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HousingControl.Forms.Add
{
    public partial class ViolationDetailForm : Form
    {
        private string connectionString;
        public int? ViolationId
        {
            get; private set;
        } 
        private DataTable inspectionsTable = new DataTable ();
        private int? _preselectedInspectionId;
        int selectedViolationId;
        public ViolationDetailForm ( string connString, int? preselectedInspectionId = null )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.ViolationId = null;
            this._preselectedInspectionId = preselectedInspectionId;
            this.Text = "Добавить нарушение";
            txtViolationId.Text = "Новое";
            txtViolationId.Enabled = false;
            txtViolationId.ReadOnly = true;
        }

        public ViolationDetailForm ( string connString, int violationId )
        {
            InitializeComponent ();
            this.connectionString = connString;
            this.ViolationId = violationId;
            this.Text = "Редактировать нарушение";
            txtViolationId.Text = violationId.ToString ();
            txtViolationId.Enabled = false;
            txtViolationId.ReadOnly = true;
        }

        private void ViolationDetailForm_Load ( object sender, EventArgs e )
        {
            this.violationsTableAdapter.Fill ( this.housingControlDataSet.Violations );
            LoadInspectionsForCombo (); 

            if ( ViolationId.HasValue ) 
            {
                LoadViolationData (); 
            }
            else
            {
                dtpDeadline.Value = DateTime.Today.AddDays ( 30 );
                chkIsFixed.Checked = false;
                if ( _preselectedInspectionId.HasValue )
                {
                    cmbInspection.SelectedValue = _preselectedInspectionId.Value;
                }
                else if ( cmbInspection.Items.Count > 0 )
                {
                    cmbInspection.SelectedIndex = -1;
                }
            }
        }
        private void LoadInspectionsForCombo ( )
        {
            string query = @"SELECT 
                                i.InspectionId, 
                                b.Address + ' (' + CONVERT(NVARCHAR, i.InspectionDate, 104) + ' - ' + i.InspectorName + ')' AS DisplayText
                             FROM Inspections i
                             JOIN Buildings b ON i.BuildingId = b.BuildingId
                             ORDER BY b.Address, i.InspectionDate DESC";
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    SqlDataAdapter adapter = new SqlDataAdapter ( query, conn );
                    inspectionsTable.Clear ();
                    adapter.Fill ( inspectionsTable );

                    cmbInspection.DataSource = inspectionsTable;
                    cmbInspection.DisplayMember = "DisplayText"; 
                    cmbInspection.ValueMember = "InspectionId";  
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки списка проверок: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Close ();
            }
        }

        private void LoadViolationData ( )
        {
            if ( !ViolationId.HasValue ) return;

            string query = "SELECT InspectionId, ViolationType, Description, Deadline, IsFixed FROM Violations WHERE ViolationId = @ViolationId";
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@ViolationId", ViolationId.Value );
                        using ( SqlDataReader reader = cmd.ExecuteReader () )
                        {
                            if ( reader.Read () )
                            {
                                int inspectionIdOrdinal = reader.GetOrdinal ( "InspectionId" );
                                if ( !reader.IsDBNull ( inspectionIdOrdinal ) )
                                {
                                    cmbInspection.SelectedValue = reader.GetInt32 ( inspectionIdOrdinal );
                                }

                                txtViolationType.Text = reader.GetString ( reader.GetOrdinal ( "ViolationType" ) );
                                txtDescription.Text = reader.GetString ( reader.GetOrdinal ( "Description" ) );
                                dtpDeadline.Value = reader.GetDateTime ( reader.GetOrdinal ( "Deadline" ) );
                                chkIsFixed.Checked = reader.GetBoolean ( reader.GetOrdinal ( "IsFixed" ) );
                            }
                            else
                            {
                                MessageBox.Show ( "Нарушение с указанным ID не найдено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                                this.Close ();
                            }
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка загрузки данных нарушения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Close ();
            }
        }

        private bool ValidateInputs ( )
        {
            if ( cmbInspection.SelectedValue == null )
            {
                MessageBox.Show ( "Пожалуйста, выберите проверку, к которой относится нарушение.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                cmbInspection.Focus ();
                return false;
            }
            if ( string.IsNullOrWhiteSpace ( txtViolationType.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, укажите тип нарушения.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtViolationType.Focus ();
                return false;
            }
            if ( string.IsNullOrWhiteSpace ( txtDescription.Text ) )
            {
                MessageBox.Show ( "Пожалуйста, введите описание нарушения.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtDescription.Focus ();
                return false;
            }
            if ( dtpDeadline.Value < DateTime.Today && !chkIsFixed.Checked )
            {
                MessageBox.Show ( "Крайний срок уже прошел. Отметьте нарушение как исправленное или выберите будущую дату.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                dtpDeadline.Focus ();
                return false;
            }
            return true;
        }

        private void violationsBindingNavigatorSaveItem_Click ( object sender, EventArgs e )
        {
            this.Validate ();
            this.violationsBindingSource.EndEdit ();
            this.tableAdapterManager.UpdateAll ( this.housingControlDataSet );

        }

        private void btnSave_Click ( object sender, EventArgs e )
        {
            if ( !ValidateInputs () )
            {
                return;
            }

            string query;
            if ( ViolationId.HasValue )
            {
                query = @"UPDATE Violations SET 
                            InspectionId = @InspectionId, 
                            ViolationType = @ViolationType, 
                            Description = @Description, 
                            Deadline = @Deadline, 
                            IsFixed = @IsFixed 
                          WHERE ViolationId = @ViolationId";
            }
            else
            {
                query = @"INSERT INTO Violations (InspectionId, ViolationType, Description, Deadline, IsFixed) 
                          VALUES (@InspectionId, @ViolationType, @Description, @Deadline, @IsFixed)";
            }

            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    using ( SqlCommand cmd = new SqlCommand ( query, conn ) )
                    {
                        cmd.Parameters.AddWithValue ( "@InspectionId", cmbInspection.SelectedValue );
                        cmd.Parameters.AddWithValue ( "@ViolationType", txtViolationType.Text.Trim () );
                        cmd.Parameters.AddWithValue ( "@Description", txtDescription.Text.Trim () );
                        cmd.Parameters.AddWithValue ( "@Deadline", dtpDeadline.Value.Date );
                        cmd.Parameters.AddWithValue ( "@IsFixed", chkIsFixed.Checked );

                        if ( ViolationId.HasValue )
                        {
                            cmd.Parameters.AddWithValue ( "@ViolationId", ViolationId.Value );
                        }

                        cmd.ExecuteNonQuery ();

                        if ( !ViolationId.HasValue || ( ViolationId.HasValue && !chkIsFixed.Checked ) )
                        {
                            UpdateInspectionViolationsFoundStatus ( ( int ) cmbInspection.SelectedValue );
                        }
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
        private void UpdateInspectionViolationsFoundStatus ( int inspectionId )
        {
            try
            {
                using ( SqlConnection conn = new SqlConnection ( connectionString ) )
                {
                    conn.Open ();
                    SqlCommand checkCmd = new SqlCommand ( "SELECT COUNT(*) FROM Violations WHERE InspectionId = @InspectionId AND IsFixed = 0", conn );
                    checkCmd.Parameters.AddWithValue ( "@InspectionId", inspectionId );
                    bool violationsFound = ( int ) checkCmd.ExecuteScalar () > 0;

                    SqlCommand updateCmd = new SqlCommand ( "UPDATE Inspections SET ViolationsFound = @ViolationsFound WHERE InspectionId = @InspectionId", conn );
                    updateCmd.Parameters.AddWithValue ( "@ViolationsFound", violationsFound );
                    updateCmd.Parameters.AddWithValue ( "@InspectionId", inspectionId );
                    updateCmd.ExecuteNonQuery ();
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка обновления статуса нарушений в проверке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnCancel_Click ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close ();
        }
    }
    public class InspectionComboBoxItem
    {
        public int InspectionId
        {
            get; set;
        }
        public string DisplayText
        {
            get; set;
        }
        public override string ToString ( ) => DisplayText;
    }
}
