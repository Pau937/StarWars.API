﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarWars.DataAccess.Data;

namespace StarWars.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200223192346_Friendship")]
    partial class Friendship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StarWars.Core.Models.Appearance", b =>
                {
                    b.Property<int>("EpisodeId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.HasKey("EpisodeId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("Appearances");
                });

            modelBuilder.Entity("StarWars.Core.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlanetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanetId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("StarWars.Core.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("StarWars.Core.Models.Friendship", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("FriendId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("StarWars.Core.Models.Planet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("StarWars.Core.Models.Appearance", b =>
                {
                    b.HasOne("StarWars.Core.Models.Character", "Character")
                        .WithMany("Appearances")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StarWars.Core.Models.Episode", "Episode")
                        .WithMany("Appearances")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("StarWars.Core.Models.Character", b =>
                {
                    b.HasOne("StarWars.Core.Models.Planet", "Planet")
                        .WithMany()
                        .HasForeignKey("PlanetId");
                });

            modelBuilder.Entity("StarWars.Core.Models.Friendship", b =>
                {
                    b.HasOne("StarWars.Core.Models.Character", "Character")
                        .WithMany("CharacterFriends")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StarWars.Core.Models.Character", "Friend")
                        .WithMany("FriendCharacters")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}