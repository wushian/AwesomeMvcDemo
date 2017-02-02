namespace AwesomeMvcDemo.ViewModels.Input
{
    public class AutocompleteDemoInput
    {
        public string Meal { get; set; }

        public string ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }

        public int PrimeNumber { get; set; }

        public string ChildMeal { get; set; }

        public int[] CategoriesData { get; set; }

        public int? CategoryData { get; set; }

        public string ChildOfManyMeal { get; set; }

        public string Meal1 { get; set; }
        public string Meal2 { get; set; }
    }
}