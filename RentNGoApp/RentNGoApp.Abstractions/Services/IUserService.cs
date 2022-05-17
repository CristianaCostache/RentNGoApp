using Microsoft.AspNetCore.Identity;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.Abstractions.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        ProfileViewModel GetUserData(ClaimsPrincipal user);
		string GenerateStatistics();
	}
}
