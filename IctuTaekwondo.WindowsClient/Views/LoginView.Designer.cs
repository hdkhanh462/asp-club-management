namespace IctuTaekwondo.WindowsClient.Views
{
    partial class LoginView
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
            parrotGradientPanel1 = new ReaLTaiizor.Controls.ParrotGradientPanel();
            label3 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            dungeonLabel1 = new ReaLTaiizor.Controls.DungeonLabel();
            tbUsername = new ReaLTaiizor.Controls.DungeonTextBox();
            dungeonLabel2 = new ReaLTaiizor.Controls.DungeonLabel();
            tbPassword = new ReaLTaiizor.Controls.DungeonTextBox();
            btnLogin = new ReaLTaiizor.Controls.MaterialButton();
            cbRememberMe = new ReaLTaiizor.Controls.MaterialCheckBox();
            controlBox1 = new ReaLTaiizor.Controls.ControlBox();
            parrotGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // parrotGradientPanel1
            // 
            parrotGradientPanel1.BottomLeft = Color.Black;
            parrotGradientPanel1.BottomRight = Color.Fuchsia;
            parrotGradientPanel1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            parrotGradientPanel1.Controls.Add(label3);
            parrotGradientPanel1.Controls.Add(pictureBox2);
            parrotGradientPanel1.Controls.Add(label2);
            parrotGradientPanel1.Controls.Add(pictureBox1);
            parrotGradientPanel1.Dock = DockStyle.Left;
            parrotGradientPanel1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            parrotGradientPanel1.Location = new Point(0, 0);
            parrotGradientPanel1.Name = "parrotGradientPanel1";
            parrotGradientPanel1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            parrotGradientPanel1.PrimerColor = Color.White;
            parrotGradientPanel1.Size = new Size(450, 450);
            parrotGradientPanel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            parrotGradientPanel1.Style = ReaLTaiizor.Controls.ParrotGradientPanel.GradientStyle.Horizontal;
            parrotGradientPanel1.TabIndex = 0;
            parrotGradientPanel1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            parrotGradientPanel1.TopLeft = Color.Blue;
            parrotGradientPanel1.TopRight = Color.RoyalBlue;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(79, 325);
            label3.Margin = new Padding(3, 20, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(209, 25);
            label3.TabIndex = 1;
            label3.Text = "ICTU Teakwondo Club";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.logo;
            pictureBox2.Location = new Point(108, 138);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(150, 150);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(50, 58);
            label2.Name = "label2";
            label2.Size = new Size(267, 25);
            label2.TabIndex = 0;
            label2.Text = "CHÀO MỪNG QUAY TRỞ LẠI";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.cloud;
            pictureBox1.Location = new Point(323, -24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(154, 498);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label1.Location = new Point(506, 58);
            label1.Name = "label1";
            label1.Size = new Size(236, 25);
            label1.TabIndex = 2;
            label1.Text = "ĐĂNG NHẬP TÀI KHOẢN";
            // 
            // dungeonLabel1
            // 
            dungeonLabel1.AutoSize = true;
            dungeonLabel1.BackColor = Color.Transparent;
            dungeonLabel1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dungeonLabel1.ForeColor = Color.Black;
            dungeonLabel1.Location = new Point(483, 123);
            dungeonLabel1.Margin = new Padding(3, 0, 3, 3);
            dungeonLabel1.Name = "dungeonLabel1";
            dungeonLabel1.Size = new Size(111, 20);
            dungeonLabel1.TabIndex = 3;
            dungeonLabel1.Text = "Tên đăng nhập";
            // 
            // tbUsername
            // 
            tbUsername.BackColor = Color.Transparent;
            tbUsername.BorderColor = Color.FromArgb(180, 180, 180);
            tbUsername.EdgeColor = Color.White;
            tbUsername.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbUsername.ForeColor = Color.Black;
            tbUsername.Location = new Point(483, 149);
            tbUsername.MaxLength = 32767;
            tbUsername.Multiline = false;
            tbUsername.Name = "tbUsername";
            tbUsername.ReadOnly = false;
            tbUsername.Size = new Size(280, 32);
            tbUsername.TabIndex = 6;
            tbUsername.TabStop = false;
            tbUsername.TextAlignment = HorizontalAlignment.Left;
            tbUsername.UseSystemPasswordChar = false;
            // 
            // dungeonLabel2
            // 
            dungeonLabel2.AutoSize = true;
            dungeonLabel2.BackColor = Color.Transparent;
            dungeonLabel2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dungeonLabel2.ForeColor = Color.Black;
            dungeonLabel2.Location = new Point(483, 204);
            dungeonLabel2.Margin = new Padding(3, 20, 3, 3);
            dungeonLabel2.Name = "dungeonLabel2";
            dungeonLabel2.Size = new Size(74, 20);
            dungeonLabel2.TabIndex = 4;
            dungeonLabel2.Text = "Mật khẩu";
            // 
            // tbPassword
            // 
            tbPassword.BackColor = Color.Transparent;
            tbPassword.BorderColor = Color.FromArgb(180, 180, 180);
            tbPassword.EdgeColor = Color.White;
            tbPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbPassword.ForeColor = Color.Black;
            tbPassword.Location = new Point(483, 230);
            tbPassword.MaxLength = 32767;
            tbPassword.Multiline = false;
            tbPassword.Name = "tbPassword";
            tbPassword.ReadOnly = false;
            tbPassword.Size = new Size(280, 32);
            tbPassword.TabIndex = 7;
            tbPassword.TabStop = false;
            tbPassword.TextAlignment = HorizontalAlignment.Left;
            tbPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnLogin.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnLogin.Depth = 0;
            btnLogin.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.HighEmphasis = true;
            btnLogin.Icon = null;
            btnLogin.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnLogin.Location = new Point(483, 322);
            btnLogin.Margin = new Padding(4, 20, 4, 6);
            btnLogin.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnLogin.Name = "btnLogin";
            btnLogin.NoAccentTextColor = Color.Empty;
            btnLogin.Size = new Size(105, 36);
            btnLogin.TabIndex = 8;
            btnLogin.TabStop = false;
            btnLogin.Text = "Đăng nhập";
            btnLogin.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            btnLogin.UseAccentColor = false;
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // cbRememberMe
            // 
            cbRememberMe.AutoSize = true;
            cbRememberMe.Depth = 0;
            cbRememberMe.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            cbRememberMe.Location = new Point(483, 265);
            cbRememberMe.Margin = new Padding(0);
            cbRememberMe.MouseLocation = new Point(-1, -1);
            cbRememberMe.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            cbRememberMe.Name = "cbRememberMe";
            cbRememberMe.ReadOnly = false;
            cbRememberMe.Ripple = true;
            cbRememberMe.Size = new Size(187, 37);
            cbRememberMe.TabIndex = 1;
            cbRememberMe.TabStop = false;
            cbRememberMe.Text = "Lưu phiên đăng nhập";
            cbRememberMe.UseAccentColor = false;
            cbRememberMe.UseVisualStyleBackColor = true;
            // 
            // controlBox1
            // 
            controlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlBox1.BackColor = Color.White;
            controlBox1.CloseHoverColor = Color.FromArgb(230, 17, 35);
            controlBox1.DefaultLocation = false;
            controlBox1.EnableHoverHighlight = true;
            controlBox1.EnableMaximizeButton = false;
            controlBox1.EnableMinimizeButton = true;
            controlBox1.ForeColor = Color.Black;
            controlBox1.Location = new Point(701, 9);
            controlBox1.Margin = new Padding(0);
            controlBox1.MaximizeHoverColor = Color.FromArgb(74, 74, 74);
            controlBox1.MinimizeHoverColor = Color.FromArgb(63, 63, 65);
            controlBox1.Name = "controlBox1";
            controlBox1.Size = new Size(90, 25);
            controlBox1.TabIndex = 5;
            controlBox1.Text = "controlBox1";
            // 
            // LoginView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(controlBox1);
            Controls.Add(cbRememberMe);
            Controls.Add(btnLogin);
            Controls.Add(tbPassword);
            Controls.Add(dungeonLabel2);
            Controls.Add(tbUsername);
            Controls.Add(dungeonLabel1);
            Controls.Add(label1);
            Controls.Add(parrotGradientPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginView";
            parrotGradientPanel1.ResumeLayout(false);
            parrotGradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.ParrotGradientPanel parrotGradientPanel1;
        private PictureBox pictureBox1;
        private Label label1;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel1;
        private ReaLTaiizor.Controls.DungeonTextBox tbUsername;
        private Label label3;
        private PictureBox pictureBox2;
        private Label label2;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel2;
        private ReaLTaiizor.Controls.DungeonTextBox tbPassword;
        private ReaLTaiizor.Controls.MaterialButton btnLogin;
        private ReaLTaiizor.Controls.MaterialCheckBox cbRememberMe;
        private ReaLTaiizor.Controls.ControlBox controlBox1;
    }
}