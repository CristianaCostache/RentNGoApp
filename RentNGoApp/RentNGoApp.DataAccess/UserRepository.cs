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
        public UserRepository(RentNGoAppContext rentNGoAppContext) : base(rentNGoAppContext)
        {
        }

    }
}
