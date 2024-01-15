using AutoMapper;
using MarefiyaApi.Dto;
using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Implementations;
using MarefiyaApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarefiyaApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelController(IHotelRepository hotelRepository, IHotelManagerRepository hotelManagerRepository, IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _hotelManagerRepository = hotelManagerRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HotelDto>))]
        public IActionResult GetHotels([FromQuery] string? State, DateTime? CheckIn, DateTime? CheckOut, int? MinPrice, int? MaxPrice)
        {
            //return Ok(new {State, CheckIn, CheckOut, MinPrice, MaxPrice});
            var hotels = _hotelRepository.GetHotels();
            if(State != null)
            {
                hotels = hotels.Where(hotel => hotel.State.Contains(State)).ToList();
            }
            if (MinPrice != null && MinPrice > 0)
            {
                hotels = hotels.Where(hotel => hotel.CheapestPrice > MinPrice).ToList();
            }
            if (MaxPrice != null && MaxPrice != 999999)
            {
                hotels = hotels.Where(hotel => hotel.CheapestPrice < MaxPrice).ToList();
            }
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_mapper.Map<List<HotelDto>>(hotels));
        }

        [HttpGet("Count")]
        [ProducesResponseType(200, Type = typeof(int))]
        public int GetHotelsCount()
        {
            return _hotelRepository.GetHotelsCount();
        }

        [HttpGet("State{state}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HotelDto>))]
        public IActionResult GetHotelsByState(string state)
        {
            var hotels = _mapper.Map<List<HotelDto>>(_hotelRepository.GetHotelsByState(state));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(hotels);
        }

        [HttpGet("TopRated")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HotelDto>))]
        public IActionResult GetTopRatedHotels()
        {
            var hotels = _mapper.Map<List<HotelDto>>(_hotelRepository.GetTopRatedHotels());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(hotels);
        }

        [HttpGet("Cheapest")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HotelDto>))]
        public IActionResult GetCheapestHotels()
        {
            var hotels = _mapper.Map<List<HotelDto>>(_hotelRepository.GetCheapestHotels());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(hotels);
        }

        [HttpGet("State/{state}/Count")]
        [ProducesResponseType(200, Type = typeof(int))]
        public int GetHotelsCountByState(string state)
        {
            return _hotelRepository.GetHotelsCountByState(state);
        }

        [HttpGet("City/{city}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HotelDto>))]
        public IActionResult GetHotelsByCity(string city)
        {
            var hotels = _mapper.Map<List<HotelDto>>(_hotelRepository.GetHotelsByCity(city));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(hotels);
        }

        [HttpGet("City/{city}/Count")]
        [ProducesResponseType(200, Type = typeof(int))]
        public int GetHotelsCountByCity(string city)
        {
            return _hotelRepository.GetHotelsCountByCity(city);
        }

        [HttpGet("{HotelId}")]
        [ProducesResponseType(200, Type = typeof(HotelDto))]
        public IActionResult GetHotel(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }
            var hotel = _mapper.Map<HotelDto>(_hotelRepository.GetHotel(HotelId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(hotel);
        }

        [HttpGet("{HotelId}/Manager")]
        [ProducesResponseType(200, Type = typeof(HotelManager))]
        public IActionResult GetHotelManager(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }
            var hotelManager = _hotelRepository.GetHotelManager(HotelId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(hotelManager);
        }

        [HttpGet("{HotelId}/Rooms")]
        [ProducesResponseType(200, Type = typeof(List<RoomDto>))]
        public IActionResult GetHotelRooms(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }
            var rooms = _mapper.Map<List<RoomDto>>(_hotelRepository.GetHotelRooms(HotelId));
        
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rooms);
        }
        
        [HttpGet("{HotelId}/Rooms/Count")]
        [ProducesResponseType(200, Type = typeof(int))]
        public int GetHotelRoomsCount(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
                return -1;
            return _hotelRepository.GetHotelRoomsCount(HotelId);
        }

        [HttpGet("{HotelId}/Bookings")]
        [ProducesResponseType(200, Type = typeof(List<BookingDto>))]
        public IActionResult GetHotelBookings(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }
            var bookings = _hotelRepository.GetHotelBookings(HotelId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }

        [HttpGet("{HotelId}/Bookings/Count")]
        [ProducesResponseType(200, Type = typeof(int))]
        public int GetHotelBookingsCount(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
                return -1;
            return _hotelRepository.GetHotelBookingsCount(HotelId);
        }

        [HttpGet("{HotelId}/Reviews")]
        [ProducesResponseType(200, Type = typeof(List<ReviewDto>))]
        public IActionResult GetHotelReviews(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }
            var reviews = _hotelRepository.GetHotelReviews(HotelId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);
        }

        [HttpGet("{HotelId}/Rating")]
        [ProducesResponseType(200, Type = typeof(Rating))]
        public IActionResult GetHotelRating(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }
            var rating = _hotelRepository.GetHotelRating(HotelId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }

        [HttpPost("{HotelManagerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateHotel(int HotelManagerId, HotelDto newHotel) { 
            if(newHotel == null || !ModelState.IsValid)
                return BadRequest(ModelState);
                                                            
            if (!_hotelManagerRepository.HotelManagerExists(HotelManagerId))
            {
                ModelState.AddModelError("", "Hotel Manager not found");
                return StatusCode(404, ModelState);
            }

            if (_hotelRepository.HotelExists(newHotel.Name))
            {
                ModelState.AddModelError("", "Hotel name already exists");
                return StatusCode(400, ModelState);
            }

            var mappedHotel = _mapper.Map<Hotel>(newHotel);
            mappedHotel.HotelManager = _hotelManagerRepository.GetHotelManager(HotelManagerId);
            if (!_hotelRepository.CreateHotel(mappedHotel)){
                ModelState.AddModelError("", "Oops, Something went wrong while saving hotel");
                return StatusCode(500, ModelState);
            }
            
            return Ok("Hotel Registered Successfully!");
        }

        [HttpPut("{HotelId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateHotel(int HotelId, HotelDto updatedHotel) {

            if (updatedHotel == null)
                return BadRequest(ModelState);

            updatedHotel.HotelId = HotelId;
            var mappedHotel = _mapper.Map<Hotel>(updatedHotel);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_hotelRepository.UpdateHotel(mappedHotel))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while updating hotel");
                return StatusCode(500, ModelState);
            }

            return Ok("Hotel Updated Successfully!");
        }

        [HttpDelete("{HotelId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteHotel(int HotelId)
        {
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }

            var hotelToDelete = _hotelRepository.GetHotel(HotelId);
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            if (!_hotelRepository.DeleteHotel(hotelToDelete))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while Deleting hotel");
                return StatusCode(500, ModelState);
            }
            return Ok("Hotel Deleted Successfully!");
        }
    }
}
