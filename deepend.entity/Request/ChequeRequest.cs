using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace deepend.entity.Request
{
   public class ChequeRequest:EntityBase
    {
        [DataMember]
        [Required]
        [Display(Name = "Person Name")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Name should contains letters only")]
        public string PersonName { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Cheque Amount")]
        [Range(typeof(decimal), "1.00", "123456789.00", ErrorMessage = "The amount of cheque is invalid or not in the accepted range ")]
        public decimal? ChequeAmount { get; set; }
    }
}
