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
using Uchet.Resources;
using Uchet.Pages;


namespace Uchet.Pages
{
    public partial class GroupsControlPage : Page
    {
        public GroupsControlPage()
        {
            InitializeComponent();
            GroupsDataGrid.ItemsSource = Core.DB.Groups.ToList();
        }

        private void CreateGroupBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new AddNewGroup(-1));
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            GroupsDataGrid.ItemsSource = Core.DB.Groups.ToList();
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem == null)
            {
                return;
            }

            int id = 0;

            while (true)
            {
                if (GroupsDataGrid.SelectedItem == Core.DB.Groups.Where(s => s.Id == id).FirstOrDefault())
                {
                    break;
                }
                id++;
            }

            Core.mainWindow.MainFrame.Navigate(new AddNewGroup(id));
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem == null)
            {
                return;
            }

            Groups deleteGroup = Core.DB.Groups.Where(s => s.Id == GroupsDataGrid.SelectedIndex).FirstOrDefault();
            Core.DB.Groups.Remove(deleteGroup);
            Core.DB.SaveChanges();
            GroupsDataGrid.ItemsSource = Core.DB.Groups.ToList();
        }
    }
}
