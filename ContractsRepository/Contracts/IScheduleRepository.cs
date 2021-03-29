using RepositoryContractsDb.Models;
using System;
using System.Collections.Generic;

namespace RepositoryContractsDb.Contracts
{
    public interface IScheduleRepository
    {
        void CreateSchedule(Schedule schedule);

        Schedule GetSchedule(int id);

        List<Schedule> GetSchedules();

        List<Schedule> GetMastersSchedule(int masterId);

        void UpdateSchedule(Schedule schedule);

        void DeleteSchedule(int id);

        List<Schedule> GetMastersScheduleByDate(int masterId, DateTime date);

        List<Schedule> GetMastersScheduleByDateAndReadyStatus(int masterId, DateTime date);
    }
}
