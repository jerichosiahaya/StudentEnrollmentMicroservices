using EnrollmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public interface IStudent : ICrud<Student>
    {
        Task<Student> GetByUsername(string username);
    }
}
