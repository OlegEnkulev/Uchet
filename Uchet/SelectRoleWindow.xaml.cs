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
using System.Windows.Shapes;
using Uchet.Pages;
using Uchet.Resources;

namespace Uchet
{
    public partial class SelectRoleWindow : Window
    {
        public SelectRoleWindow()
        {
            InitializeComponent();
        }

        private void StudBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.SecondFrame.Navigate(new StudentPage());
        }

        private void PrepBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.SecondFrame.Navigate(new TeacherPage());
        }

        private void AdminBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.SecondFrame.Navigate(new AdminPage());
        }
    }
}
