﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saitynu_projektas.Models;

namespace Saitynu_projektas.Migrations
{
    [DbContext(typeof(ClientContext))]
    [Migration("20191118160139_fk fixing")]
    partial class fkfixing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Saitynu_projektas.Models.Registration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientUserId");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<int>("ServiceId");

                    b.Property<int>("TimeId");

                    b.HasKey("RegistrationId");

                    b.HasIndex("ClientUserId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TimeId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("Saitynu_projektas.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Saitynu_projektas.Models.Time", b =>
                {
                    b.Property<int>("TimeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtistUserId");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsWorking");

                    b.HasKey("TimeId");

                    b.HasIndex("ArtistUserId");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("Saitynu_projektas.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNr")
                        .HasColumnType("varchar(9)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Saitynu_projektas.Models.Registration", b =>
                {
                    b.HasOne("Saitynu_projektas.Models.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Saitynu_projektas.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Saitynu_projektas.Models.Time", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Saitynu_projektas.Models.Time", b =>
                {
                    b.HasOne("Saitynu_projektas.Models.User", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
