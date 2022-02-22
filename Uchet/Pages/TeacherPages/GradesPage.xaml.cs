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

namespace Uchet.Pages.TeacherPages
{
    public partial class GradesPage : Page
    {
        public GradesPage()
        {
            InitializeComponent();

            SubjectsBox.ItemsSource = Core.DB.Subjects.Where(x => x.CreatorId == Core.currentUser.Id).Select(x => x.Name).ToArray();
            GradeBox.ItemsSource = Core.DB.Grades.Select(x => x.Name).ToArray();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(DG.SelectedItems == null)
            {
                MessageBox.Show("Вы не выбрали студентов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (GradeBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали оценку!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            for (int i = 0; i < DG.SelectedItems.Count; i++)
            {
                try
                {
                    UsersInSubjects user = DG.SelectedItems[i] as UsersInSubjects;

                    UsersGrades newGrade = new UsersGrades() { TopicId = Core.DB.Topics.Where(x => x.Name == TopicsBox.SelectedItem).FirstOrDefault().Id, UserId = user.Users.Id, GradeId = Core.DB.Grades.Where(x => x.Name == GradeBox.SelectedItem).FirstOrDefault().Id };

                    Core.DB.UsersGrades.Add(newGrade);
                    Core.DB.SaveChanges();

                    MessageBox.Show("Оценки добавлены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            if(SubjectsBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали предмет!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TopicsBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали топик!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DG.ItemsSource = Core.DB.UsersInSubjects.Where(x => x.Subjects.Name == SubjectsBox.SelectedItem && x.Subjects.CreatorId == Core.currentUser.Id).ToList();
        }

        private void SubjectsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TopicsBox.ItemsSource = Core.DB.Topics.Where(x => x.Subjects.Name == SubjectsBox.SelectedItem && x.Subjects.CreatorId == Core.currentUser.Id).Select(x => x.Name).ToArray();
            TopicsBox.IsEnabled = true;
        }
    }
}
