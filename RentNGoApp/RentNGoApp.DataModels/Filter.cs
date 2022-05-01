using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataModels
{
    public enum Sort
    {
        [Display(Name = "Choose...")] None, Ascending, Descending
    }
    public class Filter
    {
        public Sort sort { get; set; }
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
        public Brand brand { get; set; }
        public Color color { get; set; }
    }
}
