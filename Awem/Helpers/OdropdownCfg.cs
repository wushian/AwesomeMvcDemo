namespace Omu.Awem.Helpers
{
    /// <summary>
    /// Odropdown config
    /// </summary>
    public class OdropdownCfg
    {
        private readonly OdropdownTag tag = new OdropdownTag();

        /// <summary>
        /// css class to set for the dropdown popup
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public OdropdownCfg PopupClass(string o)
        {
            tag.Pc = o;
            return this;
        }

        /// <summary>
        /// label text in front of the caption/selected item text
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public OdropdownCfg InLabel(string o)
        {
            tag.InLabel = o;
            return this;
        }

        /// <summary>
        /// caption when no item is selected
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public OdropdownCfg Caption(string o)
        {
            tag.Caption = o;
            return this;
        }

        /// <summary>
        /// autoselect first item on load when no value is matched (change will be triggered)
        /// </summary>
        /// <returns></returns>
        public OdropdownCfg AutoSelectFirst()
        {
            tag.AutoSelectFirst = true;
            return this;
        }

        /// <summary>
        /// don't close dropdown on item select
        /// </summary>
        /// <returns></returns>
        public OdropdownCfg NoSelectClose()
        {
            tag.NoSelClose = true;
            return this;
        }

        /// <summary>
        /// set css width of the dropdown 
        /// </summary>
        /// <param name="width">(px or em e.g. 12px)</param>
        /// <returns></returns>
        public OdropdownCfg MinWidth(string width)
        {
            tag.MinWidth = width;
            return this;
        }

        /// <summary>
        /// set min width in em
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public OdropdownCfg MinWidth(int width)
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
        public OdropdownCfg SearchFunc(string func, string url = null, string key = null)
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
        public OdropdownCfg ItemFunc(string o)
        {
            tag.ItemFunc = o;
            return this;
        }

        /// <summary>
        /// js func used to render dropdown caption
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public OdropdownCfg CaptionFunc(string o)
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