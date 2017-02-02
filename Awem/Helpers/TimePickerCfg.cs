namespace Omu.Awem.Helpers
{
    /// <summary>
    /// TimePicker mod configuration
    /// </summary>
    public class TimePickerCfg
    {
        private readonly TimePickerTag tag = new TimePickerTag();

        /// <summary>
        /// caption for when there's no value
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public TimePickerCfg Caption(string o)
        {
            tag.Caption = o;
            return this;
        }

        /// <summary>
        /// autoselect first item on load when no value is matched (change will be triggered)
        /// </summary>
        /// <returns></returns>
        public TimePickerCfg AutoSelectFirst()
        {
            tag.AutoSelectFirst = true;
            return this;
        }

        /// <summary>
        /// step in minutes
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public TimePickerCfg Step(int o)
        {
            tag.Step = o;
            return this;
        }

        internal TimePickerTag ToTag()
        {
            return tag;
        }
    }
}