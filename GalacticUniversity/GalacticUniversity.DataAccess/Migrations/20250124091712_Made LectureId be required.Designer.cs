﻿// <auto-generated />
using System;
using GalacticUniversity.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GalacticUniversity.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250124091712_Made LectureId be required")]
    partial class MadeLectureIdberequired
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GalacticUniversity.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentID"));

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CommentID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseID");

                    b.HasIndex("CategoryID");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Lecture", b =>
                {
                    b.Property<int>("LectureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LectureID"));

                    b.Property<int?>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LectureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LectureID");

                    b.HasIndex("CourseID");

                    b.ToTable("lectures");
                });

            modelBuilder.Entity("GalacticUniversity.Models.LectureResource", b =>
                {
                    b.Property<int>("ResourceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResourceID"));

                    b.Property<int>("LectureID")
                        .HasColumnType("int");

                    b.Property<string>("ResourcePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResourceID");

                    b.HasIndex("LectureID");

                    b.ToTable("lectureResources");
                });

            modelBuilder.Entity("GalacticUniversity.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Comment", b =>
                {
                    b.HasOne("GalacticUniversity.Models.Course", "Course")
                        .WithMany("Comments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GalacticUniversity.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Course", b =>
                {
                    b.HasOne("GalacticUniversity.Models.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Lecture", b =>
                {
                    b.HasOne("GalacticUniversity.Models.Course", "Course")
                        .WithMany("Lectures")
                        .HasForeignKey("CourseID");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("GalacticUniversity.Models.LectureResource", b =>
                {
                    b.HasOne("GalacticUniversity.Models.Lecture", "Lecture")
                        .WithMany("LectureResources")
                        .HasForeignKey("LectureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Category", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Course", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("GalacticUniversity.Models.Lecture", b =>
                {
                    b.Navigation("LectureResources");
                });

            modelBuilder.Entity("GalacticUniversity.Models.User", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
