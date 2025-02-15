using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Services.Achievements;
using IctuTaekwondo.WindowsClient.Utils;

namespace IctuTaekwondo.WindowsClient.Forms.Achievements
{
    public partial class AchievementAddNewForm : Form
    {
        private readonly IAchievementsService service;

        private JwtResponse Jwt;

        public AchievementAddNewForm(IAchievementsService service)
        {
            InitializeComponent();
            this.service = service;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void AchievementAddNewForm_Load(object sender, EventArgs e)
        {
            achievementCreateSchemaBindingSource.DataSource = new AchievementCreateSchema();
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            achievementCreateSchemaBindingSource.EndEdit();

            if (achievementCreateSchemaBindingSource.Current is AchievementCreateSchema schema)
            {
                if (!Helpers.IsValidSchema(schema)) return;

                var response = await service.CreateAsync(Jwt.Token, schema);
                if (response.Any())
                {
                    MessageBox.Show(string.Join("\n", response.SelectMany(x => x.Value)), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Thêm mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                achievementCreateSchemaBindingSource.DataSource = new AchievementCreateSchema();
            }
        }
    }
}
