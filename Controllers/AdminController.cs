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
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdminDto>))]
        public IActionResult GetAdmins()
        {
            return Ok(_mapper.Map<List<AdminDto>>(_adminRepository.GetAdmins()));
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAdmin(UserDto newAdmin)
        {
            if (newAdmin == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            Admin mappedNewHotelManger = _mapper.Map<Admin> (newAdmin);
            if (!_adminRepository.CreateAdmin(mappedNewHotelManger))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while saving hotel manager");
                return StatusCode(500, ModelState);
            }

            return Ok("Hotel Manager Registered Successfully!");
        }

        [HttpPut("{AdminId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateHotel(int AdminId, UserDto updatedAdmin)
        {

            if (updatedAdmin == null)
                return BadRequest(ModelState);

            if (!_adminRepository.AdminExists(AdminId))
            {
                ModelState.AddModelError("", "Hotel Manager not found");
                return StatusCode(404, ModelState);
            }

            updatedAdmin.Id = AdminId;
            var mappedAdmin = _mapper.Map<Admin>(updatedAdmin);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_adminRepository.UpdateAdmin(mappedAdmin))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while updating hotel manager");
                return StatusCode(500, ModelState);
            }

            return Ok("Hotel Manager Updated Successfully!");
        }

        [HttpDelete("{AdminId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteHotel(int AdminId)
        {
            if (!_adminRepository.AdminExists(AdminId))
            {
                ModelState.AddModelError("", "Hotel Manager not found");
                return StatusCode(404, ModelState);
            }

            var adminToDelete = _adminRepository.GetAdmin(AdminId);
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            if (!_adminRepository.DeleteAdmin(adminToDelete))
            {
                ModelState.AddModelError("", "Oops, Something went wrong while Deleting hotel manager");
                return StatusCode(500, ModelState);
            }

            return Ok("Hotel Deleted Successfully!");
        }
    }
}
