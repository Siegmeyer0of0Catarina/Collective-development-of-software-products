using System;

class Program
{
    static void Main()
    {
        // Начальное количество амеб
        int startingAmoebas = 1;

        // Время в минутах
        int timeInMinutes = 60;

        // Расчет количества амеб через указанное время
        int finalAmoebas = startingAmoebas * (int)Math.Pow(2, timeInMinutes);

        Console.WriteLine("Через {0} минут в банке будет {1} амеб.", timeInMinutes, finalAmoebas);

        // Дополнительный расчет для случая, когда в банку посадили 2 амебы
        int startingAmoebas2 = 2;
        int finalAmoebas2 = startingAmoebas2 * (int)Math.Pow(2, timeInMinutes);

        Console.WriteLine("Если в банку посадить 2 амебы, через {0} минут будет {1} амеб.", timeInMinutes, finalAmoebas2);
    }
}
