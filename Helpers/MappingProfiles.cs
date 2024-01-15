using AutoMapper;
using MarefiyaApi.Dto;
using MarefiyaApi.Models;

namespace MarefiyaApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UserDto, Admin>();
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminDto, Admin>();

            CreateMap<UserDto, HotelManager>();
            CreateMap<HotelManager, HotelManagerDto>();
            CreateMap<HotelManager, UserDto>();
            CreateMap<HotelManagerDto, HotelManager>();

            CreateMap<UserDto, Receptionist>();
            CreateMap<Receptionist, ReceptionistDto>();
            CreateMap<ReceptionistDto, Receptionist>();

            CreateMap<UserDto, Customer>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, UserDto>();

            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelDto, Hotel>();

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();

            CreateMap<Booking, BookingDto>();
            CreateMap<BookingDto, Booking>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
        }
    }
}
