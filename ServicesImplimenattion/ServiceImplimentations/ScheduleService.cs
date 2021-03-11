﻿using AutoMapper;
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

            var masterId = _masterRepository.GetMasterByUserId(authUser.Id).Id;
            schedule.MasterId = masterId;
            schedule.Status = ScheduleStatus.READY;
            _scheduleRepository.CreateSchedule(schedule);
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

        public List<Schedule> GetSchedulesByMasterId(HttpContext httpContext)
        {
            var authUser = _authUserService.GetLoggedUserFull(httpContext);
            var master = _masterRepository.GetMasterByUserId(authUser.Id);
            List<Schedule> schedules = null;

            if (master != null)
            {
                schedules = _scheduleRepository.GetMastersSchedule(master.Id);
            }

            return schedules;
        }

        public List<Schedule> GetSchedules()
        {
            var schedules = _scheduleRepository.GetSchedules();

            return schedules;
        }

        public Schedule GetSchedule(int id)
        {
            var schedule = _scheduleRepository.GetSchedule(id);

            return schedule;
        }
    }
}