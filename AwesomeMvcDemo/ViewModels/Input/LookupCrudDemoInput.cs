using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.ViewModels.Input
{
    public class LookupCrudDemoInput
    {
        [Required]
        [UIHint("Lookup")]
        [Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public int? Dinner { get; set; }
    }
}