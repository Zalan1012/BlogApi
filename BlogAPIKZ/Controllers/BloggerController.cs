using BlogAPIKZ.Models;
using BlogAPIKZ.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace BlogAPIKZ.Controllers
{
    [Route("bloggers")]
    [ApiController]
    public class BloggerController : ControllerBase
    {

        public string ConnectionString = "server=localhost;database=blog;uid=root;pawword=";

        [HttpGet("all")]
        public object GetAllBlogger()
        {
            List<Blogger> bloggers = new List<Blogger>();

            var connector = new MySqlConnection(ConnectionString);

            connector.Open();

            string sql = @"SELECT * FROM `blogger`";

            var cmd = new MySqlCommand(sql, connector);

            var datareader = cmd.ExecuteReader();

            while (datareader.Read())
            {
                var blogger = new Blogger
                {
                    Id = datareader.GetInt32(0),
                    Name = datareader.GetString(1),
                    Email = datareader.GetString(2),
                    Age = datareader.GetInt32(3),
                    Password = datareader.GetString(4),
                    RegistrationTime = datareader.GetDateTime(5),
                };

                bloggers.Add(blogger);
            }

            connector.Close();

            return new { message = "Sikeres Lekérdezés", result = bloggers };
        }

        [HttpGet("byId")]
        public object GetBloggerById(int id)
        {
            var connector = new MySqlConnection(ConnectionString);

            connector.Open();

            string sql = @"SELECT * FROM `blogger` WHERE `id` = @id;";

            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();

            datareader.Read();

            var blogger = new Blogger
            {
                Id = datareader.GetInt32(0), 
                Name = datareader.GetString(1),
                Email = datareader.GetString(2),
                Age = datareader.GetInt32(3),
                Password = datareader.GetString(4),
                RegistrationTime = datareader.GetDateTime(5)
            };

            connector.Close();
            return new { message = "Sikeres találat.", result = blogger };
        }

        [HttpPost]

        public object AddNewBlogger(AddNewBloggerDTO addNewBloggerDTO)
        {

            var connector = new MySqlConnection(ConnectionString);
                
            connector.Open();

            string sql = @"INSERT INTO `blogger`(`Name`, `Email`, `Age`, `Password`, `RegistrationTime`) VALUES (@name, @email, @age, @password, @registrationTime)";

            var cmd = new MySqlCommand(sql, connector); 

            cmd.Parameters.AddWithValue("@name", addNewBloggerDTO.Name);
            cmd.Parameters.AddWithValue("@email", addNewBloggerDTO.Email);
            cmd.Parameters.AddWithValue("@age", addNewBloggerDTO.Age);
            cmd.Parameters.AddWithValue("@password", addNewBloggerDTO.Password);
            cmd.Parameters.AddWithValue("@registrationTime", DateTime.Now);

            cmd.ExecuteNonQuery();

            connector.Close();

            return new { message = "Sikeres hozzáadás.", result= addNewBloggerDTO };
        }
    }   
}