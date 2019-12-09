using System.Linq;
using Microsoft.EntityFrameworkCore;
using msac_tpa_new.Entities;
using msac_tpa_new.Entities.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace msac_tpa_new.EF
{
    public class SportmenContext : DbContext
    {
        public DbSet<Sportman> SportMans { get; set; }
        public DbSet<Attestation> Attestations { get; set; }
        public DbSet<Belt> Belts { get; set; }
        public DbSet<Comission> Comissions { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<AttestationUserBelt> AttestationUserBelts { get; set; }
        public DbSet<SportmanComission> SportmanComissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public SportmenContext(DbContextOptions<SportmenContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            builder.Entity<Attestation>()
                .HasOne(p => p.Region)
                .WithMany(p => p.Attestations)
                .HasForeignKey(sc => sc.RegionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new SportmanComissionConfiguration());
            builder.ApplyConfiguration(new AttestationUserBeltConfiguration());

            builder.Entity<AttestationUserBelt>()
                .HasKey(x => new { x.AttestationId, x.SportmanId, x.BeltId });

            builder.Entity<AttestationUserBelt>()
                .HasOne(p => p.Attestation)
                .WithMany(p => p.AttestationUserBelts)
                .HasForeignKey(sc => sc.AttestationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AttestationUserBelt>()
                .HasOne(p => p.Sportman)
                .WithMany(p => p.AttestationUserBelts)
                .HasForeignKey(sc => sc.SportmanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AttestationUserBelt>()
                .HasOne(p => p.Belt)
                .WithMany(p => p.AttestationUserBelts)
                .HasForeignKey(sc => sc.BeltId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }

    public class SportmanComissionConfiguration : IEntityTypeConfiguration<SportmanComission>
    {
        public void Configure(EntityTypeBuilder<SportmanComission> builder)
        {
            builder.ToTable("SportmanComissions")
                .HasKey(x => new { x.SportmanId, x.ComissionId });

            builder.ToTable("SportmanComissions")
                .HasOne(p => p.Sportman)
                .WithMany(p => p.SportmanComissions)
                .HasForeignKey(sc => sc.SportmanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("SportmanComissions")
                .HasOne(p => p.Comission)
                .WithMany(p => p.SportmanComissions)
                .HasForeignKey(sc => sc.ComissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class AttestationUserBeltConfiguration : IEntityTypeConfiguration<AttestationUserBelt>
    {
        public void Configure(EntityTypeBuilder<AttestationUserBelt> builder)
        {

        }
    }
}
