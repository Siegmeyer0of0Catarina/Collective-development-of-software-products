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

namespace WPF2testprogram
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
           System.Globalization.CultureInfo.GetCultureInfo("en-US");
            InitializeComponent();
        }
        private void bEnter_Click(object sender, RoutedEventArgs e)
        {
            if (tLogin.Text.Equals("ROOT") && pPassword.Password.Equals("toor"))
            {
                MessageBox.Show(Properties.Resources.LoginCorrect,
                Properties.Resources.Window,
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(Properties.Resources.LoginDiscorrect,
                Properties.Resources.Window,
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

