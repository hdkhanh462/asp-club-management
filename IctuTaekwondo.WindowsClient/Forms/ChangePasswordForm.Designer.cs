namespace IctuTaekwondo.WindowsClient.Forms
{
    partial class ChangePasswordForm
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
            label2 = new Label();
            tbCurrentPassword = new TextBox();
            changePasswordSchemaBindingSource = new BindingSource(components);
            tbNewPassword = new TextBox();
            label1 = new Label();
            tbConfirmNewPassword = new TextBox();
            label3 = new Label();
            btnSave = new Button();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)changePasswordSchemaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(215, 50);
            panel1.TabIndex = 35;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(127, 21);
            label9.TabIndex = 0;
            label9.Text = "ĐỔI MẬT KHẨU";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 66);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 36;
            label2.Text = "Mật khẩu cũ";
            // 
            // tbCurrentPassword
            // 
            tbCurrentPassword.DataBindings.Add(new Binding("Text", changePasswordSchemaBindingSource, "CurrentPassword", true));
            tbCurrentPassword.Location = new Point(12, 91);
            tbCurrentPassword.Margin = new Padding(10);
            tbCurrentPassword.Name = "tbCurrentPassword";
            tbCurrentPassword.Size = new Size(180, 23);
            tbCurrentPassword.TabIndex = 34;
            // 
            // changePasswordSchemaBindingSource
            // 
            changePasswordSchemaBindingSource.DataSource = typeof(Shared.Schemas.Account.ChangePasswordSchema);
            // 
            // tbNewPassword
            // 
            tbNewPassword.DataBindings.Add(new Binding("Text", changePasswordSchemaBindingSource, "NewPassword", true));
            tbNewPassword.Location = new Point(12, 149);
            tbNewPassword.Margin = new Padding(10);
            tbNewPassword.Name = "tbNewPassword";
            tbNewPassword.Size = new Size(180, 23);
            tbNewPassword.TabIndex = 34;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 124);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 36;
            label1.Text = "Mật khẩu mới";
            // 
            // tbConfirmNewPassword
            // 
            tbConfirmNewPassword.DataBindings.Add(new Binding("Text", changePasswordSchemaBindingSource, "ConfirmNewPassword", true));
            tbConfirmNewPassword.Location = new Point(12, 207);
            tbConfirmNewPassword.Margin = new Padding(10);
            tbConfirmNewPassword.Name = "tbConfirmNewPassword";
            tbConfirmNewPassword.Size = new Size(180, 23);
            tbConfirmNewPassword.TabIndex = 34;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 182);
            label3.Name = "label3";
            label3.Size = new Size(133, 15);
            label3.TabIndex = 36;
            label3.Text = "Xác nhận mật khẩu mới";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Location = new Point(12, 244);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 23);
            btnSave.TabIndex = 37;
            btnSave.Text = "Lưu thay đổi";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = changePasswordSchemaBindingSource;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(215, 279);
            Controls.Add(btnSave);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(tbConfirmNewPassword);
            Controls.Add(label1);
            Controls.Add(tbNewPassword);
            Controls.Add(label2);
            Controls.Add(tbCurrentPassword);
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChangePasswordForm";
            Load += ChangePasswordForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)changePasswordSchemaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label9;
        private Label label2;
        private TextBox tbCurrentPassword;
        private TextBox tbNewPassword;
        private Label label1;
        private TextBox tbConfirmNewPassword;
        private Label label3;
        private Button btnSave;
        private BindingSource changePasswordSchemaBindingSource;
        private ErrorProvider errorProvider1;
    }
}