﻿using System;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public async Task<IActionResult>

        public IActionResult Index()
        {
            var webClient = new WebClient();
            var userJson = webClient.DownloadString(@"https://jsonplaceholder.typicode.com/users");
            var photosJson = webClient.DownloadString(@"https://jsonplaceholder.typicode.com/photos");
            var albumsJson = webClient.DownloadString(@"https://jsonplaceholder.typicode.com/albums");

            var userData = JsonConvert.DeserializeObject<List<UserData>>(userJson);
            var photoData = JsonConvert.DeserializeObject<List<PhotoData>>(photosJson);
            var albumData = JsonConvert.DeserializeObject<List<AlbumData>>(albumsJson);

            var users = new Users()
            {
                AllUsers = userData,
            };

            var albumEntries = new AlbumEntries();
            albumEntries.AlbumEntires = new List<AlbumEntryData>();

            // not making any assumptions as to data ids guaranteed in ascending order
            // otherwise would track index to avoid iterating entire list of albums
            foreach (var user in userData)
            {
                foreach (var album in albumData)
                {
                    if (user.Id.Equals(album.UserId))
                    {
                        var albumEntry = new AlbumEntryData();
                        albumEntry.AlbumThumbnails = new List<AlbumThumbnailData>();

                        var albumThumbnails = new List<AlbumThumbnailData>();
                        foreach (var photo in photoData)
                        {
                            if (photo.Id == album.Id)
                            {
                                albumThumbnails.Add(new AlbumThumbnailData() { AlbumThumbnailPath = photo.ThumbnailUrl,});
                            }
                        }

                        albumEntry.AlbumThumbnails.AddRange(albumThumbnails);
                        albumEntry.AlbumTitle = album.Title;
                        albumEntry.Name = user.Name;
                        albumEntry.Phone = user.Phone;
                        albumEntry.Email = user.Email;
                        albumEntry.Address = user.Address;

                        albumEntries.AlbumEntires.Add(albumEntry);
                    }
                }
            }

            return View(albumEntries);
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
