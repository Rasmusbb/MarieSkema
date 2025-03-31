using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marie.Models
{
    public class Subject
    {
        [Key]
        public Guid SubjectID { get; set; }
        public string Name { get; set; }
        public string Education { get; set; }
        [ForeignKey("TeacherID")]
        public Guid TeacherID { get; set; }
        public int TotalHours { get; set; }
        public int TotalTPHours { get; set; }
        public int UsedTPHours { get; set; }
        public ICollection<DaySchedule> Days { get; set; }
    }
}
