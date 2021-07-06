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
    public class EnrollmentsController : ControllerBase
    {

        private IEnrollment _enrollment;
        public EnrollmentsController(IEnrollment enrollment)
        {
            _enrollment = enrollment;
        }

        // GET: api/<EnrollmentsController>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<Enrollment>> Get()
        {
            //return new string[] { "value1", "value2" };
            return await _enrollment.GetAll();
        }

        // GET api/<EnrollmentsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<Enrollment> Get(int id)
        {
            var results = await _enrollment.GetById(id.ToString());
            return results;
        }


        // POST api/<EnrollmentsController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Enrollment enrollment)
        {
            try
            {
                await _enrollment.Insert(enrollment);
                return Ok($"Data {enrollment.EnrollmentId} berhasil ditambahkan!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<EnrollmentsController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Enrollment enrollment)
        {
            try
            {
                await _enrollment.Update(id.ToString(), enrollment);
                return Ok($"Data {id} berhasil diupdate!");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<EnrollmentsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _enrollment.Delete(id.ToString());
                return Ok($"Data {id} berhasil didelete!");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
