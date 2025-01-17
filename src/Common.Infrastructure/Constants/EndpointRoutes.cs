﻿namespace Base.Shared.Constants
{
    public static class EndpointRoutes
    {
        public const string Action_GetUser = "api/Query/GetUser";
        public const string Action_GetUsers = "api/Query/GetUsers";
        public const string Action_CreateUser = "api/Command/CreateUser";
        public const string Action_SubmitUser = "api/Command/SubmitUser";
        public const string Action_UpdateUser = "api/Command/UpdateUser";
        public const string Action_UpdateUserPassword = "api/Command/UpdateUserPassword";
        public const string Action_UpdateUserRoles = "api/Command/UpdateUserRoles";

        public const string Action_AddRole = "api/Command/AddRole";
        public const string Action_UpdateRole = "api/Command/UpdateRole";

        public const string Action_ValidateToken = "api/Command/ValidateToken";
        public const string Action_AuthenticateToken = "api/Command/AuthenticateToken";

        public const string Action_GetEndPoints = "api/Query/GetEndpoints";

        public const string Action_GetRoles = "api/Query/GetRoles";
        public const string Action_GetUserRoles = "api/Query/GetUserRoles";

        public const string Action_GetClaims = "api/Query/GetClaims";
        public const string Action_AddClaimPermission = "api/Command/AddClaimPermission";

        public const string Action_UploadFile = "api/Command/UploadFile";
        public const string Action_DownloadFile = "api/Query/DownloadFile";
        public const string Action_GetFile = "api/Query/GetFile";

        public const string Action_CreateEmailTemplate = "api/Command/CreateEmailTemplate";
        public const string Action_GetEmailTemplate = "api/Query/GetEmailTemplate";
        public const string Action_GetEmailTemplateByPurpose = "api/Query/GetEmailTemplateByPurpose";
        public const string Action_UpdateEmailTemplate = "api/Command/UpdateEmailTemplate";

        public const string Action_EnqueueEmailMessage = "api/Command/EnqueueEmailMessage";

        public const string Action_AddLanguageApp = "api/Command/AddLanguageApp";
        public const string Action_AddLanguageResources = "api/Command/AddLanguageResources";
        public const string Action_GetLanguageApp = "api/Query/GetLanguageApp";
        public const string Action_GetLanguageResourcesInFormat = "api/Query/GetLanguageResourcesInFormat";

        public const string Action_SendUserAccountActivationRequest = "api/Command/SendUserAccountActivationRequest";
        public const string Action_VerifyUserAccountActivationRequest = "api/Command/VerifyUserAccountActivationRequest";

        public const string Action_AddProductSearchCriteria = "api/Command/AddProductSearchCriteria";
        public const string Action_UpdateProductSearchCriteria = "api/Command/UpdateProductSearchCriteria";
        public const string Action_GetProductSearchCriteria = "api/Query/GetProductSearchCriteria";
        public const string Action_GetProductSearchCriterias = "api/Query/GetProductSearchCriterias";
        public const string Action_GetProductSearchCriteriasForProductIdQuery = "api/Query/GetProductSearchCriteriasForProductId";

        public const string Action_AddProject = "api/Command/AddProject";
        public const string Action_UpdateProject = "api/Command/UpdateProject";
        public const string Action_GetProject = "api/Query/GetProject";        
        public const string Action_GetProjects = "api/Query/GetProjects";
        public const string Action_GetProjectsForProductId = "api/Query/GetProjectsForProductId";

        public const string Action_AddProduct = "api/Command/AddProduct";
        public const string Action_UpdateProduct = "api/Command/UpdateProduct";
        public const string Action_GetProduct = "api/Query/GetProduct";        
        public const string Action_GetProducts = "api/Query/GetProducts";
        public const string Action_GetProductRecommendations = "api/Query/GetProductRecommendations";

        public const string Action_AddQuotation = "api/Command/AddQuotation";
        public const string Action_UpdateQuotation = "api/Command/UpdateQuotation";
        public const string Action_GetQuotation = "api/Query/GetQuotation";
        public const string Action_GetQuotations = "api/Query/GetQuotations";
        public const string Action_GetQuotationStatusCounts = "api/Query/GetQuotationStatusCounts";

        public const string Action_OnboardUser = "api/Command/OnboardUser";
    }
}
