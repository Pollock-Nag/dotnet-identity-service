﻿namespace EmailCore.Declarations.Commands
{
    public class UpdateEmailTemplateCommand : CreateEmailTemplateCommand
    {
        public string TemplateId { get; set; } = string.Empty;
    }
}
