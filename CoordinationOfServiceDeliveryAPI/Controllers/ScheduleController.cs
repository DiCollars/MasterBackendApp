﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpPost("add-schedule")]
        public void AddSchedule([FromBody] Schedule schedule)
        {
            _scheduleService.SetSchedule(schedule, HttpContext);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-schedule")]
        public ScheduleShort GetSchedule([FromQuery] int id)
        {
            return _scheduleService.GetSchedule(id);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpGet("get-masters-schedules")]
        public List<ScheduleShort> GetSchedules()
        {
            return _scheduleService.GetSchedulesByMasterId(HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-all-schedules")]
        public List<ScheduleShort> GetAllSchedules()
        {
            return _scheduleService.GetSchedules();
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpPut("edit-schedule")]
        public void EditSchedule([FromBody] Schedule schedule)
        {
            _scheduleService.UpdateSchedule(schedule, HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpDelete("delete-schedule")]
        public void DeleteSchedule([FromQuery] int id)
        {
            _scheduleService.DeleteSchedule(id, HttpContext);
        }
    }
}
