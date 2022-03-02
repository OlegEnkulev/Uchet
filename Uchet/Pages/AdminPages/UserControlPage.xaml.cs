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

            Update();
        }

        void Update()
        {
            GCB.ItemsSource = Core.DB.Groups.ToList();
            RCB.ItemsSource = Core.DB.Roles.ToList();
            DG.ItemsSource = Core.DB.Users.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem == null)
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Core.DB.Entry(DG.SelectedItem).State = System.Data.Entity.EntityState.Added;
                Core.DB.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Update();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem == null)
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Core.DB.Entry(DG.SelectedItem).State = System.Data.Entity.EntityState.Modified;
                Core.DB.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Update();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem == null)
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Core.DB.Entry(DG.SelectedItem).State = System.Data.Entity.EntityState.Deleted;
                Core.DB.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Update();
        }
    }
}
