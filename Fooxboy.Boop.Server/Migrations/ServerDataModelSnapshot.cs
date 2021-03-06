﻿// <auto-generated />
using Fooxboy.Boop.Server.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fooxboy.Boop.Server.Migrations
{
    [DbContext(typeof(ServerData))]
    partial class ServerDataModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("Fooxboy.Boop.Server.Database.Message", b =>
                {
                    b.Property<long>("MsgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RecieverId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("SenderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<long>("Time")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UsersReaded")
                        .HasColumnType("TEXT");

                    b.HasKey("MsgId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Fooxboy.Boop.Server.Database.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PathProfilePic")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fooxboy.Boop.Server.Database.UsersChat", b =>
                {
                    b.Property<long>("LocalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("LastMessage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Messages")
                        .HasColumnType("TEXT");

                    b.Property<long>("Owner")
                        .HasColumnType("INTEGER");

                    b.HasKey("LocalId");

                    b.ToTable("UsersChats");
                });
#pragma warning restore 612, 618
        }
    }
}
