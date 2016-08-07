using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SweetHome.Models;

namespace SweetHome.Migrations
{
    [DbContext(typeof(SweetHomeContext))]
    [Migration("20160807102222_change-json-lists-to-tables")]
    partial class changejsonliststotables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("SweetHome.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimalId");

                    b.Property<string>("Comment");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SweetHome.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimalId");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<int?>("ShelterId");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("ShelterId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("SweetHome.Models.Shelter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasAnnotation("MaxLength", 160);

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Info");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerName")
                        .HasAnnotation("MaxLength", 160);

                    b.Property<string>("Url");

                    b.Property<string>("VKGroup");

                    b.HasKey("Id");

                    b.ToTable("Shelters");
                });

            modelBuilder.Entity("SweetHome.Models.ShelterAnimal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimalType");

                    b.Property<DateTime?>("BirthDay");

                    b.Property<int>("Color");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Gender");

                    b.Property<string>("Info")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<bool>("IsForFlat");

                    b.Property<bool>("IsForHome");

                    b.Property<bool>("IsHappy");

                    b.Property<bool>("IsHealthy");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerName");

                    b.Property<int>("PlaceType");

                    b.Property<int?>("ShelterId");

                    b.Property<int>("Size");

                    b.Property<bool>("Toilet");

                    b.HasKey("Id");

                    b.HasIndex("ShelterId");

                    b.ToTable("ShelterAnimals");
                });

            modelBuilder.Entity("SweetHome.Models.Image", b =>
                {
                    b.HasOne("SweetHome.Models.ShelterAnimal", "Animal")
                        .WithMany("Images")
                        .HasForeignKey("AnimalId");
                });

            modelBuilder.Entity("SweetHome.Models.Phone", b =>
                {
                    b.HasOne("SweetHome.Models.ShelterAnimal", "Animal")
                        .WithMany("Phones")
                        .HasForeignKey("AnimalId");

                    b.HasOne("SweetHome.Models.Shelter", "Shelter")
                        .WithMany("Phones")
                        .HasForeignKey("ShelterId");
                });

            modelBuilder.Entity("SweetHome.Models.ShelterAnimal", b =>
                {
                    b.HasOne("SweetHome.Models.Shelter", "Shelter")
                        .WithMany("Animals")
                        .HasForeignKey("ShelterId");
                });
        }
    }
}
