﻿using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using RentNGoApp.Abstractions.Repositories;
using RentNGoApp.Abstractions.Services;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.AppLogic
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private UserManager<IdentityUser> _userManager;
        private ICarService _carService;
        private IRentingInfoService _rentingInfoService;

        public UserService(IRepositoryWrapper repositoryWrapper, UserManager<IdentityUser> userManager, ICarService carService, IRentingInfoService rentingInfoService)
        {
            _repositoryWrapper = repositoryWrapper;
            _userManager = userManager;
            _carService = carService;
            _rentingInfoService = rentingInfoService;
        }
        public void AddUser(User user)
        {
            _repositoryWrapper.userRepository.Create(user);
            _repositoryWrapper.Save();
        }

		public string GenerateStatistics()
		{
            List<IdentityUser> users = _repositoryWrapper.userRepository.GetAllUsers();
            List<Car> cars = _carService.GetAllCars();
            List<RentingInfo> rentingInfos = _rentingInfoService.GetAllRentingInfos();

            Statistics statistics = new Statistics();
            statistics.users = users;
            statistics.cars = cars;
            statistics.rentingInfos = rentingInfos;

            string json = JsonConvert.SerializeObject(statistics, Formatting.Indented);

            return json;
		}

		public ProfileViewModel GetUserData(ClaimsPrincipal user)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            
            string userGuid = _userManager.GetUserId(user);
            List<Car> cars = _carService.GetCarsByUserId(userGuid);
            profileViewModel.cars = cars;

            List<RentingInfo> rentingInfos = _rentingInfoService.GetOngoingRentingInfosByUser(userGuid);
            profileViewModel.rentingInfos = rentingInfos;

            return profileViewModel;
        }
    }
}
