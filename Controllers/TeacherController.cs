using Marie.DTOs;
using Marie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using System.Data.Entity;
namespace Marie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TeacherController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("AddTeacher")]
        public async Task<ActionResult<TeacherDTOID>> AddTeacher(TeacherDTO TeacherDTO)
        {


            if (_context.Teachers == null)
            {
                return Problem("Entity set 'DatabaseContext.Teacher'  is null.");
            }
            Teacher Teacher = TeacherDTO.Adapt<Teacher>();
            _context.Teachers.Add(Teacher);
            await _context.SaveChangesAsync();
            TeacherDTOID TeacherDTOID = Teacher.Adapt<TeacherDTOID>();

            return CreatedAtAction("GetTeacher", new { id = Teacher.TeacherID },TeacherDTOID);
        }

        [HttpGet("GetTeacher")]
        public async Task<ActionResult<TeacherDTO>> GetTeacher(Guid TeacherID)
        {
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'DatabaseContext.Teachers'");
            }
            Teacher Teacher = await _context.Teachers.FindAsync(TeacherID);
            TeacherDTO TeacherDTO = Teacher.Adapt<TeacherDTO>();
            return TeacherDTO;
        }


        [HttpGet("GetAllTeachers")]
        public async Task<ActionResult<List<TeacherDTOID>>> GetAllTeachers(string Educating)
        {
            List<TeacherDTOID> Teachers = _context.Teachers.Where(x => x.Educating == Educating).ToList().Adapt<List<TeacherDTOID>>();
            return Teachers;
        }


    }
}

