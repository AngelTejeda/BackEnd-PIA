using System.ComponentModel.DataAnnotations;

namespace Core.ValidationAttributes
{
    // DataAnnotation. Verifica que un atributo sea un entero positivo.
    public class PositiveInteger : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int number = (int)validationContext.ObjectInstance;

            if (number <= 0)
                return new ValidationResult("Only positive integers are allowed.");

            return ValidationResult.Success;
        }
    }
}
