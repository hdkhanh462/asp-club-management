using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YourNamespace
{
    public partial class FinanceView : Form, IFinanceView
    {
        public event EventHandler LoadFinances;
        public event EventHandler AddFinance;
        public event EventHandler UpdateFinance;
        public event EventHandler DeleteFinance;

        public FinanceView()
        {
            InitializeComponent();
        }

        public void DisplayFinances(List<FinanceModel> finances)
        {
            // Display finances in a ListView or DataGridView
        }

        public FinanceModel GetFinanceDetails()
        {
            // Get finance details from input fields
            return new FinanceModel();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadFinances?.Invoke(this, EventArgs.Empty);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddFinance?.Invoke(this, EventArgs.Empty);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateFinance?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteFinance?.Invoke(this, EventArgs.Empty);
        }
    }
}
