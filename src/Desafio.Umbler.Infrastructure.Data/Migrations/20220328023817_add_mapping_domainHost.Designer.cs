// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Desafio.Umbler.Infrastructure.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220328023817_add_mapping_domainHost")]
    partial class add_mapping_domainHost
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Desafio.Umbler.Domain.Domains.DomainHost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("HostedAt")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Ttl")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("WhoIs")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("DomainHost", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
