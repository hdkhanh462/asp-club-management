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
        private readonly IFinancesService service;

        private JwtResponse Jwt;
        private bool IsEdit;
        private int? Id;

        public FinancesDetailForm(IFinancesService service)
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
            if (IsEdit || Id.HasValue) return;

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

                var result = await service.CreateAsync(Jwt.Token, newEvent);

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
            if (IsEdit && Id.HasValue)
            {
                var result = await service.DeleteAsync(Jwt.Token, Id.Value);

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
            if (IsEdit && Id.HasValue)
            {
                try
                {
                    var updateEvent = new FinanceUpdateSchema
                    {
                        Id = Id.Value,
                        Type = (TransactionType)Enum.Parse(typeof(TransactionType), tbType.Text),
                        Category = tbCategory.Text,
                        Amount = long.Parse(tbAmount.Text),
                        TransactionDate = DateTime.Parse(tbTransactionDate.Text),
                        Description = tbDescription.Text
                    };

                    var result = await service.UpdateAsync(Jwt.Token, Id.Value, updateEvent);

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

        internal void SetJwt(JwtResponse jwt, bool isEdit, int? id)
        {
            Jwt = jwt;
            IsEdit = isEdit;
            Id = id;
        }
    }
}
