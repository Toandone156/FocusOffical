using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FocusOffical.Models
{
    public class Order
    {
        public string Name { get; set; }
        [DisplayName("Address")]
        public string FullAddress { get; set; }
        public string? AddressNote { get; set; }
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        [RegularExpression("^(84|0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Phone Number is Invalid.")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? OrderNote { get; set; }
    }
}
