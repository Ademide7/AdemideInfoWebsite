using AdemideInfoWebsite.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.AspNetCore;

namespace AdemideInfoWebsite.Application.Validations;

public class FluentValidations
{
    // using fluent validation for create profile record.
    public class CreateProfileDtoValidator : AbstractValidator<CreateProfileDto>
    {
        public CreateProfileDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(x => x.Language)
                .IsInEnum()
                .WithMessage("Invalid language selected.");
        }
    }

    public class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
    {
        public UpdateProfileDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);
        }
    }


    public class UpdateProfileLanguageDtoValidator : AbstractValidator<UpdateProfileLanguageDto>
    {
        public UpdateProfileLanguageDtoValidator()
        {
            RuleFor(x => x.Language)
                .IsInEnum()
                .WithMessage("Invalid language selected.");
        }
    }



}
