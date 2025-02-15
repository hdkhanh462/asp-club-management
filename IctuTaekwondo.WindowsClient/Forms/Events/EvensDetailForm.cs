using System.ComponentModel.DataAnnotations;
using System.Data;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Events;
using IctuTaekwondo.WindowsClient.Utils;

namespace IctuTaekwondo.WindowsClient.Forms.Events
{
    public partial class EventsDetailForm : Form
    {
        private readonly IEventsService service;
        private readonly IRegisterationService registerationService;
        private readonly IAccountService accountService;

        private JwtResponse Jwt;
        private UserFullDetailResponse CurrentUser;
        private bool IsAdmin;
        private bool IsManager;
        private bool IsMember;
        private int Id;
        private string? UserId;

        public EventsDetailForm(IEventsService service, IRegisterationService registerationService, IAccountService accountService)
        {
            InitializeComponent();

            this.service = service;
            this.registerationService = registerationService;
            this.accountService = accountService;
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

            if (IsAdmin || IsManager)
            {
                dataGridView1.Enabled = true;
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
            }

            var detail = await service.FindByIdAsync(Jwt.Token, Id);

            if (detail != null)
            {
                eventUpdateSchemaBindingSource.DataSource = new EventUpdateSchema
                {
                    Id = detail.Id,
                    Name = detail.Name,
                    Location = detail.Location,
                    Fee = detail.Fee,
                    MaxParticipants = detail.MaxParticipants,
                    Description = detail.Description,
                    StartDate = detail.StartDate,
                    EndDate = detail.EndDate,
                };

                var registerUsers = await registerationService.GetAllAsync(Jwt.Token, Id, 1, 999);
                dataGridView1.DataSource = registerUsers.Items;

                if (registerUsers.Items.Any(r=>r.Id == currentUser.Id) && IsMember)
                {
                    btnUnregister.Enabled = true;
                    btnRegister.Enabled = false;
                }
                else
                {
                    btnUnregister.Enabled = false;
                    btnRegister.Enabled = true;
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sự kiện này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            var response = await service.DeleteAsync(Jwt.Token, Id);

            if (result == DialogResult.Yes)
            {
                if (response)
                {
                    MessageBox.Show("Xóa sự kiện thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                MessageBox.Show("Xóa sự kiện không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            eventUpdateSchemaBindingSource.EndEdit();

            if (eventUpdateSchemaBindingSource.Current is EventUpdateSchema schema)
            {
                if (!Helpers.IsValidSchema(schema)) return;

                var result = await service.UpdateAsync(Jwt.Token, Id, schema);

                if (result != null)
                {
                    eventUpdateSchemaBindingSource.DataSource = new EventUpdateSchema
                    {
                        Id = result.Id,
                        Name = result.Name,
                        Location = result.Location,
                        Fee = result.Fee,
                        MaxParticipants = result.MaxParticipants,
                        Description = result.Description,
                        StartDate = result.StartDate,
                        EndDate = result.EndDate,
                    };

                    MessageBox.Show("Cập nhật sự kiện thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Cập nhật sự kiện không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void SetJwt(JwtResponse jwt, int id)
        {
            Jwt = jwt;
            Id = id;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                UserId = id;
                btnUnregister.Enabled = true;
            }
        }

        private async void btnUnregister_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UserId) && (IsAdmin || IsManager))
            {
                var response = await registerationService.ManagerUnregisterAsync(Jwt.Token, Id, UserId);
                if (response)
                {
                    UserId = null;
                    btnUnregister.Enabled = false;

                    var registerUsers = await registerationService.GetAllAsync(Jwt.Token, Id, 1, 999);
                    dataGridView1.DataSource = registerUsers.Items;

                    MessageBox.Show("Huỷ đăng ký tham gia thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (IsMember)
            {
                var response = await registerationService.UnregisterAsync(Jwt.Token, Id);
                if (response)
                {
                    btnUnregister.Enabled = false;
                    btnRegister.Enabled = true;

                    var registerUsers = await registerationService.GetAllAsync(Jwt.Token, Id, 1, 999);
                    dataGridView1.DataSource = registerUsers.Items;

                    MessageBox.Show("Huỷ đăng ký tham gia thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            MessageBox.Show("Huỷ đăng ký tham gia không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (!IsMember) return;

            var error = await registerationService.RegisterAsync(Jwt.Token, Id);
            if (string.IsNullOrEmpty(error))
            {
                btnUnregister.Enabled = true;
                btnRegister.Enabled = false;

                var registerUsers = await registerationService.GetAllAsync(Jwt.Token, Id, 1, 999);
                dataGridView1.DataSource = registerUsers.Items;

                MessageBox.Show("Đăng ký tham gia thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
