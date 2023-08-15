﻿using BaseModule.Application.DTOs.Responses;
using BaseModule.Application.Providers.Interfaces;
using EmailModule.Application.Commands;
using EmailModule.Application.Queries;
using EmailModule.Domain.Entities;
using EmailModule.Domain.Repositories.Interfaces;
using IdentityModule.Application.Providers.Interfaces;
using MongoDB.Driver;


namespace EmailModule.Infrastructure.Persistence
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        #region Fields

        private readonly IMongoDbContextProvider _mongoDbContextProvider;
        private readonly IAuthenticationContextProvider _authenticationContextProvider;

        #endregion

        #region Ctor

        public EmailTemplateRepository(IMongoDbContextProvider mongoDbService, IAuthenticationContextProvider authenticationContext)
        {
            _mongoDbContextProvider = mongoDbService;
            _authenticationContextProvider = authenticationContext;
        }


        #endregion

        #region Methods

        public async Task<ServiceResponse> CreateEmailTemplate(EmailTemplate emailTemplate)
        {
            var authCtx = _authenticationContextProvider.GetAuthenticationContext();

            await _mongoDbContextProvider.InsertDocument(emailTemplate);

            return Response.BuildServiceResponse().BuildSuccessResponse(emailTemplate, authCtx?.RequestUri);
        }

        public async Task<QueryRecordResponse<EmailTemplate>> GetEmailTemplate(string templateId)
        {
            var authCtx = _authenticationContextProvider.GetAuthenticationContext();

            var filter = Builders<EmailTemplate>.Filter.Eq(x => x.Id, templateId);

            var emailTemplate = await _mongoDbContextProvider.FindOne(filter);

            return Response.BuildQueryRecordResponse<EmailTemplate>().BuildSuccessResponse(emailTemplate, authCtx?.RequestUri);
        }

        public async Task<ServiceResponse> UpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            var authCtx = _authenticationContextProvider.GetAuthenticationContext();

            var update = Builders<EmailTemplate>.Update
                .Set(x => x.Name, emailTemplate.Name)
                .Set(x => x.Body, emailTemplate.Body)
                .Set(x => x.EmailBodyContentType, emailTemplate.EmailBodyContentType)
                .Set(x => x.Purpose, emailTemplate.Purpose)
                .Set(x => x.Tags, emailTemplate.Tags)
                .Set(x => x.TimeStamp.ModifiedOn, DateTime.UtcNow)
                .Set(x => x.TimeStamp.ModifiedBy, authCtx.User?.Id);

            await _mongoDbContextProvider.UpdateById(update: update, id: emailTemplate.Id);

            var updatedTemplate = await _mongoDbContextProvider.FindById<EmailTemplate>(emailTemplate.Id);

            return Response.BuildServiceResponse().BuildSuccessResponse(updatedTemplate, authCtx?.RequestUri);
        }

        public async Task<bool> BeAnExistingEmailTemplate(string templateName)
        {
            var filter = Builders<EmailTemplate>.Filter.Where(x => x.Name.ToLower().Equals(templateName.ToLower()));
            return await _mongoDbContextProvider.Exists(filter);
        }

        public async Task<bool> BeAnExistingEmailTemplateById(string templateId)
        {
            var filter = Builders<EmailTemplate>.Filter.Eq(x => x.Id, templateId);
            return await _mongoDbContextProvider.Exists(filter);
        }

        public async Task<EmailTemplate> GetEmailTemplateById(string templateId)
        {
            var authCtx = _authenticationContextProvider.GetAuthenticationContext();

            var filter = Builders<EmailTemplate>.Filter.Eq(x => x.Id, templateId);

            var emailTemplate = await _mongoDbContextProvider.FindOne(filter);

            return emailTemplate;
        }

        #endregion
    }
}