using System.ComponentModel.DataAnnotations;

namespace EFcrud2
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
    }
}
