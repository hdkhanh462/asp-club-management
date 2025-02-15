namespace IctuTaekwondo.WindowsClient.Forms.Achievements
{
    partial class AchievementAddNewForm
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
            label6 = new Label();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            tbUserId = new TextBox();
            achievementCreateSchemaBindingSource = new BindingSource(components);
            tbName = new TextBox();
            tbEventId = new TextBox();
            tbDateAchieved = new TextBox();
            btnAddNew = new Button();
            tbDescription = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)achievementCreateSchemaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(416, 50);
            panel1.TabIndex = 52;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(192, 21);
            label9.TabIndex = 0;
            label9.Text = "THÊM MỚI THÀNH TÍCH";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 187);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 50;
            label6.Text = "Người dùng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(212, 187);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 48;
            label2.Text = "Sự kiện";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 129);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 51;
            label4.Text = "Ngày đạt";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(212, 129);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 49;
            label3.Text = "Mô tả";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 68);
            label5.Name = "label5";
            label5.Size = new Size(82, 15);
            label5.TabIndex = 46;
            label5.Text = "Tên thành tích";
            // 
            // tbUserId
            // 
            tbUserId.DataBindings.Add(new Binding("Text", achievementCreateSchemaBindingSource, "UserId", true));
            tbUserId.Location = new Point(12, 212);
            tbUserId.Margin = new Padding(10);
            tbUserId.Name = "tbUserId";
            tbUserId.Size = new Size(180, 23);
            tbUserId.TabIndex = 41;
            // 
            // achievementCreateSchemaBindingSource
            // 
            achievementCreateSchemaBindingSource.DataSource = typeof(Shared.Schemas.Achievement.AchievementCreateSchema);
            // 
            // tbName
            // 
            tbName.DataBindings.Add(new Binding("Text", achievementCreateSchemaBindingSource, "Name", true));
            tbName.Location = new Point(12, 93);
            tbName.Margin = new Padding(10);
            tbName.Name = "tbName";
            tbName.Size = new Size(180, 23);
            tbName.TabIndex = 40;
            // 
            // tbEventId
            // 
            tbEventId.DataBindings.Add(new Binding("Text", achievementCreateSchemaBindingSource, "EventId", true));
            tbEventId.Location = new Point(212, 212);
            tbEventId.Margin = new Padding(10);
            tbEventId.Name = "tbEventId";
            tbEventId.Size = new Size(180, 23);
            tbEventId.TabIndex = 43;
            // 
            // tbDateAchieved
            // 
            tbDateAchieved.DataBindings.Add(new Binding("Text", achievementCreateSchemaBindingSource, "DateAchieved", true));
            tbDateAchieved.Location = new Point(12, 154);
            tbDateAchieved.Margin = new Padding(10);
            tbDateAchieved.Name = "tbDateAchieved";
            tbDateAchieved.Size = new Size(180, 23);
            tbDateAchieved.TabIndex = 42;
            // 
            // btnAddNew
            // 
            btnAddNew.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddNew.Location = new Point(12, 252);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(75, 23);
            btnAddNew.TabIndex = 39;
            btnAddNew.Text = "Thêm mới";
            btnAddNew.UseVisualStyleBackColor = true;
            btnAddNew.Click += btnAddNew_Click;
            // 
            // tbDescription
            // 
            tbDescription.DataBindings.Add(new Binding("Text", achievementCreateSchemaBindingSource, "Description", true));
            tbDescription.Location = new Point(212, 154);
            tbDescription.Margin = new Padding(10);
            tbDescription.Name = "tbDescription";
            tbDescription.Size = new Size(180, 23);
            tbDescription.TabIndex = 44;
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = achievementCreateSchemaBindingSource;
            // 
            // AchievementAddNewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(416, 287);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(tbUserId);
            Controls.Add(tbName);
            Controls.Add(tbEventId);
            Controls.Add(tbDateAchieved);
            Controls.Add(btnAddNew);
            Controls.Add(tbDescription);
            Name = "AchievementAddNewForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AchievementAddNewForm";
            Load += AchievementAddNewForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)achievementCreateSchemaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label9;
        private Label label6;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label5;
        private TextBox tbUserId;
        private TextBox tbName;
        private TextBox tbEventId;
        private TextBox tbDateAchieved;
        private Button btnAddNew;
        private TextBox tbDescription;
        private ErrorProvider errorProvider1;
        private BindingSource achievementCreateSchemaBindingSource;
    }
}