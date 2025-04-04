﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marie.Models
{
    public class DaySchedule
    {
        [Key]
        public Guid DayScheduleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Education { get; set; }
        [ForeignKey("TeacherID")]
        public Guid TeacherID { get; set; }
        [ForeignKey("SubjectID")]
        public Guid SubjectID { get; set; }
        public int TPHours { get; set; }
    }
}
