﻿using System;
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
    public partial class UserSubjectPage : Page
    {
        public UserSubjectPage()
        {
            InitializeComponent();

            SubjectsBox.ItemsSource = Core.DB.Subjects.Where(s => s.GroupId == Core.currentUser.GroupID).Select(s => s.Title).ToArray();
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            if(SubjectsBox.SelectedItem == null)
            {
                MessageBox.Show("Не выбран предмет");
                return;
            }



            List<Grades> grades = new List<Grades>();

            int iCorrect = 0;
            for (int i = 0; i < Core.DB.Grades.Count(); i++)
            {
                if (Core.DB.Grades.Where(g => g.Subjects.Title == SubjectsBox.SelectedItem && g.Subjects.GroupId == Core.currentUser.GroupID && g.Id == iCorrect && g.StudentId == Core.currentUser.Id && g.Date > DateFirstDatePicker.SelectedDate && g.Date < DateLastDatePicker.SelectedDate).FirstOrDefault() != null)
                {
                    grades.Add(Core.DB.Grades.Where(g => g.Id == iCorrect).FirstOrDefault());
                }
                else
                    i--;
                iCorrect++;
            }

            GradesDataGrid.ItemsSource = grades;
        }
    }
}
