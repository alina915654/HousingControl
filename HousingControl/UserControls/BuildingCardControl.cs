using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HousingControl.UserControls 
{
    public partial class BuildingCardControl : UserControl
    {
        private PictureBox pbBuildingImage;
        private Label lblAddress;
        private Label lblManagementOrg;
        private Label lblYearBuilt;
        private Label lblFloorsApartments;
        private Label lblIsEmergency;
        private Button btnEdit;
        private Button btnDelete;

        public event EventHandler<BuildingEventArgs> EditClicked;
        public event EventHandler<BuildingEventArgs> DeleteClicked;

        public int BuildingId
        {
            get; private set;
        }
        private string _connectionString;

        public BuildingCardControl ( string connectionString )
        {
            InitializeComponent ();
            _connectionString = connectionString;

            CreateAndPlaceControls ();

            btnEdit.Click += ( s, e ) => EditClicked?.Invoke ( this, new BuildingEventArgs ( BuildingId ) );
            btnDelete.Click += ( s, e ) => DeleteClicked?.Invoke ( this, new BuildingEventArgs ( BuildingId ) );
        }

        private void CreateAndPlaceControls ( )
        {
            pbBuildingImage = new PictureBox ();
            pbBuildingImage.BorderStyle = BorderStyle.FixedSingle;
            pbBuildingImage.Location = new Point ( 10, 10 );
            pbBuildingImage.Name = "pbBuildingImage";
            pbBuildingImage.Size = new Size ( 100, 100 );
            pbBuildingImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbBuildingImage.TabStop = false;
            this.Controls.Add ( pbBuildingImage );

            lblAddress = new Label ();
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font ( "Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, ( ( byte ) ( 204 ) ) );
            lblAddress.Location = new Point ( 120, 10 ); 
            lblAddress.MaximumSize = new Size ( 250, 0 );
            lblAddress.Name = "lblAddress";
            lblAddress.Text = "Адрес:";
            this.Controls.Add ( lblAddress );

            lblManagementOrg = new Label ();
            lblManagementOrg.AutoSize = true;
            lblManagementOrg.Font = new Font ( "Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ( ( byte ) ( 204 ) ) );
            lblManagementOrg.Location = new Point ( 120, 30 );
            lblManagementOrg.MaximumSize = new Size ( 250, 0 );
            lblManagementOrg.Name = "lblManagementOrg";
            lblManagementOrg.Text = "Управл. организация:";
            this.Controls.Add ( lblManagementOrg );

            lblYearBuilt = new Label ();
            lblYearBuilt.AutoSize = true;
            lblYearBuilt.Font = new Font ( "Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ( ( byte ) ( 204 ) ) );
            lblYearBuilt.Location = new Point ( 120, 50 );
            lblYearBuilt.Name = "lblYearBuilt";
            lblYearBuilt.Text = "Год постройки:";
            this.Controls.Add ( lblYearBuilt );

            lblFloorsApartments = new Label ();
            lblFloorsApartments.AutoSize = true;
            lblFloorsApartments.Font = new Font ( "Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ( ( byte ) ( 204 ) ) );
            lblFloorsApartments.Location = new Point ( 120, 70 );
            lblFloorsApartments.Name = "lblFloorsApartments";
            lblFloorsApartments.Text = "Этажей/Квартир:";
            this.Controls.Add ( lblFloorsApartments );

            lblIsEmergency = new Label ();
            lblIsEmergency.AutoSize = true;
            lblIsEmergency.Font = new Font ( "Microsoft Sans Serif", 9.5F, FontStyle.Bold, GraphicsUnit.Point, ( ( byte ) ( 204 ) ) );
            lblIsEmergency.ForeColor = Color.Red;
            lblIsEmergency.Location = new Point ( 120, 90 );
            lblIsEmergency.Name = "lblIsEmergency";
            lblIsEmergency.Text = "АВАРИЙНЫЙ!";
            lblIsEmergency.Visible = false;
            this.Controls.Add ( lblIsEmergency );

            btnEdit = new Button ();
            btnEdit.Location = new Point ( 270, 115 ); 
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size ( 110, 30 );
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = true;
            this.Controls.Add ( btnEdit );

            btnDelete = new Button ();
            btnDelete.Location = new Point ( 10, 115 ); 
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size ( 100, 30 );
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            this.Controls.Add ( btnDelete );
        }

        public void SetBuildingData ( int buildingId, string address, string managementOrgName, int? yearBuilt, int? floorsCount, int? apartmentsCount, bool isEmergency, string imageFileName )
        {
            BuildingId = buildingId;
            lblAddress.Text = address;
            lblManagementOrg.Text = $"УК: {managementOrgName ?? "Не назначена"}";
            lblYearBuilt.Text = $"Год: {yearBuilt ?? 0}";
            lblFloorsApartments.Text = $"Этажей: {floorsCount ?? 0} / Квартир: {apartmentsCount ?? 0}";
            lblIsEmergency.Visible = isEmergency;

            LoadBuildingImage ( imageFileName );
        }

        private void LoadBuildingImage ( string imageFileName )
        {
            string imagePath = Path.Combine ( Application.StartupPath, "Imagee", "Buildings", imageFileName );

            if ( File.Exists ( imagePath ) )
            {
                try
                {
                    using ( var fs = new FileStream ( imagePath, FileMode.Open, FileAccess.Read ) )
                    using ( var ms = new MemoryStream () )
                    {
                        fs.CopyTo ( ms );
                        ms.Position = 0;
                        pbBuildingImage.Image = Image.FromStream ( ms );
                    }
                }
                catch ( Exception )
                {
                    pbBuildingImage.Image = Properties.Resources.NoImage;
                }
            }
            else
            {
                pbBuildingImage.Image = Properties.Resources.NoImage;
            }
        }

        private void BuildingCardControl_Load ( object sender, EventArgs e )
        {

        }
    }

    public class BuildingEventArgs : EventArgs
    {
        public int BuildingId
        {
            get;
        }
        public BuildingEventArgs ( int buildingId )
        {
            BuildingId = buildingId;
        }
    }
}