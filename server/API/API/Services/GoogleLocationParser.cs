﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Controllers.UsersController;

namespace API.Services
{
    public class GoogleLocationParser
    {
        public IEnumerable<Locations> Parse(string json)
        {
            var response = new List<Locations>();
            var jsonData = JsonConvert.DeserializeObject<GoogleRootObject>(json);

            foreach (var item in jsonData.Locations)
            {
                response.Add(new Locations
                {
                    DateTimeUtc = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(item.timestampMs)).UtcDateTime,
                    Latitude = item.latitudeE7,
                    Longitude = item.longitudeE7,
                    LocationSource = LocationSource.Google.ToString()
                });
            }

            return response.Where(s => s.DateTimeUtc >= new DateTime(2020, 3, 1));
        }
    }


    public class GoogleRootObject
    {
        [JsonProperty("locations")]
        public List<GoogleLocation> Locations { get; set; }
    }

    public class GoogleLocation
    {
        public string timestampMs { get; set; }
        public int latitudeE7 { get; set; }
        public int longitudeE7 { get; set; }
        public int? accuracy { get; set; }
        public int? velocity { get; set; }
        public int? altitude { get; set; }
        public int? verticalAccuracy { get; set; }
        public int? heading { get; set; }
    }
}
