using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataModels
{
	public class Statistics
	{
		public ICollection<IdentityUser> users { get; set; }
		public ICollection<Car> cars { get; set; }
		public ICollection<RentingInfo> rentingInfos { get; set; }
	}
}
