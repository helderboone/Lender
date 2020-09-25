using Lender.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lender.API.Data.Mappings
{
    public class LoanMapping : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(l => new { l.FriendId, l.GameId });

            builder.Property(c => c.StartDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(c => c.EndDate)
                .HasColumnType("date");

            builder.HasOne(l => l.Friend)
                .WithMany(f => f.Loans)
                .HasForeignKey(l => l.FriendId);

            builder.HasOne(l => l.Game)
                .WithMany(g => g.Loans)
                .HasForeignKey(l => l.GameId);

            builder.ToTable("Loans");
        }
    }
}
