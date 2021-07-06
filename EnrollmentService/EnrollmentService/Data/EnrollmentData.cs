using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public class EnrollmentData : IEnrollment
    {

        private StudentDbContext _db;
        public EnrollmentData(StudentDbContext db)
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
                    _db.Enrollments.Remove(result);
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

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var result = await _db.Enrollments.Include(e => e.Student).Include(e => e.Course).
                         OrderBy(s => s.StudentId).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Enrollment> GetById(string id)
        {
            var result = await _db.Enrollments.Where(s => s.EnrollmentId == Convert.ToInt32(id)).Include(e => e.Student).Include(e => e.Course).FirstOrDefaultAsync();
            return result;
        }

        public async Task Insert(Enrollment obj)
        {
            try
            {
                _db.Enrollments.Add(obj);
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

        public async Task Update(string id, Enrollment obj)
        {
            try
            {
                var result = await GetById(id);
                if (result != null)
                {
                    result.CourseId = obj.CourseId;
                    result.StudentId = obj.StudentId;
                    result.Grade = obj.Grade;  // utk update satu field
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
