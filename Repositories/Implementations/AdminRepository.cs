using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Interfaces;
using SQLitePCL;

namespace MarefiyaApi.Repositories.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly MarefiyaDbContext _context;

        public AdminRepository(MarefiyaDbContext context)
        {
            _context = context;
        }
        public bool CreateAdmin(Admin admin)
        {
            _context.Add(admin);
            return Save();
        }

        public bool DeleteAdmin(Admin admin)
        {
            _context.Remove(admin);
            return Save();
        }
        public ICollection<Admin> GetAdmins()
        {
            return _context.Admins.ToList();
        }

        public Admin GetAdmin(int id)
        {
            return _context.Admins.Where(manager => manager.Id == id).FirstOrDefault();  
        }

        public bool UpdateAdmin(Admin admin)
        {
            _context.Update(admin);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool AdminExists(int id)
        {
            return _context.Admins.Any(admin => admin.Id == id);
        }
    }
}
