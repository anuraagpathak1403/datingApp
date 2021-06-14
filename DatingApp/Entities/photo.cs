using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Entities
{
    [Table("photos")]
    public class photo
    {
        public int id { get; set; }
        public string url { get; set; }
        public bool isMain { get; set; }
        public string publicId { get; set; }
        public appUser appUser { get; set; }
        public int appUserId { get; set; }
    }
}