using MarefiyaApi.Models;

namespace MarefiyaApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        ICollection<Customer> GetCustomers();
        Customer GetCustomer(int id);
        Customer CustomerLogin(string Email, string Password);
        ICollection<Booking> GetBookings(int id);
        Customer CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        bool CustomerExists(int id);
        bool Save();
    }
}
