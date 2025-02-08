using YourNamespace;

public class FinancePresenter
{
    private readonly IFinanceView _view;
    private readonly List<FinanceModel> _finances;

    public FinancePresenter(IFinanceView view)
    {
        _view = view;
        _finances = new List<FinanceModel>();

        _view.LoadFinances += OnLoadFinances;
        _view.AddFinance += OnAddFinance;
        _view.UpdateFinance += OnUpdateFinance;
        _view.DeleteFinance += OnDeleteFinance;
    }

    private void OnLoadFinances(object sender, EventArgs e)
    {
        _view.DisplayFinances(_finances);
    }

    private void OnAddFinance(object sender, EventArgs e)
    {
        var finance = _view.GetFinanceDetails();
        _finances.Add(finance);
        _view.DisplayFinances(_finances);
    }

    private void OnUpdateFinance(object sender, EventArgs e)
    {
        var finance = _view.GetFinanceDetails();
        var existingFinance = _finances.FirstOrDefault(f => f.Id == finance.Id);
        if (existingFinance != null)
        {
            existingFinance.Description = finance.Description;
            existingFinance.Amount = finance.Amount;
            existingFinance.Date = finance.Date;
        }
        _view.DisplayFinances(_finances);
    }

    private void OnDeleteFinance(object sender, EventArgs e)
    {
        var finance = _view.GetFinanceDetails();
        _finances.RemoveAll(f => f.Id == finance.Id);
        _view.DisplayFinances(_finances);
    }

    internal void SetView(FinanceView financeView)
    {
        throw new NotImplementedException();
    }
}
