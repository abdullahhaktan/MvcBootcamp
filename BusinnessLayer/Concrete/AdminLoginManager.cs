using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLayer.Concrete
{
    public class AdminLoginManager : IAdminLoginService
    {
        private readonly IAdminDal _adminDal;
        public AdminLoginManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public Admin GetAdmin(string username, string password)
        {
          return _adminDal.Get(x => x.AdminUserName == username && x.AdminPassword == password);
        }
    }
}
