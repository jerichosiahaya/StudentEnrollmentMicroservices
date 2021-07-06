using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public class CourseData : ICourse
    {

        private StudentDbContext _db;
        public CourseData(StudentDbContext db)
        {
            _db = db;
        }

        public async Task Delete(string id)
        {
            var result = await GetById(id);
            if (result != null)
            {
                try
                {
                    _db.Courses.Remove(result);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateException DbEx)
                {

                    throw new Exception(DbEx.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var result = await _db.Courses.Include(e => e.Enrollments).
                         OrderBy(s => s.Title).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Course> GetById(string id)
        {
            var result = await _db.Courses.Where(s => s.CourseId == Convert.ToInt32(id)).FirstOrDefaultAsync();
            return result;
        }

        public async Task Insert(Course obj)
        {
            try
            {
                _db.Courses.Add(obj);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(string id, Course obj)
        {
            try
            {
                var result = await GetById(id);
                if (result != null)
                {
                    result.Title = obj.Title;
                    result.Credits = obj.Credits;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Data {id} tidak ditemukan");
                }
            }
            catch (DbUpdateException DbEx)
            {

                throw new Exception(DbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
