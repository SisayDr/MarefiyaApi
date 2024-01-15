using AutoMapper;
using MarefiyaApi.Dto;
using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Implementations;
using MarefiyaApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarefiyaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, 
            IHotelRepository hotelRepository, IRoomRepository roomRepository, 
            ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingDto>))]
        public IActionResult GetBookings()
        {
            return Ok(_bookingRepository.GetBookings());
        }

        [HttpPost("{CustomerId}/{HotelId}/{RoomId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBooking(int CustomerId, int HotelId, int RoomId, BookingDto newBooking)
        {
            if (newBooking == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerRepository.CustomerExists(CustomerId))
            {
                ModelState.AddModelError("", "Invalid Customer Id");
                return StatusCode(404, ModelState);
            }
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Invalid Hotel Id");
                return StatusCode(404, ModelState);
            }
            if (!_roomRepository.RoomExists(RoomId))
            {
                ModelState.AddModelError("", "Invalid Room Id");
                return StatusCode(404, ModelState);
            }

            if (!_roomRepository.GetRoom(RoomId).RoomNos.Contains(newBooking.RoomNo))
            {
                ModelState.AddModelError("", $"Room {newBooking.RoomNo} does not exist.");
                return StatusCode(404, ModelState);
            }

            Booking mappedBooking = _mapper.Map<Booking> (newBooking);
            mappedBooking.Customer = _customerRepository.GetCustomer(CustomerId);
            mappedBooking.Hotel = _hotelRepository.GetHotel(HotelId);
            mappedBooking.Room = _roomRepository.GetRoom(RoomId);
            if (!_bookingRepository.CreateBooking(mappedBooking))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while saving booking");
                return StatusCode(500, ModelState);
            }

            return Ok("Hotel Room Booked Successfully!");
        }

        [HttpPut("{CustomerId}/{HotelId}/{RoomId}/{BookingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateHotel(int CustomerId, int HotelId, int RoomId, int BookingId, BookingDto updatedBooking)
        {

            if (updatedBooking == null)
                return BadRequest(ModelState);

            if (!_bookingRepository.BookingExists(BookingId))
            {
                ModelState.AddModelError("", "Booking not found");
                return StatusCode(404, ModelState);
            }

            if (!_customerRepository.CustomerExists(CustomerId))
            {
                ModelState.AddModelError("", "Invalid Customer Id");
                return StatusCode(404, ModelState);
            }
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Invalid Hotel Id");
                return StatusCode(404, ModelState);
            }
            if (!_roomRepository.RoomExists(RoomId))
            {
                ModelState.AddModelError("", "Invalid Room Id");
                return StatusCode(404, ModelState);
            }

            if (!_roomRepository.GetRoom(RoomId).RoomNos.Contains(updatedBooking.RoomNo))
            {
                ModelState.AddModelError("", $"Room {updatedBooking.RoomNo} does not exist.");
                return StatusCode(404, ModelState);
            }
            

            updatedBooking.Id = BookingId;
            var mappedBooking = _mapper.Map<Booking>(updatedBooking);
            mappedBooking.Customer = _customerRepository.GetCustomer(CustomerId);
            mappedBooking.Hotel = _hotelRepository.GetHotel(HotelId);
            mappedBooking.Room = _roomRepository.GetRoom(RoomId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookingRepository.UpdateBooking(mappedBooking))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while updating booking details");
                return StatusCode(500, ModelState);
            }

            return Ok("Booking Updated Successfully!");
        }

        [HttpDelete("{BookingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteHotel(int BookingId)
        {
            if (!_bookingRepository.BookingExists(BookingId))
            {
                ModelState.AddModelError("", "Booking not found");
                return StatusCode(404, ModelState);
            }

            var bookingToDelete = _bookingRepository.GetBooking(BookingId);
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            if (!_bookingRepository.DeleteBooking(bookingToDelete))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while deleting booking");
                return StatusCode(500, ModelState);
            }

            return Ok("Booking Deleted Successfully!");
        }
    }
}
