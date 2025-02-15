namespace IctuTaekwondo.WindowsClient.Forms
{
    partial class DashboardForm
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
            lbTotal = new Label();
            label1 = new Label();
            panel2 = new Panel();
            lbIncome = new Label();
            label3 = new Label();
            panel3 = new Panel();
            lbExpense = new Label();
            label5 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lbTotal);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 70);
            panel1.TabIndex = 0;
            // 
            // lbTotal
            // 
            lbTotal.AutoSize = true;
            lbTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTotal.Location = new Point(6, 33);
            lbTotal.Margin = new Padding(6);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(29, 21);
            lbTotal.TabIndex = 0;
            lbTotal.Text = "0đ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 6);
            label1.Margin = new Padding(6);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 0;
            label1.Text = "Quỹ";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(lbIncome);
            panel2.Controls.Add(label3);
            panel2.Location = new Point(218, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 70);
            panel2.TabIndex = 0;
            // 
            // lbIncome
            // 
            lbIncome.AutoSize = true;
            lbIncome.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbIncome.Location = new Point(6, 33);
            lbIncome.Margin = new Padding(6);
            lbIncome.Name = "lbIncome";
            lbIncome.Size = new Size(29, 21);
            lbIncome.TabIndex = 0;
            lbIncome.Text = "0đ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 6);
            label3.Margin = new Padding(6);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 0;
            label3.Text = "Thu nhập";
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(lbExpense);
            panel3.Controls.Add(label5);
            panel3.Location = new Point(424, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(200, 70);
            panel3.TabIndex = 0;
            // 
            // lbExpense
            // 
            lbExpense.AutoSize = true;
            lbExpense.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbExpense.Location = new Point(6, 33);
            lbExpense.Margin = new Padding(6);
            lbExpense.Name = "lbExpense";
            lbExpense.Size = new Size(29, 21);
            lbExpense.TabIndex = 0;
            lbExpense.Text = "0đ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 6);
            label5.Margin = new Padding(6);
            label5.Name = "label5";
            label5.Size = new Size(48, 15);
            label5.TabIndex = 0;
            label5.Text = "Chi tiêu";
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 450);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "DashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DashboardForm";
            Load += DashboardForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lbTotal;
        private Label label1;
        private Panel panel2;
        private Label lbIncome;
        private Label label3;
        private Panel panel3;
        private Label lbExpense;
        private Label label5;
    }
}