namespace HousingControl.Forms.Add
{
    partial class EditOrganizationsForm
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
            System.Windows.Forms.Label orgIdLabel;
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label orgTypeLabel;
            System.Windows.Forms.Label phoneLabel;
            System.Windows.Forms.Label addresLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.housingControlDataSet = new HousingControl.HousingControlDataSet();
            this.managementOrganizationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.managementOrganizationsTableAdapter = new HousingControl.HousingControlDataSetTableAdapters.ManagementOrganizationsTableAdapter();
            this.tableAdapterManager = new HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager();
            this.txtOrgId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbOrgType = new System.Windows.Forms.ComboBox();
            this.txtLicenseNumber = new System.Windows.Forms.TextBox();
            this.txtDirectorName = new System.Windows.Forms.TextBox();
            orgIdLabel = new System.Windows.Forms.Label();
            nameLabel = new System.Windows.Forms.Label();
            orgTypeLabel = new System.Windows.Forms.Label();
            phoneLabel = new System.Windows.Forms.Label();
            addresLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managementOrganizationsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // orgIdLabel
            // 
            orgIdLabel.AutoSize = true;
            orgIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            orgIdLabel.Location = new System.Drawing.Point(14, 13);
            orgIdLabel.Name = "orgIdLabel";
            orgIdLabel.Size = new System.Drawing.Size(153, 24);
            orgIdLabel.TabIndex = 1;
            orgIdLabel.Text = "ID организации:";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            nameLabel.Location = new System.Drawing.Point(14, 55);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(102, 24);
            nameLabel.TabIndex = 3;
            nameLabel.Text = "Название:";
            // 
            // orgTypeLabel
            // 
            orgTypeLabel.AutoSize = true;
            orgTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            orgTypeLabel.Location = new System.Drawing.Point(14, 93);
            orgTypeLabel.Name = "orgTypeLabel";
            orgTypeLabel.Size = new System.Drawing.Size(170, 24);
            orgTypeLabel.TabIndex = 5;
            orgTypeLabel.Text = "Тип организации:";
            // 
            // phoneLabel
            // 
            phoneLabel.AutoSize = true;
            phoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            phoneLabel.Location = new System.Drawing.Point(14, 136);
            phoneLabel.Name = "phoneLabel";
            phoneLabel.Size = new System.Drawing.Size(95, 24);
            phoneLabel.TabIndex = 7;
            phoneLabel.Text = "Телефон:";
            // 
            // addresLabel
            // 
            addresLabel.AutoSize = true;
            addresLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            addresLabel.Location = new System.Drawing.Point(14, 261);
            addresLabel.Name = "addresLabel";
            addresLabel.Size = new System.Drawing.Size(69, 24);
            addresLabel.TabIndex = 11;
            addresLabel.Text = "Почта:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            label1.Location = new System.Drawing.Point(14, 178);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(165, 24);
            label1.TabIndex = 17;
            label1.Text = "Номер лицензии:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            label2.Location = new System.Drawing.Point(14, 222);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(152, 24);
            label2.TabIndex = 19;
            label2.Text = "Имя директора:";
            // 
            // housingControlDataSet
            // 
            this.housingControlDataSet.DataSetName = "HousingControlDataSet";
            this.housingControlDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // managementOrganizationsBindingSource
            // 
            this.managementOrganizationsBindingSource.DataMember = "ManagementOrganizations";
            this.managementOrganizationsBindingSource.DataSource = this.housingControlDataSet;
            // 
            // managementOrganizationsTableAdapter
            // 
            this.managementOrganizationsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BuildingsTableAdapter = null;
            this.tableAdapterManager.ComplaintsTableAdapter = null;
            this.tableAdapterManager.InspectionsTableAdapter = null;
            this.tableAdapterManager.ManagementOrganizationsTableAdapter = this.managementOrganizationsTableAdapter;
            this.tableAdapterManager.UpdateOrder = HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsersTableAdapter = null;
            this.tableAdapterManager.ViolationsTableAdapter = null;
            // 
            // txtOrgId
            // 
            this.txtOrgId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementOrganizationsBindingSource, "OrgId", true));
            this.txtOrgId.Enabled = false;
            this.txtOrgId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtOrgId.Location = new System.Drawing.Point(236, 10);
            this.txtOrgId.Name = "txtOrgId";
            this.txtOrgId.Size = new System.Drawing.Size(151, 29);
            this.txtOrgId.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementOrganizationsBindingSource, "Name", true));
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtName.Location = new System.Drawing.Point(236, 52);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(151, 29);
            this.txtName.TabIndex = 4;
            // 
            // txtPhone
            // 
            this.txtPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementOrganizationsBindingSource, "Phone", true));
            this.txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtPhone.Location = new System.Drawing.Point(236, 133);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(151, 29);
            this.txtPhone.TabIndex = 8;
            // 
            // txtEmail
            // 
            this.txtEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementOrganizationsBindingSource, "Addres", true));
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtEmail.Location = new System.Drawing.Point(236, 258);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(151, 29);
            this.txtEmail.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSave.Location = new System.Drawing.Point(18, 306);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(143, 41);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCancel.Location = new System.Drawing.Point(236, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 41);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbOrgType
            // 
            this.cmbOrgType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbOrgType.FormattingEnabled = true;
            this.cmbOrgType.Location = new System.Drawing.Point(236, 93);
            this.cmbOrgType.Name = "cmbOrgType";
            this.cmbOrgType.Size = new System.Drawing.Size(151, 32);
            this.cmbOrgType.TabIndex = 15;
            // 
            // txtLicenseNumber
            // 
            this.txtLicenseNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementOrganizationsBindingSource, "HouseNumber", true));
            this.txtLicenseNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtLicenseNumber.Location = new System.Drawing.Point(236, 175);
            this.txtLicenseNumber.Name = "txtLicenseNumber";
            this.txtLicenseNumber.Size = new System.Drawing.Size(151, 29);
            this.txtLicenseNumber.TabIndex = 16;
            // 
            // txtDirectorName
            // 
            this.txtDirectorName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementOrganizationsBindingSource, "HouseNumber", true));
            this.txtDirectorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDirectorName.Location = new System.Drawing.Point(236, 214);
            this.txtDirectorName.Name = "txtDirectorName";
            this.txtDirectorName.Size = new System.Drawing.Size(151, 29);
            this.txtDirectorName.TabIndex = 18;
            // 
            // EditOrganizationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 357);
            this.Controls.Add(label2);
            this.Controls.Add(this.txtDirectorName);
            this.Controls.Add(label1);
            this.Controls.Add(this.txtLicenseNumber);
            this.Controls.Add(this.cmbOrgType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(orgIdLabel);
            this.Controls.Add(this.txtOrgId);
            this.Controls.Add(nameLabel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(orgTypeLabel);
            this.Controls.Add(phoneLabel);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(addresLabel);
            this.Controls.Add(this.txtEmail);
            this.MaximumSize = new System.Drawing.Size(417, 396);
            this.MinimumSize = new System.Drawing.Size(417, 396);
            this.Name = "EditOrganizationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditOrganizationsForm";
            this.Load += new System.EventHandler(this.EditOrganizationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managementOrganizationsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HousingControlDataSet housingControlDataSet;
        private System.Windows.Forms.BindingSource managementOrganizationsBindingSource;
        private HousingControlDataSetTableAdapters.ManagementOrganizationsTableAdapter managementOrganizationsTableAdapter;
        private HousingControlDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox txtOrgId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbOrgType;
        private System.Windows.Forms.TextBox txtLicenseNumber;
        private System.Windows.Forms.TextBox txtDirectorName;
    }
}