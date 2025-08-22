namespace HousingControl.Forms.Add
{
    partial class ViolationDetailForm
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
            System.Windows.Forms.Label violationIdLabel;
            System.Windows.Forms.Label inspectionIdLabel;
            System.Windows.Forms.Label violationTypeLabel;
            System.Windows.Forms.Label descriptionLabel;
            System.Windows.Forms.Label deadlineLabel;
            System.Windows.Forms.Label isFixedLabel;
            this.housingControlDataSet = new HousingControl.HousingControlDataSet();
            this.violationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.violationsTableAdapter = new HousingControl.HousingControlDataSetTableAdapters.ViolationsTableAdapter();
            this.tableAdapterManager = new HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager();
            this.txtViolationId = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.dtpDeadline = new System.Windows.Forms.DateTimePicker();
            this.chkIsFixed = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbInspection = new System.Windows.Forms.ComboBox();
            this.txtViolationType = new System.Windows.Forms.TextBox();
            violationIdLabel = new System.Windows.Forms.Label();
            inspectionIdLabel = new System.Windows.Forms.Label();
            violationTypeLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            deadlineLabel = new System.Windows.Forms.Label();
            isFixedLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.violationsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // violationIdLabel
            // 
            violationIdLabel.AutoSize = true;
            violationIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            violationIdLabel.Location = new System.Drawing.Point(12, 9);
            violationIdLabel.Name = "violationIdLabel";
            violationIdLabel.Size = new System.Drawing.Size(135, 24);
            violationIdLabel.TabIndex = 1;
            violationIdLabel.Text = "ID нарушения:";
            // 
            // inspectionIdLabel
            // 
            inspectionIdLabel.AutoSize = true;
            inspectionIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            inspectionIdLabel.Location = new System.Drawing.Point(12, 52);
            inspectionIdLabel.Name = "inspectionIdLabel";
            inspectionIdLabel.Size = new System.Drawing.Size(102, 24);
            inspectionIdLabel.TabIndex = 3;
            inspectionIdLabel.Text = "Проверка:";
            // 
            // violationTypeLabel
            // 
            violationTypeLabel.AutoSize = true;
            violationTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            violationTypeLabel.Location = new System.Drawing.Point(12, 90);
            violationTypeLabel.Name = "violationTypeLabel";
            violationTypeLabel.Size = new System.Drawing.Size(152, 24);
            violationTypeLabel.TabIndex = 5;
            violationTypeLabel.Text = "Тип нарушения:";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            descriptionLabel.Location = new System.Drawing.Point(12, 134);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(105, 24);
            descriptionLabel.TabIndex = 7;
            descriptionLabel.Text = "Описание:";
            // 
            // deadlineLabel
            // 
            deadlineLabel.AutoSize = true;
            deadlineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            deadlineLabel.Location = new System.Drawing.Point(12, 176);
            deadlineLabel.Name = "deadlineLabel";
            deadlineLabel.Size = new System.Drawing.Size(138, 24);
            deadlineLabel.TabIndex = 9;
            deadlineLabel.Text = "Крайний срок:";
            // 
            // isFixedLabel
            // 
            isFixedLabel.AutoSize = true;
            isFixedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            isFixedLabel.Location = new System.Drawing.Point(12, 216);
            isFixedLabel.Name = "isFixedLabel";
            isFixedLabel.Size = new System.Drawing.Size(124, 24);
            isFixedLabel.TabIndex = 11;
            isFixedLabel.Text = "Исправлено:";
            // 
            // housingControlDataSet
            // 
            this.housingControlDataSet.DataSetName = "HousingControlDataSet";
            this.housingControlDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // violationsBindingSource
            // 
            this.violationsBindingSource.DataMember = "Violations";
            this.violationsBindingSource.DataSource = this.housingControlDataSet;
            // 
            // violationsTableAdapter
            // 
            this.violationsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BuildingsTableAdapter = null;
            this.tableAdapterManager.ComplaintsTableAdapter = null;
            this.tableAdapterManager.InspectionsTableAdapter = null;
            this.tableAdapterManager.ManagementOrganizationsTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsersTableAdapter = null;
            this.tableAdapterManager.ViolationsTableAdapter = this.violationsTableAdapter;
            // 
            // txtViolationId
            // 
            this.txtViolationId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.violationsBindingSource, "ViolationId", true));
            this.txtViolationId.Enabled = false;
            this.txtViolationId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtViolationId.Location = new System.Drawing.Point(192, 6);
            this.txtViolationId.Name = "txtViolationId";
            this.txtViolationId.Size = new System.Drawing.Size(200, 29);
            this.txtViolationId.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.violationsBindingSource, "Description", true));
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDescription.Location = new System.Drawing.Point(192, 131);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 29);
            this.txtDescription.TabIndex = 8;
            // 
            // dtpDeadline
            // 
            this.dtpDeadline.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.violationsBindingSource, "Deadline", true));
            this.dtpDeadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.dtpDeadline.Location = new System.Drawing.Point(192, 172);
            this.dtpDeadline.Name = "dtpDeadline";
            this.dtpDeadline.Size = new System.Drawing.Size(200, 29);
            this.dtpDeadline.TabIndex = 10;
            // 
            // chkIsFixed
            // 
            this.chkIsFixed.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.violationsBindingSource, "IsFixed", true));
            this.chkIsFixed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.chkIsFixed.Location = new System.Drawing.Point(281, 216);
            this.chkIsFixed.Name = "chkIsFixed";
            this.chkIsFixed.Size = new System.Drawing.Size(18, 24);
            this.chkIsFixed.TabIndex = 12;
            this.chkIsFixed.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSave.Location = new System.Drawing.Point(16, 264);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 35);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCancel.Location = new System.Drawing.Point(246, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(146, 35);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbInspection
            // 
            this.cmbInspection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbInspection.FormattingEnabled = true;
            this.cmbInspection.Location = new System.Drawing.Point(192, 49);
            this.cmbInspection.Name = "cmbInspection";
            this.cmbInspection.Size = new System.Drawing.Size(200, 32);
            this.cmbInspection.TabIndex = 15;
            // 
            // txtViolationType
            // 
            this.txtViolationType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.violationsBindingSource, "Description", true));
            this.txtViolationType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtViolationType.Location = new System.Drawing.Point(192, 90);
            this.txtViolationType.Name = "txtViolationType";
            this.txtViolationType.Size = new System.Drawing.Size(200, 29);
            this.txtViolationType.TabIndex = 16;
            // 
            // ViolationDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 318);
            this.Controls.Add(this.txtViolationType);
            this.Controls.Add(this.cmbInspection);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(violationIdLabel);
            this.Controls.Add(this.txtViolationId);
            this.Controls.Add(inspectionIdLabel);
            this.Controls.Add(violationTypeLabel);
            this.Controls.Add(descriptionLabel);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(deadlineLabel);
            this.Controls.Add(this.dtpDeadline);
            this.Controls.Add(isFixedLabel);
            this.Controls.Add(this.chkIsFixed);
            this.MaximumSize = new System.Drawing.Size(423, 357);
            this.MinimumSize = new System.Drawing.Size(423, 357);
            this.Name = "ViolationDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViolationDetailForm";
            this.Load += new System.EventHandler(this.ViolationDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.violationsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HousingControlDataSet housingControlDataSet;
        private System.Windows.Forms.BindingSource violationsBindingSource;
        private HousingControlDataSetTableAdapters.ViolationsTableAdapter violationsTableAdapter;
        private HousingControlDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox txtViolationId;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DateTimePicker dtpDeadline;
        private System.Windows.Forms.CheckBox chkIsFixed;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbInspection;
        private System.Windows.Forms.TextBox txtViolationType;
    }
}