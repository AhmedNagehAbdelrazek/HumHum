using HumHum.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HumHum.ViewModels
{
    public class FoodItemCreateViewModel
    {
        [Key]
        public long FoodItemId { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string FoodItemName { get; set; }
        [Required]
        public float Price { get; set; }
        public string DefaultImage { get; set; } = "Default_img.jpg";

        [Display(Name = "Food Image")]
        public IFormFile Image { get; set; }

        //------------------------------------------------
        public virtual Restaurant Restaurant { get; set; }
        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
        public List<Restaurant> restaurants { get; set; }
    }
}
