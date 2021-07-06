using EnrollmentService.Data;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// Author:  Jericho Cristofel Siahaya
// Date:    Tuesday, July 6, 2021

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private ICourse _course;
        public CoursesController(ICourse course)
        {
            _course = course;
        }

        // GET: api/<CoursesController>
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
            return await _course.GetAll();
        }

        // GET api/<CoursesController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<Course> Get(int id)
        {
            var results = await _course.GetById(id.ToString());
            return results;
        }

        // POST api/<CoursesController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            try
            {
                await _course.Insert(course);
                return Ok($"Data {course.Title} berhasil ditambahkan!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CoursesController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Course course)
        {
            try
            {
                await _course.Update(id.ToString(), course);
                return Ok($"Data {id} berhasil diupdate!");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<CoursesController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _course.Delete(id.ToString());
                return Ok($"Data {id} berhasil didelete!");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
