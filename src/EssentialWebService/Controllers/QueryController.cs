﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using IdentityCore.Models.Entities;
using IdentityCore.Declarations.Queries;
using BaseCore.Models.Responses;
using BaseCore.Attributes;
using BaseCommon;
using BlobCore.Declarations.Queries;
using BlobCore.Models.Entities;
using EmailCore.Declarations.Queries;
using EmailCore.Models.Entities;


namespace EssentialWebService.Controllers
{
    [ApiController]
    [AuthorizationRequired]    
    public class QueryController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public QueryController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        #region User

        [HttpGet(EndpointRoutes.Action_GetUser)]
        public async Task<QueryRecordResponse<UserResponse>> GetUser([FromQuery] GetUserQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet(EndpointRoutes.Action_GetUsers)]
        public async Task<QueryRecordsResponse<UserResponse>> GetUsers([FromQuery] GetUsersQuery query)
        {
            return await _mediator.Send(query);
        }

        #endregion

        #region Role

        [HttpGet(EndpointRoutes.Action_GetRoles)]
        public async Task<QueryRecordsResponse<Role>> GetRoles([FromQuery] GetRolesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet(EndpointRoutes.Action_GetUserRoles)]
        public async Task<QueryRecordsResponse<Role>> GetUserRoles([FromQuery] GetUserRolesQuery query)
        {
            return await _mediator.Send(query);
        }

        #endregion

        #region Claim

        [HttpGet(EndpointRoutes.Action_GetClaims)]
        public async Task<QueryRecordsResponse<ClaimPermission>> GetClaims([FromQuery] GetClaimsQuery query)
        {
            return await _mediator.Send(query);
        }

        #endregion

        #region Blob

        [HttpGet(EndpointRoutes.Action_DownloadFile)]
        public async Task<IActionResult> DownloadFile([FromQuery] DownloadBlobFileQuery query)
        {
            var blobFileResponse = await _mediator.Send(query);

            return File(blobFileResponse.Bytes, blobFileResponse.ContentType);
        }

        [HttpGet(EndpointRoutes.Action_GetFile)]
        public async Task<QueryRecordResponse<BlobFile>> GetBlobFiles([FromQuery] GetBlobFileQuery query)
        {
            return await _mediator.Send(query);
        }

        #endregion

        #region EmailTemplate

        [HttpGet(EndpointRoutes.Action_GetEmailTemplate)]
        public async Task<QueryRecordResponse<EmailTemplate>> GetEmailTemplate([FromQuery] GetEmailTemplateQuery query)
        {
            return await _mediator.Send(query);
        }

        #endregion

        #region Expired

        //[HttpGet(EndpointRoutes.Action_GetEndPoints)]
        //public async Task<QueryRecordsResponse<string>> GetEndPoints([FromQuery] GetEndPointsQuery query)
        //{
        //    return await _mediator.Send(query);
        //} 

        #endregion

        #endregion
    }
}