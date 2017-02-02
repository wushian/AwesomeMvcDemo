using System;

namespace Omu.Awem.Scheduler
{
    /// <summary>
    /// Scheduler event dto
    /// </summary>
    public class SchedulerEvent
    {
        /// <summary>
        /// event id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// event title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// event start utc
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// event end utc
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// all day event
        /// </summary>
        public bool AllDay { get; set; }

        /// <summary>
        /// event notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// event color
        /// </summary>
        public string Color { get; set; }
    }
}