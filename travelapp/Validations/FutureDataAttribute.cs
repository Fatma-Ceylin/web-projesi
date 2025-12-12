using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date < DateTime.Today)
            {
                return new ValidationResult("Event date cannot be in the past");
            }
        }

        return ValidationResult.Success;
    }
}
