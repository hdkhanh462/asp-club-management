namespace IctuTaekwondo.WindowsClient.Forms.Finances
{
    partial class FinanceAddNewForm
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
            cbTransactionType = new ComboBox();
            financeCreateSchemaBindingSource = new BindingSource(components);
            errorProvider1 = new ErrorProvider(components);
            panel1 = new Panel();
            label9 = new Label();
            label4 = new Label();
            label5 = new Label();
            tbCategory = new TextBox();
            label6 = new Label();
            label2 = new Label();
            label3 = new Label();
            tbTransactionDate = new TextBox();
            tbDescription = new TextBox();
            tbAmount = new TextBox();
            btnAddNew = new Button();
            ((System.ComponentModel.ISupportInitialize)financeCreateSchemaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cbTransactionType
            // 
            cbTransactionType.DataBindings.Add(new Binding("SelectedValue", financeCreateSchemaBindingSource, "Type", true));
            cbTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTransactionType.FormattingEnabled = true;
            cbTransactionType.Location = new Point(12, 89);
            cbTransactionType.Name = "cbTransactionType";
            cbTransactionType.Size = new Size(180, 23);
            cbTransactionType.TabIndex = 52;
            // 
            // financeCreateSchemaBindingSource
            // 
            financeCreateSchemaBindingSource.DataSource = typeof(Shared.Schemas.Finance.FinanceCreateSchema);
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = financeCreateSchemaBindingSource;
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(417, 50);
            panel1.TabIndex = 51;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(177, 21);
            label9.TabIndex = 0;
            label9.Text = "THÊM MỚI GIAO DỊCH";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 123);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 49;
            label4.Text = "Danh mục";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 64);
            label5.Name = "label5";
            label5.Size = new Size(81, 15);
            label5.TabIndex = 45;
            label5.Text = "Loại giao dịch";
            // 
            // tbCategory
            // 
            tbCategory.DataBindings.Add(new Binding("Text", financeCreateSchemaBindingSource, "Category", true));
            tbCategory.Location = new Point(12, 148);
            tbCategory.Margin = new Padding(10);
            tbCategory.Name = "tbCategory";
            tbCategory.Size = new Size(180, 23);
            tbCategory.TabIndex = 40;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 181);
            label6.Name = "label6";
            label6.Size = new Size(87, 15);
            label6.TabIndex = 50;
            label6.Text = "Ngày giao dịch";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(212, 181);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 47;
            label2.Text = "Mô tả";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(212, 123);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 48;
            label3.Text = "Số tiền";
            // 
            // tbTransactionDate
            // 
            tbTransactionDate.DataBindings.Add(new Binding("Text", financeCreateSchemaBindingSource, "TransactionDate", true));
            tbTransactionDate.Location = new Point(12, 206);
            tbTransactionDate.Margin = new Padding(10);
            tbTransactionDate.Name = "tbTransactionDate";
            tbTransactionDate.Size = new Size(180, 23);
            tbTransactionDate.TabIndex = 41;
            // 
            // tbDescription
            // 
            tbDescription.DataBindings.Add(new Binding("Text", financeCreateSchemaBindingSource, "Description", true));
            tbDescription.Location = new Point(212, 206);
            tbDescription.Margin = new Padding(10);
            tbDescription.Name = "tbDescription";
            tbDescription.Size = new Size(180, 23);
            tbDescription.TabIndex = 42;
            // 
            // tbAmount
            // 
            tbAmount.DataBindings.Add(new Binding("Text", financeCreateSchemaBindingSource, "Amount", true));
            tbAmount.Location = new Point(212, 148);
            tbAmount.Margin = new Padding(10);
            tbAmount.Name = "tbAmount";
            tbAmount.Size = new Size(180, 23);
            tbAmount.TabIndex = 43;
            // 
            // btnAddNew
            // 
            btnAddNew.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddNew.Location = new Point(12, 246);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(75, 23);
            btnAddNew.TabIndex = 39;
            btnAddNew.Text = "Thêm mới";
            btnAddNew.UseVisualStyleBackColor = true;
            btnAddNew.Click += btnAddNew_Click;
            // 
            // FinanceAddNewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(417, 281);
            Controls.Add(cbTransactionType);
            Controls.Add(panel1);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(tbCategory);
            Controls.Add(label6);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(tbTransactionDate);
            Controls.Add(tbDescription);
            Controls.Add(tbAmount);
            Controls.Add(btnAddNew);
            Name = "FinanceAddNewForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FinanceAddNewForm";
            Load += FinanceAddNewForm_Load;
            ((System.ComponentModel.ISupportInitialize)financeCreateSchemaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbTransactionType;
        private ErrorProvider errorProvider1;
        private Panel panel1;
        private Label label9;
        private Label label4;
        private Label label5;
        private TextBox tbCategory;
        private Label label6;
        private Label label2;
        private Label label3;
        private TextBox tbTransactionDate;
        private TextBox tbDescription;
        private TextBox tbAmount;
        private Button btnDelete;
        private Button btnAddNew;
        private BindingSource financeCreateSchemaBindingSource;
    }
}