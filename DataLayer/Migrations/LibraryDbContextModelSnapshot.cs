﻿// <auto-generated />
using System;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataLayer.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Aurther")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("PDF")
                        .HasColumnType("varbinary(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShelfId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShelfId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("DataLayer.Models.LookUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LookUpCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LookUpCategoryId");

                    b.ToTable("LookUps");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "FAN",
                            CreationDate = new DateTime(2024, 7, 8, 9, 26, 0, 211, DateTimeKind.Utc).AddTicks(162),
                            LookUpCategoryId = 1,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 2,
                            Code = "NOV",
                            CreationDate = new DateTime(2024, 7, 8, 9, 26, 0, 211, DateTimeKind.Utc).AddTicks(163),
                            LookUpCategoryId = 1,
                            Name = "Novel"
                        },
                        new
                        {
                            Id = 3,
                            Code = "HIS",
                            CreationDate = new DateTime(2024, 7, 8, 9, 26, 0, 211, DateTimeKind.Utc).AddTicks(165),
                            LookUpCategoryId = 1,
                            Name = "History"
                        },
                        new
                        {
                            Id = 4,
                            Code = "MED",
                            CreationDate = new DateTime(2024, 7, 8, 9, 26, 0, 211, DateTimeKind.Utc).AddTicks(166),
                            LookUpCategoryId = 1,
                            Name = "Medical"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.LookUpCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LookUpCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "1",
                            CreationDate = new DateTime(2024, 7, 8, 9, 26, 0, 211, DateTimeKind.Utc).AddTicks(33),
                            Name = "TypeOfShelf"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.Shelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsActived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Shelfs");
                });

            modelBuilder.Entity("DataLayer.Models.Book", b =>
                {
                    b.HasOne("DataLayer.Models.Shelf", "Shelf")
                        .WithMany("Books")
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("DataLayer.Models.LookUp", b =>
                {
                    b.HasOne("DataLayer.Models.LookUpCategory", null)
                        .WithMany("lookUps")
                        .HasForeignKey("LookUpCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.Shelf", b =>
                {
                    b.HasOne("DataLayer.Models.LookUp", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("DataLayer.Models.LookUpCategory", b =>
                {
                    b.Navigation("lookUps");
                });

            modelBuilder.Entity("DataLayer.Models.Shelf", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
