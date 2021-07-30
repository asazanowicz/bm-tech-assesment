using System;
using System.Linq;
using BM.DataAccess.DbContexts;
using BM.DataAccess.Entities;
using BM.Domain.Abstract;
using Microsoft.Extensions.Logging;

namespace BM.Domain.Concrete
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(BMContext context, ILogger<UserRepository> logger) : base(context, logger)
        {

        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }

        public IQueryable<User> GetUsers(Guid roleId)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            return _context.Users.Where(c => c.RoleId == roleId );
        }

        public User GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _context.Users.FirstOrDefault(c => c.UserId == userId);
        }
    }
}
