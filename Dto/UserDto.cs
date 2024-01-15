﻿namespace MarefiyaApi.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string ProfilePicture { get; set; }
        public string? Role  { get; set; }
    }
}
