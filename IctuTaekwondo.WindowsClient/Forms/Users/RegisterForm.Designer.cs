namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    partial class RegisterForm
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
            panel1 = new Panel();
            label9 = new Label();
            label1 = new Label();
            tbFullName = new TextBox();
            adminRegisterSchemaBindingSource = new BindingSource(components);
            tbPhoneNumber = new TextBox();
            label2 = new Label();
            tbEmail = new TextBox();
            label3 = new Label();
            label4 = new Label();
            tbPassword = new TextBox();
            label5 = new Label();
            tbConfirmPassword = new TextBox();
            label6 = new Label();
            cbRole = new ComboBox();
            btnRegister = new Button();
            errorProvider1 = new ErrorProvider(components);
            maskedTextBox1 = new MaskedTextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)adminRegisterSchemaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 50);
            panel1.TabIndex = 17;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(171, 21);
            label9.TabIndex = 0;
            label9.Text = "ĐĂNG KÝ TÀI KHOẢN";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 62);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 19;
            label1.Text = "Họ và tên";
            // 
            // tbFullName
            // 
            tbFullName.DataBindings.Add(new Binding("Text", adminRegisterSchemaBindingSource, "FullName", true));
            tbFullName.Location = new Point(12, 87);
            tbFullName.Margin = new Padding(10);
            tbFullName.Name = "tbFullName";
            tbFullName.Size = new Size(180, 23);
            tbFullName.TabIndex = 0;
            tbFullName.Validating += tbFullName_Validating;
            // 
            // adminRegisterSchemaBindingSource
            // 
            adminRegisterSchemaBindingSource.DataSource = typeof(Schemas.AdminRegisterSchema);
            // 
            // tbPhoneNumber
            // 
            tbPhoneNumber.DataBindings.Add(new Binding("Text", adminRegisterSchemaBindingSource, "PhoneNumber", true));
            tbPhoneNumber.Location = new Point(212, 87);
            tbPhoneNumber.Margin = new Padding(10);
            tbPhoneNumber.Name = "tbPhoneNumber";
            tbPhoneNumber.Size = new Size(180, 23);
            tbPhoneNumber.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(212, 62);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 19;
            label2.Text = "Số điện thoại";
            // 
            // tbEmail
            // 
            tbEmail.DataBindings.Add(new Binding("Text", adminRegisterSchemaBindingSource, "Email", true));
            tbEmail.Location = new Point(12, 145);
            tbEmail.Margin = new Padding(10);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(180, 23);
            tbEmail.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 120);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 19;
            label3.Text = "Email";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(212, 120);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 19;
            label4.Text = "Vai trò";
            // 
            // tbPassword
            // 
            tbPassword.DataBindings.Add(new Binding("Text", adminRegisterSchemaBindingSource, "Password", true));
            tbPassword.Location = new Point(12, 203);
            tbPassword.Margin = new Padding(10);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new Size(180, 23);
            tbPassword.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 178);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 19;
            label5.Text = "Mật khẩu";
            // 
            // tbConfirmPassword
            // 
            tbConfirmPassword.DataBindings.Add(new Binding("Text", adminRegisterSchemaBindingSource, "ConfirmPassword", true));
            tbConfirmPassword.Location = new Point(212, 203);
            tbConfirmPassword.Margin = new Padding(10);
            tbConfirmPassword.Name = "tbConfirmPassword";
            tbConfirmPassword.PasswordChar = '*';
            tbConfirmPassword.Size = new Size(180, 23);
            tbConfirmPassword.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(212, 178);
            label6.Name = "label6";
            label6.Size = new Size(109, 15);
            label6.TabIndex = 19;
            label6.Text = "Xác nhận mật khẩu";
            // 
            // cbRole
            // 
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRole.FormattingEnabled = true;
            cbRole.Location = new Point(212, 145);
            cbRole.Name = "cbRole";
            cbRole.Size = new Size(180, 23);
            cbRole.TabIndex = 3;
            cbRole.SelectedIndexChanged += cbRole_SelectedIndexChanged;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRegister.Location = new Point(12, 415);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(75, 23);
            btnRegister.TabIndex = 6;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = adminRegisterSchemaBindingSource;
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.DataBindings.Add(new Binding("Text", adminRegisterSchemaBindingSource, "FullName", true));
            maskedTextBox1.Location = new Point(444, 87);
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(100, 23);
            maskedTextBox1.TabIndex = 20;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(maskedTextBox1);
            Controls.Add(btnRegister);
            Controls.Add(cbRole);
            Controls.Add(label4);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(tbConfirmPassword);
            Controls.Add(tbPassword);
            Controls.Add(label1);
            Controls.Add(tbEmail);
            Controls.Add(tbPhoneNumber);
            Controls.Add(tbFullName);
            Controls.Add(panel1);
            Name = "RegisterForm";
            Text = "RegisterForm";
            Load += RegisterForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)adminRegisterSchemaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label9;
        private Label label1;
        private TextBox tbFullName;
        private TextBox tbPhoneNumber;
        private Label label2;
        private TextBox tbEmail;
        private Label label3;
        private Label label4;
        private TextBox tbPassword;
        private Label label5;
        private TextBox tbConfirmPassword;
        private Label label6;
        private ComboBox cbRole;
        private Button btnRegister;
        private ErrorProvider errorProvider1;
        private BindingSource adminRegisterSchemaBindingSource;
        private MaskedTextBox maskedTextBox1;
    }
}