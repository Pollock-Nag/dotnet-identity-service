﻿using BaseCore.Models.Responses;
using BaseCore.Attributes;
using IdentityCore.Declarations.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BaseCommon;
using BlobCore.Declarations.Commands;
using EmailCore.Declarations.Commands;

namespace EssentialWebService.Controllers
{
    [ApiController]
    [AuthorizationRequired]
    public class CommandController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public CommandController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        #region Token

        [AuthorizationNotRequired]
        [HttpPost(EndpointRoutes.Action_AuthenticateToken)]
        public async Task<ServiceResponse> AuthenticateToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext is null)
                return new ServiceResponse().BuildBadRequestResponse("Bad request");

            var command = new AuthenticateCommand()
            {
                Email = httpContext.Request.Form["Email"].ToString(),
                Password = httpContext.Request.Form["Password"].ToString(),
            };

            return await _mediator.Send(command);
        }

        [AuthorizationNotRequired]
        [HttpPost(EndpointRoutes.Action_ValidateToken)]
        public async Task<ServiceResponse> ValidateToken(ValidateTokenCommand command)
        {
            return await _mediator.Send(command);
        }

        #endregion

        #region User
        
        [HttpPost(EndpointRoutes.Action_CreateUser)]
        public async Task<ServiceResponse> CreateUser(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(EndpointRoutes.Action_UpdateUser)]
        public async Task<ServiceResponse> UpdateUser(UpdateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(EndpointRoutes.Action_UpdateUserPassword)]
        public async Task<ServiceResponse> UpdateUserPassword(UpdateUserPasswordCommand command)
        {
            return await _mediator.Send(command);
        }

        #endregion

        #region Role

        [HttpPost(EndpointRoutes.Action_AddRole)]
        public async Task<ServiceResponse> AddRole(AddRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(EndpointRoutes.Action_UpdateRole)]
        public async Task<ServiceResponse> UpdateRole(UpdateRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(EndpointRoutes.Action_UpdateUserRoles)]
        public async Task<ServiceResponse> UpdateUserRoles(UpdateUserRolesCommand command)
        {
            return await _mediator.Send(command);
        }

        #endregion

        #region Claim

        [HttpPost(EndpointRoutes.Action_AddClaimPermission)]
        public async Task<ServiceResponse> AddClaimPermission(AddClaimPermissionCommand command)
        {
            return await _mediator.Send(command);
        }

        #endregion

        #region Blob

        [HttpPost(EndpointRoutes.Action_UploadFile)]
        public async Task<ServiceResponse> UploadFile(IFormFile file)
        {
            return await _mediator.Send(new UploadBlobFileCommand() { FormFile = file });
        }

        #endregion

        #region EmailTemplate

        [HttpPost(EndpointRoutes.Action_CreateEmailTemplate)]
        public async Task<ServiceResponse> CreateEmailTemplate(CreateEmailTemplateCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut(EndpointRoutes.Action_UpdateEmailTemplate)]
        public async Task<ServiceResponse> UpdateEmailTemplate(UpdateEmailTemplateCommand command)
        {
            return await _mediator.Send(command);
        }

        #endregion

        #region EmailMessage

        [HttpPost(EndpointRoutes.Action_EnqueueEmailMessage)]
        public async Task<ServiceResponse> EnqueueEmailMessage(EnqueueEmailMessageCommand command)
        {
            return await _mediator.Send(command);
        }

        #endregion

        //TODO: forget password - > send email address and then send a link to reset password

        //TODO: activate user - > send user id

        #endregion
    }
}