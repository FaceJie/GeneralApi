using DataPojo.Entity;
using DataPojo.EntityExtention.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PojoService.Service
{
    public class UserImp : BaseRepository<User>, IService.IUser
    {
        public string GetUserDptName(int id)
        {
            throw new NotImplementedException();
        }

        public string GetUserName(int userId)
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int userId)
        {
            throw new NotImplementedException();
        }

        public User UserLogin(string useraccount, string password)
        {
            throw new NotImplementedException();
        }
    }
}
