namespace HousingControl.Forms.Add
{
    partial class InspectionDetailForm
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
            System.Windows.Forms.Label inspectionIdLabel;
            System.Windows.Forms.Label buildingIdLabel;
            System.Windows.Forms.Label inspectionDateLabel;
            System.Windows.Forms.Label inspectionTypeLabel;
            System.Windows.Forms.Label resultLabel;
            System.Windows.Forms.Label violationsFoundLabel;
            System.Windows.Forms.Label lblAssignedToInspector;
            this.txtInspectionId = new System.Windows.Forms.TextBox();
            this.dtpInspectionDate = new System.Windows.Forms.DateTimePicker();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.chkViolationsFound = new System.Windows.Forms.CheckBox();
            this.cmbBuilding = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbInspectionType = new System.Windows.Forms.ComboBox();
            this.cmbInspector = new System.Windows.Forms.ComboBox();
            inspectionIdLabel = new System.Windows.Forms.Label();
            buildingIdLabel = new System.Windows.Forms.Label();
            inspectionDateLabel = new System.Windows.Forms.Label();
            inspectionTypeLabel = new System.Windows.Forms.Label();
            resultLabel = new System.Windows.Forms.Label();
            violationsFoundLabel = new System.Windows.Forms.Label();
            lblAssignedToInspector = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inspectionIdLabel
            // 
            inspectionIdLabel.AutoSize = true;
            inspectionIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            inspectionIdLabel.Location = new System.Drawing.Point(11, 15);
            inspectionIdLabel.Name = "inspectionIdLabel";
            inspectionIdLabel.Size = new System.Drawing.Size(123, 24);
            inspectionIdLabel.TabIndex = 1;
            inspectionIdLabel.Text = "ID проверки:";
            // 
            // buildingIdLabel
            // 
            buildingIdLabel.AutoSize = true;
            buildingIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            buildingIdLabel.Location = new System.Drawing.Point(12, 106);
            buildingIdLabel.Name = "buildingIdLabel";
            buildingIdLabel.Size = new System.Drawing.Size(53, 24);
            buildingIdLabel.TabIndex = 3;
            buildingIdLabel.Text = "Дом:";
            // 
            // inspectionDateLabel
            // 
            inspectionDateLabel.AutoSize = true;
            inspectionDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            inspectionDateLabel.Location = new System.Drawing.Point(12, 148);
            inspectionDateLabel.Name = "inspectionDateLabel";
            inspectionDateLabel.Size = new System.Drawing.Size(150, 24);
            inspectionDateLabel.TabIndex = 5;
            inspectionDateLabel.Text = "Дата проверки:";
            // 
            // inspectionTypeLabel
            // 
            inspectionTypeLabel.AutoSize = true;
            inspectionTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            inspectionTypeLabel.Location = new System.Drawing.Point(12, 188);
            inspectionTypeLabel.Name = "inspectionTypeLabel";
            inspectionTypeLabel.Size = new System.Drawing.Size(140, 24);
            inspectionTypeLabel.TabIndex = 9;
            inspectionTypeLabel.Text = "Тип проверки:";
            // 
            // resultLabel
            // 
            resultLabel.AutoSize = true;
            resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            resultLabel.Location = new System.Drawing.Point(12, 228);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new System.Drawing.Size(108, 24);
            resultLabel.TabIndex = 11;
            resultLabel.Text = "Результат:";
            // 
            // violationsFoundLabel
            // 
            violationsFoundLabel.AutoSize = true;
            violationsFoundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            violationsFoundLabel.Location = new System.Drawing.Point(12, 266);
            violationsFoundLabel.Name = "violationsFoundLabel";
            violationsFoundLabel.Size = new System.Drawing.Size(231, 24);
            violationsFoundLabel.TabIndex = 13;
            violationsFoundLabel.Text = "Выявленные нарушения:";
            // 
            // lblAssignedToInspector
            // 
            lblAssignedToInspector.AutoSize = true;
            lblAssignedToInspector.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            lblAssignedToInspector.Location = new System.Drawing.Point(11, 60);
            lblAssignedToInspector.Name = "lblAssignedToInspector";
            lblAssignedToInspector.Size = new System.Drawing.Size(112, 24);
            lblAssignedToInspector.TabIndex = 20;
            lblAssignedToInspector.Text = "Инспектор:";
            // 
            // txtInspectionId
            // 
            this.txtInspectionId.Enabled = false;
            this.txtInspectionId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtInspectionId.Location = new System.Drawing.Point(275, 12);
            this.txtInspectionId.Name = "txtInspectionId";
            this.txtInspectionId.Size = new System.Drawing.Size(200, 29);
            this.txtInspectionId.TabIndex = 2;
            // 
            // dtpInspectionDate
            // 
            this.dtpInspectionDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.dtpInspectionDate.Location = new System.Drawing.Point(276, 144);
            this.dtpInspectionDate.Name = "dtpInspectionDate";
            this.dtpInspectionDate.Size = new System.Drawing.Size(200, 29);
            this.dtpInspectionDate.TabIndex = 6;
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtResult.Location = new System.Drawing.Point(276, 225);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(200, 29);
            this.txtResult.TabIndex = 12;
            // 
            // chkViolationsFound
            // 
            this.chkViolationsFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.chkViolationsFound.Location = new System.Drawing.Point(353, 269);
            this.chkViolationsFound.Name = "chkViolationsFound";
            this.chkViolationsFound.Size = new System.Drawing.Size(21, 24);
            this.chkViolationsFound.TabIndex = 14;
            this.chkViolationsFound.UseVisualStyleBackColor = true;
            // 
            // cmbBuilding
            // 
            this.cmbBuilding.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbBuilding.FormattingEnabled = true;
            this.cmbBuilding.Location = new System.Drawing.Point(276, 102);
            this.cmbBuilding.Name = "cmbBuilding";
            this.cmbBuilding.Size = new System.Drawing.Size(200, 32);
            this.cmbBuilding.TabIndex = 15;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCancel.Location = new System.Drawing.Point(317, 307);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(159, 35);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSave.Location = new System.Drawing.Point(16, 307);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 35);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbInspectionType
            // 
            this.cmbInspectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbInspectionType.FormattingEnabled = true;
            this.cmbInspectionType.Location = new System.Drawing.Point(276, 185);
            this.cmbInspectionType.Name = "cmbInspectionType";
            this.cmbInspectionType.Size = new System.Drawing.Size(200, 32);
            this.cmbInspectionType.TabIndex = 19;
            // 
            // cmbInspector
            // 
            this.cmbInspector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInspector.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbInspector.FormattingEnabled = true;
            this.cmbInspector.Location = new System.Drawing.Point(275, 56);
            this.cmbInspector.Name = "cmbInspector";
            this.cmbInspector.Size = new System.Drawing.Size(200, 32);
            this.cmbInspector.TabIndex = 21;
            // 
            // InspectionDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 352);
            this.Controls.Add(this.cmbInspector);
            this.Controls.Add(lblAssignedToInspector);
            this.Controls.Add(this.cmbInspectionType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbBuilding);
            this.Controls.Add(inspectionIdLabel);
            this.Controls.Add(this.txtInspectionId);
            this.Controls.Add(buildingIdLabel);
            this.Controls.Add(inspectionDateLabel);
            this.Controls.Add(this.dtpInspectionDate);
            this.Controls.Add(inspectionTypeLabel);
            this.Controls.Add(resultLabel);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(violationsFoundLabel);
            this.Controls.Add(this.chkViolationsFound);
            this.MaximumSize = new System.Drawing.Size(509, 391);
            this.MinimumSize = new System.Drawing.Size(509, 391);
            this.Name = "InspectionDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InspectionDetailForm";
            this.Load += new System.EventHandler(this.InspectionDetailForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtInspectionId;
        private System.Windows.Forms.DateTimePicker dtpInspectionDate;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.CheckBox chkViolationsFound;
        private System.Windows.Forms.ComboBox cmbBuilding;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbInspectionType;
        private System.Windows.Forms.ComboBox cmbInspector;
    }
}