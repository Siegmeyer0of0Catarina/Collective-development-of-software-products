using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{

    public static class Translator
{
    public static string Thousands(double number)
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
}
}
