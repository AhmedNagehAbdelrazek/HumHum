using HumHum.Areas.Identity.Data;
using HumHum.Models;

namespace HumHum.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<Restaurant> restaurants { get; set; }
        public IEnumerable<FoodItem> foodItems { get; set; }
        public IEnumerable<ApplicationUser> applicationUsers { get; set; }
    }
}
