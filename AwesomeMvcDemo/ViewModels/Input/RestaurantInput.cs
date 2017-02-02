using System.ComponentModel.DataAnnotations;

namespace AwesomeMvcDemo.ViewModels.Input
{
    public class RestaurantInput
    {
        [UIHint("Hidden")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}