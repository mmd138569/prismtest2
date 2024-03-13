using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;
using prismtest2.Models.Entity;

namespace prismtest2.Models.Services
{
    public interface IAddUser
    {
        public void adduser(Users users);
    }
}
