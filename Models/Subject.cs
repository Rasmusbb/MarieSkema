using System.ComponentModel.DataAnnotations;

namespace Marie.Models
{
    public class Subject
    {
        [Key]
        public Guid SubjectID { get; set; }
        public Guid Name { get; set; }
        public DateTime Days { get; set; }
        public Guid DaysCount { get; set; }
        public Guid Hourse { get; set; }
    }
}
