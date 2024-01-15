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
    public class HotelManagerController : ControllerBase
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IMapper _mapper;

        public HotelManagerController(IHotelManagerRepository hotelManagerRepository, IMapper mapper)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HotelManagerDto>))]
        public IActionResult GetHotelManagers()
        {
            return Ok(_hotelManagerRepository.GetHotelManagers());
        }

        [HttpGet("{ManagerId}")]
        [ProducesResponseType(200, Type = typeof(HotelManagerDto))]
        public IActionResult GetHotelManager(int ManagerId)
        {
            if (!_hotelManagerRepository.HotelManagerExists(ManagerId))
            {
                ModelState.AddModelError("", "Hotel Manager not found");
                return StatusCode(404, ModelState);
            }
            return Ok(_mapper.Map<HotelManagerDto>(_hotelManagerRepository.GetHotelManager(ManagerId)));
        }
        [HttpGet("Login/{Email}/{Password}")]
        [ProducesResponseType(200, Type = typeof(HotelManagerDto))]
        public IActionResult HotelManagerLogin(string Email, string Password)
        {
            var hotelManager = _hotelManagerRepository.HotelManagerLogin(Email, Password);
            if (hotelManager == null) { return Unauthorized(); }

            return Ok(_mapper.Map<HotelManagerDto>(hotelManager));
        }

        [HttpGet("{ManagerId}/hotel")]
        [ProducesResponseType(200, Type = typeof(HotelDto))]
        public IActionResult GetHotel(int ManagerId)
        {
            if (!_hotelManagerRepository.HotelManagerExists(ManagerId))
            {
                ModelState.AddModelError("", "Hotel Manager not found");
                return StatusCode(404, ModelState);
            }
            return Ok(_mapper.Map<HotelDto>(_hotelManagerRepository.GetHotel(ManagerId)));
        }

        [HttpGet("{ManagerId}/rooms")]
        [ProducesResponseType(200, Type = typeof(ICollection<RoomDto>))]
        public IActionResult GetRooms(int ManagerId)
        {
            if (!_hotelManagerRepository.HotelManagerExists(ManagerId))
            {
                ModelState.AddModelError("", "Hotel Manager not found");
                return StatusCode(404, ModelState);
            }
            return Ok(_mapper.Map<List<RoomDto>>(_hotelManagerRepository.GetRooms(ManagerId)));
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateHotelManager(UserDto newHotelManager)
        {
            if (newHotelManager == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            HotelManager mappedHotelManager = _mapper.Map<HotelManager> (newHotelManager);
            var HotelManagerCreated = _hotelManagerRepository.CreateHotelManager(mappedHotelManager);
            if (HotelManagerCreated == null)
            {
                ModelState.AddModelError("", "Oops, Something went wrong while saving hotel manager");
                return StatusCode(500, ModelState);
            }

            return Ok(_mapper.Map<HotelManagerDto>(HotelManagerCreated));
        }

        [HttpPut("{HotelManagerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateHotel(int HotelManagerId, UserDto updatedHotelManager)
        {

            if (updatedHotelManager == null)
                return BadRequest(ModelState);

            if (!_hotelManagerRepository.HotelManagerExists(HotelManagerId))
            {
                ModelState.AddModelError("", "Hotel Manager not found");
                return StatusCode(404, ModelState);
            }

            updatedHotelManager.Id = HotelManagerId;
            var mappedHotelManager = _mapper.Map<HotelManager>(updatedHotelManager);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_hotelManagerRepository.UpdateHotelManager(mappedHotelManager))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while updating hotel manager");
                return StatusCode(500, ModelState);
            }

            return Ok("Hotel Manager Updated Successfully!");
        }

        [HttpDelete("{HotelManagerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteHotel(int HotelManagerId)
        {
            if (!_hotelManagerRepository.HotelManagerExists(HotelManagerId))
            {
                ModelState.AddModelError("", "Hotel Manager not found");
                return StatusCode(404, ModelState);
            }

            var hotelManagerToDelete = _hotelManagerRepository.GetHotelManager(HotelManagerId);
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            if (!_hotelManagerRepository.DeleteHotelManager(hotelManagerToDelete))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while Deleting hotel manager");
                return StatusCode(500, ModelState);
            }

            return Ok("Hotel Manager Deleted Successfully!");
        }
    }
}
