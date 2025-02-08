namespace IctuTaekwondo.WindowsClient.Views
{
    partial class MainView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            drawerIcons = new ImageList(components);
            drawerTabControl = new ReaLTaiizor.Controls.MaterialTabControl();
            tabPageHome = new TabPage();
            tabPageDashboard = new TabPage();
            tabPageUsers = new TabPage();
            drawerTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // drawerIcons
            // 
            drawerIcons.ColorDepth = ColorDepth.Depth32Bit;
            drawerIcons.ImageStream = (ImageListStreamer)resources.GetObject("drawerIcons.ImageStream");
            drawerIcons.TransparentColor = Color.Transparent;
            drawerIcons.Images.SetKeyName(0, "icons8-find-user-male-24.png");
            drawerIcons.Images.SetKeyName(1, "icons8-home-24.png");
            drawerIcons.Images.SetKeyName(2, "icons8-income-24.png");
            drawerIcons.Images.SetKeyName(3, "icons8-party-24.png");
            drawerIcons.Images.SetKeyName(4, "icons8-dashboard-24.png");
            // 
            // drawerTabControl
            // 
            drawerTabControl.Controls.Add(tabPageHome);
            drawerTabControl.Controls.Add(tabPageDashboard);
            drawerTabControl.Controls.Add(tabPageUsers);
            drawerTabControl.Depth = 0;
            drawerTabControl.Dock = DockStyle.Fill;
            drawerTabControl.ImageList = drawerIcons;
            drawerTabControl.ItemSize = new Size(70, 30);
            drawerTabControl.Location = new Point(3, 64);
            drawerTabControl.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            drawerTabControl.Multiline = true;
            drawerTabControl.Name = "drawerTabControl";
            drawerTabControl.SelectedIndex = 0;
            drawerTabControl.Size = new Size(794, 383);
            drawerTabControl.TabIndex = 0;
            // 
            // tabPageHome
            // 
            tabPageHome.ImageKey = "icons8-home-24.png";
            tabPageHome.Location = new Point(4, 34);
            tabPageHome.Name = "tabPageHome";
            tabPageHome.Padding = new Padding(3);
            tabPageHome.Size = new Size(786, 345);
            tabPageHome.TabIndex = 0;
            tabPageHome.Text = "Trang chủ";
            tabPageHome.UseVisualStyleBackColor = true;
            // 
            // tabPageDashboard
            // 
            tabPageDashboard.ImageKey = "icons8-dashboard-24.png";
            tabPageDashboard.Location = new Point(4, 34);
            tabPageDashboard.Name = "tabPageDashboard";
            tabPageDashboard.Padding = new Padding(3);
            tabPageDashboard.Size = new Size(786, 345);
            tabPageDashboard.TabIndex = 1;
            tabPageDashboard.Text = "Bảng điều khiển";
            tabPageDashboard.UseVisualStyleBackColor = true;
            // 
            // tabPageUsers
            // 
            tabPageUsers.ImageKey = "icons8-find-user-male-24.png";
            tabPageUsers.Location = new Point(4, 34);
            tabPageUsers.Name = "tabPageUsers";
            tabPageUsers.Size = new Size(786, 345);
            tabPageUsers.TabIndex = 2;
            tabPageUsers.Text = "Quản lý người dùng";
            tabPageUsers.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(drawerTabControl);
            DrawerHighlightWithAccent = false;
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = drawerTabControl;
            Name = "MainView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainView";
            FormClosed += MainView_FormClosed;
            drawerTabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ImageList drawerIcons;
        private ReaLTaiizor.Controls.MaterialTabControl drawerTabControl;
        private TabPage tabPageHome;
        private TabPage tabPageDashboard;
        private TabPage tabPageUsers;
    }
}