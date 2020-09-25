using Lender.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lender.API.Data.Mappings
{
    public class GameMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Gender)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.PhotoUrl)
               .HasColumnType("varchar(255)");

            builder.Property(c => c.PhotoPublicId)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.CreationTime)
                .IsRequired()
                .HasColumnType("timestamp");


            // 1 : N => User : Games
            builder.HasOne(f => f.User)
                .WithMany(u => u.Games);

            builder.ToTable("Games");
        }
    }
}
