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

namespace WPF

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
        public string ConvertNumberToWords(double numb)
        {
            int number = (int)numb;
            if (number == 0)
                return "ноль";

            if (number < 0)
                return "минус " + ConvertNumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + WPF.Translator.Thousands(number / 1000);
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " сто ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "и ";

                string[] unitsMap = new string[]
                {
                    "ноль", "один", "два", "три", "четыре", "пять", "шесть",
                    "семь", "восемь", "девять", "десять", "одиннадцать",
                    "двенадцать", "тринадцать", "четырнадцать", "пятнадцать",
                    "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать"
                };

                string[] tensMap = new string[]
                {
                    "ноль", "десять", "двадцать", "тридцать", "сорок", "пятьдесят",
                    "шестьдесят", "семьдесят", "восемьдесят", "девяносто"
                };

                if (number < 20)
                    words += unitsMap[(int)number];
                else
                {

                    words += tensMap[(int)number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[(int)number % 10];
                }
            }

            return words;
        }
        public string Thousands(int number)
        {
            if (number == 1)
            {
                return " тысяча ";
            }
            if (number > 1 && number < 5)
            {
                return " тысячи ";
            }
            if (number > 4)
            {
                return " тысяч ";
            }
            return "";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }
            txtbx.Text += "1";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }
            txtbx.Text += "2";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }
            txtbx.Text += "3";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }
            txtbx.Text += "4";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }


            txtbx.Text += "5";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }
            txtbx.Text += "6";
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }
            txtbx.Text += "7";
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }
            txtbx.Text += "8";
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == "0")
            {
                txtbx.Text = "";
            }
            txtbx.Text += "9";
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text != "0")
                txtbx.Text += "0";
        }
        private void eql_Click(object sender, RoutedEventArgs e)
        {
            double res = Evaluate(txtbx.Text);
            string result = ConvertNumberToWords(res);
            txtbx.Text = $"{result}";
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text != "")
            {
                txtbx.Text += "+";
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text != "")
            {
                txtbx.Text += "-";
            }
        }

        private void multiply_Click(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text != "")
            {
                txtbx.Text += "*";
            }
        }

        private void division_Click(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text != "")
            {
                txtbx.Text += "/";
            }
        }
        static double Evaluate(string input)
        {
            string rpn = ConvertToRPN(input);
            Stack<double> stack = new Stack<double>();

            foreach (string token in rpn.Split(' '))
            {
                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else if (IsOperator(token))
                {
                    double operand2 = stack.Pop();
                    double operand1 = stack.Pop();
                    double result = ApplyOperator(token, operand1, operand2);
                    stack.Push(result);
                }
            }

            return stack.Pop();
        }

        static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }

        static double ApplyOperator(string op, double a, double b)
        {
            switch (op)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "*": return a * b;
                case "/": return a / b;
                default: throw new ArgumentException("Invalid operator: " + op);
            }
        }

        static string ConvertToRPN(string input)
        {
            string output = "";
            Stack<string> stack = new Stack<string>();

            foreach (char c in input)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    output += c;

                }
                else if (IsOperator(c.ToString()))
                {
                    while (stack.Count > 0 && IsOperator(stack.Peek()) && GetPrecedence(c.ToString()) <= GetPrecedence(stack.Peek()))
                    {
                        output += " " + stack.Pop();
                    }
                    output += " ";
                    stack.Push(c.ToString());
                }
                else if (c == '(')
                {
                    stack.Push(c.ToString());
                }
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        output += " " + stack.Pop();
                    }
                    stack.Pop();
                }
            }

            while (stack.Count > 0)
            {
                output += " " + stack.Pop();
            }

            return output.Trim();
        }

        static int GetPrecedence(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                default:
                    throw new ArgumentException("Invalid operator: " + op);
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            txtbx.Text = "";
        }
    }
}

   