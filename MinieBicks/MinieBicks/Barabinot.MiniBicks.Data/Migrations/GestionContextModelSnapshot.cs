﻿// <auto-generated />
using System;
using Barabinot.MiniBicks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Barabinot.MiniBicks.Data.Migrations
{
    [DbContext(typeof(GestionContext))]
    partial class GestionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1");

            modelBuilder.Entity("Barabinot.MiniBicks.Data.Conge", b =>
                {
                    b.Property<int>("CongeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NbConge")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NbRTT")
                        .HasColumnType("INTEGER");

                    b.HasKey("CongeId");

                    b.ToTable("Conge");
                });

            modelBuilder.Entity("Barabinot.MiniBicks.Data.Utilisateurs", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CodePostal")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CongeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdSuperieur")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .HasColumnType("TEXT");

                    b.Property<string>("Pays")
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ville")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("CongeId");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("Barabinot.MiniBicks.Data.Utilisateurs", b =>
                {
                    b.HasOne("Barabinot.MiniBicks.Data.Conge", "Conge")
                        .WithMany()
                        .HasForeignKey("CongeId");
                });
#pragma warning restore 612, 618
        }
    }
}
