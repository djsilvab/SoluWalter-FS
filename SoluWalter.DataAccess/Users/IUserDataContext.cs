using SoluWalter.Entities.Models;
using SoluWalter.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoluWalter.DataAccess.Users
{
    public interface IUserDataContext
    {
        Task<User> GetById(string id);
        Task<User> CreateUser(User user);
        Task<User> Authenticate(string username, string password);
    }
}
