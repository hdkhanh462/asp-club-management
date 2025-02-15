namespace IctuTaekwondo.WindowsClient.Forms.Events
{
    partial class EventsDetailForm
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
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label8 = new Label();
            label6 = new Label();
            label5 = new Label();
            label1 = new Label();
            tbFee = new TextBox();
            eventUpdateSchemaBindingSource = new BindingSource(components);
            tbName = new TextBox();
            tbEndDate = new TextBox();
            tbStartDate = new TextBox();
            tbMax = new TextBox();
            tbLocation = new TextBox();
            tbId = new TextBox();
            btnDelete = new Button();
            btnSave = new Button();
            errorProvider1 = new ErrorProvider(components);
            tbDescription = new TextBox();
            label7 = new Label();
            dataGridView1 = new DataGridView();
            btnUnregister = new Button();
            btnRegister = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eventUpdateSchemaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(880, 50);
            panel1.TabIndex = 36;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(165, 21);
            label9.TabIndex = 0;
            label9.Text = "THÔNG TIN SỰ KIỆN";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 242);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 34;
            label4.Text = "Ngày bắt đầu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 184);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 33;
            label3.Text = "Giới hạn tham gia";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 126);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 35;
            label2.Text = "Địa điểm tổ chức";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(212, 242);
            label8.Name = "label8";
            label8.Size = new Size(81, 15);
            label8.TabIndex = 28;
            label8.Text = "Ngày kết thúc";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(212, 126);
            label6.Name = "label6";
            label6.Size = new Size(74, 15);
            label6.TabIndex = 30;
            label6.Text = "Phí tham gia";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 68);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 31;
            label5.Text = "Tên sự kiện";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 68);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 32;
            label1.Text = "Id";
            // 
            // tbFee
            // 
            tbFee.DataBindings.Add(new Binding("Text", eventUpdateSchemaBindingSource, "Fee", true));
            tbFee.Location = new Point(212, 151);
            tbFee.Margin = new Padding(10);
            tbFee.Name = "tbFee";
            tbFee.Size = new Size(180, 23);
            tbFee.TabIndex = 20;
            // 
            // eventUpdateSchemaBindingSource
            // 
            eventUpdateSchemaBindingSource.DataSource = typeof(Shared.Schemas.Event.EventUpdateSchema);
            // 
            // tbName
            // 
            tbName.DataBindings.Add(new Binding("Text", eventUpdateSchemaBindingSource, "Name", true));
            tbName.Location = new Point(212, 93);
            tbName.Margin = new Padding(10);
            tbName.Name = "tbName";
            tbName.Size = new Size(180, 23);
            tbName.TabIndex = 21;
            // 
            // tbEndDate
            // 
            tbEndDate.DataBindings.Add(new Binding("Text", eventUpdateSchemaBindingSource, "EndDate", true));
            tbEndDate.Location = new Point(212, 267);
            tbEndDate.Margin = new Padding(10);
            tbEndDate.Name = "tbEndDate";
            tbEndDate.Size = new Size(180, 23);
            tbEndDate.TabIndex = 22;
            // 
            // tbStartDate
            // 
            tbStartDate.DataBindings.Add(new Binding("Text", eventUpdateSchemaBindingSource, "StartDate", true));
            tbStartDate.Location = new Point(12, 267);
            tbStartDate.Margin = new Padding(10);
            tbStartDate.Name = "tbStartDate";
            tbStartDate.Size = new Size(180, 23);
            tbStartDate.TabIndex = 24;
            // 
            // tbMax
            // 
            tbMax.DataBindings.Add(new Binding("Text", eventUpdateSchemaBindingSource, "MaxParticipants", true));
            tbMax.Location = new Point(12, 209);
            tbMax.Margin = new Padding(10);
            tbMax.Name = "tbMax";
            tbMax.Size = new Size(180, 23);
            tbMax.TabIndex = 25;
            // 
            // tbLocation
            // 
            tbLocation.DataBindings.Add(new Binding("Text", eventUpdateSchemaBindingSource, "Location", true));
            tbLocation.Location = new Point(12, 151);
            tbLocation.Margin = new Padding(10);
            tbLocation.Name = "tbLocation";
            tbLocation.Size = new Size(180, 23);
            tbLocation.TabIndex = 26;
            // 
            // tbId
            // 
            tbId.DataBindings.Add(new Binding("Text", eventUpdateSchemaBindingSource, "Id", true));
            tbId.Enabled = false;
            tbId.Location = new Point(12, 93);
            tbId.Margin = new Padding(10);
            tbId.Name = "tbId";
            tbId.Size = new Size(180, 23);
            tbId.TabIndex = 27;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Enabled = false;
            btnDelete.Location = new Point(93, 392);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 18;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Enabled = false;
            btnSave.Location = new Point(12, 392);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 19;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = eventUpdateSchemaBindingSource;
            // 
            // tbDescription
            // 
            tbDescription.DataBindings.Add(new Binding("Text", eventUpdateSchemaBindingSource, "Description", true));
            tbDescription.Location = new Point(12, 325);
            tbDescription.Margin = new Padding(10);
            tbDescription.Multiline = true;
            tbDescription.Name = "tbDescription";
            tbDescription.Size = new Size(380, 50);
            tbDescription.TabIndex = 24;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 300);
            label7.Name = "label7";
            label7.Size = new Size(38, 15);
            label7.TabIndex = 34;
            label7.Text = "Mô tả";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Enabled = false;
            dataGridView1.Location = new Point(423, 93);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(445, 282);
            dataGridView1.TabIndex = 37;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnUnregister
            // 
            btnUnregister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUnregister.Enabled = false;
            btnUnregister.Location = new Point(773, 392);
            btnUnregister.Name = "btnUnregister";
            btnUnregister.Size = new Size(95, 23);
            btnUnregister.TabIndex = 18;
            btnUnregister.Text = "Huỷ đăng ký";
            btnUnregister.UseVisualStyleBackColor = true;
            btnUnregister.Click += btnUnregister_Click;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRegister.Enabled = false;
            btnRegister.Location = new Point(672, 392);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(95, 23);
            btnRegister.TabIndex = 18;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // EventsDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 421);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(tbFee);
            Controls.Add(tbName);
            Controls.Add(tbDescription);
            Controls.Add(tbEndDate);
            Controls.Add(tbStartDate);
            Controls.Add(tbMax);
            Controls.Add(tbLocation);
            Controls.Add(tbId);
            Controls.Add(btnRegister);
            Controls.Add(btnUnregister);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Name = "EventsDetailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EvensDetailForm";
            Load += EvensDetailForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)eventUpdateSchemaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label9;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label8;
        private Label label6;
        private Label label5;
        private Label label1;
        private TextBox tbFee;
        private TextBox tbName;
        private TextBox tbEndDate;
        private TextBox tbStartDate;
        private TextBox tbMax;
        private TextBox tbLocation;
        private TextBox tbId;
        private Button btnDelete;
        private Button btnSave;
        private BindingSource eventUpdateSchemaBindingSource;
        private ErrorProvider errorProvider1;
        private Label label7;
        private TextBox tbDescription;
        private DataGridView dataGridView1;
        private Button btnUnregister;
        private Button btnRegister;
    }
}