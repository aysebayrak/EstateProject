﻿using Emlak.UI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Emlak.UI.Controllers
{
    public class DefaultController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string p, int price, int room)
        {

            ViewBag.userName = HttpContext.Session.GetString("UserName");
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Estate/getall");

            if (responseMessage.IsSuccessStatusCode)
            {

                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<Estate>>(jsonData);

                if (!string.IsNullOrEmpty(p))
                {
                    var searchString = p.ToLower();
                    values = values.Where(y =>
                y.City.ToLower().Contains(searchString) ||
                y.Title.ToLower().Contains(searchString) ||
                y.Medicine.ToLower().Contains(searchString)
            ).ToList();
                }

                if (price != 0 && room != 0)
                {
                    values = values.Where(y => y.Price <= price && y.Room == room).ToList();
                }

                else if (price != 0)
                {
                    values = values.Where(y => y.Price == price).ToList();
                }
                else if (room != 0)
                {
                    values = values.Where(y => y.Room == room).ToList();
                }
                else
                {
                    return View(values);
                }

                return View(values);
            }

            return View();


        }



        public async Task<IActionResult> Get(string id)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7196/api/Estate/get?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Estate>(jsonData);
                return View(values);
            }

            return View();
        }

    }
}
