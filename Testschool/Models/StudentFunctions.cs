using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testschool.Models
{
    public static class StudentFunctions
    {
        public static void GetStudents()
        {
            Console.WriteLine("Choose sorting order:");
            Console.WriteLine("1. Ascending");
            Console.WriteLine("2. Descending");
            Console.Write("Enter your choice: ");
            bool isAscending = Console.ReadLine() == "1";

            Console.WriteLine("Choose sorting by:");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.Write("Enter your choice: ");
            bool sortByFirstName = Console.ReadLine() == "1";

            using (var context = new ReallytestSchoolContext())
            {
                var query = context.Students.AsQueryable();

                if (sortByFirstName)
                {
                    query = isAscending ? query.OrderBy(s => s.FirstName) : query.OrderByDescending(s => s.FirstName);
                }
                else
                {
                    query = isAscending ? query.OrderBy(s => s.LastName) : query.OrderByDescending(s => s.LastName);
                }

                var students = query.ToList();

                Console.WriteLine("List of all students:");
                foreach (var student in students) 
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}");
                }
            }
        }
        

        public static void GetStudentsInClass()
        {
            using (var context = new ReallytestSchoolContext())
            {
                
                Console.WriteLine("List of all classes:");
                var classes = context.Classes.ToList();
                foreach (var schoolClass in classes)
                {
                    Console.WriteLine($"ID: {schoolClass.Id}, Class Name: {schoolClass.ClassName}");
                }

                
                Console.Write("Enter Class ID to view students: ");
                int classId;
                while (!int.TryParse(Console.ReadLine(), out classId) || !classes.Any(c => c.Id == classId))
                {
                    Console.WriteLine("Invalid Class ID. Please try again.");
                    Console.Write("Enter Class ID to view students: ");
                }

                
                var students = context.Students.Where(s => s.ClassId == classId).ToList();

                if (students.Any())
                {
                    Console.WriteLine($"List of students in Class ID {classId}:");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("No students found for the selected class.");
                }
            }
        }
        public static void AddNewStudent()
        {
            using (var context = new ReallytestSchoolContext())
            {
                Console.WriteLine("Enter the details for the new student:");

                Console.Write("First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Last Name: ");
                string lastName = Console.ReadLine();

                Console.WriteLine("Available Classes:");
                var classes = context.Classes.ToList();
                foreach (var schoolClass in classes)
                {
                    Console.WriteLine($"ID: {schoolClass.Id}, Name: {schoolClass.ClassName}");
                }

                Console.Write("Enter the Class ID for the student: ");
                if (!int.TryParse(Console.ReadLine(), out int classId))
                {
                    Console.WriteLine("Invalid input. Exiting.");
                    return;
                }

                // Create a new student instance and set its properties
                var newStudent = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    ClassId = classId
                };

                // Add the new student to the Students DbSet
                context.Students.Add(newStudent);
                // Save changes to the database
                context.SaveChanges();

                Console.WriteLine($"New student {newStudent.FirstName} {newStudent.LastName} added successfully!");
            }
        }
    }
}
