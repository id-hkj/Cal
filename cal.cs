using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace main_Program
{
    class Program
    {
        static string[] Month_cal(int m, int y)
        {
            string[] days = { "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su" };
            string[] months = { "       January       ", "      February       ", "        March        ",
                                "        April        ", "         May         ", "        June         ",
                                "        July         ", "       August        ", "      September      ",
                                "       October       ", "      November       ", "      December       "};
            DateTime FirstOfMonth = new DateTime(y, m, 1);
            DateTime Today = DateTime.Today;
            string one_code = Convert.ToString(FirstOfMonth.DayOfWeek).Substring(0, 2);
            string big_days = "";
            bool prev = false;
            string[] final = { months[m - 1], " " + String.Join(' ', days), "", "", "", "", "", "" };

            foreach (string day in days)
            {
                if (one_code == day)
                {
                    break;
                }
                big_days += "   ";
            }
            for (int d = 1; d <= System.DateTime.DaysInMonth(y, m); d++)
            {
                DateTime test_day = new DateTime(y, m, d);
                bool next = test_day == Today;

                if (next)
                {
                    if (d < 10) { big_days += " ["; }
                    else { big_days += "["; }
                    prev = true;
                }
                else if (prev)
                {
                    if (d < 10) { big_days += "] "; }
                    else { big_days += "]"; }
                    prev = false;
                }
                else
                {
                    if (d < 10) { big_days += "  "; }
                    else { big_days += " "; }
                }
                big_days += Convert.ToString(d);
            }
            while (big_days.Length < 126)
            {
                big_days += ' ';
            }
            for (int i = 0; i < big_days.Length; i += 21)
            {
                final[(i / 21) + 2] = big_days.Substring(i, 21);
            }
            return final;
        }

        static string[] Year_cal(int y)
        {
            string[] final = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            string[,] months = { {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""},
                                 {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""},
                                 {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""},
                                 {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}};
            for (int i = 1; i < 13; i++)
            {
                string[] month = Month_cal(i, y);
                for (int j = 0; j < 8; j++)
                {
                    months[i - 1, j] = month[j];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    string appender = "";
                    for (int k = 0; k < 4; k++)
                    {
                        appender += months[(4 * i) + k, j];
                        appender += "   ";
                    }
                    final[(9 * i) + j] = appender;
                }
            }

            return final;
        }

        static bool check(int max, string argument)
        {
            int int_arg = Convert.ToInt32(argument);
            if (int_arg >=1 & int_arg <= max) {
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                try {
                    if (arg == "-h" | arg == "--help")
                    {
                        Console.WriteLine("cal - outputs a calendar for the current month.");
                        Console.WriteLine("cal year - outputs a calendar for the year specified.");
                        Console.WriteLine("cal month year - outputs a calendar for the month of the year specified.");
                        Console.WriteLine("");
                        Console.WriteLine("month - an integer between 1 and 12");
                        Console.WriteLine("year - an integer between 1 and 9999");
                        return;
                    }
                    else { int a = Convert.ToInt32(arg); }
                }
                catch (FormatException)
                {
                    Console.WriteLine("ERROR: Parameters provided have to be integers.");
                    return;
                }
            }

            DateTime date = DateTime.Now;
            if (args.Length == 0)
            {
                string[] Curr_Month = Month_cal(date.Month, date.Year);
                Console.WriteLine(String.Join('\n', Curr_Month));
            }
            else if (args.Length == 1)
            {
                if (check(9999, args[0]))
                {
                    Console.WriteLine("ERROR: Year needs to be between 1 and 9999");
                    return;
                }
                string[] Curr_Year = Year_cal(Convert.ToInt32(args[0]));
                Console.WriteLine(String.Join('\n', Curr_Year));
            }
            else if (args.Length == 2)
            {
                if (check(12, args[0]))
                {
                    Console.WriteLine("ERROR: Month needs to be between 1 and 12");
                    return;
                }
                else if (check(9999, args[1]))
                {
                    Console.WriteLine("ERROR: Year needs to be between 1 and 9999");
                    return;
                }
                string[] Month = Month_cal(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
                Console.WriteLine(String.Join('\n', Month));
            }
        }
    }
}
