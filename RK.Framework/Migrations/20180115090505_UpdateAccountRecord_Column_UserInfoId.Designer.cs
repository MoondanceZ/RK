﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RK.Framework.Database;
using System;

namespace RK.Framework.Migrations
{
    [DbContext(typeof(RkDbContext))]
    [Migration("20180115090505_UpdateAccountRecord_Column_UserInfoId")]
    partial class UpdateAccountRecord_Column_UserInfoId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("RK.Model.AccountRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountRecordTypeId");

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("DeletedTime");

                    b.Property<int?>("RecordTypeId");

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdatedTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.Property<int>("UserInfoId");

                    b.HasKey("Id");

                    b.HasIndex("RecordTypeId");

                    b.ToTable("AccountRecord");
                });

            modelBuilder.Entity("RK.Model.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.Property<int?>("UserInfoId");

                    b.HasKey("Id");

                    b.ToTable("AccountType");
                });

            modelBuilder.Entity("RK.Model.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .HasMaxLength(32);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Phone")
                        .HasMaxLength(32);

                    b.Property<int>("Sex");

                    b.Property<DateTime>("UpdatedTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("RK.Model.AccountRecord", b =>
                {
                    b.HasOne("RK.Model.AccountType", "RecordType")
                        .WithMany()
                        .HasForeignKey("RecordTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
