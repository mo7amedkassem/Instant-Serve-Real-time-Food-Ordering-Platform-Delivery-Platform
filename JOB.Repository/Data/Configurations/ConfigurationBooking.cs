using Job.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository.Data.Configurations
{
    public class ConfigurationBooking : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(O => O.User)
                   .WithMany(u => u.userOrders)
                   .HasForeignKey(O => O.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(O => O.product)
                     .WithMany(P => P.orders)
                     .HasForeignKey(O => O.ProductId)
                     .OnDelete(DeleteBehavior.NoAction);

            builder.Property(O => O.Status)
                .IsRequired();
            builder.Property(O => O.BookingDate)
                .IsRequired();

        }
    }
}
