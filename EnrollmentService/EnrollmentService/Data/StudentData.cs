using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public class StudentData : IStudent
    {
        private StudentDbContext _db;
        public StudentData(StudentDbContext db)
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
                    _db.Students.Remove(result);
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

        public async Task<IEnumerable<Student>> GetAll()
        {
            var result = await _db.Students.OrderBy(s => s.FirstName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Student> GetById(string id)
        {
            var result = await _db.Students.Where(s => s.StudentId == Convert.ToInt32(id)).FirstOrDefaultAsync();
            var enrollment = await _db.Enrollments.Include(e => e.Course).Where(e => e.StudentId == Convert.ToInt32(id)).AsNoTracking().ToListAsync();
            result.Enrollments = enrollment;
            return result;
        }

        public async Task<Student> GetByUsername(string username)
        {
            var result = await _db.Students.Where(s => s.UserName == username).FirstOrDefaultAsync();
            //var enrollment = await _db.Enrollments.Include(e => e.Course).Where(e => e.Username == Convert.ToInt32(id)).AsNoTracking().ToListAsync();
            //result.Enrollments = enrollment;
            return result;
        }

        public async Task Insert(Student obj)
        {
            try
            {
                _db.Students.Add(obj);
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

        public async Task Update(string id, Student obj)
        {
            try
            {
                var result = await GetById(id);
                if (result != null)
                {
                    //_db.Update(obj);
                    result.FirstName = obj.FirstName;
                    result.MiddleName = result.MiddleName;
                    result.LastName = obj.LastName;
                    result.PhoneNumber = obj.PhoneNumber;
                    result.Dob = result.Dob;
                    result.EnrollmentDate = obj.EnrollmentDate;// utk update satu field
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
