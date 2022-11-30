using ApplicationCore.Common;
using ApplicationCore.Entities;
using EllipticCurve;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDay.Services.SitterCenter;
using System;
using System.Collections.Generic;

namespace PawsDay.WebApi.BecomePetsitter
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitterAptitudeWebApiController : ControllerBase
    {
        private readonly SitterCenterAptitudeService _aptitudeService;

        public SitterAptitudeWebApiController(SitterCenterAptitudeService aptitudeService)
        {
            _aptitudeService = aptitudeService;
        }

        [HttpGet]
        public int[] GetAptitudePoint()
        {
            int memberId = _aptitudeService.GetMemberId();
            int[] result = _aptitudeService.GetAptitudePoint(memberId);

            return result;
        }
    }
}