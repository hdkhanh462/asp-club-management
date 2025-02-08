namespace YourNamespace
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAchievements;
        private System.Windows.Forms.Button btnEvents;
        private System.Windows.Forms.Button btnFinances;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnAchievements = new System.Windows.Forms.Button();
            this.btnEvents = new System.Windows.Forms.Button();
            this.btnFinances = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAchievements
            // 
            this.btnAchievements.Location = new System.Drawing.Point(12, 12);
            this.btnAchievements.Name = "btnAchievements";
            this.btnAchievements.Size = new System.Drawing.Size(100, 23);
            this.btnAchievements.TabIndex = 0;
            this.btnAchievements.Text = "Achievements";
            this.btnAchievements.UseVisualStyleBackColor = true;
            this.btnAchievements.Click += new System.EventHandler(this.btnAchievements_Click);
            // 
            // btnEvents
            // 
            this.btnEvents.Location = new System.Drawing.Point(12, 41);
            this.btnEvents.Name = "btnEvents";
            this.btnEvents.Size = new System.Drawing.Size(100, 23);
            this.btnEvents.TabIndex = 1;
            this.btnEvents.Text = "Events";
            this.btnEvents.UseVisualStyleBackColor = true;
            this.btnEvents.Click += new System.EventHandler(this.btnEvents_Click);
            // 
            // btnFinances
            // 
            this.btnFinances.Location = new System.Drawing.Point(12, 70);
            this.btnFinances.Name = "btnFinances";
            this.btnFinances.Size = new System.Drawing.Size(100, 23);
            this.btnFinances.TabIndex = 2;
            this.btnFinances.Text = "Finances";
            this.btnFinances.UseVisualStyleBackColor = true;
            this.btnFinances.Click += new System.EventHandler(this.btnFinances_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnFinances);
            this.Controls.Add(this.btnEvents);
            this.Controls.Add(this.btnAchievements);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);
        }
    }
}
