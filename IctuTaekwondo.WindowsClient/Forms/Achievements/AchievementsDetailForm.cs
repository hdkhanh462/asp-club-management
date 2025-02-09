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
        private readonly IAchievementsService service;

        private JwtResponse Jwt;
        private bool IsEdit;
        private int? Id;

        public AchievementsDetailForm(IAchievementsService service)
        {
            this.service = service;
            
            InitializeComponent();
        }

        private async void EvensDetailForm_Load(object sender, EventArgs e)
        {
            if (IsEdit && Id.HasValue)
            {
                var detail = await service.FindByIdAsync(Jwt.Token, Id.Value);

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
            if (IsEdit || Id.HasValue) return;

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

                var result = await service.CreateAsync(Jwt.Token, newEvent);

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
            if (IsEdit && Id.HasValue)
            {
                var result = await service.DeleteAsync(Jwt.Token, Id.Value);

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
            if (IsEdit && Id.HasValue)
            {
                try
                {
                    var updateEvent = new AchievementUpdateSchema
                    {
                        Id = Id.Value,
                        Name = tbName.Text,
                        DateAchieved = DateOnly.Parse(tbDateAchieved.Text),
                        Description = tbDescription.Text,
                        UserId = tbUserId.Text,
                        EventId = int.TryParse(tbEventId.Text, out int eventId) ? eventId : null
                    };

                    var result = await service.UpdateAsync(Jwt.Token, Id.Value, updateEvent);

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

        internal void SetJwt(JwtResponse jwt, bool isEdit, int? id)
        {
            Jwt = jwt;
            IsEdit = isEdit;
            Id = id;
        }
    }
}
