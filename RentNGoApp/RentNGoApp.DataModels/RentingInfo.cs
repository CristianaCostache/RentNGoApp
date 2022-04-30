using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataModels
{
    public class RentingInfo
    {
        public int rentingInfoId { get; set; }
        public User user { get; set; }
        public Car car { get; set; }
        public DateTime rentingDate { get; set; }
        public string status { get; set; }

    }
}
