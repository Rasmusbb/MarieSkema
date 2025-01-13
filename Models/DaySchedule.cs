using System.ComponentModel.DataAnnotations;

namespace Marie.Models
{
    public class DaySchedule
    {
        [Key]
        public Guid DayID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}
