namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    partial class UsersDetailForm
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
            btnDelete = new Button();
            btnSave = new Button();
            tbJoinDate = new TextBox();
            tbAddress = new TextBox();
            tbDateOfBirth = new TextBox();
            tbPhoneNumber = new TextBox();
            tbFullName = new TextBox();
            tbId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            panel1 = new Panel();
            label9 = new Label();
            pbAvatar = new PictureBox();
            cbCurrentRank = new ComboBox();
            userUpdateSchemaBindingSource = new BindingSource(components);
            cbGender = new ComboBox();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userUpdateSchemaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(93, 302);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Location = new Point(12, 302);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 8;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // tbJoinDate
            // 
            tbJoinDate.Location = new Point(212, 145);
            tbJoinDate.Margin = new Padding(10);
            tbJoinDate.Name = "tbJoinDate";
            tbJoinDate.Size = new Size(180, 23);
            tbJoinDate.TabIndex = 3;
            // 
            // tbAddress
            // 
            tbAddress.Location = new Point(212, 261);
            tbAddress.Margin = new Padding(10);
            tbAddress.Name = "tbAddress";
            tbAddress.Size = new Size(180, 23);
            tbAddress.TabIndex = 7;
            // 
            // tbDateOfBirth
            // 
            tbDateOfBirth.Location = new Point(212, 203);
            tbDateOfBirth.Margin = new Padding(10);
            tbDateOfBirth.Name = "tbDateOfBirth";
            tbDateOfBirth.Size = new Size(180, 23);
            tbDateOfBirth.TabIndex = 5;
            // 
            // tbPhoneNumber
            // 
            tbPhoneNumber.Location = new Point(12, 203);
            tbPhoneNumber.Margin = new Padding(10);
            tbPhoneNumber.Name = "tbPhoneNumber";
            tbPhoneNumber.Size = new Size(180, 23);
            tbPhoneNumber.TabIndex = 4;
            // 
            // tbFullName
            // 
            tbFullName.Location = new Point(12, 145);
            tbFullName.Margin = new Padding(10);
            tbFullName.Name = "tbFullName";
            tbFullName.Size = new Size(180, 23);
            tbFullName.TabIndex = 2;
            // 
            // tbId
            // 
            tbId.Enabled = false;
            tbId.Location = new Point(12, 87);
            tbId.Margin = new Padding(10);
            tbId.Name = "tbId";
            tbId.Size = new Size(180, 23);
            tbId.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 62);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 12;
            label1.Text = "Id";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 120);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 14;
            label2.Text = "Họ và tên";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 178);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 16;
            label3.Text = "Số điện thoại";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 236);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 18;
            label4.Text = "Giới tính";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 62);
            label5.Margin = new Padding(3);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 13;
            label5.Text = "Cấp đai";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(212, 120);
            label6.Name = "label6";
            label6.Size = new Size(85, 15);
            label6.TabIndex = 15;
            label6.Text = "Ngày tham gia";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(212, 178);
            label7.Name = "label7";
            label7.Size = new Size(60, 15);
            label7.TabIndex = 17;
            label7.Text = "Ngày sinh";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(212, 236);
            label8.Name = "label8";
            label8.Size = new Size(43, 15);
            label8.TabIndex = 19;
            label8.Text = "Địa chỉ";
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(585, 50);
            panel1.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(208, 21);
            label9.TabIndex = 0;
            label9.Text = "THÔNG TIN NGƯỜI DÙNG";
            // 
            // pbAvatar
            // 
            pbAvatar.BorderStyle = BorderStyle.FixedSingle;
            pbAvatar.Location = new Point(405, 85);
            pbAvatar.Name = "pbAvatar";
            pbAvatar.Size = new Size(167, 141);
            pbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            pbAvatar.TabIndex = 17;
            pbAvatar.TabStop = false;
            pbAvatar.Click += pbAvatar_Click;
            // 
            // cbCurrentRank
            // 
            cbCurrentRank.DataBindings.Add(new Binding("SelectedValue", userUpdateSchemaBindingSource, "CurrentRank", true));
            cbCurrentRank.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCurrentRank.FormattingEnabled = true;
            cbCurrentRank.Location = new Point(212, 87);
            cbCurrentRank.Name = "cbCurrentRank";
            cbCurrentRank.Size = new Size(180, 23);
            cbCurrentRank.TabIndex = 20;
            // 
            // userUpdateSchemaBindingSource
            // 
            userUpdateSchemaBindingSource.DataSource = typeof(Shared.Schemas.Account.UserUpdateSchema);
            // 
            // cbGender
            // 
            cbGender.DataBindings.Add(new Binding("SelectedValue", userUpdateSchemaBindingSource, "Gender", true));
            cbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGender.FormattingEnabled = true;
            cbGender.Location = new Point(12, 261);
            cbGender.Name = "cbGender";
            cbGender.Size = new Size(180, 23);
            cbGender.TabIndex = 21;
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = userUpdateSchemaBindingSource;
            // 
            // UsersDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 337);
            Controls.Add(cbGender);
            Controls.Add(cbCurrentRank);
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
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Name = "UsersDetailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UsersDetailForm";
            FormClosed += UsersDetailForm_FormClosed;
            Load += UsersDetailForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)userUpdateSchemaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnDelete;
        private Button btnSave;
        private TextBox tbJoinDate;
        private TextBox tbAddress;
        private TextBox tbDateOfBirth;
        private TextBox tbPhoneNumber;
        private TextBox tbFullName;
        private TextBox tbId;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Panel panel1;
        private Label label9;
        private PictureBox pbAvatar;
        private ComboBox cbCurrentRank;
        private ComboBox cbGender;
        private ErrorProvider errorProvider1;
        private BindingSource userUpdateSchemaBindingSource;
    }
}