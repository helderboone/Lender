using Lender.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lender.API.Data.Mappings
{
    public class FriendMapping : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Url)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.PublicId)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.CreationTime)
                .IsRequired()
                .HasColumnType("timestamp");

            // 1 : 1 => Friend : Address
            builder.HasOne(f => f.Address)
                .WithOne(a => a.Friend)
                .HasForeignKey<Address>(b => b.FriendId);

            // 1 : N => User : Friends
            builder.HasOne(f => f.User)
                .WithMany(u => u.Friends);

            builder.ToTable("Friends");
        }
    }
}
