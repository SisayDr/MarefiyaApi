using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Interfaces;
using SQLitePCL;

namespace MarefiyaApi.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MarefiyaDbContext _context;

        public CustomerRepository(MarefiyaDbContext context)
        {
            _context = context;
        }
        public Customer CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            return Save() ? customer : null;
        }

        public bool DeleteCustomer(Customer customer)
        {
            _context.Remove(customer);
            return Save();
        }
        public ICollection<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Where(manager => manager.Id == id).FirstOrDefault();  
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(customer => customer.Id == id);
        }

        public ICollection<Booking> GetBookings(int id)
        {
            return _context.Customers.Where(customer =>  customer.Id == id).Select(customer => customer.Bookings).FirstOrDefault().ToList();
        }

        public Customer CustomerLogin(string Email, string Password)
        {
            return _context.Customers.Where(customer => customer.Email == Email && customer.Password == Password).FirstOrDefault();
        }
    }
}
