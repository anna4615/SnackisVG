using SnackisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Gateways
{
    public interface ISubjectGateway
    {
        Task<List<Subject>> GetSubjects();
        Task<Subject> GetSubject(int id);
        Task<Subject> PostSubject(Subject subject);
        Task PutSubject(int editId, Subject subject);
        Task DeleteSubject(int deleteId);

      
    }
}
