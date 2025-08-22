namespace HousingControl.Forms.Inspector
{
    partial class ActiveViolationsForm
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
            this.btnInspDet = new System.Windows.Forms.Button();
            this.btnMarkFixed = new System.Windows.Forms.Button();
            this.dgvActiveViolations = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveViolations)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtSearch.Location = new System.Drawing.Point(1, 34);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(165, 29);
            this.txtSearch.TabIndex = 20;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label2.Location = new System.Drawing.Point(638, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 19;
            this.label2.Text = "Сортировка";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label1.Location = new System.Drawing.Point(44, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 24);
            this.label1.TabIndex = 18;
            this.label1.Text = "Найти";
            // 
            // cmbSort
            // 
            this.cmbSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(587, 34);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(212, 32);
            this.cmbSort.TabIndex = 17;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // btnSbros
            // 
            this.btnSbros.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSbros.Location = new System.Drawing.Point(266, 34);
            this.btnSbros.Name = "btnSbros";
            this.btnSbros.Size = new System.Drawing.Size(231, 32);
            this.btnSbros.TabIndex = 16;
            this.btnSbros.Text = "Сбросить";
            this.btnSbros.UseVisualStyleBackColor = true;
            this.btnSbros.Click += new System.EventHandler(this.btnSbros_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnExit.Location = new System.Drawing.Point(628, 396);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(171, 46);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Выйти";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnInspDet
            // 
            this.btnInspDet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnInspDet.Location = new System.Drawing.Point(375, 396);
            this.btnInspDet.Name = "btnInspDet";
            this.btnInspDet.Size = new System.Drawing.Size(247, 46);
            this.btnInspDet.TabIndex = 13;
            this.btnInspDet.Text = "Проверка";
            this.btnInspDet.UseVisualStyleBackColor = true;
            this.btnInspDet.Click += new System.EventHandler(this.btnInspDet_Click);
            // 
            // btnMarkFixed
            // 
            this.btnMarkFixed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnMarkFixed.Location = new System.Drawing.Point(1, 397);
            this.btnMarkFixed.Name = "btnMarkFixed";
            this.btnMarkFixed.Size = new System.Drawing.Size(368, 46);
            this.btnMarkFixed.TabIndex = 12;
            this.btnMarkFixed.Text = "Отметить исправленным";
            this.btnMarkFixed.UseVisualStyleBackColor = true;
            this.btnMarkFixed.Click += new System.EventHandler(this.btnMarkFixed_Click);
            // 
            // dgvActiveViolations
            // 
            this.dgvActiveViolations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveViolations.Location = new System.Drawing.Point(1, 73);
            this.dgvActiveViolations.Name = "dgvActiveViolations";
            this.dgvActiveViolations.Size = new System.Drawing.Size(798, 317);
            this.dgvActiveViolations.TabIndex = 11;
            this.dgvActiveViolations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvActiveViolations_CellDoubleClick);
            // 
            // ActiveViolationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.btnSbros);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnInspDet);
            this.Controls.Add(this.btnMarkFixed);
            this.Controls.Add(this.dgvActiveViolations);
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "ActiveViolationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Активные нарушения";
            this.Load += new System.EventHandler(this.ActiveViolationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveViolations)).EndInit();
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
        private System.Windows.Forms.Button btnInspDet;
        private System.Windows.Forms.Button btnMarkFixed;
        private System.Windows.Forms.DataGridView dgvActiveViolations;
    }
}