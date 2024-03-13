using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prismtest2.Models.Services
{
    public interface ICreateUser
    {
        List<UserListDTO> userListDtos();
    }
    public class UserListDTO
    {
        public int id;
        public string User_name;
        public string Password;
        public string Email;
    }
}
