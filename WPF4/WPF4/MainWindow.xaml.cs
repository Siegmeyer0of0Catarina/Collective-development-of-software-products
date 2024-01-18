using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WPF4
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
    }
    class CovertorInDifferentNumberSystems : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return DependencyProperty.UnsetValue;
            }
            if (!int.TryParse(value.ToString(), out int number))
            {
                return DependencyProperty.UnsetValue;
            }
            if (!int.TryParse(parameter.ToString(), out int system))
            {
                return DependencyProperty.UnsetValue;
            }
            return ConvertDifferentNumberSystems(number, system);
        }
        public object ConvertBack(object value, Type targetType, object
        parameter, CultureInfo culture)
        {
            string number = value as string;
            if (number == null || parameter == null)
            {
                return DependencyProperty.UnsetValue;
            }
            if (!int.TryParse(parameter.ToString(), out int system))
            {
                return DependencyProperty.UnsetValue;
            }
            return ConvertingToDecimal(number, system);
        }
        private static string ConvertDifferentNumberSystems(int number, int
        system)
        {
            string convertedNumber = "";
            while (number > 0)
            {
                int remainder = number % system;
                convertedNumber = (remainder < 10 ? remainder.ToString() :
                ((char)(remainder - 10 + 'A')).ToString()) + convertedNumber;
                number = number / system;
            }
            return convertedNumber;
        }
        private static int ConvertingToDecimal(string number, int system)
        {
            int result = 0;
            for (int i = 0; i < number.Length; i++)
            {
                char digit = number[i];
                int value = (digit >= '0' && digit <= '9') ? (digit - '0') : (digit - 'A' +
                10);
                result = result * system + value;
            }
            return result;
        }
    }
    public class RomanNumeralConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }
            if (!int.TryParse(value.ToString(), out int number))
            {
                return DependencyProperty.UnsetValue;
            }
            string romanNumeral = ToRoman(number);
            return romanNumeral;
        }
        public object ConvertBack(object value, Type targetType, object
parameter, CultureInfo culture)
        {
            string romanNumeral = value as string;
            if (romanNumeral == null)
            {
                return DependencyProperty.UnsetValue;
            }
            int number = ToArabic(romanNumeral);
            return number;
        }
        public static string ToRoman(int number)
        {
            (int number, string symbol)[] table = new (int num, string symbol)[15]
            { (1,"I"), (4,"IV"), (5,"V"), (9,"IV"),(10,"X"), (40,"XL"), (50,"L"),
(90,"XC"),
(100,"C"), (400,"CD"), (500,"D"), (900,"CM"), (1000,"M"),
(10000,"MM"), (100000,"MMM")
            };
            string result = "";
            int N = number;
            int i = 14;
            while (N > 0)
            {
                while (table[i].number > N)
                {
                    i--;
                }
                result += table[i].symbol;
                N -= table[i].number;
            }
            return result;
        }
        public static int ToArabic(string romanNumber)
        {
            (int number, string symbol)[] table = new (int num, string symbol)[15]
            { (1,"I"), (4,"IV"), (5,"V"), (9,"IV"),(10,"X"), (40,"XL"), (50,"L"),
(90,"XC"),
(100,"C"), (400,"CD"), (500,"D"), (900,"CM"),(1000,"M"),
(10000,"MM"), (100000,"MMM")
            };
            int i = 14;
            int p = 1;
            int res = 0;
            while (p <= romanNumber.Length)
            {
                while (romanNumber.Substring(p - 1, table[i].symbol.Length) !=
                table[i].symbol)
                {
                    i--;
                    if (i == 0) break;
                }
                res += table[i].number;
                p += table[i].symbol.Length;
            }
            return res;
        }
    }
}


        

