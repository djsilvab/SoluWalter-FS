using SoluWalter.Entities.Dtos;
using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoluWalter.BusinessLogic.Users
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
        Task<User> CreateUser(User user);
        Task<User> Authenticate(string username, string password);

    }
}
