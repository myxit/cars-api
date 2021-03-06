﻿// <auto-generated />
using System;
using AntilopaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Antilopa.Migrations
{
    [DbContext(typeof(AntilopaApi.Data.ApplicationDbContext))]
    [Migration("20190808182718_CreatedUpdatedAt")]
    partial class CreatedUpdatedAt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AntilopaApi.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Model");

                    b.Property<string>("Nickname");

                    b.Property<int?>("OwnerId");

                    b.Property<string>("PicUrl");

                    b.Property<string>("RegistrationNr");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("AntilopaApi.Models.Maintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CarId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("PlannedFrom");

                    b.Property<DateTime>("PlannedTo");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Maintenance");
                });

            modelBuilder.Entity("AntilopaApi.Models.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("AntilopaApi.Models.Car", b =>
                {
                    b.HasOne("AntilopaApi.Models.Owner", "Owner")
                        .WithMany("Cars")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("AntilopaApi.Models.Maintenance", b =>
                {
                    b.HasOne("AntilopaApi.Models.Car", "Car")
                        .WithMany("Maintenance")
                        .HasForeignKey("CarId");
                });
#pragma warning restore 612, 618
        }
    }
}
