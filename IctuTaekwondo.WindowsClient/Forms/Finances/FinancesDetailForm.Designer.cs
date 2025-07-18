﻿namespace IctuTaekwondo.WindowsClient.Forms.Finances
{
    partial class FinancesDetailForm
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
            label5 = new Label();
            label1 = new Label();
            tbCategory = new TextBox();
            financeUpdateSchemaBindingSource = new BindingSource(components);
            tbAmount = new TextBox();
            tbId = new TextBox();
            btnDelete = new Button();
            btnSave = new Button();
            tbDescription = new TextBox();
            tbTransactionDate = new TextBox();
            label2 = new Label();
            label6 = new Label();
            cbTransactionType = new ComboBox();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)financeUpdateSchemaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(415, 50);
            panel1.TabIndex = 36;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 15);
            label9.Name = "label9";
            label9.Size = new Size(184, 21);
            label9.TabIndex = 0;
            label9.Text = "THÔNG TIN GIAO DỊCH";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 126);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 34;
            label4.Text = "Danh mục";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(212, 126);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 33;
            label3.Text = "Số tiền";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 68);
            label5.Name = "label5";
            label5.Size = new Size(81, 15);
            label5.TabIndex = 31;
            label5.Text = "Loại giao dịch";
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
            // tbCategory
            // 
            tbCategory.DataBindings.Add(new Binding("Text", financeUpdateSchemaBindingSource, "Category", true));
            tbCategory.Location = new Point(12, 151);
            tbCategory.Margin = new Padding(10);
            tbCategory.Name = "tbCategory";
            tbCategory.Size = new Size(180, 23);
            tbCategory.TabIndex = 24;
            // 
            // financeUpdateSchemaBindingSource
            // 
            financeUpdateSchemaBindingSource.DataSource = typeof(Shared.Schemas.Finance.FinanceUpdateSchema);
            // 
            // tbAmount
            // 
            tbAmount.DataBindings.Add(new Binding("Text", financeUpdateSchemaBindingSource, "Amount", true));
            tbAmount.Location = new Point(212, 151);
            tbAmount.Margin = new Padding(10);
            tbAmount.Name = "tbAmount";
            tbAmount.Size = new Size(180, 23);
            tbAmount.TabIndex = 25;
            // 
            // tbId
            // 
            tbId.DataBindings.Add(new Binding("Text", financeUpdateSchemaBindingSource, "Id", true));
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
            btnDelete.Location = new Point(93, 245);
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
            btnSave.Location = new Point(12, 245);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 19;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // tbDescription
            // 
            tbDescription.DataBindings.Add(new Binding("Text", financeUpdateSchemaBindingSource, "Description", true));
            tbDescription.Location = new Point(212, 209);
            tbDescription.Margin = new Padding(10);
            tbDescription.Name = "tbDescription";
            tbDescription.Size = new Size(180, 23);
            tbDescription.TabIndex = 25;
            // 
            // tbTransactionDate
            // 
            tbTransactionDate.DataBindings.Add(new Binding("Text", financeUpdateSchemaBindingSource, "TransactionDate", true));
            tbTransactionDate.Location = new Point(12, 209);
            tbTransactionDate.Margin = new Padding(10);
            tbTransactionDate.Name = "tbTransactionDate";
            tbTransactionDate.Size = new Size(180, 23);
            tbTransactionDate.TabIndex = 24;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(212, 184);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 33;
            label2.Text = "Mô tả";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 184);
            label6.Name = "label6";
            label6.Size = new Size(87, 15);
            label6.TabIndex = 34;
            label6.Text = "Ngày giao dịch";
            // 
            // cbTransactionType
            // 
            cbTransactionType.DataBindings.Add(new Binding("SelectedValue", financeUpdateSchemaBindingSource, "Type", true));
            cbTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTransactionType.FormattingEnabled = true;
            cbTransactionType.Location = new Point(212, 93);
            cbTransactionType.Name = "cbTransactionType";
            cbTransactionType.Size = new Size(180, 23);
            cbTransactionType.TabIndex = 37;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            errorProvider1.DataSource = financeUpdateSchemaBindingSource;
            // 
            // FinancesDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(415, 274);
            Controls.Add(cbTransactionType);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(tbTransactionDate);
            Controls.Add(tbDescription);
            Controls.Add(tbCategory);
            Controls.Add(tbAmount);
            Controls.Add(tbId);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Name = "FinancesDetailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DetailForm";
            Load += EvensDetailForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)financeUpdateSchemaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label9;
        private Label label4;
        private Label label3;
        private Label label5;
        private Label label1;
        private TextBox tbCategory;
        private TextBox tbAmount;
        private TextBox tbId;
        private Button btnDelete;
        private Button btnSave;
        private TextBox tbDescription;
        private TextBox tbTransactionDate;
        private Label label2;
        private Label label6;
        private BindingSource financeUpdateSchemaBindingSource;
        private ComboBox cbTransactionType;
        private ErrorProvider errorProvider1;
    }
}