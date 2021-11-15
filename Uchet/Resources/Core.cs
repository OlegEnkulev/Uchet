using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uchet.Pages;
using Uchet.Resources;

namespace Uchet.Resources
{
    public static class Core
    {
        public static MainWindow mainWindow;
        public static UchetDBEntities DB = new UchetDBEntities();
        public static Users currentUser;

        public static void OpenUserPage()
        {
            if(currentUser == null)
            {
                return;
            }

            switch (currentUser.Role)
            {
                case 0:
                    mainWindow.SecondFrame.Navigate(new AdminPage());
                    break;
                case 1:
                    mainWindow.SecondFrame.Navigate(new TeacherPage());
                    break;
                case 2:
                    mainWindow.SecondFrame.Navigate(new StudentPage());
                    break;
            }
        }

        public static void ExitUser()
        {
            currentUser = null;

            mainWindow.MainFrame.Navigate(new NewsPage());
            mainWindow.SecondFrame.Navigate(new LoginPage());
        }
    }
}
