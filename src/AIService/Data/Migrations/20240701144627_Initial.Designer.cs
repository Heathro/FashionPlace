﻿// <auto-generated />
using System;
using AIService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AIService.Data.Migrations
{
    [DbContext(typeof(AIDbContext))]
    [Migration("20240701144627_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AIService.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<bool>("IsUser")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MessageThreadId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MessageThreadId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("AIService.Entities.MessageThread", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConnectionId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MessageThreads");
                });

            modelBuilder.Entity("AIService.Entities.Message", b =>
                {
                    b.HasOne("AIService.Entities.MessageThread", "MessageThread")
                        .WithMany("Messages")
                        .HasForeignKey("MessageThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MessageThread");
                });

            modelBuilder.Entity("AIService.Entities.MessageThread", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}