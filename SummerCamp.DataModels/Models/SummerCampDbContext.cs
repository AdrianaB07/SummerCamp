using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SummerCamp.DataModels.Models;

public partial class SummerCampDbContext : DbContext
{
    public SummerCampDbContext()
    {
    }

    public SummerCampDbContext(DbContextOptions<SummerCampDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<CompetitionMatch> CompetitionMatches { get; set; }

    public virtual DbSet<CompetitionTeam> CompetitionTeams { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Sponsor> Sponsors { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamSponsor> TeamSponsors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SummerCamp");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coach__3214EC07D5425E49");

            entity.ToTable("Coach");

            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC079394E50B");

            entity.ToTable("Competition");

            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Sponsor).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.SponsorId)
                .HasConstraintName("FK__Competiti__Spons__18EBB532");
        });

        modelBuilder.Entity<CompetitionMatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07D257006C");

            entity.ToTable("CompetitionMatch");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.CompetitionMatchAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__AwayT__1CBC4616");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionMatches)
                .HasForeignKey(d => d.CompetitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__Compe__1AD3FDA4");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.CompetitionMatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__HomeT__1BC821DD");
        });

        modelBuilder.Entity<CompetitionTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC07B0DC16F3");

            entity.ToTable("CompetitionTeam");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionTeams)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__Compe__19DFD96B");

            entity.HasOne(d => d.Team).WithMany(p => p.CompetitionTeams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Competiti__TeamI__5FB337D6");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__3214EC070136E834");

            entity.ToTable("Player");

            entity.HasIndex(e => new { e.TeamId, e.ShirtNumber }, "UQ_Team_Name").IsUnique();

            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Player__TeamId__4F7CD00D");
        });

        modelBuilder.Entity<Sponsor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sponsor__3214EC0704B06540");

            entity.ToTable("Sponsor");

            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team__3214EC07231C8EC5");

            entity.ToTable("Team");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NickName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Coach).WithMany(p => p.Teams)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK__Team__CoachId__403A8C7D");
        });

        modelBuilder.Entity<TeamSponsor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeamSpon__3214EC07E13B0716");

            entity.ToTable("TeamSponsor");

            entity.HasIndex(e => new { e.TeamId, e.SponsorId }, "UQ_Team_Sponsor").IsUnique();

            entity.HasOne(d => d.Sponsor).WithMany(p => p.TeamSponsors)
                .HasForeignKey(d => d.SponsorId)
                .HasConstraintName("FK__TeamSpons__Spons__5629CD9C");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamSponsors)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__TeamSpons__TeamI__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
