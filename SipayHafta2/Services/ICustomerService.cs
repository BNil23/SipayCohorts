using SipayHafta2.Models;

namespace SipayHafta2.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(int id, Customer customer);
        void DeleteCustomer(int id);
    }
}
