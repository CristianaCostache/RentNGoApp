using Microsoft.AspNetCore.Identity;
using RentNGoApp.Abstractions.Repositories;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataAccess
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
		private readonly RentNGoAppContext _rentNGoAppContext;

		public UserRepository(RentNGoAppContext rentNGoAppContext) : base(rentNGoAppContext)
        {
			_rentNGoAppContext = rentNGoAppContext;
		}

		public List<IdentityUser> GetAllUsers()
		{
			return _rentNGoAppContext.Users.ToList();
		}
	}
}
