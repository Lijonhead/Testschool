using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testschool.Models
{
    public static class StaffFunctions
    {
        public static void AddNewStaff()
        {
            Console.WriteLine("Enter the following details for the new staff member:");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Job Title (e.g., Teacher, Administrator, etc.): ");
            string jobTitle = Console.ReadLine();

            
            var newStaff = new Personnel
            {
                FirstName = firstName,
                LastName = lastName,
                Job = jobTitle
            };

            
            using (var context = new ReallytestSchoolContext())
            {
                context.Personnel.Add(newStaff);
                context.SaveChanges();
            }

            Console.WriteLine("New staff member added successfully!");
        }

        public static void GetStaff()
        {
            using (var context = new ReallytestSchoolContext())
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Get all staff members");
                Console.WriteLine("2. Get staff members by job title");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllStaff(context);
                        break;
                    case "2":
                        DisplayStaffByJobTitle(context);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        private static void DisplayAllStaff(ReallytestSchoolContext context)
        {
            var staffMembers = context.Personnel.ToList();
            if (staffMembers.Any())
            {
                Console.WriteLine("List of all staff members:");
                foreach (var staff in staffMembers)
                {
                    Console.WriteLine($"ID: {staff.Id}, Name: {staff.FirstName} {staff.LastName}, Job Title: {staff.Job}");
                }
            }
            else
            {
                Console.WriteLine("No staff members found.");
            }
        }
        private static void DisplayStaffByJobTitle(ReallytestSchoolContext context)
        {
            Console.Write("Enter job title to filter staff members: ");
            string jobTitle = Console.ReadLine();

            var staffMembers = context.Personnel.Where(p => p.Job == jobTitle).ToList();
            if (staffMembers.Any())
            {
                Console.WriteLine($"List of staff members with job title '{jobTitle}':");
                foreach (var staff in staffMembers)
                {
                    Console.WriteLine($"ID: {staff.Id}, Name: {staff.FirstName} {staff.LastName}, Job Title: {staff.Job}");
                }
            }
            else
            {
                Console.WriteLine($"No staff members found with job title '{jobTitle}'.");
            }
        }
    }
}
