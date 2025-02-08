namespace IctuTaekwondo.WindowsClient.Forms
{
    partial class UsersForm
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
            tabControl1 = new TabControl();
            tabPageList = new TabPage();
            dataGridView1 = new DataGridView();
            btnSearch = new Button();
            tbSearch = new TextBox();
            tabPageDetail = new TabPage();
            btnRegister = new Button();
            btnCancel = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            tbJoinDate = new TextBox();
            tbCurrentRank = new TextBox();
            tbAddress = new TextBox();
            tbDateOfBirth = new TextBox();
            tbGender = new TextBox();
            tbPhoneNumber = new TextBox();
            tbFullName = new TextBox();
            tbId = new TextBox();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPageDetail.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 50);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(189, 21);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ NGƯỜI DÙNG";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageList);
            tabControl1.Controls.Add(tabPageDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 50);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 400);
            tabControl1.TabIndex = 2;
            // 
            // tabPageList
            // 
            tabPageList.Controls.Add(dataGridView1);
            tabPageList.Controls.Add(btnSearch);
            tabPageList.Controls.Add(tbSearch);
            tabPageList.Location = new Point(4, 24);
            tabPageList.Name = "tabPageList";
            tabPageList.Padding = new Padding(3);
            tabPageList.Size = new Size(792, 372);
            tabPageList.TabIndex = 0;
            tabPageList.Text = "Danh sách người dùng";
            tabPageList.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(8, 35);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(776, 329);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(709, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // tbSearch
            // 
            tbSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSearch.Location = new Point(8, 6);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(695, 23);
            tbSearch.TabIndex = 0;
            // 
            // tabPageDetail
            // 
            tabPageDetail.Controls.Add(btnRegister);
            tabPageDetail.Controls.Add(btnCancel);
            tabPageDetail.Controls.Add(btnDelete);
            tabPageDetail.Controls.Add(btnSave);
            tabPageDetail.Controls.Add(tbJoinDate);
            tabPageDetail.Controls.Add(tbCurrentRank);
            tabPageDetail.Controls.Add(tbAddress);
            tabPageDetail.Controls.Add(tbDateOfBirth);
            tabPageDetail.Controls.Add(tbGender);
            tabPageDetail.Controls.Add(tbPhoneNumber);
            tabPageDetail.Controls.Add(tbFullName);
            tabPageDetail.Controls.Add(tbId);
            tabPageDetail.Location = new Point(4, 24);
            tabPageDetail.Name = "tabPageDetail";
            tabPageDetail.Padding = new Padding(3);
            tabPageDetail.Size = new Size(792, 372);
            tabPageDetail.TabIndex = 1;
            tabPageDetail.Text = "Thông tin người dùng";
            tabPageDetail.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRegister.Location = new Point(664, 21);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(75, 23);
            btnRegister.TabIndex = 2;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Location = new Point(502, 21);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Huỷ bỏ";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(583, 21);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(421, 21);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 2;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // tbJoinDate
            // 
            tbJoinDate.Location = new Point(228, 64);
            tbJoinDate.Margin = new Padding(10);
            tbJoinDate.Name = "tbJoinDate";
            tbJoinDate.Size = new Size(180, 23);
            tbJoinDate.TabIndex = 0;
            // 
            // tbCurrentRank
            // 
            tbCurrentRank.Location = new Point(228, 21);
            tbCurrentRank.Margin = new Padding(10);
            tbCurrentRank.Name = "tbCurrentRank";
            tbCurrentRank.Size = new Size(180, 23);
            tbCurrentRank.TabIndex = 0;
            // 
            // tbAddress
            // 
            tbAddress.Location = new Point(228, 150);
            tbAddress.Margin = new Padding(10);
            tbAddress.Name = "tbAddress";
            tbAddress.Size = new Size(180, 23);
            tbAddress.TabIndex = 0;
            // 
            // tbDateOfBirth
            // 
            tbDateOfBirth.Location = new Point(228, 107);
            tbDateOfBirth.Margin = new Padding(10);
            tbDateOfBirth.Name = "tbDateOfBirth";
            tbDateOfBirth.Size = new Size(180, 23);
            tbDateOfBirth.TabIndex = 0;
            // 
            // tbGender
            // 
            tbGender.Location = new Point(28, 150);
            tbGender.Margin = new Padding(10);
            tbGender.Name = "tbGender";
            tbGender.Size = new Size(180, 23);
            tbGender.TabIndex = 0;
            // 
            // tbPhoneNumber
            // 
            tbPhoneNumber.Location = new Point(28, 107);
            tbPhoneNumber.Margin = new Padding(10);
            tbPhoneNumber.Name = "tbPhoneNumber";
            tbPhoneNumber.Size = new Size(180, 23);
            tbPhoneNumber.TabIndex = 0;
            // 
            // tbFullName
            // 
            tbFullName.Location = new Point(28, 64);
            tbFullName.Margin = new Padding(10);
            tbFullName.Name = "tbFullName";
            tbFullName.Size = new Size(180, 23);
            tbFullName.TabIndex = 0;
            // 
            // tbId
            // 
            tbId.Enabled = false;
            tbId.Location = new Point(28, 21);
            tbId.Margin = new Padding(10);
            tbId.Name = "tbId";
            tbId.Size = new Size(180, 23);
            tbId.TabIndex = 0;
            // 
            // UsersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            Name = "UsersForm";
            Text = "UsersForm";
            Load += UsersForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPageList.ResumeLayout(false);
            tabPageList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPageDetail.ResumeLayout(false);
            tabPageDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabPageList;
        private Button btnSearch;
        private TextBox tbSearch;
        private TabPage tabPageDetail;
        private DataGridView dataGridView1;
        private TextBox tbDateOfBirth;
        private TextBox tbGender;
        private TextBox tbPhoneNumber;
        private TextBox tbFullName;
        private TextBox tbId;
        private TextBox tbAddress;
        private TextBox tbCurrentRank;
        private TextBox tbJoinDate;
        private Button btnSave;
        private Button button3;
        private Button btnCancel;
        private Button btnRegister;
        private Button btnDelete;
    }
}