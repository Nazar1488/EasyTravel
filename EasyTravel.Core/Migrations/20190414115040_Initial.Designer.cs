﻿// <auto-generated />
using System;
using EasyTravel.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyTravel.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190414115040_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyTravel.Core.Models.GeoName", b =>
                {
                    b.Property<int>("GeoNameId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlternateNames");

                    b.Property<string>("AsciiName");

                    b.Property<string>("FeatureClass");

                    b.Property<string>("FeatureCode");

                    b.Property<double?>("Latitude");

                    b.Property<double?>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.HasKey("GeoNameId");

                    b.ToTable("GeoNames");
                });
#pragma warning restore 612, 618
        }
    }
}
