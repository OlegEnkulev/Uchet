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
using Uchet.Resources;
using Uchet.Pages;


namespace Uchet.Pages
{
    public partial class UserControlPage : Page
    {
        public UserControlPage()
        {
            InitializeComponent();
            UsersDataGrid.ItemsSource = Core.DB.Users.ToList();
        }

        private void CreateUserBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new AddNewUser(-1));
        }

        private void RefreshBTN_Click(object sender, RoutedEventArgs e)
        {
            UsersDataGrid.ItemsSource = Core.DB.Users.ToList();
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem == null)
            {
                return;
            }

            int id = 0;

            while (true)
            {
                if (UsersDataGrid.SelectedItem == Core.DB.Users.Where(s => s.Id == id).FirstOrDefault())
                {
                    break;
                }
                id++;
            }

            Core.mainWindow.MainFrame.Navigate(new AddNewUser(id));
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem == null)
            {
                return;
            }

            Users deleteUser = Core.DB.Users.Where(s => s.Id == UsersDataGrid.SelectedIndex).FirstOrDefault();
            Core.DB.Users.Remove(deleteUser);
            Core.DB.SaveChanges();
            UsersDataGrid.ItemsSource = Core.DB.Users.ToList();
        }
    }
}
