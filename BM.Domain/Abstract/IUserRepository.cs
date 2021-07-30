using System;
using System.Linq;
using BM.DataAccess.Entities;

namespace BM.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();

        IQueryable<User> GetUsers(Guid roleId);

        User GetUser(Guid userId);
    }
}
