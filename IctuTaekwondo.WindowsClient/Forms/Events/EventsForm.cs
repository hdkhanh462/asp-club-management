using Autofac;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Services.Events;

namespace IctuTaekwondo.WindowsClient.Forms.Events
{
    public partial class EventsForm : Form
    {
        private readonly IEventsService service;
        private readonly IContainer appContainer;
        private JwtResponse jwt;

        public EventsForm(IContainer appContainer, JwtResponse jwt)
        {
            InitializeComponent();
            this.appContainer = appContainer;
            this.jwt = jwt;
            this.service = appContainer.Resolve<IEventsService>();
        }

        private async void EventsForm_Load(object sender, EventArgs e)
        {
            var response = await service.GetAllAsync(jwt.Token, 1, 999);

            dataGridView1.DataSource = response.Items;
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                int.TryParse(id, out var result);

                var userDetailForm = new EventsDetailForm(appContainer, jwt, true, result);
                userDetailForm.ShowDialog();
            }
        }

        private void btnAđNew_Click(object sender, EventArgs e)
        {
            var userDetailForm = new EventsDetailForm(appContainer, jwt, false);
            userDetailForm.ShowDialog();
        }
    }
}
