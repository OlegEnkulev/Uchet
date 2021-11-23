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
    public partial class AddNewGrades : Page
    {
        Grades currentGrade;
        bool isCreated = true;
        public AddNewGrades(int userId)
        {
            InitializeComponent();


            GroupBox.ItemsSource = Core.DB.Groups.Select(g => g.Name).ToArray();

            if (userId == -1)
            {
                isCreated = false;
                return;
            }

            currentGrade = Core.DB.Grades.Where(s => s.Id == userId).FirstOrDefault();

            if (currentGrade == null)
            {
                MessageBox.Show("Акак?");
                return;
            }

            
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if(GradeBox.Text.Length == 0)
            {
                MessageBox.Show("Введите оценку от 1 до 5");
                return;
            }

            if (!isCreated)
            {
                int o = 0;
                while (true)
                {
                    if (Core.DB.Grades.Where(s => s.Id == o).FirstOrDefault() == null)
                    {
                        currentGrade = new Grades { Id = o };
                        break;
                    }
                    o++;
                }
            }
            else
            {
                Core.DB.Grades.Remove(Core.DB.Grades.Where(s => s.Id == currentGrade.Id).FirstOrDefault());
                Core.DB.SaveChanges();
            }

            currentGrade.Date = DateTime.Now;
            currentGrade.SubjectID = Core.DB.Subjects.Where(s => s.Title == SubjectBox.SelectedItem).Select(g => g.Id).FirstOrDefault();
            currentGrade.UserId = Core.currentUser.Id;
            currentGrade.StudentId = Core.DB.Users.Where(u => u.LastName == StudentBox.SelectedItem && u.Groups.Name == GroupBox.SelectedItem).FirstOrDefault().Id;
            try
            {
                currentGrade.Grade = Convert.ToInt32(GradeBox.Text);
            }
            catch
            {
                MessageBox.Show("Убедитесь, что оценка введена правильно(цифра от 1 до 5)");
                return;
            }

            Core.DB.Grades.Add(currentGrade);
            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new TeacherSubjectPage());
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new TeacherSubjectPage());
        }

        private void SubjectBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StudentBox.IsEnabled = true;
            GradeBox.IsEnabled = false;
            SaveBTN.IsEnabled = false;

            StudentBox.ItemsSource = Core.DB.Users.Where(u => u.Groups.Name == GroupBox.SelectedItem).Select(u => u.LastName).ToArray();
        }

        private void GroupBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubjectBox.IsEnabled = true;
            StudentBox.IsEnabled = false;
            GradeBox.IsEnabled = false;
            SaveBTN.IsEnabled = false;

            SubjectBox.ItemsSource = Core.DB.Subjects.Where(s => s.Groups.Name == GroupBox.SelectedItem && s.TeacherId == Core.currentUser.Id).Select(s => s.Title).ToArray();
        }

        private void StudentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GradeBox.IsEnabled = true;
            SaveBTN.IsEnabled = true;
        }
    }
}
