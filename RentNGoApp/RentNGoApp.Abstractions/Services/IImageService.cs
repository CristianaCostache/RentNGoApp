using Microsoft.AspNetCore.Http;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.Abstractions.Services
{
    public interface IImageService
    {
        ICollection<Image> AddImage(ICollection<IFormFile> imageFiles);
        ICollection<Image> GetAllImages();
        List<Image> GetImagesByCarId(int carId);
    }
}
