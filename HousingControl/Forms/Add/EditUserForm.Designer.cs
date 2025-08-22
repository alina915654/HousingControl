namespace HousingControl.Forms.Add
{
    partial class EditUserForm
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
            System.Windows.Forms.Label userIdLabel;
            System.Windows.Forms.Label usernameLabel;
            System.Windows.Forms.Label labelP;
            System.Windows.Forms.Label fullNameLabel;
            System.Windows.Forms.Label roleLabel;
            System.Windows.Forms.Label labelCP;
            this.housingControlDataSet = new HousingControl.HousingControlDataSet();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usersTableAdapter = new HousingControl.HousingControlDataSetTableAdapters.UsersTableAdapter();
            this.tableAdapterManager = new HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            userIdLabel = new System.Windows.Forms.Label();
            usernameLabel = new System.Windows.Forms.Label();
            labelP = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            roleLabel = new System.Windows.Forms.Label();
            labelCP = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // userIdLabel
            // 
            userIdLabel.AutoSize = true;
            userIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            userIdLabel.Location = new System.Drawing.Point(20, 15);
            userIdLabel.Name = "userIdLabel";
            userIdLabel.Size = new System.Drawing.Size(163, 24);
            userIdLabel.TabIndex = 1;
            userIdLabel.Text = "ID пользователя:";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            usernameLabel.Location = new System.Drawing.Point(20, 52);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new System.Drawing.Size(69, 24);
            usernameLabel.TabIndex = 3;
            usernameLabel.Text = "Логин:";
            // 
            // labelP
            // 
            labelP.AutoSize = true;
            labelP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            labelP.Location = new System.Drawing.Point(20, 91);
            labelP.Name = "labelP";
            labelP.Size = new System.Drawing.Size(81, 24);
            labelP.TabIndex = 5;
            labelP.Text = "Пароль:";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            fullNameLabel.Location = new System.Drawing.Point(20, 166);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(59, 24);
            fullNameLabel.TabIndex = 7;
            fullNameLabel.Text = "ФИО:";
            // 
            // roleLabel
            // 
            roleLabel.AutoSize = true;
            roleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            roleLabel.Location = new System.Drawing.Point(20, 204);
            roleLabel.Name = "roleLabel";
            roleLabel.Size = new System.Drawing.Size(59, 24);
            roleLabel.TabIndex = 9;
            roleLabel.Text = "Роль:";
            // 
            // labelCP
            // 
            labelCP.AutoSize = true;
            labelCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            labelCP.Location = new System.Drawing.Point(20, 129);
            labelCP.Name = "labelCP";
            labelCP.Size = new System.Drawing.Size(183, 24);
            labelCP.TabIndex = 21;
            labelCP.Text = "Повторить пароль:";
            // 
            // housingControlDataSet
            // 
            this.housingControlDataSet.DataSetName = "HousingControlDataSet";
            this.housingControlDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // usersBindingSource
            // 
            this.usersBindingSource.DataMember = "Users";
            this.usersBindingSource.DataSource = this.housingControlDataSet;
            // 
            // usersTableAdapter
            // 
            this.usersTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BuildingsTableAdapter = null;
            this.tableAdapterManager.ComplaintsTableAdapter = null;
            this.tableAdapterManager.InspectionsTableAdapter = null;
            this.tableAdapterManager.ManagementOrganizationsTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsersTableAdapter = this.usersTableAdapter;
            this.tableAdapterManager.ViolationsTableAdapter = null;
            // 
            // txtUserId
            // 
            this.txtUserId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.usersBindingSource, "UserId", true));
            this.txtUserId.Enabled = false;
            this.txtUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtUserId.Location = new System.Drawing.Point(219, 12);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(148, 29);
            this.txtUserId.TabIndex = 2;
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.usersBindingSource, "Username", true));
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtUsername.Location = new System.Drawing.Point(219, 49);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(148, 29);
            this.txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.usersBindingSource, "Password", true));
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtPassword.Location = new System.Drawing.Point(219, 88);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(148, 29);
            this.txtPassword.TabIndex = 6;
            // 
            // txtFullName
            // 
            this.txtFullName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.usersBindingSource, "FullName", true));
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtFullName.Location = new System.Drawing.Point(219, 163);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(148, 29);
            this.txtFullName.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCancel.Location = new System.Drawing.Point(239, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 35);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSave.Location = new System.Drawing.Point(24, 244);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 35);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.chkShowPassword.Location = new System.Drawing.Point(373, 116);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(15, 14);
            this.chkShowPassword.TabIndex = 19;
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // cmbRole
            // 
            this.cmbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(219, 206);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(148, 32);
            this.cmbRole.TabIndex = 20;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.usersBindingSource, "Password", true));
            this.txtConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(219, 126);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(148, 29);
            this.txtConfirmPassword.TabIndex = 22;
            // 
            // EditUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 282);
            this.Controls.Add(labelCP);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(userIdLabel);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(usernameLabel);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(labelP);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(fullNameLabel);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(roleLabel);
            this.MaximumSize = new System.Drawing.Size(408, 321);
            this.MinimumSize = new System.Drawing.Size(408, 321);
            this.Name = "EditUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditUserForm";
            this.Load += new System.EventHandler(this.EditUserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HousingControlDataSet housingControlDataSet;
        private System.Windows.Forms.BindingSource usersBindingSource;
        private HousingControlDataSetTableAdapters.UsersTableAdapter usersTableAdapter;
        private HousingControlDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.TextBox txtConfirmPassword;
    }
}