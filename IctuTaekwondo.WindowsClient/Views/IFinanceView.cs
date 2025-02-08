public interface IFinanceView
{
    event EventHandler LoadFinances;
    event EventHandler AddFinance;
    event EventHandler UpdateFinance;
    event EventHandler DeleteFinance;

    void DisplayFinances(List<FinanceModel> finances);
    FinanceModel GetFinanceDetails();
}
