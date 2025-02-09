using Autofac;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Shared.Services.Achievements;
using Microsoft.Extensions.Logging;

namespace IctuTaekwondo.WindowsClient.Forms.Achievements
{
    public partial class AchievementsDetailForm : Form
    {
        private readonly IContainer appContainer;
        private JwtResponse jwt;
        private bool isEdit;
        private int? id;
        private readonly IAchievementsService service;

        public AchievementsDetailForm(IContainer appContainer, JwtResponse jwt, bool isEdit, int? id = null)
        {
            InitializeComponent();

            this.appContainer = appContainer;
            this.jwt = jwt;
            this.isEdit = isEdit;
            this.id = id;
            this.service = appContainer.Resolve<IAchievementsService>();
        }

        private async void EvensDetailForm_Load(object sender, EventArgs e)
        {
            if (isEdit && id.HasValue)
            {
                var detail = await service.FindByIdAsync(jwt.Token, id.Value);

                if (detail != null)
                {
                    tbId.Text = detail.Id.ToString();
                    tbName.Text = detail.Name;
                    tbDescription.Text = detail.Description;
                    tbDateAchieved.Text = detail.DateAchieved.ToString();
                    tbUserId.Text = detail.User.Id;
                    tbEventId.Text = detail.Event?.Id.ToString();
                }

                btnSave.Enabled = detail != null;
                btnDelete.Enabled = detail != null;
                btnAddNew.Enabled = false;
            }
            else
            {
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
                btnAddNew.Enabled = true;
            }
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            if (isEdit || id.HasValue) return;

            try
            {
                var newEvent = new AchievementCreateSchema
                {
                    Name = tbName.Text,
                    DateAchieved = DateOnly.Parse(tbDateAchieved.Text),
                    Description = tbDescription.Text,
                    UserId = tbUserId.Text,
                    EventId = int.TryParse(tbEventId.Text, out int eventId) ? eventId : null
                };

                var result = await service.CreateAsync(jwt.Token, newEvent);

                if (result != null)
                {
                    MessageBox.Show("Tạo thành tích thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tạo thành tích không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (isEdit && id.HasValue)
            {
                var result = await service.DeleteAsync(jwt.Token, id.Value);

                if (result)
                {
                    MessageBox.Show("Xóa thành tích thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xóa thành tích không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (isEdit && id.HasValue)
            {
                try
                {
                    var updateEvent = new AchievementUpdateSchema
                    {
                        Id = id.Value,
                        Name = tbName.Text,
                        DateAchieved = DateOnly.Parse(tbDateAchieved.Text),
                        Description = tbDescription.Text,
                        UserId = tbUserId.Text,
                        EventId = int.TryParse(tbEventId.Text, out int eventId) ? eventId : null
                    };

                    var result = await service.UpdateAsync(jwt.Token, id.Value, updateEvent);

                    if (result != null)
                    {
                        MessageBox.Show("Cập nhật thành tích thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thành tích không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
