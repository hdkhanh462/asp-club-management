namespace IctuTaekwondo.WebClient.Models
{
    public class TableHeaderViewModel
    {
        public List<string> Orders { get; set; } = [];

        public bool HasOrder(string name)
        {
            return Orders.Contains(name) || Orders.Contains($"-{name}");
        }
    }
}
