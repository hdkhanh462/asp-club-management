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
            btnFinances = new Button();
            btnAchivements = new Button();
            btnEvents = new Button();
            buttonUsers = new Button();
            buttonDashboard = new Button();
            buttonHome = new Button();
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
            panel2.Controls.Add(btnFinances);
            panel2.Controls.Add(btnAchivements);
            panel2.Controls.Add(btnEvents);
            panel2.Controls.Add(buttonUsers);
            panel2.Controls.Add(buttonDashboard);
            panel2.Controls.Add(buttonHome);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 50);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 400);
            panel2.TabIndex = 1;
            // 
            // btnFinances
            // 
            btnFinances.Dock = DockStyle.Top;
            btnFinances.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFinances.Location = new Point(0, 200);
            btnFinances.Name = "btnFinances";
            btnFinances.Size = new Size(200, 40);
            btnFinances.TabIndex = 7;
            btnFinances.Text = "Quản lý tài chính";
            btnFinances.UseVisualStyleBackColor = true;
            btnFinances.Click += btnFinances_Click;
            // 
            // btnAchivements
            // 
            btnAchivements.Dock = DockStyle.Top;
            btnAchivements.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAchivements.Location = new Point(0, 160);
            btnAchivements.Name = "btnAchivements";
            btnAchivements.Size = new Size(200, 40);
            btnAchivements.TabIndex = 6;
            btnAchivements.Text = "Quản lý thành tích";
            btnAchivements.UseVisualStyleBackColor = true;
            btnAchivements.Click += btnAchivements_Click;
            // 
            // btnEvents
            // 
            btnEvents.Dock = DockStyle.Top;
            btnEvents.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEvents.Location = new Point(0, 120);
            btnEvents.Name = "btnEvents";
            btnEvents.Size = new Size(200, 40);
            btnEvents.TabIndex = 5;
            btnEvents.Text = "Quản lý sự kiện";
            btnEvents.UseVisualStyleBackColor = true;
            btnEvents.Click += btnEventsManager_Click;
            // 
            // buttonUsers
            // 
            buttonUsers.Dock = DockStyle.Top;
            buttonUsers.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonUsers.Location = new Point(0, 80);
            buttonUsers.Name = "buttonUsers";
            buttonUsers.Size = new Size(200, 40);
            buttonUsers.TabIndex = 4;
            buttonUsers.Text = "Quản lý người dùng";
            buttonUsers.UseVisualStyleBackColor = true;
            buttonUsers.Click += buttonUsers_Click;
            // 
            // buttonDashboard
            // 
            buttonDashboard.Dock = DockStyle.Top;
            buttonDashboard.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonDashboard.Location = new Point(0, 40);
            buttonDashboard.Name = "buttonDashboard";
            buttonDashboard.Size = new Size(200, 40);
            buttonDashboard.TabIndex = 3;
            buttonDashboard.Text = "Bảng điều khiển";
            buttonDashboard.UseVisualStyleBackColor = true;
            // 
            // buttonHome
            // 
            buttonHome.Dock = DockStyle.Top;
            buttonHome.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonHome.Location = new Point(0, 0);
            buttonHome.Name = "buttonHome";
            buttonHome.Size = new Size(200, 40);
            buttonHome.TabIndex = 2;
            buttonHome.Text = "Trang chủ";
            buttonHome.UseVisualStyleBackColor = true;
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
        private Button buttonUsers;
        private Button buttonDashboard;
        private Button buttonHome;
        private Button btnFinances;
        private Button btnAchivements;
        private Button btnEvents;
    }
}