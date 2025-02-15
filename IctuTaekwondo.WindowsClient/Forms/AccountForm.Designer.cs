namespace IctuTaekwondo.WindowsClient.Forms
{
    partial class AccountForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            errorProvider1 = new ErrorProvider(components);
            userUpdateSchemaBindingSource = new BindingSource(components);
            cbCurrentRank = new ComboBox();
            label9 = new Label();
            cbGender = new ComboBox();
            pbAvatar = new PictureBox();
            panel1 = new Panel();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label1 = new Label();
            tbJoinDate = new TextBox();
            tbAddress = new TextBox();
            tbDateOfBirth = new TextBox();
            tbPhoneNumber = new TextBox();
            tbFullName = new TextBox();
            tbId = new TextBox();
            btnChangePassword = new Button();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userUpdateSchemaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = userUpdateSchemaBindingSource;
            // 
            // userUpdateSchemaBindingSource
            // 
            userUpdateSchemaBindingSource.DataSource = typeof(Shared.Schemas.Account.UserUpdateSchema);
            // 
            // cbCurrentRank
            // 
            cbCurrentRank.DataBindings.Add(new Binding("SelectedValue", userUpdateSchemaBindingSource, "CurrentRank", true));
            cbCurrentRank.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCurrentRank.FormattingEnabled = true;
            cbCurrentRank.Location = new Point(212, 87);
            cbCurrentRank.Name = "cbCurrentRank";
            cbCurrentRank.Size = new Size(180, 23);
            cbCurrentRank.TabIndex = 40;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(188, 21);
            label9.TabIndex = 0;
            label9.Text = "THÔNG TIN TÀI KHOẢN";
            // 
            // cbGender
            // 
            cbGender.DataBindings.Add(new Binding("SelectedValue", userUpdateSchemaBindingSource, "Gender", true));
            cbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGender.FormattingEnabled = true;
            cbGender.Location = new Point(12, 261);
            cbGender.Name = "cbGender";
            cbGender.Size = new Size(180, 23);
            cbGender.TabIndex = 41;
            // 
            // pbAvatar
            // 
            pbAvatar.BorderStyle = BorderStyle.FixedSingle;
            pbAvatar.DataBindings.Add(new Binding("ImageLocation", userUpdateSchemaBindingSource, "AvatarUrl", true));
            pbAvatar.Location = new Point(415, 85);
            pbAvatar.Name = "pbAvatar";
            pbAvatar.Size = new Size(167, 141);
            pbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            pbAvatar.TabIndex = 36;
            pbAvatar.TabStop = false;
            pbAvatar.Click += pbAvatar_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(595, 50);
            panel1.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 236);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 38;
            label4.Text = "Giới tính";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 178);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 35;
            label3.Text = "Số điện thoại";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 120);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 33;
            label2.Text = "Họ và tên";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(212, 236);
            label8.Name = "label8";
            label8.Size = new Size(43, 15);
            label8.TabIndex = 39;
            label8.Text = "Địa chỉ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(212, 178);
            label7.Name = "label7";
            label7.Size = new Size(60, 15);
            label7.TabIndex = 37;
            label7.Text = "Ngày sinh";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(212, 120);
            label6.Name = "label6";
            label6.Size = new Size(85, 15);
            label6.TabIndex = 34;
            label6.Text = "Ngày tham gia";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 62);
            label5.Margin = new Padding(3);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 32;
            label5.Text = "Cấp đai";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 62);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 31;
            label1.Text = "Id";
            // 
            // tbJoinDate
            // 
            tbJoinDate.DataBindings.Add(new Binding("Text", userUpdateSchemaBindingSource, "JoinDate", true));
            tbJoinDate.Location = new Point(212, 145);
            tbJoinDate.Margin = new Padding(10);
            tbJoinDate.Name = "tbJoinDate";
            tbJoinDate.Size = new Size(180, 23);
            tbJoinDate.TabIndex = 24;
            // 
            // tbAddress
            // 
            tbAddress.DataBindings.Add(new Binding("Text", userUpdateSchemaBindingSource, "Address", true));
            tbAddress.Location = new Point(212, 261);
            tbAddress.Margin = new Padding(10);
            tbAddress.Name = "tbAddress";
            tbAddress.Size = new Size(180, 23);
            tbAddress.TabIndex = 27;
            // 
            // tbDateOfBirth
            // 
            tbDateOfBirth.DataBindings.Add(new Binding("Text", userUpdateSchemaBindingSource, "DateOfBirth", true));
            tbDateOfBirth.Location = new Point(212, 203);
            tbDateOfBirth.Margin = new Padding(10);
            tbDateOfBirth.Name = "tbDateOfBirth";
            tbDateOfBirth.Size = new Size(180, 23);
            tbDateOfBirth.TabIndex = 26;
            // 
            // tbPhoneNumber
            // 
            tbPhoneNumber.DataBindings.Add(new Binding("Text", userUpdateSchemaBindingSource, "PhoneNumber", true));
            tbPhoneNumber.Location = new Point(12, 203);
            tbPhoneNumber.Margin = new Padding(10);
            tbPhoneNumber.Name = "tbPhoneNumber";
            tbPhoneNumber.Size = new Size(180, 23);
            tbPhoneNumber.TabIndex = 25;
            // 
            // tbFullName
            // 
            tbFullName.DataBindings.Add(new Binding("Text", userUpdateSchemaBindingSource, "FullName", true));
            tbFullName.Location = new Point(12, 145);
            tbFullName.Margin = new Padding(10);
            tbFullName.Name = "tbFullName";
            tbFullName.Size = new Size(180, 23);
            tbFullName.TabIndex = 23;
            // 
            // tbId
            // 
            tbId.Enabled = false;
            tbId.Location = new Point(12, 87);
            tbId.Margin = new Padding(10);
            tbId.Name = "tbId";
            tbId.Size = new Size(180, 23);
            tbId.TabIndex = 22;
            // 
            // btnChangePassword
            // 
            btnChangePassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnChangePassword.Location = new Point(93, 301);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(99, 23);
            btnChangePassword.TabIndex = 29;
            btnChangePassword.Text = "Đổi mật khẩu";
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Location = new Point(12, 301);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 28;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // AccountForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 336);
            Controls.Add(cbCurrentRank);
            Controls.Add(cbGender);
            Controls.Add(pbAvatar);
            Controls.Add(panel1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(tbJoinDate);
            Controls.Add(tbAddress);
            Controls.Add(tbDateOfBirth);
            Controls.Add(tbPhoneNumber);
            Controls.Add(tbFullName);
            Controls.Add(tbId);
            Controls.Add(btnChangePassword);
            Controls.Add(btnSave);
            Name = "AccountForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AccountForm";
            FormClosed += AccountForm_FormClosed;
            Load += AccountForm_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)userUpdateSchemaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ErrorProvider errorProvider1;
        private ComboBox cbCurrentRank;
        private ComboBox cbGender;
        private PictureBox pbAvatar;
        private Panel panel1;
        private Label label9;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label1;
        private TextBox tbJoinDate;
        private TextBox tbAddress;
        private TextBox tbDateOfBirth;
        private TextBox tbPhoneNumber;
        private TextBox tbFullName;
        private TextBox tbId;
        private Button btnChangePassword;
        private Button btnSave;
        private BindingSource userUpdateSchemaBindingSource;
    }
}