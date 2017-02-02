namespace Omu.Awem.Helpers
{
    /// <summary>
    /// Popup config
    /// </summary>
    public class PopupCfg
    {
        private readonly PopupTag tag = new PopupTag
        {
            DropdownPopup = true,
            Dd = true
        };

        /// <summary>
        /// Toggle open
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public PopupCfg Toggle(bool o = true)
        {
            tag.Tg = o;
            return this;
        }

        /// <summary>
        /// Close popup when clicking outside of it
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public PopupCfg OutClickClose(bool o = true)
        {
            tag.Occ = o;
            return this;
        }

        /// <summary>
        /// dropdown popup from opener position
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public PopupCfg Dropdown(bool o = true)
        {
            tag.Dd = o;
            return this;
        }

        /// <summary>
        /// show header
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public PopupCfg ShowHeader(bool o = true)
        {
            tag.Sh = o;
            return this;
        }

        internal PopupTag ToTag()
        {
            return tag;
        }
    }
}