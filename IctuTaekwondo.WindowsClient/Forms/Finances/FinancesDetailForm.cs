using Autofac;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Schemas.Finance;
using IctuTaekwondo.Shared.Services.Finances;
using IctuTaekwondo.WindowsClient.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace IctuTaekwondo.WindowsClient.Forms.Finances
{
    public partial class FinancesDetailForm : Form
    {
        private readonly IFinancesService service;

        private JwtResponse Jwt;
        private int Id;

        public FinancesDetailForm(IFinancesService service)
        {
            InitializeComponent();
            Helpers.LoadComboBoxs<TransactionType>(cbTransactionType);

            this.service = service;
        }

        internal void SetJwt(JwtResponse jwt, int id)
        {
            Jwt = jwt;
            Id = id;
        }

        private async void EvensDetailForm_Load(object sender, EventArgs e)
        {
            var detail = await service.FindByIdAsync(Jwt.Token, Id);

            if (detail != null)
            {
                financeUpdateSchemaBindingSource.DataSource = new FinanceUpdateSchema
                {
                    Id = detail.Id,
                    Category = detail.Category,
                    TransactionDate = detail.TransactionDate,
                    Amount = detail.Amount,
                    Type = detail.Type,
                    Description = detail.Description    
                };
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            var response = await service.DeleteAsync(Jwt.Token, Id);

            if (result == DialogResult.Yes)
            {
                if (response)
                {
                    MessageBox.Show("Xóa giao dịch thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                MessageBox.Show("Xóa giao dịch không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            financeUpdateSchemaBindingSource.EndEdit();

            if (financeUpdateSchemaBindingSource.Current is FinanceUpdateSchema schema)
            {
                if (!Helpers.IsValidSchema(schema)) return;

                var result = await service.UpdateAsync(Jwt.Token, Id, schema);

                if (result != null)
                {
                    financeUpdateSchemaBindingSource.DataSource = new FinanceUpdateSchema
                    {
                        Id = result.Id,
                        Category = result.Category,
                        TransactionDate = result.TransactionDate,
                        Amount = result.Amount,
                        Type = result.Type,
                        Description = result.Description
                    };
                    MessageBox.Show("Cập nhật giao dịch thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Cập nhật giao dịch không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
