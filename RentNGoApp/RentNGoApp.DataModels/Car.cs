﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataModels
{
    public enum Brand
    {
        [Display(Name = "Choose...")] None, Opel, Dacia, Audi, Tesla, BMW, Ford, Toyota, Volkswagen, MercedesBenz
    }
    public enum Color
    {
        [Display(Name = "Choose...")] None, Red, Blue, Green, White, Black, Yellow, Grey, Orange, Silver, Gold
    }
    public class Car
    {
        public int carId { get; set; }
        public string name { get; set; }
        public Brand brand { get; set; }
        public Color color { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public ICollection<Image> images { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
