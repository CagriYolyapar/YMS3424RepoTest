using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.WinUI.LoginForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            appUserRepository = new AppUserRepository();
        }

        AppUserRepository appUserRepository;

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            if (appUserRepository.Any(x => x.UserName == txtUserName.Text
                                        && x.Password == txtPassword.Text
                                        && x.Role == MODEL.Enums.UserRole.Admin))
            {
                ChooseForm adminForm = new ChooseForm();
                this.Hide();
                adminForm.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı!!");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
