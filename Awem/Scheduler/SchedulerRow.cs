using System.Collections.Generic;

namespace Omu.Awem.Scheduler
{
    /// <summary>
    /// Scheduler grid model item
    /// </summary>
    public class SchedulerRow
    {
        /// <summary>
        /// row time
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// css class for the row
        /// </summary>
        public string RowClass { get; set; }

        /// <summary>
        /// row date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// row day cells
        /// </summary>
        public IList<DayCell> Cells { get; set; }

        /// <summary>
        /// is an all day row
        /// </summary>
        public bool AllDay { get; set; }
        
        /// <summary>
        /// event title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// event notes
        /// </summary>
        public string Notes { get; set; }
    }
}