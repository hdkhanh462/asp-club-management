using YourNamespace;

public class AchievementPresenter
{
    private readonly IAchievementView _view;
    private readonly List<AchievementModel> _achievements;

    public AchievementPresenter(IAchievementView view)
    {
        _view = view;
        _achievements = new List<AchievementModel>();

        _view.LoadAchievements += OnLoadAchievements;
        _view.AddAchievement += OnAddAchievement;
        _view.UpdateAchievement += OnUpdateAchievement;
        _view.DeleteAchievement += OnDeleteAchievement;
    }

    private void OnLoadAchievements(object? sender, EventArgs e)
    {
        _view.DisplayAchievements(_achievements);
    }

    private void OnAddAchievement(object? sender, EventArgs e)
    {
        var achievement = _view.GetAchievementDetails();
        _achievements.Add(achievement);
        _view.DisplayAchievements(_achievements);
    }

    private void OnUpdateAchievement(object? sender, EventArgs e)
    {
        var achievement = _view.GetAchievementDetails();
        var existingAchievement = _achievements.FirstOrDefault(a => a.Id == achievement.Id);
        if (existingAchievement != null)
        {
            existingAchievement.Name = achievement.Name;
            existingAchievement.Description = achievement.Description;
            existingAchievement.DateAchieved = achievement.DateAchieved;
        }
        _view.DisplayAchievements(_achievements);
    }

    private void OnDeleteAchievement(object? sender, EventArgs e)
    {
        var achievement = _view.GetAchievementDetails();
        _achievements.RemoveAll(a => a.Id == achievement.Id);
        _view.DisplayAchievements(_achievements);
    }

    internal void SetView(AchievementView achievementView)
    {
        //
    }
}
