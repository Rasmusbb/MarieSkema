namespace Marie.DTOs
{
    public class SubjectDTO
    {
        public string Name { get; set; }
        public string Education { get; set; }
        public Guid TeacherID { get; set; }
        public int TotalHours { get; set; }
        public int TotalTPHours { get; set; }
        public int UsedTPHours { get; set; }
    }

    public class SubjectDTOID : SubjectDTO
    {
        public Guid SubjectID { get; set; }
    }   
}
