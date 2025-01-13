namespace Marie.DTOs
{
    public class TeacherDTO
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Educating { get; set; }
    }

    public class TeacherDTOID : TeacherDTO
    {
        public Guid TeacherID { get; set; }
    }
}
