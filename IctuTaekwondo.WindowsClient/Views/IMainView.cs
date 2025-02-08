using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.WindowsClient.Views
{
    public interface IMainView
    {
        UserFullDetailResponse User { get; set; }

        event EventHandler LogoutEvent;
        event EventHandler ShowLoginViewEvent;
        event EventHandler ShowUsersViewEvent;

        void Show();
        void Hide();
        void Close();
    }
}
