namespace HousingControl.Forms.Add
{
    partial class UserDetailForm
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
            System.Windows.Forms.Label passwordLabel;
            System.Windows.Forms.Label fullNameLabel;
            System.Windows.Forms.Label roleLabel;
            this.housingControlDataSet = new HousingControl.HousingControlDataSet();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usersTableAdapter = new HousingControl.HousingControlDataSetTableAdapters.UsersTableAdapter();
            this.tableAdapterManager = new HousingControl.HousingControlDataSetTableAdapters.TableAdapterManager();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            userIdLabel = new System.Windows.Forms.Label();
            usernameLabel = new System.Windows.Forms.Label();
            passwordLabel = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            roleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.housingControlDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // userIdLabel
            // 
            userIdLabel.AutoSize = true;
            userIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            userIdLabel.Location = new System.Drawing.Point(12, 19);
            userIdLabel.Name = "userIdLabel";
            userIdLabel.Size = new System.Drawing.Size(163, 24);
            userIdLabel.TabIndex = 1;
            userIdLabel.Text = "ID пользователя:";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            usernameLabel.Location = new System.Drawing.Point(12, 60);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new System.Drawing.Size(69, 24);
            usernameLabel.TabIndex = 3;
            usernameLabel.Text = "Логин:";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            passwordLabel.Location = new System.Drawing.Point(12, 101);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new System.Drawing.Size(81, 24);
            passwordLabel.TabIndex = 5;
            passwordLabel.Text = "Пароль:";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            fullNameLabel.Location = new System.Drawing.Point(12, 142);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(59, 24);
            fullNameLabel.TabIndex = 7;
            fullNameLabel.Text = "ФИО:";
            // 
            // roleLabel
            // 
            roleLabel.AutoSize = true;
            roleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            roleLabel.Location = new System.Drawing.Point(12, 183);
            roleLabel.Name = "roleLabel";
            roleLabel.Size = new System.Drawing.Size(59, 24);
            roleLabel.TabIndex = 9;
            roleLabel.Text = "Роль:";
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
            this.txtUserId.Location = new System.Drawing.Point(223, 19);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(160, 29);
            this.txtUserId.TabIndex = 2;
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.usersBindingSource, "Username", true));
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtUsername.Location = new System.Drawing.Point(223, 60);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(160, 29);
            this.txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.usersBindingSource, "Password", true));
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtPassword.Location = new System.Drawing.Point(223, 101);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(160, 29);
            this.txtPassword.TabIndex = 6;
            // 
            // txtFullName
            // 
            this.txtFullName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.usersBindingSource, "FullName", true));
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtFullName.Location = new System.Drawing.Point(223, 142);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(160, 29);
            this.txtFullName.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSave.Location = new System.Drawing.Point(16, 228);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 36);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCancel.Location = new System.Drawing.Point(223, 228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 36);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbRole
            // 
            this.cmbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(223, 186);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(160, 32);
            this.cmbRole.TabIndex = 13;
            // 
            // UserDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 272);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(userIdLabel);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(usernameLabel);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(fullNameLabel);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(roleLabel);
            this.MaximumSize = new System.Drawing.Size(409, 311);
            this.MinimumSize = new System.Drawing.Size(409, 311);
            this.Name = "UserDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование пользователя";
            this.Load += new System.EventHandler(this.UserDetailForm_Load);
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbRole;
    }
}