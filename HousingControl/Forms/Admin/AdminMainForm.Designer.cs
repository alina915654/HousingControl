namespace HousingControl.Forms.Admin
{
    partial class AdminMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminMainForm));
            this.btnComplaints = new System.Windows.Forms.Button();
            this.btnInspections = new System.Windows.Forms.Button();
            this.btnViolations = new System.Windows.Forms.Button();
            this.btnBuildings = new System.Windows.Forms.Button();
            this.btnOrganizations = new System.Windows.Forms.Button();
            this.btnViolationsReport = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnComplaints
            // 
            this.btnComplaints.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnComplaints.Location = new System.Drawing.Point(12, 12);
            this.btnComplaints.Name = "btnComplaints";
            this.btnComplaints.Size = new System.Drawing.Size(140, 91);
            this.btnComplaints.TabIndex = 0;
            this.btnComplaints.Text = "Жалобы жителей";
            this.btnComplaints.UseVisualStyleBackColor = true;
            this.btnComplaints.Click += new System.EventHandler(this.btnComplaints_Click);
            // 
            // btnInspections
            // 
            this.btnInspections.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnInspections.Location = new System.Drawing.Point(158, 12);
            this.btnInspections.Name = "btnInspections";
            this.btnInspections.Size = new System.Drawing.Size(142, 91);
            this.btnInspections.TabIndex = 1;
            this.btnInspections.Text = "Проверки";
            this.btnInspections.UseVisualStyleBackColor = true;
            this.btnInspections.Click += new System.EventHandler(this.btnInspections_Click);
            // 
            // btnViolations
            // 
            this.btnViolations.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnViolations.Location = new System.Drawing.Point(304, 12);
            this.btnViolations.Name = "btnViolations";
            this.btnViolations.Size = new System.Drawing.Size(157, 91);
            this.btnViolations.TabIndex = 2;
            this.btnViolations.Text = "Нарушения";
            this.btnViolations.UseVisualStyleBackColor = true;
            this.btnViolations.Click += new System.EventHandler(this.btnViolations_Click);
            // 
            // btnBuildings
            // 
            this.btnBuildings.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnBuildings.Location = new System.Drawing.Point(467, 109);
            this.btnBuildings.Name = "btnBuildings";
            this.btnBuildings.Size = new System.Drawing.Size(160, 181);
            this.btnBuildings.TabIndex = 3;
            this.btnBuildings.Text = "Дома";
            this.btnBuildings.UseVisualStyleBackColor = true;
            this.btnBuildings.Click += new System.EventHandler(this.btnBuildings_Click);
            // 
            // btnOrganizations
            // 
            this.btnOrganizations.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnOrganizations.Location = new System.Drawing.Point(12, 296);
            this.btnOrganizations.Name = "btnOrganizations";
            this.btnOrganizations.Size = new System.Drawing.Size(226, 66);
            this.btnOrganizations.TabIndex = 4;
            this.btnOrganizations.Text = "Управляющие компании";
            this.btnOrganizations.UseVisualStyleBackColor = true;
            this.btnOrganizations.Click += new System.EventHandler(this.btnOrganizations_Click);
            // 
            // btnViolationsReport
            // 
            this.btnViolationsReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnViolationsReport.Location = new System.Drawing.Point(244, 296);
            this.btnViolationsReport.Name = "btnViolationsReport";
            this.btnViolationsReport.Size = new System.Drawing.Size(217, 66);
            this.btnViolationsReport.TabIndex = 5;
            this.btnViolationsReport.Text = "Отчеты по нарушениям";
            this.btnViolationsReport.UseVisualStyleBackColor = true;
            this.btnViolationsReport.Click += new System.EventHandler(this.btnViolationsReport_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 109);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(449, 181);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.button1.Location = new System.Drawing.Point(467, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 91);
            this.button1.TabIndex = 9;
            this.button1.Text = "Пользователи";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.button2.Location = new System.Drawing.Point(467, 296);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 66);
            this.button2.TabIndex = 10;
            this.button2.Text = "Выйти";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AdminMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 369);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnViolationsReport);
            this.Controls.Add(this.btnOrganizations);
            this.Controls.Add(this.btnBuildings);
            this.Controls.Add(this.btnViolations);
            this.Controls.Add(this.btnInspections);
            this.Controls.Add(this.btnComplaints);
            this.MaximumSize = new System.Drawing.Size(649, 408);
            this.MinimumSize = new System.Drawing.Size(649, 408);
            this.Name = "AdminMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню администратора";
            this.Load += new System.EventHandler(this.AdminMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnComplaints;
        private System.Windows.Forms.Button btnInspections;
        private System.Windows.Forms.Button btnViolations;
        private System.Windows.Forms.Button btnBuildings;
        private System.Windows.Forms.Button btnOrganizations;
        private System.Windows.Forms.Button btnViolationsReport;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}