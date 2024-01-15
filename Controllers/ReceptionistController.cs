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
    public class ReceptionistController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly IMapper _mapper;

        public ReceptionistController(IReceptionistRepository receptionistRepository, IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _receptionistRepository = receptionistRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReceptionistDto>))]
        public IActionResult GetReceptionists()
        {
            return Ok(_mapper.Map<List<ReceptionistDto>>(_receptionistRepository.GetReceptionists()));
        }

        [HttpGet("{ReceptionistId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReceptionistDto>))]
        public IActionResult GetReceptionist(int ReceptionistId)
        {
            return Ok(_mapper.Map<ReceptionistDto>(_receptionistRepository.GetReceptionist(ReceptionistId)));
        }

        [HttpGet("{ReceptionistId}/hotel")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReceptionistDto>))]
        public IActionResult GetReceptionistHotel(int ReceptionistId)
        {
            return Ok(_mapper.Map<HotelDto>(_receptionistRepository.GetReceptionistHotel(ReceptionistId)));
        }

        [HttpPost("{HotelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReceptionist(int HotelId, UserDto newReceptionist)
        {
            if (newReceptionist == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_hotelRepository.HotelExists(HotelId))
            {
                ModelState.AddModelError("", "Hotel not found");
                return StatusCode(404, ModelState);
            }
            Receptionist mappedReceptionist = _mapper.Map<Receptionist> (newReceptionist);
            mappedReceptionist.Hotel = _hotelRepository.GetHotel(HotelId);

            if (!_receptionistRepository.CreateReceptionist(mappedReceptionist))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while saving receptionist");
                return StatusCode(500, ModelState);
            }

            return Ok("Receptionist Registered Successfully!");
        }

        [HttpPut("{ReceptionistId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateHotel(int ReceptionistId, UserDto updatedReceptionist)
        {

            if (updatedReceptionist == null)
                return BadRequest(ModelState);

            if (!_receptionistRepository.ReceptionistExists(ReceptionistId))
            {
                ModelState.AddModelError("", "Receptionist not found");
                return StatusCode(404, ModelState);
            }

            updatedReceptionist.Id = ReceptionistId;
            var mappedReceptionist = _mapper.Map<Receptionist>(updatedReceptionist);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_receptionistRepository.UpdateReceptionist(mappedReceptionist))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while updating receptionist");
                return StatusCode(500, ModelState);
            }

            return Ok("Receptionist Updated Successfully!");
        }

        [HttpDelete("{ReceptionistId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteHotel(int ReceptionistId)
        {
            if (!_receptionistRepository.ReceptionistExists(ReceptionistId))
            {
                ModelState.AddModelError("", "Receptionist not found");
                return StatusCode(404, ModelState);
            }

            var receptionistToDelete = _receptionistRepository.GetReceptionist(ReceptionistId);
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            if (!_receptionistRepository.DeleteReceptionist(receptionistToDelete))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while Deleting receptionist");
                return StatusCode(500, ModelState);
            }

            return Ok("Receptionist Deleted Successfully!");
        }
    }
}
