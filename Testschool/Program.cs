using Testschool.Models;

namespace Testschool
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

                bool exit = false;

            do
            {
                Menu.DisplayMainMenu();
                
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        StudentFunctions.GetStudents();
                        break;
                    case "2":
                        StudentFunctions.GetStudentsInClass();
                        break;
                    case "3":
                        StaffFunctions.AddNewStaff();
                        break;
                    case "4":
                        StaffFunctions.GetStaff();
                        break;
                    case "5":
                        GradeFunctions.GetGradesLastMonth();
                        break;
                    case "6":
                        GradeFunctions.GetAverageGradePerCourse();
                        break;
                    case "7":
                        StudentFunctions.AddNewStudent();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("Press Enter to return to the main menu.");
                Console.ReadLine();
            } while (!exit);
        }  
    }
}
