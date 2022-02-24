﻿using FluentValidation;

namespace ContosoPizza.Features.Users.Update
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(p => p.User.Name)
                .NotEmpty();

            RuleFor(p => p.User.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
