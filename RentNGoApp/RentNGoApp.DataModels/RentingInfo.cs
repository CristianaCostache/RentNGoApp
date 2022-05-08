using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataModels
{
    public class RentingInfo
    {
        public const string STATUS_ONGOING = "ongoing";
        public const string STATUS_EXPIRED = "expired";
        public int rentingInfoId { get; set; }
        public User user { get; set; }
        public Car car { get; set; }
        public DateTime rentingDate { get; set; } = DateTime.Now;
        public string status { get; set; } = STATUS_ONGOING;

    }
}
