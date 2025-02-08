namespace IctuTaekwondo.WindowsClient.Views
{
    partial class UsersView
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
            materialTabControl1 = new ReaLTaiizor.Controls.MaterialTabControl();
            tabPageList = new TabPage();
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            btnSearch = new ReaLTaiizor.Controls.MaterialButton();
            btnRegister = new ReaLTaiizor.Controls.MaterialButton();
            materialTextBoxEdit1 = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            tabPageDetail = new TabPage();
            materialTextBoxEdit4 = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            materialTextBoxEdit3 = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            materialTextBoxEdit2 = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            materialComboBox1 = new ReaLTaiizor.Controls.MaterialComboBox();
            materialComboBox2 = new ReaLTaiizor.Controls.MaterialComboBox();
            materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            materialButton2 = new ReaLTaiizor.Controls.MaterialButton();
            materialTabControl1.SuspendLayout();
            tabPageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            tabPageDetail.SuspendLayout();
            SuspendLayout();
            // 
            // materialTabControl1
            // 
            materialTabControl1.Controls.Add(tabPageList);
            materialTabControl1.Controls.Add(tabPageDetail);
            materialTabControl1.Depth = 0;
            materialTabControl1.Dock = DockStyle.Fill;
            materialTabControl1.Location = new Point(0, 0);
            materialTabControl1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.Size = new Size(800, 450);
            materialTabControl1.TabIndex = 0;
            // 
            // tabPageList
            // 
            tabPageList.Controls.Add(dataGridView1);
            tabPageList.Controls.Add(panel1);
            tabPageList.Location = new Point(4, 24);
            tabPageList.Name = "tabPageList";
            tabPageList.Padding = new Padding(3);
            tabPageList.Size = new Size(792, 422);
            tabPageList.TabIndex = 0;
            tabPageList.Text = "tabPageList";
            tabPageList.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 78);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(786, 341);
            dataGridView1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnRegister);
            panel1.Controls.Add(materialTextBoxEdit1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(786, 75);
            panel1.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSearch.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnSearch.Depth = 0;
            btnSearch.HighEmphasis = true;
            btnSearch.Icon = null;
            btnSearch.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnSearch.Location = new Point(595, 20);
            btnSearch.Margin = new Padding(0, 0, 10, 0);
            btnSearch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnSearch.Name = "btnSearch";
            btnSearch.NoAccentTextColor = Color.Empty;
            btnSearch.Size = new Size(86, 36);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            btnSearch.UseAccentColor = false;
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            btnRegister.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRegister.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRegister.Depth = 0;
            btnRegister.HighEmphasis = true;
            btnRegister.Icon = null;
            btnRegister.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRegister.Location = new Point(688, 20);
            btnRegister.Margin = new Padding(0, 0, 10, 0);
            btnRegister.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRegister.Name = "btnRegister";
            btnRegister.NoAccentTextColor = Color.Empty;
            btnRegister.Size = new Size(83, 36);
            btnRegister.TabIndex = 1;
            btnRegister.Text = "Đăng ký";
            btnRegister.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            btnRegister.UseAccentColor = false;
            btnRegister.UseVisualStyleBackColor = true;
            // 
            // materialTextBoxEdit1
            // 
            materialTextBoxEdit1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialTextBoxEdit1.AnimateReadOnly = false;
            materialTextBoxEdit1.AutoCompleteMode = AutoCompleteMode.None;
            materialTextBoxEdit1.AutoCompleteSource = AutoCompleteSource.None;
            materialTextBoxEdit1.BackgroundImageLayout = ImageLayout.None;
            materialTextBoxEdit1.CharacterCasing = CharacterCasing.Normal;
            materialTextBoxEdit1.Depth = 0;
            materialTextBoxEdit1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBoxEdit1.HideSelection = true;
            materialTextBoxEdit1.LeadingIcon = null;
            materialTextBoxEdit1.Location = new Point(12, 14);
            materialTextBoxEdit1.Margin = new Padding(10);
            materialTextBoxEdit1.MaxLength = 32767;
            materialTextBoxEdit1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialTextBoxEdit1.Name = "materialTextBoxEdit1";
            materialTextBoxEdit1.PasswordChar = '\0';
            materialTextBoxEdit1.PrefixSuffixText = null;
            materialTextBoxEdit1.ReadOnly = false;
            materialTextBoxEdit1.RightToLeft = RightToLeft.No;
            materialTextBoxEdit1.SelectedText = "";
            materialTextBoxEdit1.SelectionLength = 0;
            materialTextBoxEdit1.SelectionStart = 0;
            materialTextBoxEdit1.ShortcutsEnabled = true;
            materialTextBoxEdit1.Size = new Size(573, 48);
            materialTextBoxEdit1.TabIndex = 0;
            materialTextBoxEdit1.TabStop = false;
            materialTextBoxEdit1.Text = "Tìm kiếm";
            materialTextBoxEdit1.TextAlign = HorizontalAlignment.Left;
            materialTextBoxEdit1.TrailingIcon = null;
            materialTextBoxEdit1.UseSystemPasswordChar = false;
            // 
            // tabPageDetail
            // 
            tabPageDetail.Controls.Add(materialButton2);
            tabPageDetail.Controls.Add(materialButton1);
            tabPageDetail.Controls.Add(materialComboBox2);
            tabPageDetail.Controls.Add(materialComboBox1);
            tabPageDetail.Controls.Add(materialTextBoxEdit4);
            tabPageDetail.Controls.Add(materialTextBoxEdit3);
            tabPageDetail.Controls.Add(materialTextBoxEdit2);
            tabPageDetail.Location = new Point(4, 24);
            tabPageDetail.Name = "tabPageDetail";
            tabPageDetail.Padding = new Padding(3);
            tabPageDetail.Size = new Size(792, 422);
            tabPageDetail.TabIndex = 1;
            tabPageDetail.Text = "tabPageDetail";
            tabPageDetail.UseVisualStyleBackColor = true;
            // 
            // materialTextBoxEdit4
            // 
            materialTextBoxEdit4.AnimateReadOnly = false;
            materialTextBoxEdit4.AutoCompleteMode = AutoCompleteMode.None;
            materialTextBoxEdit4.AutoCompleteSource = AutoCompleteSource.None;
            materialTextBoxEdit4.BackgroundImageLayout = ImageLayout.None;
            materialTextBoxEdit4.CharacterCasing = CharacterCasing.Normal;
            materialTextBoxEdit4.Depth = 0;
            materialTextBoxEdit4.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBoxEdit4.HideSelection = true;
            materialTextBoxEdit4.Hint = "Địa chỉ";
            materialTextBoxEdit4.LeadingIcon = null;
            materialTextBoxEdit4.Location = new Point(15, 69);
            materialTextBoxEdit4.Margin = new Padding(10);
            materialTextBoxEdit4.MaxLength = 32767;
            materialTextBoxEdit4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialTextBoxEdit4.Name = "materialTextBoxEdit4";
            materialTextBoxEdit4.PasswordChar = '\0';
            materialTextBoxEdit4.PrefixSuffixText = null;
            materialTextBoxEdit4.ReadOnly = false;
            materialTextBoxEdit4.RightToLeft = RightToLeft.No;
            materialTextBoxEdit4.SelectedText = "";
            materialTextBoxEdit4.SelectionLength = 0;
            materialTextBoxEdit4.SelectionStart = 0;
            materialTextBoxEdit4.ShortcutsEnabled = true;
            materialTextBoxEdit4.Size = new Size(250, 36);
            materialTextBoxEdit4.TabIndex = 0;
            materialTextBoxEdit4.TabStop = false;
            materialTextBoxEdit4.TextAlign = HorizontalAlignment.Left;
            materialTextBoxEdit4.TrailingIcon = null;
            materialTextBoxEdit4.UseAccent = false;
            materialTextBoxEdit4.UseSystemPasswordChar = false;
            materialTextBoxEdit4.UseTallSize = false;
            // 
            // materialTextBoxEdit3
            // 
            materialTextBoxEdit3.AnimateReadOnly = false;
            materialTextBoxEdit3.AutoCompleteMode = AutoCompleteMode.None;
            materialTextBoxEdit3.AutoCompleteSource = AutoCompleteSource.None;
            materialTextBoxEdit3.BackgroundImageLayout = ImageLayout.None;
            materialTextBoxEdit3.CharacterCasing = CharacterCasing.Normal;
            materialTextBoxEdit3.Depth = 0;
            materialTextBoxEdit3.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBoxEdit3.HideSelection = true;
            materialTextBoxEdit3.Hint = "Số điện thoại";
            materialTextBoxEdit3.LeadingIcon = null;
            materialTextBoxEdit3.Location = new Point(285, 13);
            materialTextBoxEdit3.Margin = new Padding(10);
            materialTextBoxEdit3.MaxLength = 32767;
            materialTextBoxEdit3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialTextBoxEdit3.Name = "materialTextBoxEdit3";
            materialTextBoxEdit3.PasswordChar = '\0';
            materialTextBoxEdit3.PrefixSuffixText = null;
            materialTextBoxEdit3.ReadOnly = false;
            materialTextBoxEdit3.RightToLeft = RightToLeft.No;
            materialTextBoxEdit3.SelectedText = "";
            materialTextBoxEdit3.SelectionLength = 0;
            materialTextBoxEdit3.SelectionStart = 0;
            materialTextBoxEdit3.ShortcutsEnabled = true;
            materialTextBoxEdit3.Size = new Size(250, 36);
            materialTextBoxEdit3.TabIndex = 0;
            materialTextBoxEdit3.TabStop = false;
            materialTextBoxEdit3.TextAlign = HorizontalAlignment.Left;
            materialTextBoxEdit3.TrailingIcon = null;
            materialTextBoxEdit3.UseAccent = false;
            materialTextBoxEdit3.UseSystemPasswordChar = false;
            materialTextBoxEdit3.UseTallSize = false;
            // 
            // materialTextBoxEdit2
            // 
            materialTextBoxEdit2.AnimateReadOnly = false;
            materialTextBoxEdit2.AutoCompleteMode = AutoCompleteMode.None;
            materialTextBoxEdit2.AutoCompleteSource = AutoCompleteSource.None;
            materialTextBoxEdit2.BackgroundImageLayout = ImageLayout.None;
            materialTextBoxEdit2.CharacterCasing = CharacterCasing.Normal;
            materialTextBoxEdit2.Depth = 0;
            materialTextBoxEdit2.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBoxEdit2.HideSelection = true;
            materialTextBoxEdit2.Hint = "Họ và tên";
            materialTextBoxEdit2.LeadingIcon = null;
            materialTextBoxEdit2.Location = new Point(15, 13);
            materialTextBoxEdit2.Margin = new Padding(10);
            materialTextBoxEdit2.MaxLength = 32767;
            materialTextBoxEdit2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialTextBoxEdit2.Name = "materialTextBoxEdit2";
            materialTextBoxEdit2.PasswordChar = '\0';
            materialTextBoxEdit2.PrefixSuffixText = null;
            materialTextBoxEdit2.ReadOnly = false;
            materialTextBoxEdit2.RightToLeft = RightToLeft.No;
            materialTextBoxEdit2.SelectedText = "";
            materialTextBoxEdit2.SelectionLength = 0;
            materialTextBoxEdit2.SelectionStart = 0;
            materialTextBoxEdit2.ShortcutsEnabled = true;
            materialTextBoxEdit2.Size = new Size(250, 36);
            materialTextBoxEdit2.TabIndex = 0;
            materialTextBoxEdit2.TabStop = false;
            materialTextBoxEdit2.TextAlign = HorizontalAlignment.Left;
            materialTextBoxEdit2.TrailingIcon = null;
            materialTextBoxEdit2.UseAccent = false;
            materialTextBoxEdit2.UseSystemPasswordChar = false;
            materialTextBoxEdit2.UseTallSize = false;
            // 
            // materialComboBox1
            // 
            materialComboBox1.AutoResize = false;
            materialComboBox1.BackColor = Color.FromArgb(255, 255, 255);
            materialComboBox1.Depth = 0;
            materialComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            materialComboBox1.DropDownHeight = 118;
            materialComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBox1.DropDownWidth = 121;
            materialComboBox1.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialComboBox1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialComboBox1.FormattingEnabled = true;
            materialComboBox1.Hint = "Giới tính";
            materialComboBox1.IntegralHeight = false;
            materialComboBox1.ItemHeight = 29;
            materialComboBox1.Items.AddRange(new object[] { "Nam", "Nữ" });
            materialComboBox1.Location = new Point(15, 125);
            materialComboBox1.Margin = new Padding(10);
            materialComboBox1.MaxDropDownItems = 4;
            materialComboBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialComboBox1.Name = "materialComboBox1";
            materialComboBox1.Size = new Size(252, 35);
            materialComboBox1.StartIndex = 0;
            materialComboBox1.TabIndex = 1;
            materialComboBox1.UseAccent = false;
            materialComboBox1.UseTallSize = false;
            // 
            // materialComboBox2
            // 
            materialComboBox2.AutoResize = false;
            materialComboBox2.BackColor = Color.FromArgb(255, 255, 255);
            materialComboBox2.Depth = 0;
            materialComboBox2.DrawMode = DrawMode.OwnerDrawVariable;
            materialComboBox2.DropDownHeight = 118;
            materialComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBox2.DropDownWidth = 121;
            materialComboBox2.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialComboBox2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialComboBox2.FormattingEnabled = true;
            materialComboBox2.Hint = "Cấp đai";
            materialComboBox2.IntegralHeight = false;
            materialComboBox2.ItemHeight = 29;
            materialComboBox2.Items.AddRange(new object[] { "Đai trắng", "Đai vàng", "Đai xanh", "Đai đỏ", "Đai đen" });
            materialComboBox2.Location = new Point(287, 125);
            materialComboBox2.Margin = new Padding(10);
            materialComboBox2.MaxDropDownItems = 4;
            materialComboBox2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialComboBox2.Name = "materialComboBox2";
            materialComboBox2.Size = new Size(250, 35);
            materialComboBox2.StartIndex = 0;
            materialComboBox2.TabIndex = 1;
            materialComboBox2.UseAccent = false;
            materialComboBox2.UseTallSize = false;
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton1.Location = new Point(15, 373);
            materialButton1.Margin = new Padding(10);
            materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(64, 36);
            materialButton1.TabIndex = 3;
            materialButton1.Text = "Lưu";
            materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            // 
            // materialButton2
            // 
            materialButton2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton2.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton2.Depth = 0;
            materialButton2.HighEmphasis = true;
            materialButton2.Icon = null;
            materialButton2.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton2.Location = new Point(99, 373);
            materialButton2.Margin = new Padding(10);
            materialButton2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton2.Name = "materialButton2";
            materialButton2.NoAccentTextColor = Color.Empty;
            materialButton2.Size = new Size(64, 36);
            materialButton2.TabIndex = 3;
            materialButton2.Text = "Huỷ";
            materialButton2.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton2.UseAccentColor = false;
            materialButton2.UseVisualStyleBackColor = true;
            // 
            // UsersView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(materialTabControl1);
            Name = "UsersView";
            Text = "UsersView";
            materialTabControl1.ResumeLayout(false);
            tabPageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabPageDetail.ResumeLayout(false);
            tabPageDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialTabControl materialTabControl1;
        private TabPage tabPageList;
        private TabPage tabPageDetail;
        private Panel panel1;
        private DataGridView dataGridView1;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit materialTextBoxEdit1;
        private ReaLTaiizor.Controls.MaterialButton btnRegister;
        private ReaLTaiizor.Controls.MaterialButton btnSearch;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit materialTextBoxEdit2;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit materialTextBoxEdit4;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit materialTextBoxEdit3;
        private ReaLTaiizor.Controls.MaterialComboBox materialComboBox1;
        private ReaLTaiizor.Controls.MaterialComboBox materialComboBox2;
        private ReaLTaiizor.Controls.MaterialButton materialButton2;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
    }
}