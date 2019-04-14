﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTravel.Contracts.Interfaces
{
    public interface ITripFinder
    {
        Task<IEnumerable<ITrip>> FindTripsAsync(string from, string to, DateTime departureDate, TimeSpan departureTime);
    }
}