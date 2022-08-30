using SoluWalter.DataAccess.Users;
using SoluWalter.Entities.Dtos;
using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoluWalter.BusinessLogic.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDataContext _context;
        public UserRepository(IUserDataContext _context)
        {
            this._context = _context;
        }

        public async Task<User> GetById(string id)
        {
            return await _context.GetById(id);
        }

        public async Task<User> CreateUser(User user)
        {
            return await _context.CreateUser(user);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            return await _context.Authenticate(username, password);
        }

    }
}
