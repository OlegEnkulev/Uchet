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
        List<UsersGrades> userGrades;
        List<float> grades;
        float averageGrade;
        int skipsCount;
        int skipsUpCount;
        int skipsNeupCount;

        public StudentPage()
        {
            InitializeComponent();

            SubjectsBox.ItemsSource = Core.DB.UsersInSubjects.Where(x => x.UserId == Core.currentUser.Id).Select(s => s.Subjects.Name).ToArray();
            CleanUp();
        }

        void CleanUp()
        {
            userGrades = new List<UsersGrades>();
            grades = new List<float>();
            averageGrade = 0;
            skipsCount = 0;
            skipsUpCount = 0;
            skipsNeupCount = 0;
        }

        void Cycle()
        {
            for (int i = 0; i < userGrades.Count; i++)
            {
                switch (userGrades[i].GradeId)
                {
                    case 0:
                        grades.Add(2);
                        break;
                    case 1:
                        grades.Add(3);
                        break;
                    case 2:
                        grades.Add(4);
                        break;
                    case 3:
                        grades.Add(5);
                        break;
                    case 4:
                        skipsCount++;
                        skipsUpCount++;
                        break;
                    case 5:
                        skipsCount++;
                        skipsNeupCount++;
                        break;
                    case 6:
                        skipsCount++;
                        skipsUpCount++;
                        break;
                }
            }
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            CleanUp();

            if (SubjectsBox.SelectedItem == null && DateFirstDatePicker.SelectedDate == null && DateLastDatePicker.SelectedDate == null)
            {
                userGrades = Core.DB.UsersGrades.Where(g => g.UserId == Core.currentUser.Id).ToList();
                Cycle();
                GradesDataGrid.ItemsSource = userGrades;
            }
            else if (SubjectsBox.SelectedItem != null && DateFirstDatePicker.SelectedDate == null && DateLastDatePicker.SelectedDate == null)
            {
                userGrades = Core.DB.UsersGrades.Where(g => g.Topics.Subjects.Name == SubjectsBox.SelectedItem && g.UserId == Core.currentUser.Id).ToList();
                Cycle();
                GradesDataGrid.ItemsSource = userGrades;
            }
            else if (SubjectsBox.SelectedItem == null && DateFirstDatePicker.SelectedDate != null && DateLastDatePicker.SelectedDate != null)
            {
                userGrades = Core.DB.UsersGrades.Where(g => g.UserId == Core.currentUser.Id && g.Topics.Date > DateFirstDatePicker.SelectedDate && g.Topics.Date < DateLastDatePicker.SelectedDate).ToList();
                Cycle();
                GradesDataGrid.ItemsSource = userGrades;
            }
            else if (SubjectsBox.SelectedItem != null && DateFirstDatePicker.SelectedDate != null && DateLastDatePicker.SelectedDate != null)
            {
                userGrades = Core.DB.UsersGrades.Where(g => g.Topics.Subjects.Name == SubjectsBox.SelectedItem && g.UserId == Core.currentUser.Id && g.Topics.Date > DateFirstDatePicker.SelectedDate && g.Topics.Date < DateLastDatePicker.SelectedDate).ToList();
                Cycle();
                GradesDataGrid.ItemsSource = userGrades;
            }
            else
            {
                MessageBox.Show("Вы не уточнили некоторые параметры!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SkipsCountLabel.Content = "Кол-во пропусков: " + skipsCount.ToString();
            skipsUpCountLabel.Content = "По УП: " + skipsUpCount.ToString();
            skipsNeupCountLabel.Content = "По НеУП:" + skipsNeupCount.ToString();

            if (SubjectsBox.SelectedItem != null)
            {
                if(grades.Count != 0)
                {
                    averageGrade = grades.Sum() / grades.Count();
                    AverageGradeLabel.Content = "Средняя оценка: " + averageGrade.ToString();
                }
            }
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.ExitUser();
        }
    }
}
