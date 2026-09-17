using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPIKZ.Models.DTOs
{
    public class LoginNewBloggerDTO
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
