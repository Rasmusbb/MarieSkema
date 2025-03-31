using Mapster;
using Marie.DTOs;
using Marie.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data.Entity;
using System.Data.OleDb;
using System.Security.Claims;

namespace Marie.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DayScheduleController : Controller
    {
        private readonly DatabaseContext _context;

        public DayScheduleController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("AddDaySchedule")]
        public async Task<ActionResult<DayScheduleDTOID>> AddDaySchedule(DayScheduleDTO DayScheduleDTO)
        {
            Subject subject = await _context.Subjects.FindAsync(DayScheduleDTO.SubjectID);
            Teacher Teacher = await _context.Teachers.FindAsync(DayScheduleDTO.TeacherID);
            if(subject == null)
            {
                return BadRequest("The subjectID do not exist");
            }
            if(Teacher == null)
            {
                Teacher = await _context.Teachers.FindAsync(subject.TeacherID);
                DayScheduleDTO.TeacherID = subject.TeacherID;
            }
            int TPHours = DayScheduleDTO.TPHours + subject.UsedTPHours;
            if (TPHours > subject.TotalHours)
            {
                if(subject.TotalHours == 0)
                {
                    return BadRequest("This subject has no TP time allocated");
                }
                return BadRequest("You have used all the TP hours");
            }

            await _context.SaveChangesAsync();
            if (_context.DaySchedules == null)
            {
                return Problem("Entity set 'DatabaseContext.Subjects'  is null.");
            }
            DaySchedule daySchedule = DayScheduleDTO.Adapt<DaySchedule>();
            _context.DaySchedules.Add(daySchedule);
            await _context.SaveChangesAsync();
            DayScheduleDTOID dayScheduleDTOID = daySchedule.Adapt<DayScheduleDTOID>();
            dayScheduleDTOID.TeacherName = Teacher.Name;
            dayScheduleDTOID.SubjectName = subject.Name;
            return CreatedAtAction("GetDaySchedule", new { id = daySchedule.SubjectID }, dayScheduleDTOID);
        }

        [HttpGet("GetDaySchedule")]
        public async Task<ActionResult<DayScheduleDTO>> GetDaySchedule(Guid dayScheduleID)
        {
            if (_context.DaySchedules == null)
            {
                return Problem("Entity set 'DatabaseContext.DaySchedules'");
            }
            DaySchedule daySchedule = await _context.DaySchedules.FindAsync(dayScheduleID);
            DayScheduleDTO dayScheduleDTO = daySchedule.Adapt<DayScheduleDTO>();
            return dayScheduleDTO;
        }


        [HttpGet("GetWeekSchedule")]
        public async Task<ActionResult<List<DayScheduleDTOID>>> GetDaySchedule(int weeknum = 0,int year = 0)
        {
            if (_context.DaySchedules == null)
            {
                return Problem("Entity set 'DatabaseContext.DaySchedules'");
            }
            if (year == 0)
            {
                year = DateTime.Now.Year;
            }
            if (weeknum <= 0 && 54 < weeknum)
            {
                return BadRequest("WeekNumber has to be between 1-53");
            }
            List<DaySchedule> daySchedules = _context.DaySchedules.Where(x => x.StartTime.Year == year).ToList();
            DateTime MondayOfWeek = new(year, 1, 1);

            if (DateTime.IsLeapYear(year))
            {
                MondayOfWeek = MondayOfWeek.AddDays(7 * (weeknum));
            }
            else
            {
                MondayOfWeek = MondayOfWeek.AddDays(7 * (weeknum - 1));
            }
            while (MondayOfWeek.DayOfWeek != DayOfWeek.Monday)
            {
                MondayOfWeek = MondayOfWeek.AddDays(-1);
            }
            DateTime sundayOFWeek = MondayOfWeek.AddDays(6);
            daySchedules = _context.DaySchedules.Where(daySchedule => (daySchedule.StartTime <= sundayOFWeek && daySchedule.EndTime >= MondayOfWeek)).ToList();
            List<DayScheduleDTOID> dayscheduleDTOs = daySchedules.Adapt<List<DayScheduleDTOID>>();
            foreach (DayScheduleDTOID scheduleDTO in dayscheduleDTOs)
            {
                scheduleDTO.TeacherName = _context.Teachers.Find(scheduleDTO.TeacherID).Name;
                scheduleDTO.SubjectName = _context.Subjects.Find(scheduleDTO.SubjectID).Name;
            }

            return dayscheduleDTOs;
        }

        [HttpGet("GetAllDaySchedules")]
        public async Task<ActionResult<List<DayScheduleDTOID>>> GetAllDaySchedules(string Education)
        {
            List<DayScheduleDTOID> DayScheduleDTOID = _context.DaySchedules.Where(x => x.Education == Education).ToList().Adapt<List<DayScheduleDTOID>>();
            return DayScheduleDTOID;
        }

        [HttpPut("ChangeAsignedTeacher")]
        public async Task<ActionResult<string>> EditedTPTime(Guid DayScheduleID, Guid TeacherID)
        {
            DaySchedule daySchedule = await _context.DaySchedules.FindAsync(DayScheduleID);
            daySchedule.TeacherID = TeacherID;

            await _context.SaveChangesAsync();

            return Ok("Teacher updated");
        }

        [HttpPut("EditedMeetingTime")]
        public async Task<ActionResult<string>> EditedMeetingTime(Guid DayScheduleID, DateTime StartTime,DateTime EndTime)
        {
            DaySchedule daySchedule = await _context.DaySchedules.FindAsync(DayScheduleID);
            daySchedule.StartTime = StartTime;
            daySchedule.EndTime = EndTime;
            await _context.SaveChangesAsync();
            return Ok("Meeting Time updated");
        }

        [HttpPut("EditedTPTime")]
        public async Task<ActionResult<string>> EditedTPTime(Guid DayScheduleID, int TPhours)
        {
            DaySchedule daySchedule = await _context.DaySchedules.FindAsync(DayScheduleID);
            daySchedule.TPHours = TPhours;
            await _context.SaveChangesAsync();            
            return Ok("Time updated");
        }


        [HttpDelete("DeleteSchedules")]
        public async Task<ActionResult<string>> DeleteScheduleDay(Guid ScheduleDayID)
        {

            DaySchedule dayschedule = await _context.DaySchedules.FindAsync(ScheduleDayID);

            int tptime = dayschedule.TPHours;
            Guid subjectID = dayschedule.SubjectID;
            _context.Remove(dayschedule);
            await _context.SaveChangesAsync();
            if(tptime > 0)
            {
                Subject subject = await _context.Subjects.FindAsync(subjectID);
                subject.UsedTPHours -= tptime;
                await _context.SaveChangesAsync();
            }
            
            return Ok("DaySchedule was Deleted");
        }

    }
}
