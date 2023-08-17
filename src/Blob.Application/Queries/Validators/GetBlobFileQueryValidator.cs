﻿using Base.Application.Extensions;
using Blob.Domain.Repositories.Interfaces;
using FluentValidation;

namespace Blob.Application.Queries.Validators
{
    public class GetBlobFileQueryValidator : AbstractValidator<GetBlobFileQuery>
    {
        private readonly IBlobFileRepository _blobFileRepository;

        public GetBlobFileQueryValidator(IBlobFileRepository blobFileRepository)
        {
            _blobFileRepository = blobFileRepository;

            RuleFor(x => x.FileId).NotNull().NotEmpty();
            RuleFor(x => x.FileId).MustAsync(BeAnExistingBlobFile).WithMessage("File doesn't exist.").When(x => !x.FileId.IsNullOrBlank());
        }

        private async Task<bool> BeAnExistingBlobFile(string fileId, CancellationToken token)
        {
            return await _blobFileRepository.BeAnExistingBlobFile(fileId);
        }
    }
}
