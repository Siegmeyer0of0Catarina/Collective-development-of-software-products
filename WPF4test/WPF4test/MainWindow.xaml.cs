using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace WPF4test
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            labelForDate.DataContext = new DateForLab();
        }
    }
    public class DateForLab
    {
        public DateTime Date { get { return DateTime.Now; } }
    }
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object
        parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return string.Format("Дата: {0}", date.ToShortDateString());
        }
        public object ConvertBack(object value, Type targetType, object
        parameter, CultureInfo culture)
        {
            return null;
        }
    }
}