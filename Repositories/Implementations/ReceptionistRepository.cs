using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Interfaces;
using SQLitePCL;

namespace MarefiyaApi.Repositories.Implementations
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly MarefiyaDbContext _context;

        public ReceptionistRepository(MarefiyaDbContext context)
        {
            _context = context;
        }
        public bool CreateReceptionist(Receptionist receptionist)
        {
            _context.Add(receptionist);
            return Save();
        }

        public bool DeleteReceptionist(Receptionist receptionist)
        {
            _context.Remove(receptionist);
            return Save();
        }
        public ICollection<Receptionist> GetReceptionists()
        {
            return _context.Receptionists.ToList();
        }

        public Receptionist GetReceptionist(int id)
        {
            return _context.Receptionists.Where(manager => manager.Id == id).FirstOrDefault();  
        }

        public bool UpdateReceptionist(Receptionist receptionist)
        {
            _context.Update(receptionist);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ReceptionistExists(int id)
        {
            return _context.Receptionists.Any(receptionist => receptionist.Id == id);
        }

        public Hotel GetReceptionistHotel(int id)
        {
            return _context.Receptionists.Where(receptionist => receptionist.Id == id).Select(receptionist => receptionist.Hotel).FirstOrDefault();
        }
    }
}
