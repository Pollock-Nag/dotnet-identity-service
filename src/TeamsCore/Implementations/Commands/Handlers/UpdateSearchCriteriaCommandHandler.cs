﻿using BaseCore.Extensions;
using BaseCore.Models.Responses;
using BaseCore.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using TeamsCore.Declarations.Commands;
using TeamsCore.Declarations.Repositories;
using TeamsCore.Implementations.Commands.Validators;

namespace TeamsCore.Implementations.Commands.Handlers
{
    public class UpdateSearchCriteriaCommandHandler : IRequestHandler<UpdateSearchCriteriaCommand, ServiceResponse>
    {
        #region Fields

        private readonly ISearchCriteriaRepository _searchCriteriaRepository;
        private readonly UpdateSearchCriteriaCommandValidator _validator;
        private readonly IAuthenticationContextProvider _authenticationContextProvider;
        private readonly ILogger<UpdateSearchCriteriaCommandHandler> _logger;

        #endregion

        #region Ctor

        public UpdateSearchCriteriaCommandHandler(
            ISearchCriteriaRepository searchCriteriaRepository,
            UpdateSearchCriteriaCommandValidator validator,
            IAuthenticationContextProvider authenticationContextProvider,
            ILogger<UpdateSearchCriteriaCommandHandler> logger)
        {   
            _searchCriteriaRepository = searchCriteriaRepository;
            _validator = validator;
            _authenticationContextProvider = authenticationContextProvider;
            _logger = logger;
        }

        #endregion

        public async Task<ServiceResponse> Handle(UpdateSearchCriteriaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                validationResult.EnsureValidResult();

                return await _searchCriteriaRepository.UpdateSearchCriteria(request);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Response.BuildServiceResponse().BuildErrorResponse(ex.Message, _authenticationContextProvider.GetAuthenticationContext()?.RequestUri);
            }
        }
    }
}