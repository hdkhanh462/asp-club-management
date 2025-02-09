using Autofac;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Schemas.Finance;
using IctuTaekwondo.Shared.Services.Finances;

namespace IctuTaekwondo.WindowsClient.Forms.Finances
{
    public partial class FinancesDetailForm : Form
    {
        private readonly IContainer appContainer;
        private JwtResponse jwt;
        private bool isEdit;
        private int? id;
        private readonly IFinancesService service;

        public FinancesDetailForm(IContainer appContainer, JwtResponse jwt, bool isEdit, int? id = null)
        {
            InitializeComponent();

            this.appContainer = appContainer;
            this.jwt = jwt;
            this.isEdit = isEdit;
            this.id = id;
            this.service = appContainer.Resolve<IFinancesService>();
        }

        private async void EvensDetailForm_Load(object sender, EventArgs e)
        {
            if (isEdit && id.HasValue)
            {
                var detail = await service.FindByIdAsync(jwt.Token, id.Value);

                if (detail != null)
                {
                    tbId.Text = detail.Id.ToString();
                    tbType.Text = detail.Type.GetDisplayName();
                    tbType.Tag = detail.Type.ToString();
                    tbAmount.Text = detail.Description;
                    tbCategory.Text = detail.Category;
                    tbTransactionDate.Text = detail.TransactionDate.ToString();
                    tbDescription.Text = detail.Description;
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
                var newEvent = new FinanceCreateSchema
                {
                    Type = (TransactionType)Enum.Parse(typeof(TransactionType), tbType.Text),
                    Category = tbCategory.Text,
                    Amount = long.Parse(tbAmount.Text),
                    TransactionDate = DateTime.Parse(tbTransactionDate.Text),
                    Description = tbDescription.Text
                };

                var result = await service.CreateAsync(jwt.Token, newEvent);

                if (result != null)
                {
                    MessageBox.Show("Tạo giao dịch thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tạo giao dịch không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Xóa giao dịch thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xóa giao dịch không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (isEdit && id.HasValue)
            {
                try
                {
                    var updateEvent = new FinanceUpdateSchema
                    {
                        Id = id.Value,
                        Type = (TransactionType)Enum.Parse(typeof(TransactionType), tbType.Text),
                        Category = tbCategory.Text,
                        Amount = long.Parse(tbAmount.Text),
                        TransactionDate = DateTime.Parse(tbTransactionDate.Text),
                        Description = tbDescription.Text
                    };

                    var result = await service.UpdateAsync(jwt.Token, id.Value, updateEvent);

                    if (result != null)
                    {
                        MessageBox.Show("Cập nhật giao dịch thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật giao dịch không thành công.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
