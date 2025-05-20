using System.ComponentModel.DataAnnotations;

namespace Society_Management_System.Models
{
    public class Resident
    {
        [Key]
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string HouseNumber {  get; set; }
        public string PhoneNumber {  get; set; }
        public string Street {  get; set; }

       
    }
}
