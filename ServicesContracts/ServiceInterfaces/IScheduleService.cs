using Microsoft.AspNetCore.Http;
using RepositoryContractsDb.Models;
using System;
using System.Collections.Generic;

namespace ServicesContracts.ServiceInterfaces
{
    public interface IScheduleService
    {
        void SetSchedule(Schedule schedule, HttpContext httpContext);

        void UpdateSchedule(Schedule schedule, HttpContext httpContext);

        void UpdateScheduleForOperatorOrAdmin(Schedule schedule);

        void DeleteSchedule(int id, HttpContext httpContext);

        List<Schedule> GetSchedulesByMasterId(HttpContext httpContext);

        List<Schedule> GetSchedulesByDate(HttpContext httpContext, DateTime dateTime);

        List<Schedule> GetSchedulesByMasterIdAndDate(int masterId, DateTime dateTime);

        bool IsMastersScheduleDateAvailable(int masterId, DateTime date);

        List<Schedule> GetSchedules();

        Schedule GetSchedule(int id);

        List<Schedule> GetSchedulesByMasterIdAndDateHours(int masterId, DateTime dateTime);
    }
}
