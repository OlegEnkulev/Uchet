using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Uchet.Pages;
using Uchet.Resources;

namespace Uchet.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            if(LoginBox.Text.Length < 4 || PasswordBox.Password.Length < 4)
            {
                MessageBox.Show("Не введены данные");
                return;
            }

            if(Core.DB.Users.Where(s => s.Login == LoginBox.Text && s.Password == PasswordBox.Password).FirstOrDefault() == null)
            {
                MessageBox.Show("Данные неверны");
                return;
            }

            Core.currentUser = Core.DB.Users.Where(s => s.Login == LoginBox.Text && s.Password == PasswordBox.Password).FirstOrDefault();

            Core.OpenUserPage();
        }
    }
}
