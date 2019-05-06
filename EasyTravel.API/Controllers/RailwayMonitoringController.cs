﻿using EasyTravel.API.ViewModels;
using EasyTravel.Contracts.Interfaces.Services;
using EasyTravel.HangFire.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyTravel.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RailwayMonitoringController : ControllerBase
    {
        private readonly IMonitoringService monitoringService;

        public RailwayMonitoringController(RailwayMonitoringService monitoringService)
        {
            this.monitoringService = monitoringService;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(MonitoringViewModel viewModel)
        {
            monitoringService.StartMonitoring(viewModel.From, viewModel.To, viewModel.DepartureDate);
            return Ok();
        }
    }
}