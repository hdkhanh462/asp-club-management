using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.WindowsClient.Utils;

namespace IctuTaekwondo.WindowsClient.Views
{
    public interface ILoginView : IView
    {
        string EmailValue { get; set; }
        string PasswordValue { get; set; }
        bool RememberMeValue { get; set; }

        event EventHandler LoginEvent;

        
    }
}
