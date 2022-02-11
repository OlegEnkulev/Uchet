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

            if(Core.currentUser.Role == 0)
            {
                TeacherBox.ItemsSource = Core.DB.Users.Where(u => u.Role == 1).Select(u => u.FirstName + " " + u.LastName).ToArray();
            }
            else
            {
                TeacherBox.IsEnabled = false;
            }

            GroupsDG.ItemsSource = Core.DB.Groups.ToArray();
            
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
            if(Core.currentUser.Role == 0)
            {
                TeacherBox.SelectedItem = currentSubject.Users.FirstName + " " + currentSubject.Users.LastName;
            }
            SelectedGroupsDG.ItemsSource = Core.DB.SubjectGroups.Where(g => g.SubjectId ==  currentSubject.Id).ToList();
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

            currentSubject.Title = TitleBox.Text;
            currentSubject.Description = DescriptionBox.Text;
            currentSubject.TeacherId = Core.DB.Users.Where(u => u.FirstName + " " + u.LastName == TeacherBox.SelectedItem).FirstOrDefault().Id;

            Core.DB.Subjects.Add(currentSubject);
            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new SubjectsControlPage());
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new SubjectsControlPage());
        }

        private void DeleteGroupBTN_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedGroupsDG.SelectedItems == null)
            {
                MessageBox.Show("Не выбрано ни одного элемента");
                return;
            }

            for (int i = 0; i < SelectedGroupsDG.SelectedItems.Count; i++)
            {
                Core.DB.SubjectGroups.Remove((SubjectGroups)SelectedGroupsDG.SelectedItems[i]);
            }
        }

        private void SelectGroupBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDG.SelectedItems == null)
            {
                MessageBox.Show("Не выбрано ни одного элемента");
                return;
            }

            for (int i = 0; i < GroupsDG.SelectedItems.Count; i++)
            {
                int o = 0;
                while (true)
                {
                    if (Core.DB.SubjectGroups.Where(s => s.Id == o).FirstOrDefault() == null)
                    {
                        SubjectGroups newSelect = new SubjectGroups() { GroupId = ((Groups)GroupsDG.SelectedItems[i]).Id, SubjectId = currentSubject.Id, Id = o};
                        Core.DB.SubjectGroups.Add(newSelect);
                        break;
                    }
                    o++;
                }
            }
        }
    }
}
