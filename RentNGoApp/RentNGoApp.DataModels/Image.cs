using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataModels
{
    public class Image
    {
        public int imageId { get; set; }
        public string name { get; set; }
        public int carId { get; set; }

        [NotMapped]
        public ICollection<IFormFile> imageFiles { get; set; }
    }
}
