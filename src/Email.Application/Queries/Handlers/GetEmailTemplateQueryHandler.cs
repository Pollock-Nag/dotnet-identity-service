﻿using MediatR;
using Microsoft.Extensions.Logging;
using Email.Domain.Entities;
using Email.Application.Queries.Validators;
using Email.Domain.Repositories.Interfaces;
using Base.Application.DTOs.Responses;
using Identity.Application.Providers.Interfaces;
using Base.Application.Extensions;

namespace Email.Application.Queries.Handlers
{
    public class GetEmailTemplateQueryHandler : IRequestHandler<GetEmailTemplateQuery, QueryRecordResponse<EmailTemplate>>
    {
        private readonly IEmailTemplateRepository _emailRepository;
        private readonly IAuthenticationContextProvider _authenticationContextProvider;
        private readonly ILogger<GetEmailTemplateQueryHandler> _logger;
        private readonly GetEmailTemplateQueryValidator _validator;

        public GetEmailTemplateQueryHandler(
            ILogger<GetEmailTemplateQueryHandler> logger,
            GetEmailTemplateQueryValidator validator,
            IEmailTemplateRepository emailRepository,
            IAuthenticationContextProvider authenticationContext)
        {
            _logger = logger;
            _validator = validator;
            _emailRepository = emailRepository;
            _authenticationContextProvider = authenticationContext;
        }

        public async Task<QueryRecordResponse<EmailTemplate>> Handle(GetEmailTemplateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                validationResult.EnsureValidResult();

                var authCtx = _authenticationContextProvider.GetAuthenticationContext();

                var result =  await _emailRepository.GetEmailTemplate(request.TemplateId);

                return Response.BuildQueryRecordResponse<EmailTemplate>().BuildSuccessResponse(result, authCtx.RequestUri);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Response.BuildQueryRecordResponse<EmailTemplate>().BuildErrorResponse(Response.BuildErrorResponse().BuildExternalError(ex.Message, _authenticationContextProvider.GetAuthenticationContext().RequestUri));
            }
        }
    }
}
