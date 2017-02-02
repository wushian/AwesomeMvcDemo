namespace AwesomeMvcDemo.ViewModels.Input
{
    public class GridDemoGroupingCfgInput
    {
        public bool Groupable { get; set; }

        public bool ShowGroupedColumn { get; set; }

        public bool ShowGroupBar { get; set; }

        public int PageSize { get; set; }

        public bool PersonGrouped { get; set; }

        public bool PersonRem { get; set; }

        public bool PersonGroupable { get; set; }

        public int PersonRank { get; set; }

        public bool FoodGrouped { get; set; }

        public bool FoodRem { get; set; }

        public bool FoodGroupable { get; set; }

        public int FoodRank { get; set; }

        public bool Collapsed { get; set; }
    }
}