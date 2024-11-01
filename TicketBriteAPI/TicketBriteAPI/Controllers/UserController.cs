﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("get-user")]
        [Authorize] // Beveilig het eindpunt met JWT-authenticatie
        public IActionResult GetUserData()
        {
            // Haal de gebruikersnaam van de claims
            var username = User.FindFirst("name")?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return NotFound("Gebruiker niet gevonden.");
            }

            // Hier kun je andere gebruikersgegevens ophalen, bijvoorbeeld uit een database
            var userModel = new 
            {
                Username = username,
                // Vul hier andere gegevens in
                Email = "user@example.com" // Dit moet normaal uit je database komen
            };

            return Ok(userModel); // Retourneer de gebruikersgegevens
        }
    }
}