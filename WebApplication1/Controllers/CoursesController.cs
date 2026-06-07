using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApplication1.Data;
using WebApplication1.Dto.Course;
using WebApplication1.Dto.Student;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[ProducesResponseType(typeof(CourseListDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(StudentListDto), StatusCodes.Status410Gone)]
        public async Task<ActionResult<List<CourseListDto>>> GetAll(bool activeOnly = false)
        {
            var query = _context.Courses.AsQueryable();


            if (activeOnly)
                query = query.Where(x => x.IsActive);


            var courses = await query
                .Select( c => new CourseListDto(
                         c.Id,
                         c.Title,
                         c.Price,
                         c.Enrollments.Count(),
                         c.IsActive
                         )).ToListAsync();


            return Ok(courses);

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CourseListDto>> GetById(Guid id)
        {
            var query = _context.Courses.AsQueryable();


            

            var courses = await query
                .Where(c=>c.Id==id)
                .Select(c => new CourseListDto(
                         c.Id,
                         c.Title,
                         c.Price,
                         c.Enrollments.Count(),
                         c.IsActive
                         )).FirstOrDefaultAsync();


            return Ok(courses);

        }

        [HttpGet("expensive")]
        public async Task<ActionResult<CourseListDto>> GetExpensive (decimal minPrice=10)
        {
            var query = _context.Courses.AsQueryable();




            var courses = await query
                .Where(x=>x.Price >=minPrice)
                .OrderByDescending(c=>c.Price)
                .Select(c => new CourseListDto(
                         c.Id,
                         c.Title,
                         c.Price,
                         c.Enrollments.Count(),
                         c.IsActive
                         )).ToListAsync();


            return Ok(courses);
        }

        [HttpPost]
         public  async Task<ActionResult<CourseListDto>> Create(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                IsActive = true
            };


            _context.Courses.Add(course);
            await _context.SaveChangesAsync();


            var result = new CourseListDto(course.Id, course.Title, course.Price, 0, true);
            return Ok(result);


        }

    }
}
