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
    public partial class NewsPage : Page
    {
        Border[] border = new Border[Core.DB.News.Count()];
        StackPanel[] stackPanel = new StackPanel[Core.DB.News.Count()];
        Grid[] grid = new Grid[Core.DB.News.Count()];
        Label[] label = new Label[Core.DB.News.Count()];
        Label[] label1 = new Label[Core.DB.News.Count()];
        Border[] border1 = new Border[Core.DB.News.Count()];
        TextBlock[] textBlock = new TextBlock[Core.DB.News.Count()];

        public NewsPage()
        {
            InitializeComponent();

            UpdateNews();
        }

        void UpdateNews()
        {
            NewsSP.Children.Clear();

            int iCorrect = 0;

            for (int i = 0; i < Core.DB.News.Count(); i++)
            {
                if (Core.DB.News.Where(s => s.Id == iCorrect).FirstOrDefault() != null)
                {
                    News news = Core.DB.News.Where(s => s.Id == iCorrect).FirstOrDefault();

                    textBlock[i] = new TextBlock();
                    textBlock[i].Margin = new Thickness(5);
                    textBlock[i].Foreground = Brushes.White;
                    textBlock[i].Text = news.Description;
                    textBlock[i].TextWrapping = TextWrapping.Wrap;

                    border1[i] = new Border();
                    border1[i].Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#776993");
                    border1[i].CornerRadius = new CornerRadius(10);
                    border1[i].BorderThickness = new Thickness(2);
                    border1[i].BorderBrush = Brushes.White;
                    border1[i].Margin = new Thickness(5);
                    border1[i].Child = textBlock[i];

                    label[i] = new Label();
                    label[i].FontSize = 14;
                    label[i].Content = news.Title;
                    label[i].Foreground = Brushes.White;
                    label[i].FontWeight = FontWeights.Bold;
                    label[i].HorizontalAlignment = HorizontalAlignment.Left;
                    label[i].Width = 350;

                    label1[i] = new Label();
                    label1[i].FontSize = 14;
                    label1[i].Content = news.Date.ToString("dd.MM.yyyy");
                    label1[i].Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#635679");
                    label1[i].FontWeight = FontWeights.Light;
                    label1[i].HorizontalAlignment = HorizontalAlignment.Right;

                    grid[i] = new Grid();
                    grid[i].Children.Add(label[i]);
                    grid[i].Children.Add(label1[i]);

                    stackPanel[i] = new StackPanel();
                    stackPanel[i].Children.Add(grid[i]);
                    stackPanel[i].Children.Add(border1[i]);

                    border[i] = new Border();
                    border[i].Margin = new Thickness(5);
                    border[i].Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#3d3549");
                    border[i].Width = 500;
                    border[i].CornerRadius = new CornerRadius(10);
                    border[i].BorderThickness = new Thickness(2);
                    border[i].BorderBrush = Brushes.White;
                    border[i].Child = stackPanel[i];

                    NewsSP.Children.Insert(0, border[i]);
                }
                else
                    i--;
                iCorrect++;
            }
        }
    }
}
