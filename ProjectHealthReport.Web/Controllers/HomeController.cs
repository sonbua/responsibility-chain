﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectHealthReport.Features.TestFeature;
using ProjectHealthReport.Web.Models;
using ResponsibilityChain.Business;

namespace ProjectHealthReport.Web.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator )
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var dto = await _mediator.SendAsync(new GetTestFeatureQuery());
            return View();
        }
        
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Index2()
        {
            var dto = await _mediator.SendAsync(new GetTestFeatureQuery());
            return View("Index");
        }

        public async Task<IActionResult> Privacy()
        {
            await HttpContext.SignOutAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}