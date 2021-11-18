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
    public partial class AddNewUser : Page
    {
        Users currentUser;
        bool isCreated = true;
        public AddNewUser(int userId)
        {
            InitializeComponent();

            GroupBox.ItemsSource = Core.DB.Groups.Select(g => g.Name).ToArray();

            if (userId == -1)
            {
                isCreated = false;
                return;
            }

            currentUser = Core.DB.Users.Where(s => s.Id == userId).FirstOrDefault();

            if (currentUser == null)
            {
                MessageBox.Show("Акак?");
                return;
            }

            FirstNameBox.Text = currentUser.FirstName;
            LastNameBox.Text = currentUser.LastName;
            LoginBox.Text = currentUser.Login;
            PasswordBox.Text = currentUser.Password;
            RoleBox.SelectedIndex = currentUser.Role;
            GroupBox.SelectedItem = currentUser.Groups.Name;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreated)
            {
                int o = 0;
                while (true)
                {
                    if (Core.DB.Users.Where(s => s.Id == o).FirstOrDefault() == null)
                    {
                        currentUser = new Users { Id = o };
                        break;
                    }
                    o++;
                }
            }
            else
            {
                Core.DB.Users.Remove(Core.DB.Users.Where(s => s.Id == currentUser.Id).FirstOrDefault());
                Core.DB.SaveChanges();
            }

            currentUser.FirstName = FirstNameBox.Text;
            currentUser.LastName = LastNameBox.Text;
            currentUser.Login = LoginBox.Text;
            currentUser.Password = PasswordBox.Text;
            currentUser.Role = RoleBox.SelectedIndex;
            currentUser.GroupID = Core.DB.Groups.Where(g => g.Name == GroupBox.SelectedItem).Select(g => g.Id).FirstOrDefault();

            Core.DB.Users.Add(currentUser);
            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new UserControlPage());
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new UserControlPage());
        }
    }
}
