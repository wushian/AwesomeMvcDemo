using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.ViewModels.Input
{
    public class GridDemoSortingCfgInput
    {
        public int PageSize { get; set; }

        public bool SingleColumnSort { get; set; }

        public bool FoodSortable { get; set; }

        public bool PersonSortable { get; set; }

        public int PersonRank { get; set; }

        public Sort PersonSort { get; set; }

        public int FoodRank { get; set; }

        public Sort FoodSort { get; set; }

        public bool Sortable { get; set; }
    }
}