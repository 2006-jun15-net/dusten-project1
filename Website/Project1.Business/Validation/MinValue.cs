using System.ComponentModel.DataAnnotations;
using System;

namespace Project1.Business.Validation {

    public class MinValue : ValidationAttribute {

        private int mMinValue;

        public MinValue (int minValue) {
            mMinValue = minValue;
        }

        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {

            int numValue = Convert.ToInt32 (value);

            if (numValue < mMinValue) {
                return new ValidationResult ($"Can't be less than {mMinValue}");
            }

            return ValidationResult.Success;
        }
    }
}