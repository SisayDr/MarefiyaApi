using MarefiyaApi.Models;

namespace MarefiyaApi.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        ICollection<Admin> GetAdmins();
        Admin GetAdmin(int id);
        bool CreateAdmin(Admin admin);
        bool UpdateAdmin(Admin admin);
        bool DeleteAdmin(Admin admin);
        bool AdminExists(int id);
        bool Save();
    }
}
