using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LR5_1.Models
{
    public class Phone : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Довжина рядка повинна бути від 3 до 50 символів")]
        [Display(Name = "Марка")]
        public string Mark { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Довжина рядка повинна бути від 3 до 50 символів")]
        [Display(Name = "Модель")]

        public string Model { get; set; }
        [Required]
        [Display(Name = "Ціна")]
        public int Price { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Mark))
            {
                errors.Add(new ValidationResult("Введіть Марку"));
            }
            if (string.IsNullOrWhiteSpace(this.Model))
            {
                errors.Add(new ValidationResult("Введіть назву моделі"));
            }
            if (this.Price == null || this.Price < 200 || this.Price > 20000)
            {
                errors.Add(new ValidationResult("Некоректна ціна"));
            }

            return errors;
        }
    }
    public class PhonePropertyValidator : ModelValidator
    {
        public PhonePropertyValidator(ModelMetadata metadata, ControllerContext context)
            : base(metadata, context)
        { }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            Phone t = container as Phone;
            if (t != null)
            {
                switch (Metadata.PropertyName)
                {
                    case "Mark":
                        if (string.IsNullOrEmpty(t.Mark))
                        {
                            return new ModelValidationResult[]{
                            new ModelValidationResult { MemberName="Mark", Message="Введіть Mark"}
                        };
                        }
                        break;
                    case "Model":
                        if (string.IsNullOrEmpty(t.Model))
                        {
                            return new ModelValidationResult[]{
                            new ModelValidationResult { MemberName="Model", Message="Введіть Model"}
                        };
                        }
                        break;
                    case "Price":
                        if (t.Price < 200 || t.Price > 20000)
                        {
                            return new ModelValidationResult[]{
                            new ModelValidationResult { MemberName="Price", Message="Введіть коректну ціну"}
                        };
                        }
                        break;
                  
                }
            }
            return Enumerable.Empty<ModelValidationResult>();
        }
    }
    public class MyValidationProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata.ContainerType == typeof(Phone))
            {
                return new ModelValidator[] { new PhonePropertyValidator(metadata, context) };
            }

            if (metadata.ModelType == typeof(Phone))
            {
                return new ModelValidator[] { new TrainValidator(metadata, context) };
            }

            return Enumerable.Empty<ModelValidator>();
        }
    }

    public class TrainValidator : ModelValidator
    {
        public TrainValidator(ModelMetadata metadata, ControllerContext context)
            : base(metadata, context)
        { }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            Phone t = (Phone)Metadata.Model;

            List<ModelValidationResult> errors = new List<ModelValidationResult>();

            if (t.Model == "A" && t.Mark == "B" && t.Price == 1900)
            {
                errors.Add(new ModelValidationResult { MemberName = "", Message = "Неправильний телефон" });
            }
            return errors;
        }
    }
}