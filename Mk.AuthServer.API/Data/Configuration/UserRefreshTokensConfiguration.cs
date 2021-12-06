using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mk.AuthServer.Core.models;

namespace Mk.AuthServer.API.Data.Configuration
{
    public class UserRefreshTokensConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x => x.UserId);
        }
    }
}