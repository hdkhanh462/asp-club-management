using IctuTaekwondo.Shared.Responses;
using NuGet.Packaging;

namespace IctuTaekwondo.WebClient.Models
{
    public class TableFooterViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public int CurrentItemCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public List<int> ItemRange { get; set; } = [0, 0];
        public List<int> DisplayPageRange { get; set; } = [];
    }
}
