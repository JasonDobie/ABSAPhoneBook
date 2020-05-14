using ABSAPhoneBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ABSAPhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Globals _globals;
        private PhoneBook _phoneBook;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _globals = new Globals(config);
        }

        [HttpGet]
        public IActionResult PhoneBook()
        {
            try
            {
                if (System.IO.File.Exists(_globals.Path))
                {
                    _phoneBook = JsonConvert.DeserializeObject<PhoneBook>(System.IO.File.ReadAllText(_globals.Path));
                }
                else
                {
                    _phoneBook = new PhoneBook { Name = "ABSA Phone Book" };
                    System.IO.File.WriteAllText(_globals.Path, JsonConvert.SerializeObject(_phoneBook));
                }

                if (_phoneBook.Entries == null)
                {
                    _phoneBook.Entries = new List<Entry>();
                }

                HttpContext.Session.SetString("PhoneBook", JsonConvert.SerializeObject(_phoneBook));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return View(_phoneBook);
        }

        [HttpPost]
        public IActionResult PhoneBook(string searchString)
        {
            _phoneBook = JsonConvert.DeserializeObject<PhoneBook>(HttpContext.Session.GetString("PhoneBook"));

            if (!string.IsNullOrEmpty(searchString))
            {
                _phoneBook.Entries = _phoneBook.Entries.Where(e => e.Name.Contains(searchString)).ToList();
            }
            
            return View(_phoneBook);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Entry entry = new Entry();

            return View(entry);
        }

        [HttpPost]
        public IActionResult Create(Entry model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _phoneBook = JsonConvert.DeserializeObject<PhoneBook>(HttpContext.Session.GetString("PhoneBook"));
                    _phoneBook.Entries.Add(model);

                    _phoneBook.Entries = _phoneBook.Entries.OrderBy(e => e.Name).ToList();

                    HttpContext.Session.SetString("PhoneBook", JsonConvert.SerializeObject(_phoneBook));
                    System.IO.File.WriteAllText(_globals.Path, JsonConvert.SerializeObject(_phoneBook));
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return View("PhoneBook", _phoneBook);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
