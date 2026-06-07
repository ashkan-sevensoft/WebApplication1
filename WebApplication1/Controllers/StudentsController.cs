using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto.Student;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        ///Repo
        ///Get ... Crud

        ///Service
        ///Irepo




        public StudentsController(AppDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<StudentListDtoRec>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10

            )
        {
            var query = _context.students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.FullName.Contains(search));
            }



            var result = await query
                .OrderBy(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentListDtoRec(
                    s.Id,
                    s.FullName,
                    s.Email,
                    s.Age
                    )
                ).ToListAsync();

            return Ok(result);

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<StudentListDtoRec>> GetById(Guid id)
        {

            var result = await
                _context.students.AsQueryable().Where(x => x.Id == id)
                .Select(s => new StudentListDtoRec(
                    s.Id,
                    s.FullName,
                    s.Email,
                    s.Age
                    )
                ).FirstOrDefaultAsync();

            if (result is null)
            {
                return NotFound($"Student  with Id : {id.ToString()} wasnot Found");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StudentListDtoRec>> Create(CreateStudentDto dto)
        {
            var existEmail = await _context.students.AnyAsync(x => x.Email == dto.Email);


            if (existEmail)
                return BadRequest("Email Is Already Exist");


            var student = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Age = dto.Age,

            };

            _context.students.Add(student);
            await _context.SaveChangesAsync();

            var result = new StudentListDtoRec(student.Id, student.FullName, student.Email, student.Age);


            return Ok(result);


        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult<StudentListDtoRec>> Update(Guid id, UpdateStudentDto dto)
        {
            var existStudent = await _context.students.AnyAsync(x => x.Id == id);
            if (!existStudent)
                return BadRequest($"Student With Id : {id} was not Existt");


            var existEmail = await _context.students.AnyAsync(x => x.Email == dto.Email);
            if (existEmail)
                return BadRequest("Email Is Already Exist");


            var student = await _context.students.FirstOrDefaultAsync(x => x.Id == id);

            student.FullName = dto.FullName;
            student.Email = dto.Email;
            student.Age = dto.Age;


            await _context.SaveChangesAsync();

            return Ok(student);

        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existStudent = await _context.students.AnyAsync(x => x.Id == id);
            if (!existStudent)
                return BadRequest($"Student With Id : {id} was not Existt");


          
            var student = await _context.students.FirstOrDefaultAsync(x => x.Id == id);

            student.IsDelted = true;
            student.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(true);

        }


         
    }
}
