namespace Omu.Awem.Helpers
{
    /// <summary>
    /// Multiselect config
    /// </summary>
    public class MultiselectCfg
    {
        private readonly MultiselectTag tag = new MultiselectTag
        {
            Reor = true
        };

        /// <summary>
        /// make selected items reorderable
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public MultiselectCfg Reorderable(bool o)
        {
            tag.Reor = o;
            return this;
        }

        /// <summary>
        /// caption when no item is selected
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public MultiselectCfg Caption(string o)
        {
            tag.Caption = o;
            return this;
        }

        /// <summary>
        /// don't close dropdown on item select
        /// </summary>
        /// <returns></returns>
        public MultiselectCfg NoSelectClose()
        {
            tag.NoSelClose = true;
            return this;
        }

        /// <summary>
        /// set css width of the dropdown 
        /// </summary>
        /// <param name="width">(px or em e.g. 12px)</param>
        /// <returns></returns>
        public MultiselectCfg MinWidth(string width)
        {
            tag.MinWidth = width;
            return this;
        }

        /// <summary>
        /// set min width in em
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public MultiselectCfg MinWidth(int width)
        {
            tag.MinWidth = width + "em";
            return this;
        }

        /// <summary>
        /// js func to be used for search
        /// </summary>
        /// <param name="func"></param>
        /// <param name="url"></param>
        /// <param name="key">cache key, id used by default</param>
        /// <returns></returns>
        public MultiselectCfg SearchFunc(string func, string url = null, string key = null)
        {
            tag.Key = key;
            tag.SrcFunc = func;
            tag.Url = url;
            return this;
        }

        /// <summary>
        /// js func used to render dropdown item
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public MultiselectCfg ItemFunc(string o)
        {
            tag.ItemFunc = o;
            return this;
        }

        /// <summary>
        /// js func used to render dropdown caption
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public MultiselectCfg CaptionFunc(string o)
        {
            tag.CaptionFunc = o;
            return this;
        }

        internal OdropdownTag ToTag()
        {
            return tag;
        }
    }
}