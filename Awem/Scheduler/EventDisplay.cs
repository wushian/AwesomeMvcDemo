namespace Omu.Awem.Scheduler
{
    /// <summary>
    /// Scheduler Event display dto
    /// </summary>
    public class EventDisplay
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
        /// event time
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// event color
        /// </summary>
        public string Color { get; set; }
    }
}