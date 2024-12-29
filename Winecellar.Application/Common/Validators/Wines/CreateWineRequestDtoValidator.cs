using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Winecellar.Application.Dtos.Wines;

namespace Winecellar.Application.Common.Validators.Wines
{
    public class CreateWineRequestDtoValidator : AbstractValidator<CreateWineRequestDto>
    {
        public CreateWineRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
}