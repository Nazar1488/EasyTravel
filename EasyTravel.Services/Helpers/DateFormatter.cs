﻿using System;
using EasyTravel.Contracts.Interfaces;

namespace EasyTravel.Services.Helpers
{
    public class DateFormatter : IDateFormatter
    {
        public string BlaBlaCarDate(DateTime date, TimeSpan time)
        {
            var timeString = time.ToString("HH:mm:ss");
            var dateString = date.ToString("yyyy-MM-dd");
            return $"{dateString} {timeString}";
        }

        public string RailwayDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public string RailwayTime(TimeSpan time)
        {
            return time.ToString("HH:mm");
        }

        public string BusDate(DateTime date)
        {
            return date.ToString("dd.MM.yy");
        }
    }
}
