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
using WpfApp12.Views;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((MainWindow)System.Windows.Application.Current.MainWindow).mainGrid.Children.Clear();
            ((MainWindow)System.Windows.Application.Current.MainWindow).mainGrid.Children.Add(new SearchView());
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).mainGrid.Children.Clear();
            ((MainWindow)System.Windows.Application.Current.MainWindow).mainGrid.Children.Add(new SearchView());
        }

        private void ListBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).mainGrid.Children.Clear();
            ((MainWindow)System.Windows.Application.Current.MainWindow).mainGrid.Children.Add(new ClientView());
        }

        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).mainGrid.Children.Clear();
            ((MainWindow)System.Windows.Application.Current.MainWindow).mainGrid.Children.Add(new ReservationView());
        }
    }
}
