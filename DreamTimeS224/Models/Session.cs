using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamTimeS224.Models
{
    [PrimaryKey(nameof(Date), nameof(SessionTypeId))]
    public class Session
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Session Type")]
        public int SessionTypeId { get; set; }

        [Required]
        [DisplayName("Start Time")]
        public DateTime StartTimeId { get; set; }

        [Required]
        [DisplayName("End Time")]
        public DateTime EndTimeId { get; set; }

        public StatusEnum Status { get; set; }

        public enum StatusEnum
        {
            Available = 1,
            Unavailable = 0,
        }


        // Associations (Navigation Properties)

        public SessionType? SessionType { get; set; } = null;

        public Timeslot? StartTime { get; set; } = null;
        
        public Timeslot? EndTime { get; set; } = null;
    }
}
