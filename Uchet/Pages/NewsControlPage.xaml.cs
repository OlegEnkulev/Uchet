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
    public partial class NewsControlPage : Page
    {
        public NewsControlPage()
        {
            InitializeComponent();
            NewsDataGrid.ItemsSource = Core.DB.News.ToList();
        }

        private void CreateUserBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new AddNewNews(-1));
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            NewsDataGrid.ItemsSource = Core.DB.News.ToList();
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if(NewsDataGrid.SelectedItem == null)
            {
                return;
            }

            int id = 0;

            while (true)
            {
                if (NewsDataGrid.SelectedItem == Core.DB.News.Where(s => s.Id == id).FirstOrDefault())
                {
                    break;
                }
                id++;
            }

            Core.mainWindow.MainFrame.Navigate(new AddNewNews(id));
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (NewsDataGrid.SelectedItem == null)
            {
                return;
            }

            News deleteNew = Core.DB.News.Where(s => s.Id == NewsDataGrid.SelectedIndex).FirstOrDefault();
            Core.DB.News.Remove(deleteNew);
            Core.DB.SaveChanges();
            NewsDataGrid.ItemsSource = Core.DB.News.ToList();
        }
    }
}
