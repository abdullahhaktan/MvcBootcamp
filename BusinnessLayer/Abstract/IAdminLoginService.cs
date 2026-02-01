using EntityLayer.Concrete;

namespace BusinnessLayer.Abstract
{
    public interface IAdminLoginService
    {
        Admin GetAdmin(string username, string password);
    }
}
