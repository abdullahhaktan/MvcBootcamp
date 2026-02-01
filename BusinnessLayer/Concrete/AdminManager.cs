using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinnessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminLoginDal _adminLoginDal;
        public AdminManager(IAdminLoginDal adminLoginDal)
        {
            _adminLoginDal = adminLoginDal;
        }

        public void AdminAdd(Admin admin)
        {
            _adminLoginDal.Insert(admin);
        }

        public void AdminDelete(Admin admin)
        {
            _adminLoginDal.Delete(admin);
        }

        public void AdminUpdate(Admin admin)
        {
            _adminLoginDal.Update(admin);
        }

        public Admin GetByID(int id)
        {
            return _adminLoginDal.Get(x => x.AdminID == id);
        }

        public List<Admin> GetList()
        {
            return _adminLoginDal.List();
        }
    }
}
