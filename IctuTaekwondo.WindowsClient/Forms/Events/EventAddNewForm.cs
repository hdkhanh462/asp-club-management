using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Schemas.Event;
using IctuTaekwondo.Shared.Services.Events;
using IctuTaekwondo.Shared.Services.Users;
using IctuTaekwondo.WindowsClient.Utils;

namespace IctuTaekwondo.WindowsClient.Forms.Events
{
    public partial class EventAddNewForm : Form
    {
        private readonly IEventsService service;

        private JwtResponse Jwt;

        public EventAddNewForm(IEventsService service)
        {
            InitializeComponent();
            this.service = service;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void EventAddNewForm_Load(object sender, EventArgs e)
        {
            eventCreateSchemaBindingSource.DataSource = new EventCreateSchema();
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            eventCreateSchemaBindingSource.EndEdit();

            if (eventCreateSchemaBindingSource.Current is EventCreateSchema schema)
            {
                if (!Helpers.IsValidSchema(schema)) return;

                var response = await service.CreateAsync(Jwt.Token, schema);
                if (response.Any())
                {
                    MessageBox.Show(string.Join("\n", response.SelectMany(x => x.Value)), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Thêm mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                eventCreateSchemaBindingSource.DataSource = new EventCreateSchema();
            }
        }
    }
}
