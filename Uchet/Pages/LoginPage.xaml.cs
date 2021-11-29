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
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length < 4 || PasswordBox.Password.Length < 4)
            {
                MessageBox.Show("Не введены данные");
                return;
            }

            if (Core.DB.Users.Where(s => s.Login.Equals(LoginBox.Text) && s.Password.Equals(PasswordBox.Password)).FirstOrDefault() == null)
            {
                MessageBox.Show("Данные неверны");
                return;
            }

            Core.currentUser = Core.DB.Users.Where(s => s.Login == LoginBox.Text && s.Password == PasswordBox.Password).FirstOrDefault();

            Core.OpenUserPage();

            //int count = 96;
            //Random rnd = new Random();

            //int iCorrect = 0;
            //for(int i = 0; i < count; i++)
            //{
            //    if (Core.DB.Grades.Where(g => g.Id == iCorrect).FirstOrDefault() == null)
            //    {
            //        int[] studIds = Core.DB.Users.Where(u => u.Role == 2).Select(u => u.Id).ToArray();
            //        int currentStudentId = studIds[rnd.Next(0, studIds.Count() - 1)];
            //        Users currentStudent = Core.DB.Users.Where(u => u.Id == currentStudentId).FirstOrDefault();

            //        int[] subIds = Core.DB.Subjects.Where(s => s.GroupId == currentStudent.GroupID).Select(u => u.Id).ToArray();
            //        int currentSubjectId = subIds[rnd.Next(0, subIds.Count() - 1)];
            //        Subjects currentSubject = Core.DB.Subjects.Where(s => s.Id == currentSubjectId).FirstOrDefault();

            //        Grades grade = new Grades { Id = iCorrect,  Grade = rnd.Next(2, 5), Date = DateTime.Today, StudentId = currentStudentId, SubjectID = currentSubjectId, UserId = currentSubject.TeacherId};

            //        Core.DB.Grades.Add(grade);
            //        Core.DB.SaveChanges();
            //        MessageBox.Show(count.ToString());
            //    }
            //    else
            //        i--;
            //    iCorrect++;
            //}

            //MessageBox.Show("Готово!");
        }
    }
}
