using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testschool.Models
{
    public static class GradeFunctions
    {
        public static void GetGradesLastMonth()
        {
            using (var context = new ReallytestSchoolContext())
            {
                
                var lastMonthStartDate = DateTime.Now.AddMonths(-1).Date;
                
                var lastMonthEndDate = DateTime.Now.Date;

                
                var gradesLastMonth = context.Grades
                    .Include(g => g.Student)
                    .Include(g => g.Course)
                    .Where(g => g.GradeDate >= lastMonthStartDate && g.GradeDate <= lastMonthEndDate)
                    .ToList();

                if (gradesLastMonth.Any())
                {
                    Console.WriteLine("List of grades set in the last month:");
                    foreach (var grade in gradesLastMonth)
                    {
                        
                        var student = context.Students.SingleOrDefault(s => s.Id == grade.StudentId);
                        var course = context.Courses.SingleOrDefault(c => c.Id == grade.CourseId);

                        if (student != null && course != null)
                        {
                            Console.WriteLine($"Student Name: {student.FirstName} {student.LastName}, " +
                                              $"Course Name: {course.CourseName}, " +
                                              $"Grade: {grade.Grade1}");
                        }
                        else
                        {
                            Console.WriteLine($"Grade ID {grade.Id} has missing Student or Course information.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No grades found that were set in the last month.");
                }
            }
        }

        public static void GetAverageGradePerCourse()
        {
            using (var context = new ReallytestSchoolContext())
            {
                var averageGrades = context.Grades
                    .GroupBy(g => g.CourseId)
                    .Select(g => new
                    {
                        CourseId = g.Key,
                        AverageGrade = g.Average(x => x.Grade1 ?? 0),
                        HighestGrade = g.Max(x => x.Grade1 ?? 0),
                        LowestGrade = g.Min(x => x.Grade1 ?? 0)
                    })
                    .ToList();

                if (averageGrades.Any())
                {
                    Console.WriteLine("Average Grade per Course:");
                    foreach (var gradeInfo in averageGrades)
                    {
                        var course = context.Courses.FirstOrDefault(c => c.Id == gradeInfo.CourseId);
                        if (course != null)
                        {
                            Console.WriteLine($"Course Name: {course.CourseName}");
                            Console.WriteLine($"Average Grade: {gradeInfo.AverageGrade:F2}");
                            Console.WriteLine($"Highest Grade: {gradeInfo.HighestGrade}");
                            Console.WriteLine($"Lowest Grade: {gradeInfo.LowestGrade}");
                            Console.WriteLine("--------------");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No grades found for calculating averages.");
                }
            }
        }
    }
}
