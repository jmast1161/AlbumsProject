using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Albums.Models;
using System.Net;
using Newtonsoft.Json;

namespace Albums.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private DataRepository _dataRepo = new DataRepository();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string currentFilter, string searchString, int? pageNumber)
        {
            var entries = _dataRepo.AlbumEntries;

            if (!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }
            else 
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                entries = entries.Where(e => e.UserInfo.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) || e.AlbumTitle.Contains(searchString)).ToList();
            }

            return View(PagedList<AlbumEntryData>.CreatePage(entries, pageNumber ?? 1, 10));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
