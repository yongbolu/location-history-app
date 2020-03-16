﻿using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/users")]
    public partial class UsersController : Controller
    {
        private readonly UserLocationsService locationService;
        private readonly ILogger<UsersController> logger;

        public UsersController(UserLocationsService locationService, ILogger<UsersController> logger)
        {
            this.locationService = locationService;
            this.logger = logger;
        }

        [HttpGet("{userId}/locations")]
        public async Task<IEnumerable<LocationViewModel>> GetAsync(string userId)
        {
            var locations = await locationService.GetUserLocations(userId);

            var result = locations.Select(s => new LocationViewModel
            {
                DateTimeUtc = s.DateTimeUtc,
                Accuracy = s.Accuracy,
                Latitude = s.Latitude,
                Longitude = s.Longitude
            });

            return result;
        }

        [HttpPost("{userId}/file")]
        public async Task<IActionResult> UploadFileAsync(string userId, [FromForm]IFormFile file)
        {
            try
            {
                var userFolderPath = $"{Directory.GetCurrentDirectory()}/wwwroot/{userId}";
                if (!System.IO.File.Exists(userFolderPath))
                {
                    Directory.CreateDirectory(userFolderPath);

                    var path = Path.Combine(userFolderPath, file.FileName);
                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var userFolderDataPath = Directory.CreateDirectory(Path.Combine(userFolderPath, "data"));
                    ZipFile.ExtractToDirectory(path, userFolderDataPath.FullName);
                    string jsonPath = Path.Combine(userFolderDataPath.FullName, "Takeout", "Location History", "Location History.json");

                    var json = System.IO.File.ReadAllText(jsonPath);

                    await locationService.CreateLocations(userId, json);


                    System.IO.File.SetAttributes(userFolderPath, FileAttributes.Normal);
                    System.IO.File.Delete(userFolderPath);
                }
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, nameof(UsersController));
            }

            return Ok();
        }
    }
}
