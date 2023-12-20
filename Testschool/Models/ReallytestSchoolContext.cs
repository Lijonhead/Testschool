using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Testschool.Models;

public partial class ReallytestSchoolContext : DbContext
{
    public ReallytestSchoolContext()
    {
        
    }

    public ReallytestSchoolContext(DbContextOptions<ReallytestSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=ReallytestSchool;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Classes_pk");

            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Courses_pk");

            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Grades_pk");

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.Grade1).HasColumnName("Grade");
            entity.Property(e => e.StudentId).HasColumnName("Student_Id");

            entity.HasOne(d => d.Course).WithMany(p => p.Grades)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("Grades_Courses");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("Grades_Students");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Personnel_pk");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Students_pk");

            entity.Property(e => e.ClassId).HasColumnName("Class_Id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Students_Classes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
