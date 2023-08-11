using SipayHafta2.Models;

namespace SipayHafta2.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers;

        public CustomerService()
        {
            _customers = new List<Customer>();
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public Customer GetCustomerById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            customer.Id = _customers.Count + 1;
            _customers.Add(customer);
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            var existingCustomer = _customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer == null)
            {
                throw new InvalidOperationException("Customer not found.");
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            // Diğer özellikleri güncelle
        }

        public void DeleteCustomer(int id)
        {
            var customerToRemove = _customers.FirstOrDefault(c => c.Id == id);
            if (customerToRemove != null)
            {
                _customers.Remove(customerToRemove);
            }
        }
    }
}
