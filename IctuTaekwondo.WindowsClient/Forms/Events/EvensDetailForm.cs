using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Shared.Services.Events;
using IctuTaekwondo.Shared.Services.Users;

namespace IctuTaekwondo.WindowsClient.Forms.Events
{
    public partial class EventsDetailForm : Form
    {
        private readonly IEventsService service;

        private JwtResponse Jwt;
        private bool IsEdit;
        private int? Id;

        public EventsDetailForm(IEventsService service)
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
                    tbLocation.Text = detail.Location;
                    tbFee.Text = detail.Fee.ToString();
                    tbMax.Text = detail.MaxParticipants.ToString();
                    tbStartDate.Text = detail.StartDate.ToString();
                    tbEndDate.Text = detail.EndDate.ToString();

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
                var newEvent = new EventCreateSchema
                {
                    Name = tbName.Text,
                    Location = tbLocation.Text,
                    StartDate = DateTime.Parse(tbStartDate.Text),
                    EndDate = DateTime.Parse(tbEndDate.Text),
                    Fee = long.Parse(tbFee.Text),
                    MaxParticipants = short.Parse(tbMax.Text)
                };

                var result = await service.CreateAsync(Jwt.Token, newEvent);

                if (result != null)
                {
                    MessageBox.Show("Tạo sự kiện thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tạo sự kiện không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Xóa sự kiện thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xóa sự kiện không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (IsEdit && Id.HasValue)
            {
                try
                {
                    var updateEvent = new EventUpdateSchema
                    {
                        Id = Id.Value,
                        Name = tbName.Text,
                        Location = tbLocation.Text,
                        StartDate = DateTime.Parse(tbStartDate.Text),
                        EndDate =  !string.IsNullOrWhiteSpace(tbEndDate.Text) ?  DateTime.Parse(tbEndDate.Text) : null,
                        Fee = long.Parse(tbFee.Text),
                        MaxParticipants = short.Parse(tbMax.Text)
                    };

                    var result = await service.UpdateAsync(Jwt.Token, Id.Value, updateEvent);

                    if (result != null)
                    {
                        MessageBox.Show("Cập nhật sự kiện thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật sự kiện không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
