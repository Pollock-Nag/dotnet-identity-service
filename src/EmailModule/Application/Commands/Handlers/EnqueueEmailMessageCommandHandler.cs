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
    public class EnqueueEmailMessageCommandHandler : IRequestHandler<EnqueueEmailMessageCommand, ServiceResponse>
    {
        #region Fields

        private readonly ILogger<EnqueueEmailMessageCommandHandler> _logger;
        private readonly EnqueueEmailMessageCommandValidator _validator;
        private readonly IEmailMessageRepository _emailRepository;
        private readonly IAuthenticationContextProvider _authenticationContextProvider;

        #endregion

        #region Ctor

        public EnqueueEmailMessageCommandHandler(
            ILogger<EnqueueEmailMessageCommandHandler> logger,
            EnqueueEmailMessageCommandValidator validator,
            IEmailMessageRepository emailRepository,
            IAuthenticationContextProvider authenticationContextProvider)
        {
            _logger = logger;
            _validator = validator;
            _emailRepository = emailRepository;
            _authenticationContextProvider = authenticationContextProvider;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse> Handle(EnqueueEmailMessageCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                validationResult.EnsureValidResult();

                var authCtx = _authenticationContextProvider.GetAuthenticationContext();

                var emailMessage = EnqueueEmailMessageCommand.Initialize(command, authCtx);

                return await _emailRepository.EnqueueEmailMessage(emailMessage);
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