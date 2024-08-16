using System.ComponentModel.DataAnnotations;

namespace PvrWebApp.Model
{
    public class AddPvrUser
    {
        [StringLength(40)]
        public string? Name { get; set; }
        public long? Phone { get; set; }
        [StringLength(150)]
        public string? Address { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
