namespace Omu.Awem.Scheduler
{
    /// <summary>
    /// scheduler day cell
    /// </summary>
    public class DayCell
    {
        /// <summary>
        /// cell time representation start in ticks 
        /// </summary>
        public long Ticks { get; set; }
        
        /// <summary>
        /// cell events
        /// </summary>
        public EventDisplay[] Events { get; set; }

        /// <summary>
        /// cell day  ( for month view )
        /// </summary>
        public string Day { get; set; }
        
        /// <summary>
        /// cell date ( for month view ) 
        /// </summary>
        public string Date { get; set; }
    }
}