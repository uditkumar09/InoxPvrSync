using System.ComponentModel.DataAnnotations;
namespace InoxWebApp.Model
{
    public class InoxUser
    {
        [Key]
        public int InoxUserId { get; set; }
        [StringLength(40)]
        public string? Name { get; set; }
        public long? Phone { get; set; }
        [StringLength(150)]
        public string? Address { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
