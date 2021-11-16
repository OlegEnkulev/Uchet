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
    public partial class AddNewNews : Page
    {
        News currentNew;
        bool isCreated = true;
        public AddNewNews(int newId)
        {
            InitializeComponent();

            if(newId == -1)
            {
                isCreated = false;
                return;
            }

            currentNew = Core.DB.News.Where(s => s.Id == newId).FirstOrDefault();

            if(currentNew == null)
            {
                MessageBox.Show("Акак?");
                return;
            }

            TitleBox.Text = currentNew.Title;
            DescriptionBox.Text = currentNew.Description;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if(!isCreated)
            {
                int o = 0;
                while (true)
                {
                    if(Core.DB.News.Where(s => s.Id == o).FirstOrDefault() == null)
                    {
                        currentNew = new News { Id = o };
                        break;
                    }
                    o++;
                }
            }
            else
            {
                Core.DB.News.Remove(Core.DB.News.Where(s => s.Id == currentNew.Id).FirstOrDefault());
                Core.DB.SaveChanges();
            }

            currentNew.Title = TitleBox.Text;
            currentNew.Description = DescriptionBox.Text;
            currentNew.Date = DateTime.Now;

            Core.DB.News.Add(currentNew);
            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new NewsControlPage());
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new NewsControlPage());
        }
    }
}
