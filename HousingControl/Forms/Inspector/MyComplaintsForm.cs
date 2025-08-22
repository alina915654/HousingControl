using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HousingControl.Forms.Add;
using HousingControl.Forms.Inspector; 

namespace HousingControl.Forms.Inspector
{
    public partial class MyComplaintsForm : Form
    {
        private string connectionString;
        private DataTable complaintsTable;
        private BindingSource bindingSource;
        private readonly int _currentUserId;
        private readonly string _passedConnectionString;
        private Form _parentForm;

        public MyComplaintsForm ( int userId, string connString, Form parentForm = null )
        {
            InitializeComponent ();
            _currentUserId = userId;
            _passedConnectionString = connString;
            this.connectionString = connString;
            _parentForm = parentForm;
            this.Text = $"Мои жалобы (Инспектор ID: {userId})";

            complaintsTable = new DataTable ();
            bindingSource = new BindingSource ();
        }

        private void MyComplaintsForm_Load ( object sender, EventArgs e )
        {
            SetupDataGridView ();
            PopulateSortComboBox ();
            LoadComplaints ();
        }

        private void SetupDataGridView ( )
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font ( "Microsoft YaHei UI", 10F, FontStyle.Bold );
            dataGridView.Columns.Clear ();

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "ComplaintId",
                HeaderText = "ID",
                Name = "ComplaintIdColumn",
                Visible = false
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "AssignedToUserId",
                HeaderText = "Назначено ID",
                Name = "AssignedToUserIdColumn",
                Visible = false
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "Address",
                HeaderText = "Адрес дома",
                Name = "AddressColumn",
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "ResidentName",
                HeaderText = "ФИО жителя",
                Name = "ResidentNameColumn",
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "ComplaintDate",
                HeaderText = "Дата жалобы",
                Name = "ComplaintDateColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" },
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "Description",
                HeaderText = "Описание",
                Name = "DescriptionColumn",
            } );

            dataGridView.Columns.Add ( new DataGridViewTextBoxColumn ()
            {
                DataPropertyName = "Status",
                HeaderText = "Статус",
                Name = "StatusColumn",
            } );

            bindingSource.DataSource = complaintsTable;
            dataGridView.DataSource = bindingSource;
        }

        private void PopulateSortComboBox ( )
        {
            cmbSort.Items.Clear ();
            cmbSort.Items.Add ( new SortOption ( "По дате (новые)", "ComplaintDate DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По дате (старые)", "ComplaintDate ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По адресу (А-Я)", "Address ASC" ) );
            cmbSort.Items.Add ( new SortOption ( "По адресу (Я-А)", "Address DESC" ) );
            cmbSort.Items.Add ( new SortOption ( "По статусу", "Status ASC" ) );

            cmbSort.DisplayMember = "DisplayName";
            cmbSort.ValueMember = "SqlOrderBy";

            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            if ( cmbSort.Items.Count > 0 )
                cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
        }

        private void LoadComplaints ( )
        {
            if ( _currentUserId <= 0 )
            {
                MessageBox.Show ( "Пользователь не авторизован или ID пользователя некорректен.", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                complaintsTable.Clear ();
                bindingSource.DataSource = complaintsTable.Clone ();
                return;
            }
            try
            {
                using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                {
                    string query = @"SELECT c.ComplaintId, b.Address, c.ResidentName, 
                               c.ComplaintDate, c.Description, c.Status, c.AssignedToUserId
                               FROM Complaints c
                               JOIN Buildings b ON c.BuildingId = b.BuildingId
                               WHERE c.AssignedToUserId = @CurrentUserId";

                    SqlDataAdapter adapter = new SqlDataAdapter ( query, connection );
                    adapter.SelectCommand.Parameters.AddWithValue ( "@CurrentUserId", _currentUserId );

                    complaintsTable.Clear ();
                    adapter.Fill ( complaintsTable );
                }

                ApplySortingAndFiltering ();

                if ( complaintsTable.Rows.Count == 0 && string.IsNullOrWhiteSpace ( txtSearch.Text ) )
                {
                    MessageBox.Show ( "У вас нет назначенных жалоб.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
                else if ( complaintsTable.Rows.Count == 0 && !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
                {
                    MessageBox.Show ( "Жалобы не найдены по текущим критериям поиска.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }

            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка при загрузке жалоб: {ex.Message}\n\nПроверьте подключение к БД и наличие колонки AssignedToUserId в таблице Complaints.",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void ApplySortingAndFiltering ( )
        {
            if ( complaintsTable == null || complaintsTable.Rows.Count == 0 )
            {
                bindingSource.DataSource = complaintsTable != null ? complaintsTable.Clone () : new DataTable ();
                return;
            }

            DataView dv = new DataView ( complaintsTable );
            List<string> filterParts = new List<string> ();

            if ( !string.IsNullOrWhiteSpace ( txtSearch.Text ) )
            {
                string searchText = txtSearch.Text.Trim ().Replace ( "'", "''" );
                filterParts.Add ( $"(Address LIKE '%{searchText}%' OR ResidentName LIKE '%{searchText}%' OR Description LIKE '%{searchText}%' OR Status LIKE '%{searchText}%')" );
            }

            if ( filterParts.Any () )
            {
                dv.RowFilter = string.Join ( " AND ", filterParts );
            }
            else
            {
                dv.RowFilter = "";
            }

            string sortOrder = "ComplaintDate DESC";
            if ( cmbSort.SelectedItem is SortOption selectedSort )
            {
                if ( !string.IsNullOrEmpty ( selectedSort.SqlOrderBy ) )
                {
                    sortOrder = selectedSort.SqlOrderBy;
                }
            }
            else
            {
                switch ( cmbSort.SelectedIndex )
                {
                    case 0: sortOrder = "ComplaintDate DESC"; break;
                    case 1: sortOrder = "ComplaintDate ASC"; break;
                    case 2: sortOrder = "Address ASC"; break;
                    case 3: sortOrder = "Address DESC"; break;
                    case 4: sortOrder = "Status ASC"; break;
                }
            }
            dv.Sort = sortOrder;

            bindingSource.DataSource = dv;
        }

        private void btnAdd_Click ( object sender, EventArgs e )
        {
            using ( var form = new EditComplaintForm ( connectionString, null, _currentUserId ) )
            {
                if ( form.ShowDialog ( this ) == DialogResult.OK )
                {
                    LoadComplaints ();
                }
            }
        }

        private void btnEdit_Click ( object sender, EventArgs e )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Выберите жалобу для редактирования",
                               "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            try
            {
                var selectedRow = dataGridView.SelectedRows [ 0 ];
                int complaintId = Convert.ToInt32 ( selectedRow.Cells [ "ComplaintIdColumn" ].Value );

                int assignedUserId = selectedRow.Cells [ "AssignedToUserIdColumn" ].Value != DBNull.Value ?
                                     Convert.ToInt32 ( selectedRow.Cells [ "AssignedToUserIdColumn" ].Value ) : 0;

                if ( assignedUserId != _currentUserId && assignedUserId != 0 )
                {
                    MessageBox.Show ( "Вы можете редактировать только свои назначенные жалобы или неназначенные.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    return;
                }
                using ( var form = new EditComplaintForm ( connectionString, complaintId, _currentUserId ) )
                {
                    if ( form.ShowDialog ( this ) == DialogResult.OK )
                    {
                        LoadComplaints ();
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( $"Ошибка при открытии жалобы: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnDelete_Click ( object sender, EventArgs e )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Выберите жалобу для удаления", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            var assignedUserIdCell = dataGridView.SelectedRows [ 0 ].Cells [ "AssignedToUserIdColumn" ];
            int assignedUserId = assignedUserIdCell.Value != DBNull.Value ? Convert.ToInt32 ( assignedUserIdCell.Value ) : 0;

            if ( assignedUserId != _currentUserId && assignedUserId != 0 )
            {
                MessageBox.Show ( "Вы можете удалять только свои назначенные жалобы или неназначенные.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            if ( MessageBox.Show ( "Удалить выбранную жалобу?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
            {
                int complaintId = ( int ) dataGridView.SelectedRows [ 0 ].Cells [ "ComplaintIdColumn" ].Value;

                try
                {
                    using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                    {
                        connection.Open ();
                        SqlCommand cmd = new SqlCommand ( "DELETE FROM Complaints WHERE ComplaintId = @Id", connection );
                        cmd.Parameters.AddWithValue ( "@Id", complaintId );
                        cmd.ExecuteNonQuery ();
                    }

                    LoadComplaints ();
                    MessageBox.Show ( "Жалоба успешно удалена.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( "Ошибка при удалении жалобы: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }
        private void UpdateComplaintStatus ( string newStatus )
        {
            if ( dataGridView.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( "Выберите жалобу для изменения статуса", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            int complaintId = Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "ComplaintIdColumn" ].Value );
            int assignedUserId = dataGridView.SelectedRows [ 0 ].Cells [ "AssignedToUserIdColumn" ].Value != DBNull.Value ?
                                 Convert.ToInt32 ( dataGridView.SelectedRows [ 0 ].Cells [ "AssignedToUserIdColumn" ].Value ) : 0;

            if ( assignedUserId != _currentUserId )
            {
                MessageBox.Show ( "Вы можете изменять статус только своей назначенной жалобы.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            if ( MessageBox.Show ( $"Изменить статус жалобы на '{newStatus}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
            {
                try
                {
                    using ( SqlConnection connection = new SqlConnection ( connectionString ) )
                    {
                        connection.Open ();
                        string query = "UPDATE Complaints SET Status = @Status WHERE ComplaintId = @ComplaintId";
                        SqlCommand cmd = new SqlCommand ( query, connection );
                        cmd.Parameters.AddWithValue ( "@Status", newStatus );
                        cmd.Parameters.AddWithValue ( "@ComplaintId", complaintId );
                        cmd.ExecuteNonQuery ();
                    }
                    LoadComplaints ();
                    MessageBox.Show ( $"Статус жалобы успешно изменен на '{newStatus}'.", "Обновление статуса", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show ( $"Ошибка при изменении статуса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
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

        private void btnExit_Click ( object sender, EventArgs e )
        {
            this.Close ();
            if ( _parentForm != null )
            {
                _parentForm.Show ();
            }
            else
            {
                InspectorMainForm inspectorMainForm = new InspectorMainForm (_currentUserId, _passedConnectionString);
                inspectorMainForm.Show ();
                this.Hide ();
            }
        }

        private void btnMarkAsInProgress_Click ( object sender, EventArgs e )
        {
            UpdateComplaintStatus ( "В работе" );
        }

        private void btnMarkAsClosed_Click ( object sender, EventArgs e )
        {
            UpdateComplaintStatus ( "Закрыта" );
        }

        private void btnSbros_Click ( object sender, EventArgs e )
        {
            txtSearch.Text = "";
            cmbSort.SelectedIndexChanged -= cmbSort_SelectedIndexChanged;
            cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
            ApplySortingAndFiltering ();
        }

        private void dataGridView_CellDoubleClick ( object sender, DataGridViewCellEventArgs e )
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
    }

}