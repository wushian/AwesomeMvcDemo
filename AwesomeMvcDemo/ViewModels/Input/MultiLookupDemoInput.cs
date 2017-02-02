namespace AwesomeMvcDemo.ViewModels.Input
{
    public class MultiLookupDemoInput
    {
        public int[] MealsCustomItem { get; set; }
        public int[] MealsTableLayout { get; set; }

        public int[] CategoriesData { get; set; }

        public int? Category { get; set; }

        public int[] ChildOfMany { get; set; }

        public int[] Meals1 { get; set; }
        public int[] Meals2 { get; set; }
    }
}