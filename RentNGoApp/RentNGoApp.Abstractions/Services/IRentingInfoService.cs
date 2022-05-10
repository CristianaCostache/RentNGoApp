﻿using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.Abstractions.Services
{
    public interface IRentingInfoService
    {
        void Rent(int id);
        List<RentingInfo> GetRentingInfosByUserId(int userId);
    }
}
