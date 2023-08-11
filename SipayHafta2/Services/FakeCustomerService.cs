using SipayHafta2.Models;

namespace SipayHafta2.Services
{
    public class FakeCustomerService : ICustomerService
    {
        private List<Customer> _customers;

        public FakeCustomerService()
        {
            _customers = new List<Customer>();
        }

        // ICustomerService metodları implemente edilir
    }
}
