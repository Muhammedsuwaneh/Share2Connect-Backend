﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Share2Connect.Api.Context;

#nullable disable

namespace Share2Connect.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221227182507_imagePropertyUpdated")]
    partial class imagePropertyUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Share2Connect.Api.Models.Announcement", b =>
                {
                    b.Property<int>("post_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("post_id"), 1L, 1);

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("dataId")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("post_id");

                    b.HasIndex("dataId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("Share2Connect.Api.Models.AnnouncementData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("adClubName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adDateText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adDescText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("adImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("adNameText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adPlaceGPS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adPlaceText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adPriceText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adRouteEndGPS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adRouteEndText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adRouteStartGPS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adRouteStartText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adSeatText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adTicketText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AnnouncementData");
                });

            modelBuilder.Entity("Share2Connect.Api.Models.Participant", b =>
                {
                    b.Property<int>("user")
                        .HasColumnType("int");

                    b.Property<int?>("AnnouncementDataId")
                        .HasColumnType("int");

                    b.HasKey("user");

                    b.HasIndex("AnnouncementDataId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("Share2Connect.Api.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"), 1L, 1);

                    b.Property<string>("userBio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userDepartment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("userImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("userMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userNameText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Share2Connect.Api.Models.Announcement", b =>
                {
                    b.HasOne("Share2Connect.Api.Models.AnnouncementData", "data")
                        .WithMany()
                        .HasForeignKey("dataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("data");
                });

            modelBuilder.Entity("Share2Connect.Api.Models.Participant", b =>
                {
                    b.HasOne("Share2Connect.Api.Models.AnnouncementData", null)
                        .WithMany("Participants")
                        .HasForeignKey("AnnouncementDataId");
                });

            modelBuilder.Entity("Share2Connect.Api.Models.AnnouncementData", b =>
                {
                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}