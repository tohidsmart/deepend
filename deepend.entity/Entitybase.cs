using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace deepend.entity
{
    public class EntityBase
    {
        public List<ValidationResult> ValidationResults { get; }

        public string ErrorMessages
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                ValidationResults.ForEach(v => sb.AppendLine(v.ErrorMessage));

                return sb.ToString();
            }
        }

        public EntityBase()
        {
            ValidationResults = new List<ValidationResult>();
        }

        public bool Validate()
        {
            var context = new ValidationContext(this, null, null);

            return Validator.TryValidateObject(this, context, ValidationResults, true);
        }
    }
}
