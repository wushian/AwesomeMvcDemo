using System.ComponentModel.DataAnnotations;

namespace AwesomeMvcDemo.ViewModels.Input
{
    public class AjaxCheckboxListDemoInput
    {
        public int[] Categories { get; set; }

        public int[] ParentCategory { get; set; }

        public int[] ChildMeals { get; set; }

        public int[] CategoriesData { get; set; }

        public int? CategoryData { get; set; }

        public int[] ChildOfManyMeal { get; set; }

        [Required]
        public int[] Meals1 { get; set; }

        [Required]
        public int[] Meals2 { get; set; }
    }
}