using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AMSWebApi.Models;

public partial class AssetsDbContext : DbContext
{
    public AssetsDbContext()
    {
    }

    public AssetsDbContext(DbContextOptions<AssetsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetDefinition> AssetDefinitions { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Assets_DB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Assets__434923526CE4AE2D");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.AssetDefinition).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AssetDefinitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assets_AssetDefinition");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.Assets)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assets_PurchaseOrder");
        });

        modelBuilder.Entity<AssetDefinition>(entity =>
        {
            entity.HasKey(e => e.AssetDefinitionId).HasName("PK__AssetDef__02A2EFC52458E7AE");

            entity.ToTable("AssetDefinition");

            entity.Property(e => e.AssetName).HasMaxLength(100);

            entity.HasOne(d => d.AssetType).WithMany(p => p.AssetDefinitions)
                .HasForeignKey(d => d.AssetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetDefinition_AssetType");
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.HasKey(e => e.AssetTypeId).HasName("PK__AssetTyp__FD33C2C2813F4666");

            entity.ToTable("AssetType");

            entity.HasIndex(e => e.TypeName, "UQ__AssetTyp__D4E7DFA8082ADDF5").IsUnique();

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PK__Purchase__036BACA423563FA8");

            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.Status).HasMaxLength(100);

            entity.HasOne(d => d.AssetDefinition).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.AssetDefinitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrder_AssetDefinition");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AF0123C9D");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160100EF6F0").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C42529EE7");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E411BDB0BA").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
