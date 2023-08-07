﻿using BaseCore.Models.Responses;
using EmailCore.Declarations.Commands;
using EmailCore.Models.Entities;
using EmailCore.Declarations.Queries;

namespace EmailCore.Declarations.Repositories
{
    public interface IEmailTemplateRepository
    {
        Task<ServiceResponse> CreateEmailTemplate(CreateEmailTemplateCommand command);

        Task<QueryRecordResponse<EmailTemplate>> GetEmailTemplate(GetEmailTemplateQuery query);

        Task<ServiceResponse> UpdateEmailTemplate(UpdateEmailTemplateCommand command);

        Task<bool> BeAnExistingEmailTemplate(string templateName);

        Task<bool> BeAnExistingEmailTemplateById(string templateId);

        Task<EmailTemplate> GetEmailTemplate(string templateId);
    }
}
