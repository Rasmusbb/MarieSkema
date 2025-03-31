namespace Marie.DTOs
{
    public class DayScheduleDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Education { get; set; }
        public Guid TeacherID { get; set; }
        public Guid SubjectID { get; set; }
        public int TPHours { get; set; }
    }

    public class DayScheduleDTOID : DayScheduleDTO
    {
        public Guid DayScheduleID { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }

    }
}
