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
    public partial class TeacherSubjectPage : Page
    {
        public TeacherSubjectPage()
        {
            InitializeComponent();
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            List<Grades> grades = new List<Grades>();

            int iCorrect = 0;
            for (int i = 0; i < Core.DB.Grades.Count(); i++)
            {
                if (Core.DB.Grades.Where(g => g.Id == iCorrect && g.UserId == Core.currentUser.Id && g.Date > DateFirstDatePicker.SelectedDate && g.Date < DateLastDatePicker.SelectedDate).FirstOrDefault() != null)
                {
                    grades.Add(Core.DB.Grades.Where(g => g.Id == iCorrect).FirstOrDefault());
                }
                else
                    i--;
                iCorrect++;
            }

            GradesDataGrid.ItemsSource = grades;
        }

        private void AddGrades_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
