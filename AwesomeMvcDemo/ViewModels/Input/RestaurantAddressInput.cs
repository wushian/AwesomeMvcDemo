using System.ComponentModel.DataAnnotations;

namespace AwesomeMvcDemo.ViewModels.Input
{
    public class RestaurantAddressInput
    {
        public int Id { get; set; }

        [UIHint("Hidden")]
        public int RestaurantId { get; set; }

        [Required]
        public string Line1 { get; set; }

        public string Line2 { get; set; }
    }
}