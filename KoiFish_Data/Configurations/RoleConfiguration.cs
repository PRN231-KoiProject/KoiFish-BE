using KoiFish_Core.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(
                new AppRole
                {
                    Id = Guid.Parse("C0278115-8549-4FAD-890A-44F8E8FCC011"),
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    DisplayName = "Khách Hàng"
                });
        }
    }
}
