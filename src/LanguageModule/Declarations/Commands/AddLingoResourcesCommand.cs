﻿using BaseModule.Domain.DTOs.Responses;
using MediatR;

namespace LanguageModule.Declarations.Commands
{
    public class AddLingoResourcesCommand : IRequest<ServiceResponse>
    {
        public string AppId { get; set; } = string.Empty;

        public List<ResourceKeyEntry> ResourceKeys { get; set; } = new List<ResourceKeyEntry>();

        public class ResourceKeyEntry
        {
            public string ResourceKey { get; set; } = string.Empty;
            public List<CultureValue> CultureValues { get; set; } = new List<CultureValue>();
        }

        public class CultureValue
        {
            public string LanguageCode { get; set; } = string.Empty;
            public string ResourceValue { get; set; } = string.Empty;
        }
    }
}
