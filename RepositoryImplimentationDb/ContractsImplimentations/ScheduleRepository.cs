using Dapper;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplimentationDb.ContractsImplimentations
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ISqlRepositoryBase _sqlRepositoryBase;

        public ScheduleRepository(ISqlRepositoryBase sqlRepositoryBase)
        {
            _sqlRepositoryBase = sqlRepositoryBase;
        }

        public void CreateSchedule(Schedule schedule)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "INSERT INTO \"Schedules\" (\"MasterId\", \"WorkingHours\", \"Status\") VALUES(@MasterId, @WorkingHours, @Status)";
            db.Execute(sqlQuery, schedule);
        }

        public void DeleteSchedule(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "DELETE FROM \"Schedules\" WHERE \"Id\" = @id";
            db.Execute(sqlQuery, new { id });
        }

        public Schedule GetSchedule(int id)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Schedule>("SELECT \"Id\", \"MasterId\", \"WorkingHours\", \"Status\" FROM \"Schedules\" WHERE \"Id\" = @id", new { id }).FirstOrDefault();
        }

        public List<Schedule> GetMastersSchedule(int masterId)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Schedule>("SELECT \"Id\", \"MasterId\", \"WorkingHours\", \"Status\" FROM \"Schedules\" WHERE \"MasterId\" = @masterId", new { masterId }).ToList();
        }

        public List<Schedule> GetMastersScheduleByDate(int masterId, DateTime date)
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Schedule>("SELECT \"Id\", \"MasterId\", \"WorkingHours\", \"Status\" FROM \"Schedules\" WHERE \"MasterId\" = @masterId AND  \"WorkingHours\"::date =@date", new { masterId, date }).ToList();
        }

        public List<Schedule> GetSchedules()
        {
            using var db = _sqlRepositoryBase.Connection();
            return db.Query<Schedule>("SELECT \"Id\", \"MasterId\", \"WorkingHours\", \"Status\" FROM \"Schedules\"").ToList();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            using var db = _sqlRepositoryBase.Connection();
            var sqlQuery = "UPDATE \"Schedules\" SET \"MasterId\" = @MasterId, \"WorkingHours\" = @WorkingHours, \"Status\" = @Status WHERE \"Id\" = @Id";
            db.Execute(sqlQuery, schedule);
        }
    }
}
