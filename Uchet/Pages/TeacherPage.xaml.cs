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
using Uchet.Pages.TeacherPages;

namespace Uchet.Pages
{
    public partial class TeacherPage : Page
    {

        public TeacherPage()
        {
            InitializeComponent();

            IdLabel.Content = "Ваш айди: " + Core.currentUser.Id;
        }

        private void SubjectsBTN_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new SubjectsControlPage());
        }

        private void TopicsBTN_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new TopicsControlPage());
        }

        private void UsersTopicsBTN_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new UsersWithTopicsPage());
        }

        private void GradesBTN_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new GradesPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Core.ExitUser();
        }

        private void CheckGradesBTN_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new CheckGradesPage());
        }
    }
}
