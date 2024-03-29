﻿using Newtonsoft.Json;

namespace BrivoAPI.Models
{
    public class LoginResponse
    {

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string SCOPE { get; set; }

        [JsonProperty(PropertyName = "jti")]
        public string JTI { get; set; }
    }
}
