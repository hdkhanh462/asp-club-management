using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YourNamespace
{

    public partial class AchievementView : Form, IAchievementView
    {
        private ListView lvAchievements; // Add this line to declare lvAchievements
        private TextBox txtId; // Add this line to declare txtId
        private TextBox txtName; // Add this line to declare txtName
        private TextBox txtDescription; // Add this line to declare txtDescription
        private DateTimePicker dtpDateAchieved; // Add this line to declare dtpDateAchieved

        public event EventHandler? LoadAchievements;
        public event EventHandler? AddAchievement;
        public event EventHandler? UpdateAchievement;
        public event EventHandler? DeleteAchievement;

        public AchievementView()
        {
            InitializeComponent();
            lvAchievements = new ListView(); // Initialize lvAchievements
            txtId = new TextBox(); // Initialize txtId
            txtName = new TextBox(); // Initialize txtName
            txtDescription = new TextBox(); // Initialize txtDescription
            dtpDateAchieved = new DateTimePicker(); // Initialize dtpDateAchieved
        }

        public void DisplayAchievements(List<AchievementModel> achievements)
        {
            lvAchievements.Items.Clear();
            foreach (var achievement in achievements)
            {
                var item = new ListViewItem(achievement.Id.ToString());
                item.SubItems.Add(achievement.Name);
                item.SubItems.Add(achievement.Description);
                item.SubItems.Add(achievement.DateAchieved.ToShortDateString());
                lvAchievements.Items.Add(item);
            }
        }

        public AchievementModel GetAchievementDetails()
        {
            return new AchievementModel
            {
                Id = int.Parse(txtId.Text),
                Name = txtName.Text,
                Description = txtDescription.Text,
                DateAchieved = dtpDateAchieved.Value
            };
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadAchievements?.Invoke(this, EventArgs.Empty);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddAchievement?.Invoke(this, EventArgs.Empty);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAchievement?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteAchievement?.Invoke(this, EventArgs.Empty);
        }
    }
}
