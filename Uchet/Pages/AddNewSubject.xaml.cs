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
    public partial class AddNewSubject : Page
    {
        Subjects currentSubject;
        bool isCreated = true;
        public AddNewSubject(int userId)
        {
            InitializeComponent();

            TeacherBox.ItemsSource = Core.DB.Users.Where(u => u.Role == 1).Select(u => u.FirstName + " " + u.LastName).ToArray();
            GroupBox.ItemsSource = Core.DB.Groups.Select(g => g.Name).ToArray();

            if (userId == -1)
            {
                isCreated = false;
                return;
            }

            currentSubject = Core.DB.Subjects.Where(s => s.Id == userId).FirstOrDefault();

            if (currentSubject == null)
            {
                MessageBox.Show("Акак?");
                return;
            }

            TitleBox.Text = currentSubject.Title;
            DescriptionBox.Text = currentSubject.Description;
            TeacherBox.SelectedItem = currentSubject.Users.FirstName + " " + currentSubject.Users.LastName;
            GroupBox.SelectedItem = currentSubject.Groups.Name;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreated)
            {
                int o = 0;
                while (true)
                {
                    if (Core.DB.Subjects.Where(s => s.Id == o).FirstOrDefault() == null)
                    {
                        currentSubject = new Subjects { Id = o };
                        break;
                    }
                    o++;
                }
            }
            else
            {
                Core.DB.Subjects.Remove(Core.DB.Subjects.Where(s => s.Id == currentSubject.Id).FirstOrDefault());
                Core.DB.SaveChanges();
            }

            currentSubject.Title = TitleBox.Text;
            currentSubject.Description = DescriptionBox.Text;
            currentSubject.TeacherId = Core.DB.Users.Where(u => u.FirstName + " " + u.LastName == TeacherBox.SelectedItem).FirstOrDefault().Id;
            currentSubject.GroupId = Core.DB.Groups.Where(g => g.Name == GroupBox.SelectedItem).Select(g => g.Id).FirstOrDefault();

            Core.DB.Subjects.Add(currentSubject);
            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new SubjectsControlPage());
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new SubjectsControlPage());
        }
    }
}
