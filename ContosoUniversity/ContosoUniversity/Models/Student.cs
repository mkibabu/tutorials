using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID {  get; set; }    // primary key
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // navigation property
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}