using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ODataBookStoreWebClient.Models;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace ODataBookStoreWebClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public BooksController()
        {
            // Initialize HttpClient with the base URL of the OData API
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5271/odata/")
            };

            // Set the desired request headers (if needed)
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
                HttpResponseMessage response = await _httpClient.GetAsync("Books");
                string data = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(data);
                var list = temp.value;
                List<Book> items = ((JArray)temp.value).Select(x => new Book
                {
                    Id = (int)x["Id"],
                    Author = (string)x["Author"],
                    ISBN = (string)x["ISBN"],
                    Title = (string)x["Title"],
                    Price = (decimal)x["Price"],
                }).ToList();
                return View(items);
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
