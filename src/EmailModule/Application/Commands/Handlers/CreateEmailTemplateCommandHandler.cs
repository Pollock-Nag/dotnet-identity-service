﻿using BaseModule.Application.DTOs.Responses;
using BaseModule.Infrastructure.Extensions;
using EmailModule.Application.Commands.Validators;
using EmailModule.Domain.Repositories.Interfaces;
using IdentityModule.Application.Providers.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace EmailModule.Application.Commands.Handlers
{
    public class CreateEmailTemplateCommandHandler : IRequestHandler<CreateEmailTemplateCommand, ServiceResponse>
    {
        #region Fields

        private readonly ILogger<CreateEmailTemplateCommandHandler> _logger;
        private readonly CreateEmailTemplateCommandValidator _validator;
        private readonly IEmailTemplateRepository _emailRepository;
        private readonly IAuthenticationContextProvider _authenticationContextProvider;

        #endregion

        #region Ctor

        public CreateEmailTemplateCommandHandler(
            ILogger<CreateEmailTemplateCommandHandler> logger,
            CreateEmailTemplateCommandValidator validator,
            IEmailTemplateRepository emailRepository,
            IAuthenticationContextProvider authenticationContextProvider)
        {
            _logger = logger;
            _validator = validator;
            _emailRepository = emailRepository;
            _authenticationContextProvider = authenticationContextProvider;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse> Handle(CreateEmailTemplateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                validationResult.EnsureValidResult();

                var authCtx = _authenticationContextProvider.GetAuthenticationContext();
                var emailTemplate = CreateEmailTemplateCommand.Initialize(command, authCtx);

                return await _emailRepository.CreateEmailTemplate(emailTemplate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Response.BuildServiceResponse().BuildErrorResponse(ex.Message, _authenticationContextProvider.GetAuthenticationContext()?.RequestUri);
            }
        }

        #endregion
    }
}