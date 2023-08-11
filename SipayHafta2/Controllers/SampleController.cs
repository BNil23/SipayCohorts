using Microsoft.AspNetCore.Mvc;
using SipayHafta2.Attributes;
using SipayHafta2.Services;

namespace SipayHafta2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [FakeUser(true)] // Fake kullanıcı girişi sağlar
    public class SampleController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public SampleController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [FakeUser(false)] // Kimlik doğrulaması gerektirir
        public IActionResult Get()
        {
            var data = _customerService.GetCustomers();
            return Ok(data);
        }
    }

}
