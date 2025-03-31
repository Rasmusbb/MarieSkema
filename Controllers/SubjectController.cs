using Mapster;
using Marie.DTOs;
using Marie.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : Controller
    {

        private readonly DatabaseContext _context;

        public SubjectController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("AddSubject")]
        public async Task<ActionResult<SubjectDTOID>> AddSubject(SubjectDTO subjectDTO)
        {


            if (_context.Subjects == null)
            {
                return Problem("Entity set 'DatabaseContext.Subjects'  is null.");
            }
            Subject subject = subjectDTO.Adapt<Subject>();
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            SubjectDTOID subjectDTOID = subject.Adapt<SubjectDTOID>();

            return CreatedAtAction("GetSubject", new { id = subject.SubjectID }, subjectDTOID);
        }

        [HttpGet("GetSubject")]
        public async Task<ActionResult<SubjectDTO>> GetSubject(Guid subjectID)
        {
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'DatabaseContext.subject'");
            }
            Subject subject = await _context.Subjects.FindAsync(subjectID);
            SubjectDTO subjectDTO = subject.Adapt<SubjectDTO>();
            return subjectDTO;
        }

        [HttpGet("GetAllSubjects")]
        public async Task<ActionResult<List<SubjectDTOID>>> GetAllSubjects(string Education)
        {
            List<SubjectDTOID> subjectsDTOID = _context.Subjects.Where(x => x.Education == Education).ToList().Adapt<List<SubjectDTOID>>();
            return subjectsDTOID;
        }


        [HttpPut("EditedSubject")]
        public async Task<ActionResult<TeacherDTO>> EditedTeacher(Guid SubjectID, SubjectDTO subjectDTO)
        {
            Subject subject = await _context.Subjects.FindAsync(SubjectID);
            subject = subjectDTO.Adapt<Subject>();
            await _context.SaveChangesAsync();
            return Ok("Teacher infomation updated");
        }

        [HttpDelete("DeleteSubject")]
        public async Task<ActionResult<List<TeacherDTOID>>> DeleteTeacher(Guid subjectID)
        {
            _context.Remove(await _context.Subjects.FindAsync(subjectID));
            await _context.SaveChangesAsync();
            return Ok("Subject was Deleted");
        }
    }
}
