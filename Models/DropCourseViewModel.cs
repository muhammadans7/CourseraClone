using System.Collections.Generic;

namespace courseraClone.Models
{
    public class DropCourseViewModel
    {
        public string Email { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}