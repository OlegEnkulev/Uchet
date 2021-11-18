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
    public partial class AddNewGroup : Page
    {
        Groups currentGroup;
        bool isCreated = true;
        public AddNewGroup(int userId)
        {
            InitializeComponent();

            if (userId == -1)
            {
                isCreated = false;
                return;
            }

            currentGroup = Core.DB.Groups.Where(s => s.Id == userId).FirstOrDefault();

            if (currentGroup == null)
            {
                MessageBox.Show("Акак?");
                return;
            }

            NameBox.Text = currentGroup.Name;
            DescriptionBox.Text = currentGroup.Description;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreated)
            {
                int o = 0;
                while (true)
                {
                    if (Core.DB.Groups.Where(s => s.Id == o).FirstOrDefault() == null)
                    {
                        currentGroup = new Groups { Id = o };
                        break;
                    }
                    o++;
                }
            }
            else
            {
                Core.DB.Groups.Remove(Core.DB.Groups.Where(s => s.Id == currentGroup.Id).FirstOrDefault());
                Core.DB.SaveChanges();
            }

            currentGroup.Name = NameBox.Text;
            currentGroup.Description = DescriptionBox.Text;

            Core.DB.Groups.Add(currentGroup);
            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new GroupsControlPage());
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new GroupsControlPage());
        }
    }
}
