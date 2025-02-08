public interface IAchievementView
{
    event EventHandler LoadAchievements;
    event EventHandler AddAchievement;
    event EventHandler UpdateAchievement;
    event EventHandler DeleteAchievement;

    void DisplayAchievements(List<AchievementModel> achievements);
    AchievementModel GetAchievementDetails();
}
