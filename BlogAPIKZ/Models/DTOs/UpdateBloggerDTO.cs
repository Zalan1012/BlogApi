using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPIKZ.Models.DTOs
{
    public class UpdateBloggerDTO
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public int Age { get; set; }

        public string? Password { get; set; }
    }
}

