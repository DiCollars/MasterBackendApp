using AutoMapper;
using Microsoft.AspNetCore.Http;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System.Collections.Generic;

namespace ServicesImplimentation.ServiceImplimentations
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAuthUserService _authUserService;
        private readonly IMasterRepository _masterRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IUserRepository _userRepository;

        public ScheduleService(IScheduleRepository scheduleRepository, IAuthUserService authUserService, IMasterRepository masterRepository, ISpecializationRepository specializationRepository, IUserRepository userRepository)
        {
            _scheduleRepository = scheduleRepository;
            _authUserService = authUserService;
            _masterRepository = masterRepository;
            _specializationRepository = specializationRepository;
            _userRepository = userRepository;
        }

        public void SetSchedule(Schedule schedule, HttpContext httpContext)
        {
            var authUser = _authUserService.GetLoggedUser(httpContext);

            if (authUser.Role == "MASTER")
            {
                var masterId = _masterRepository.GetMasterByUserId(authUser.Id).Id;
                schedule.MasterId = masterId;
                schedule.Status = ScheduleStatus.READY;
                _scheduleRepository.CreateSchedule(schedule);
            }
        }

        public void UpdateSchedule(Schedule schedule, HttpContext httpContext)
        {
            var authUser = _authUserService.GetLoggedUserFull(httpContext);
            var masterId = _masterRepository.GetMasterByUserId(authUser.Id).Id;

            if (schedule.MasterId == masterId)
            {
                _scheduleRepository.UpdateSchedule(schedule);
            }
            
        }

        public void DeleteSchedule(int id, HttpContext httpContext)
        {
            var authUser = _authUserService.GetLoggedUserFull(httpContext);
            var masterId = _masterRepository.GetMasterByUserId(authUser.Id).Id;
            var schedule = _scheduleRepository.GetSchedule(id);

            if (schedule.MasterId == masterId)
            {
                _scheduleRepository.DeleteSchedule(id);
            }
        }

        public List<ScheduleShort> GetSchedulesByMasterId(HttpContext httpContext)
        {
            var authUser = _authUserService.GetLoggedUserFull(httpContext);
            var master = _masterRepository.GetMasterByUserId(authUser.Id);
            List<ScheduleShort> mappedSchedules = null;

            if (master != null)
            {
                var schedules = _scheduleRepository.GetMastersSchedule(master.Id);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Schedule, ScheduleShort>()));
                mappedSchedules = mapper.Map<List<ScheduleShort>>(schedules);
            }
          
            return mappedSchedules;
        }

        public List<ScheduleShort> GetSchedules()
        {
            var schedules = _scheduleRepository.GetSchedules();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Schedule, ScheduleShort>()));
            var mappedSchedules = mapper.Map<List<ScheduleShort>>(schedules);

            return mappedSchedules;
        }

        public ScheduleShort GetSchedule(int id)
        {
            var schedules = _scheduleRepository.GetSchedule(id);

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Schedule, ScheduleShort>()));
            var mappedSchedule = mapper.Map<ScheduleShort>(schedules);

            return mappedSchedule;
        }
    }
}
