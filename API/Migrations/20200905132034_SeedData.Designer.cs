﻿// <auto-generated />
using System;
using LikeButton.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LikeButton.API.Migrations
{
    [DbContext(typeof(MysqlDataContext))]
    [Migration("20200905132034_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LikeButton.Domain.DB.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Abstract")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("post");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Abstract = "EarningType",
                            DatePublished = new DateTime(2020, 9, 5, 18, 50, 33, 904, DateTimeKind.Local).AddTicks(9250),
                            Title = "EarningType"
                        },
                        new
                        {
                            Id = 2,
                            Abstract = "AddressType",
                            DatePublished = new DateTime(2020, 9, 5, 18, 50, 33, 906, DateTimeKind.Local).AddTicks(291),
                            Title = "AddressType"
                        },
                        new
                        {
                            Id = 3,
                            Abstract = "ContactType",
                            DatePublished = new DateTime(2020, 9, 5, 18, 50, 33, 906, DateTimeKind.Local).AddTicks(332),
                            Title = "ContactType"
                        });
                });

            modelBuilder.Entity("LikeButton.Domain.DB.PostLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserAgent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("UserLike")
                        .HasColumnType("bit(1)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("postlike");
                });

            modelBuilder.Entity("LikeButton.Domain.DB.PostLike", b =>
                {
                    b.HasOne("LikeButton.Domain.DB.Post", "Post")
                        .WithMany("PostLikes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
