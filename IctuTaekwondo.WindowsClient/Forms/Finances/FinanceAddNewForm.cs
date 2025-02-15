using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Schemas.Finance;
using IctuTaekwondo.Shared.Services.Achievements;
using IctuTaekwondo.Shared.Services.Finances;
using IctuTaekwondo.WindowsClient.Utils;

namespace IctuTaekwondo.WindowsClient.Forms.Finances
{
    public partial class FinanceAddNewForm : Form
    {
        private readonly IFinancesService service;

        private JwtResponse Jwt;

        public FinanceAddNewForm(IFinancesService service)
        {
            InitializeComponent();
            Helpers.LoadComboBoxs<TransactionType>(cbTransactionType);

            this.service = service;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void FinanceAddNewForm_Load(object sender, EventArgs e)
        {
            financeCreateSchemaBindingSource.DataSource = new FinanceCreateSchema();
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            financeCreateSchemaBindingSource.EndEdit();

            if (financeCreateSchemaBindingSource.Current is FinanceCreateSchema schema)
            {
                if (!Helpers.IsValidSchema(schema)) return;

                var response = await service.CreateAsync(Jwt.Token, schema);
                if (response.Any())
                {
                    MessageBox.Show(string.Join("\n", response.SelectMany(x => x.Value)), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Thêm mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                financeCreateSchemaBindingSource.DataSource = new FinanceCreateSchema();
            }
        }
    }
}
