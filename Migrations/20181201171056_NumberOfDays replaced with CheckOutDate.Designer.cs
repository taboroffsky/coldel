﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using coldel.Persistance;

namespace coldel.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    [Migration("20181201171056_NumberOfDays replaced with CheckOutDate")]
    partial class NumberOfDaysreplacedwithCheckOutDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("coldel.Persistance.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("coldel.Persistance.Models.Registration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("ClientId");

                    b.Property<Guid?>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("RoomId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("coldel.Persistance.Models.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<int>("Price");

                    b.Property<int>("RoomType");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("coldel.Persistance.Models.Registration", b =>
                {
                    b.HasOne("coldel.Persistance.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("coldel.Persistance.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");
                });
#pragma warning restore 612, 618
        }
    }
}
