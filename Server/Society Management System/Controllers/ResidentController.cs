﻿using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Models;
using Society_Management_System.Services;
using Society_Management_System.DTO;

namespace Society_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentService _residentService;

        public ResidentController(IResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Resident>>> GetResidents()
        {
            var residents = await _residentService.GetResidents();
            return Ok(residents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resident>> GetResidentById(int id)
        {
            var resident = await _residentService.GetResidentById(id);

            if (resident == null)
            {
                return NotFound();
            }

            return Ok(resident);
        }

        [HttpPost]
        public async Task<ActionResult<Resident>> AddResident(Resident resident)
        {
            var addedResident = await _residentService.AddResident(resident);
            return CreatedAtAction(nameof(GetResidentById), new { id = addedResident.Id }, addedResident);
        }
        [HttpPost("checkResident")]
        public async Task<ActionResult<Resident>> CheckResident([FromBody] CheckResidentDTO checkResident)
        {
            Resident Found = await _residentService.CheckResident(checkResident.Name, checkResident.HouseNumber);
            if(Found != null)
            {
                return Found;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Resident>> UpdateResident(int id, Resident resident)
        {
            if (id != resident.Id)
            {
                return BadRequest("Resident ID mismatch");
            }

            var updatedResident = await _residentService.UpdateResident(resident);

            if (updatedResident == null)
            {
                return NotFound();
            }

            return Ok(updatedResident);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResident(int id)
        {
            var deleted = await _residentService.DeleteResident(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
