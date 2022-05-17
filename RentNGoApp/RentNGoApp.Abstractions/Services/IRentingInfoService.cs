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
    public interface IRentingInfoService
    {
        void Rent(int id, ClaimsPrincipal user);
        RentingInfo GetOngoingRentingInfoByCarId(int id);
        void UnrentCar(int id);
        List<RentingInfo> GetOngoingRentingInfosByUser(ClaimsPrincipal user);
    }
}
