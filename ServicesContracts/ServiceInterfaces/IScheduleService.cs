﻿using Microsoft.AspNetCore.Http;
using RepositoryContractsDb.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IScheduleService
    {
        void SetSchedule(Schedule schedule, HttpContext httpContext);

        void UpdateSchedule(Schedule schedule, HttpContext httpContext);

        void DeleteSchedule(int id, HttpContext httpContext);

        List<Schedule> GetSchedulesByMasterId(HttpContext httpContext);

        List<Schedule> GetSchedules();

        Schedule GetSchedule(int id);
    }
}