using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<Student>> GetStudentName() 
        {
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email

            });

            return Ok(CollegeRepository.Students);
        }







        [HttpGet]
        [Route("{id:int}" , Name = "GetStudentsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]



        public ActionResult<Student> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var stu = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (stu == null)
                return NotFound($"The student with id {id} not found");

            var studentDTO = new StudentDTO()
            {
                Id = stu.Id,
                StudentName = stu.StudentName,
                Address = stu.Address,
                Email = stu.Email

            };

            return Ok(stu);
        }









        [HttpGet("{name:alpha}" , Name = "GetStudentsByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Student> GetStudentByName(string name)
        {
            if(string.IsNullOrEmpty(name))
                return BadRequest();

            var stu = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();

            if (stu == null)
                return NotFound($"The student with name {name} not found");

            return Ok(stu);
        }





        [HttpPost]
        [HttpGet("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //api/student/create
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model) 
        { 
            if(model == null)
                return BadRequest();

            if (model.AdmissionDate < DateTime.Now)
            {
                //1. Directly adding error message to modelstate
                //2. Using custom attribute

                ModelState.AddModelError("AdmissionDate Error", "Admission date must be greater than or equal to todays date");
                return BadRequest(ModelState);    
            }

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;

            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email
            };

            CollegeRepository.Students.Add(student);

            model.Id = student.Id;
            // Status - 201
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            
        }









        [HttpDelete("{id}", Name = "DeleteStudentsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();

            var stu =  CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            return NotFound($"The student with id {id} not found");
            
            CollegeRepository.Students.Remove(stu);

            return Ok(stu);
        }


        [HttpPut]
        [Route("Update")]
        //api/student/create
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        { 
            if(model == null || model.Id <=0)
                BadRequest();

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;

            return NoContent();
        }
    }
}
