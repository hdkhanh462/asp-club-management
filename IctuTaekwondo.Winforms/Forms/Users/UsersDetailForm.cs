using System;
using System.Windows.Forms;
using IctuTaekwondo.Winforms.Utils;

namespace IctuTaekwondo.Winforms.Forms.Users
{
    public partial class UsersDetailForm : Form
    {
        private readonly IApiService api;
        
        private JwtResponse Jwt;

        public UsersDetailForm(IApiService api)
        {
            InitializeComponent();
            this.api = api;
        }

        private  void UsersDetailForm_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadAvatarImage(string avatarUrl)
        {
            
        }

        private  void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
           
        }

        private  void btnSave_Click(object sender, EventArgs e)
        {
           
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }
    }
}
