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
    public partial class StudentPage : Page
    {

        public StudentPage()
        {
            InitializeComponent();

            SubjectsBox.ItemsSource = Core.DB.UsersInSubjects.Where(x => x.UserId == Core.currentUser.Id).Select(s => s.Subjects.Name).ToArray();
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsBox.SelectedItem == null && DateFirstDatePicker.SelectedDate == null && DateLastDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Не выбран предмет");

                GradesDataGrid.ItemsSource = Core.DB.UsersGrades.Where(g => g.UserId == Core.currentUser.Id).ToList();
            }
            else if (SubjectsBox.SelectedItem != null && DateFirstDatePicker.SelectedDate == null && DateLastDatePicker.SelectedDate == null)
            {
                GradesDataGrid.ItemsSource = Core.DB.UsersGrades.Where(g => g.Topics.Subjects.Name == SubjectsBox.SelectedItem && g.UserId == Core.currentUser.Id).ToList();
            }
            else if (SubjectsBox.SelectedItem == null && DateFirstDatePicker.SelectedDate != null && DateLastDatePicker.SelectedDate != null)
            {
                GradesDataGrid.ItemsSource = Core.DB.UsersGrades.Where(g => g.UserId == Core.currentUser.Id && g.Topics.Date > DateFirstDatePicker.SelectedDate && g.Topics.Date < DateLastDatePicker.SelectedDate).ToList();
            }
            else if (SubjectsBox.SelectedItem != null && DateFirstDatePicker.SelectedDate != null && DateLastDatePicker.SelectedDate != null)
            {
                GradesDataGrid.ItemsSource = Core.DB.UsersGrades.Where(g => g.Topics.Subjects.Name == SubjectsBox.SelectedItem && g.UserId == Core.currentUser.Id && g.Topics.Date > DateFirstDatePicker.SelectedDate && g.Topics.Date < DateLastDatePicker.SelectedDate).ToList();
            }
            else
            {
                MessageBox.Show("Вы не уточнили некоторые параметры!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
