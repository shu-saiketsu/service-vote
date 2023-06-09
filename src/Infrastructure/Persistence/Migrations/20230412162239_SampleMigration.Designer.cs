﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Saiketsu.Service.Vote.Infrastructure.Persistence;

#nullable disable

namespace Saiketsu.Service.Vote.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230412162239_SampleMigration")]
    partial class SampleMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Saiketsu.Service.Vote.Domain.Entities.CandidateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id")
                        .HasName("pk_candidate");

                    b.ToTable("candidate", (string)null);
                });

            modelBuilder.Entity("Saiketsu.Service.Vote.Domain.Entities.ElectionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id")
                        .HasName("pk_election");

                    b.ToTable("election", (string)null);
                });

            modelBuilder.Entity("Saiketsu.Service.Vote.Domain.Entities.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Saiketsu.Service.Vote.Domain.Entities.VoteEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<int>("ElectionId")
                        .HasColumnType("integer")
                        .HasColumnName("election_id");

                    b.Property<int>("CandidateId")
                        .HasColumnType("integer")
                        .HasColumnName("candidate_id");

                    b.HasKey("UserId", "ElectionId")
                        .HasName("pk_vote");

                    b.HasIndex("CandidateId")
                        .HasDatabaseName("ix_vote_candidate_id");

                    b.HasIndex("ElectionId")
                        .HasDatabaseName("ix_vote_election_id");

                    b.ToTable("vote", (string)null);
                });

            modelBuilder.Entity("Saiketsu.Service.Vote.Domain.Entities.VoteEntity", b =>
                {
                    b.HasOne("Saiketsu.Service.Vote.Domain.Entities.CandidateEntity", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_vote_candidate_candidate_id");

                    b.HasOne("Saiketsu.Service.Vote.Domain.Entities.ElectionEntity", "Election")
                        .WithMany()
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_vote_election_election_id");

                    b.HasOne("Saiketsu.Service.Vote.Domain.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_vote_user_user_id");

                    b.Navigation("Candidate");

                    b.Navigation("Election");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
