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
        int subjectCount;

        Button[] subjectButton = new Button[Core.DB.Subjects.Where(s => s.GroupId == Core.currentUser.GroupID).Count()];

        public StudentPage()
        {
            InitializeComponent();
            //UpdateSubjects();
        }

        //void UpdateSubjects()
        //{
        //    subjectCount = Core.DB.Subjects.Where(s => s.GroupId == Core.currentUser.GroupID).Count();

        //    SubjectSP.Children.Clear();

        //    int iCorrect = 0;

        //    for (int i = 0; i < subjectCount; i++)
        //    {
        //        if (Core.DB.Subjects.Where(s => s.Id == iCorrect && s.GroupId == Core.currentUser.GroupID).FirstOrDefault() != null)
        //        {
        //            Subjects subject = Core.DB.Subjects.Where(s => s.Id == iCorrect).FirstOrDefault();

        //            subjectButton[i] = new Button();
        //            subjectButton[i].Margin = new Thickness(5);
        //            subjectButton[i].Content = subject.Title;
        //            subjectButton[i].Tag = iCorrect;
        //            subjectButton[i].Click += OpenSubjectPage;

        //            SubjectSP.Children.Add(subjectButton[i]);
        //        }
        //        else
        //            i--;
        //        iCorrect++;
        //    }

        //    Button exitButton = new Button();
        //    exitButton.Margin = new Thickness(5);
        //    exitButton.Content = "Выход";
        //    exitButton.Click += ExitBTN_Click;

        //    SubjectSP.Children.Add(exitButton);
        //}

        //public void OpenSubjectPage(object sender, EventArgs e)
        //{

        //}

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.ExitUser();
        }

        private void ShowGradesBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new UserSubjectPage());
        }
    }
}
