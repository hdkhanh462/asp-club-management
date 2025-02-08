using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IctuTaekwondo.WindowsClient.Utils
{
    public interface IView 
    {
        void SetPresenter(IPresenter presenter);
        void Show();
        void Hide();
        void Close();
    }
}
