using System;
using System.Windows.Forms;
using HousingControl.Forms.Add;
using HousingControl.Forms.Admin;
using HousingControl.Forms.Auth;


namespace HousingControl.Forms.Inspector
{
    public partial class InspectorMainForm : Form
    {
        private readonly int _userId;
        private readonly string _connectionString;

        public InspectorMainForm ( int userId, string connectionString )
        {
            InitializeComponent ();
            _userId = userId;
            _connectionString = connectionString;
        }

        private void InspectorMainForm_Load ( object sender, EventArgs e )
        {
            
        }

        private void btnActiveVio_Click ( object sender, EventArgs e )
        {
            ActiveViolationsForm activeViolationsForm = new ActiveViolationsForm ( _userId, _connectionString ); 
            activeViolationsForm.Show ();
            this.Hide ();
        }

        private void btnInspDet_Click ( object sender, EventArgs e )
        {
            InspectionDetailForm inspectionDetailForm = new InspectionDetailForm ( _connectionString );
            inspectionDetailForm.ShowDialog ();
            this.Hide (); 
        }

        private void btnMyComp_Click ( object sender, EventArgs e )
        {
            MyComplaintsForm myComp = new MyComplaintsForm ( _userId, _connectionString );
            myComp.Show ();
            this.Hide ();
        }

        private void btnMyInsp_Click ( object sender, EventArgs EventArgs )
        {
            MyInspectionsForm myInspectionsForm = new MyInspectionsForm ( _userId, _connectionString );
            myInspectionsForm.Show ();
            this.Hide ();
        }

        private void btnNewComp_Click ( object sender, EventArgs e )
        {
            EditComplaintForm newComplaintForm = new EditComplaintForm ( _connectionString );
            newComplaintForm.Show ();
        }

        private void btnNewInsp_Click ( object sender, EventArgs e )
        {
            InspectionDetailForm newInspectionForm = new InspectionDetailForm ( _connectionString );
            newInspectionForm.Show (); 
        }

        private void btnVioDet_Click ( object sender, EventArgs e )
        {
            AllViolationsForm violationDetailsForm = new AllViolationsForm ( _userId, _connectionString, _userId);
            violationDetailsForm.Show ();
            this.Hide ();
        }

        private void button1_Click ( object sender, EventArgs e )
        {
            LoginForm loginForm = new LoginForm ();
            loginForm.Show ();
            this.Hide ();
        }
    }
}