using System.Linq;
using Aparts.Data;
using Aparts.Models;
using Aparts.Models.DLModels;

namespace Aparts.Services
{
    public class ManageUserService
    {
        private readonly ApplicationDbContext _dbContext;        
        public ManageUserService(ApplicationDbContext dbContext, ApartsDataContext partContext)
        {
            _dbContext = dbContext;            
        }

        public IQueryable<ApplicationUser> GetUsersForManage()
        {
            var result = _dbContext.Users; 
            return result;
        }

        public string[] GetAllRoles()
        {
            return _dbContext.Roles.Select(role => role.Name).ToArray();
        }
       
    }
}
