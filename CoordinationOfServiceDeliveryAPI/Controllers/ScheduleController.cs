using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Models;
using ServicesContracts.ServiceInterfaces;
using System;
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
        public Schedule GetSchedule([FromQuery] int id)
        {
            return _scheduleService.GetSchedule(id);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpGet("get-masters-schedules")]
        public List<Schedule> GetSchedules()
        {
            return _scheduleService.GetSchedulesByMasterId(HttpContext);
        }

        [Authorize(Roles = "ADMIN, MASTER")]
        [HttpGet("get-masters-schedules-by-string-date/{date}")]
        public List<Schedule> GetMastersScheduleByDate(DateTime date)
        {
            return _scheduleService.GetSchedulesByMasterIdAndDate(HttpContext, date);
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT")]
        [HttpGet("is-masters-schedules-date-available")]
        public bool IsMastersScheduleDateAvailable([FromQuery]DateTime date, [FromQuery]int masterId)
        {
            return _scheduleService.IsMastersScheduleDateAvailable(masterId, date);
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT, OPERATOR")]
        [HttpGet("get-all-schedules")]
        public List<Schedule> GetAllSchedules()
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
