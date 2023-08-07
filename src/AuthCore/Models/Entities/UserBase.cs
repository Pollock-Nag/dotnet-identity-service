﻿using System.Text.Json.Serialization;

namespace BaseCore.Models.Entities
{
    public class UserBase : EntityBase
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ProfileImageUrl { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public Address Address { get; set; } = new Address();

        public UserSatus UserSatus { get; set; } = UserSatus.Active;

        public string[] MetaTags { get; set; } = new string[] { };

    }

    internal class User : UserBase
    {

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserSatus
    {
        Active = 0,
        Inactive = 1,
    }
}
