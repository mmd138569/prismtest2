using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Microsoft.EntityFrameworkCore;
using prismtest2.Models.Context;

namespace prismtest2.Models.Services
{
    public class CreateUser : ICreateUser
    {
        private readonly DataBaseContext context;
        public CreateUser()
        {
            context = new DataBaseContext();
        }

        public List<UserListDTO> userListDtos()
        {
            var users = context.username.Select(p => new UserListDTO
            {
                id=p.Id,
                User_name = p.Username,
                Password = p.Password
            }).ToList();
            return users;
        } 
    }
}
