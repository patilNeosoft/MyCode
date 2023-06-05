using Microsoft.AspNetCore.Mvc;

namespace Pagination.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        List<User> users = new List<User>
        {
            new User { Id = 1, Name = "John" },
            new User { Id = 2, Name = "Alice" },
            new User { Id = 3, Name = "Bob" },
            new User { Id = 4, Name = "Emily" },
            new User { Id = 5, Name = "David" },
            new User { Id = 6, Name = "Olivia" },
            new User { Id = 7, Name = "Sophia" },
            new User { Id = 8, Name = "Jacob" },
            new User { Id = 9, Name = "Emma" },
            new User { Id = 10, Name = "Liam" }
        };

        [HttpGet("GetUsers",Name = "GetUsers")]
        public IActionResult Get()
        {
            return Ok(users);
        }


        //pagination for backend
        [HttpGet("Pagination",Name = "Pagination")]
        public IActionResult Get(int page = 1, int pageSize = 2)
        {
            int totalCount = users.Count;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int startRow = (page - 1) * pageSize;

            var pagedUsers = users.Skip(startRow).Take(pageSize);

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Users = pagedUsers
            };

            return Ok(result);
        }
    }
}