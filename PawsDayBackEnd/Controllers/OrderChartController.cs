using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.Models;
using PawsDayBackEnd.Services;
using System.Collections.Generic;

namespace PawsDayBackEnd.Controllers
{
    public class OrderChartController : Controller
    {
        private readonly ChartOrderServices _orderChartservice;

        public OrderChartController(ChartOrderServices orderChartservice)
        {
            _orderChartservice = orderChartservice;
        }

        public IActionResult Sales()
        {
            return View();
        }

    }
}
