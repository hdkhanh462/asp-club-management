using System;
using System.Windows.Forms;

namespace YourNamespace
{
    public partial class MainForm : Form
    {
        private readonly AchievementPresenter _achievementPresenter;
        private readonly EventPresenter _eventPresenter;
        private readonly FinancePresenter _financePresenter;

        public MainForm()
        {
            InitializeComponent();
            _achievementPresenter = new AchievementPresenter(new AchievementView());
            _eventPresenter = new EventPresenter(new EventView());
            _financePresenter = new FinancePresenter(new FinanceView());
        }

        private void btnAchievements_Click(object sender, EventArgs e)
        {
            var achievementView = new AchievementView();
            _achievementPresenter.SetView(achievementView);
            achievementView.Show();
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            var eventView = new EventView();
            _eventPresenter.SetView(eventView);
            eventView.Show();
        }

        private void btnFinances_Click(object sender, EventArgs e)
        {
            var financeView = new FinanceView();
            _financePresenter.SetView(financeView);
            financeView.Show();
        }
    }
}
