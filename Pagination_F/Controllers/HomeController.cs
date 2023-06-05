using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pagination_F.Models;
using System.Diagnostics;

namespace Pagination_F.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress;
        HttpClient client;
        public HomeController()
        {
            baseAddress = new Uri("https://localhost:7287/WeatherForecast/GetUsers");
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public ActionResult Pagination(int page = 1, int pageSize = 3)
        {
           //initially page number is 1 and pagesize that is how many records will be there on one page is set to 2.

            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<User> users = JsonConvert.DeserializeObject<List<User>>(data);
            int totalCount = users.Count;

            //total number of data will be grouped in set of 2.as pagesize is 2
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            //starting of set.
            int startRow = (page - 1) * pageSize;

            //it will take records from end of previous  set.
            var pagedUsers = users.Skip(startRow).Take(pageSize);

            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return View(pagedUsers);
        }
    }

    }
