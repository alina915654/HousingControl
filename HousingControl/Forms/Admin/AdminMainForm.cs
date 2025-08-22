using System;
using System.Windows.Forms;
using HousingControl.Forms.Auth;

namespace HousingControl.Forms.Admin
{
    public partial class AdminMainForm : Form
    {
        public readonly int _userId;
        public readonly string _connectionString;
        public AdminMainForm ( int userId, string connectionString )
        {
            InitializeComponent ();
            _userId = userId;
            _connectionString = connectionString;
        }

        private void btnComplaints_Click ( object sender, EventArgs e )
        {
            AllComplaintsForm allComplaintsForm = new AllComplaintsForm ();
            allComplaintsForm.Show ();
            this.Hide ();
        }

        private void btnInspections_Click ( object sender, EventArgs e )
        {
            AllInspectionsForm allInspectionsForm = new AllInspectionsForm ();
            allInspectionsForm.Show ();
            this.Hide ();
        }

        private void btnViolations_Click ( object sender, EventArgs e )
        {
            AllViolationsForm allViolationsForm = new AllViolationsForm ();
            allViolationsForm.Show ();
            this.Hide ();
        }

        private void btnBuildings_Click ( object sender, EventArgs e )
        {
            BuildingsForm buildingsForm = new BuildingsForm ( _userId, _connectionString );
            buildingsForm.Show ();
            this.Hide ();
        }

        private void btnOrganizations_Click ( object sender, EventArgs e )
        {
            OrganizationsForm organizationForm = new OrganizationsForm (_userId, _connectionString);
            organizationForm.Show ();
            this.Hide ();
        }
        public int _currentUserId;
        string _passedConnectionString;
        private void btnViolationsReport_Click ( object sender, EventArgs e )
        {
            ViolationsReportForm violationsReportForm = new ViolationsReportForm ( _currentUserId, _passedConnectionString );
            violationsReportForm.Show ();
            this.Hide ();
        }

        private void button1_Click ( object sender, EventArgs e )
        {
            UsersForm usersForm = new UsersForm ();
            usersForm.Show ();
            this.Hide ();
        }

        private void button2_Click ( object sender, EventArgs e )
        {
            LoginForm loginForm = new LoginForm ();
            loginForm.Show ();
            this.Hide ();
        }

        private void AdminMainForm_Load ( object sender, EventArgs e ){}
    }
}
