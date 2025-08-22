namespace HousingControl.Forms.Admin
{
    partial class AllViolationsForm
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.btnSbros = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvViolations = new System.Windows.Forms.DataGridView();
            this.btnMarkFixed = new System.Windows.Forms.Button();
            this.btnInspDet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViolations)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtSearch.Location = new System.Drawing.Point(2, 28);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(165, 29);
            this.txtSearch.TabIndex = 32;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label2.Location = new System.Drawing.Point(765, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 31;
            this.label2.Text = "Сортировка";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label1.Location = new System.Drawing.Point(45, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 24);
            this.label1.TabIndex = 30;
            this.label1.Text = "Найти";
            // 
            // cmbSort
            // 
            this.cmbSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(725, 29);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(212, 32);
            this.cmbSort.TabIndex = 29;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // btnSbros
            // 
            this.btnSbros.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSbros.Location = new System.Drawing.Point(328, 29);
            this.btnSbros.Name = "btnSbros";
            this.btnSbros.Size = new System.Drawing.Size(305, 32);
            this.btnSbros.TabIndex = 28;
            this.btnSbros.Text = "Сбросить";
            this.btnSbros.UseVisualStyleBackColor = true;
            this.btnSbros.Click += new System.EventHandler(this.btnSbros_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnExit.Location = new System.Drawing.Point(489, 373);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(144, 64);
            this.btnExit.TabIndex = 27;
            this.btnExit.Text = "Выйти";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnDelete.Location = new System.Drawing.Point(347, 373);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(141, 64);
            this.btnDelete.TabIndex = 26;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnEdit.Location = new System.Drawing.Point(162, 373);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(185, 64);
            this.btnEdit.TabIndex = 25;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnAdd.Location = new System.Drawing.Point(2, 373);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(160, 64);
            this.btnAdd.TabIndex = 24;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvViolations
            // 
            this.dgvViolations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViolations.Location = new System.Drawing.Point(2, 67);
            this.dgvViolations.Name = "dgvViolations";
            this.dgvViolations.Size = new System.Drawing.Size(936, 300);
            this.dgvViolations.TabIndex = 23;
            this.dgvViolations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvViolations_CellDoubleClick);
            // 
            // btnMarkFixed
            // 
            this.btnMarkFixed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnMarkFixed.Location = new System.Drawing.Point(633, 373);
            this.btnMarkFixed.Name = "btnMarkFixed";
            this.btnMarkFixed.Size = new System.Drawing.Size(168, 64);
            this.btnMarkFixed.TabIndex = 33;
            this.btnMarkFixed.Text = "Отметить исправленным";
            this.btnMarkFixed.UseVisualStyleBackColor = true;
            this.btnMarkFixed.Click += new System.EventHandler(this.btnMarkFixed_Click);
            // 
            // btnInspDet
            // 
            this.btnInspDet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnInspDet.Location = new System.Drawing.Point(807, 373);
            this.btnInspDet.Name = "btnInspDet";
            this.btnInspDet.Size = new System.Drawing.Size(131, 64);
            this.btnInspDet.TabIndex = 34;
            this.btnInspDet.Text = "Проверки";
            this.btnInspDet.UseVisualStyleBackColor = true;
            this.btnInspDet.Click += new System.EventHandler(this.btnInspDet_Click_1);
            // 
            // AllViolationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 440);
            this.Controls.Add(this.btnInspDet);
            this.Controls.Add(this.btnMarkFixed);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.btnSbros);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvViolations);
            this.MaximumSize = new System.Drawing.Size(959, 479);
            this.MinimumSize = new System.Drawing.Size(959, 479);
            this.Name = "AllViolationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Нарушения";
            this.Load += new System.EventHandler(this.AllViolationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViolations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Button btnSbros;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvViolations;
        private System.Windows.Forms.Button btnMarkFixed;
        private System.Windows.Forms.Button btnInspDet;
    }
}