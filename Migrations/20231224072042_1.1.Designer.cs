﻿// <auto-generated />
using System;
using MarefiyaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MarefiyaApi.Migrations
{
    [DbContext(typeof(MarefiyaDbContext))]
    [Migration("20231224072042_1.1")]
    partial class _11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MarefiyaApi.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("NoOfGuests")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNo")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"), 1L, 1);

                    b.Property<double>("CheapestPrice")
                        .HasColumnType("float");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int>("HotelManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Photos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("HotelId");

                    b.HasIndex("HotelManagerId")
                        .IsUnique();

                    b.ToTable("Hotels", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("CustomerServiceRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<decimal>("HygieneRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LocationRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Features")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("money");

                    b.Property<string>("RoomNos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoomId");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Admin", b =>
                {
                    b.HasBaseType("MarefiyaApi.Models.User");

                    b.ToTable("Admins", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Customer", b =>
                {
                    b.HasBaseType("MarefiyaApi.Models.User");

                    b.ToTable("Customers", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.HotelManager", b =>
                {
                    b.HasBaseType("MarefiyaApi.Models.User");

                    b.ToTable("HotelManagers", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Receptionist", b =>
                {
                    b.HasBaseType("MarefiyaApi.Models.User");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasIndex("HotelId");

                    b.ToTable("Receptionists", "Marefiya");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Booking", b =>
                {
                    b.HasOne("MarefiyaApi.Models.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarefiyaApi.Models.Hotel", "Hotel")
                        .WithMany("Bookings")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarefiyaApi.Models.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Hotel");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Hotel", b =>
                {
                    b.HasOne("MarefiyaApi.Models.HotelManager", "HotelManager")
                        .WithOne("Hotel")
                        .HasForeignKey("MarefiyaApi.Models.Hotel", "HotelManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HotelManager");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Review", b =>
                {
                    b.HasOne("MarefiyaApi.Models.Customer", null)
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId");

                    b.HasOne("MarefiyaApi.Models.Hotel", "Hotel")
                        .WithMany("Reviews")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarefiyaApi.Models.Room", "Room")
                        .WithMany("Reviews")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarefiyaApi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Room", b =>
                {
                    b.HasOne("MarefiyaApi.Models.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Admin", b =>
                {
                    b.HasOne("MarefiyaApi.Models.User", null)
                        .WithOne()
                        .HasForeignKey("MarefiyaApi.Models.Admin", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MarefiyaApi.Models.Customer", b =>
                {
                    b.HasOne("MarefiyaApi.Models.User", null)
                        .WithOne()
                        .HasForeignKey("MarefiyaApi.Models.Customer", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MarefiyaApi.Models.HotelManager", b =>
                {
                    b.HasOne("MarefiyaApi.Models.User", null)
                        .WithOne()
                        .HasForeignKey("MarefiyaApi.Models.HotelManager", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MarefiyaApi.Models.Receptionist", b =>
                {
                    b.HasOne("MarefiyaApi.Models.Hotel", "Hotel")
                        .WithMany("Receptionists")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarefiyaApi.Models.User", null)
                        .WithOne()
                        .HasForeignKey("MarefiyaApi.Models.Receptionist", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Hotel", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Receptionists");

                    b.Navigation("Reviews");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Room", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MarefiyaApi.Models.Customer", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MarefiyaApi.Models.HotelManager", b =>
                {
                    b.Navigation("Hotel")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
