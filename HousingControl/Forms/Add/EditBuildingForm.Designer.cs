namespace HousingControl.Forms.Add
{
    partial class EditBuildingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label buildingIdLabel;
            System.Windows.Forms.Label addressLabel;
            System.Windows.Forms.Label floorsCountLabel;
            System.Windows.Forms.Label apartmentsCountLabel;
            System.Windows.Forms.Label yearBuiltLabel;
            System.Windows.Forms.Label orgIdLabel;
            System.Windows.Forms.Label isEmergencyLabel;
            System.Windows.Forms.Label label1;
            this.housingControlDataSet = new HousingControl.HousingControlDataSet();
            this.buildingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buildingsTableAdapter = new HousingControl.HousingControlDataSetTableAdapters.BuildingsTableAdapter();
            this.tableAdapterManager = new HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager();
            this.txtBuildingId = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtFloorsCount = new System.Windows.Forms.TextBox();
            this.txtApartmentsCount = new System.Windows.Forms.TextBox();
            this.txtYearBuilt = new System.Windows.Forms.TextBox();
            this.chkIsEmergency = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbManagementOrg = new System.Windows.Forms.ComboBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.txtImageFilePath = new System.Windows.Forms.TextBox();
            this.pbSelectedImage = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            buildingIdLabel = new System.Windows.Forms.Label();
            addressLabel = new System.Windows.Forms.Label();
            floorsCountLabel = new System.Windows.Forms.Label();
            apartmentsCountLabel = new System.Windows.Forms.Label();
            yearBuiltLabel = new System.Windows.Forms.Label();
            orgIdLabel = new System.Windows.Forms.Label();
            isEmergencyLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // buildingIdLabel
            // 
            buildingIdLabel.AutoSize = true;
            buildingIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            buildingIdLabel.Location = new System.Drawing.Point(12, 9);
            buildingIdLabel.Name = "buildingIdLabel";
            buildingIdLabel.Size = new System.Drawing.Size(83, 24);
            buildingIdLabel.TabIndex = 1;
            buildingIdLabel.Text = "ID дома:";
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            addressLabel.Location = new System.Drawing.Point(12, 47);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new System.Drawing.Size(72, 24);
            addressLabel.TabIndex = 3;
            addressLabel.Text = "Адрес:";
            // 
            // floorsCountLabel
            // 
            floorsCountLabel.AutoSize = true;
            floorsCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            floorsCountLabel.Location = new System.Drawing.Point(12, 86);
            floorsCountLabel.Name = "floorsCountLabel";
            floorsCountLabel.Size = new System.Drawing.Size(193, 24);
            floorsCountLabel.TabIndex = 5;
            floorsCountLabel.Text = "Количество этажей:";
            // 
            // apartmentsCountLabel
            // 
            apartmentsCountLabel.AutoSize = true;
            apartmentsCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            apartmentsCountLabel.Location = new System.Drawing.Point(12, 126);
            apartmentsCountLabel.Name = "apartmentsCountLabel";
            apartmentsCountLabel.Size = new System.Drawing.Size(200, 24);
            apartmentsCountLabel.TabIndex = 7;
            apartmentsCountLabel.Text = "Количество квартир:";
            // 
            // yearBuiltLabel
            // 
            yearBuiltLabel.AutoSize = true;
            yearBuiltLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            yearBuiltLabel.Location = new System.Drawing.Point(12, 165);
            yearBuiltLabel.Name = "yearBuiltLabel";
            yearBuiltLabel.Size = new System.Drawing.Size(149, 24);
            yearBuiltLabel.TabIndex = 9;
            yearBuiltLabel.Text = "Год постройки:";
            // 
            // orgIdLabel
            // 
            orgIdLabel.AutoSize = true;
            orgIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            orgIdLabel.Location = new System.Drawing.Point(12, 202);
            orgIdLabel.Name = "orgIdLabel";
            orgIdLabel.Size = new System.Drawing.Size(134, 24);
            orgIdLabel.TabIndex = 11;
            orgIdLabel.Text = "Организация:";
            // 
            // isEmergencyLabel
            // 
            isEmergencyLabel.AutoSize = true;
            isEmergencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            isEmergencyLabel.Location = new System.Drawing.Point(12, 245);
            isEmergencyLabel.Name = "isEmergencyLabel";
            isEmergencyLabel.Size = new System.Drawing.Size(231, 24);
            isEmergencyLabel.TabIndex = 13;
            isEmergencyLabel.Text = "Чрезвычайная ситуация:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            label1.Location = new System.Drawing.Point(12, 417);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(257, 24);
            label1.TabIndex = 21;
            label1.Text = "Путь к файлу изображения:";
            // 
            // housingControlDataSet
            // 
            this.housingControlDataSet.DataSetName = "HousingControlDataSet";
            this.housingControlDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // buildingsBindingSource
            // 
            this.buildingsBindingSource.DataMember = "Buildings";
            this.buildingsBindingSource.DataSource = this.housingControlDataSet;
            // 
            // buildingsTableAdapter
            // 
            this.buildingsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BuildingsTableAdapter = this.buildingsTableAdapter;
            this.tableAdapterManager.ComplaintsTableAdapter = null;
            this.tableAdapterManager.InspectionsTableAdapter = null;
            this.tableAdapterManager.ManagementOrganizationsTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsersTableAdapter = null;
            this.tableAdapterManager.ViolationsTableAdapter = null;
            // 
            // txtBuildingId
            // 
            this.txtBuildingId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.buildingsBindingSource, "BuildingId", true));
            this.txtBuildingId.Enabled = false;
            this.txtBuildingId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtBuildingId.Location = new System.Drawing.Point(275, 6);
            this.txtBuildingId.Name = "txtBuildingId";
            this.txtBuildingId.Size = new System.Drawing.Size(139, 29);
            this.txtBuildingId.TabIndex = 2;
            // 
            // txtAddress
            // 
            this.txtAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.buildingsBindingSource, "Address", true));
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtAddress.Location = new System.Drawing.Point(275, 44);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(139, 29);
            this.txtAddress.TabIndex = 4;
            // 
            // txtFloorsCount
            // 
            this.txtFloorsCount.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.buildingsBindingSource, "FloorsCount", true));
            this.txtFloorsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtFloorsCount.Location = new System.Drawing.Point(275, 83);
            this.txtFloorsCount.Name = "txtFloorsCount";
            this.txtFloorsCount.Size = new System.Drawing.Size(139, 29);
            this.txtFloorsCount.TabIndex = 6;
            // 
            // txtApartmentsCount
            // 
            this.txtApartmentsCount.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.buildingsBindingSource, "ApartmentsCount", true));
            this.txtApartmentsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtApartmentsCount.Location = new System.Drawing.Point(275, 123);
            this.txtApartmentsCount.Name = "txtApartmentsCount";
            this.txtApartmentsCount.Size = new System.Drawing.Size(139, 29);
            this.txtApartmentsCount.TabIndex = 8;
            // 
            // txtYearBuilt
            // 
            this.txtYearBuilt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.buildingsBindingSource, "YearBuilt", true));
            this.txtYearBuilt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtYearBuilt.Location = new System.Drawing.Point(275, 162);
            this.txtYearBuilt.Name = "txtYearBuilt";
            this.txtYearBuilt.Size = new System.Drawing.Size(139, 29);
            this.txtYearBuilt.TabIndex = 10;
            // 
            // chkIsEmergency
            // 
            this.chkIsEmergency.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.buildingsBindingSource, "IsEmergency", true));
            this.chkIsEmergency.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.chkIsEmergency.Location = new System.Drawing.Point(339, 246);
            this.chkIsEmergency.Name = "chkIsEmergency";
            this.chkIsEmergency.Size = new System.Drawing.Size(17, 24);
            this.chkIsEmergency.TabIndex = 14;
            this.chkIsEmergency.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSave.Location = new System.Drawing.Point(12, 459);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(172, 44);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCancel.Location = new System.Drawing.Point(246, 459);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(168, 44);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbManagementOrg
            // 
            this.cmbManagementOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbManagementOrg.FormattingEnabled = true;
            this.cmbManagementOrg.Location = new System.Drawing.Point(275, 202);
            this.cmbManagementOrg.Name = "cmbManagementOrg";
            this.cmbManagementOrg.Size = new System.Drawing.Size(139, 32);
            this.cmbManagementOrg.TabIndex = 17;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSelectImage.Location = new System.Drawing.Point(275, 284);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(139, 127);
            this.btnSelectImage.TabIndex = 18;
            this.btnSelectImage.Text = "Добавить фото";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click_1);
            // 
            // txtImageFilePath
            // 
            this.txtImageFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.buildingsBindingSource, "YearBuilt", true));
            this.txtImageFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtImageFilePath.Location = new System.Drawing.Point(275, 417);
            this.txtImageFilePath.Name = "txtImageFilePath";
            this.txtImageFilePath.Size = new System.Drawing.Size(139, 29);
            this.txtImageFilePath.TabIndex = 19;
            // 
            // pbSelectedImage
            // 
            this.pbSelectedImage.Location = new System.Drawing.Point(16, 284);
            this.pbSelectedImage.Name = "pbSelectedImage";
            this.pbSelectedImage.Size = new System.Drawing.Size(253, 130);
            this.pbSelectedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSelectedImage.TabIndex = 20;
            this.pbSelectedImage.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // EditBuildingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 513);
            this.Controls.Add(label1);
            this.Controls.Add(this.pbSelectedImage);
            this.Controls.Add(this.txtImageFilePath);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.cmbManagementOrg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(buildingIdLabel);
            this.Controls.Add(this.txtBuildingId);
            this.Controls.Add(addressLabel);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(floorsCountLabel);
            this.Controls.Add(this.txtFloorsCount);
            this.Controls.Add(apartmentsCountLabel);
            this.Controls.Add(this.txtApartmentsCount);
            this.Controls.Add(yearBuiltLabel);
            this.Controls.Add(this.txtYearBuilt);
            this.Controls.Add(orgIdLabel);
            this.Controls.Add(isEmergencyLabel);
            this.Controls.Add(this.chkIsEmergency);
            this.MaximumSize = new System.Drawing.Size(438, 552);
            this.MinimumSize = new System.Drawing.Size(438, 552);
            this.Name = "EditBuildingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditBuildingForm";
            this.Load += new System.EventHandler(this.EditBuildingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HousingControlDataSet housingControlDataSet;
        private System.Windows.Forms.BindingSource buildingsBindingSource;
        private HousingControlDataSetTableAdapters.BuildingsTableAdapter buildingsTableAdapter;
        private HousingControlDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox txtBuildingId;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtFloorsCount;
        private System.Windows.Forms.TextBox txtApartmentsCount;
        private System.Windows.Forms.TextBox txtYearBuilt;
        private System.Windows.Forms.CheckBox chkIsEmergency;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbManagementOrg;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.TextBox txtImageFilePath;
        private System.Windows.Forms.PictureBox pbSelectedImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}