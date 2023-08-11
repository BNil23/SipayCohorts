using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SipayHafta2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private static List<Customer> _customers = new List<Customer>();

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound(new ErrorResponse { Message = "Customer not found", StatusCode = 404 });
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest(new ErrorResponse { Message = "Invalid input", StatusCode = 400 });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse { Message = "Validation failed", StatusCode = 400, Errors = GetModelErrors() });
            }

            customer.Id = Guid.NewGuid().GetHashCode(); // Sadece örnek bir ID oluşturma yöntemi
            _customers.Add(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            var existingCustomer = _customers.Find(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound(new ErrorResponse { Message = "Customer not found", StatusCode = 404 });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse { Message = "Validation failed", StatusCode = 400, Errors = GetModelErrors() });
            }

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;
            // Diğer özellikleri güncelle

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound(new ErrorResponse { Message = "Customer not found", StatusCode = 404 });
            }

            _customers.Remove(customer);
            return NoContent();
        }

        private List<string> GetModelErrors()
        {
            var errors = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }

    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Diğer zorunlu alanlar ve özellikler eklenebilir
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
