using SnackisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Gateways
{
    public interface IForumGateway
    {
        Task<List<Forum>> GetForums();
        Task<Forum> PostForum(Forum forum);
        Task<Forum> PutForum(int editId, Forum forum);
        Task<Forum> DeleteForum(int deleteId);
    }
}
