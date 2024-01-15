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
    public class RoomController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomController(IHotelRepository hotelRepository, IRoomRepository roomRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet("{roomId}")]
        [ProducesResponseType(200, Type=typeof(RoomDto))]
        public IActionResult GetRoom(int roomId)
        {
            if (!_roomRepository.RoomExists(roomId))
            {
                ModelState.AddModelError("", "Room not found.");
                return StatusCode(404, ModelState);
            }
            var room = _mapper.Map<RoomDto>(_roomRepository.GetRoom(roomId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_roomRepository.GetRoom(roomId));
        }
        
        [HttpGet("type/{roomType}")]
        [ProducesResponseType(200, Type= typeof(List<RoomDto>))]
        public IActionResult GetRoomsByType(string roomType)
        {
            return Ok(_mapper.Map<List<RoomController>>(_roomRepository.GetRoomsByType(roomType)));
        }

        [HttpGet("type/{roomType}/Count")]
        [ProducesResponseType(200, Type = typeof(List<RoomDto>))]
        public IActionResult GetRoomsCountByType(string roomType)
        {
            return Ok(_roomRepository.GetRoomsCountByType(roomType));
        }

        [HttpGet("{roomId}/Bookings")]
        [ProducesResponseType(200, Type = typeof(List<RoomDto>))]
        public IActionResult GetRoomBookings(int roomId)
        {
            if (!_roomRepository.RoomExists(roomId))
            {
                ModelState.AddModelError("", "Room not found.");
                return StatusCode(404, ModelState);
            }
            var bookings = _mapper.Map<BookingDto>(_roomRepository.GetRoomBookings(roomId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }


        [HttpGet("{roomId}/Reviews")]
        [ProducesResponseType(200, Type = typeof(List<RoomDto>))]
        public IActionResult GetRoomReviews(int roomId)
        {
            if (!_roomRepository.RoomExists(roomId))
            {
                ModelState.AddModelError("", "Room not found.");
                return StatusCode(404, ModelState);
            }
            var reviews = _mapper.Map<ReviewDto>(_roomRepository.GetRoomReviews(roomId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }


        [HttpGet("{roomId}/Rating")]
        [ProducesResponseType(200, Type = typeof(Rating))]
        public IActionResult GetRoomRating(int roomId)
        {
            if (!_roomRepository.RoomExists(roomId))
            {
                ModelState.AddModelError("", "Room not found.");
                return StatusCode(404, ModelState);
            }
            var rating = _roomRepository.GetRoomRating(roomId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }


        [HttpPost("{HotelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRoom(int HotelId, RoomDto newRoom)
        {
            if (newRoom == null)
                return BadRequest(ModelState);

            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }

            var mappedRoom = _mapper.Map<Room>(newRoom);
            mappedRoom.Hotel = _hotelRepository.GetHotel(HotelId);
            if (!_roomRepository.CreateRoom(mappedRoom))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while saving room");
                return StatusCode(500, ModelState);
            }

            return Ok("Room Added Successfully!");
        }

        [HttpPut("{RoomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRoom(int RoomId, RoomDto updatedRoom)
        {
            if (updatedRoom == null)
                return BadRequest(ModelState);
            if (!_roomRepository.RoomExists(RoomId))
            {
                ModelState.AddModelError("", "Room not found");
                return StatusCode(404, ModelState);
            }
            //prevent RoomId changes
            updatedRoom.RoomId = RoomId;

            var mappedRoom = _mapper.Map<Room>(updatedRoom);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_roomRepository.UpdateRoom(mappedRoom))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while updating hotel");
                return StatusCode(500, ModelState);
            }

            return Ok("Room Updated Successfully!");
        }

        [HttpDelete("{RoomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRoom(int RoomId)
        {
            if (!_roomRepository.RoomExists(RoomId))
            {
                ModelState.AddModelError("", "Room not found");
                return StatusCode(404, ModelState);
            }

            var hotelToDelete = _roomRepository.GetRoom(RoomId);
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            if (!_roomRepository.DeleteRoom(hotelToDelete))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while deleting room");
                return StatusCode(500, ModelState);
            }
            return Ok("Room Deleted Successfully!");
        }

    }
}
