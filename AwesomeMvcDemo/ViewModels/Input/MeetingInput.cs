using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AwesomeMvcDemo.ViewModels.Input
{
    public class MeetingInput
    {
        public int Id { get; set; }

        [DisplayName("All day")]
        public bool AllDay { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime? Start { get; set; }

        [DisplayName("Start time")]
        [UIHint("TimePickerm")]
        public DateTime StartTime { get; set; }
       
        [Required]
        public DateTime? End { get; set; }

        [DisplayName("End time")]
        [UIHint("TimePickerm")]
        public DateTime EndTime { get; set; }

        public string Location { get; set; }

        [UIHint("ColorDropdown")]
        public string Color { get; set; }

        [UIHint("Textarea")]
        public string Notes { get; set; }
    }
}