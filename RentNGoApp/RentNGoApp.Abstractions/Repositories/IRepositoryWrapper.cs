using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.Abstractions.Repositories
{
    public interface IRepositoryWrapper
    {
        ICarRepository carRepository { get; }
        IUserRepository userRepository { get; }
        IRentingInfoRepository rentingInfoRepository { get; }
        IImageRepository imageRepository { get; }
        void Save();
    }
}
