using System.ComponentModel.DataAnnotations;
namespace PvrWebApp.Model
{
    public class PvrUser
    {
        [Key]
        public int PvrUserId { get; set; }
        [StringLength(40)]
        public string? Name { get; set; }
        public long? Phone { get; set; }
        [StringLength(150)]
        public string? Address { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
