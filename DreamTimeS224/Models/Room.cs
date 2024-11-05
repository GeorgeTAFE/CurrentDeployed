using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamTimeS224.Models
{
    public class Room
    {
        [Key]
        [StringLength(5, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{1,2}\d{1,3}$", ErrorMessage = "Must follow the pattern XX000, e.g. M1, AB001")]
        public string Code { get; set; }

        [Required]
        [DisplayName("Room Type")]
        public int RoomTypeId { get; set; }

        // Associations (Navigation Properties)

        [DisplayName("Room Type")]
        public RoomType? RoomType { get; set; }

        // Constructors

        public Room()
        {
            Code = "";
        }

        public Room(string code, int roomTypeId)
        {
            Code = code;
            RoomTypeId = roomTypeId;
        }
    }
}
