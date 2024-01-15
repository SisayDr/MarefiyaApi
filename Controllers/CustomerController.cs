using AutoMapper;
using MarefiyaApi.Dto;
using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarefiyaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerDto>))]
        public IActionResult GetCustomers()
        {
            return Ok(_mapper.Map<List<CustomerDto>>(_customerRepository.GetCustomers()));
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        public IActionResult GetCustomer(int Id)
        {
            return Ok(_mapper.Map<CustomerDto>(_customerRepository.GetCustomer(Id)));
        }

        [HttpGet("Login/{Email}/{Password}")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        public IActionResult CustomerLogin(string Email, string Password)
        {
            var customer = _customerRepository.CustomerLogin(Email, Password);
            if(customer == null) {return Unauthorized();}
         
            return Ok(_mapper.Map<CustomerDto>(customer));
        }
        [HttpGet("{CustomerID}/Bookings")]
        [ProducesResponseType(200, Type = typeof(List<BookingDto>))]
        public IActionResult GetHotelBookings(int CustomerID)
        {
            if (!_customerRepository.CustomerExists(CustomerID))
            {
                ModelState.AddModelError("", "Customer not found");
                return StatusCode(404, ModelState);
            }
            var bookings = _customerRepository.GetBookings(CustomerID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCustomer(UserDto newCustomer)
        {
            if (newCustomer == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            Customer mappedNewHotelManger = _mapper.Map<Customer> (newCustomer);
            var CustomerCreated = _customerRepository.CreateCustomer(mappedNewHotelManger);
            if (CustomerCreated == null)
            {
                ModelState.AddModelError("", "Oops, Something went wrong while saving customer");
                return StatusCode(500, ModelState);
            }

            return Ok(_mapper.Map<CustomerDto>(CustomerCreated));
        }

        [HttpPut("{CustomerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateHotel(int CustomerId, UserDto updatedCustomer)
        {

            if (updatedCustomer == null)
                return BadRequest(ModelState);

            if (!_customerRepository.CustomerExists(CustomerId))
            {
                ModelState.AddModelError("", "Customer not found");
                return StatusCode(404, ModelState);
            }

            updatedCustomer.Id = CustomerId;
            var mappedCustomer = _mapper.Map<Customer>(updatedCustomer);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerRepository.UpdateCustomer(mappedCustomer))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while updating customer");
                return StatusCode(500, ModelState);
            }

            return Ok("Customer Updated Successfully!");
        }

        [HttpDelete("{CustomerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteHotel(int CustomerId)
        {
            if (!_customerRepository.CustomerExists(CustomerId))
            {
                ModelState.AddModelError("", "Customer not found");
                return StatusCode(404, ModelState);
            }

            var customerToDelete = _customerRepository.GetCustomer(CustomerId);
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            if (!_customerRepository.DeleteCustomer(customerToDelete))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while Deleting customer");
                return StatusCode(500, ModelState);
            }

            return Ok("Hotel Deleted Successfully!");
        }
    }
}
