using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
        // primary key
        public int ID { get; set; }
 
        // nullable; null grade != zero grade
        public Grade? Grade { get; set; }

        // foreign keys
        public int CourseID { get; set; }   
        public int StudentID { get; set; } 

        // navigation properties
        // one-to-one relationship with both
        public virtual Course Course { get; set; } 
        public virtual Student Student { get; set; }
    }
}