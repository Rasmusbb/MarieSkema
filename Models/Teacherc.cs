using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Marie.Models
{
    public class Teacher
    {
        [Key]
        public Guid TeacherID { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }

        public string Educating { get; set; }
        public ICollection<DaySchedule> Days { get; set; }

    }
}
