using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinnessLayer.Concrete
{
    public class AdminLoginManager : IAdminLoginService
    {
        private readonly IAdminLoginDal _adminLoginDal;
        public AdminLoginManager(IAdminLoginDal adminDal)
        {
            _adminLoginDal = adminDal;
        }

        public Admin GetAdmin(string username, string password)
        {
            return _adminLoginDal.Get(x => x.AdminUserName == username && x.AdminPassword == password);
        }
    }
}
