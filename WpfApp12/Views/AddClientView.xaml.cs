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
using System.Windows.Shapes;
using WpfApp12.Models;
using WpfApp12.ViewModels;

namespace WpfApp12.Views
{
    /// <summary>
    /// Логика взаимодействия для AddClientView.xaml
    /// </summary>
    public partial class AddClientView : Window
    {
        public Guests Guest { get; set; }

        public AddClientView(Guests guest)
        {
            InitializeComponent();
            Guest = guest;
            DataContext = Guest;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
