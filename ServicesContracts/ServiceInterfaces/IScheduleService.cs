using Microsoft.AspNetCore.Http;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IScheduleService
    {
        void SetSchedule(Schedule schedule, HttpContext httpContext);

        void UpdateSchedule(Schedule schedule, HttpContext httpContext);

        void DeleteSchedule(int id, HttpContext httpContext);

        List<ScheduleShort> GetSchedulesByMasterId(HttpContext httpContext);

        List<ScheduleShort> GetSchedules();

        ScheduleShort GetSchedule(int id);
    }
}
