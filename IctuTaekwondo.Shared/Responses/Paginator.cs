using System.Text.Json.Serialization;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses
{
    public class Paginator<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public virtual ICollection<T> Items { get; set; } = [];

        [JsonConstructor]
        public Paginator(int currentPage, int pageSize, int totalItemCount, ICollection<T> items)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            Items = items;
            PageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            HasPrevious = currentPage > 1;
            HasNext = PageCount > currentPage;
        }

        public List<int> GetItemRange()
        {
            var list = new List<int>() { 0, 0 };

            if (Items.Count > 0)
            {
                var startItem = (CurrentPage - 1) * PageSize + 1;
                var endItem = Math.Min(CurrentPage * PageSize, TotalItemCount);
                
                list.Clear();
                list.Add(startItem);
                list.Add(endItem);
            }

            return list;
        }

        public List<int> GetDisplayPageRange()
        {
            var list = new List<int>();

            for (int i = Math.Max(1, CurrentPage - 2); i <= Math.Min(PageCount, CurrentPage + 2); i++)
            {
                list.Add(i);
            }

            return list;
        }

        public static Paginator<T> GetDefaultInstance()
        {
            return new Paginator<T>(1, 10, 0, []);
        }
    }
}
