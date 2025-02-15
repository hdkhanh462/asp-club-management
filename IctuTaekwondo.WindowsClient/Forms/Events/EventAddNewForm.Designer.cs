namespace IctuTaekwondo.WindowsClient.Forms.Events
{
    partial class EventAddNewForm
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
            label7 = new Label();
            errorProvider1 = new ErrorProvider(components);
            eventCreateSchemaBindingSource = new BindingSource(components);
            tbName = new TextBox();
            tbEndDate = new TextBox();
            tbStartDate = new TextBox();
            tbMax = new TextBox();
            tbLocation = new TextBox();
            btnAddNew = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label8 = new Label();
            label6 = new Label();
            label5 = new Label();
            tbFee = new TextBox();
            label9 = new Label();
            panel1 = new Panel();
            tbDescription = new TextBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eventCreateSchemaBindingSource).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 237);
            label7.Name = "label7";
            label7.Size = new Size(38, 15);
            label7.TabIndex = 53;
            label7.Text = "Mô tả";
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = eventCreateSchemaBindingSource;
            // 
            // eventCreateSchemaBindingSource
            // 
            eventCreateSchemaBindingSource.DataSource = typeof(Shared.Schemas.Event.EventCreateSchema);
            // 
            // tbName
            // 
            tbName.DataBindings.Add(new Binding("Text", eventCreateSchemaBindingSource, "Name", true));
            tbName.Location = new Point(12, 88);
            tbName.Margin = new Padding(10);
            tbName.Name = "tbName";
            tbName.Size = new Size(180, 23);
            tbName.TabIndex = 40;
            // 
            // tbEndDate
            // 
            tbEndDate.DataBindings.Add(new Binding("Text", eventCreateSchemaBindingSource, "EndDate", true));
            tbEndDate.Location = new Point(212, 204);
            tbEndDate.Margin = new Padding(10);
            tbEndDate.Name = "tbEndDate";
            tbEndDate.Size = new Size(180, 23);
            tbEndDate.TabIndex = 41;
            // 
            // tbStartDate
            // 
            tbStartDate.DataBindings.Add(new Binding("Text", eventCreateSchemaBindingSource, "StartDate", true));
            tbStartDate.Location = new Point(12, 204);
            tbStartDate.Margin = new Padding(10);
            tbStartDate.Name = "tbStartDate";
            tbStartDate.Size = new Size(180, 23);
            tbStartDate.TabIndex = 43;
            // 
            // tbMax
            // 
            tbMax.DataBindings.Add(new Binding("Text", eventCreateSchemaBindingSource, "MaxParticipants", true));
            tbMax.Location = new Point(212, 146);
            tbMax.Margin = new Padding(10);
            tbMax.Name = "tbMax";
            tbMax.Size = new Size(180, 23);
            tbMax.TabIndex = 44;
            // 
            // tbLocation
            // 
            tbLocation.DataBindings.Add(new Binding("Text", eventCreateSchemaBindingSource, "Location", true));
            tbLocation.Location = new Point(12, 146);
            tbLocation.Margin = new Padding(10);
            tbLocation.Name = "tbLocation";
            tbLocation.Size = new Size(180, 23);
            tbLocation.TabIndex = 45;
            // 
            // btnAddNew
            // 
            btnAddNew.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddNew.Location = new Point(12, 330);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(75, 23);
            btnAddNew.TabIndex = 38;
            btnAddNew.Text = "Lưu";
            btnAddNew.UseVisualStyleBackColor = true;
            btnAddNew.Click += btnAddNew_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 179);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 52;
            label4.Text = "Ngày bắt đầu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(212, 121);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 51;
            label3.Text = "Giới hạn tham gia";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 121);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 54;
            label2.Text = "Địa điểm tổ chức";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(212, 179);
            label8.Name = "label8";
            label8.Size = new Size(81, 15);
            label8.TabIndex = 47;
            label8.Text = "Ngày kết thúc";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(212, 63);
            label6.Name = "label6";
            label6.Size = new Size(74, 15);
            label6.TabIndex = 48;
            label6.Text = "Phí tham gia";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 63);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 49;
            label5.Text = "Tên sự kiện";
            // 
            // tbFee
            // 
            tbFee.DataBindings.Add(new Binding("Text", eventCreateSchemaBindingSource, "Fee", true));
            tbFee.Location = new Point(212, 88);
            tbFee.Margin = new Padding(10);
            tbFee.Name = "tbFee";
            tbFee.Size = new Size(180, 23);
            tbFee.TabIndex = 39;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(158, 21);
            label9.TabIndex = 0;
            label9.Text = "THÊM MỚI SỰ KIỆN";
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(413, 50);
            panel1.TabIndex = 55;
            // 
            // tbDescription
            // 
            tbDescription.DataBindings.Add(new Binding("Text", eventCreateSchemaBindingSource, "Description", true));
            tbDescription.Location = new Point(12, 262);
            tbDescription.Margin = new Padding(10);
            tbDescription.Multiline = true;
            tbDescription.Name = "tbDescription";
            tbDescription.Size = new Size(380, 50);
            tbDescription.TabIndex = 42;
            // 
            // EventAddNewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 370);
            Controls.Add(label7);
            Controls.Add(tbName);
            Controls.Add(tbEndDate);
            Controls.Add(tbStartDate);
            Controls.Add(tbMax);
            Controls.Add(tbLocation);
            Controls.Add(btnAddNew);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(tbFee);
            Controls.Add(panel1);
            Controls.Add(tbDescription);
            Name = "EventAddNewForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EventAddNewForm";
            Load += EventAddNewForm_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)eventCreateSchemaBindingSource).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label7;
        private ErrorProvider errorProvider1;
        private TextBox tbName;
        private TextBox tbEndDate;
        private TextBox tbStartDate;
        private TextBox tbMax;
        private TextBox tbLocation;
        private Button btnDelete;
        private Button btnAddNew;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label8;
        private Label label6;
        private Label label5;
        private TextBox tbFee;
        private Panel panel1;
        private Label label9;
        private TextBox tbDescription;
        private BindingSource eventCreateSchemaBindingSource;
    }
}