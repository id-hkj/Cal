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
            string one_code = Convert.ToString(FirstOfMonth.DayOfWeek).Substring(0, 2);
            string big_days = "";
            string[] final = { months[m - 1], " " + String.Join(' ', days), "", "", "", "", "", ""};

            foreach (string day in days)
            {
                if (one_code == day)
                {
                    break;
                }
                big_days += "   ";
            }
            for (int i = 1; i <= System.DateTime.DaysInMonth(y, m); i++)
            {
                if (i < 10)
                {
                    big_days += " ";
                }
                big_days += " ";
                big_days += Convert.ToString(i);
            }
            while (big_days.Length < 126)
            {
                big_days += ' ';
            }
            for (int i=0; i<big_days.Length; i += 21)
            {
                final[(i/21)+2] = big_days.Substring(i, 21);
            }
            return final;
        }

        static string[] Year_cal(int y)
        {
            string[] final = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            string[,] months = { {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""},
                                 {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""},
                                 {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""},
                                 {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}, {"", "", "", "", "", "", "", "", ""}};
            for (int i=1; i < 13; i++)
            {
                string[] month = Month_cal(i, y);
                for (int j=0; j < 8; j++)
                {
                    months[i - 1, j] = month[j];
                }
            }

            for (int i=0; i<3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    string appender = "";
                    for (int k = 0; k < 4; k++)
                    {
                        appender += months[(4 * i) + k, j];
                        appender += "   ";
                    }
                    final[(9*i) + j] = appender;
                }
            }

            return final;
        }
        static void Main(string[] args)
        {
            DateTime date = DateTime.Now;
            if (args.Length == 0)
            {
                string[] Curr_Month = Month_cal(date.Month, date.Year);
                Console.WriteLine(String.Join('\n', Curr_Month));
            }
            else if (args.Length == 1)
            {
                string[] Curr_Year = Year_cal(Convert.ToInt32(args[0]));
                Console.WriteLine(String.Join('\n', Curr_Year));
            }
            else if (args.Length == 2)
            {
                string[] Month = Month_cal(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
                Console.WriteLine(String.Join('\n', Month));
            }
        }
    }
}
