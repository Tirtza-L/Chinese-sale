﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.DAL;

#nullable disable

namespace Project.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240705001914_withoutChanges")]
    partial class withoutChanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 9L, 14);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Project.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 17);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Password")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Project.Models.Gift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 45L, 25);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Gifts");
                });

            modelBuilder.Entity("Project.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 35L, 88);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("GiftId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("GiftId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Project.Models.Winner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 18L, 24);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("GiftId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("GiftId")
                        .IsUnique();

                    b.ToTable("Winners");
                });

            modelBuilder.Entity("Project.Models.Gift", b =>
                {
                    b.HasOne("Project.Models.Category", "Category1")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Project.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category1");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Project.Models.Sale", b =>
                {
                    b.HasOne("Project.Models.Customer", "Customer")
                        .WithMany("SaleList")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Project.Models.Gift", "Gift")
                        .WithMany("SaleList")
                        .HasForeignKey("GiftId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Gift");
                });

            modelBuilder.Entity("Project.Models.Winner", b =>
                {
                    b.HasOne("Project.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Models.Gift", "Gift")
                        .WithMany()
                        .HasForeignKey("GiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Gift");
                });

            modelBuilder.Entity("Project.Models.Customer", b =>
                {
                    b.Navigation("SaleList");
                });

            modelBuilder.Entity("Project.Models.Gift", b =>
                {
                    b.Navigation("SaleList");
                });
#pragma warning restore 612, 618
        }
    }
}
