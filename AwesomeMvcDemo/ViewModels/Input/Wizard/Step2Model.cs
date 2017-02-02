using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.ViewModels.Input.Wizard
{
    public class Step2Model
    {
        public string WizardId { get; set; }

        [UIHint("Hidden")]
        public int? CategoryId { get; set; }

        [Required]
        [AwesomeParents("{ categories: 'CategoryId' }")]
        [AweUrl(Action = "GetMeals", Controller = "Data")]
        [DisplayName("Meals")]
        [UIHint("AjaxCheckboxList")]
        public int[] MealIds { get; set; }
    }
}