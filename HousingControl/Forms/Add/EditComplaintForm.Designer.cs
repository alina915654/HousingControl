namespace HousingControl.Forms.Add
{
    partial class EditComplaintForm
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
            System.Windows.Forms.Label complaintIdLabel;
            System.Windows.Forms.Label buildingIdLabel;
            System.Windows.Forms.Label complaintDateLabel;
            System.Windows.Forms.Label residentNameLabel;
            System.Windows.Forms.Label contactPhoneLabel;
            System.Windows.Forms.Label descriptionLabel;
            System.Windows.Forms.Label statusLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.housingControlDataSet = new HousingControl.HousingControlDataSet();
            this.complaintsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.complaintsTableAdapter = new HousingControl.HousingControlDataSetTableAdapters.ComplaintsTableAdapter();
            this.tableAdapterManager = new HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager();
            this.complaintIdTextBox = new System.Windows.Forms.TextBox();
            this.dtpComplaintDate = new System.Windows.Forms.DateTimePicker();
            this.txtResidentName = new System.Windows.Forms.TextBox();
            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbBuilding = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbAssignedToUser = new System.Windows.Forms.ComboBox();
            complaintIdLabel = new System.Windows.Forms.Label();
            buildingIdLabel = new System.Windows.Forms.Label();
            complaintDateLabel = new System.Windows.Forms.Label();
            residentNameLabel = new System.Windows.Forms.Label();
            contactPhoneLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            statusLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.complaintsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // complaintIdLabel
            // 
            complaintIdLabel.AutoSize = true;
            complaintIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            complaintIdLabel.Location = new System.Drawing.Point(15, 15);
            complaintIdLabel.Name = "complaintIdLabel";
            complaintIdLabel.Size = new System.Drawing.Size(106, 24);
            complaintIdLabel.TabIndex = 1;
            complaintIdLabel.Text = "ID жалобы:";
            // 
            // buildingIdLabel
            // 
            buildingIdLabel.AutoSize = true;
            buildingIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            buildingIdLabel.Location = new System.Drawing.Point(15, 53);
            buildingIdLabel.Name = "buildingIdLabel";
            buildingIdLabel.Size = new System.Drawing.Size(53, 24);
            buildingIdLabel.TabIndex = 3;
            buildingIdLabel.Text = "Дом:";
            // 
            // complaintDateLabel
            // 
            complaintDateLabel.AutoSize = true;
            complaintDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            complaintDateLabel.Location = new System.Drawing.Point(15, 94);
            complaintDateLabel.Name = "complaintDateLabel";
            complaintDateLabel.Size = new System.Drawing.Size(133, 24);
            complaintDateLabel.TabIndex = 5;
            complaintDateLabel.Text = "Дата жалобы:";
            // 
            // residentNameLabel
            // 
            residentNameLabel.AutoSize = true;
            residentNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            residentNameLabel.Location = new System.Drawing.Point(15, 133);
            residentNameLabel.Name = "residentNameLabel";
            residentNameLabel.Size = new System.Drawing.Size(130, 24);
            residentNameLabel.TabIndex = 7;
            residentNameLabel.Text = "ФИО жителя:";
            // 
            // contactPhoneLabel
            // 
            contactPhoneLabel.AutoSize = true;
            contactPhoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            contactPhoneLabel.Location = new System.Drawing.Point(15, 224);
            contactPhoneLabel.Name = "contactPhoneLabel";
            contactPhoneLabel.Size = new System.Drawing.Size(95, 24);
            contactPhoneLabel.TabIndex = 9;
            contactPhoneLabel.Text = "Телефон:";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            descriptionLabel.Location = new System.Drawing.Point(15, 263);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(105, 24);
            descriptionLabel.TabIndex = 11;
            descriptionLabel.Text = "Описание:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            statusLabel.Location = new System.Drawing.Point(15, 297);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(77, 24);
            statusLabel.TabIndex = 13;
            statusLabel.Text = "Статус:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            label1.Location = new System.Drawing.Point(15, 170);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(136, 24);
            label1.TabIndex = 19;
            label1.Text = "Назначенный ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            label2.Location = new System.Drawing.Point(15, 193);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(145, 24);
            label2.TabIndex = 21;
            label2.Text = "пользователю:";
            // 
            // housingControlDataSet
            // 
            this.housingControlDataSet.DataSetName = "HousingControlDataSet";
            this.housingControlDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // complaintsBindingSource
            // 
            this.complaintsBindingSource.DataMember = "Complaints";
            this.complaintsBindingSource.DataSource = this.housingControlDataSet;
            // 
            // complaintsTableAdapter
            // 
            this.complaintsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BuildingsTableAdapter = null;
            this.tableAdapterManager.ComplaintsTableAdapter = this.complaintsTableAdapter;
            this.tableAdapterManager.InspectionsTableAdapter = null;
            this.tableAdapterManager.ManagementOrganizationsTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsersTableAdapter = null;
            this.tableAdapterManager.ViolationsTableAdapter = null;
            // 
            // complaintIdTextBox
            // 
            this.complaintIdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.complaintsBindingSource, "ComplaintId", true));
            this.complaintIdTextBox.Enabled = false;
            this.complaintIdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.complaintIdTextBox.Location = new System.Drawing.Point(318, 12);
            this.complaintIdTextBox.Name = "complaintIdTextBox";
            this.complaintIdTextBox.Size = new System.Drawing.Size(200, 29);
            this.complaintIdTextBox.TabIndex = 2;
            // 
            // dtpComplaintDate
            // 
            this.dtpComplaintDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.complaintsBindingSource, "ComplaintDate", true));
            this.dtpComplaintDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.dtpComplaintDate.Location = new System.Drawing.Point(318, 90);
            this.dtpComplaintDate.Name = "dtpComplaintDate";
            this.dtpComplaintDate.Size = new System.Drawing.Size(200, 29);
            this.dtpComplaintDate.TabIndex = 6;
            // 
            // txtResidentName
            // 
            this.txtResidentName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.complaintsBindingSource, "ResidentName", true));
            this.txtResidentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtResidentName.Location = new System.Drawing.Point(318, 130);
            this.txtResidentName.Name = "txtResidentName";
            this.txtResidentName.Size = new System.Drawing.Size(200, 29);
            this.txtResidentName.TabIndex = 8;
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.complaintsBindingSource, "ContactPhone", true));
            this.txtContactPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtContactPhone.Location = new System.Drawing.Point(318, 221);
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(200, 29);
            this.txtContactPhone.TabIndex = 10;
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.complaintsBindingSource, "Description", true));
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDescription.Location = new System.Drawing.Point(318, 260);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 29);
            this.txtDescription.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSave.Location = new System.Drawing.Point(19, 345);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 35);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCancel.Location = new System.Drawing.Point(359, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(159, 35);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbBuilding
            // 
            this.cmbBuilding.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbBuilding.FormattingEnabled = true;
            this.cmbBuilding.Location = new System.Drawing.Point(318, 51);
            this.cmbBuilding.Name = "cmbBuilding";
            this.cmbBuilding.Size = new System.Drawing.Size(200, 32);
            this.cmbBuilding.TabIndex = 17;
            // 
            // cmbStatus
            // 
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(318, 297);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 32);
            this.cmbStatus.TabIndex = 18;
            // 
            // cmbAssignedToUser
            // 
            this.cmbAssignedToUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbAssignedToUser.FormattingEnabled = true;
            this.cmbAssignedToUser.Location = new System.Drawing.Point(318, 177);
            this.cmbAssignedToUser.Name = "cmbAssignedToUser";
            this.cmbAssignedToUser.Size = new System.Drawing.Size(200, 32);
            this.cmbAssignedToUser.TabIndex = 20;
            // 
            // EditComplaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 389);
            this.Controls.Add(label2);
            this.Controls.Add(this.cmbAssignedToUser);
            this.Controls.Add(label1);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.cmbBuilding);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(complaintIdLabel);
            this.Controls.Add(this.complaintIdTextBox);
            this.Controls.Add(buildingIdLabel);
            this.Controls.Add(complaintDateLabel);
            this.Controls.Add(this.dtpComplaintDate);
            this.Controls.Add(residentNameLabel);
            this.Controls.Add(this.txtResidentName);
            this.Controls.Add(contactPhoneLabel);
            this.Controls.Add(this.txtContactPhone);
            this.Controls.Add(descriptionLabel);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(statusLabel);
            this.MaximumSize = new System.Drawing.Size(544, 428);
            this.MinimumSize = new System.Drawing.Size(544, 428);
            this.Name = "EditComplaintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EditComplaintForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.complaintsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HousingControlDataSet housingControlDataSet;
        private System.Windows.Forms.BindingSource complaintsBindingSource;
        private HousingControlDataSetTableAdapters.ComplaintsTableAdapter complaintsTableAdapter;
        private HousingControlDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox complaintIdTextBox;
        private System.Windows.Forms.DateTimePicker dtpComplaintDate;
        private System.Windows.Forms.TextBox txtResidentName;
        private System.Windows.Forms.TextBox txtContactPhone;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbBuilding;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbAssignedToUser;
    }
}