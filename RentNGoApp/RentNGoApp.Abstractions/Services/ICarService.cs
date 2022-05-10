﻿using Microsoft.AspNetCore.Http;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.Abstractions.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        void AddCar(Car car, ICollection<IFormFile> imageFiles);
        List<Car> GetCarsByFilter(Filter filter);
        Car GetCarById(int id);
        void Delete(int id);
        List<Car> GetCarsByUserId(int userId);
    }
}
