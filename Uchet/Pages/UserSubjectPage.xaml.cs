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
    public partial class UserSubjectPage : Page
    {
        public UserSubjectPage()
        {
            InitializeComponent();

            SubjectsBox.ItemsSource = Core.DB.Subjects.Select(s => s.Title).ToArray();
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            if(SubjectsBox.SelectedItem == null)
            {
                MessageBox.Show("Не выбран предмет");
                return;
            }

            GradesDataGrid.ItemsSource = Core.DB.UsersGrades.Where(g => g.Subjects.Title == SubjectsBox.SelectedItem && g.StudentId == Core.currentUser.Id && g.Date > DateFirstDatePicker.SelectedDate && g.Date < DateLastDatePicker.SelectedDate).ToList();
        }
    }
}
