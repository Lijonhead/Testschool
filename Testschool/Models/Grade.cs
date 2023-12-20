using System;
using System.Collections.Generic;

namespace Testschool.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public int? Grade1 { get; set; }

    public DateTime GradeDate { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student { get; set; }
}
