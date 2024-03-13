using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;
using prismtest2.Models.Context;
using prismtest2.Models.Entity;

namespace prismtest2.Models.Services
{
    public class AddUser:IAddUser
    {
        private readonly DataBaseContext context;
        public AddUser()
        {
            context=new DataBaseContext();
        }
        public void adduser(Users users)
        {
            context.username.Add(users);
        }
    }
}
