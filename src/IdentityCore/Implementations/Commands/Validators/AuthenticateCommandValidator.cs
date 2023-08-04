﻿using BaseCore.Extensions;
using FluentValidation;
using IdentityCore.Declarations.Commands;
using IdentityCore.Declarations.Repositories;

namespace IdentityCore.Implementations.Commands.Validators
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Email).Must(e => e.Contains('@')).WithMessage("Invalid email.").When(x => !x.Email.IsNullOrBlank());
            RuleFor(x => x.Email).MustAsync(BeAnExistingUserEmail).WithMessage("Email doesn't exist.").When(x => !x.Email.IsNullOrBlank());

            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x).MustAsync(BeValidUser).WithMessage("Invalid pasword.").When(x => !x.Password.IsNullOrBlank());
        }

        private async Task<bool> BeAnExistingUserEmail(string email, CancellationToken token)
        {
            return await _userRepository.BeAnExistingUserEmail(email);
        }

        private async Task<bool> BeValidUser(AuthenticateCommand command, CancellationToken arg2)
        {
            return await _userRepository.BeValidUser(userEmail: command.Email, password: command.Password);
        }
    }
}
