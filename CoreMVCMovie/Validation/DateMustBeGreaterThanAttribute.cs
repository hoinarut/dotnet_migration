using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CoreMVCMovie.Validation
{
    public class DateMustBeGreaterThanAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly DateTime _firstDate;
        private readonly string _invalidDateMessage = "{0} must be a valid date.";

        public DateMustBeGreaterThanAttribute(string firstDate)
        : this(firstDate, "{0} must be greather than {1:d}")
        {

        }

        public DateMustBeGreaterThanAttribute(string firstDate, string errorMessage)
        : base(errorMessage)
        {
            _firstDate = DateTime.Parse(firstDate);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _firstDate);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var displayName = validationContext.DisplayName;
            if (!DateTime.TryParse(value.ToString(), out DateTime result))
            {
                return new ValidationResult(string.Format(_invalidDateMessage, displayName));
            }
            if (result > _firstDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(displayName));
            }
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            string propertyDisplayName = context.ModelMetadata.DisplayName ?? context.ModelMetadata.PropertyName;
            string errorMessage = FormatErrorMessage(propertyDisplayName);
            context.Attributes.Add("data-val-mustbegreaterthan", errorMessage);
            context.Attributes.Add("data-val-mustbegreaterthan-minimumdate", _firstDate.ToString("d"));
        }
    }
}