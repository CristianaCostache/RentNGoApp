using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataModels
{
    public class FeedViewModel
    {
        public Car car { get; set; }
        public ICollection<Image> images { get; set; }
    }
}
