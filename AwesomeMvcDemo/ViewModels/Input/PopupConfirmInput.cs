using System.ComponentModel.DataAnnotations;

namespace AwesomeMvcDemo.ViewModels.Input
{
    public class PopupConfirmInput
    {
        [Required]
        public string Name { get; set; }
    }
}