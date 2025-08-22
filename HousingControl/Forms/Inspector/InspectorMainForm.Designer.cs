namespace HousingControl.Forms.Inspector
{
    partial class InspectorMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InspectorMainForm));
            this.btnActiveVio = new System.Windows.Forms.Button();
            this.btnMyComp = new System.Windows.Forms.Button();
            this.btnMyInsp = new System.Windows.Forms.Button();
            this.btnNewInsp = new System.Windows.Forms.Button();
            this.btnNewComp = new System.Windows.Forms.Button();
            this.btnVioDet = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnActiveVio
            // 
            this.btnActiveVio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnActiveVio.Location = new System.Drawing.Point(12, 12);
            this.btnActiveVio.Name = "btnActiveVio";
            this.btnActiveVio.Size = new System.Drawing.Size(143, 65);
            this.btnActiveVio.TabIndex = 0;
            this.btnActiveVio.Text = "Активные нарушения";
            this.btnActiveVio.UseVisualStyleBackColor = true;
            this.btnActiveVio.Click += new System.EventHandler(this.btnActiveVio_Click);
            // 
            // btnMyComp
            // 
            this.btnMyComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnMyComp.Location = new System.Drawing.Point(161, 12);
            this.btnMyComp.Name = "btnMyComp";
            this.btnMyComp.Size = new System.Drawing.Size(160, 65);
            this.btnMyComp.TabIndex = 2;
            this.btnMyComp.Text = "Назначенные жалобы";
            this.btnMyComp.UseVisualStyleBackColor = true;
            this.btnMyComp.Click += new System.EventHandler(this.btnMyComp_Click);
            // 
            // btnMyInsp
            // 
            this.btnMyInsp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnMyInsp.Location = new System.Drawing.Point(12, 270);
            this.btnMyInsp.Name = "btnMyInsp";
            this.btnMyInsp.Size = new System.Drawing.Size(143, 62);
            this.btnMyInsp.TabIndex = 3;
            this.btnMyInsp.Text = "Список проверок";
            this.btnMyInsp.UseVisualStyleBackColor = true;
            this.btnMyInsp.Click += new System.EventHandler(this.btnMyInsp_Click);
            // 
            // btnNewInsp
            // 
            this.btnNewInsp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnNewInsp.Location = new System.Drawing.Point(310, 270);
            this.btnNewInsp.Name = "btnNewInsp";
            this.btnNewInsp.Size = new System.Drawing.Size(160, 62);
            this.btnNewInsp.TabIndex = 4;
            this.btnNewInsp.Text = "Создание проверок";
            this.btnNewInsp.UseVisualStyleBackColor = true;
            this.btnNewInsp.Click += new System.EventHandler(this.btnNewInsp_Click);
            // 
            // btnNewComp
            // 
            this.btnNewComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnNewComp.Location = new System.Drawing.Point(161, 270);
            this.btnNewComp.Name = "btnNewComp";
            this.btnNewComp.Size = new System.Drawing.Size(143, 62);
            this.btnNewComp.TabIndex = 5;
            this.btnNewComp.Text = "Регистрация жалоб";
            this.btnNewComp.UseVisualStyleBackColor = true;
            this.btnNewComp.Click += new System.EventHandler(this.btnNewComp_Click);
            // 
            // btnVioDet
            // 
            this.btnVioDet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnVioDet.Location = new System.Drawing.Point(327, 12);
            this.btnVioDet.Name = "btnVioDet";
            this.btnVioDet.Size = new System.Drawing.Size(293, 65);
            this.btnVioDet.TabIndex = 6;
            this.btnVioDet.Text = "Детали нарушения";
            this.btnVioDet.UseVisualStyleBackColor = true;
            this.btnVioDet.Click += new System.EventHandler(this.btnVioDet_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(458, 181);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.button1.Location = new System.Drawing.Point(477, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 249);
            this.button1.TabIndex = 10;
            this.button1.Text = "Выйти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InspectorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 338);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnVioDet);
            this.Controls.Add(this.btnNewComp);
            this.Controls.Add(this.btnNewInsp);
            this.Controls.Add(this.btnMyInsp);
            this.Controls.Add(this.btnMyComp);
            this.Controls.Add(this.btnActiveVio);
            this.MaximumSize = new System.Drawing.Size(648, 377);
            this.MinimumSize = new System.Drawing.Size(648, 377);
            this.Name = "InspectorMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню инспектора";
            this.Load += new System.EventHandler(this.InspectorMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnActiveVio;
        private System.Windows.Forms.Button btnMyComp;
        private System.Windows.Forms.Button btnMyInsp;
        private System.Windows.Forms.Button btnNewInsp;
        private System.Windows.Forms.Button btnNewComp;
        private System.Windows.Forms.Button btnVioDet;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
    }
}