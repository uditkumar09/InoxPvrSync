using System.ComponentModel.DataAnnotations;

namespace InoxWebApp.Model
{
    public class AddInoxUser
    {
        [StringLength(40)]
        public string? Name { get; set; }
        public long? Phone { get; set; }
        [StringLength(150)]
        public string? Address { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
