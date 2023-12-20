using System;
using System.Collections.Generic;

namespace Testschool.Models;

public partial class Course
{
    public int Id { get; set; }

    public string? CourseName { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
