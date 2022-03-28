using Desafio.Umbler.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Umbler.Infrastructure.Data
{
    public class DomainHostMap : IEntityTypeConfiguration<DomainHost>
    {
        public void Configure(EntityTypeBuilder<DomainHost> builder)
        {
            builder.ToTable("DomainHost");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Ip)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.Property(x => x.WhoIs)
                .IsRequired();

            builder.Property(x => x.Ttl)
                .IsRequired();

            builder.Property(x => x.HostedAt)
                .IsRequired()
                .HasColumnType("varchar(200)");

        }
    }
}
