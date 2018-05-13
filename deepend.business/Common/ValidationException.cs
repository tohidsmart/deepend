using System;
using System.Collections.Generic;
using System.Text;

namespace deepend.business.Common
{
    public class ValidationException:Exception
    {
        public string EntityName { get; set; }

        public ValidationException(string message, string entityName) : base(message)
        {
            EntityName = entityName;
        }
    }
}
