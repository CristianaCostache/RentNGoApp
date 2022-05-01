using RentNGoApp.DataModels;

namespace RentNGoApp.ViewModels
{
    public class Feed
    {
        public Filter filter { get; set; }
        public ICollection<Car> cars { get; set; }
    }
}
