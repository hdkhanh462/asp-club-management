using System.Windows.Forms;
using Autofac;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Achievements;
using IctuTaekwondo.WindowsClient.Utils;
using Microsoft.Extensions.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace IctuTaekwondo.WindowsClient.Forms.Achievements
{
    public partial class AchievementsDetailForm : Form
    {
        private readonly IAchievementsService service;
        private readonly IAccountService accountService;

        private JwtResponse Jwt;
        private int Id;

        private UserFullDetailResponse CurrentUser;
        private bool IsAdmin;
        private bool IsManager;
        private bool IsMember;

        public AchievementsDetailForm(IAchievementsService service, IAccountService accountService)
        {
            InitializeComponent();

            this.service = service;
            this.accountService = accountService;
        }

        internal void SetJwt(JwtResponse jwt, int id)
        {
            Jwt = jwt;
            Id = id;
        }

        private async void EvensDetailForm_Load(object sender, EventArgs e)
        {
            var currentUser = await accountService.GetProfileAsync(Jwt.Token);

            if (currentUser == null)
            {
                MessageBox.Show("Lỗi xác thực tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            IsAdmin = currentUser.Roles.Contains(Role.Admin.ToString());
            IsManager = currentUser.Roles.Contains(Role.Manager.ToString());
            IsMember = currentUser.Roles.Contains(Role.Member.ToString());
            CurrentUser = currentUser;

            if (IsAdmin)
            {
                btnDelete.Enabled = true;
            }

            if (IsAdmin ||  IsManager)
            {
                btnSave.Enabled = true;
            }

            var detail = await service.FindByIdAsync(Jwt.Token, Id);

            if (detail != null)
            {
                achievementUpdateSchemaBindingSource.DataSource = new AchievementUpdateSchema
                {
                    Id = detail.Id,
                    Name = detail.Name,
                    DateAchieved = detail.DateAchieved,
                    Description = detail.Description,
                    EventId = detail.Event?.Id,
                    UserId = detail.User.Id,
                };
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa thành tích này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            var response = await service.DeleteAsync(Jwt.Token, Id);

            if (result == DialogResult.Yes)
            {
                if (response)
                {
                    MessageBox.Show("Xóa thành tích thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                MessageBox.Show("Xóa thành tích không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            achievementUpdateSchemaBindingSource.EndEdit();

            if (achievementUpdateSchemaBindingSource.Current is AchievementUpdateSchema schema)
            {
                if (!Helpers.IsValidSchema(schema)) return;

                var result = await service.UpdateAsync(Jwt.Token, Id, schema);

                if (result != null)
                {
                    achievementUpdateSchemaBindingSource.DataSource = new AchievementUpdateSchema
                    {
                        Id = result.Id,
                        Name = result.Name,
                        DateAchieved = result.DateAchieved,
                        Description = result.Description,
                        EventId = result.Event?.Id,
                        UserId = result.User.Id,
                    };
                    MessageBox.Show("Cập nhật thành tích thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Cập nhật thành tích không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
