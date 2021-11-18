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
    public partial class SubjectsControlPage : Page
    {
        public SubjectsControlPage()
        {
            InitializeComponent();
            SubjectsDataGrid.ItemsSource = Core.DB.Subjects.ToList();
        }

        private void CreateSubjectBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new AddNewSubject(-1));
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            SubjectsDataGrid.ItemsSource = Core.DB.Subjects.ToList();
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsDataGrid.SelectedItem == null)
            {
                return;
            }

            int id = 0;

            while (true)
            {
                if (SubjectsDataGrid.SelectedItem == Core.DB.Subjects.Where(s => s.Id == id).FirstOrDefault())
                {
                    break;
                }
                id++;
            }

            Core.mainWindow.MainFrame.Navigate(new AddNewSubject(id));
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsDataGrid.SelectedItem == null)
            {
                return;
            }

            Subjects deleteSubject = Core.DB.Subjects.Where(s => s.Id == SubjectsDataGrid.SelectedIndex).FirstOrDefault();
            Core.DB.Subjects.Remove(deleteSubject);
            Core.DB.SaveChanges();
            SubjectsDataGrid.ItemsSource = Core.DB.Subjects.ToList();
        }
    }
}
