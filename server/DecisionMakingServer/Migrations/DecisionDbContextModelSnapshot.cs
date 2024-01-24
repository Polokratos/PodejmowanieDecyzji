﻿// <auto-generated />
using System;
using DecisionMakingServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DecisionMakingServer.Migrations
{
    [DbContext(typeof(DecisionDbContext))]
    partial class DecisionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DecisionMakingServer.Models.Alternative", b =>
                {
                    b.Property<int>("AlternativeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlternativeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RankingId")
                        .HasColumnType("int");

                    b.HasKey("AlternativeId");

                    b.HasIndex("RankingId");

                    b.ToTable("Alternatives");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerId"));

                    b.Property<int>("CriterionId")
                        .HasColumnType("int");

                    b.Property<int>("LeftAlternativeId")
                        .HasColumnType("int");

                    b.Property<int>("RankingId")
                        .HasColumnType("int");

                    b.Property<int>("RightAlternativeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("AnswerId");

                    b.HasIndex("CriterionId");

                    b.HasIndex("LeftAlternativeId");

                    b.HasIndex("RankingId");

                    b.HasIndex("RightAlternativeId");

                    b.HasIndex("UserId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Criterion", b =>
                {
                    b.Property<int>("CriterionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CriterionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("RankingId")
                        .HasColumnType("int");

                    b.HasKey("CriterionId");

                    b.HasIndex("ParentId");

                    b.HasIndex("RankingId");

                    b.ToTable("Criteria");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.CriterionAnswer", b =>
                {
                    b.Property<int>("CriterionAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CriterionAnswerId"));

                    b.Property<int>("LeftCriterionId")
                        .HasColumnType("int");

                    b.Property<int>("RankingId")
                        .HasColumnType("int");

                    b.Property<int>("RightCriterionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("CriterionAnswerId");

                    b.HasIndex("LeftCriterionId");

                    b.HasIndex("RankingId");

                    b.HasIndex("RightCriterionId");

                    b.HasIndex("UserId");

                    b.ToTable("CriterionAnswers");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Ranking", b =>
                {
                    b.Property<int>("RankingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RankingId"));

                    b.Property<int>("AggregationMethod")
                        .HasColumnType("int");

                    b.Property<string>("AskOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CalculationMethod")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScaleId")
                        .HasColumnType("int");

                    b.HasKey("RankingId");

                    b.HasIndex("ScaleId")
                        .IsUnique();

                    b.ToTable("Rankings");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Result", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultId"));

                    b.Property<int>("AlternativeId")
                        .HasColumnType("int");

                    b.Property<int>("RankingId")
                        .HasColumnType("int");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.HasKey("ResultId");

                    b.HasIndex("AlternativeId");

                    b.HasIndex("RankingId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Scale", b =>
                {
                    b.Property<int>("ScaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScaleId"));

                    b.Property<int>("RankingId")
                        .HasColumnType("int");

                    b.HasKey("ScaleId");

                    b.ToTable("Scales");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.ScaleValue", b =>
                {
                    b.Property<int>("ScaleValueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScaleValueID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScaleId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("ScaleValueID");

                    b.HasIndex("ScaleId");

                    b.ToTable("ScaleValues");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.UserRanking", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RankingId")
                        .HasColumnType("int");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RankingId");

                    b.HasIndex("RankingId");

                    b.ToTable("UserRankings");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Alternative", b =>
                {
                    b.HasOne("DecisionMakingServer.Models.Ranking", "Ranking")
                        .WithMany("Alternatives")
                        .HasForeignKey("RankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ranking");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Answer", b =>
                {
                    b.HasOne("DecisionMakingServer.Models.Criterion", "Criterion")
                        .WithMany("Answers")
                        .HasForeignKey("CriterionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.Alternative", "LeftAlternative")
                        .WithMany("LeftAnswers")
                        .HasForeignKey("LeftAlternativeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.Ranking", "Ranking")
                        .WithMany("Answers")
                        .HasForeignKey("RankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.Alternative", "RightAlternative")
                        .WithMany("RightAnswers")
                        .HasForeignKey("RightAlternativeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Criterion");

                    b.Navigation("LeftAlternative");

                    b.Navigation("Ranking");

                    b.Navigation("RightAlternative");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Criterion", b =>
                {
                    b.HasOne("DecisionMakingServer.Models.Criterion", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("DecisionMakingServer.Models.Ranking", "Ranking")
                        .WithMany("Criteria")
                        .HasForeignKey("RankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Ranking");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.CriterionAnswer", b =>
                {
                    b.HasOne("DecisionMakingServer.Models.Criterion", "LeftCriterion")
                        .WithMany()
                        .HasForeignKey("LeftCriterionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.Ranking", "Ranking")
                        .WithMany("CriterionAnswers")
                        .HasForeignKey("RankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.Criterion", "RightCriterion")
                        .WithMany()
                        .HasForeignKey("RightCriterionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeftCriterion");

                    b.Navigation("Ranking");

                    b.Navigation("RightCriterion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Ranking", b =>
                {
                    b.HasOne("DecisionMakingServer.Models.Scale", "Scale")
                        .WithOne("Ranking")
                        .HasForeignKey("DecisionMakingServer.Models.Ranking", "ScaleId");

                    b.Navigation("Scale");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Result", b =>
                {
                    b.HasOne("DecisionMakingServer.Models.Alternative", "Alternative")
                        .WithMany("Results")
                        .HasForeignKey("AlternativeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.Ranking", "Ranking")
                        .WithMany("Results")
                        .HasForeignKey("RankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alternative");

                    b.Navigation("Ranking");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.ScaleValue", b =>
                {
                    b.HasOne("DecisionMakingServer.Models.Scale", "Scale")
                        .WithMany()
                        .HasForeignKey("ScaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scale");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.UserRanking", b =>
                {
                    b.HasOne("DecisionMakingServer.Models.Ranking", "Ranking")
                        .WithMany("UserRankings")
                        .HasForeignKey("RankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecisionMakingServer.Models.User", "User")
                        .WithMany("UserRankings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ranking");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Alternative", b =>
                {
                    b.Navigation("LeftAnswers");

                    b.Navigation("Results");

                    b.Navigation("RightAnswers");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Criterion", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Ranking", b =>
                {
                    b.Navigation("Alternatives");

                    b.Navigation("Answers");

                    b.Navigation("Criteria");

                    b.Navigation("CriterionAnswers");

                    b.Navigation("Results");

                    b.Navigation("UserRankings");
                });

            modelBuilder.Entity("DecisionMakingServer.Models.Scale", b =>
                {
                    b.Navigation("Ranking")
                        .IsRequired();
                });

            modelBuilder.Entity("DecisionMakingServer.Models.User", b =>
                {
                    b.Navigation("UserRankings");
                });
#pragma warning restore 612, 618
        }
    }
}
