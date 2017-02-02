using System.Collections.Generic;

namespace AwesomeMvcDemo.ViewModels.Input
{
    public class MultiLookupDemoCfgInput
    {
        public IEnumerable<int> Meals { get; set; }

        public bool ClearButton { get; set; }

        public bool HighlightChange { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public bool Fullscreen { get; set; }

        public bool DragAndDrop { get; set; }
    }
}