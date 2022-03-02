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
using Uchet.Pages.TeacherPages;

namespace Uchet.Pages
{
    public partial class CheckGradesPage : Page
    {
        public CheckGradesPage()
        {
            InitializeComponent();

            SubjectsBox.ItemsSource = Core.DB.Subjects.Where(x => x.CreatorId == Core.currentUser.Id).Select(x => x.Name).ToArray();
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали предмет!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TopicsBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали топик!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            GradesDataGrid.ItemsSource = Core.DB.UsersGrades.Where(x => x.Topics.Name == TopicsBox.SelectedItem).ToList();
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GradesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Core.DB.Entry(GradesDataGrid.SelectedItem).State = System.Data.Entity.EntityState.Deleted;
                Core.DB.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            GradesDataGrid.ItemsSource = Core.DB.UsersGrades.Where(x => x.Topics.Name == TopicsBox.SelectedItem).ToList();
        }

        private void SubjectsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TopicsBox.ItemsSource = Core.DB.Topics.Where(x => x.Subjects.Name == SubjectsBox.SelectedItem && x.Subjects.CreatorId == Core.currentUser.Id).Select(x => x.Name).ToArray();
            TopicsBox.IsEnabled = true;
        }
    }
}
