using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RentNGoApp.Abstractions.Repositories;
using RentNGoApp.Abstractions.Services;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.AppLogic
{
    public class ImageService : IImageService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IWebHostEnvironment _webHostEnvironment;

        public ImageService(IRepositoryWrapper repositoryWrapper, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public ICollection<Image> AddImage(ICollection<IFormFile> imageFiles)
        {
            List<Image> images = new List<Image>();
            if (imageFiles.Count != 0)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                foreach (IFormFile imageFile in imageFiles)
                {
                    Image image = new Image();
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    image.name = fileName = fileName + DateTime.Now.ToString("_yyMMddHHmmss") + extension;
                    string path = Path.Combine(wwwRootPath + "/img/car/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }
                    _repositoryWrapper.imageRepository.Create(image);
                    images.Add(image);
                }
            }
            return images;
        }

        public ICollection<Image> GetAllImages()
        {
            var images = _repositoryWrapper.imageRepository.FindAll().ToList();
            return images;
        }

        public List<Image> GetImagesByCarId(int carId)
        {
            List<Image> images = _repositoryWrapper.imageRepository.FindByCondition(image => image.carId == carId).ToList();
            return images;
        }
    }
}
