using EntityLayer.Concrete;

namespace BusinnessLayer.Abstract
{
    public interface IWriterLoginService
    {
        Writer GetWriter(string username, string password);
    }
}
