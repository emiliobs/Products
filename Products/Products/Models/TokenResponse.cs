namespace Products.Models
{
    using Newtonsoft.Json;
    using SQLite;
    using SQLite.Net.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class TokenResponse
    {
        #region Properties
        [PrimaryKey, AutoIncrement]
        public int TokenResponseId { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty(".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        public bool IsRemembered { get; set; }

        #endregion

        #region Meethods

        public override int GetHashCode()
        {
            return TokenResponseId;
        }

        #endregion

    }



}
