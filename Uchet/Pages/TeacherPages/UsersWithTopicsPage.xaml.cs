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
    public partial class UsersWithTopicsPage : Page
    {
        public static Subjects currentSubject;
        public UsersWithTopicsPage()
        {
            InitializeComponent();

            DGLeft.ItemsSource = Core.DB.Users.Where(x => x.RoleId == 2).ToList();
            SubjectsBox.ItemsSource = Core.DB.Subjects.Where(x => x.CreatorId == Core.currentUser.Id).Select(x => x.Name).ToArray();
        }

        private void RightBTN_Click(object sender, RoutedEventArgs e)
        {
            if (DGLeft.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали пользователя в левой колонке!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            for(int i = 0; i < DGLeft.SelectedItems.Count; i++)
            {
                Users selectedUser = DGLeft.SelectedItems[i] as Users;

                UsersInSubjects newRelationship = new UsersInSubjects() { SubjectId = currentSubject.Id, UserId = selectedUser.Id };

                if(Core.DB.UsersInSubjects.Any(u => u == newRelationship))
                {
                    try
                    {
                        Core.DB.UsersInSubjects.Add(newRelationship);
                        Core.DB.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось добавить пользователя " + selectedUser.LastName + " " + selectedUser.FirstName + "!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            UpdateRight();
        }

        private void LeftBTN_Click(object sender, RoutedEventArgs e)
        {
            if (DGRight.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали пользователя в правой колонке!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            for (int i = 0; i < DGRight.SelectedItems.Count; i++)
            {
                UsersInSubjects currentRelationship = DGRight.SelectedItems[i] as UsersInSubjects;

                Core.DB.UsersInSubjects.Remove(currentRelationship);
                Core.DB.SaveChanges();
            }

            UpdateRight();
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            if(SubjectsBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали предмет!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentSubject = Core.DB.Subjects.Where(x => x.Name == SubjectsBox.SelectedItem && x.CreatorId == Core.currentUser.Id).FirstOrDefault();

            UpdateRight();
        }

        void UpdateRight()
        {
            DGRight.ItemsSource = Core.DB.UsersInSubjects.Where(x => x.SubjectId == currentSubject.Id).ToList();
        }
    }
}
