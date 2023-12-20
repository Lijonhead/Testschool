using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testschool.Models
{
    public static class Menu
    {
         public static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Get all students");
            Console.WriteLine("2. Get all students in a certain class");
            Console.WriteLine("3. Add new staff");
            Console.WriteLine("4. Get staff");
            Console.WriteLine("5. Get all grades set in the last month");
            Console.WriteLine("6. Average grade per course");
            Console.WriteLine("7. Add new students");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }
    }
}
