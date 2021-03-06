﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyTravel.Contracts.Interfaces;
using EasyTravel.Contracts.Interfaces.Core;
using EasyTravel.Contracts.Interfaces.Helpers;
using EasyTravel.Contracts.Interfaces.Services;
using EasyTravel.Core.Config;
using EasyTravel.Core.Models.Bus;
using EasyTravel.Services.Helpers.Bus;
using Microsoft.Extensions.Options;

namespace EasyTravel.Services.Bus
{
    public class BusFinder : ITripFinder
    {
        private readonly IHttpService httpService;
        private readonly IDateFormatter dateFormatter;
        private readonly IMapsService mapsService;
        private readonly ILinkBuilder linkBuilder;
        private readonly BusConfig config;
        private readonly IEnumerable<Station> availableStations;

        public BusFinder(IHttpService httpService, IDateFormatter dateFormatter, IOptions<BusConfig> options, IMapsService mapsService, ILinkBuilder linkBuilder)
        {
            this.httpService = httpService;
            this.dateFormatter = dateFormatter;
            this.mapsService = mapsService;
            this.linkBuilder = linkBuilder;
            config = options.Value;
            availableStations = GetAvailableStations().Result;
        }

        public async Task<IEnumerable<ITrip>> FindTripsAsync(string from, string to, DateTime departureDate)
        {
            var fromCode = availableStations.Select(i => i).FirstOrDefault(i => i.Location == from.ToUpper())?.Code;
            var toCode = availableStations.Select(i => i).FirstOrDefault(i => i.Location == to.ToUpper())?.Code;
            var url = config.ApiUrl.Replace("{from}", fromCode).Replace("{to}", toCode)
                .Replace("{date}", dateFormatter.BusDate(departureDate));
            var headers = new WebHeaderCollection
            {
                {"Accept-Language", "ua"}
            };
            var response = await httpService.MakeGetRequestAsync(url, headers);
            var responseString = await new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEndAsync();
            var result = Parser.ParseTrips(responseString).ToList();
            result.ForEach(t =>
            {
                t.FromCode = fromCode;
                t.ToCode = toCode;
                t.BookingLink = linkBuilder.BuildBusLink(t);
            });
            result.RemoveAll(t => t.DepartureDate < departureDate);
            return result;
        }

        public async Task<IEnumerable<ITrip>> FindAllTripsAsync(string @from, string to, DateTime departureDate)
        {
            var buses = new List<ITrip>();
            var locations = (await mapsService.FindLocationsBetweenAsync(from, to)).ToList();
            for (var i = locations.Count - 1; i >= 1; --i)
            {
                var result = await FindTripsAsync(from, locations[i], departureDate);
                buses.AddRange(result);
            }

            return buses;
        }

        private async Task<IEnumerable<Station>> GetAvailableStations()
        {
            var headers = new WebHeaderCollection
            {
                {"Accept-Language", "ua"}
            };
            var response = await httpService.MakeGetRequestAsync(config.SiteUrl, headers);
            var responseString = await new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEndAsync();
            var result = Parser.ParseAllStations(responseString);
            return result;
        }
    }
}
