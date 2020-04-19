using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class Client
    {
        public Client()
        {
            ClientClaims = new HashSet<ClientClaim>();
            ClientCorsOrigins = new HashSet<ClientCorsOrigin>();
            ClientGrantTypes = new HashSet<ClientGrantType>();
            ClientIdPrestrictions = new HashSet<ClientIdPrestriction>();
            ClientPostLogoutRedirectUris = new HashSet<ClientPostLogoutRedirectUri>();
            ClientProperties = new HashSet<ClientProperty>();
            ClientRedirectUris = new HashSet<ClientRedirectUri>();
            ClientScopes = new HashSet<ClientScope>();
            ClientSecrets = new HashSet<ClientSecret>();
        }

        [Key]
        public int Id { get; set; }
        public bool Enabled { get; set; }
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        [Required]
        [StringLength(200)]
        public string ProtocolType { get; set; }
        public bool RequireClientSecret { get; set; }
        [StringLength(200)]
        public string ClientName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(2000)]
        public string ClientUri { get; set; }
        [StringLength(2000)]
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        [StringLength(2000)]
        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; }
        [StringLength(2000)]
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public int IdentityTokenLifetime { get; set; }
        [StringLength(100)]
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int? ConsentLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public int RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int AccessTokenType { get; set; }
        public bool EnableLocalLogin { get; set; }
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        [StringLength(200)]
        public string ClientClaimsPrefix { get; set; }
        [StringLength(200)]
        public string PairWiseSubjectSalt { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        public int? UserSsoLifetime { get; set; }
        [StringLength(100)]
        public string UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; }
        public bool NonEditable { get; set; }

        [InverseProperty(nameof(ClientClaim.Client))]
        public virtual ICollection<ClientClaim> ClientClaims { get; set; }
        [InverseProperty(nameof(ClientCorsOrigin.Client))]
        public virtual ICollection<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        [InverseProperty(nameof(ClientGrantType.Client))]
        public virtual ICollection<ClientGrantType> ClientGrantTypes { get; set; }
        [InverseProperty(nameof(ClientIdPrestriction.Client))]
        public virtual ICollection<ClientIdPrestriction> ClientIdPrestrictions { get; set; }
        [InverseProperty(nameof(ClientPostLogoutRedirectUri.Client))]
        public virtual ICollection<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        [InverseProperty(nameof(ClientProperty.Client))]
        public virtual ICollection<ClientProperty> ClientProperties { get; set; }
        [InverseProperty(nameof(ClientRedirectUri.Client))]
        public virtual ICollection<ClientRedirectUri> ClientRedirectUris { get; set; }
        [InverseProperty(nameof(ClientScope.Client))]
        public virtual ICollection<ClientScope> ClientScopes { get; set; }
        [InverseProperty(nameof(ClientSecret.Client))]
        public virtual ICollection<ClientSecret> ClientSecrets { get; set; }
    }
}
