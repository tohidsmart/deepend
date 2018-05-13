using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace deepend.entity.Response
{
   
        public class ChequeResponse : EntityBase
        {
            [DataMember]
            public string PersonName { get; set; }
            [DataMember]
            public string ChequeAmountInLetter { get; set; }

            [DataMember]
            public decimal Amount { get; set; }

            [DataMember]
            public DateTime DateTime { get; set; } = DateTime.Now;

            [DataMember]
            public int Id { get; set; } = 1;
        }
    
}
