using System.Collections.Generic;

namespace AwesomeMvcDemo.Models
{
    public class SiteMapItem
    {
        private static int newId = 1;

        private SiteMapItem parent;

        public SiteMapItem()
        {
            Id = newId;
            newId++;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public bool Collapsed { get; set; }

        public string Url { get; set; }

        public string Anchor { get; set; }

        public string Keywords { get; set; }

        public SiteMapItem Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
                if (parent.Children == null) parent.Children = new List<SiteMapItem>();
                parent.Children.Add(this);
                Keywords = parent.Name + " " + parent.Keywords + " " + Keywords;
            }
        }

        public IList<SiteMapItem> Children { get; set; }
    }
}