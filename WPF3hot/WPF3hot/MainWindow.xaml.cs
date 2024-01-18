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


namespace WPF3hot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    private void One(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Вы нажали \'Ctrl+U\'");
    }
    private void Two(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Вы нажали \'Ctrl+Alt+E\'");
    }
    private void Three(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Вы нажали \'Win+S\'");
    }
    private void bOne_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}


