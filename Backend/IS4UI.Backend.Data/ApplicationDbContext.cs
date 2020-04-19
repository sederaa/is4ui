using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using IS4UI.Backend.Data.Entities;

namespace IS4UI.Backend.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApiClaim> ApiClaims { get; set; }
        public virtual DbSet<ApiProperty> ApiProperties { get; set; }
        public virtual DbSet<ApiResource> ApiResources { get; set; }
        public virtual DbSet<ApiScope> ApiScopes { get; set; }
        public virtual DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }
        public virtual DbSet<ApiSecret> ApiSecrets { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientClaim> ClientClaims { get; set; }
        public virtual DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public virtual DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        public virtual DbSet<ClientIdPrestriction> ClientIdPrestrictions { get; set; }
        public virtual DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        public virtual DbSet<ClientProperty> ClientProperties { get; set; }
        public virtual DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        public virtual DbSet<ClientScope> ClientScopes { get; set; }
        public virtual DbSet<ClientSecret> ClientSecrets { get; set; }
        public virtual DbSet<DeviceCode> DeviceCodes { get; set; }
        public virtual DbSet<IdentityClaim> IdentityClaims { get; set; }
        public virtual DbSet<IdentityProperty> IdentityProperties { get; set; }
        public virtual DbSet<IdentityResource> IdentityResources { get; set; }
        public virtual DbSet<PersistedGrant> PersistedGrants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiClaim>(entity =>
            {
                entity.HasIndex(e => e.ApiResourceId);
            });

            modelBuilder.Entity<ApiProperty>(entity =>
            {
                entity.HasIndex(e => e.ApiResourceId);
            });

            modelBuilder.Entity<ApiResource>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<ApiScope>(entity =>
            {
                entity.HasIndex(e => e.ApiResourceId);

                entity.HasIndex(e => e.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<ApiScopeClaim>(entity =>
            {
                entity.HasIndex(e => e.ApiScopeId);
            });

            modelBuilder.Entity<ApiSecret>(entity =>
            {
                entity.HasIndex(e => e.ApiResourceId);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.ClientId)
                    .IsUnique();
            });

            modelBuilder.Entity<ClientClaim>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<ClientCorsOrigin>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<ClientGrantType>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<ClientIdPrestriction>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<ClientPostLogoutRedirectUri>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<ClientProperty>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<ClientRedirectUri>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<ClientScope>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<ClientSecret>(entity =>
            {
                entity.HasIndex(e => e.ClientId);
            });

            modelBuilder.Entity<DeviceCode>(entity =>
            {
                entity.HasIndex(e => e.DeviceCode1)
                    .IsUnique();

                entity.HasIndex(e => e.Expiration);
            });

            modelBuilder.Entity<IdentityClaim>(entity =>
            {
                entity.HasIndex(e => e.IdentityResourceId);
            });

            modelBuilder.Entity<IdentityProperty>(entity =>
            {
                entity.HasIndex(e => e.IdentityResourceId);
            });

            modelBuilder.Entity<IdentityResource>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<PersistedGrant>(entity =>
            {
                entity.HasIndex(e => e.Expiration);

                entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
