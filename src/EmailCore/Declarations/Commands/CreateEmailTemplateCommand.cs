﻿using BaseCore.Models.Responses;
using EmailCore.Models.Entities;
using MediatR;

namespace EmailCore.Declarations.Commands
{
    public class CreateEmailTemplateCommand : IRequest<ServiceResponse>
    {
        public string Name { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public EmailBodyContentType EmailBodyContentType { get; set; } = EmailBodyContentType.Text;

        public string[] Tags { get; set; } = new string[] { };
    }

}
