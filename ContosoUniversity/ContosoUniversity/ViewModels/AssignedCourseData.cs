using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    /// <summary>
    /// Describes a class used to store the courses that will be displayed on
    /// the Instructor.Edit page
    /// </summary>
    public class AssignedCourseData
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}