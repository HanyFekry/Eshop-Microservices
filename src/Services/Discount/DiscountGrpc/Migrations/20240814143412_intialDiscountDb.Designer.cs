﻿// <auto-generated />
using DiscountGrpc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiscountGrpc.Migrations
{
    [DbContext(typeof(DiscountDbContext))]
    [Migration("20240814143412_intialDiscountDb")]
    partial class intialDiscountDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("DiscountGrpc.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 1,
                            Description = "this product 4 test",
                            ProductName = "Product 1"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 3,
                            Description = "this product 4 test",
                            ProductName = "Product 2"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 2,
                            Description = "this product 4 test",
                            ProductName = "Product 3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
