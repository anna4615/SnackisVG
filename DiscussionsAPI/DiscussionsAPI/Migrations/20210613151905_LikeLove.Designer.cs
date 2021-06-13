﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostsAPI.Data;

namespace PostsAPI.Migrations
{
    [DbContext(typeof(SnackisContext))]
    [Migration("20210613151905_LikeLove")]
    partial class LikeLove
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PostsAPI.Models.Forum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Forum");
                });

            modelBuilder.Entity("PostsAPI.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOffensiv")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfLike")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLove")
                        .HasColumnType("int");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("PostsAPI.Models.PostImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostImage");
                });

            modelBuilder.Entity("PostsAPI.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ForumId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ForumId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("PostsAPI.Models.Post", b =>
                {
                    b.HasOne("PostsAPI.Models.Post", null)
                        .WithMany("Posts")
                        .HasForeignKey("PostId");

                    b.HasOne("PostsAPI.Models.Subject", "Subject")
                        .WithMany("Posts")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("PostsAPI.Models.PostImage", b =>
                {
                    b.HasOne("PostsAPI.Models.Post", null)
                        .WithMany("Images")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostsAPI.Models.Subject", b =>
                {
                    b.HasOne("PostsAPI.Models.Forum", "Forum")
                        .WithMany("Subjects")
                        .HasForeignKey("ForumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Forum");
                });

            modelBuilder.Entity("PostsAPI.Models.Forum", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("PostsAPI.Models.Post", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("PostsAPI.Models.Subject", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
