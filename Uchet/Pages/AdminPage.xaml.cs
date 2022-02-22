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
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void UserControlBTN_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new UserControlPage());
        }

        private void GroupsControlBTN_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new GroupsControlPage());
        }

        private void SubjectsControlBTN_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new SubjectsControlPage());
        }

        private void TopicsControlBTN_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new TopicsControlPage());
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.ExitUser();
        }
    }
}
