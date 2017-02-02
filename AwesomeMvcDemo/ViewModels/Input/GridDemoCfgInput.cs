namespace AwesomeMvcDemo.ViewModels.Input
{
    public class GridDemoCfgInput
    {
        public bool Sortable { get; set; }

        public bool Groupable { get; set; }

        public int Height { get; set; }

        public int PageSize { get; set; }

        public int MinHeight { get; set; }

        public bool SingleColumnSorting { get; set; }

        public bool ShowGroupedColumn { get; set; }

        public bool LoadOnParentChange { get; set; }

        public bool Resizable { get; set; }

        public bool Reorderable { get; set; }

        public bool UseRemoteData { get; set; }

        public string DataFunc { get; set; }

        public bool Page1OnSort { get; set; }
    }
}