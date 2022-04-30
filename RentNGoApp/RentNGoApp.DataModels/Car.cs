using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataModels
{
    public class Car
    {
        public int carId { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string color { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public ICollection<Image> images { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
