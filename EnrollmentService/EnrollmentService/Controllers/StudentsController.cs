using EnrollmentService.Data;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private IStudent _student;
        public StudentsController(IStudent student)
        {
            _student = student;
        }

        // GET: api/<StudentsController>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            var results = await _student.GetAll();
            return results;
        }

        // GET api/<StudentsController>/5
        [Authorize]
        [HttpGet("id/{id}")]
        public async Task<Student> Get(int id)
        {
            var results = await _student.GetById(id.ToString());
            return results;
        }

        // GET api/<StudentsController>/{username}
        [Authorize]
        [HttpGet("username/{username}")]
        public async Task<Student> Get(string username)
        {
            var results = await _student.GetByUsername(username);
            return results;
        }

        // POST api/<StudentsController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student) //IActionResult untuk kembalikan status code
        {
            try
            {
                await _student.Insert(student);
                return Ok($"Data {student.FirstName} {student.LastName} berhasil ditambahkan!"); // di EntityFramework Id-nya sudah langsung diketahui
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<StudentsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            try
            {
                await _student.Update(id.ToString(), student);
                return Ok($"Data {id} berhasil diupdate!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<StudentsController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _student.Delete(id.ToString());
                return Ok($"Data {id} berhasil didelete!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
