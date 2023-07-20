using HumHum.Models;

namespace HumHum.ViewModels
{
    public class RestaurantFoodViewModel
    {
        public Restaurant restaurant { get; set; }
        public List<FoodItem> foodItems { get; set; }
    }
}
