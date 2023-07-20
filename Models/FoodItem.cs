using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumHum.Models
{
    public class FoodItem
    {
        [Key]
        public long FoodItemId { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string FoodItemName { get; set; }
        [Required]
        public float Price { get; set; }
        public string? Image { get; set; }

        //------------------------------------------------
        public virtual Restaurant Restaurant { get; set; }
        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
    }
    static class defaultfooditems
    {
        public static string Defaultfooditem { get; set; } = "Default_img.jpg";
    }
}
