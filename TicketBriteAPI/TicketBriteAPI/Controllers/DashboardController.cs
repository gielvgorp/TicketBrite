﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Enums;
using TicketBrite.Core.Services;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;
        private readonly AuthService _authService;
        public DashboardController(DashboardService dashboardService, AuthService authService) 
        { 
            _dashboardService = dashboardService;
            _authService = authService;
        }

        [HttpGet("tickets-statistics/{eventID}")]
        [Authorize]
        [ProducesResponseType(typeof(List<TicketStatistic>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult GetTicketStats(Guid eventID)
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || !_authService.VerifyAccessPermission(userID, Roles.Organization))
                {
                    throw new UnauthorizedAccessException();
                }

                List<TicketStatistic> model = _dashboardService.GetTicketStatistics(eventID);
                return new JsonResult(Ok(model));
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(Unauthorized(ExceptionMessages.UnauthorizedAccess));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }
    }
}
