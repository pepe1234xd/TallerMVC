using System.ComponentModel.DataAnnotations;

namespace TallerMVC.Data.Validaciones
{
    public class NoFinDeSemanaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateOnly fecha && (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday))
            {
                return new ValidationResult("La fecha no puede caer en sábado o domingo.");
            }

            return ValidationResult.Success;
        }
    }
}
