using HumHum.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HumHum.ViewModels
{
    public class EditFoodItem
    {
        [Key]
        public long FoodItemId { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string FoodItemName { get; set; }
        [Required]
        public float Price { get; set; }
        public string? Image { get; set; }
        public IFormFile Editedimg { get; set; }

        //------------------------------------------------
        public virtual Restaurant Restaurant { get; set; }
        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
    }
}
