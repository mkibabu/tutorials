namespace ContosoUniversity.Migrations
{
    using ContosoUniversity.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContosoUniversity.DataAccessLayer.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContosoUniversity.DataAccessLayer.SchoolContext context)
        {
            // create list of students
            var students = new List<Student>
            {
                new Student { FirstMidName = "Carson", LastName = "Alexander", 
                    EnrollmentDate = DateTime.Parse("2010-09-01")},
                new Student { FirstMidName = "Meredith", LastName = "Alonso", 
                    EnrollmentDate = DateTime.Parse("2012-09-01")},
                new Student { FirstMidName = "Arturo",   LastName = "Anand",     
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Gytis",    LastName = "Barzdukas", 
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Yan",      LastName = "Li",        
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Peggy",    LastName = "Justice",   
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstMidName = "Laura",    LastName = "Norman",    
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Nino",     LastName = "Olivetto",  
                    EnrollmentDate = DateTime.Parse("2005-08-11") },
                new Student { FirstMidName = "Brad", LastName = "Thornton", 
                    EnrollmentDate = DateTime.Parse("2000-09-01")}
            };
            // add each student to the db
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            // save changes
            context.SaveChanges();

            // repeat...
            var courses = new List<Course>
            {
                new Course {ID = 1050, Title = "Chemistry", Credits = 3, },
                new Course {ID = 4022, Title = "Microeconomics", Credits = 3, },
                new Course {ID = 4041, Title = "Macroeconomics", Credits = 3, },
                new Course {ID = 1045, Title = "Calculus", Credits = 4, },
                new Course {ID = 3141, Title = "Trigonometry", Credits = 4, },
                new Course {ID = 2021, Title = "Composition",Credits = 3, },
                new Course {ID = 2042, Title = "Literature", Credits = 4, }
            };

            courses.ForEach(c => context.Courses.AddOrUpdate(p => p.Title, c));

            context.SaveChanges();

            var enrollments = new List<Enrollment> 
            {
                new Enrollment 
                {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").ID,
                    Grade = Grade.A
                },

                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").ID,
                    Grade = Grade.C
                },

                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics").ID,

                },

                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics").ID,
                    Grade = Grade.B
                },

                new Enrollment 
                { 
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus").ID, 
                    Grade = Grade.B 
                },

                new Enrollment
                { 
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry").ID, 
                    Grade = Grade.B 
                 },

                 new Enrollment
                 {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").ID, 
                    Grade = Grade.B 
                 },

                 new Enrollment 
                 { 
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").ID
                    // Grade purposefully left out; is nullable
                 },

                 new Enrollment 
                 { 
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").ID,
                    Grade = Grade.B         
                 },

                new Enrollment
                { 
                    StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").ID,
                    Grade = Grade.B         
                 },

                 new Enrollment 
                 { 
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").ID,
                    Grade = Grade.B         
                 },

                 new Enrollment 
                 { 
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").ID,
                    Grade = Grade.B         
                 },

                 new Enrollment 
                 { 
                    StudentID = students.Single(s => s.LastName == "Thornton").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").ID,
                    Grade = Grade.D         
                 },

                 new Enrollment
                 { 
                    StudentID = students.Single(s => s.LastName == "Thornton").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").ID,
                    Grade = Grade.C         
                 }
            };

            foreach (Enrollment e in enrollments)
            {
            var enrollmentInDatabase = context.Enrollments.Where (s => 
                s.Student.ID == e.StudentID && s.Course.ID == e.CourseID)
                .SingleOrDefault();
                     
                if(enrollmentInDatabase == null)
                {
                    context.Enrollments.Add(e);
                }
            }

        }
    }
}
