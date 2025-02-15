namespace IctuTaekwondo.WindowsClient.Forms
{
    partial class MainForm
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
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            btnLogout = new Button();
            btnAccount = new Button();
            btnFinances = new Button();
            btnAchivements = new Button();
            btnEvents = new Button();
            btnUsers = new Button();
            btnDashboard = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 50);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(102, 21);
            label1.TabIndex = 0;
            label1.Text = "TRANG CHỦ";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnLogout);
            panel2.Controls.Add(btnAccount);
            panel2.Controls.Add(btnFinances);
            panel2.Controls.Add(btnAchivements);
            panel2.Controls.Add(btnEvents);
            panel2.Controls.Add(btnUsers);
            panel2.Controls.Add(btnDashboard);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 50);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 400);
            panel2.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.Dock = DockStyle.Top;
            btnLogout.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogout.Location = new Point(0, 240);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(200, 40);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnAccount
            // 
            btnAccount.Dock = DockStyle.Top;
            btnAccount.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAccount.Location = new Point(0, 200);
            btnAccount.Name = "btnAccount";
            btnAccount.Size = new Size(200, 40);
            btnAccount.TabIndex = 5;
            btnAccount.Text = "Tài khoản";
            btnAccount.UseVisualStyleBackColor = true;
            btnAccount.Click += btnAccount_Click;
            // 
            // btnFinances
            // 
            btnFinances.Dock = DockStyle.Top;
            btnFinances.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFinances.Location = new Point(0, 160);
            btnFinances.Name = "btnFinances";
            btnFinances.Size = new Size(200, 40);
            btnFinances.TabIndex = 4;
            btnFinances.Text = "Quản lý tài chính";
            btnFinances.UseVisualStyleBackColor = true;
            btnFinances.Visible = false;
            btnFinances.Click += btnFinances_Click;
            // 
            // btnAchivements
            // 
            btnAchivements.Dock = DockStyle.Top;
            btnAchivements.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAchivements.Location = new Point(0, 120);
            btnAchivements.Name = "btnAchivements";
            btnAchivements.Size = new Size(200, 40);
            btnAchivements.TabIndex = 3;
            btnAchivements.Text = "Quản lý thành tích";
            btnAchivements.UseVisualStyleBackColor = true;
            btnAchivements.Click += btnAchivements_Click;
            // 
            // btnEvents
            // 
            btnEvents.Dock = DockStyle.Top;
            btnEvents.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEvents.Location = new Point(0, 80);
            btnEvents.Name = "btnEvents";
            btnEvents.Size = new Size(200, 40);
            btnEvents.TabIndex = 2;
            btnEvents.Text = "Quản lý sự kiện";
            btnEvents.UseVisualStyleBackColor = true;
            btnEvents.Click += btnEventsManager_Click;
            // 
            // btnUsers
            // 
            btnUsers.Dock = DockStyle.Top;
            btnUsers.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUsers.Location = new Point(0, 40);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(200, 40);
            btnUsers.TabIndex = 1;
            btnUsers.Text = "Quản lý người dùng";
            btnUsers.UseVisualStyleBackColor = true;
            btnUsers.Visible = false;
            btnUsers.Click += buttonUsers_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDashboard.Location = new Point(0, 0);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(200, 40);
            btnDashboard.TabIndex = 0;
            btnDashboard.Text = "Bảng điều khiển";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Visible = false;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Button btnUsers;
        private Button btnDashboard;
        private Button btnFinances;
        private Button btnAchivements;
        private Button btnEvents;
        private Button btnLogout;
        private Button btnAccount;
    }
}