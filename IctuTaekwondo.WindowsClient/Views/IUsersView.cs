using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.WindowsClient.Views
{
    public interface IUsersView
    {
        UserFullDetailResponse User { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }

        event EventHandler SearchEvent;
        event EventHandler RegisterEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetBindingSource(BindingSource bindingSource);
        void Show();
        void Hide();
        void Close();
    }
}
